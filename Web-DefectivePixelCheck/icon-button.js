class IconButton extends HTMLElement {
  #shadow = undefined;
  /************************************************/
  #gooIcon = undefined;
  /************************************************/
  
  
  
  
  
  constructor() {
    super();
    /************************************************/
    this.#shadow = this.attachShadow({ mode: 'closed' });
    /************************************************/
    this.#shadow.innerHTML = `
    <style>
    @import url('https://fonts.googleapis.com/icon?family=Material+Icons');
    
    :host {
      /* background */
      --background-color: rgba(255, 255, 255, 0.5);
      --background-color-hover: rgba(255, 255, 255, 1);
      
      /* foreground */
      --foreground-color: rgba(0, 0, 0, 1);
    }
    
    :host {
      /* cursor */
      cursor: pointer;
      
      /* background */
      background-color: var(--background-color);
      
      /* foreground */
      color: var(--foreground-color);
      
      /* appearance */
      margin: 0px;
      border-color: rgba(0, 0, 0, 1);
      border-radius: 4px;
      border-style: solid;
      border-width: 1px;
      padding: 5px;
      
      /* flexbox */
      display: flex;
      align-items: center;
      justify-content: center;
      
      /* layout */
      width: 24px;
      height: 24px;
    }
    
    :host(:hover) {
      /* background */
      background-color: var(--background-color-hover);
    }
    </style>
    <span id="google-icon" class="material-icons"></span>
    `;
    /************************************************/
    this.#gooIcon = this.#shadow.getElementById('google-icon');
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
    retValue.push('data-icon-name');
    /************************************************/
    return retValue;
  }
  /************************************************/
  attributeChangedCallback(name, oldValue, newValue) {
    if (name === 'data-icon-name') {
      // do nothing...
    }
    /************************************************/
    this.updateUI();
  }
  /************************************************/
  
  
  
  
  
  get iconName() {
    let retValue = this.getAttribute('data-icon-name');
    /************************************************/
    return retValue;
  }
  /************************************************/
  set iconName(value) {
    this.setAttribute('data-icon-name', value);
  }
  /************************************************/
  
  
  
  
  
  updateUI() {
    // content
    this.#gooIcon.textContent = this.iconName;
  }
  /************************************************/
}
/************************************************/
customElements.define('icon-button', IconButton);