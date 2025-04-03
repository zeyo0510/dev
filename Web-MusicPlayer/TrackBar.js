// TrackBar.js
class TrackBar extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({ mode: 'open' });
    this._value = 0;
    this._max = 100;
    this._isDragging = false;
  }

  connectedCallback() {
    this.render();
    this.setupEventListeners();
  }

  static get observedAttributes() {
    return ['value', 'max'];
  }

  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'value') {
      this.value = parseFloat(newValue) || 0;
    } else if (name === 'max') {
      this.max = parseFloat(newValue) || 100;
    }
    this.updateFill();
  }

  get value() {
    return this._value;
  }

  set value(val) {
    this._value = Math.max(0, Math.min(val, this._max));
    this.setAttribute('value', this._value);
    this.updateFill();
  }

  get max() {
    return this._max;
  }

  set max(val) {
    this._max = Math.max(0, val);
    this.setAttribute('max', this._max);
    this.updateFill();
  }

  updateFill() {
    if (this.fillElement) {
      const percent = (this._value / this._max) * 100;
      this.fillElement.style.width = `${percent}%`;
    }
  }

  render() {
    this.shadowRoot.innerHTML = `
      <style>
        :host {
          display: block;
          height: 5px;
          background: #ccc;
          margin: 4px 16px;
          border-radius: 3px;
          overflow: hidden;
          position: relative;
          cursor: pointer;
        }
        
        .fill {
          width: 0%;
          height: 100%;
          background: #111;
          transition: width 0.1s;
        }
      </style>
      <div class="fill"></div>
    `;
    this.fillElement = this.shadowRoot.querySelector('.fill');
  }

  setupEventListeners() {
    this.addEventListener('click', (e) => this.handleClick(e));
    
    this.addEventListener('mousedown', (e) => {
      this._isDragging = true;
      this.handleDrag(e);
      e.preventDefault();
    });

    document.addEventListener('mousemove', (e) => {
      if (this._isDragging) {
        this.handleDrag(e);
      }
    });

    document.addEventListener('mouseup', () => {
      this._isDragging = false;
    });
  }

  handleClick(e) {
    const rect = this.getBoundingClientRect();
    const offsetX = e.clientX - rect.left;
    const percent = offsetX / rect.width;
    const newValue = percent * this._max;
    
    this.value = newValue;
    this.dispatchEvent(new CustomEvent('change', {
      detail: { value: this.value }
    }));
  }

  handleDrag(e) {
    const rect = this.getBoundingClientRect();
    let offsetX = e.clientX - rect.left;
    offsetX = Math.max(0, Math.min(offsetX, rect.width));
    const percent = offsetX / rect.width;
    const newValue = percent * this._max;
    
    this.value = newValue;
    this.dispatchEvent(new CustomEvent('change', {
      detail: { value: this.value }
    }));
  }
}

customElements.define('track-bar', TrackBar);