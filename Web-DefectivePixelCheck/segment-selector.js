class SegmentSelector extends HTMLElement {
  #shadow = undefined;
  /************************************************/
  
  
  
  
  
  constructor() {
    super();
    /************************************************/
    this.#shadow = this.attachShadow({ mode: 'closed' });
    /************************************************/
    this.#shadow.innerHTML = `
    <style>
    :host {
      /* appearance */
      overflow: hidden;
      margin: 0px;
      border-color: rgba(64, 64, 64, 1);
      border-radius: 8px;
      border-style: solid;
      border-width: 3px;
      padding: 0px;
      
      /* flexbox */
      display: flex;
      align-items: center;
      justify-content: center;
      
      /* layout */
      width: 100%;
      height: 20px;
    }
    </style>
    <slot></slot>
    `;
  }
  /************************************************/
  connectedCallback() {
    this.updateUI();
  }
  /************************************************/
  disconnectedCallback() {
    // do nothing...
  }
  /************************************************/
  static get observedAttributes() {
    let retValue = [];
    /************************************************/
    retValue.push('data-colors');
    retValue.push('data-current-color-index');
    /************************************************/
    return retValue;
  }
  /************************************************/
  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'data-colors') {
      this.#renderPresetColors();
    }
    /************************************************/
    if (name === 'data-current-color-index') {
      // do nothing...
    }
    /************************************************/
    this.updateUI();
  }
  /************************************************/
  
  
  
  
  
  get colors() {
    let retValue = this.getAttribute('data-colors');
    /************************************************/
    return retValue ? JSON.parse(retValue) : [];
  }
  /************************************************/
  set colors(value) {
    this.setAttribute('data-colors', JSON.stringify(value));
  }
  /************************************************/
  get currentColor() {
    let obj = this.querySelector(`segment-color[data-segment-index="${this.currentColorIndex}"]`);
    /************************************************/
    return obj.colorValue;
  }
  /************************************************/
  get currentColorName() {
    let obj = this.querySelector(`segment-color[data-segment-index="${this.currentColorIndex}"]`);
    /************************************************/
    return obj.colorName;
  }
  /************************************************/
  get currentColorIndex() {
    let retValue = parseInt(this.getAttribute('data-current-color-index'));
    /************************************************/
    return retValue ? retValue : 0;
  }
  /************************************************/
  set currentColorIndex(value) {
    this.setAttribute('data-current-color-index', parseInt(value));
    /************************************************/
    this.dispatchEvent(new CustomEvent('color-selected', {
      bubbles: true,
      detail: {
        index: value,
        color: this.currentColor
      }
    }));
  }
  /************************************************/
  
  
  
  
  
  nextColor() {
    this.currentColorIndex = (this.currentColorIndex + 1) % this.colors.length;
  }
  /************************************************/
  previousColor() {
    this.currentColorIndex = (this.currentColorIndex - 1 + this.colors.length) % this.colors.length;
  }
  /************************************************/
  swapColor(fromIndex, toIndex) {
    const element1 = this.querySelector(`segment-color[data-segment-index="${fromIndex}"]`);
    const element2 = this.querySelector(`segment-color[data-segment-index="${toIndex}"]`);
    /************************************************/
    [
      element1.colorValue, element1.colorName,
      element2.colorValue, element2.colorName,
    ] = [
      element2.colorValue, element2.colorName,
      element1.colorValue, element1.colorName,
    ];
    /************************************************/
    if (this.currentColorIndex === fromIndex) {
      this.currentColorIndex = toIndex;
    } else if (this.currentColorIndex === toIndex) {
      this.currentColorIndex = fromIndex;
    }
    /************************************************/
    this.dispatchEvent(new CustomEvent('color-swapped', {
      bubbles: true,
      detail: {
        fromIndex,
        toIndex
      }
    }));
  }
  /************************************************/
  #renderPresetColors() {
    this.innerHTML = '';
    /************************************************/
    this.colors.forEach((color, index) => {
      const segment = document.createElement('segment-color');
      /************************************************/
      segment.colorValue   = color.hex;
      segment.colorName    = color.name;
      segment.segmentIndex = index;
      /************************************************/
      this.appendChild(segment);
      /************************************************/
      segment.updateUI();
    });
  }
  /************************************************/
  updateUI() {
    const segments = this.querySelectorAll('segment-color');
    /************************************************/
    segments.forEach((segment, index) => {
      segment.updateUI();
    });
  }
}
/************************************************/
customElements.define('segment-selector', SegmentSelector);