// ToggleButton.js
class ToggleButton extends HTMLElement {
  constructor() {
    super();
    this.attachShadow({ mode: 'open' });
    this._isOn = false;
    this._onIcon = 'check';
    this._offIcon = 'close';
    this._color = '';
  }

  static get observedAttributes() {
    return ['onicon', 'officon', 'color'];
  }

  connectedCallback() {
    this.render();
    this.addEventListener('click', this.toggle);
    
    // 確保 Google Font 圖標已載入
    if (!document.querySelector('link[href="https://fonts.googleapis.com/icon?family=Material+Icons"]')) {
      const link = document.createElement('link');
      link.href = 'https://fonts.googleapis.com/icon?family=Material+Icons';
      link.rel = 'stylesheet';
      document.head.appendChild(link);
    }
  }

  disconnectedCallback() {
    this.removeEventListener('click', this.toggle);
  }

  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'onicon') {
      this._onIcon = newValue;
    } else if (name === 'officon') {
      this._offIcon = newValue;
    } else if (name === 'color') {
      this._color = newValue;
    }
    this.render();
  }

  get isOn() {
    return this._isOn;
  }

  set isOn(value) {
    this._isOn = value;
    this.render();
  }

  get onIcon() {
    return this._onIcon;
  }

  set onIcon(value) {
    this.setAttribute('onicon', value);
  }

  get offIcon() {
    return this._offIcon;
  }

  set offIcon(value) {
    this.setAttribute('officon', value);
  }

  get color() {
    return this._color;
  }

  set color(value) {
    this.setAttribute('color', value);
  }

  toggle() {
    this.isOn = !this.isOn;
    this.dispatchEvent(new CustomEvent('toggle', { detail: this.isOn }));
  }

  render() {
    this.shadowRoot.innerHTML = `
      <style>
        :host {
          display: inline-block;
          cursor: pointer;
        }
        button {
          background: none;
          border: none;
          font-size: 20px;
          cursor: pointer;
          color: ${this._isOn ? (this._color || '#333') : '#333'};
          transition: transform 0.2s, color 0.2s;
          outline: none;
          padding: 0;
          margin: 0;
        }
        button:hover {
          transform: scale(1.2);
        }
        .material-icons {
          font-family: 'Material Icons';
          font-weight: normal;
          font-style: normal;
          font-size: 24px;
          line-height: 1;
          letter-spacing: normal;
          text-transform: none;
          display: inline-block;
          white-space: nowrap;
          word-wrap: normal;
          direction: ltr;
          -webkit-font-feature-settings: 'liga';
          -webkit-font-smoothing: antialiased;
        }
      </style>
      <button title="${this.getAttribute('title') || ''}">
        <span class="material-icons">${this._isOn ? this._onIcon : this._offIcon}</span>
      </button>
    `;
  }
}

// 註冊自定義元素
customElements.define('toggle-button', ToggleButton);