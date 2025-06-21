'use strict';
/************************************************/
// var Launch = function() {
//   var data = undefined;
//   return {
//     load: async function() {
//       var response = await fetch('./config.json');
//       var json = await response.json();
//       this.data = await json;
//     },
//     get: async function() {
//       console.log("get");
//       return this.data;
//     }
//   }
// };

// (async () => {
//   var a = await new Launch();
//   await a.load();
//   console.log(await a.get());
// })();

// class Launch
// {
//   data = undefined;

//   constructor()
//   {
        
//   }

//   async init()
//   {
//     var response = await fetch('./config.json');
//     var json = await response.json();
//     this.data = await json;
//   }

//   async get()
//   {
//     console.log('get');
//     return this.data;
//   }
// }

(() =>
{
  const run = () =>
  {
    fetch('config.json')
  . then((response) =>
    {
      return response.json();
    })
  . then((config) =>
    {
      config.application.id         = chrome.runtime.id;
      config.application.name       = chrome.runtime.getManifest().name;
      config.application.desciption = chrome.runtime.getManifest().description;
      config.application.icons      = chrome.runtime.getManifest().icons;
      config.application.version    = chrome.runtime.getManifest().version;
      /************************************************/
      return config;
    })
  . then((config) =>
    {
      let url      = "";
      let options  = {};
      let callback = undefined;
      /************************************************/
      url = config.window.url;
      /************************************************/
      options.id            = config.window.id;
      options.frame         = 'none';
      options.minWidth      =  500;
      options.minHeight     =  500;
      options.maxWidth      =  -1;
      options.maxHeight     =  -1;
      options.bounds        = {};
      options.bounds.left   = 0;
      options.bounds.top    = 0;
      options.bounds.width  = 800;
      options.bounds.height = 600;
      /************************************************/
      callback = (createdWindow) =>
      {
        createdWindow.contentWindow.addEventListener('load', (event) =>
        {
          const $doc = createdWindow.contentWindow.document;
          /************************************************/
          const $header        = $doc.getElementById('header    '.trim());
          const $navigationBar = $doc.getElementById('navigation'.trim());
          const $logoIco       = $doc.getElementById('logo      '.trim());
          const $minimizeBtn   = $doc.getElementById('minimize  '.trim());
          const $maximizeBtn   = $doc.getElementById('maximize  '.trim());
          const $closeBtn      = $doc.getElementById('close     '.trim());
          const $main          = $doc.getElementById('main      '.trim());
          const $horizontal    = $doc.getElementById('horizontal'.trim());
          const $left          = $doc.getElementById('left      '.trim());
          const $vertical      = $doc.getElementById('vertical  '.trim());
          const $top           = $doc.getElementById('top       '.trim());
          const $panel         = $doc.getElementById('panel     '.trim());
          const $discord       = $doc.getElementById('discord   '.trim());
          const $bottom        = $doc.getElementById('bottom    '.trim());
          const $right         = $doc.getElementById('right     '.trim());
          const $footer        = $doc.getElementById('footer    '.trim());
          /************************************************/
          // minimizeBtn
          {
            $minimizeBtn.addEventListener('mouseover', (event) => { minimizeBtn_mouseover($minimizeBtn, event); });
            $minimizeBtn.addEventListener('mouseout' , (event) => { minimizeBtn_mouseout ($minimizeBtn, event); });
            $minimizeBtn.addEventListener('click'    , (event) => { minimizeBtn_click    ($minimizeBtn, event); });
          }
          /************************************************/
          // maximizeBtn
          {
            $maximizeBtn.addEventListener('mouseover', (event) => { maximizeBtn_mouseover($maximizeBtn, event); });
            $maximizeBtn.addEventListener('mouseout' , (event) => { maximizeBtn_mouseout ($maximizeBtn, event); });
            $maximizeBtn.addEventListener('click'    , (event) => { maximizeBtn_click    ($maximizeBtn, event); });
          }
          /************************************************/
          // closeBtn
          {
            $closeBtn.addEventListener('mouseover', (event) => { closeBtn_mouseover($closeBtn, event); });
            $closeBtn.addEventListener('mouseout' , (event) => { closeBtn_mouseout ($closeBtn, event); });
            $closeBtn.addEventListener('click'    , (event) => { closeBtn_click    ($closeBtn, event); });
          }
          /************************************************/
          // discord
          {
            $discord.partition = `persist:${config.webview.partition}`;
            $discord.src = `${config.webview.origin}/${config.webview.application}`;
            /************************************************/
            $discord.addContentScripts
            ([{
              name: 'config',
              matches: [ `${config.webview.origin}/${config.webview.all}` ],
              js:
              {
                code:
                `
                var inlineScript = document.createElement('script');
                inlineScript.innerHTML += \`
                  window.config = ${JSON.stringify(config)};
                \`;
                document.documentElement.append(inlineScript);
                `
              },
              run_at: 'document_start'
            }]);
            $discord.addContentScripts
            ([{
              name: 'notification',
              matches: [ `${config.webview.origin}/${config.webview.all}` ],
              js:
              {
                files: [ 'scripts/notification.js' ]
              },
              run_at: 'document_start'
            }]);
            $discord.addContentScripts
            ([{
              name: 'storage',
              matches: [ `${config.webview.origin}/${config.webview.all}` ],
              js:
              {
                files: [ 'scripts/storage.js' ]
              },
              run_at: 'document_start'
            }]);
            /************************************************/
            $discord.addEventListener('permissionrequest', (event) => { discord_permissionrequest($discord, event); });
            $discord.addEventListener('newwindow'        , (event) => { discord_newwindow        ($discord, event); });
            $discord.addEventListener('contentload'      , (event) => { discord_contentload      ($discord, event); });
          }
          /************************************************/
          createdWindow.contentWindow.chrome.runtime.onMessageExternal.addListener((message, sender, sendResponse) =>
          {
            if (message.command == 'notification')
            {
              const options = {};
              /************************************************/
              options.priority = 1;
              options.type     = 'basic';
              options.title    = message.data.caption;
              options.message  = message.data.text;
              options.iconUrl  = message.data.image || 'app.png';
              /************************************************/
              createdWindow.contentWindow.chrome.notifications.update(message.data.id, {}, (wasUpdated) =>
              {
                (wasUpdated)
              ? chrome.notifications.update(message.data.id, options)
              : chrome.notifications.create(message.data.id, options);
              }),
              createdWindow.isMinimized() && createdWindow.drawAttention();
            }
            /************************************************/
            if (
              message.command  == 'storage                  '.trim() &&
              message.data.key == 'UnsyncedUserSettingsStore'.trim()
            ) {
              config.window.useSystemTheme = JSON.parse(message.data.value)._state['useSystemTheme'];
            }
            /************************************************/
            if (
              message.command  == 'storage   '.trim() &&
              message.data.key == 'ThemeStore'.trim()
            ) {
              config.window.theme = JSON.parse(message.data.value)['_state']['theme'];
            }
            /************************************************/
            if (
              message.command  == 'storage                  '.trim() &&
              message.data.key == 'UnsyncedUserSettingsStore'.trim()
            ) {
              config.window.darkSidebar = JSON.parse(message.data.value)._state['darkSidebar'];
            }
            /************************************************/
            updateUI();
          });
          createdWindow.contentWindow.chrome.notifications.onClicked.addListener((notificationId) =>
          {
            createdWindow.contentWindow.chrome.notifications.clear(notificationId, () =>
            {
              createdWindow.isMinimized() && createdWindow.show();
            });
          });
          /************************************************/
          const updateUI = ((type, target) =>
          {
            const dark_light =
            ((config.window.useSystemTheme == 2 &&  window.matchMedia('(prefers-color-scheme: dark)').matches                              ) && 'd') ||
            ((config.window.useSystemTheme == 2 && !window.matchMedia('(prefers-color-scheme: dark)').matches &&  config.window.darkSidebar) && 'd') ||
            ((config.window.useSystemTheme == 2 && !window.matchMedia('(prefers-color-scheme: dark)').matches && !config.window.darkSidebar) && 'l') ||
            ((config.window.useSystemTheme == 1 && config.window.theme == 'dark '.trim()                                                   ) && 'd') ||
            ((config.window.useSystemTheme == 1 && config.window.theme == 'light'.trim()                      &&  config.window.darkSidebar) && 'd') ||
            ((config.window.useSystemTheme == 1 && config.window.theme == 'light'.trim()                      && !config.window.darkSidebar) && 'l') ||
            undefined;
            /************************************************/
            // header
            {
              $header.style.position = 'absolute';
              $header.style.inset    = '0px auto auto 0px';
              $header.style.margin   = '0px 0px 0px 0px';
              $header.style.border   = '0px 0px 0px 0px';
              $header.style.padding  = '0px 0px 0px 0px';
              $header.style.width    = '100%';
              $header.style.height   = '22px';
            }
            // navigationBar
            {
              $navigationBar.style.position        = 'absolute';
              $navigationBar.style.inset           = '0px auto auto 0px';
              $navigationBar.style.margin          = '0px 0px 0px 0px';
              $navigationBar.style.border          = '0px 0px 0px 0px';
              $navigationBar.style.padding         = '0px 0px 0px 0px';
              $navigationBar.style.width           = '100%';
              $navigationBar.style.height          = '100%';
              $navigationBar.style.backgroundColor = (dark_light == 'd') ? '#202225' : $navigationBar.style.backgroundColor;
              $navigationBar.style.backgroundColor = (dark_light == 'l') ? '#e3e5e8' : $navigationBar.style.backgroundColor;
              $navigationBar.style.webkitAppRegion = 'drag';
            }
            // logoIco
            {
              $logoIco.style.position           = 'absolute';
              $logoIco.style.inset              = '6px auto auto 10px';
              $logoIco.style.margin             = '0px 0px 0px 0px';
              $logoIco.style.border             = '0px 0px 0px 0px';
              $logoIco.style.padding            = '0px 0px 0px 0px';
              $logoIco.style.width              = '60px';
              $logoIco.style.height             = '10px';
              $logoIco.style.backgroundImage    = 'url(/images/logo.svg)';
              $logoIco.style.backgroundRepeat   = 'no-repeat';
              $logoIco.style.backgroundPosition = (dark_light == 'd') ? '0px   0px' : $logoIco.style.backgroundPosition;
              $logoIco.style.backgroundPosition = (dark_light == 'l') ? '0px -10px' : $logoIco.style.backgroundPosition;
              $logoIco.style.backgroundSize     = 'auto auto';
              $logoIco.style.webkitAppRegion    = 'no-drag';
            }
            // minimizeBtn
            {
              $minimizeBtn.style.position           = 'absolute';
              $minimizeBtn.style.inset              = '0px auto auto calc(100% - 28px - 28px - 28px)';
              $minimizeBtn.style.margin             = '0px 0px 0px 0px';
              $minimizeBtn.style.border             = '0px 0px 0px 0px';
              $minimizeBtn.style.padding            = '0px 0px 0px 0px';
              $minimizeBtn.style.width              = '28px';
              $minimizeBtn.style.height             = '100%';
              $minimizeBtn.style.backgroundColor    = (dark_light == 'd'                               && target != $minimizeBtn) ? '       '.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundColor    = (dark_light == 'l'                               && target != $minimizeBtn) ? '       '.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseover'.trim() && target == $minimizeBtn) ? '#282b2e'.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseover'.trim() && target == $minimizeBtn) ? '#dadde0'.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseout '.trim() && target == $minimizeBtn) ? '       '.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseout '.trim() && target == $minimizeBtn) ? '       '.trim() : $minimizeBtn.style.backgroundColor;
              $minimizeBtn.style.backgroundImage    = 'url(/images/minimize.svg)';
              $minimizeBtn.style.backgroundRepeat   = 'no-repeat';
              $minimizeBtn.style.backgroundPosition = (dark_light == 'd'                               && target != $minimizeBtn) ? '  0px   0px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundPosition = (dark_light == 'l'                               && target != $minimizeBtn) ? '  0px -22px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseover'.trim() && target == $minimizeBtn) ? '-28px   0px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseover'.trim() && target == $minimizeBtn) ? '-28px -22px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseout '.trim() && target == $minimizeBtn) ? '  0px   0px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseout '.trim() && target == $minimizeBtn) ? '  0px -22px' : $minimizeBtn.style.backgroundPosition;
              $minimizeBtn.style.backgroundSize     = 'auto auto';
              $minimizeBtn.style.webkitAppRegion    = 'no-drag';
            }
            // maximizeBtn
            {
              $maximizeBtn.style.position           = 'absolute';
              $maximizeBtn.style.inset              = '0px auto auto calc(100% - 28px - 28px)';
              $maximizeBtn.style.margin             = '0px 0px 0px 0px';
              $maximizeBtn.style.border             = '0px 0px 0px 0px';
              $maximizeBtn.style.padding            = '0px 0px 0px 0px';
              $maximizeBtn.style.width              = '28px';
              $maximizeBtn.style.height             = '100%';
              $maximizeBtn.style.backgroundColor    = (dark_light == 'd'                               && target != $maximizeBtn) ? '       '.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundColor    = (dark_light == 'l'                               && target != $maximizeBtn) ? '       '.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseover'.trim() && target == $maximizeBtn) ? '#282b2e'.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseover'.trim() && target == $maximizeBtn) ? '#dadde0'.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseout '.trim() && target == $maximizeBtn) ? '       '.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseout '.trim() && target == $maximizeBtn) ? '       '.trim() : $maximizeBtn.style.backgroundColor;
              $maximizeBtn.style.backgroundImage    = 'url(/images/maximize.svg)';
              $maximizeBtn.style.backgroundRepeat   = 'no-repeat';
              $maximizeBtn.style.backgroundPosition = (dark_light == 'd'                               && target != $maximizeBtn) ? '  0px   0px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundPosition = (dark_light == 'l'                               && target != $maximizeBtn) ? '  0px -22px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseover'.trim() && target == $maximizeBtn) ? '-28px   0px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseover'.trim() && target == $maximizeBtn) ? '-28px -22px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseout '.trim() && target == $maximizeBtn) ? '  0px   0px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseout '.trim() && target == $maximizeBtn) ? '  0px -22px' : $maximizeBtn.style.backgroundPosition;
              $maximizeBtn.style.backgroundSize     = 'auto auto';
              $maximizeBtn.style.webkitAppRegion    = 'no-drag';
            }
            // closeBtn
            {
              $closeBtn.style.position           = 'absolute';
              $closeBtn.style.inset              = '0px auto auto calc(100% - 28px)';
              $closeBtn.style.margin             = '0px 0px 0px 0px';
              $closeBtn.style.border             = '0px 0px 0px 0px';
              $closeBtn.style.padding            = '0px 0px 0px 0px';
              $closeBtn.style.width              = '28px';
              $closeBtn.style.height             = '100%';
              $closeBtn.style.backgroundColor    = (dark_light == 'd'                               && target != $closeBtn) ? '       '.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundColor    = (dark_light == 'l'                               && target != $closeBtn) ? '       '.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseover'.trim() && target == $closeBtn) ? '#f04747'.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseover'.trim() && target == $closeBtn) ? '#f04747'.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundColor    = (dark_light == 'd' && type == 'mouseout '.trim() && target == $closeBtn) ? '       '.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundColor    = (dark_light == 'l' && type == 'mouseout '.trim() && target == $closeBtn) ? '       '.trim() : $closeBtn.style.backgroundColor;
              $closeBtn.style.backgroundImage    = 'url(/images/close.svg)';
              $closeBtn.style.backgroundRepeat   = 'no-repeat';
              $closeBtn.style.backgroundPosition = (dark_light == 'd'                               && target != $closeBtn) ? '  0px   0px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundPosition = (dark_light == 'l'                               && target != $closeBtn) ? '  0px -22px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseover'.trim() && target == $closeBtn) ? '-28px   0px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseover'.trim() && target == $closeBtn) ? '-28px -22px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundPosition = (dark_light == 'd' && type == 'mouseout '.trim() && target == $closeBtn) ? '  0px   0px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundPosition = (dark_light == 'l' && type == 'mouseout '.trim() && target == $closeBtn) ? '  0px -22px' : $closeBtn.style.backgroundPosition;
              $closeBtn.style.backgroundSize     = 'auto auto';
              $closeBtn.style.webkitAppRegion    = 'no-drag';
            }
            // main
            {
              $main.style.position = 'absolute';
              $main.style.inset    = '22px auto auto 0px';
              $main.style.margin   = '0px 0px 0px 0px';
              $main.style.border   = '0px 0px 0px 0px';
              $main.style.padding  = '0px 0px 0px 0px';
              $main.style.width    = '100%';
              $main.style.height   = 'calc(100% - 22px)';
            }
            // horizontal
            {
              $horizontal.style.position = 'absolute';
              $horizontal.style.inset    = '0px auto auto 0px';
              $horizontal.style.margin   = '0px 0px 0px 0px';
              $horizontal.style.border   = '0px 0px 0px 0px';
              $horizontal.style.padding  = '0px 0px 0px 0px';
              $horizontal.style.width    = '100%';
              $horizontal.style.height   = '100%';
            }
            // left
            {
              $left.style.position = 'absolute';
              $left.style.inset    = '0px auto auto 0px';
              $left.style.margin   = '0px 0px 0px 0px';
              $left.style.border   = '0px 0px 0px 0px';
              $left.style.padding  = '0px 0px 0px 0px';
              $left.style.width    = '0px';
              $left.style.height   = '100%';
            }
            // vertical
            {
              $vertical.style.position = 'absolute';
              $vertical.style.inset    = '0px auto auto 0px';
              $vertical.style.margin   = '0px 0px 0px 0px';
              $vertical.style.border   = '0px 0px 0px 0px';
              $vertical.style.padding  = '0px 0px 0px 0px';
              $vertical.style.width    = '100%';
              $vertical.style.height   = '100%';
            }
            // top
            {
              $top.style.position = 'absolute';
              $top.style.inset    = '0px auto auto 0px';
              $top.style.margin   = '0px 0px 0px 0px';
              $top.style.border   = '0px 0px 0px 0px';
              $top.style.padding  = '0px 0px 0px 0px';
              $top.style.width    = '100%';
              $top.style.height   = '0px';
            }
            // panel
            {
              $panel.style.position = 'absolute';
              $panel.style.inset    = '0px auto auto 0px';
              $panel.style.margin   = '0px 0px 0px 0px';
              $panel.style.border   = '0px 0px 0px 0px';
              $panel.style.padding  = '0px 0px 0px 0px';
              $panel.style.width    = '100%';
              $panel.style.height   = '100%';
            }
            // discord
            {
              $discord.style.position = 'absolute';
              $discord.style.inset    = '0px auto auto 0px';
              $discord.style.margin   = '0px 0px 0px 0px';
              $discord.style.border   = '0px 0px 0px 0px';
              $discord.style.padding  = '0px 0px 0px 0px';
              $discord.style.width    = '100%';
              $discord.style.height   = '100%';
            }
            // bottom
            {
              $bottom.style.position = 'absolute';
              $bottom.style.inset    = '100% auto auto 0px';
              $bottom.style.margin   = '0px 0px 0px 0px';
              $bottom.style.border   = '0px 0px 0px 0px';
              $bottom.style.padding  = '0px 0px 0px 0px';
              $bottom.style.width    = '100%';
              $bottom.style.height   = '0px';
            }
            // right
            {
              $right.style.position = 'absolute';
              $right.style.inset    = '0px auto auto 100%';
              $right.style.margin   = '0px 0px 0px 0px';
              $right.style.border   = '0px 0px 0px 0px';
              $right.style.padding  = '0px 0px 0px 0px';
              $right.style.width    = '0px';
              $right.style.height   = '100%';
            }
            // footer
            {
              $footer.style.position = 'absolute';
              $footer.style.inset    = '100% auto auto 0px';
              $footer.style.margin   = '0px 0px 0px 0px';
              $footer.style.border   = '0px 0px 0px 0px';
              $footer.style.padding  = '0px 0px 0px 0px';
              $footer.style.width    = '100%';
              $footer.style.height   = '0px';
            }
          });
          /************************************************/
          const minimizeBtn_mouseover = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const minimizeBtn_mouseout = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const minimizeBtn_click = ((sender, event) => {
            createdWindow.minimize();
          });
          /************************************************/
          const maximizeBtn_mouseover = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const maximizeBtn_mouseout = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const maximizeBtn_click = ((sender, event) => {
            createdWindow.isMaximized()
          ? createdWindow.restore()
          : createdWindow.maximize();
          });
          /************************************************/
          const closeBtn_mouseover = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const closeBtn_mouseout = ((sender, event) => {
            updateUI(event.type, event.target);
          });
          /************************************************/
          const closeBtn_click = ((sender, event) => {
            createdWindow.close();
          });
          /************************************************/
          const discord_permissionrequest = ((sender, event) => {
            event.request.allow();
          })
          /************************************************/
          const discord_newwindow = ((sender, event) => {
            event.preventDefault();
            event.stopPropagation();
            /************************************************/
            (
              event.windowOpenDisposition == 'new_foreground_tab' &&
              /************************************************/
              (() =>
              {
                createdWindow.contentWindow.chrome.browser.openTab
                ({
                  url: event.targetUrl
                });
              })()
            ) || (
              // TODO: testing
              event.windowOpenDisposition == 'new_popup' &&
              /************************************************/
              (() =>
              {
                chrome.app.window.create
                (
                  'frames/index.html',
                  {},
                  () => {
                    console.log("new_popup event")
                  }
                )
              })()
            );
          });
          /************************************************/
          const discord_contentload = ((sender, event) => {
            updateUI();
          });
          /************************************************/
          updateUI();
        });
      };
      /************************************************/
      chrome.app.window.create(url, options, callback);
    })
  };
  /************************************************/
  chrome.app.runtime.onLaunched.addListener((launchData) =>
  {
    // run();
    (async () => {
      let a = await new _.DiscordWindow();
      /************************************************/
      await a.Show();
      /************************************************/
      // await a.LoadScript();
      // await a.LoadScript();
    })();
  });
  /************************************************/
  chrome.app.runtime.onRestarted.addListener(() =>
  {
    run();
  });
})();