(()=>{"use strict";var e,t,n,o={1203:function(e,t,n){var o=this&&this.__rest||function(e,t){var n={};for(var o in e)Object.prototype.hasOwnProperty.call(e,o)&&t.indexOf(o)<0&&(n[o]=e[o]);if(null!=e&&"function"==typeof Object.getOwnPropertySymbols){var r=0;for(o=Object.getOwnPropertySymbols(e);r<o.length;r++)t.indexOf(o[r])<0&&Object.prototype.propertyIsEnumerable.call(e,o[r])&&(n[o[r]]=e[o[r]])}return n},r=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const l=r(n(7294)),i=n(7294),a=n(7027),u=(0,i.memo)((e=>{var{children:t}=e,n=o(e,["children"]);return l.default.createElement(a.SFabIconButton,Object.assign({},n),t)}));t.default=u},8877:function(e,t,n){var o=this&&this.__createBinding||(Object.create?function(e,t,n,o){void 0===o&&(o=n);var r=Object.getOwnPropertyDescriptor(t,n);r&&!("get"in r?!t.__esModule:r.writable||r.configurable)||(r={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,o,r)}:function(e,t,n,o){void 0===o&&(o=n),e[o]=t[n]}),r=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),l=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&o(t,e,n);return r(t,e),t},i=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SIconButton=t.SPinIconButton=t.SHeaderIconButton=t.SListItemRight=t.SListItemLeft=t.SListItem=t.SSubdirectoryArrowLeftIcon=t.SActionMessageSpan=t.SActionMessageText=t.SMessageText=t.SContent=t.SHeaderRight=t.SHeaderLeft=t.SHeader=t.GlobalStyle=void 0;const a=l(n(2788)),u=i(n(7328)),d=n(2447),c=n(6188);t.GlobalStyle=a.createGlobalStyle`
  ${c.resetCSS}
`,t.SHeader=a.default.header`
  padding: 1em;
  display: flex;
  align-items: center;
`,t.SHeaderLeft=a.default.div`
  flex: 1;
`,t.SHeaderRight=a.default.div``,t.SContent=a.default.div``,t.SMessageText=a.default.p`
  padding: 1em;
  color: #aaa;
`,t.SActionMessageText=a.default.p`
  padding: 1em;
`,t.SActionMessageSpan=a.default.span`
  font-size: 1.25em;
`,t.SSubdirectoryArrowLeftIcon=(0,a.default)(d.SubdirectoryArrowLeftIcon)`
  width: 2em;
  height: 2em;
  transform: rotate(90deg);
  margin-left: 0.75em;
  margin-right: 0.5em;
`,t.SListItem=a.default.li`
  display: flex;
  justify-content: space-between;
  border-top: 1px solid rgba(0, 0, 0, 0.1);
`,t.SListItemLeft=a.default.div`
  padding: 1em 0.25rem 1em 1em;
  flex: 1;
  text-overflow: ellipsis;
  overflow: hidden;
  white-space: nowrap;
  cursor: pointer;
  display: flex;
  align-items: center;

  &:hover {
    background-color: rgba(0, 0, 0, 0.05);
  }

  ${({disabled:e})=>e&&a.css`
      cursor: default;

      &:hover {
        background-color: transparent;
      }
    `}

    & > span {
      flex: 1;
    }

    & > svg {
      width: 1rem;
    }
`,t.SListItemRight=a.default.div`
  padding: 1em;
`,t.SHeaderIconButton=(0,a.default)(u.default)`
  margin-left: 1em;
  width: 2em;
  height: 2em;
  padding: 0.25em;
`,t.SPinIconButton=(0,a.default)(u.default)`
  margin: 0 0.5em;

  ${({isPin:e})=>e&&a.css`
      opacity: 0.2;

      &:hover {
        opacity: 1;
      }
    `}

  ${({disabled:e})=>e&&a.css`
      opacity: 1;
    `}
`,t.SIconButton=(0,a.default)(u.default)`
  margin: 0 0.5em;
`},6748:function(e,t,n){var o=this&&this.__createBinding||(Object.create?function(e,t,n,o){void 0===o&&(o=n);var r=Object.getOwnPropertyDescriptor(t,n);r&&!("get"in r?!t.__esModule:r.writable||r.configurable)||(r={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,o,r)}:function(e,t,n,o){void 0===o&&(o=n),e[o]=t[n]}),r=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),l=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&o(t,e,n);return r(t,e),t},i=this&&this.__rest||function(e,t){var n={};for(var o in e)Object.prototype.hasOwnProperty.call(e,o)&&t.indexOf(o)<0&&(n[o]=e[o]);if(null!=e&&"function"==typeof Object.getOwnPropertySymbols){var r=0;for(o=Object.getOwnPropertySymbols(e);r<o.length;r++)t.indexOf(o[r])<0&&Object.prototype.propertyIsEnumerable.call(e,o[r])&&(n[o[r]]=e[o[r]])}return n},a=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.Popup=void 0;const u=l(n(7294)),d=n(153),c=a(n(1203)),s=a(n(7907)),f=n(8877),p=l(n(5352)),m=n(8593);t.Popup=()=>{const[e,t]=(0,u.useState)(!1),[n,o]=(0,u.useState)(!0),[r,l]=(0,u.useState)([]),[a,g]=(0,u.useState)();return(0,u.useEffect)((()=>{chrome.tabs.query({active:!0,currentWindow:!0},(([e])=>{e&&e.url?(g(e),p.fetchAllNotes(e).then((e=>{const{notes:n,isVisible:r}=e;n&&l(n),void 0!==r&&o(r),t(!0)})).catch((e=>{t(!1)}))):t(!1)}))}),[]),u.default.createElement(u.default.Fragment,null,u.default.createElement(f.GlobalStyle,null),u.default.createElement("div",{style:{width:"320px"}},u.default.createElement(f.SHeader,null,u.default.createElement(f.SHeaderLeft,null,u.default.createElement(c.default,{onClick:()=>{a&&p.sendCreateNote(a).then((({notes:e})=>{e&&l(e),t(!0)})).catch((e=>{t(!1)})).finally((()=>{window.close()}))},disabled:!e},u.default.createElement(d.PlusIcon,{fill:"#fff"}))),u.default.createElement(f.SHeaderRight,null,u.default.createElement(f.SHeaderIconButton,{onClick:()=>{chrome.runtime.openOptionsPage&&chrome.runtime.openOptionsPage()}},u.default.createElement(d.Bars3Icon,{fill:"rgba(0, 0, 0, 0.4)"})))),u.default.createElement(f.SContent,null,!e&&u.default.createElement(f.SMessageText,null,(0,m.msg)("note_unavailable_msg")),e&&0===r.length&&u.default.createElement(u.default.Fragment,null,u.default.createElement(f.SActionMessageText,null,u.default.createElement(f.SSubdirectoryArrowLeftIcon,null),u.default.createElement(f.SActionMessageSpan,null,(0,m.msg)("no_note_created_msg"))),u.default.createElement(f.SMessageText,null,(0,m.msg)("no_note_created_option_msg"))),e&&0!==r.length&&u.default.createElement("ul",null,r.map((e=>u.default.createElement(f.SListItem,{key:e.id},u.default.createElement(f.SListItemLeft,{disabled:e.is_fixed,onClick:()=>!e.is_fixed&&(e=>{a&&p.sendScrollToTargetNote(a,e).then((()=>{t(!0)})).catch((e=>{t(!1)})).finally((()=>{}))})(e)},u.default.createElement("span",null,e.title||e.description||(0,m.msg)("new_note_title_msg")),!e.is_fixed&&u.default.createElement(d.ChevronRightIcon,{fill:"rgba(0, 0, 0, 0.5)"})),u.default.createElement(f.SListItemRight,null,u.default.createElement(s.default,{title:(0,m.msg)("reset_position_msg"),placement:"top"},u.default.createElement("span",null,u.default.createElement(f.SIconButton,{onClick:()=>(e=>{const{position_x:n,position_y:o}=e,r=i(e,["position_x","position_y"]);a&&p.sendUpdateNote(a,Object.assign(Object.assign({},r),{is_fixed:!0,is_open:!0})).then((({notes:e})=>{e&&l(e),t(!0)})).catch((e=>{t(!1)}))})(e)},u.default.createElement(d.ArrowPathIcon,{fill:"rgba(0, 0, 0, 0.5)"})))),u.default.createElement(f.SIconButton,{onClick:()=>(e=>{const{id:n,title:o}=e;a&&confirm(`"${null!=o?o:(0,m.msg)("note")}" ${(0,m.msg)("confirm_remove_next_note_msg")}`)&&p.sendDeleteNote(a,e).then((({notes:e})=>{e&&l(e),t(!0)})).catch((e=>{t(!1)}))})(e)},u.default.createElement(d.TrashIcon,{fill:"rgba(0, 0, 0, 0.5)"}))))))))))}},4454:function(e,t,n){var o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const r=o(n(7294)),l=o(n(3935)),i=n(6748);l.default.render(r.default.createElement(r.default.StrictMode,null,r.default.createElement(i.Popup,null)),document.getElementById("root"))},5352:function(e,t,n){var o=this&&this.__awaiter||function(e,t,n,o){return new(n||(n=Promise))((function(r,l){function i(e){try{u(o.next(e))}catch(e){l(e)}}function a(e){try{u(o.throw(e))}catch(e){l(e)}}function u(e){var t;e.done?r(e.value):(t=e.value,t instanceof n?t:new n((function(e){e(t)}))).then(i,a)}u((o=o.apply(e,t||[])).next())}))};Object.defineProperty(t,"__esModule",{value:!0}),t.sendUpdateNoteVisible=t.sendScrollToTargetNote=t.sendDeleteNote=t.sendUpdateNote=t.sendCreateNote=t.fetchAllNotes=void 0;const r=n(7364),l=n(7755);t.fetchAllNotes=e=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.GET_ALL_NOTES,r.POPUP,{tab:e})})),t.sendCreateNote=e=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.CREATE_NOTE,r.POPUP,{tab:e})})),t.sendUpdateNote=(e,t)=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.UPDATE_NOTE,r.POPUP,{tab:e,note:t})})),t.sendDeleteNote=(e,t)=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.DELETE_NOTE,r.POPUP,{tab:e,note:t})})),t.sendScrollToTargetNote=(e,t)=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.SCROLL_TO_TARGET_NOTE,r.POPUP,{tab:e,note:t})})),t.sendUpdateNoteVisible=(e,t)=>o(void 0,void 0,void 0,(function*(){return yield(0,l.sendAction)(r.UPDATE_NOTE_VISIBLE,r.POPUP,{tab:e,isVisible:t})}))}},r={};function l(e){var t=r[e];if(void 0!==t)return t.exports;var n=r[e]={exports:{}};return o[e].call(n.exports,n,n.exports,l),n.exports}l.m=o,e=[],l.O=(t,n,o,r)=>{if(!n){var i=1/0;for(c=0;c<e.length;c++){for(var[n,o,r]=e[c],a=!0,u=0;u<n.length;u++)(!1&r||i>=r)&&Object.keys(l.O).every((e=>l.O[e](n[u])))?n.splice(u--,1):(a=!1,r<i&&(i=r));if(a){e.splice(c--,1);var d=o();void 0!==d&&(t=d)}}return t}r=r||0;for(var c=e.length;c>0&&e[c-1][2]>r;c--)e[c]=e[c-1];e[c]=[n,o,r]},l.n=e=>{var t=e&&e.__esModule?()=>e.default:()=>e;return l.d(t,{a:t}),t},n=Object.getPrototypeOf?e=>Object.getPrototypeOf(e):e=>e.__proto__,l.t=function(e,o){if(1&o&&(e=this(e)),8&o)return e;if("object"==typeof e&&e){if(4&o&&e.__esModule)return e;if(16&o&&"function"==typeof e.then)return e}var r=Object.create(null);l.r(r);var i={};t=t||[null,n({}),n([]),n(n)];for(var a=2&o&&e;"object"==typeof a&&!~t.indexOf(a);a=n(a))Object.getOwnPropertyNames(a).forEach((t=>i[t]=()=>e[t]));return i.default=()=>e,l.d(r,i),r},l.d=(e,t)=>{for(var n in t)l.o(t,n)&&!l.o(e,n)&&Object.defineProperty(e,n,{enumerable:!0,get:t[n]})},l.g=function(){if("object"==typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"==typeof window)return window}}(),l.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),l.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},l.j=42,(()=>{var e={42:0};l.O.j=t=>0===e[t];var t=(t,n)=>{var o,r,[i,a,u]=n,d=0;if(i.some((t=>0!==e[t]))){for(o in a)l.o(a,o)&&(l.m[o]=a[o]);if(u)var c=u(l)}for(t&&t(n);d<i.length;d++)r=i[d],l.o(e,r)&&e[r]&&e[r][0](),e[r]=0;return l.O(c)},n=self.webpackChunkgithub_language_extension=self.webpackChunkgithub_language_extension||[];n.forEach(t.bind(null,0)),n.push=t.bind(null,n.push.bind(n))})(),l.nc=void 0;var i=l.O(void 0,[736],(()=>l(4454)));i=l.O(i)})();