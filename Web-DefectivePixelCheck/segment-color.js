class SegmentColor extends HTMLElement {
  #shadow = undefined;
  /************************************************/
  #colorInfo = undefined;
  /************************************************/
  
  
  
  
  
  constructor() {
    super();
    /************************************************/
    this.#shadow = this.attachShadow({ mode: 'closed' });
    /************************************************/
    this.#shadow.innerHTML = `
    <style>
    :host {
      /* background */
      --background-color: transparent;
      
      /* foreground */
      --foreground-color: transparent;
      --foreground-color-selected: transparent;
    }
    
    :host {
      /* cursor */
      cursor: pointer;
      
      /* font */
      font-family: monospace;
      font-size: 12px;
      font-weight: bold;
      
      /* background */
      background-color: var(--background-color);
      
      /* foreground */
      color: var(--foreground-color);
      
      /* appearance */
      margin: 0px;
      border: 0px;
      padding: 0px;
      
      /* flexbox */
      display: flex;
      align-items: center;
      justify-content: center;
      
      /* layout */
      position: relative;
      width: 100%;
      height: 100%;
    }
    
    :host(.selected) {
      /* foreground */
      color: var(--foreground-color-selected);
    }
    
    :host(.dragging) {
      /* appearance */
      opacity: 0.5;
    }
    </style>
    <span id="color-info"></span>
    `;
    /************************************************/
    this.#colorInfo = this.#shadow.getElementById('color-info');
  }
  /************************************************/
  connectedCallback() {
    this.draggable = true;
    /************************************************/
    this.addEventListener('click',     this.handleClick);
    this.addEventListener('dragstart', this.handleDragStart);
    this.addEventListener('dragend',   this.handleDragEnd);
    this.addEventListener('dragover',  this.handleDragOver);
    this.addEventListener('drop',      this.handleDrop);
    /************************************************/
    this.updateUI();
  }
  /************************************************/
  disconnectedCallback() {
    this.removeEventListener('click',     this.handleClick);
    this.removeEventListener('dragstart', this.handleDragStart);
    this.removeEventListener('dragend',   this.handleDragEnd);
    this.removeEventListener('dragover',  this.handleDragOver);
    this.removeEventListener('drop',      this.handleDrop);
  }
  /************************************************/
  static get observedAttributes() {
    let retValue = [];
    /************************************************/
    retValue.push('data-color-name');
    retValue.push('data-color-value');
    retValue.push('data-segment-index');
    /************************************************/
    return retValue;
  }
  /************************************************/
  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'data-color-value') {
      // do nothing...
    }
    /************************************************/
    if (name === 'data-color-name') {
      // do nothing...
    }
    /************************************************/
    if (name === 'data-segment-index') {
      // do nothing...
    }
    /************************************************/
    this.updateUI();
  }
  /************************************************/
  get colorName() {
    let retValue = this.getAttribute('data-color-name');
    /************************************************/
    return retValue;
  }
  /************************************************/
  set colorName(value) {
    this.setAttribute('data-color-name', value);
  }
  /************************************************/
  get colorValue() {
    let retValue = this.getAttribute('data-color-value');
    /************************************************/
    return retValue;
  }
  /************************************************/
  set colorValue(value) {
    this.setAttribute('data-color-value', value);
  }
  /************************************************/
  get segmentIndex() {
    let retValue = parseInt(this.getAttribute('data-segment-index'));
    /************************************************/
    return retValue;
  }
  /************************************************/
  set segmentIndex(value) {
    this.setAttribute('data-segment-index', parseInt(value));
  }
  /************************************************/
  get isSelected() {
    let retValue = false;
    /************************************************/
    const parent = this.parentElement;
    /************************************************/
    if (parent instanceof SegmentSelector) {
      retValue = parent.currentColorIndex === this.segmentIndex;
    }
    /************************************************/
    return retValue;
  }
  /************************************************/
  
  
  
  
  
  handleClick(e) {
    const parent = this.parentElement;
    /************************************************/
    if (parent instanceof SegmentSelector) {
      parent.currentColorIndex = this.segmentIndex;
    }
  }
  /************************************************/
  handleDragStart(e) {
    e.dataTransfer.setData('text/plain', this.segmentIndex);
    /************************************************/
    this.classList.add('dragging');
  }
  /************************************************/
  handleDragEnd() {
    this.classList.remove('dragging');
  }
  /************************************************/
  handleDragOver(e) {
    e.preventDefault();
  }
  /************************************************/
  handleDrop(e) {
    e.preventDefault();
    /************************************************/
    const fromIndex = parseInt(e.dataTransfer.getData('text/plain'));
    const toIndex   = this.segmentIndex;
    /************************************************/
    if (fromIndex === toIndex) return;
    /************************************************/
    const parent = this.parentElement;
    /************************************************/
    if (parent instanceof SegmentSelector) {
      parent.swapColor(fromIndex, toIndex);
    }
  }
  /************************************************/
  #isColorDark(hexColor) {
    let retValue = false;
    /************************************************/
    const r = parseInt(hexColor.substr(1, 2), 16);
    const g = parseInt(hexColor.substr(3, 2), 16);
    const b = parseInt(hexColor.substr(5, 2), 16);
    /************************************************/
    const brightness = (r * 299 + g * 587 + b * 114) / 1000;
    /************************************************/
    retValue = brightness < 128;
    /************************************************/
    return retValue;
  }
  /************************************************/
  updateUI() {
    // classList
    if ( this.isSelected) { this.classList.add('selected'); }
    if (!this.isSelected) { this.classList.remove('selected'); }
    /************************************************/
    // class variable
                                                                                      this.style.setProperty('--background-color'         , this.colorValue);
                                                                                      this.style.setProperty('--foreground-color'         , this.colorValue);
    if (this.classList.contains("selected") &&  this.#isColorDark(this.colorValue)) { this.style.setProperty('--foreground-color-selected', '#fff'); }
    if (this.classList.contains("selected") && !this.#isColorDark(this.colorValue)) { this.style.setProperty('--foreground-color-selected', '#000'); }
    /************************************************/
    // content
    this.#colorInfo.textContent = this.colorValue;
    /************************************************/
    // title
    this.title = `${this.colorName} (${this.colorValue})`;
  }
  /************************************************/
}
/************************************************/
customElements.define('segment-color', SegmentColor);