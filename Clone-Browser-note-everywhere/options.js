(()=>{"use strict";var e,t,n,l={7237:function(e,t,n){var l=this&&this.__rest||function(e,t){var n={};for(var l in e)Object.prototype.hasOwnProperty.call(e,l)&&t.indexOf(l)<0&&(n[l]=e[l]);if(null!=e&&"function"==typeof Object.getOwnPropertySymbols){var a=0;for(l=Object.getOwnPropertySymbols(e);a<l.length;a++)t.indexOf(l[a])<0&&Object.prototype.propertyIsEnumerable.call(e,l[a])&&(n[l[a]]=e[l[a]])}return n},a=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const i=a(n(7294)),o=n(7294),r=n(7027),d=(0,o.memo)((e=>{var{children:t}=e,n=l(e,["children"]);return i.default.createElement(r.SButton,Object.assign({},n),t)}));t.default=d},5801:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SColorDefault=t.SColorCheckIcon=t.SColor=t.SColors=void 0;const r=n(153),d=o(n(7294)),u=n(7294),c=i(n(2788)),s=(0,u.memo)((({onChangeColor:e,color:n="",hasDefault:l=!1})=>d.default.createElement(t.SColors,null,l&&d.default.createElement(t.SColorDefault,{$isActive:l&&""===n,onClick:t=>{t.stopPropagation(),null==e||e("")}},l&&""===n&&d.default.createElement(t.SColorCheckIcon,{color:"rgb(0,0,0)"})),["#FFFFFF","#EB9694","#FAD0C3","#FFF7CC","#C1E1C5","#BEDADC","#C4DEF6","#D4C4FB"].map((a=>{const i="#FFFFFF"===a&&""===n&&!l||a===n;return d.default.createElement(t.SColor,{key:a,$isActive:i,style:{backgroundColor:a},onClick:t=>{t.stopPropagation(),null==e||e(a)}},i&&d.default.createElement(t.SColorCheckIcon,{color:"rgb(0,0,0)"}))})))));t.default=s,t.SColors=c.default.div`
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
`,t.SColor=c.default.button`
  border: 1px solid #ddd;
  border-radius: 8px;
  margin: 4px;
  width: 48px;
  height: 32px;
  text-align: center;
  cursor: pointer;

  ${({$isActive:e})=>e&&c.css`
      cursor: default;
      border: 2px solid #000;
    `}
`,t.SColorCheckIcon=(0,c.default)(r.CheckIcon)`
  width: 20px;
  height: 20px;
  margin: auto;
`,t.SColorDefault=(0,c.default)(t.SColor)`
  position: relative;
  border-color: #aaa;
  overflow: hidden;
  background-color: #fff;

  ${t.SColorCheckIcon} {
    position: relative;
    z-index: 1;
  }

  &::before {
    content: "";
    position: absolute;
    top: 50%;
    left: 0;
    width: 100%;
    height: 2px;
    background: #aaa;
    transform: rotate(45deg);
  }
`},5326:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SNoteDetailDataSpan=t.SNoteDetailDataInput=t.SNoteDetailData=t.SNoteDetailTitle=t.SNoteDetail=t.SNoteDetailArea=t.SAccordionSummaryText=t.SAccordionSummary=t.SAccordion=t.SDivider=t.SIconButton=t.SButton=t.SMenuListItem=t.SMenuList=t.SModalActionsRight=t.SModalActionsLeft=t.SModalActions=t.SModalDescriptionText=t.SModalDescription=t.SModalSectionTitle=t.SModalSection=t.SModalContent=t.SModalTitle=t.SModalHeader=t.SModalScrollContent=t.SBackdrop=t.SModal=t.SModalWrapper=void 0;const r=o(n(5729)),d=o(n(8514)),u=i(n(2788)),c=o(n(7237)),s=o(n(7328)),f=o(n(5261));t.SModalWrapper=u.default.div`
  position: fixed;
  width: 100%;
  max-height: 100%;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 2em;
  pointer-events: none;
  outline: none;
  transition: transform 0.15s ease-in-out;
  z-index: 1300;

  ${({$isApeal:e})=>e&&u.css`
      transform: translate(-50%, -50%) scale(1.03);
    `}
`,t.SModal=u.default.div`
  display: flex;
  flex-direction: column;
  pointer-events: initial;
  margin: auto;
  width: 800px;
  max-width: 100%;
  max-height: calc(100vh - 4em);
  background-color: #fff;
  border-radius: 0.25em;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
`,t.SBackdrop=(0,u.default)(r.default)`
  z-index: 1300;
`,t.SModalScrollContent=u.default.div`
  overflow-y: auto;
`,t.SModalHeader=u.default.div`
  padding: 1em 1.5em 0.5em;
`,t.SModalTitle=(0,u.default)(d.default)`
  padding: 0.2em;
  width: 100%;
  font-size: 1.25em;
  line-height: 1.5;
  word-break: break-all;
  white-space: pre-line;
  resize: none;

  &::placeholder {
    color: #aaa;
  }
`,t.SModalContent=u.default.div``,t.SModalSection=u.default.div`
  padding: 0 1.5em 1em;
`,t.SModalSectionTitle=u.default.h4`
  font-size: 0.875em;
  line-height: 1.5;
  font-weight: bold;
  margin-bottom: 0.5em;
`,t.SModalDescription=u.default.div`
  position: relative;
`,t.SModalDescriptionText=(0,u.default)(d.default)`
  padding: 0.25em;
  width: 100%;
  font-size: 1em;
  word-break: break-all;
  white-space: pre-line;
  resize: none;
  overflow-y: hidden;

  &::placeholder {
    color: #aaa;
  }
`,t.SModalActions=u.default.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1em 0.75em;
`,t.SModalActionsLeft=u.default.div`
  display: flex;
  align-items: center;
`,t.SModalActionsRight=u.default.div`
  display: flex;
  align-items: center;
`,t.SMenuList=u.default.div`
  display: flex;
  flex-direction: column;
  align-items: stretch;
`,t.SMenuListItem=u.default.button`
  min-width: 8.5em;
  padding: 0.75em 1em;
  font-size: 0.875em;

  &:hover {
    background-color: rgba(0, 0, 0, 0.02);
  }

  &:not(:first-child) {
    border-top: 1px solid rgba(0, 0, 0, 0.1);
  }
`,t.SButton=(0,u.default)(c.default)`
  margin-right: 1.25em;
  padding: 0.75em 1em;
`,t.SIconButton=(0,u.default)(s.default)`
  margin-right: 1em;
`,t.SDivider=u.default.hr`
  border-width: 0;
  border-bottom-width: 1px;
  border-color: rgba(0, 0, 0, 0.1);
`,t.SAccordion=u.default.details``,t.SAccordionSummary=u.default.summary`
  padding: 1em 1.5em;
  cursor: pointer;

  &:hover {
    background-color: rgba(0, 0, 0, 0.02);
  }

  &::marker {
    font-size: 0.75em;
  }
`,t.SAccordionSummaryText=u.default.span`
  font-size: 0.75em;
`,t.SNoteDetailArea=u.default.div`
  display: flex;
  flex-wrap: wrap;
`,t.SNoteDetail=u.default.dl`
  width: 33%;
  min-width: 11.5em;
  margin-bottom: 1em;
`,t.SNoteDetailTitle=u.default.dt`
  color: #888;
  font-weight: normal;
  font-size: 0.75em;
  margin-bottom: 0.25em;
`,t.SNoteDetailData=u.default.dd`
  flex: 1;
  font-size: 0.875em;
  line-height: 1.5;
`,t.SNoteDetailDataInput=(0,u.default)(f.default)`
  text-align: left;
  padding: 0em 0.25em;
  width: 3.75em;
`,t.SNoteDetailDataSpan=u.default.span`
  vertical-align: middle;
`},3227:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__awaiter||function(e,t,n,l){return new(n||(n=Promise))((function(a,i){function o(e){try{d(l.next(e))}catch(e){i(e)}}function r(e){try{d(l.throw(e))}catch(e){i(e)}}function d(e){var t;e.done?a(e.value):(t=e.value,t instanceof n?t:new n((function(e){e(t)}))).then(o,r)}d((l=l.apply(e,t||[])).next())}))},r=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.NoteEditModal=void 0;const d=r(n(7367)),u=r(n(6021)),c=i(n(7294)),s=n(3104),f=n(8593),m=n(153),g=n(5326),p=n(8593);t.NoteEditModal=({isOpen:e,note:t,onClose:n,onUpdateNote:l,onDeleteNote:a,initFocus:i})=>{const{title:r,description:S,is_fixed:h,is_open:_,position_x:v,position_y:b,width:E,height:y,created_at:O,updated_at:w}=t,[I,C]=(0,c.useState)(h),[D,P]=(0,c.useState)(!1),[x,N]=(0,c.useState)(null),{editTitle:M,setEditTitle:k,editDescription:T,setEditDescription:j,editIsOpen:L,setEditIsOpen:A,editPositionX:H,editPositionY:F,setEditPosition:B,editWidth:$,editHeight:z,setEditSize:W}=(0,s.useNoteEdit)(t),R=Object.assign(Object.assign({},t),{title:M,description:T,position_x:H,position_y:F,width:$,height:z,is_fixed:I,is_open:I}),U=(0,c.useMemo)((()=>!((r===M||void 0===r&&""===M)&&(S===T||void 0===S&&""===T)&&v===H&&b===F&&E===$&&y===z&&h===I&&_===L)),[t,R]),G=(0,c.useCallback)((e=>{"fixed"===e.target.value?(B(void 0,void 0),C(!0)):(B(v,b),C(!1))}),[]),Y=(0,c.useCallback)((e=>{"true"===e.target.value?A(!0):A(!1)}),[]),X=(0,c.useCallback)((()=>o(void 0,void 0,void 0,(function*(){var e;if(yield l(R))n&&n();else{const t=(null!==(e=null==S?void 0:S.length)&&void 0!==e?e:0)>2e3?(0,p.msg)("save_error_word_maximum_msg"):(0,p.msg)("save_error_msg_2");alert(`${(0,p.msg)("save_error_msg")}${t}`)}}))),[R]),q=(0,c.useCallback)((()=>{k(r),j(S),B(v,b),W(null!=E?E:s.MIN_NOTE_WIDTH,null!=y?y:s.MIN_NOTE_HEIGHT),n&&n()}),[t,n]),Q=(0,c.useCallback)((()=>o(void 0,void 0,void 0,(function*(){a&&(yield a(t))&&(n&&n(),V())}))),[]),V=()=>{N(null)};return c.default.createElement(d.default,{open:!!e,onClose:()=>{U?(P(!0),setTimeout((()=>{P(!1)}),150)):q()},BackdropComponent:g.SBackdrop},c.default.createElement(g.SModalWrapper,{$isApeal:D},c.default.createElement(g.SModal,null,c.default.createElement(g.SModalScrollContent,null,c.default.createElement(g.SModalHeader,null,c.default.createElement(g.SModalTitle,{autoFocus:"title"===i,placeholder:(0,p.msg)("title_sort_option"),defaultValue:r,onChange:e=>k(e.target.value),minRows:1})),c.default.createElement(g.SModalContent,null,c.default.createElement(g.SModalSection,null,c.default.createElement(g.SModalDescription,null,c.default.createElement(g.SModalDescriptionText,{autoFocus:"description"===i,minRows:1,placeholder:(0,p.msg)("input_description_placeholder"),defaultValue:S,onChange:e=>j(e.target.value)}))),c.default.createElement(g.SDivider,null),c.default.createElement(g.SAccordion,null,c.default.createElement(g.SAccordionSummary,null,c.default.createElement(g.SAccordionSummaryText,null,(0,p.msg)("detail_msg"))),c.default.createElement(g.SModalSection,null,c.default.createElement(g.SNoteDetailArea,null,c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("pin_msg")),c.default.createElement(g.SNoteDetailData,null,c.default.createElement("select",{value:I?"fixed":"unfixed",onChange:G},c.default.createElement("option",{value:"unfixed"},(0,p.msg)("pin_select_option_unfixed_msg")),c.default.createElement("option",{value:"fixed"},(0,p.msg)("pin_select_option_fixed_msg"))))),c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("open_msg")),c.default.createElement(g.SNoteDetailData,null,c.default.createElement("select",{value:L?"true":"false",onChange:Y},c.default.createElement("option",{value:"false"},(0,p.msg)("open_select_option_yes_msg")),c.default.createElement("option",{value:"true"},(0,p.msg)("open_select_option_no_msg"))))),c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("position_msg")),c.default.createElement(g.SNoteDetailData,null,c.default.createElement(g.SNoteDetailDataSpan,null,"x:"),c.default.createElement(g.SNoteDetailDataInput,{valueNum:null!=H?H:0,onChangeNumber:e=>B(e,F)}),c.default.createElement(g.SNoteDetailDataSpan,null,"y:"),c.default.createElement(g.SNoteDetailDataInput,{valueNum:null!=F?F:0,onChangeNumber:e=>B(H,e)}))),c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("size_msg")),c.default.createElement(g.SNoteDetailData,null,c.default.createElement(g.SNoteDetailDataSpan,null,(0,p.msg)("size_width_msg"),":"),c.default.createElement(g.SNoteDetailDataInput,{valueNum:$,onChangeNumber:e=>W(e,z)}),c.default.createElement(g.SNoteDetailDataSpan,null,(0,p.msg)("size_height_msg"),":"),c.default.createElement(g.SNoteDetailDataInput,{valueNum:z,onChangeNumber:e=>W($,e)}))),w&&c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("updated_at_msg")),c.default.createElement(g.SNoteDetailData,null,(0,f.formatDate)(new Date(w)))),O&&c.default.createElement(g.SNoteDetail,null,c.default.createElement(g.SNoteDetailTitle,null,(0,p.msg)("created_at_msg")),c.default.createElement(g.SNoteDetailData,null,(0,f.formatDate)(new Date(O))))))))),c.default.createElement(g.SDivider,null),c.default.createElement(g.SModalActions,null,c.default.createElement(g.SModalActionsLeft,null,c.default.createElement(g.SButton,{onClick:X,disabled:!U},(0,p.msg)("save_msg")),c.default.createElement(g.SButton,{secondary:!0,onClick:q},U?(0,p.msg)("discard_close_msg"):(0,p.msg)("close_msg"))),c.default.createElement(g.SModalActionsRight,null,c.default.createElement(g.SIconButton,{"aria-labelledby":"action-popover",onClick:e=>{N(e.currentTarget)}},c.default.createElement(m.EllipsisVerticalIcon,{fill:"rgba(0, 0, 0, 0.4)"})),c.default.createElement(u.default,{id:"action-popover",open:Boolean(x),anchorEl:x,onClose:V,anchorOrigin:{vertical:"top",horizontal:"left"},transformOrigin:{vertical:"bottom",horizontal:"left"}},c.default.createElement(g.SMenuList,null,c.default.createElement(g.SMenuListItem,{onClick:Q},(0,p.msg)("delete_msg")))))))))}},4292:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SHeaderContentLink=t.SHeaderContent=t.STopLink=t.STitle=t.SLogo=t.SHeaderLeft=t.SHeader=t.OptionHeader=void 0;const r=o(n(7294)),d=i(n(2788)),u=n(3727),c=n(8593);t.OptionHeader=({current:e})=>r.default.createElement(t.SHeader,null,r.default.createElement(t.SHeaderLeft,null,r.default.createElement(t.STopLink,{to:"/memos.html"},r.default.createElement(t.SLogo,{src:"/images/icon_38.png",alt:""}),r.default.createElement(t.STitle,null,(0,c.msg)("appName")))),r.default.createElement(t.SHeaderContent,null,r.default.createElement(t.SHeaderContentLink,{$isActive:"memos"===e,to:"/memos.html"},(0,c.msg)("note_header_msg")),r.default.createElement(t.SHeaderContentLink,{$isActive:"setting"===e,to:"/setting.html"},(0,c.msg)("settings_header_msg")))),t.default=t.OptionHeader,t.SHeader=d.default.header`
  display: flex;
  align-items: center;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 2.75em;
  padding: 0 1.75em;
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  background-color: #fff;
  z-index: 1;
`,t.SHeaderLeft=d.default.div`
  display: flex;
  align-items: center;
  position: relative;
`,t.SLogo=d.default.img`
  width: 1.5em;
  margin-top: -0.25em;
  margin-right: 0.25em;
`,t.STitle=d.default.h1``,t.STopLink=(0,d.default)(u.Link)`
  display: flex;
  align-items: center;
  padding: 0.25em;
  position: relative;

  &:hover {
    opacity: 0.8;
  }
`,t.SHeaderContent=d.default.div`
  display: flex;
  align-items: center;
  position: relative;
  height: 100%;
  padding: 0 1em;
`,t.SHeaderContentLink=(0,d.default)(u.Link)`
  display: flex;
  align-items: center;
  position: relative;
  height: 100%;
  margin: 0 0.25em;
  padding: 0 1em;

  &:hover {
    background-color: rgba(0, 0, 0, 0.05);
  }

  ${({$isActive:e})=>e&&d.css`
      &:after {
        content: "";
        position: absolute;
        bottom: 0;
        left: 0;
        width: 100%;
        height: 2px;
        background-color: #4c4722;
      }
    `}
`},4076:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SLaunchIcon=t.SPageInfoLink=t.SPageInfoTitle=t.SPageInfoFaviconImage=t.SPageInfoHeader=t.SPageInfo=t.SPageInfoWrap=t.SIconButton=t.SCopySuccessIcon=t.SIconButtonWrap=t.SCardActions=t.SCardFooter=t.SCardDateText=t.SCardDate=t.SCardDescriptionText=t.SCardDescription=t.SCardTitle=t.noteDescriptionCSS=t.noteTitleCSS=t.SCardHeader=t.SCard=void 0;const r=i(n(2788)),d=n(153),u=n(2447),c=o(n(7328));t.SCard=r.default.div`
  display: flex;
  flex-direction: column;
  align-items: stretch;
  background-color: #fff;
  border-radius: 0.25em;
  padding: 0.75em 1em;
  width: 100%;
  box-shadow: rgba(0, 0, 0, 0.14) 0px 2px 2px 0px, rgba(0, 0, 0, 0.12) 0px 1px 5px 0px;
  transition: box-shadow 300ms cubic-bezier(0.25, 0.8, 0.25, 1);
`,t.SCardHeader=r.default.div`
  /* border-bottom: 1px solid rgba(0, 0, 0, 0.1); */
`,t.noteTitleCSS=r.css`
  font-size: 1em;
  line-height: 1.25;
  color: #333;
  border-width: 0.0625em;
  border-color: transparent;
  border-radius: 0.2em;
  word-break: break-all;
  white-space: pre-line;
`,t.noteDescriptionCSS=r.css`
  margin-bottom: 0.75em;
  padding: 0.25em 0;
  line-height: 1.25;
  color: #333;
  border-width: 1px;
  border-color: transparent;
  border-radius: 0.2em;
  word-break: break-all;
  white-space: pre-line;
`,t.SCardTitle=r.default.h3`
  ${t.noteTitleCSS}
`,t.SCardDescription=r.default.div`
  flex: 1;
  ${t.noteDescriptionCSS}
`,t.SCardDescriptionText=r.default.span`
  font-size: 0.875em;
`,t.SCardDate=r.default.div`
  font-size: 0.75em;
  line-height: 1.5;
  text-align: right;
  color: #777;
`,t.SCardDateText=r.default.span`
  margin-right: 1em;
`,t.SCardFooter=r.default.div`
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
`,t.SCardActions=r.default.div`
  display: flex;
  align-items: center;
`,t.SIconButtonWrap=r.default.div`
  margin-right: 1em;
`,t.SCopySuccessIcon=(0,r.default)(u.CopySuccessIcon)`
  width: 1.25em;
  height: 1.25em;
`,t.SIconButton=(0,r.default)(c.default)`
  width: 1.25em;
  height: 1.25em;
`,t.SPageInfoWrap=r.default.div`
  display: inline-flex;
  margin-bottom: 0.75em;
`,t.SPageInfo=r.default.div`
  display: inline-block;
  justify-self: stretch;
  border: 1px solid rgba(0, 0, 0, 0.1);
  border-radius: 0.25em;
  padding: 0.5em 0.75em;
  cursor: pointer;

  ${({$isFilter:e})=>e&&r.css`
      display: inline-flex;
      align-items: center;
    `}

  &:not(:first-child) {
    border-top-left-radius: 0;
    border-bottom-left-radius: 0;
  }

  &:not(:last-child) {
    border-top-right-radius: 0;
    border-bottom-right-radius: 0;
    border-right-width: 0;
  }

  &:hover {
    background-color: rgba(0, 0, 0, 0.05);
  }
`,t.SPageInfoHeader=r.default.div`
  display: flex;
  align-items: center;
`,t.SPageInfoFaviconImage=r.default.img`
  width: 1em;
  height: 1em;
  margin-right: 0.5em;
`,t.SPageInfoTitle=r.default.p`
  font-size: 0.75em;
  line-height: 1.25;
  word-break: break-all;
`,t.SPageInfoLink=r.default.p`
  font-size: 0.625em;
  word-break: break-all;
  text-decoration: underline;
  color: #00379e;
  margin-top: 0.5em;
  margin-left: 2.4em;
`,t.SLaunchIcon=(0,r.default)(d.FunnelIcon)`
  width: 1em;
  height: 1em;
`},7477:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const r=i(n(7294)),d=o(n(6021)),u=o(n(7907)),c=n(153),s=n(8593),f=n(2447),m=n(2691),g=n(3227),p=n(4076),S=n(8593),h=o(n(5801)),_=(0,r.memo)((({note:e,defaultColor:t,pageInfo:n,showPageInfo:l,currentPageInfoId:a,onUpdate:i,onDelete:o,onClickLink:_,onClickFilter:v,measure:b})=>{const{id:E,title:y,description:O,created_at:w,updated_at:I}=e,[C,D]=(0,r.useState)(!1),{isSuccessCopy:P,copyClipboard:x}=(0,m.useClipboard)(),[N,M]=(0,r.useState)();(0,r.useEffect)((()=>{b&&b()}),[e,a]);const[k,T]=r.default.useState(null),j=Boolean(k),L=j?"color-picker-popover":void 0;return r.default.createElement(r.default.Fragment,null,r.default.createElement(p.SCard,{onDoubleClick:()=>{D(!0)},style:{backgroundColor:e.color||t||"#fff"}},r.default.createElement(p.SCardHeader,null,r.default.createElement(p.SCardTitle,{onDoubleClick:e=>{e.stopPropagation(),M("title"),D(!0)}},y)),r.default.createElement(p.SCardDescription,{onDoubleClick:e=>{e.stopPropagation(),M("description"),D(!0)}},r.default.createElement(p.SCardDescriptionText,null,O)),l&&n&&r.default.createElement(p.SPageInfoWrap,null,r.default.createElement(p.SPageInfo,{onClick:e=>{var t;e.preventDefault(),e.stopPropagation(),_(null!==(t=n.page_url)&&void 0!==t?t:"")}},r.default.createElement(p.SPageInfoHeader,null,r.default.createElement(p.SPageInfoFaviconImage,{src:n.fav_icon_url}),r.default.createElement(p.SPageInfoTitle,null,n.page_title)),r.default.createElement(p.SPageInfoLink,null,n.page_url)),r.default.createElement(u.default,{title:(0,S.msg)("this_page_note_list_msg")},r.default.createElement(p.SPageInfo,{$isFilter:!0,onClick:e=>{e.stopPropagation(),v(n.id)}},r.default.createElement(p.SLaunchIcon,{fill:"rgba(0, 0, 0, 0.4)"})))),r.default.createElement(p.SCardFooter,null,r.default.createElement(p.SCardActions,null,r.default.createElement(u.default,{title:(0,S.msg)("edit_msg"),enterDelay:300},r.default.createElement(p.SIconButtonWrap,null,r.default.createElement(p.SIconButton,{onClick:e=>{e.stopPropagation(),D(!0)}},r.default.createElement(c.PencilSquareIcon,{fill:"rgba(0, 0, 0, 0.4)"})))),r.default.createElement(u.default,{title:P?(0,S.msg)("copied_msg"):(0,S.msg)("copy_msg"),enterDelay:300},r.default.createElement(p.SIconButtonWrap,null,P?r.default.createElement(p.SCopySuccessIcon,{fill:"#22c55e"}):r.default.createElement(p.SIconButton,{onClick:e=>{e.stopPropagation(),x(`${y}\n${O}`)}},r.default.createElement(f.CopyIcon,{fill:"rgba(0, 0, 0, 0.4)"})))),r.default.createElement(u.default,{title:(0,S.msg)("color_msg"),enterDelay:300},r.default.createElement(p.SIconButtonWrap,null,r.default.createElement(p.SIconButton,{onClick:e=>{e.stopPropagation(),T(e.currentTarget)}},r.default.createElement(f.PalletIcon,{fill:"rgba(0, 0, 0, 0.4)"})))),r.default.createElement(d.default,{id:L,open:j,anchorEl:k,onClose:e=>{e.stopPropagation(),T(null)},anchorOrigin:{vertical:"bottom",horizontal:"left"}},r.default.createElement("div",{style:{width:"168px",textAlign:"center"},onClick:e=>e.stopPropagation()},r.default.createElement(h.default,{hasDefault:!0,color:e.color,onChangeColor:t=>{i(Object.assign(Object.assign({},e),{color:t}))}}))),r.default.createElement(u.default,{title:(0,S.msg)("delete_msg"),enterDelay:300},r.default.createElement(p.SIconButtonWrap,null,r.default.createElement(p.SIconButton,{onClick:t=>{t.stopPropagation(),E&&o(e)}},r.default.createElement(c.TrashIcon,{fill:"rgba(0, 0, 0, 0.4)"}))))),r.default.createElement(p.SCardDate,null,w&&r.default.createElement(p.SCardDateText,null,(0,S.msg)("created_at_msg"),": ",(0,s.formatDate)(new Date(w))),I&&r.default.createElement(p.SCardDateText,null,(0,S.msg)("updated_at_msg"),": ",(0,s.formatDate)(new Date(I)))))),r.default.createElement(g.NoteEditModal,{isOpen:C,onClose:()=>{D(!1),M(void 0)},note:e,onUpdateNote:i,onDeleteNote:o,initFocus:N}))}));t.default=_},5261:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__rest||function(e,t){var n={};for(var l in e)Object.prototype.hasOwnProperty.call(e,l)&&t.indexOf(l)<0&&(n[l]=e[l]);if(null!=e&&"function"==typeof Object.getOwnPropertySymbols){var a=0;for(l=Object.getOwnPropertySymbols(e);a<l.length;a++)t.indexOf(l[a])<0&&Object.prototype.propertyIsEnumerable.call(e,l[a])&&(n[l[a]]=e[l[a]])}return n};Object.defineProperty(t,"__esModule",{value:!0});const r=i(n(7294)),d=(0,r.memo)((e=>{var{valueNum:t,onChangeNumber:n}=e,l=o(e,["valueNum","onChangeNumber"]);const[a,i]=(0,r.useState)(`${t}`);(0,r.useEffect)((()=>{i(`${t}`)}),[t]);const d=(0,r.useCallback)((e=>{i(e.target.value)}),[i]),u=(0,r.useCallback)((e=>{"Enter"===e.key&&c()}),[t,a,i,n]),c=(0,r.useCallback)((()=>{const e=Number(a);isNaN(e)||n(e),i(`${t}`)}),[a,i,n]);return r.default.createElement("input",Object.assign({type:"text",value:a,onChange:d,onKeyDown:u,onBlur:c},l))}));t.default=d},4294:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t};Object.defineProperty(t,"__esModule",{value:!0});const o=i(n(7294)),r=n(8593),d=i(n(2788)),u=(0,o.memo)((()=>{const e="#init"===window.location.hash;return o.default.createElement(c,{$isInit:e},o.default.createElement(s,null,e&&o.default.createElement(m,null,(0,r.msg)("welcome_msg")),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage01")),o.default.createElement(g,null,o.default.createElement("img",{width:400,src:"/images/usage/usage01.png",alt:""}))),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage02")),o.default.createElement(g,null,o.default.createElement("img",{width:400,src:"/images/usage/usage02.png",alt:""}))),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage02_2")),o.default.createElement(g,null,o.default.createElement("img",{width:200,src:"/images/usage/usage02_2.png",alt:""}))),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage03"),o.default.createElement("br",null),(0,r.msg)("usage04")),o.default.createElement(g,null,o.default.createElement("img",{src:"/images/usage/usage03.png",alt:""}))),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage05")),o.default.createElement(g,null,o.default.createElement("img",{width:400,src:"/images/usage/usage05.png",alt:""}))),o.default.createElement(f,null,o.default.createElement("h3",null,o.default.createElement("a",{href:"./memos.html"},(0,r.msg)("usage06")),(0,r.msg)("usage06_2"))),o.default.createElement(f,null,o.default.createElement("h3",null,(0,r.msg)("usage07")),o.default.createElement(g,null,o.default.createElement("img",{width:300,src:"/images/usage/usage06.png",alt:""})),o.default.createElement(p,null,o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("pin_msg")),": ",(0,r.msg)("pin_explain_msg")),o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("edit_msg")),": ",(0,r.msg)("edit_explain_msg")),o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("copy_msg")),": ",(0,r.msg)("copy_explain_msg")),o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("color_msg")),": ",(0,r.msg)("color_explain_msg")),o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("delete_msg")),": ",(0,r.msg)("delete_explain_msg")),o.default.createElement("li",null,o.default.createElement("b",null,(0,r.msg)("open_msg")),": ",(0,r.msg)("open_explain_msg"))))))}));t.default=u;const c=d.default.div`
  padding: 2rem 1rem;

  ${({$isInit:e})=>e&&d.css`
      border-radius: 0.5rem;
      background-color: #FFF7CC;
      position: relative;

      &:before {
        content: "";

        animation: 2s ease-out 0s 3 fluffyAnimation forwards;

        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        border-radius: 0.5rem;
        transform-origin: center;
      }

      /* ふわっと広がるアニメーション */
      @keyframes fluffyAnimation {
        0% {
          box-shadow: 0 0 0 0 #ffe600;
        }
        50% {
          box-shadow: 0 0 0 1rem rgba(255, 230, 0, 0);
        }
        100% {
          box-s
          box-shadow: 0 0 0 2rem rgba(255, 230, 0, 0);
        }
      }
    `}

  ${({$isInit:e})=>!e&&d.css`
      /* border: 1px solid #ccc; */
      background-color: #fff;
    `}
`,s=d.default.div`
  position: relative;
`,f=d.default.div`
  margin-bottom: 2.5rem;

  & > h3 {
    text-align: center;
    line-height: 1.5;
    font-size: 1rem;
    margin-bottom: 0.5rem;
  }

  a {
    color: #002761;
    text-decoration: underline;
  }
`,m=d.default.h2`
  text-align: center;
  font-size: 1.5rem;
  margin-bottom: 1.5rem;
  font-weight: bold;
`,g=d.default.div`
  text-align: center;
  margin-bottom: 1rem;

  & > img {
    max-width: 30rem;
    margin: auto;
  }
`,p=d.default.ol`
  font-size: 0.875rem;
  width: 25rem;
  list-style: decimal;
  margin: auto;

  & > li {
    margin-bottom: 0.5rem;
    line-height: 1.25;
  }
`},2691:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.useClipboard=void 0;const l=n(7294);t.useClipboard=()=>{const[e,t]=(0,l.useState)(!1);return{isSuccessCopy:e,copyClipboard:(0,l.useCallback)((e=>{navigator.clipboard.writeText(e).then((()=>{t(!0),setTimeout((()=>{t(!1)}),1e3)}))}),[])}}},3104:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.useNoteSize=t.useNotePosition=t.initialPositionY=t.initialPositionX=t.useNoteEdit=t.NOTE_LEFT_POSITION=t.NOTE_TOP_POSITION=t.MIN_NOTE_HEIGHT=t.MIN_NOTE_WIDTH=void 0;const l=n(7294),a=n(4020);t.MIN_NOTE_WIDTH=160,t.MIN_NOTE_HEIGHT=120,t.NOTE_TOP_POSITION=0,t.NOTE_LEFT_POSITION=0,t.useNoteEdit=({title:e,description:n,position_x:a,position_y:i,width:o=t.MIN_NOTE_WIDTH,height:r=t.MIN_NOTE_HEIGHT,is_open:d})=>{const[u,c]=(0,l.useState)(e),[s,f]=(0,l.useState)(n),[m,g]=(0,l.useState)(d),{positionX:p,positionY:S,setPosition:h}=(0,t.useNotePosition)(a,i),{width:_,height:v,setSize:b}=(0,t.useNoteSize)(o,r),E=(0,l.useCallback)((e=>{const n=e?-1:1;return{positionX:(null!=p?p:(0,t.initialPositionX)())+window.scrollX*n,positionY:(null!=S?S:(0,t.initialPositionY)())+window.scrollY*n}}),[p,S]);return(0,l.useEffect)((()=>{g(d)}),[d]),(0,l.useEffect)((()=>{f(n)}),[n]),(0,l.useEffect)((()=>{c(e)}),[e]),{editTitle:u,setEditTitle:c,editDescription:s,setEditDescription:f,editIsOpen:m,setEditIsOpen:g,editPositionX:p,editPositionY:S,setEditPosition:h,editWidth:_,editHeight:v,setEditSize:b,getFixedPosition:E}},t.initialPositionX=()=>(window.innerWidth-a.DEAULT_NOTE_WIDTH)/2,t.initialPositionY=()=>(window.innerHeight-a.DEAULT_NOTE_HEIGHT)/2,t.useNotePosition=(e,n)=>{const[a,i]=(0,l.useState)(e),[o,r]=(0,l.useState)(n),d=(0,l.useCallback)(((e,n)=>{i(void 0===e?void 0:e>=t.NOTE_LEFT_POSITION?e:t.NOTE_LEFT_POSITION),r(void 0===n?void 0:n>=t.NOTE_TOP_POSITION?n:t.NOTE_TOP_POSITION)}),[]);return(0,l.useEffect)((()=>{d(e,n)}),[e,n]),{positionX:a,positionY:o,setPosition:d}},t.useNoteSize=(e=t.MIN_NOTE_WIDTH,n=t.MIN_NOTE_HEIGHT)=>{const[a,i]=(0,l.useState)(e),[o,r]=(0,l.useState)(n),d=(0,l.useCallback)(((e,n)=>{i(e>=t.MIN_NOTE_WIDTH?e:t.MIN_NOTE_WIDTH),r(n>=t.MIN_NOTE_HEIGHT?n:t.MIN_NOTE_HEIGHT)}),[]);return{width:a,height:o,setSize:d}}},3340:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.useNoteDownload=t.handleDownload=void 0;const l=n(8593);t.handleDownload=(e,t="text/csv",n="csv")=>{const a=(new Date).toISOString().slice(0,10).replace(/-/g,""),i=`export_${(0,l.msg)("app_name")}_${a}.${n}`;var o=new Blob([e],{type:t});const r=document.createElement("a");r.href=URL.createObjectURL(o),r.download=i,document.body.appendChild(r),r.click()},t.useNoteDownload=()=>({handleDownload:t.handleDownload})},3683:(e,t,n)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.useQuery=void 0;const l=n(7294),a=n(3727);t.useQuery=function(){const{search:e}=(0,a.useLocation)();return(0,l.useMemo)((()=>new URLSearchParams(e)),[e])}},2490:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SSkeleton=t.SCardListItem=t.SCardList=t.SNoNoteText=t.SPageLinkEditButton=t.SPageLinkEditInputAlert=t.SPageLinkEditInput=t.SCurrentPageLinkEditButton=t.SCurrentPageLink=t.SCurrentPageLinkArea=t.SCurrentPageCloseButton=t.SCurrentPageTitle=t.SCurrentPageFaviconImage=t.SCurrentPageAreaHeader=t.SCurrentPageArea=t.SSelect=t.SSelectIcon=t.SSelectWrap=t.SInput=t.SInputIcon=t.SInputWrap=t.SMainRightHeader=t.SSideNavItemLink=t.SSideNavItemTitle=t.SSideNavItemHeader=t.SFaviconImage=t.SSideNavItem=t.SSideNav=void 0;const r=i(n(2788)),d=n(153),u=o(n(1982)),c=o(n(7237)),s=o(n(7328));t.SSideNav=r.default.ul`
  list-style: none;
  padding: 1.5em 0.75em;
`,t.SSideNavItem=r.default.a`
  display: block;
  border-radius: 0.5em;
  padding: 0.75em;
  margin-bottom: 0.125em;
  cursor: pointer;

  &:hover {
    background-color: rgba(0, 0, 0, 0.05);
  }

  ${({$isActive:e})=>e&&r.css`
      font-weight: bold;
      cursor: default;
      background-color: rgb(0, 0, 0, 20%);

      &:hover {
        background-color: rgb(0, 0, 0, 20%);
      }

      ${t.SSideNavItemLink} {
        color: #000;
      }
    `}
`,t.SFaviconImage=r.default.img`
  width: 1em;
  height: 1em;
  margin-right: 0.5em;
`,t.SSideNavItemHeader=r.default.div`
  word-break: break-all;
  display: flex;
  align-items: center;
`,t.SSideNavItemTitle=r.default.p`
  overflow: hidden;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  -webkit-line-clamp: 2;
  font-size: 0.875em;
  line-height: 1.25;
  flex: 1;
`,t.SSideNavItemLink=r.default.p`
  font-weight: normal;
  font-size: 0.75em;
  margin-top: 0.25em;
  margin-left: 2em;
  display: block;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  color: #aaa;
`,t.SMainRightHeader=r.default.div`
  display: flex;
  margin-bottom: 0.25em;
`,t.SInputWrap=r.default.div`
  position: relative;
  flex: 1;
`,t.SInputIcon=(0,r.default)(d.MagnifyingGlassIcon)`
  position: absolute;
  left: 0.75em;
  top: 50%;
  width: 1.25em;
  transform: translateY(-50%);
  pointer-events: none;
`,t.SInput=r.default.input`
  border: 1px solid rgba(0, 0, 0, 0.1);
  padding: 0.5em 0.75em 0.5em 2.25em;
  border-radius: 999em;
  width: 100%;
  background-color: #fff;

  &:hover,
  &:focus {
    border-color: #fcd34d;
  }
`,t.SSelectWrap=r.default.div`
  position: relative;
  margin-left: 0.75em;
  width: 12em;
`,t.SSelectIcon=(0,r.default)(d.ArrowsUpDownIcon)`
  position: absolute;
  left: 0.5em;
  top: 50%;
  width: 1.25em;
  transform: translateY(-50%);
  pointer-events: none;
`,t.SSelect=r.default.select`
  border: 1px solid rgba(0, 0, 0, 0.1);
  padding: 0.5em 0.75em 0.5em 2em;
  border-radius: 0.2em;
  cursor: pointer;
  width: 100%;
  background-color: #fff;

  &:hover,
  &:focus {
    border-color: #fcd34d;
  }
`,t.SCurrentPageArea=r.default.div`
  margin-top: 0.75em;
  padding: 0.75em;
  border-radius: 0.25em;
  background-color: rgba(0, 0, 0, 0.08);
`,t.SCurrentPageAreaHeader=r.default.div`
  display: flex;
  align-items: center;
`,t.SCurrentPageFaviconImage=r.default.img`
  width: 1em;
  height: 1em;
  margin-right: 0.5em;
`,t.SCurrentPageTitle=r.default.p`
  flex: 1;
  font-size: 1em;
  margin-bottom: 0.25em;
`,t.SCurrentPageCloseButton=(0,r.default)(s.default)`
  margin-left: 0.5em;
`,t.SCurrentPageLinkArea=r.default.div`
  display: flex;
  align-items: baseline;
`,t.SCurrentPageLink=r.default.a`
  display: inline-block;
  margin-left: 1.5em;
  word-break: break-all;
  text-decoration: underline;
  color: #00379e;
`,t.SCurrentPageLinkEditButton=(0,r.default)(s.default)`
  margin-left: 0.5em;
  min-width: 1.25rem;
`,t.SPageLinkEditInput=r.default.input`
  background-color: #fff;
  border: 1px solid rgba(0, 0, 0, 0.1);
  padding: 0.5em 0.75em;
  flex: 1;
  width: 100%;
`,t.SPageLinkEditInputAlert=r.default.p`
  margin-top: 0.25rem;
  color: #888;
  font-size: 0.75rem;
`,t.SPageLinkEditButton=(0,r.default)(c.default)`
  margin-left: 0.5em;
  padding: 0.5em 0.75em;
`,t.SNoNoteText=r.default.p`
  padding: 1em;
  color: #aaa;
`,t.SCardList=r.default.div`
  flex: 1;
  display: flex;
  flex-wrap: wrap;
  padding: 0.25em 0;
  margin: 0 -0.5em;
`,t.SCardListItem=r.default.div`
  padding: 0.5em;
  width: 100%;
`,t.SSkeleton=(0,r.default)(u.default)`
  margin-top: 1em;
`},5934:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__awaiter||function(e,t,n,l){return new(n||(n=Promise))((function(a,i){function o(e){try{d(l.next(e))}catch(e){i(e)}}function r(e){try{d(l.throw(e))}catch(e){i(e)}}function d(e){var t;e.done?a(e.value):(t=e.value,t instanceof n?t:new n((function(e){e(t)}))).then(o,r)}d((l=l.apply(e,t||[])).next())}))},r=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const d=i(n(7294)),u=n(6550),c=n(153),s=n(8274),f=r(n(7477)),m=n(3683),g=n(2490),p=n(6832),S=r(n(4292)),h=i(n(9520)),_=n(8593),v=s.List,b=s.AutoSizer,E=s.CellMeasurer,y=()=>d.default.createElement(d.default.Fragment,null,d.default.createElement("div",{style:{display:"flex"}},d.default.createElement(g.SSkeleton,{style:{borderRadius:"999rem",marginRight:"1rem"},variant:"rounded",width:"100%",height:36}),d.default.createElement(g.SSkeleton,{variant:"rounded",width:192,height:36})),d.default.createElement(g.SSkeleton,{variant:"rounded",width:"100%",height:100}),d.default.createElement(g.SSkeleton,{variant:"rounded",width:"100%",height:100}),d.default.createElement(g.SSkeleton,{variant:"rounded",width:"100%",height:100}));t.default=()=>{const[e,t]=(0,d.useState)(""),[n,l]=(0,d.useState)([]),[a,i]=(0,d.useState)([]),[r,O]=(0,d.useState)("created_at"),[w,I]=(0,d.useState)(""),[C,D]=(0,d.useState)(!1),P=(0,m.useQuery)(),x=(0,u.useHistory)(),N=P.get("filter")?Number(P.get("filter")):void 0,M=(0,d.useMemo)((()=>a.find((e=>e.id===N))),[a,N]),k=(0,d.useMemo)((()=>{const e=(void 0===N?n:n.filter((e=>e.page_info_id===N))).filter((e=>{var t,n;return""===w||(null===(t=e.title)||void 0===t?void 0:t.includes(w))||(null===(n=e.description)||void 0===n?void 0:n.includes(w))}));return"updated_at"===r?[...e].sort(((e,t)=>{var n,l;return new Date(null!==(n=null==e?void 0:e.updated_at)&&void 0!==n?n:"1900/01/01")<new Date(null!==(l=null==t?void 0:t.updated_at)&&void 0!==l?l:"1900/01/01")?1:-1})):"created_at"===r?[...e].sort(((e,t)=>{var n,l;return new Date(null!==(n=null==e?void 0:e.created_at)&&void 0!==n?n:"1900/01/01")<new Date(null!==(l=null==t?void 0:t.created_at)&&void 0!==l?l:"1900/01/01")?1:-1})):"title"===r?[...e].sort(((e,t)=>{var n,l;return(null!==(n=null==e?void 0:e.title)&&void 0!==n?n:"")>(null!==(l=null==t?void 0:t.title)&&void 0!==l?l:"")?1:-1})):e}),[w,r,n,N]),T=(0,d.useMemo)((()=>{const e=""===w?[...a]:[...a].filter((e=>{var t,n;return(null===(t=e.page_url)||void 0===t?void 0:t.includes(w))||(null===(n=e.page_title)||void 0===n?void 0:n.includes(w))}));return"updated_at"===r?[...e].sort(((e,t)=>{var n,l;return new Date(null!==(n=null==e?void 0:e.updated_at)&&void 0!==n?n:"1900/01/01")<new Date(null!==(l=null==t?void 0:t.updated_at)&&void 0!==l?l:"1900/01/01")?1:-1})):"created_at"===r?[...e].sort(((e,t)=>{var n,l;return new Date(null!==(n=null==e?void 0:e.created_at)&&void 0!==n?n:"1900/01/01")<new Date(null!==(l=null==t?void 0:t.created_at)&&void 0!==l?l:"1900/01/01")?1:-1})):"title"===r?[...e].sort(((e,t)=>{var n,l;return(null!==(n=null==e?void 0:e.page_title)&&void 0!==n?n:"")>(null!==(l=null==t?void 0:t.page_title)&&void 0!==l?l:"")?1:-1})):e.reverse()}),[w,a,r]),j=(0,d.useCallback)((e=>{O(e.target.value)}),[]),L=(0,d.useCallback)((e=>{I(e.target.value)}),[]),A=e=>{window.scrollTo(0,0),e?P.set("filter",`${e}`):P.delete("filter"),I(""),x.push({search:P.toString()})},H=e=>o(void 0,void 0,void 0,(function*(){var t;try{const n=null===(t=a.find((t=>t.id===e.page_info_id)))||void 0===t?void 0:t.page_url,{notes:o,pageInfos:r}=yield h.sendUpdateNote(e,n);return o&&l(o),r&&i(r),!0}catch(e){return!1}})),F=e=>o(void 0,void 0,void 0,(function*(){var t;if(!confirm((0,_.msg)("confirm_remove_note_msg")))return!1;try{const n=null===(t=a.find((t=>t.id===e.page_info_id)))||void 0===t?void 0:t.page_url,{notes:o,pageInfos:r}=yield h.sendDeleteNote(e,n);return o&&l(o),r&&i(r),!0}catch(e){return!1}})),B=e=>o(void 0,void 0,void 0,(function*(){try{const[t]=yield chrome.tabs.query({url:e,currentWindow:!0});(null==t?void 0:t.id)?(yield chrome.tabs.update(t.id,{active:!0}),yield chrome.tabs.reload(t.id)):yield chrome.tabs.create({url:e})}catch(e){alert((0,_.msg)("failed_load_page_msg"))}})),[$,z]=(0,d.useState)(!1),[W,R]=(0,d.useState)(""),U=(0,d.useMemo)((()=>new s.CellMeasurerCache({fixedWidth:!0,defaultHeight:50})),[]);return(0,d.useEffect)((()=>{D(!0),h.sendFetchAllNotes().then((({notes:e,pageInfos:t})=>{e&&l(e),t&&i(t)})).finally((()=>{D(!1)})),h.sendFetchSetting().then((({setting:e})=>{var n;t(null!==(n=null==e?void 0:e.default_color)&&void 0!==n?n:"")}))}),[]),(0,d.useEffect)((()=>{z(!1)}),[N]),d.default.createElement(d.default.Fragment,null,d.default.createElement(p.GlobalStyle,null),d.default.createElement("div",{className:"p-4"},d.default.createElement(p.SContainer,null,d.default.createElement(S.default,{current:"memos"}),d.default.createElement(p.SMain,null,d.default.createElement(p.SMainLeft,null,d.default.createElement(g.SSideNav,null,d.default.createElement("li",null,d.default.createElement(g.SSideNavItem,{href:"#",onClick:e=>{e.preventDefault(),A()},$isActive:void 0===N},d.default.createElement(g.SSideNavItemHeader,null,d.default.createElement(g.SSideNavItemTitle,null,(0,_.msg)("show_all_note_msg"))))),T.map((e=>d.default.createElement("li",{key:e.id},d.default.createElement(g.SSideNavItem,{href:"#",onClick:t=>{t.preventDefault(),A(e.id)},$isActive:e.id===N},d.default.createElement(g.SSideNavItemHeader,null,d.default.createElement(g.SFaviconImage,{src:e.fav_icon_url}),d.default.createElement(g.SSideNavItemTitle,null,e.page_title)),d.default.createElement(g.SSideNavItemLink,null,e.page_url))))))),d.default.createElement(p.SMainRight,null,d.default.createElement(p.SMainRightInner,null,C&&d.default.createElement(y,null),!C&&d.default.createElement(p.SMainRightHeader,null,d.default.createElement(g.SInputWrap,null,d.default.createElement(g.SInputIcon,{fill:"rgba(0,0,0,0.4)"}),d.default.createElement(g.SInput,{placeholder:(0,_.msg)("search_query_msg"),onChange:L,value:w,type:"text"})),d.default.createElement(g.SSelectWrap,null,d.default.createElement(g.SSelectIcon,{fill:"rgba(0,0,0,0.4)"}),d.default.createElement(g.SSelect,{onChange:j},d.default.createElement("option",{value:"created_at"},(0,_.msg)("created_at_sort_option")),d.default.createElement("option",{value:"updated_at"},(0,_.msg)("updated_at_sort_option")),d.default.createElement("option",{value:"title"},(0,_.msg)("title_sort_option"))))),M&&d.default.createElement(g.SCurrentPageArea,null,d.default.createElement(g.SCurrentPageAreaHeader,null,d.default.createElement(g.SCurrentPageFaviconImage,{src:M.fav_icon_url}),d.default.createElement(g.SCurrentPageTitle,null,M.page_title),d.default.createElement(g.SCurrentPageCloseButton,{onClick:()=>A()},d.default.createElement(c.XMarkIcon,{fill:"rgba(0, 0, 0, 0.4)"}))),d.default.createElement(g.SCurrentPageLinkArea,null,$?d.default.createElement(d.default.Fragment,null,d.default.createElement("div",{style:{flex:1}},d.default.createElement(g.SPageLinkEditInput,{value:W,onChange:e=>R(e.target.value)}),d.default.createElement(g.SPageLinkEditInputAlert,null,(0,_.msg)("link_edit_note_msg"))),d.default.createElement(g.SPageLinkEditButton,{onClick:()=>{W!==(null==M?void 0:M.page_url)&&h.sendUpdatePageInfo(Object.assign(Object.assign({},M),{page_url:W})).then((({pageInfos:e})=>{e&&i(e)})),z(!1)}},(0,_.msg)("save_msg")),d.default.createElement(g.SPageLinkEditButton,{secondary:!0,onClick:()=>z(!1)},(0,_.msg)("cancel_msg"))):d.default.createElement(d.default.Fragment,null,d.default.createElement(g.SCurrentPageLink,{href:"#",onClick:e=>{var t;e.preventDefault(),B(null!==(t=M.page_url)&&void 0!==t?t:"")}},M.page_url),d.default.createElement(g.SCurrentPageLinkEditButton,{onClick:()=>{var e;R(null!==(e=null==M?void 0:M.page_url)&&void 0!==e?e:""),z(!0)}},d.default.createElement(c.PencilSquareIcon,{fill:"rgba(0, 0, 0, 0.4)"}))))),!C&&0===k.length&&d.default.createElement(g.SNoNoteText,null,(0,_.msg)("no_note_msg")),!C&&0!==k.length&&d.default.createElement(g.SCardList,null,d.default.createElement(b,null,(({height:t,width:n})=>d.default.createElement(v,{width:n,height:t,rowCount:k.length,deferredMeasurementCache:U,rowHeight:U.rowHeight,rowRenderer:({key:t,parent:n,index:l,style:i})=>{const o=k[l];return d.default.createElement(E,{key:o.id,cache:U,parent:n,columnIndex:0,rowIndex:l},(({measure:t,registerChild:n})=>d.default.createElement(g.SCardListItem,{id:`note-${o.page_info_id}-${o.id}`,ref:n,style:i},d.default.createElement(f.default,{note:o,defaultColor:e,showPageInfo:!M,currentPageInfoId:N,pageInfo:a.find((e=>e.id===o.page_info_id)),onDelete:F,onUpdate:H,onClickLink:B,onClickFilter:A,measure:t}))))}}))))))))))}},5060:function(e,t,n){var l=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0}),t.SSettingItemContent=t.SSettingItemTitle=t.SSettingItem=void 0;const a=l(n(2788));t.SSettingItem=a.default.div`
  margin-bottom: 3em;
`,t.SSettingItemTitle=a.default.div`
  margin-bottom: 0.5em;
  font-size: 1rem;
  font-weight: bold;
  color: #000;
`,t.SSettingItemContent=a.default.div``},2747:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t},o=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const r=i(n(7294)),d=n(6832),u=n(5060),c=o(n(4292)),s=i(n(9520)),f=o(n(5801)),m=n(8593),g=n(3340),p=o(n(7237)),S=o(n(4294));t.default=()=>{const[e,t]=(0,r.useState)(""),[n,l]=(0,r.useState)(!1),[a,i]=(0,r.useState)([]),[o,h]=(0,r.useState)([]),{handleDownload:_}=(0,g.useNoteDownload)();return(0,r.useEffect)((()=>{l(!0),s.sendFetchSetting().then((({setting:e})=>{var n;t(null!==(n=null==e?void 0:e.default_color)&&void 0!==n?n:"")})).finally((()=>{l(!1)})),s.sendFetchAllNotes().then((({notes:e,pageInfos:t})=>{e&&i(e),t&&h(t)}))}),[]),r.default.createElement(r.default.Fragment,null,r.default.createElement(d.GlobalStyle,null),r.default.createElement("div",{className:"p-4"},r.default.createElement(d.SContainer,null,r.default.createElement(c.default,{current:"setting"}),r.default.createElement(d.SMain,null,r.default.createElement(d.SMainContent,null,!n&&r.default.createElement(r.default.Fragment,null,r.default.createElement(u.SSettingItem,null,r.default.createElement(u.SSettingItemTitle,null,(0,m.msg)("default_color_msg")),r.default.createElement(u.SSettingItemContent,null,r.default.createElement(f.default,{color:e,onChangeColor:e=>{s.sendUpdateDefaultColor(e).then((({setting:e})=>{var n;t(null!==(n=null==e?void 0:e.default_color)&&void 0!==n?n:"")}))}}))),r.default.createElement(u.SSettingItem,null,r.default.createElement(u.SSettingItemTitle,null,(0,m.msg)("export_msg")),r.default.createElement(u.SSettingItemContent,null,r.default.createElement(p.default,{onClick:()=>{const e=a.map((e=>{var t;return`${e.id}, ${e.title?`"${e.title}"`:""}, ${e.description?`"${e.description}"`:""}, ${null===(t=o.find((t=>t.id===e.page_info_id)))||void 0===t?void 0:t.page_url}`})).join("\n");_("id, title, description, url\n"+e)}},(0,m.msg)("csv_download_msg")),r.default.createElement(p.default,{onClick:()=>{const e=a.map((e=>{var t,n,l;return`id: ${e.id}\ntitle: ${null!==(t=e.title)&&void 0!==t?t:""}\npage: ${null===(n=o.find((t=>t.id===e.page_info_id)))||void 0===n?void 0:n.page_url}\ncontent:\n${null!==(l=e.description)&&void 0!==l?l:""}`})).join("\n------------------------------------------------------------\n");_(e,"text/plain","txt")},style:{marginLeft:"0.5rem"}},(0,m.msg)("text_download_msg")))),r.default.createElement(u.SSettingItem,null,r.default.createElement(u.SSettingItemTitle,null,(0,m.msg)("how_to_use_page_link_msg")),r.default.createElement(u.SSettingItemContent,null,r.default.createElement(S.default,null))),r.default.createElement(u.SSettingItem,null,r.default.createElement(u.SSettingItemTitle,null,(0,m.msg)("maker_msg")),r.default.createElement(u.SSettingItemContent,null,r.default.createElement("a",{href:"https://twitter.com/takumi_bv",target:"_blank"},"@takumi_bv")))))))))}},6832:function(e,t,n){var l=this&&this.__createBinding||(Object.create?function(e,t,n,l){void 0===l&&(l=n);var a=Object.getOwnPropertyDescriptor(t,n);a&&!("get"in a?!t.__esModule:a.writable||a.configurable)||(a={enumerable:!0,get:function(){return t[n]}}),Object.defineProperty(e,l,a)}:function(e,t,n,l){void 0===l&&(l=n),e[l]=t[n]}),a=this&&this.__setModuleDefault||(Object.create?function(e,t){Object.defineProperty(e,"default",{enumerable:!0,value:t})}:function(e,t){e.default=t}),i=this&&this.__importStar||function(e){if(e&&e.__esModule)return e;var t={};if(null!=e)for(var n in e)"default"!==n&&Object.prototype.hasOwnProperty.call(e,n)&&l(t,e,n);return a(t,e),t};Object.defineProperty(t,"__esModule",{value:!0}),t.SMainRightHeader=t.SMainContent=t.SMainRightInner=t.SMainRight=t.SMainLeft=t.SMain=t.SContainer=t.GlobalStyle=void 0;const o=i(n(2788)),r=n(6188);t.GlobalStyle=o.createGlobalStyle`
  ${r.resetCSS}

  body {
    font-size: 16px;
  }

  /* TODO dark theme https://medium.com/bigpanda-engineering/dark-theme-with-styled-components-a573dd898e2a */
  @media (prefers-color-scheme: dark) {
    /* body {
      background-color: #000;
      color: #fff;
    } */
  }
`,t.SContainer=o.default.div`
  position: relative;
`,t.SMain=o.default.div`
  overflow: auto;
  background-color: #fafaf6;
  height: 100vh;
  height: 100dvh;
`,t.SMainLeft=o.default.div`
  position: fixed;
  left: 0;
  top: 2.75em;
  bottom: 0;
  overflow-y: auto;
  width: 18em;
  padding-left: 1em;
  z-index: 1;
`,t.SMainRight=o.default.main`
  position: relative;
  padding: 4em 2em 0 19em;
  height: 100%;
`,t.SMainRightInner=o.default.div`
  height: 100%;
  display: flex;
  flex-direction: column;
`,t.SMainContent=o.default.main`
  padding: 4em 2em 1.5em;
  margin: 0 auto;
  max-width: 44rem;
`,t.SMainRightHeader=o.default.div`
  display: flex;
  margin-bottom: 0.25em;
`},2417:function(e,t,n){var l=this&&this.__importDefault||function(e){return e&&e.__esModule?e:{default:e}};Object.defineProperty(t,"__esModule",{value:!0});const a=l(n(7294)),i=l(n(3935)),o=n(3727),r=n(6550),d=l(n(5934)),u=l(n(2747));i.default.render(a.default.createElement(o.BrowserRouter,null,a.default.createElement(a.default.StrictMode,null,a.default.createElement(r.Switch,null,a.default.createElement(r.Route,{exact:!0,path:"/memos.html",component:d.default}),a.default.createElement(r.Route,{exact:!0,path:"/setting.html",component:u.default}),a.default.createElement(r.Route,{path:"*",component:()=>a.default.createElement(r.Redirect,{to:"/memos.html"})})))),document.getElementById("root"))},9520:function(e,t,n){var l=this&&this.__awaiter||function(e,t,n,l){return new(n||(n=Promise))((function(a,i){function o(e){try{d(l.next(e))}catch(e){i(e)}}function r(e){try{d(l.throw(e))}catch(e){i(e)}}function d(e){var t;e.done?a(e.value):(t=e.value,t instanceof n?t:new n((function(e){e(t)}))).then(o,r)}d((l=l.apply(e,t||[])).next())}))};Object.defineProperty(t,"__esModule",{value:!0}),t.sendUpdateDefaultColor=t.sendFetchSetting=t.sendUpdatePageInfo=t.sendFetchAllNotes=t.sendDeleteNote=t.sendUpdateNote=void 0;const a=n(7364),i=n(7755);t.sendUpdateNote=(e,t)=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.UPDATE_NOTE,a.OPTIONS,{url:t,note:e})})),t.sendDeleteNote=(e,t)=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.DELETE_NOTE,a.OPTIONS,{url:t,note:e})})),t.sendFetchAllNotes=()=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.GET_ALL_NOTES_AND_PAGE_INFO,a.OPTIONS)})),t.sendUpdatePageInfo=e=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.UPDATE_NOTE_INFO,a.OPTIONS,{pageInfo:e})})),t.sendFetchSetting=()=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.GET_SETTING,a.OPTIONS)})),t.sendUpdateDefaultColor=e=>l(void 0,void 0,void 0,(function*(){return yield(0,i.sendAction)(a.UPDATE_DEFAULT_COLOR,a.OPTIONS,{defaultColor:e})}))},4020:(e,t)=>{Object.defineProperty(t,"__esModule",{value:!0}),t.DEAULT_NOTE_HEIGHT=t.DEAULT_NOTE_WIDTH=void 0,t.DEAULT_NOTE_WIDTH=300,t.DEAULT_NOTE_HEIGHT=180}},a={};function i(e){var t=a[e];if(void 0!==t)return t.exports;var n=a[e]={exports:{}};return l[e].call(n.exports,n,n.exports,i),n.exports}i.m=l,e=[],i.O=(t,n,l,a)=>{if(!n){var o=1/0;for(c=0;c<e.length;c++){for(var[n,l,a]=e[c],r=!0,d=0;d<n.length;d++)(!1&a||o>=a)&&Object.keys(i.O).every((e=>i.O[e](n[d])))?n.splice(d--,1):(r=!1,a<o&&(o=a));if(r){e.splice(c--,1);var u=l();void 0!==u&&(t=u)}}return t}a=a||0;for(var c=e.length;c>0&&e[c-1][2]>a;c--)e[c]=e[c-1];e[c]=[n,l,a]},i.n=e=>{var t=e&&e.__esModule?()=>e.default:()=>e;return i.d(t,{a:t}),t},n=Object.getPrototypeOf?e=>Object.getPrototypeOf(e):e=>e.__proto__,i.t=function(e,l){if(1&l&&(e=this(e)),8&l)return e;if("object"==typeof e&&e){if(4&l&&e.__esModule)return e;if(16&l&&"function"==typeof e.then)return e}var a=Object.create(null);i.r(a);var o={};t=t||[null,n({}),n([]),n(n)];for(var r=2&l&&e;"object"==typeof r&&!~t.indexOf(r);r=n(r))Object.getOwnPropertyNames(r).forEach((t=>o[t]=()=>e[t]));return o.default=()=>e,i.d(a,o),a},i.d=(e,t)=>{for(var n in t)i.o(t,n)&&!i.o(e,n)&&Object.defineProperty(e,n,{enumerable:!0,get:t[n]})},i.g=function(){if("object"==typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"==typeof window)return window}}(),i.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),i.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},i.j=798,(()=>{var e={798:0};i.O.j=t=>0===e[t];var t=(t,n)=>{var l,a,[o,r,d]=n,u=0;if(o.some((t=>0!==e[t]))){for(l in r)i.o(r,l)&&(i.m[l]=r[l]);if(d)var c=d(i)}for(t&&t(n);u<o.length;u++)a=o[u],i.o(e,a)&&e[a]&&e[a][0](),e[a]=0;return i.O(c)},n=self.webpackChunkgithub_language_extension=self.webpackChunkgithub_language_extension||[];n.forEach(t.bind(null,0)),n.push=t.bind(null,n.push.bind(n))})(),i.nc=void 0;var o=i.O(void 0,[736],(()=>i(2417)));o=i.O(o)})();