// Этот файл предоставляется вам по лицензии Freeware.
// Все права на этот файл принадлежат компании АКСМА Софт http://sm.axmasoft.com.
// Вы соглашаетесь не распространять этот файл ни коим способом, а также не модифицировать и не изменять его. Также Вы не можете удалять или изменять сведения о правах собственности в копиях этого файла.
function GetURLHashVar(name)
{
name = name.trim();
var str = window.location.hash.substr(1);
str = str.split('&');
for (var key in str)
{
var arr = str[key].split('=');
arr[0] = arr[0].trim();
if (arr[0] && arr[1] && arr[0]==name)
{
arr[1] = arr[1].trim();
if (arr[1].toLowerCase()=='true')
{
return true;
}
else if (arr[1].toLowerCase()=='false')
{
return false;
}
else if (!isNaN(+arr[1]))
{
return parseInt(arr[1],10);
}
else
{
return arr[1];
}
}
}
return undefined;
}
var LangCode;
LangCode = GetURLHashVar('LangCode');
if (!LangCode)
{
if (window.location.hostname.substr(-3).toLowerCase()=='.ru' || window.location.hostname.substr(-3).toLowerCase()=='.рф')
{
LangCode = 'ru';
}
else
{
LangCode = 'en';
}
}
if (!LangCode)
{
LangCode = 'en';
}
var tmpdiv = document.createElement("div");
tmpdiv.innerHTML = "<!--[if lt IE 9]><i></i><![endif]-->";
var isIeLessThan9 = (tmpdiv.getElementsByTagName("i").length == 1);
if (isIeLessThan9)
{
if (LangCode=='ru')
{
alert('Internet Explorer версии ниже 9 не поддерживается! Обновите свой браузер.');
}
else
{
alert('Internet Explorer earlier than version 9 is not supported! Please upgrade your browser.');
}
document.location.href='http://www.google.com/intl/'+LangCode+'/chrome/browser/';
}
if (LangCode=='ru')
{
var Lang_Navigator = 'Навигатор';
var Lang_ToolButtons = [ 'Добавить', 'Действия', 'Вид', 'Сохранить', 'Загрузить', 'Новая', 'Запустить', 'Экспорт', Lang_Navigator, 'Помощь' ];
var Lang_EditorButtons = [ 'Закрыть и сохранить', 'Добавить ссылку', 'Добавить макрос', 'Отформатировать текст', 'Закомментировать текст', 'Установить цвет параграфа' ];
var Lang_SmallButtons = [ 'Назад', 'Тестировать', 'Искать далее', 'Табуляция/Автодополнение', 'Соединительные линии' ];
var Lang_HTMLSave = 'Сохранить';
var Lang_HTMLLoad = 'Восстановить';
var Lang_HTMLSaveSure = 'Хотите сохранить состояние?';
var Lang_HTMLLoadSure = 'Хотите восстановить состояние?';
var Lang_Error = 'Произошла ошибка кода';
var Lang_Audio = 'Аудио';
var Lang_UNDO = 'Назад';
var Lang_StartAgain = 'Хотите начать заново?';
var Lang_HTMLRestart = 'Начать заново';
var Lang_HTMLStart = 'Начать';
var Lang_Console = 'Консоль';
var Lang_HTMLMenu = 'Меню';
var Lang_HTMLPrefs = 'Настройки';
var Lang_HTMLTextSize = 'Размер текста';
var Lang_HTMLAnimOn = 'Включить анимацию';
var Lang_HTMLAnimOff = 'Отключить анимацию';
var Lang_HTMLSoundOn = 'Включить звуки';
var Lang_HTMLSoundOff = 'Отключить звуки';
var Lang_MacrosDescTHEMEdefault = 'Стандартная тема';
var Lang_MacrosDescTHEMEdark = 'Тёмная тема';
var Lang_MacrosDescTHEMEsepia = 'Тема «Сепия»';
var Lang_CreatedBy = 'использована&nbsp;платформа';
var Lang_JSrequires = 'Для работы файла требуется, чтобы Javascript был включен в Вашем браузере';
var Lang_Location = 'Параграф';
var Lang_NotFound = 'не найден';
var Lang_License = '<!DOCTYPE html>\n<!--\nиспользована платформа AXMA Story Maker\nhttp://sm.axmasoft.com/\nВы соглашаетесь ни коим образом не модифицировать и не изменять этот файл. Также Вы не можете удалять или изменять сведения о правах собственности в копиях этого файла.\n-->\n';
var Lang_Err_IncorrTitle = 'Некорректные символы в названии параграфа';
var Lang_Err_ForbExp = 'Запрещённое выражение';
var Lang_Err_AbsOpenMacro = 'Отсутствуют открывающиеся скобки макроса';
var Lang_Err_AbsClosMacro = 'Отсутствуют закрывающиеся скобки макроса';
var Lang_Err_IncorrectMacro = 'Некорректный макрос';
var Lang_Err_ExcessMacro = 'Лишний макрос';
var Lang_Err_MissingMacro = 'Отсутствует макрос';
var Lang_Err_MissingCond = 'Отсутствует условие в макросе';
var Lang_Err_OdnoRavno = 'Используйте "eq" или "==" вместо "="';
var Lang_Err_UnknownSysVar = 'Неизвестный системный объект';
var Lang_Err_AbsOpenLink = 'Отсутствуют открывающиеся скобки ссылки';
var Lang_Err_AbsClosLink = 'Отсутствуют закрывающиеся скобки ссылки';
var Lang_Err_IncorrectLink = 'Некорректная ссылка';
var Lang_Err_MissingDolOrQuot = 'Отсутствует $ или кавычки';
var Lang_Err_ForbFunction = 'Недопустимая функция';
var Lang_Err_TooMuchLinks = 'Слишком много ссылок в этом параграфе';
var Lang_Err_LinksNoEnd = 'Ссылки должны идти подряд в конце параграфа';
var Lang_Err_ForbPassage = 'Запрещённый параграф';
var Lang_NotFound2 = 'Ничего не найдено';
var Lang_Found = 'Найдено';
var Lang_Replaced = 'Заменено';
var Lang_Passages = 'параграфов';
var Lang_PassagesUpper = 'Параграфы';
var Lang_PassagesErrors = 'с ошибками';
var Lang_Chars = 'символов';
var Lang_NoPassages = 'Параграфы отсутствуют';
var Lang_NoPassage = 'Не выбран параграф';
var Lang_BadPassageTitle = 'Недопустимое название параграфа!';
var Lang_PleaseWait = 'Подождите, пожалуйста...';
var Lang_Create = 'создать';
var Lang_PassagesGroup = 'группа параграфов';
var Lang_SplitAll = 'Разъединить все';
var Lang_Back = 'Назад';
var Lang_Return = 'Вернуться';
var Lang_Grouped = 'Соединены';
var Lang_Saved = 'Сохранено';
var Lang_Saving = 'Сохраняется...';
var Lang_StorySaved = 'Состояние сохранено';
var Lang_StoryNotSaved = 'Ошибка при сохранении!';
var Lang_StoryNotLoaded = 'Не удалось восстановить состояние!';
var Lang_Loading = 'Загрузка...';
var Lang_Loaded = 'Загружено';
var Lang_AreYouSure = 'Вы уверены?';
var Lang_No = 'нет';
var Lang_LinesOn = 'Линии включены';
var Lang_LinesOff = 'Линии выключены';
var Lang_NO = 'Нет';
var Lang_YES = 'Да';
var Lang_FindInPassages = 'Поиск';
var Lang_MenuEdit = 'Редактировать';
var Lang_MenuDelete = 'Удалить';
var Lang_MenuDuplicate = 'Дублировать';
var Lang_MenuLinked = 'Связанные';
var Lang_MenuTest = 'Тестировать';
var Lang_MenuFindNReplace = 'Поиск и замена';
var Lang_MenuGotoLink = 'Перейти по ссылке';
var Lang_MenuUndo = 'Отменить изменения';
var Lang_MenuOutLinks = 'Исходящие';
var Lang_MenuInLinks = 'Входящие';
var Lang_MenuArrangeByTitle = 'Расставить по названию';
var Lang_MenuArrangeByColor = 'Расставить по цвету';
var Lang_MenuArrangeBySize = 'Расставить по размеру';
var Lang_MenuArrangeZoomOut = 'Уменьшить';
var Lang_MenuArrangeZoomIn = 'Увеличить';
var Lang_MenuSelectStory = 'Выбрать файл';
var Lang_MenuMergeStory = 'Добавить к текущему';
var Lang_MenuFromBackUp = 'Из резервной копии';
var Lang_NoBackUp = 'Нет резервной копии';
var Lang_MenuAddInventory = 'Добавить модуль Inventory';
var Lang_MenuNewStandart = 'Шаблон примера';
var Lang_MenuNewCompact = 'Стандартный шаблон';
var Lang_MenuNewClipboard = 'Из буфера обмена';
var Lang_MenuNewClipboardMess = 'Скопируйте исходный текст в буфер обмена и вставьте его в это поле';
var Lang_MenuPublishFile = 'Сохранить в HTML файл';
var Lang_MenuPublishInet = 'Отправить в библиотеку';
var Lang_MenuPublishInetSure = 'Вы уверены, что всё готово к публикации?';
var Lang_MenuQuickStart = 'Быстрый старт';
var Lang_MenuUserManual = 'Руководство пользователя';
var Lang_MenuProfile = 'Мой профиль';
var Lang_MenuToLibrary = 'Библиотека';
var Lang_MenuDesktopVersion = 'Скачать программу';
var Lang_AYSRemoveThisPassage = 'Вы действительно хотите удалить этот параграф?';
var Lang_AYSRemovePassage = 'Вы действительно хотите удалить параграф';
var Lang_AYSSaveNewStory = 'Вы действительно хотите создать новый файл?';
var Lang_AYSCreateNewStory = 'Вы действительно хотите создать новую игру?';
var Lang_AYSUndoChanges = 'Вы действительно хотите отменить все изменения, сделанные в параграфе?';
var Lang_AYSExitSourceEditor = 'Вы действительно хотите выйти из редактора исходного кода?';
var Lang_NotAvailEditPass = 'Недоступно при редактировании параграфа';
var Lang_NotAvailInBeta = 'Недоступно в бета-версии';
var Lang_NotAvailInEditor = 'Недоступно в редакторе исходного кода';
var Lang_ArrangeByTitle = 'Отсортировать параграфы по названию?';
var Lang_DontForgetSave = 'Не забудьте сохранить сделанные изменения!';
var Lang_UnableRemoveGroup = 'Невозможно удалить группу параграфов';
var Lang_NoSavedFiles = 'Нет сохранённых файлов';
var Lang_UpdateFound = 'Доступно обновление!';
var Lang_ReloadApp = 'Перегрузить приложение?';
var Lang_SourceCode = 'Исходный код';
var Lang_FoundDuplicates = 'Найдены и объединены дубликаты';
var Lang_FoundDuplicates2 = 'Найден дубликат параграфа';
var Lang_FindFind = 'Найти';
var Lang_FindCount = 'Подсчитать';
var Lang_FindCase = 'С учётом регистра';
var Lang_FindSelected = 'В выделенном';
var Lang_FindRegEx = 'Регулярное выражение';
var Lang_FindReplaceTo = 'Заменить на';
var Lang_FindReplace = 'Заменить';
var Lang_FindReplaceAll = 'Заменить все';
var Lang_Object = 'объект';
var Lang_MacroDisplay = 'Текст другого параграфа';
var Lang_MacroPicture = 'Основная иллюстрация';
var Lang_MacroFilter = 'Эффект на иллюстрацию';
var Lang_MacroMenu = 'Главное меню';
var Lang_MacroTitle = 'Заголовок страницы';
var Lang_MacroClrScr = 'Очистить страницу';
var Lang_MacroFade = 'Плавность текста';
var Lang_MacroNoAudio = 'Выключить музыку';
var Lang_MacroRow = 'Строка таблицы';
var Lang_MacroSet = 'Присвоить значение';
var Lang_MacroPrint = 'Вывести значение';
var Lang_MacroRandom = 'Случайное значение';
var Lang_MacroInput = 'Ввести значение';
var Lang_MacroIf = 'Условие';
var Lang_MacroGoto = 'Перейти к параграфу';
var Lang_MacroLoop = 'Цикл';
var Lang_MacroRepeat = 'Повторять параграф';
var Lang_MacroStop = 'Остановить параграф';
var Lang_MacroNop = 'Не создавать абзацы';
var Lang_MacroClass = 'Установить класс';
var Lang_MacroSound = 'Звуковой эффект';
var Lang_MacroImage = 'Изображение';
var Lang_LinkSimple = 'Обычная ссылка';
var Lang_LinkAdd = 'Добавляющая ссылка';
var Lang_LinkMulti = 'Ссылка с открытием окна';
var Lang_LinkNo = 'Невозвратная ссылка';
var Lang_LinkObj = 'Установить значения объектов';
var Lang_LinkBack = 'Ссылка &laquo;назад&raquo;';
var Lang_LinkReturn = 'Ссылка &laquo;вернуться&raquo;';
var Lang_LinkRestart = 'Ссылка &laquo;начать заново&raquo;';
var Lang_LinkFile = 'Ссылка на изображение, музыку или видео (YouTube&nbsp;/&nbsp;Vimeo)';
var Lang_LinkFileLocal = 'Изображение или музыка';
var Lang_FormatHTML = 'В сущности HTML';
var Lang_FormatLink = 'Ссылка';
var Lang_FormatItalic = 'Наклонный';
var Lang_FormatBold = 'Жирный';
var Lang_FormatUnderline = 'Подчёркнутый';
var Lang_FormatHeader = 'Заголовок';
var Lang_FormatMonospace = 'Моноширинный';
var Lang_FormatCenter = 'По центру';
var Lang_FormatRight = 'По правому краю';
var Lang_FormatBreak = 'Разрыв<br>строки';
var Lang_FormatTab = 'Табуляция';
var Lang_FormatTire = 'Тире';
var Lang_FormatQuotes = '&laquo;Кавычки&raquo;';
var Lang_FormatAsterisk = 'Звёздочки';
var Lang_FormatHorline = 'Горизонтальная линия';
var Lang_FormatSub = 'Нижний <sub>индекс</sub>';
var Lang_FormatSup = 'Верхний <sup>индекс</sup>';
var Lang_FormatLi = '• Строка списка';
var Lang_Server = 'http://hyperbook.ru';
var Lang_SignIn = 'Авторизация';
var Lang_Login = 'Логин (адрес e-mail)';
var Lang_Password = 'Пароль';
var Lang_Close = 'Закрыть';
var Lang_GraphicMode = 'Визуальный режим';
var Lang_SwitchTheme = 'Переключить тему';
var Lang_NeedOnline = 'Необходимо подключение к интернету';
var Lang_SaveToCloud = 'Экспортировать в облако';
var Lang_LoadFromCloud = 'Импортировать из облака';
var Lang_OnColorMode = 'Включить подсветку';
var Lang_OffColorMode = 'Выключить подсветку';
var Lang_Register = 'Зарегистрироваться';
var Lang_Error2 = 'Произошла ошибка';
var Lang_InText = 'В параграфах';
var Lang_RestoreStoryStyle = 'Восстановить';
var Lang_ThemeEdit = 'Изменить тему';
var Lang_CloseWindow = 'Закрыть окно?';
var Lang_Show = 'Показывать';
var Lang_ChangeButton = 'Изменить кнопку';
var Lang_Body = 'Основной фон';
var Lang_PrintBlock = 'Текстовый блок';
var Lang_PrintAddBlock = 'Всплывающий блок';
var Lang_PrintImage = 'Иллюстрация';
var Lang_PrintMenu = 'Меню';
var Lang_Toolbar = 'Панель инструментов';
var Lang_SpecialBlock = 'Служебный блок';
var Lang_PathToImage = 'путь/к/изображению';
var Lang_StoryMenu = 'Нет доступных действий';
var Lang_GlobalSettings = 'Настройки';
var Lang_ChangeTheme = 'Выбрать тему';
var Lang_Font = 'Шрифт';
var Lang_MainColor = 'Цвет основного фона';
var Lang_ShadowsStatBlocks = 'Тени у статичных блоков';
var Lang_SetTexture = 'Установить фон';
var Lang_RemoveTexture = 'Удалить фон';
var Lang_ColorBack = 'Цвет фона';
var Lang_ColorText = 'Цвет текста';
var Lang_ColorLink = 'Цвет ссылок';
var Lang_ColorHeader = 'Цвет заголовков';
var Lang_ButtonRadius = 'Закругление кнопок';
var Lang_FrameWidth = 'Ширина рамки';
var Lang_FrameSet = 'Изменить рамку';
var Lang_FrameRemove = 'Удалить рамку';
var Lang_NoProperties = 'Нет доступных свойств';
var Lang_SelectImageSize = 'Выберите изображение размером';
var Lang_NeedPaidAccount = 'Для совершения этой операции необходим профессиональный аккаунт';
var Lang_BuyPaidAccount = 'Купить профессиональный аккаунт';
var Lang_ChangeLang = 'Переключить язык';
var Lang_CreateStoryStyle = 'Параграф стилей';
var Lang_StoryStyleAlready = 'Параграф стилей уже создан';
var Lang_PassageTitle = 'Название параграфа';
var Lang_FileSound = 'Звук';
var Lang_FileImage = 'Спрайт';
var Lang_FileImageDelete = 'Удалить спрайт';
var Lang_FileImageDeleteAll = 'Удалить все спрайты';
var Lang_ChoiceMacro = 'Выбор действия';
var Lang_PurePassage = 'Обычный параграф';
var Lang_InputFileName = 'Введите имя файла!';
var Lang_FileNameExists = 'Файл с таким именем уже существует!';
var Lang_FileAnother = 'Другой';
var Lang_FileSelect = 'Выбрать файл';
var Lang_FileName = 'Имя файла';
var Lang_FileTooLarge = 'Слишком большой файл';
var Lang_FilesList = 'Список файлов';
var Lang_FilesNo = 'Нет файлов';
var Lang_FilesAll = 'Все файлы';
var Lang_FilesImages = 'Спрайты';
var Lang_FilesSounds = 'Звуки';
var Lang_FileRename = 'Переименовать';
var Lang_FileRemove = 'Удалить';
var Lang_EditNo = 'Недоступно в визуальном режиме';
var Lang_Overwrite = 'Перезаписать';
var Lang_Empty = 'Пусто';
var Lang_ShowSize = 'Показать размер';
var Lang_WantNewPub = 'Вы действительно хотите создать <u>новую</u> публикацию?';
var Lang_WantUpdPub = 'Вы действительно хотите <u>обновить</u> публикацию?';
var Lang_ErrorPub = 'При публикации произошла ошибка';
var Lang_SuccessPub = 'Успешно отправлено в библиотеку';
var Lang_MissReqPass = 'Отсутствует или пустой обязательный параграф';
var Lang_ErrorLoadFile = 'Ошибка загрузки файла';
var Lang_MultiView = 'Интерактивная история';
var Lang_ClassicView = 'Книга-игра';
var Lang_InterABook = 'Интерактивная аудиокнига';
var Lang_InterQuest = 'Классический квест или RPG';
var Lang_InterVisual = 'Визуальная новелла';
var Lang_NotForABook = 'Недоступно для аудиокниг';
var Lang_GameType = 'Тип игры';
var Lang_GameTheme = 'Тема оформления';
var Lang_DefaultFileName = 'Новая игра';
var Lang_AppWillBeRestarted = 'Приложение будет перезапущено. Продолжить?';
var Lang_NeedSignIn = 'Для сохранения необходимо авторизироваться';
var Lang_NeedFilesDir = 'Необходимо указать папку для локальных медиафайлов';
var Lang_MenuSetFilesDir = 'Указать папку для медиафайлов';
var Lang_Images = 'Изображения';
var Lang_AudioMP3 = 'MP3';
var Lang_RecentFiles = 'Последние файлы';
var Lang_Send = 'Отправить';
var Lang_PubNew = 'Новая публикация';
var Lang_AboutPub = 'Вы можете создать новую публикацию или обновить существующую';
var Lang_ReadMedia = 'Чтение медиафайла';
var Lang_ErrorReadMedia = 'Ошибка чтения файла';
var Lang_ErrorReadCode = 'Код ошибки';
var Lang_ErrorTotalSize = 'Общий размер медиафайлов не может быть больше 20 Мб';
var Lang_ErrorFileSize = 'Размер медиафайла не может быть больше 10 Мб';
var Lang_Sending = 'Отправка';
var Lang_Version = 'Версия';
var Lang_QuickStart = "Перед вами квадратики, соединённые между собой стрелками. Так выглядит стандартный шаблон, который генерируется при создании новой игры. Каждый квадратик — это параграф, т.е. небольшая порция текста вашей игры. <p></p>Кликните (тапните) дважды на любом параграфе — и Вы сможете отредактировать его текст.<p></p>Расставьте параграфы так, как вам удобно. Стрелки, соединяющие их, отображают ссылки, ведущие из одних параграфов в другие.<p></p>Ссылки позволяют читателю переходить от одного параграфа к другому, совершая придуманное вами путешествие по игре. А ссылка — это просто название другого параграфа, заключённое в двойные квадратные скобки. Вот так:<p><tt>[[Какой-то параграф]]</tt></p>Нажав на эту ссылку, читатель попадёт в параграф <i>Какой-то параграф</i>, если, конечно, он есть в вашей игре. Но как же его добавить?<p></p>Сделать это очень просто. Кликните на кнопку <i>Добавить</i>, введите имя параграфа, нажмите Ввод и введите текст параграфа. Это всё :) Теперь Вы можете закрыть окно редактирования нажатием на крестик и увидеть, что новый параграф появился на рабочем столе.<p></p>Таким образом Вы можете создавать и изменять любые параграфы, расширяя и усложняя сюжет вашей игры.<p></p>Самый главный параграф имеет имя <i>Start</i>. С него для читателя начинается придуманная вами игра. Не менее важный параграф имеет в себе имя автора игры (конечно, ваше!). Он называется <i>StoryAuthor</i>. А параграфы <i>StoryTitle</i> и  <i>StorySubtitle</i> содержат название и краткое введение в игру, соответственно.<p></p>Ну вот, Вы отредактировали стандартные параграфы, добавили несколько новых... Пора посмотреть на результат вашего труда! Для этого нажмите на кнопку <i>Запустить</i> и через секунду игра откроется во всей своей красе!<p></p>Конечно, потом вам захочется ещё более усложнить и расширить свою работу, добавив в игру предметы и персонажей, а потом и опубликовать её в интернете для своих друзей.<p></p>Пожалуйста, всё это возможно! Но для начала придётся ознакомиться с <span class='plink' id='item1'>Руководством пользователя</span>. Также целую кучу полезных советов и классных идей Вы можете получить из раздела <span class='plink' id='item2'>Авторам</span> нашего сайта.<p></p>Так что удачи! Придумайте свой уникальный игровой мир и станьте его властелином!<p></p><i>Примечание</i>. Обратите внимание, что для сохранения игры и публикации её в библиотеку, вам необходимо <span class='plink' id='item3'>зарегистрироваться</span> на сайте.<h3>Горячие клавиши</h3><b>F1</b> — вызвать меню помощи<br><b>F4</b> — редактор исходного кода<br><b>Esc</b> — закрыть редактор и сохранить параграф или закрыть меню<br><b>Курсор (вверх/вниз)</b> — перемещение по пунктам меню<br><b>Tab</b> — вывести список всех параграфов или перемещение по пунктам меню<br><b>Tab</b> — автодополнение ссылок, макросов и объектов или вызов меню форматирования выделенного текста (в редакторе)<br><b>Shift+Tab</b> — выбрать следующий параграф<br><b>Пробел (Space)</b> — редактировать выбранный параграф<br><b>Ввод (Enter)</b> — выбор пункта меню<br><b>Del (Backspace)</b> — удалить выбранный параграф<br><b>Ctrl+R (F5)</b> — запустить игру или тестировать открытый параграф<br><b>Ctrl+Shift+R (Ctrl+Shift+F5)</b> — запустить игру (в редакторе)<br><b>Ctrl+P</b> — добавить параграф<br><b>Ctrl+A</b> — показать меню &laquo;Вид&raquo;<br><b>Ctrl+O</b> — загрузить игру<br><b>Ctrl+S</b> — сохранить игру<br><b>Ctrl+N</b> — создать новую игру<br><b>Ctrl+B</b> — опубликовать игру<br><b>Ctrl+K</b> — комментировать выделенные строки (в редакторе)<br><b>Ctrl+F</b> — искать в параграфах (поиск и замена в редакторе)<br><b>Ctrl+G (F3)</b> — искать далее (в редакторе)<br><b>Ctrl+H</b> — заменить далее (в редакторе)<br><b>Ctrl+L</b> — перейти по ссылке (в редакторе)<br><b>Ctrl+M</b> — вызвать меню вставки макроса (в редакторе)<br><b>Ctrl+]</b> — сдвинуть текст вправо (в редакторе)<br><b>Ctrl+[</b> — сдвинуть текст влево (в редакторе)<br><b>Ctrl+D</b> — дублировать выбранный параграф или выделенные строки в редакторе<br><b>Ctrl+/-</b> — масштабировать параграфы или изменить размер шрифта<br><b>Ctrl+I</b> — вставить иллюстрацию (в редакторе)<br><b>Shift+Enter</b> — вставка макроса переноса строки (в редакторе)<p align='center'><i>Это окно всегда можно вызвать через меню &laquo;Помощь&raquo;</i></p>";
}
else
{
var Lang_Navigator = 'Navigator';
var Lang_ToolButtons = [ 'Add', 'Actions', 'View', 'Save', 'Open', 'New', 'Run', 'Export', Lang_Navigator, 'Help' ];
var Lang_EditorButtons = [ 'Close and Save', 'Add a Link', 'Add a Macro', 'Format Text', 'Comment Text', 'Set Color' ];
var Lang_SmallButtons = [ 'Back', 'Test', 'Find Next', 'Tab/Autocompletion', 'Connecting Lines' ];
var Lang_HTMLSave = 'Save';
var Lang_HTMLLoad = 'Restore';
var Lang_HTMLSaveSure = 'Do you want to save?';
var Lang_HTMLLoadSure = 'Do you want to restore?';
var Lang_Error = 'An error in the code';
var Lang_Audio = 'Audio';
var Lang_UNDO = 'Back';
var Lang_StartAgain = 'Start from the beginning?';
var Lang_HTMLRestart = 'Restart';
var Lang_HTMLStart = 'Start';
var Lang_Console = 'Console';
var Lang_HTMLMenu = 'Menu';
var Lang_HTMLPrefs = 'Preferences';
var Lang_HTMLTextSize = 'Change Text Size';
var Lang_HTMLAnimOn = 'Enable Animation';
var Lang_HTMLAnimOff = 'Disable Animation';
var Lang_HTMLSoundOn = 'Enable Sound';
var Lang_HTMLSoundOff = 'Disable Sound';
var Lang_MacrosDescTHEMEdefault = 'Default style';
var Lang_MacrosDescTHEMEdark = 'Dark style';
var Lang_MacrosDescTHEMEsepia = 'Sepia style';
var Lang_CreatedBy = 'created&nbsp;with';
var Lang_JSrequires = 'This file requires Javascript. Please enable it in your browser';
var Lang_Location = 'Passage';
var Lang_NotFound = 'not found';
var Lang_License = '<!DOCTYPE html>\n<!--\ncreated with AXMA Story Maker\nhttp://sm.axmasoft.com/\nYou agree not to modify or alter this File in any way. You may not remove or alter any copyright notices or other proprietary notices on any copies of this File.\n-->\n';
var Lang_Err_IncorrTitle = 'Invalid characters in the title';
var Lang_Err_ForbExp = 'Forbidden expression';
var Lang_Err_AbsOpenMacro = 'Absent opening brackets macros';
var Lang_Err_AbsClosMacro = 'Absent closing brackets macros';
var Lang_Err_IncorrectMacro = 'Incorrect macro';
var Lang_Err_ExcessMacro = 'Excess macro';
var Lang_Err_MissingMacro = 'Missing macro';
var Lang_Err_MissingCond = 'Missing condition in the macro';
var Lang_Err_OdnoRavno = 'Use "eq" or "==" instead of "="';
var Lang_Err_UnknownSysVar = 'Unknown system variable';
var Lang_Err_AbsOpenLink = 'Absent opening brackets links';
var Lang_Err_AbsClosLink = 'Absent closing brackets links';
var Lang_Err_IncorrectLink = 'Incorrect link';
var Lang_Err_MissingDolOrQuot = 'Missing $ or quotes';
var Lang_Err_ForbFunction = 'Invalid function';
var Lang_Err_TooMuchLinks = 'Too many links in this passage';
var Lang_Err_LinksNoEnd = 'Links should be in a row at the end of passage';
var Lang_Err_ForbPassage = 'Forbidden passage';
var Lang_NotFound2 = 'Not found';
var Lang_Found = 'Found';
var Lang_Replaced = 'Replaced';
var Lang_Passages = 'passages';
var Lang_PassagesUpper = 'Passages';
var Lang_PassagesErrors = 'errors';
var Lang_Chars = 'characters';
var Lang_NoPassages = 'No passages';
var Lang_NoPassage = 'No selected passage';
var Lang_BadPassageTitle = 'Bad title!';
var Lang_PleaseWait = 'Please Wait...';
var Lang_Create = 'create';
var Lang_PassagesGroup = 'group of passages';
var Lang_SplitAll = 'Ungroup All';
var Lang_Back = 'Back';
var Lang_Return = 'Return';
var Lang_Grouped = 'Grouped';
var Lang_Saved = 'Saved';
var Lang_Saving = 'Saving...';
var Lang_StorySaved = 'State Stored';
var Lang_StoryNotSaved = 'Failed save!';
var Lang_StoryNotLoaded = 'Failed restore!';
var Lang_Loading = 'Loading...';
var Lang_Loaded = 'Loaded';
var Lang_AreYouSure = 'Are you sure?';
var Lang_No = 'no';
var Lang_LinesOn = 'Lines On';
var Lang_LinesOff = 'Lines Off';
var Lang_NO = 'No';
var Lang_YES = 'Yes';
var Lang_FindInPassages = 'Search';
var Lang_MenuEdit = 'Edit';
var Lang_MenuDelete = 'Remove';
var Lang_MenuDuplicate = 'Duplicate';
var Lang_MenuLinked = 'Linked';
var Lang_MenuTest = 'Test';
var Lang_MenuFindNReplace = 'Find and Replace';
var Lang_MenuGotoLink = 'Go to Link';
var Lang_MenuUndo = 'Cancel Changes';
var Lang_MenuOutLinks = 'Outgoing';
var Lang_MenuInLinks = 'Incoming';
var Lang_MenuArrangeByTitle = 'Arrange By Title';
var Lang_MenuArrangeByColor = 'Arrange By Color';
var Lang_MenuArrangeBySize = 'Arrange By Size';
var Lang_MenuArrangeZoomOut = 'Zoom Out';
var Lang_MenuArrangeZoomIn = 'Zoom In';
var Lang_MenuSelectStory = 'Select Story';
var Lang_MenuMergeStory = 'Add Story';
var Lang_MenuFromBackUp = 'From Backup Copy';
var Lang_NoBackUp = 'No backup';
var Lang_MenuAddInventory = 'Add Inventory Module';
var Lang_MenuNewStandart = 'Example';
var Lang_MenuNewCompact = 'Standard Template';
var Lang_MenuNewClipboard = 'From Clipboard';
var Lang_MenuNewClipboardMess = 'Copy the source code to the clipboard and paste it into this field';
var Lang_MenuPublishFile = 'Save to HTML File';
var Lang_MenuPublishInet = 'Send to Library';
var Lang_MenuPublishInetSure = 'Are you sure that the story<br>ready for publication?';
var Lang_MenuQuickStart = 'Quick Start';
var Lang_MenuUserManual = 'Online User Manual';
var Lang_MenuProfile = 'My Account';
var Lang_MenuToLibrary = 'Library';
var Lang_MenuDesktopVersion = 'Download';
var Lang_AYSRemoveThisPassage = 'Do you really want to remove this passage?';
var Lang_AYSRemovePassage = 'Do you really want to remove';
var Lang_AYSSaveNewStory = 'Do you really want to create the new file?';
var Lang_AYSCreateNewStory = 'Do you really want to create the new story?';
var Lang_AYSUndoChanges = 'Do you really want to cancel all changes?';
var Lang_AYSExitSourceEditor = 'Do you really want to close the source code editor?';
var Lang_NotAvailEditPass = 'Not available now';
var Lang_NotAvailInBeta = 'Not available in beta version';
var Lang_NotAvailInEditor = 'Not available in the source code editor';
var Lang_ArrangeByTitle = 'Sort passages by title?';
var Lang_DontForgetSave = 'Do not forget to save your changes!';
var Lang_UnableRemoveGroup = 'Unable to remove a group of passages';
var Lang_NoSavedFiles = 'There are no saved stories';
var Lang_UpdateFound = 'Update is available!';
var Lang_ReloadApp = 'Reload app?';
var Lang_SourceCode = 'Source Code';
var Lang_FoundDuplicates = 'Found duplicates and merged';
var Lang_FoundDuplicates2 = 'Found duplicate';
var Lang_FindFind = 'Find';
var Lang_FindCount = 'Count';
var Lang_FindCase = 'Case sensitive';
var Lang_FindSelected = 'Selected text only';
var Lang_FindRegEx = 'Regular expressions';
var Lang_FindReplaceTo = 'Replace';
var Lang_FindReplace = 'Replace';
var Lang_FindReplaceAll = 'Replace All';
var Lang_Object = 'variable';
var Lang_MacroDisplay = 'Insert passage';
var Lang_MacroPicture = 'Main picture';
var Lang_MacroFilter = 'Set picture filter';
var Lang_MacroMenu = 'Main menu';
var Lang_MacroTitle = 'Title page';
var Lang_MacroClrScr = 'Clear page';
var Lang_MacroFade = 'Fade effect';
var Lang_MacroNoAudio = 'Stop music playback';
var Lang_MacroRow = 'Table row';
var Lang_MacroSet = 'Assign a value to a variable';
var Lang_MacroPrint = "Display a variable's value";
var Lang_MacroRandom = 'Random number';
var Lang_MacroInput = "Input a variable's value";
var Lang_MacroIf = 'Condition';
var Lang_MacroGoto = 'Goto statement';
var Lang_MacroLoop = 'Loop';
var Lang_MacroRepeat = 'Repeat passage';
var Lang_MacroStop = 'Stop repeating';
var Lang_MacroNop = 'Do not break a text';
var Lang_MacroClass = 'Define a class';
var Lang_MacroSound = 'Sound effect';
var Lang_MacroImage = 'Image';
var Lang_LinkSimple = 'Simple link';
var Lang_LinkAdd = 'Adds link';
var Lang_LinkMulti = 'Popup window';
var Lang_LinkNo = 'One way link';
var Lang_LinkObj = 'Set variables in the link';
var Lang_LinkBack = 'Back';
var Lang_LinkReturn = 'Return';
var Lang_LinkRestart = 'Restart';
var Lang_LinkFile = 'Insert media from Internet';
var Lang_LinkFileLocal = 'Insert image or music';
var Lang_FormatHTML = 'Text to HTML Entities';
var Lang_FormatLink = 'Link';
var Lang_FormatItalic = 'Italic';
var Lang_FormatBold = 'Bold';
var Lang_FormatUnderline = 'Underline';
var Lang_FormatHeader = 'Header';
var Lang_FormatMonospace = 'Monospace';
var Lang_FormatCenter = 'Center Text';
var Lang_FormatRight = 'Align Text Right';
var Lang_FormatBreak = 'Line<br>Break';
var Lang_FormatTab = 'Tab';
var Lang_FormatTire = 'Dash';
var Lang_FormatQuotes = '“Quotes”';
var Lang_FormatAsterisk = 'Asterisks';
var Lang_FormatHorline = 'Horizontal Line';
var Lang_FormatSub = 'Subscript <sub>Text</sub>';
var Lang_FormatSup = 'Superscript <sup>Text</sup>';
var Lang_FormatLi = '• List Item';
var Lang_Server = 'http://ifiction.net';
var Lang_SignIn = 'Sign In';
var Lang_Login = 'Login (email address)';
var Lang_Password = 'Password';
var Lang_Close = 'Close';
var Lang_GraphicMode = 'Visual Mode';
var Lang_SwitchTheme = 'Switch Theme';
var Lang_NeedOnline = 'Need Connection of Internet';
var Lang_SaveToCloud = 'Export to Cloud';
var Lang_LoadFromCloud = 'Import from Cloud';
var Lang_OnColorMode = 'Turn on Color Mode';
var Lang_OffColorMode = 'Turn off Color Mode';
var Lang_Register = 'Register';
var Lang_Error2 = 'Error';
var Lang_InText = 'In contents';
var Lang_RestoreStoryStyle = 'Defaults';
var Lang_ThemeEdit = 'Change Theme';
var Lang_CloseWindow = 'Close Window?';
var Lang_Show = 'Show';
var Lang_ChangeButton = 'Change Button';
var Lang_Body = 'Main Background';
var Lang_PrintBlock = 'Content Block';
var Lang_PrintAddBlock = 'Popup Block';
var Lang_PrintImage = 'Picture';
var Lang_PrintMenu = 'Story Menu';
var Lang_Toolbar = 'Toolbar';
var Lang_SpecialBlock = 'Service Block';
var Lang_PathToImage = 'path/to/image';
var Lang_StoryMenu = 'No actions available';
var Lang_GlobalSettings = 'Main Settings';
var Lang_ChangeTheme = 'Select Theme';
var Lang_Font = 'Font';
var Lang_MainColor = 'Main Background Color';
var Lang_ShadowsStatBlocks = 'Show shadows of static blocks';
var Lang_SetTexture = 'Set Background';
var Lang_RemoveTexture = 'Remove Background';
var Lang_ColorBack = 'Background Color';
var Lang_ColorText = 'Text Color';
var Lang_ColorLink = 'Link Color';
var Lang_ColorHeader = 'Header Color';
var Lang_ButtonRadius = 'Button Radius';
var Lang_FrameWidth = 'Frame Width';
var Lang_FrameSet = 'Set Frame';
var Lang_FrameRemove = 'Remove Frame';
var Lang_NoProperties = 'No properties';
var Lang_SelectImageSize = 'The image must be';
var Lang_NeedPaidAccount = 'To execute the operation needs a professional account';
var Lang_BuyPaidAccount = 'Buy a professional account';
var Lang_ChangeLang = 'Change Language';
var Lang_CreateStoryStyle = 'StoryStyle';
var Lang_StoryStyleAlready = 'StoryStyle already exists';
var Lang_PassageTitle = 'Passage Title';
var Lang_FileSound = 'Sound';
var Lang_FileImage = 'Sprite';
var Lang_FileImageDelete = 'Delete sprite';
var Lang_FileImageDeleteAll = 'Delete all sprites';
var Lang_ChoiceMacro = 'Action Choice';
var Lang_PurePassage = 'Passage';
var Lang_InputFileName = 'Enter the file name!';
var Lang_FileNameExists = 'A file with this name already exists!';
var Lang_FileAnother = 'Another';
var Lang_FileSelect = 'Select the file';
var Lang_FileName = 'File Name';
var Lang_FileTooLarge = 'File too large';
var Lang_FilesList = 'List of Files';
var Lang_FilesNo = 'No files';
var Lang_FilesAll = 'All Files';
var Lang_FilesImages = 'Sprites';
var Lang_FilesSounds = 'Sounds';
var Lang_FileRename = 'Rename';
var Lang_FileRemove = 'Remove';
var Lang_EditNo = 'Not available in the Visual mode';
var Lang_Overwrite = 'Overwrite';
var Lang_Empty = 'Empty';
var Lang_ShowSize = 'Show size';
var Lang_WantNewPub = 'Do you really want to create the new publication?';
var Lang_WantUpdPub = 'Do you really want to update the publication?';
var Lang_ErrorPub = 'An error has occurred publication';
var Lang_SuccessPub = 'Successfully sent to the library';
var Lang_MissReqPass = 'Missing or empty required passage';
var Lang_ErrorLoadFile = 'Error loading file';
var Lang_MultiView = 'Interactive fiction';
var Lang_ClassicView = 'CYOA book';
var Lang_InterABook = 'Interactive audiobook';
var Lang_InterQuest = 'Classic quest or RPG';
var Lang_InterVisual = 'Visual novel';
var Lang_NotForABook = 'Not available for audiobooks';
var Lang_GameType = 'Type of game';
var Lang_GameTheme = 'Theme';
var Lang_DefaultFileName = 'New game';
var Lang_AppWillBeRestarted = 'The application will be restarted. Continue?';
var Lang_NeedSignIn = 'To save please sign in';
var Lang_NeedFilesDir = 'You must specify a folder for the local media';
var Lang_MenuSetFilesDir = 'Set a folder for local media';
var Lang_Images = 'Images';
var Lang_AudioMP3 = 'MP3';
var Lang_RecentFiles = 'Recent Files';
var Lang_Send = 'Send';
var Lang_PubNew = 'New publication';
var Lang_AboutPub = 'You can create a new publication or update an existing one';
var Lang_ReadMedia = 'Reading a media file';
var Lang_ErrorReadMedia = 'Error reading file';
var Lang_ErrorReadCode = 'Error code';
var Lang_ErrorTotalSize = 'The total size of media files can not be larger than 20 MB';
var Lang_ErrorFileSize = 'The size of a media file can not be larger than 10 MB';
var Lang_Sending = 'Sending';
var Lang_Version = 'Version';
var Lang_QuickStart = "<h1>Hotkeys</h1><b>F1</b> — show Help menu<br><b>F4</b> — source code editor<br><b>Esc</b> — close editor and save passage / close window<br><b>Arrow keys</b> — move the selected passage<br><b>Arrow keys (up/down)</b> — navigating the menu<br><b>Tab</b> — show the Navigator / navigating the menu<br><b>Tab</b> — autocompletion / show format menu of the selected text (editor)<br><b>Shift+Tab</b> — select next passage<br><b>Space bar</b> — edit selected passage<br><b>Enter</b> — activate menu item<br><b>Del (Backspace)</b> — remove selected passage<br><b>Ctrl+R (F5)</b> — run story / test passage<br><b>Ctrl+Shift+R (Ctrl+Shift+F5)</b> — run story (editor)<br><b>Ctrl+P</b> — add a new passage<br><b>Ctrl+A</b> — show view menu<br><b>Ctrl+O</b> — open a story file<br><b>Ctrl+S</b> — save a story file<br><b>Ctrl+N</b> — create a new story<br><b>Ctrl+B</b> — publish the story<br><b>Ctrl+K</b> —  comment/uncomment selection (editor)<br><b>Ctrl+F</b> — find / find and replace (editor)<br><b>Ctrl+G (F3)</b> — find next (editor)<br><b>Ctrl+H</b> — replace next (editor)<br><b>Ctrl+L</b> — go to the link (editor)<br><b>Ctrl+M</b> — show insert macro menu (editor)<br><b>Ctrl+]</b> — shift right (editor)<br><b>Ctrl+[</b> — shift left (editor)<br><b>Ctrl+D</b> — duplicate selected passage or lines<br><b>Ctrl+/-</b> — change zoom or font size<br><b>Ctrl+I</b> — insert a main picture (editor)<br><b>Shift+Enter</b> — insert a new line (editor)";
}
function $(id) {
return document.getElementById(id);
}
function toTop()
{
top.document.body.scrollTop = 0;
}
function GetKeyCode(event)
{
return event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;
}
function getElementsByClass(classList,node)
{
var node = node || document,
list = node.getElementsByTagName('*'),
length = list.length,
classArray = classList.split(/\s+/),
classes = classArray.length,
result = [],i,j;
for (var i=0; i<length; i++) {
for(var j=0; j<classes; j++)  {
if(list[i].className.search('\\b' + classArray[j] + '\\b') != -1) {
result.push(list[i]);
break;
}
}
}
return result;
}
function ScrollToPos(obj,pos)
{
var text;
if (obj.value!==undefined)
{
text = obj.value;
obj.value = text.substr(0,pos);
obj.scrollTop = obj.scrollHeight;
obj.value = text;
}
else
{
var anchor = 'ASM_view33245';
text = GetEditorText();
SetEditorText(text.substr(0,pos)+"<span id='"+anchor+"'></span>"+text.substr(pos));
obj.innerHTML = obj.innerHTML.replace("&lt;span id='"+anchor+"'&gt;","<span id='"+anchor+"'>");
obj.innerHTML = obj.innerHTML.replace("&lt;/span&gt;","</span>");
$(anchor).scrollIntoView(false);
}
}
function RTrim(str)
{
if (!str) {return '';}
return str.replace(/\s+$/g,'');
}
function LTrim(str)
{
if (!str) {return '';}
return str.replace(/^\s+/g,'');
}
function getInputSelection(obj)
{
var start = 0, end = 0, normalizedValue, range, textInputRange, len, endRange;
if (typeof obj.selectionStart == 'number' && typeof obj.selectionEnd == 'number')
{
start = obj.selectionStart;
end = obj.selectionEnd;
}
else
{
range = document.selection.createRange();
if (range && range.parentElement() == obj)
{
len = obj.value.length;
normalizedValue = obj.value.replace(/\\r\\n/g, '\\n');
textInputRange = obj.createTextRange();
textInputRange.moveToBookmark(range.getBookmark());
endRange = obj.createTextRange();
endRange.collapse(false);
if (textInputRange.compareEndPoints('StartToEnd', endRange) > -1)
{
start = end = len;
}
else
{
start = -textInputRange.moveStart('character', -len);
start += normalizedValue.slice(0, start).split('\\n').length - 1;
if (textInputRange.compareEndPoints('EndToEnd', endRange) > -1)
{
end = len;
}
else
{
end = -textInputRange.moveEnd('character', -len);
end += normalizedValue.slice(0, end).split('\\n').length - 1;
}
}
}
}
return {start: start, end: end};
}
function getTextNodesIn(node)
{
var textNodes = [];
if (node.nodeType == 3)
{
textNodes.push(node);
}
else
{
var children = node.childNodes;
for (var i = 0, len = children.length; i < len; ++i)
{
textNodes.push.apply(textNodes, getTextNodesIn(children[i]));
}
}
return textNodes;
}
function SelectText(obj,SelStart,SelLength)
{
if (obj.value===undefined)
{
var SelEnd = SelStart + SelLength;
if (document.createRange && window.getSelection)
{
var range = document.createRange();
range.selectNodeContents(obj);
var textNodes = getTextNodesIn(obj);
var foundStart = false;
var foundEnd = false;
var charCount = 0, endCharCount;
for (var i = 0, textNode; textNode = textNodes[i++]; )
{
endCharCount = charCount + textNode.length;
if (!foundStart && SelStart >= charCount && (SelStart < endCharCount || (SelStart == endCharCount && i < textNodes.length)))
{
range.setStart(textNode, SelStart - charCount);
foundStart = true;
}
if (foundStart && SelEnd <= endCharCount)
{
range.setEnd(textNode, SelEnd - charCount);
foundEnd = true;
break;
}
charCount = endCharCount;
}
if (!foundEnd)
{
range.selectNodeContents(obj);
range.collapse(false);
}
var sel = window.getSelection();
sel.removeAllRanges();
sel.addRange(range);
}
else if (document.selection && document.body.createTextRange)
{
var textRange = document.body.createTextRange();
textRange.moveToElementText(obj);
textRange.collapse(true);
textRange.moveEnd("character", SelEnd);
textRange.moveStart("character", SelStart);
textRange.select();
}
}
else
{
if ('selectionStart' in obj)
{
obj.selectionStart = SelStart;
obj.selectionEnd = SelStart+SelLength;
obj.focus();
}
else
{
var inputRange = obj.createTextRange();
inputRange.moveStart ("character", SelStart);
inputRange.collapse();
inputRange.moveEnd ("character", SelStart+SelLength);
inputRange.select();
}
}
}
function GetSelection(obj)
{
if (obj.value===undefined)
{
var start, end;
obj.focus();
start = getCaretCharacterOffsetWithin(obj);
end = start + window.getSelection().toString().length;
return {start: start, end: end};
}
else
{
return getInputSelection(obj);
}
}
function getCaretCharacterOffsetWithin(obj)
{
var caretOffset = 0;
var doc = obj.ownerDocument || obj.document;
var win = doc.defaultView || doc.parentWindow;
var sel;
if (typeof win.getSelection != 'undefined')
{
sel = win.getSelection();
var range = sel.getRangeAt(0);
var preCaretRange = range.cloneRange();
preCaretRange.selectNodeContents(obj);
preCaretRange.setEnd(range.endContainer, range.endOffset);
caretOffset = preCaretRange.toString().length - sel.toString().length;
}
else if ((sel = doc.selection) && sel.type != 'Control')
{
sel = doc.selection;
var textRange = sel.createRange();
var preCaretTextRange = doc.body.createTextRange();
preCaretTextRange.moveToobjText(obj);
preCaretTextRange.setEndPoint('EndToEnd', textRange);
caretOffset = preCaretTextRange.text.length - sel.createRange().text.length;
}
return caretOffset;
}
function fixEvent(e)
{
e = e || window.event;
if ( e.pageX == null && e.clientX != null )
{
var html = document.documentElement;
var body = document.body;
e.pageX = e.clientX + (html && html.scrollLeft || body && body.scrollLeft || 0) - (html.clientLeft || 0);
e.pageY = e.clientY + (html && html.scrollTop || body && body.scrollTop || 0) - (html.clientTop || 0);
}
if (!e.which && e.button)
{
e.which = e.button & 1 ? 1 : ( e.button & 2 ? 3 : ( e.button & 4 ? 2 : 0 ) );
}
return e;
}
function array_multisort (arr)
{
var argl = arguments.length,
sal = 0,
flags = {
'SORT_REGULAR': 16,
'SORT_NUMERIC': 17,
'SORT_STRING': 18,
'SORT_ASC': 32,
'SORT_DESC': 40
},
sortArrs = [
[]
],
sortFlag = [0],
sortKeys = [
[]
],
g = 0,
i = 0,
j = 0,
k = '',
l = 0,
thingsToSort = [],
vkey = 0,
zlast = null,
args = arguments,
nLastSort = [],
lastSort = [],
lastSorts = [],
tmpArray = [],
elIndex = 0,
sortDuplicator = function (a, b) {
return nLastSort.shift();
},
sortFunctions = [
[function (a, b) {
lastSort.push(a > b ? 1 : (a < b ? -1 : 0));
return a > b ? 1 : (a < b ? -1 : 0);
}, function (a, b) {
lastSort.push(b > a ? 1 : (b < a ? -1 : 0));
return b > a ? 1 : (b < a ? -1 : 0);
}],
[function (a, b) {
lastSort.push(a - b);
return a - b;
}, function (a, b) {
lastSort.push(b - a);
return b - a;
}],
[function (a, b) {
lastSort.push((a + '') > (b + '') ? 1 : ((a + '') < (b + '') ? -1 : 0));
return (a + '') > (b + '') ? 1 : ((a + '') < (b + '') ? -1 : 0);
}, function (a, b) {
lastSort.push((b + '') > (a + '') ? 1 : ((b + '') < (a + '') ? -1 : 0));
return (b + '') > (a + '') ? 1 : ((b + '') < (a + '') ? -1 : 0);
}]
];
if (Object.prototype.toString.call(arr) === '[object Array]') {
sortArrs[0] = arr;
}
else if (arr && typeof arr === 'object') {
for (i in arr) {
if (arr.hasOwnProperty(i)) {
sortKeys[0].push(i);
sortArrs[0].push(arr[i]);
}
}
}
else {
return false;
}
var arrMainLength = sortArrs[0].length,
sortComponents = [0, arrMainLength];
for (j = 1; j < argl; j++) {
if (Object.prototype.toString.call(arguments[j]) === '[object Array]') {
sortArrs[j] = arguments[j];
sortFlag[j] = 0;
if (arguments[j].length !== arrMainLength) {
return false;
}
} else if (arguments[j] && typeof arguments[j] === 'object') {
sortKeys[j] = [];
sortArrs[j] = [];
sortFlag[j] = 0;
for (i in arguments[j]) {
if (arguments[j].hasOwnProperty(i)) {
sortKeys[j].push(i);
sortArrs[j].push(arguments[j][i]);
}
}
if (sortArrs[j].length !== arrMainLength) {
return false;
}
} else if (typeof arguments[j] === 'string') {
var lFlag = sortFlag.pop();
if (typeof flags[arguments[j]] === 'undefined' || ((((flags[arguments[j]]) >>> 4) & (lFlag >>> 4)) > 0)) {
return false;
}
sortFlag.push(lFlag + flags[arguments[j]]);
} else {
return false;
}
}
for (i = 0; i !== arrMainLength; i++) {
thingsToSort.push(true);
}
for (i in sortArrs) {
if (sortArrs.hasOwnProperty(i)) {
lastSorts = [];
tmpArray = [];
elIndex = 0;
nLastSort = [];
lastSort = [];
if (sortComponents.length === 0) {
if (Object.prototype.toString.call(arguments[i]) === '[object Array]') {
args[i] = sortArrs[i];
} else {
for (k in arguments[i]) {
if (arguments[i].hasOwnProperty(k)) {
delete arguments[i][k];
}
}
sal = sortArrs[i].length;
for (j = 0, vkey = 0; j < sal; j++) {
vkey = sortKeys[i][j];
args[i][vkey] = sortArrs[i][j];
}
}
delete sortArrs[i];
delete sortKeys[i];
continue;
}
var sFunction = sortFunctions[(sortFlag[i] & 3)][((sortFlag[i] & 8) > 0) ? 1 : 0];
for (l = 0; l !== sortComponents.length; l += 2) {
tmpArray = sortArrs[i].slice(sortComponents[l], sortComponents[l + 1] + 1);
tmpArray.sort(sFunction);
lastSorts[l] = [].concat(lastSort);
elIndex = sortComponents[l];
for (g in tmpArray) {
if (tmpArray.hasOwnProperty(g)) {
sortArrs[i][elIndex] = tmpArray[g];
elIndex++;
}
}
}
sFunction = sortDuplicator;
for (j in sortArrs) {
if (sortArrs.hasOwnProperty(j)) {
if (sortArrs[j] === sortArrs[i]) {
continue;
}
for (l = 0; l !== sortComponents.length; l += 2) {
tmpArray = sortArrs[j].slice(sortComponents[l], sortComponents[l + 1] + 1);
nLastSort = [].concat(lastSorts[l]);
tmpArray.sort(sFunction);
elIndex = sortComponents[l];
for (g in tmpArray) {
if (tmpArray.hasOwnProperty(g)) {
sortArrs[j][elIndex] = tmpArray[g];
elIndex++;
}
}
}
}
}
for (j in sortKeys) {
if (sortKeys.hasOwnProperty(j)) {
for (l = 0; l !== sortComponents.length; l += 2) {
tmpArray = sortKeys[j].slice(sortComponents[l], sortComponents[l + 1] + 1);
nLastSort = [].concat(lastSorts[l]);
tmpArray.sort(sFunction);
elIndex = sortComponents[l];
for (g in tmpArray) {
if (tmpArray.hasOwnProperty(g)) {
sortKeys[j][elIndex] = tmpArray[g];
elIndex++;
}
}
}
}
}
zlast = null;
sortComponents = [];
for (j in sortArrs[i]) {
if (sortArrs[i].hasOwnProperty(j)) {
if (!thingsToSort[j]) {
if ((sortComponents.length & 1)) {
sortComponents.push(j - 1);
}
zlast = null;
continue;
}
if (!(sortComponents.length & 1)) {
if (zlast !== null) {
if (sortArrs[i][j] === zlast) {
sortComponents.push(j - 1);
} else {
thingsToSort[j] = false;
}
}
zlast = sortArrs[i][j];
} else {
if (sortArrs[i][j] !== zlast) {
sortComponents.push(j - 1);
zlast = sortArrs[i][j];
}
}
}
}
if (sortComponents.length & 1) {
sortComponents.push(j);
}
if (Object.prototype.toString.call(arguments[i]) === '[object Array]') {
args[i] = sortArrs[i];
}
else {
for (j in arguments[i]) {
if (arguments[i].hasOwnProperty(j)) {
delete arguments[i][j];
}
}
sal = sortArrs[i].length;
for (j = 0, vkey = 0; j < sal; j++) {
vkey = sortKeys[i][j];
args[i][vkey] = sortArrs[i][j];
}
}
delete sortArrs[i];
delete sortKeys[i];
}
}
return true;
}
function setCookie(name,value)
{
var expires = new Date();
expires.setTime(expires.getTime()+31536000000);
document.cookie = name + '=' + escape(value) + '; expires=' + expires.toGMTString() + '; path=/';
}
function getCookie(name)
{
var cookie = ' ' + document.cookie;
var search = ' ' + name + '=';
var setStr = null;
var offset = 0;
var end = 0;
if (cookie.length>0){offset = cookie.indexOf(search);
if (offset != -1){offset += search.length;end = cookie.indexOf(';', offset);
if (end == -1){end = cookie.length;}
setStr = decodeURIComponent(cookie.substring(offset, end));}}
if (setStr==null){setStr = '';}
return(setStr);
}
function getXmlHttp()
{
var xmlhttp;
try {
xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
} catch (e) {
try {
xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
} catch (E) {
xmlhttp = false;
}
}
if (!xmlhttp && typeof XMLHttpRequest!='undefined') {
xmlhttp = new XMLHttpRequest();
}
return xmlhttp;
}
function loadData(url,callback)
{
var xmlhttp = getXmlHttp();
if (!callback)
{
xmlhttp.open('GET', url, false);
xmlhttp.send(null);
return xmlhttp.responseText;
}
else
{
xmlhttp.open('GET', url, true);
xmlhttp.onreadystatechange = function()
{
if (xmlhttp.readyState == 4)
{
if(xmlhttp.status == 200)
{
callback(xmlhttp.responseText);
}
}
};
xmlhttp.send(null);
}
}
function saveData(url,data,callback)
{
var xmlhttp = getXmlHttp();
if (!callback)
{
xmlhttp.open('POST', url, false);
xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
xmlhttp.send(data);
if(xmlhttp.status == 200)
{
return xmlhttp.responseText;
}
else
{
return '';
}
}
else
{
xmlhttp.open('POST', url, true);
xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
xmlhttp.onreadystatechange = function()
{
if (xmlhttp.readyState == 4)
{
if(xmlhttp.status == 200)
{
callback(xmlhttp.responseText);
}
}
};
xmlhttp.send(data);
}
}
function DrawOtr(x1,y1,x2,y2)
{
var x3, y3, x4, y4, xp, yp;
ctx.lineWidth = 1;
if (Math.abs(x2-x1)>LocSize*2 || Math.abs(y2-y1)<LocSize) {
x3 = Math.min(x1,x2)+(Math.max(x1,x2)-Math.min(x1,x2))/2;
y3 = y1;
x4 = x3;
y4 = y2;
yp = y1;
if (Math.abs(x2-x1)>(LocSize+16)) {
if (x2<x1) {
xp = x1-LocSize/2;
ctx.beginPath();
ctx.moveTo(xp-10, yp-3);
ctx.lineTo(xp-10, yp+5);
ctx.lineTo(xp+2, yp+1);
ctx.fill();
ctx.stroke();
ctx.closePath();
} else {
xp = x1+LocSize/2;
ctx.beginPath();
ctx.moveTo(xp+10, yp-3);
ctx.lineTo(xp+10, yp+5);
ctx.lineTo(xp-2, yp+1);
ctx.fill();
ctx.stroke();
ctx.closePath();
}
} else {
if (x2<x1) {
xp = x1-LocSize/2;
ctx.fillRect(xp-8, yp-3, 8, 8);
} else {
xp = x1+LocSize/2;
ctx.fillRect(xp, yp-3, 8, 8);
}
}
if (x2<x1) {
ctx.fillRect(x2+LocSize/2, y2-3, 2, 8);
} else {
ctx.fillRect(x2-LocSize/2-2, y2-3, 2, 8);
}
} else {
x3 = x1;
y3 = Math.min(y1,y2)+(Math.max(y1,y2)-Math.min(y1,y2))/2;
x4 = x2;
y4 = y3;
xp = x1;
if (Math.abs(y2-y1)>(LocSize+16)) {
if (y2<y1) {
yp = y1-LocSize/2;
ctx.beginPath();
ctx.moveTo(xp-3, yp-10);
ctx.lineTo(xp+5, yp-10);
ctx.lineTo(xp+1, yp+2);
ctx.fill();
ctx.stroke();
ctx.closePath();
} else {
yp = y1+LocSize/2;
ctx.beginPath();
ctx.moveTo(xp-3, yp+10);
ctx.lineTo(xp+5, yp+10);
ctx.lineTo(xp+1, yp-2);
ctx.fill();
ctx.stroke();
ctx.closePath();
}
} else {
if (y2<y1) {
yp = y1-LocSize/2;
ctx.fillRect(xp-3, yp-8, 8, 8);
} else {
yp = y1+LocSize/2;
ctx.fillRect(xp-3, yp, 8, 8);
}
}
if (y2<y1) {
ctx.fillRect(x2-3, y2+LocSize/2, 8, 2);
} else {
ctx.fillRect(x2-3, y2-LocSize/2-2, 8, 2);
}
}
ctx.lineWidth = 2;
ctx.beginPath();
ctx.moveTo(x1+1,y1+1);
ctx.lineTo(x3+1,y3+1);
ctx.lineTo(x4+1,y4+1);
ctx.lineTo(x2+1,y2+1);
ctx.stroke();
}
function getFocusedId()
{
var elem = document.activeElement;
if (elem)
{
return elem.id;
}
else
{
return undefined;
}
}
function getFocusedTag()
{
var elem = document.activeElement;
if (elem)
{
return elem.tagName.toLowerCase();
}
else
{
return '';
}
}
function ArrayUnique(myarray)
{
myarray.sort();
for (var i=myarray.length-1; i>=1; i--)
{
if (myarray[i].toLowerCase()==myarray[i-1].toLowerCase())
{
myarray.splice(i,1);
}
}
return myarray;
}
function ArraySortLen(myarray)
{
var lenVars = [];
for (var i=0; i<myarray.length; i++)
{
lenVars.push(myarray[i].length);
}
array_multisort(lenVars, myarray,'SORT_NUMERIC');
return myarray;
}
function Mid(str,start,len)
{
start = start-1;
if (start<0)
{
len = len + start;
start = 0;
}
if (len<0)
{
len = 0;
}
return str.substr(start,len);
}
function Left(str,len)
{
return str.substr(0,len);
}
function Right(str,len)
{
if (len<1)
{
return '';
}
return str.substr(-len);
}
function ReplaceLineEndings(str)
{
str =  str.replace(/\r\n/g,'\n');
str =  str.replace(/\n\r/g,'\n');
return str.replace(/\r/g,'\n');
}
function NthField(str,delim,n,caseSens)
{
var cs = 'i';
if (caseSens)
{
cs = '';
}
n--;
var arr = str.split(new RegExp(EscapeRegExp(delim),cs));
if (n>(arr.length-1))
{
return '';
}
else
{
return arr[n];
}
}
function CountFields(str,delim,caseSens)
{
var cs = 'i';
if (caseSens)
{
cs = '';
}
return str.split(new RegExp(EscapeRegExp(delim),cs)).length;
}
function EscapeRegExp(str)
{
return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g,"\\$&");
}
function Replace(str,find,replace)
{
return str.replace(new RegExp(EscapeRegExp(find),'i'),replace);
}
function ReplaceB(str,find,replace)
{
return str.replace(new RegExp(EscapeRegExp(find),''),replace);
}
function ReplaceAll(str,find,replace)
{
return str.replace(new RegExp(EscapeRegExp(find),'gi'),replace);
}
function ReplaceAllB(str,find,replace)
{
return str.replace(new RegExp(EscapeRegExp(find),'g'),replace);
}
function ReplaceInText(Input,Find,Replace,BegText,EndText)
{
var nl = '[[Temp554#ёNewline233e]]';
if (Find=='\n')
{
Find = nl;
}
Input = Input.replace(/\n/g,nl);
var inner;
var start, start2;
var myMatch;
var rg = new RegExp(BegText+"(.*?(?!"+BegText+").*?)("+EndText+")",'gi');
while ((myMatch = rg.exec(Input)) != null)
{
start = Input.indexOf(myMatch[1],myMatch.index);
start2 = Input.indexOf(myMatch[2],myMatch.index);
inner = myMatch[1];
inner = inner.replace(new RegExp(EscapeRegExp(Find),'gi'),Replace);
Input = Input.substr(0,start)+inner+Input.substr(start2);
rg.lastIndex = start;
}
Input = Input.replace(new RegExp(EscapeRegExp(nl),'g'),'\n');
return Input;
}
function Split(str,delimiter)
{
if (delimiter===undefined)
{
delimiter = ' ';
}
if (str.indexOf(delimiter)==-1)
{
return [str];
}
else
{
return str.split(delimiter);
}
}
function CompareRegEx(str,pattern)
{
var re = new RegExp("^"+pattern+"$",'im');
if (str.match(re)!=null)
{
return true;
}
else
{
return false;
}
}
function InStr(str,find)
{
return str.indexOf(find)+1;
}
function isLocalStorageAvailable()
{
if (!window.localStorage)
{
return false;
}
else
{
return true;
}
}
function urlencode(str)
{
str = (str+'').toString();
return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/\*/g, '%2A').replace(/%20/g, '+');
}
function random(min,max)
{
return Math.floor(Math.random() * (max - min + 1)) + min;
}
function SetEvent(id,func,event)
{
var elem = $(id);
if (elem) {
if (!event) {
event = 'click';
}
elem.addEventListener(event,func);
return true;
}
return false;
}
(function () {
if ( typeof window.CustomEvent === "function" ) return false;
function CustomEvent ( event, params ) {
params = params || { bubbles: false, cancelable: false, detail: undefined };
var evt = document.createEvent( 'CustomEvent' );
evt.initCustomEvent( event, params.bubbles, params.cancelable, params.detail );
return evt;
}
CustomEvent.prototype = window.Event.prototype;
window.CustomEvent = CustomEvent;
})();
!function(a){'use strict';function b(a,b){var c=(65535&a)+(65535&b),d=(a>>16)+(b>>16)+(c>>16);return d<<16|65535&c}function c(a,b){return a<<b|a>>>32-b}function d(a,d,e,f,g,h){return b(c(b(b(d,a),b(f,h)),g),e)}function e(a,b,c,e,f,g,h){return d(b&c|~b&e,a,b,f,g,h)}function f(a,b,c,e,f,g,h){return d(b&e|c&~e,a,b,f,g,h)}function g(a,b,c,e,f,g,h){return d(b^c^e,a,b,f,g,h)}function h(a,b,c,e,f,g,h){return d(c^(b|~e),a,b,f,g,h)}function i(a,c){a[c>>5]|=128<<c%32,a[(c+64>>>9<<4)+14]=c;var d,i,j,k,l,m=1732584193,n=-271733879,o=-1732584194,p=271733878;for(d=0;d<a.length;d+=16)i=m,j=n,k=o,l=p,m=e(m,n,o,p,a[d],7,-680876936),p=e(p,m,n,o,a[d+1],12,-389564586),o=e(o,p,m,n,a[d+2],17,606105819),n=e(n,o,p,m,a[d+3],22,-1044525330),m=e(m,n,o,p,a[d+4],7,-176418897),p=e(p,m,n,o,a[d+5],12,1200080426),o=e(o,p,m,n,a[d+6],17,-1473231341),n=e(n,o,p,m,a[d+7],22,-45705983),m=e(m,n,o,p,a[d+8],7,1770035416),p=e(p,m,n,o,a[d+9],12,-1958414417),o=e(o,p,m,n,a[d+10],17,-42063),n=e(n,o,p,m,a[d+11],22,-1990404162),m=e(m,n,o,p,a[d+12],7,1804603682),p=e(p,m,n,o,a[d+13],12,-40341101),o=e(o,p,m,n,a[d+14],17,-1502002290),n=e(n,o,p,m,a[d+15],22,1236535329),m=f(m,n,o,p,a[d+1],5,-165796510),p=f(p,m,n,o,a[d+6],9,-1069501632),o=f(o,p,m,n,a[d+11],14,643717713),n=f(n,o,p,m,a[d],20,-373897302),m=f(m,n,o,p,a[d+5],5,-701558691),p=f(p,m,n,o,a[d+10],9,38016083),o=f(o,p,m,n,a[d+15],14,-660478335),n=f(n,o,p,m,a[d+4],20,-405537848),m=f(m,n,o,p,a[d+9],5,568446438),p=f(p,m,n,o,a[d+14],9,-1019803690),o=f(o,p,m,n,a[d+3],14,-187363961),n=f(n,o,p,m,a[d+8],20,1163531501),m=f(m,n,o,p,a[d+13],5,-1444681467),p=f(p,m,n,o,a[d+2],9,-51403784),o=f(o,p,m,n,a[d+7],14,1735328473),n=f(n,o,p,m,a[d+12],20,-1926607734),m=g(m,n,o,p,a[d+5],4,-378558),p=g(p,m,n,o,a[d+8],11,-2022574463),o=g(o,p,m,n,a[d+11],16,1839030562),n=g(n,o,p,m,a[d+14],23,-35309556),m=g(m,n,o,p,a[d+1],4,-1530992060),p=g(p,m,n,o,a[d+4],11,1272893353),o=g(o,p,m,n,a[d+7],16,-155497632),n=g(n,o,p,m,a[d+10],23,-1094730640),m=g(m,n,o,p,a[d+13],4,681279174),p=g(p,m,n,o,a[d],11,-358537222),o=g(o,p,m,n,a[d+3],16,-722521979),n=g(n,o,p,m,a[d+6],23,76029189),m=g(m,n,o,p,a[d+9],4,-640364487),p=g(p,m,n,o,a[d+12],11,-421815835),o=g(o,p,m,n,a[d+15],16,530742520),n=g(n,o,p,m,a[d+2],23,-995338651),m=h(m,n,o,p,a[d],6,-198630844),p=h(p,m,n,o,a[d+7],10,1126891415),o=h(o,p,m,n,a[d+14],15,-1416354905),n=h(n,o,p,m,a[d+5],21,-57434055),m=h(m,n,o,p,a[d+12],6,1700485571),p=h(p,m,n,o,a[d+3],10,-1894986606),o=h(o,p,m,n,a[d+10],15,-1051523),n=h(n,o,p,m,a[d+1],21,-2054922799),m=h(m,n,o,p,a[d+8],6,1873313359),p=h(p,m,n,o,a[d+15],10,-30611744),o=h(o,p,m,n,a[d+6],15,-1560198380),n=h(n,o,p,m,a[d+13],21,1309151649),m=h(m,n,o,p,a[d+4],6,-145523070),p=h(p,m,n,o,a[d+11],10,-1120210379),o=h(o,p,m,n,a[d+2],15,718787259),n=h(n,o,p,m,a[d+9],21,-343485551),m=b(m,i),n=b(n,j),o=b(o,k),p=b(p,l);return[m,n,o,p]}function j(a){var b,c='';for(b=0;b<32*a.length;b+=8)c+=String.fromCharCode(a[b>>5]>>>b%32&255);return c}function k(a){var b,c=[];for(c[(a.length>>2)-1]=void 0,b=0;b<c.length;b+=1)c[b]=0;for(b=0;b<8*a.length;b+=8)c[b>>5]|=(255&a.charCodeAt(b/8))<<b%32;return c}function l(a){return j(i(k(a),8*a.length))}function m(a,b){var c,d,e=k(a),f=[],g=[];for(f[15]=g[15]=void 0,e.length>16&&(e=i(e,8*a.length)),c=0;16>c;c+=1)f[c]=909522486^e[c],g[c]=1549556828^e[c];return d=i(f.concat(k(b)),512+8*b.length),j(i(g.concat(d),640))}function n(a){var b,c,d='0123456789abcdef',e='';for(c=0;c<a.length;c+=1)b=a.charCodeAt(c),e+=d.charAt(b>>>4&15)+d.charAt(15&b);return e}function o(a){return unescape(encodeURIComponent(a))}function p(a){return l(o(a))}function q(a){return n(p(a))}function r(a,b){return m(o(a),o(b))}function s(a,b){return n(r(a,b))}function t(a,b,c){return b?c?r(b,a):s(b,a):c?p(a):q(a)}'function'==typeof define&&define.amd?define(function(){return t}):a.md5=t}(this);
function InsertMedia()
{
if (TargetChromeApp)
{
chrome_InsertMedia();
}
else if (TargetDesktop)
{
FromJavascript('InsertMedia');
ShowWind(false);
}
else
{
InsertLink('[[File: http://]]');
}
}
function InsertPicture()
{
if (TargetChromeApp)
{
chrome_InsertMedia(true);
}
else if (TargetDesktop)
{
FromJavascript('InsertPicture');
ShowWind(false);
}
else
{
InsertLink("<<picture '[[File: http://]]'>>");
}
}
function InsertMacro(text)
{
if (!text)
{
var str = '';
var macros = [
"display ''::"+Lang_MacroDisplay,
"noaudio::"+Lang_MacroNoAudio,
"set $"+Lang_Object+" = 1::"+Lang_MacroSet,
"print $"+Lang_Object+"::"+Lang_MacroPrint,
"random $"+Lang_Object+" = 9::"+Lang_MacroRandom,
"if $"+Lang_Object+" eq 1>>\n\n<<elseif $"+Lang_Object+" eq 2>>\n\n<<else>>\n\n<<endif::"+Lang_MacroIf,
"goto '"+Lang_Location+"'::"+Lang_MacroGoto,
"loop 10>>\n\n\t#<<break>>\n\t#<<continue>>\n<<endloop::"+Lang_MacroLoop,
"repeat '"+Lang_Location+"' 1::"+Lang_MacroRepeat,
"stop '"+Lang_Location+"'::"+Lang_MacroStop
];
if (TargetFormat=='html')
{
macros.push(
"clrscr::"+Lang_MacroClrScr,
"choice ''::"+Lang_ChoiceMacro,
"menu '[["+Lang_Location+"]] [[*"+Lang_Location+"]]'::"+Lang_MacroMenu,
"title ''::"+Lang_MacroTitle,
"fade = 1000::"+Lang_MacroFade,
"row '' ''::"+Lang_MacroRow,
"input '"+Lang_Location+"' $"+Lang_Object+"::"+Lang_MacroInput,
"image ''::"+Lang_MacroImage,
"sound ''::"+Lang_MacroSound,
"sprite '',@,@,@,@,100,1,0::"+Lang_FileImage,
"delete ''::"+Lang_FileImageDelete,
"delete::"+Lang_FileImageDeleteAll,
"filter ''::"+Lang_MacroFilter
);
}
for (var key in macros)
{
var m = macros[key].split('::');
str += "<div class='menu_item' forevent=\""+m[0]+"\"><b>"+m[0].split(' ')[0]+"</b><br>"+m[1]+"</div>";
}
str += "<div class='menu_item' id='item2'>"+"<b>picture</b><br>"+Lang_MacroPicture+"</div>";
str += "<div class='menu_item' id='item1'>"+"<b>nop</b><br>"+Lang_MacroNop+"</div>";
mywindow_inner.innerHTML = str;
SetEventsByAttr(8);
SetEvent('item1',function(){ShowWind(false);InsertText('<<nop>>\n\n<<endnop>>');});
SetEvent('item2',function(){ShowWind(false);InsertPicture();});
ShowWind(true);
}
else
{
ToUndoBuffer();
InsertText('<<'+text+'>>');
}
}
function InsertLink(text)
{
if (!text)
{
var str = '';
var links = [
"[[,|]]::"+Lang_LinkSimple,
"[[+,|]]::"+Lang_LinkAdd,
"[[*,|]]::"+Lang_LinkMulti,
"[[-,|]]::"+Lang_LinkNo,
"{$object=1}::"+Lang_LinkObj,
"<<back ','>>::"+Lang_LinkBack,
"<<return ','>>::"+Lang_LinkReturn,
"<<restart ','>>::"+Lang_LinkRestart
];
for (var key in links)
{
var l = links[key].split('::');
str += "<div class='menu_item' forevent=\""+l[0]+"\">"+l[1]+"</div>";
}
var insmedp = Lang_LinkFileLocal;
if (TargetBrowser)
{
insmedp = Lang_LinkFile;
}
str += "<div class='menu_item' id='item1'>"+insmedp+"</div>";
LocationArrDup = GetTitlesArray(true);
if (LocationArrDup.length>0)
{
str += "<div class='space'></div><b>"+Lang_PassagesUpper+"</b>";
for (var key in LocationArrDup)
{
str += "<div class='menu_item' forevent2=\""+LocationArrDup[key]+"\">"+LocationArrDup[key]+"</div>";
}
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(9);
SetEvent('item1',function(){ShowWind(false);InsertMedia();});
SetEventsByAttr(10,'forevent2');
ShowWind(true);
}
else
{
ToUndoBuffer();
var f = text.split(',');
if (f[1])
{
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
var strLeft = GetEditorText().substr(0,currentPos);
var strSele = GetEditorText().substr(currentPos,currentLen);
var strRight = GetEditorText().substr(currentPos+currentLen,GetEditorText().length);
SetEditorText(strLeft+f[0]+strSele+f[1]+strRight);
var offset = 1;
if (f[0].substr(0,2)=='<<')
{
offset = 0;
}
Select(editor,currentPos+strSele.length+f[0].length+offset,0);
}
else
{
InsertText(text);
}
}
}
function HTMLChars()
{
var selData = GetSelection(editor);
var currentPos = selData['start']+1;
var currentLen = selData['end']-selData['start'];
if (currentLen!=0)
{
ToUndoBuffer();
var pos, slen;
var arrChars = Split(Mid(GetEditorText(),currentPos,currentLen),'');
var ent = '';
for (var i=0; i<arrChars.length; i++)
{
if (arrChars[i]!='\n' && arrChars[i]!='\r')
{
if (arrChars[i]==" ")
{
arrChars[i] = "&nbsp;";
}
else if (arrChars[i]=="<")
{
arrChars[i] = "&lt;";
}
else if (arrChars[i]==">")
{
arrChars[i] = "&gt;";
}
else
{
arrChars[i] = "&#"+arrChars[i].charCodeAt(0)+";";
}
}
}
ent = arrChars.join('');
pos = currentPos-1;
slen = ent.length;
SetEditorText(Left(GetEditorText(),pos) + ent + Right(GetEditorText(),GetEditorText().length-pos-currentLen));
Select(editor,pos,slen);
}
else
{
editor.focus();
}
}
function FormatText(text)
{
if (!text)
{
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
if (currentLen>0)
{
var selText = GetEditorText().substr(currentPos,currentLen);
selText = selText.split(/[\r\n]/)[0];
Select(editor,currentPos,selText.length);
editor.blur();
}
var str = '';
var quotes = '«,»';
if (LangCode=='en')
{
quotes = '“,”';
}
var format = [
"[[,]]::[["+Lang_FormatLink+"]]",
"//,//::<i>"+Lang_FormatItalic+"</i>",
"'',''::<b>"+Lang_FormatBold+"</b>",
"__,__::<u>"+Lang_FormatUnderline+"</u>",
"%%%,%%%::<div style=\"font-size:1.3em;\">"+Lang_FormatHeader+"</div>",
"{{{,}}}::<span class='mono'>"+Lang_FormatMonospace+"</span>",
"===,===::"+Lang_FormatCenter,
"@@@,@@@::<div style='text-align:right'>"+Lang_FormatRight+"</div>",
"--::"+Lang_FormatTire+" —",
quotes+"::"+Lang_FormatQuotes,
"[[asmNewline]]***[[asmNewline]]::"+Lang_FormatAsterisk+" *&nbsp;*&nbsp;*&nbsp;",
"[[asmNewline]]----[[asmNewline]]::"+Lang_FormatHorline,
"~~,~~::"+Lang_FormatSub,
"^^,^^::"+Lang_FormatSup,
"[[asmNewline]]* ::"+Lang_FormatLi,
"<<br>>::"+Lang_FormatBreak,
"<<tab 4>>::"+Lang_FormatTab
];
if (TargetFormat=='html')
{
format.push(
"<<class ''>>,<<endclass>>::"+Lang_MacroClass
);
}
for (var key in format)
{
var f = format[key].split('::');
str += "<div class='menu_item' forevent=\""+f[0]+"\">"+f[1]+'</div>';
}
str += "<div class='menu_item' id='item1'>"+Lang_FormatHTML+"</div>";
mywindow_inner.innerHTML = str;
SetEventsByAttr(11);
SetEvent('item1',function(){HTMLChars();ShowWind(false);});
ShowWind(true);
}
else
{
ToUndoBuffer();
var f = text.split(',');
if (f[1])
{
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
var strLeft = GetEditorText().substr(0,currentPos);
var strSele = GetEditorText().substr(currentPos,currentLen);
var strRight = GetEditorText().substr(currentPos+currentLen,GetEditorText().length);
SetEditorText(strLeft+f[0]+strSele+f[1]+strRight);
Select(editor,currentPos+f[0].length,strSele.length);
}
else
{
text = text.replace(/\[\[asmNewline\]\]/g,'\n');
InsertText(text);
}
}
}
function CommentText()
{
var myText = GetEditorText();
myText = ReplaceLineEndings(myText);
var myTextLen = myText.length;
var selData = GetSelection(editor);
var pos = selData['start'];
var sbegin = pos;
var slen_bak = selData['end']-selData['start'];
var slen = slen_bak;
for (var i=pos; i>=0; i--)
{
if (i==0 || myText.substr(i-1,1)=='\n')
{
sbegin = i;
slen = pos-sbegin+slen;
break;
}
}
if (slen<1)
{
slen = 1;
}
var AddComment;
if (Left(LTrim(myText.substr(sbegin,myTextLen)),1)!='#')
{
AddComment = true;
}
else
{
AddComment = false;
}
var n = 0;
var added = false;
var removed = false;
var s = '';
var linesArr = Split(myText.substr(sbegin,slen),'\n');
for (var i=0; i<linesArr.length; i++)
{
s = linesArr[i];
if (AddComment)
{
if (s!='' && LTrim(s).substr(0,1)!='#')
{
if (!EditorMode || (EditorMode && LTrim(s).substr(0,2)!='::' && LTrim(myText).substr(sbegin,2)!='::'))
{
linesArr[i] = '#' + s;
added = true;
n++;
}
}
}
else
{
if (s!='' && s.substr(0,1)=='#')
{
linesArr[i] = Right(s,s.length-1);
removed = true;
n++;
if (sbegin==pos)
{
pos++;
}
}
}
}
s = linesArr.join('\n');
ToUndoBuffer();
SetEditorText(Left(myText,sbegin) + s + Right(myText,myText.length-sbegin-slen));
if (added)
{
Select(editor,pos+1,slen_bak+n-1);
}
else if (removed)
{
Select(editor,pos-1,slen_bak-n+1);
}
else
{
Select(editor,pos,slen_bak);
}
}
function ClearUndoBuffer()
{
UndoBuffer = [];
}
function ToUndoBuffer()
{
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
var elem = [];
elem.push(GetEditorText());
elem.push(currentPos);
elem.push(currentLen);
UndoBuffer.push(elem);
}
function FromUndoBuffer()
{
var elem = UndoBuffer.pop();
SetEditorText(elem[0]);
Select(editor,elem[1],elem[2]);
}
function AutoDopWind(arr,offset)
{
if (arr.length>0)
{
var str = '';
for (var key in arr)
{
var ins = arr[key];
str += "<div class='menu_item' forevent=\""+Mid(ins,offset)+"\">"+ins+"</div>";
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(12);
ShowWind(true);
}
}
function AutoDop(event)
{
var selData = GetSelection(editor);
var pos = selData['start'];
var text = GetEditorText();
ToUndoBuffer();
if (pos==0 || text.substr(pos-1,1)=='\n' || text.substr(pos-1,1)=='\r' || text.substr(pos,1)=='\t' || text.substr(pos-1,1)=='\t')
{
if (!event || !event.shiftKey)
{
InsertText('\t',true);
return;
}
}
var ins1, ins2, prev = '';
var found = false;
var n;
var countLeft, countRight;
if (CompareRegEx(Mid(GetEditorText(),pos-1,2),"\\$\\$"))
{
AutoDopWind(SysVars,prev.length+1);
}
else if (CompareRegEx(Mid(GetEditorText(),pos-200,200+1),".*?\\$\\$[^"+BadVarsSymbs+"]+?"))
{
n = 20;
var LocVars = [];
for (var i=0; i<SysVars.length; i++)
{
LocVars.push(Mid('$$'+SysVars[i],3));
}
prev = Mid(GetEditorText(),pos-n,n+1);
countLeft = CountFields(prev,"$")-1;
prev = LTrim(NthField(prev,"$",countLeft+1));
if (prev!='')
{
for (var i=0; i<LocVars.length; i++)
{
if (prev.toLowerCase()==Left(LocVars[i],prev.length).toLowerCase() && prev.toLowerCase()!=LocVars[i].toLowerCase())
{
found = true;
InsertText(Mid(LocVars[i],prev.length+1),true);
break;
}
}
}
}
else if (CompareRegEx(Mid(GetEditorText(),pos-1,2),"[^\\$]\\$"))
{
AutoDopWind(GetVarsArray(),prev.length+1);
}
else if (CompareRegEx(Mid(GetEditorText(),pos-200,200+1),".*?\\$[^"+BadVarsSymbs+"]+?"))
{
n = 200;
prev = Mid(GetEditorText(),pos-n,n+1);
countLeft = CountFields(prev,"$")-1;
prev = LTrim(NthField(prev,"$",countLeft+1));
if (prev!='')
{
var LocVars = GetVarsArray();
var foundArr = [];
for (var i=0; i<LocVars.length; i++)
{
if (prev.toLowerCase()==Left(LocVars[i],prev.length).toLowerCase() && prev.toLowerCase()!=LocVars[i].toLowerCase())
{
found = true;
foundArr.push(LocVars[i]);
}
}
if (foundArr.length==1)
{
InsertText(Mid(foundArr[0],prev.length+1),true);
}
else if (foundArr.length>1)
{
AutoDopWind(foundArr,prev.length+1);
}
}
}
else if (Left(LTrim(Mid(GetEditorText(),pos+1,200)),2)=="]]" || Left(ReplaceAll(Mid(GetEditorText(),pos+1,200)," ",""),2)=="{$")
{
n = 500;
prev = Mid(GetEditorText(),pos-n,n+1);
prev = ReplaceAll(prev,"[[+","[[");
prev = ReplaceAll(prev,"[[-","[[");
prev = ReplaceAll(prev,"[[*","[[");
prev = ReplaceAll(prev,"[[","|");
countLeft = CountFields(prev,"|")-1;
prev = LTrim(NthField(prev,"|",countLeft+1));
var LocNames = GetTitlesArray(true);
if (prev=='')
{
AutoDopWind(LocNames,prev.length+1);
}
else
{
var foundArr = [];
for (var i=0; i<LocNames.length; i++)
{
if (prev.toLowerCase()==Left(LocNames[i],prev.length).toLowerCase())
{
found = true;
foundArr.push(LocNames[i]);
}
}
if (foundArr.length==1)
{
InsertText(Mid(foundArr[0],prev.length+1),true);
}
else if (foundArr.length>1)
{
AutoDopWind(foundArr,prev.length+1);
}
}
}
else if (CompareRegEx(Mid(GetEditorText(),pos-200,200+1),".*?<<\\s*?(display|repeat|stop|input|goto)\\s*?['\"]\\s*?[^'\"]*?"))
{
n = 500;
prev = Mid(GetEditorText(),pos-n,n+1);
prev = ReplaceAll(prev,'"',"'");
countLeft = CountFields(prev,"'")-1;
prev = LTrim(NthField(prev,"'",countLeft+1));
var LocNames = GetTitlesArray(true);
if (prev=='')
{
AutoDopWind(LocNames,prev.length+1);
}
else
{
var foundArr = [];
for (var i=0; i<LocNames.length; i++)
{
if (prev.toLowerCase()==Left(LocNames[i],prev.length).toLowerCase())
{
found = true;
foundArr.push(LocNames[i]);
}
}
if (foundArr.length==1)
{
InsertText(Mid(foundArr[0],prev.length+1),true);
}
else if (foundArr.length>1)
{
AutoDopWind(foundArr,prev.length+1);
}
}
}
else if (Mid(GetEditorText(),pos-1,2)=="[[" && Mid(GetEditorText(),pos+1,1)!="|" && Mid(GetEditorText(),pos+1,2)!="]]")
{
ins1 = "|";
if (Mid(GetEditorText(),pos+1,2)!="]]")
{
ins2 = "]]";
}
else
{
ins2 = "";
}
found = true;
FormatText(ins1+','+ins2);
}
else if (Mid(GetEditorText(),pos-2,3)=="[[|" && Mid(GetEditorText(),pos+1,1)!="]")
{
found = true;
InsertText("]]",true);
Select(editor,pos,0);
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?Math\\.[a-zA-Z]+?") || Mid(GetEditorText(),pos-4,5)=='Math.')
{
var methods = [];
methods.push("abs(x)");
methods.push("max(,)");
methods.push("min(,)");
methods.push("pow(,)");
methods.push("sqrt(x)");
methods.push("log(x)");
methods.push("round(x)");
methods.push("floor(x)");
methods.push("ceil(x)");
methods.push("random()");
methods.push("sin(x)");
methods.push("cos(x)");
methods.push("tan(x)");
methods.push("acos(x)");
methods.push("asin(x)");
methods.push("atan(x)");
if (Mid(GetEditorText(),pos-4,5)=='Math.')
{
AutoDopWind(methods,prev.length+1);
}
else
{
n = 20;
prev = Mid(GetEditorText(),pos-n,n+1);
countLeft = CountFields(prev,"Math.",true)-1;
prev = LTrim(NthField(prev,"Math.",countLeft+1,true));
if (prev!='')
{
for (var i=0; i<methods.length; i++)
{
if (prev==Left(methods[i],prev.length) && prev!=methods[i])
{
found = true;
InsertText(Mid(methods[i],prev.length+1),true);
break;
}
}
}
}
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?\\.[a-zA-Z]+?") || Mid(GetEditorText(),pos,1)=='.')
{
var methods = [];
methods.push("length");
methods.push("slice(,)");
methods.push("sort()");
methods.push("reverse()");
methods.push("push('')");
methods.push("unshift('')");
methods.push("shift()");
methods.push("pop()");
methods.push("splice(,)");
methods.push("join('')");
methods.push("split('')");
methods.push("concat($)");
methods.push("toLowerCase()");
methods.push("toUpperCase()");
methods.push("toString()");
methods.push("charAt(0)");
methods.push("charCodeAt(0)");
methods.push("substr(,)");
methods.push("indexOf('')");
methods.push("replace('','')");
methods.push("bitTest(n)");
methods.push("bitSet(n)");
methods.push("bitClear(n)");
methods.push("bitToggle(n)");
if (Mid(GetEditorText(),pos,1)=='.')
{
AutoDopWind(methods,prev.length+1);
}
else
{
n = 20;
prev = Mid(GetEditorText(),pos-n,n+1);
countLeft = CountFields(prev,".")-1;
prev = LTrim(NthField(prev,".",countLeft+1));
if (prev!='')
{
for (var i=0; i<methods.length; i++)
{
if (prev==Left(methods[i],prev.length) && prev!=methods[i])
{
found = true;
InsertText(Mid(methods[i],prev.length+1),true);
break;
}
}
}
}
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?\\=\\s*?[tf][a-z]*?"))
{
n = 20;
var methods = [];
methods.push("true");
methods.push("false");
prev = Mid(GetEditorText(),pos-n,n+1);
countLeft = CountFields(prev,"=")-1;
prev = LTrim(NthField(prev,"=",countLeft+1));
if (prev!='')
{
for (var i=0; i<methods.length; i++)
{
if (prev==Left(methods[i],prev.length) && prev!=methods[i])
{
found = true;
InsertText(Mid(methods[i],prev.length+1),true);
break;
}
}
}
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?<<\\s*?sound\\s+?['\"]\\s*?"))
{
FileDataWind('audio',true);
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?<<\\s*?image\\s+?['\"]\\s*?"))
{
FileDataWind('image',true);
}
else if (CompareRegEx(Mid(GetEditorText(),pos-20,20+1),".*?<<\\s*?(sprite|delete)\\s+?['\"]\\s*?"))
{
FileDataWind('image',true);
}
else
{
n = 30;
var macros = [];
macros.push("br");
macros.push("back ''");
macros.push("break");
macros.push("choice ''");
macros.push("clrscr");
macros.push("class ''");
macros.push("continue");
macros.push("display ''");
macros.push("delete ''");
macros.push("else");
macros.push("elseif $");
macros.push("endif");
macros.push("endloop");
macros.push("endnop");
macros.push("endclass");
macros.push("fade = ");
macros.push("filter ''");
macros.push("goto ''");
macros.push("if $");
macros.push("image ''");
macros.push("input '' $");
macros.push("loop 1");
macros.push("menu '[[]]'");
macros.push("nop");
macros.push("noaudio");
macros.push("picture '[[File: ]]'");
macros.push("print ");
macros.push("random $");
macros.push("return ''");
macros.push("repeat '' 1");
macros.push("restart ''");
macros.push("row ");
macros.push("set $");
macros.push("sound ''");
macros.push("sprite '',@,@,@,@,100,1,0");
macros.push("stop ''");
macros.push("tab 4");
macros.push("title ''");
prev = Mid(GetEditorText(),pos-n,n+1);
countRight = CountFields(prev,">>")-1;
countLeft = CountFields(prev,"<<")-1;
prev = LTrim(NthField(prev,"<<",countLeft+1));
if (countLeft>0)
{
if (prev!='')
{
for (var i=0; i<macros.length; i++)
{
if (prev.toLowerCase()==Left(macros[i],prev.length).toLowerCase() && Right(macros[i],2)!=">>")
{
ins1 = Mid(macros[i],prev.length+1);
if (Mid(GetEditorText(),pos+1,2)==">>")
{
ins2 = "";
}
else
{
ins2 = ">>";
}
found = true;
break;
}
}
}
else
{
AutoDopWind(macros,prev.length+1);
if (Left(LTrim(Mid(GetEditorText(),pos+1,100)),2)!=">>")
{
InsertText(">>",true);
Select(editor,pos,0);
}
}
}
if (found)
{
InsertText(ins1,true);
if (ins2!='')
{
InsertText(ins2,true);
Select(editor,pos+ins1.length,0);
}
}
if (!found)
{
if ( (countLeft<1 || countRight>0) && Mid(GetEditorText(),pos,1)!="$" && Mid(GetEditorText(),pos,1)!="<" && Mid(GetEditorText(),pos,1)!="[" && Left(LTrim(Mid(GetEditorText(),pos+1,100)),2)!=">>" && Right(RTrim(Mid(GetEditorText(),pos-100,100+1)),2)!="[[" && Left(LTrim(Mid(GetEditorText(),pos+1,100)),1)!="|" && "Math."!=Mid(GetEditorText(),pos-4,5) )
{
FormatText('<<,>>');
}
}
}
editor.focus();
}
function GetVarsArray()
{
var Vars = [];
var s, macros, Var = '';
var myRe, myMatch, myRe2, myMatch2;
s = GetEditorText();
s = s.replace(/\[\[.*?(?!\]\]).*?\]?\]\]/g,"<<link $&>>");
myRe = /<<(.*?(?!<<)(?!>>).*?)>>/g;
myRe2 = new RegExp("(\\${1,2}[^"+BadVarsSymbs+"]+?)["+BadVarsSymbs+"]",'g');
while ((myMatch = myRe.exec(s)) != null)
{
macros = ' '+myMatch[1]+' ';
while ((myMatch2 = myRe2.exec(macros)) != null)
{
Var = myMatch2[1].trim();
Var = Var.replace(/^\$/,'');
if (Left(Var,1)!='$')
{
Vars.push(Var);
}
}
}
if (!EditorMode)
{
for (var i=0; i<LocationArr.length; i++)
{
for (var j=0; j<LocationArr[i].Vars.length; j++)
{
Vars.push(LocationArr[i].Vars[j]);
}
}
}
return ArrayUnique(Vars);
}
function GetTitlesArray(lowercase,nounique)
{
var LocNames = [];
var title;
if (EditorMode)
{
var s = GetEditorText();
var myMatch;
EditrTitlesRegExp.lastIndex = 0;
while ((myMatch = EditrTitlesRegExp.exec(s)) != null)
{
title = myMatch[1].split('[::]')[0].trim();
if (title!='')
{
if (lowercase)
{
title = title.toLowerCase();
}
LocNames.push(title);
}
}
}
else
{
for (var i=0; i<LocationArr.length; i++)
{
title = LocationArr[i].Title;
if (!ForbiddenLocation(title))
{
if (lowercase)
{
title = title.toLowerCase();
}
LocNames.push(title);
}
}
}
if (!nounique)
{
LocNames = ArrayUnique(LocNames);
}
else
{
LocNames.sort();
}
return LocNames;
}
function FindNReplace()
{
var str = '';
str += "<div>";
str += "<input id='findField' type='text' style='margin-bottom:10px;' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' placeholder='"+Lang_FindFind+"'>";
str += "<div style='margin-top:4px;'>";
str += "<span class='button' id='button1' style='float:left;'>"+Lang_FindFind+"</span>";
str += "<span class='button' id='button2' style='float:right;'>"+Lang_FindCount+"</span>";
str += "</div>";
str += "<div style='clear:both;'></div>";
str += "</div><div style='clear:both;'></div>";
str += "<div style='font-size:11px;margin-top:8px;text-align:left;'>";
str += "<label><input id='caseField' type='checkbox'>"+Lang_FindCase+"</label>";
str += "<br><label><input id='regexField' type='checkbox'>"+Lang_FindRegEx+"</label>";
str += "</div>";
str += "<div style='margin-top:8px;'>";
str += "<input id='replaceField' type='text' style='margin-bottom:8px;' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' onblur='RememberFindFields();' placeholder='"+Lang_FindReplaceTo+"'>";
str += "<div style='margin-top:4px;'>";
str += "<span class='button' id='button3' style='float:left;'>"+Lang_FindReplace+"</span>";
str += "<span class='button' id='button4' style='float:right;'>"+Lang_FindReplaceAll+"</span>";
str += "</div>";
str += "<div style='clear:both;'></div>";
str += "</div>";
mywindow_inner.innerHTML = str;
SetEvent('findField',function(){RememberFindFields();},'blur');
SetEvent('button1',function(){EditorFindNext();});
SetEvent('button2',function(){EditorFindCount();});
SetEvent('button3',function(){EditorReplaceNext();});
SetEvent('button4',function(){EditorReplaceAll();});
SetEvent('caseField',function(){EditrCaseField=!EditrCaseField},'change');
SetEvent('regexField',function(){EditrRegExField=!EditrRegExField},'change');
ShowWind(true);
var findField = $('findField');
findField.value = EditrFindField;
Select(findField,0,EditrFindField.length);
$('replaceField').value = EditrReplaceField;
if (EditrCaseField)
{
$('caseField').checked = true;
}
if (EditrSelectedField)
{
$('selectField').checked = true;
}
if (EditrRegExField)
{
$('regexField').checked = true;
}
}
function RememberFindFields()
{
var findField = $('findField');
if (findField)
{
EditrFindField = findField.value;
}
var replaceField = $('replaceField');
if (replaceField)
{
EditrReplaceField = replaceField.value;
}
}
function EditorFind(from)
{
var ret = new Array(2);
var text = GetEditorText();
var myRe, myMatch;
if (EditrRegExField)
{
try
{
var mod;
if (!EditrCaseField)
{
mod = 'i';
}
else
{
mod = '';
}
myRe = new RegExp(EditrFindField,'g'+mod);
myRe.lastIndex = from;
myMatch = myRe.exec(text);
if (myMatch!=null)
{
ret[1] = myMatch[0].length;
ret[0] = myRe.lastIndex-ret[1]+1;
}
else
{
ret[1] = 0;
ret[0] = 0;
}
}
catch(e)
{
ret[0] = 0;
ret[1] = 0;
return ret;
}
}
else
{
ret[1] = EditrFindField.length;
if (!EditrCaseField)
{
ret[0] = text.toLowerCase().indexOf(EditrFindField.toLowerCase(),from)+1;
}
else
{
ret[0] = text.indexOf(EditrFindField,from)+1;
}
}
return ret;
}
function EditorFindNext(from,replace)
{
RememberFindFields();
var findField = $('findField');
var found = false;
if (EditrFindField!='')
{
if (from===undefined)
{
var selData = GetSelection(editor);
from = selData['start'] + (selData['end']-selData['start']);
found = false;
}
var fnd = EditorFind(from);
var i = fnd[0];
if (i!=0)
{
ShowWind(false);
found = true;
ScrollToPos(editor,i-1);
if (!replace)
{
Select(editor,i-1,fnd[1]);
}
else
{
ToUndoBuffer();
var MyText = GetEditorText();
MyText = MyText.substr(0,i-1) + EditrReplaceField + MyText.substr(i-1+fnd[1]);
SetEditorText(MyText);
Select(editor,i-1,EditrReplaceField.length);
}
}
else
{
if (from==0 && !found)
{
Message(Lang_NotFound2);
if (findField)
{
Select(findField,0,findField.value.length)
}
else
{
FindNReplace();
}
}
else
{
EditorFindNext(0,replace);
}
}
}
else
{
if (findField)
{
findField.focus();
}
else
{
FindNReplace();
}
}
}
function EditorFindCount(returnCount)
{
RememberFindFields();
var findField = $('findField');
findField.focus();
var MyText;
var n = 0;
if (EditrSelectedField)
{
var selData = GetSelection(editor);
MyText = GetEditorText().substr(selData['start'],selData['end']-selData['start']);
}
else
{
MyText = GetEditorText();
}
if (EditrFindField!='')
{
if (EditrRegExField)
{
try
{
if (!EditrCaseField)
{
n = MyText.split(new RegExp((EditrFindField),'i')).length-1;
}
else
{
n = MyText.split(new RegExp((EditrFindField),'')).length-1;
}
}
catch(e)
{
Message(Lang_NotFound2);
}
}
else
{
if (!EditrCaseField)
{
n = MyText.toLowerCase().split(EditrFindField.toLowerCase()).length-1;
}
else
{
n = MyText.split(EditrFindField).length-1;
}
}
}
else
{
n = MyText.length;
}
if (!returnCount)
{
if (n>0)
{
Message(Lang_Found+": "+n)
}
else
{
Message(Lang_NotFound2);
}
}
else
{
return n;
}
}
function EditorReplaceAll()
{
RememberFindFields();
var findField = $('findField');
if (EditrFindField!='')
{
var n = EditorFindCount(true);
if (n>0)
{
ToUndoBuffer();
var MyText = GetEditorText();
if (EditrRegExField)
{
try
{
if (!EditrCaseField)
{
var mod = 'gi';
}
else
{
var mod = 'g';
}
MyText = MyText.replace(new RegExp((EditrFindField),mod),EditrReplaceField);
}
catch(e)
{
Message(Lang_NotFound2);
}
}
else
{
if (!EditrCaseField)
{
MyText = ReplaceAll(MyText,EditrFindField,EditrReplaceField);
}
else
{
MyText = ReplaceAllB(MyText,EditrFindField,EditrReplaceField);
}
}
SetEditorText(MyText);
ShowWind(false);
Message(Lang_Replaced+': '+n);
ScrollToPos(editor,0);
Select(editor,0,0);
}
else
{
Message(Lang_NotFound2);
}
}
else
{
if (findField)
{
findField.focus();
}
}
}
function EditorReplaceNext()
{
EditorFindNext(undefined,true);
}
function EditrReload()
{
SetEditorText(CreateSaveData(true));
ScrollToPos(editor,0);
if (!isTouchDevice)
{
Select(editor,0,0);
}
UpdateFooter();
}
function EditrShowHide()
{
if (EditorMode)
{
AreYouSure('EditrHide',Lang_AYSExitSourceEditor);
}
else
{
EditrShow();
}
}
function EditrShow()
{
ShowWind(false);
if (EditorVisible)
{
SaveLocation(false);
}
$('navToolText').innerHTML = Lang_Navigator;
EditorMode = true;
EditorVisible = true;
VisualEventsStop();
SetColorButton(0);
editorBlock.style.display = 'block';
editor_buttons.style.display = 'inline';
show_lines.style.display = 'none';
ResizeEditor();
footer_status.innerHTML = '';
editorTitle.value = Lang_SourceCode;
editorTitle.readOnly = true;
EditrReload();
SetPrefValue('EditorMode','true');
}
function EditrHide()
{
ShowWind(false);
HideBackButton();
VisualEventsStart();
EditorVisible = false;
EditrSave();
ClearEditorColor();
EditorMode = false;
editorTitle.readOnly = false;
editorBlock.style.display = 'none';
editor_buttons.style.display = 'none';
ShowLinesButton();
ClearUndoBuffer();
SelectNPaint();
UpdateFooter();
SetPrefValue('EditorMode','false');
}
function EditrSave()
{
var Data = GetEditorText();
var DataArr = Data.split('\n');
var Loc, line, Title;
var n = Cell;
var UpdatedTitles = [];
var DupsTitles = [];
for (var key in DataArr)
{
line = DataArr[key];
if (line.trim()!='')
{
if (line.trim().substr(0,2)=="::")
{
Title = line.trim().substr(2);
Title = Title.split('[::]')[0].trim();
if (Title!='')
{
Loc = GetLocByTitle(Title);
if (Loc==false)
{
n+=Cell*2;
Loc = new Locations(Title,'',n,n,0,false);
LocationArr.push(Loc);
}
else
{
if (UpdatedTitles.indexOf(Title.toLowerCase())==-1)
{
Loc.Title = Title;
Loc.Text = '';
UpdatedTitles.push(Title.toLowerCase());
}
else
{
Loc.Text += '\n***\n';
DupsTitles.push(Title.toLowerCase());
}
}
}
}
else
{
if (Loc)
{
Loc.Text = Loc.Text+RTrim(line)+'\n';
}
}
}
}
UpdatedTitles = [];
var LocNames = GetTitlesArray(true);
LocNames = ArrayUnique(LocNames);
for (var i=0; i<LocationArr.length; i++)
{
Title = LocationArr[i].Title.toLowerCase();
if (LocNames.indexOf(Title)==-1)
{
LocationArr.splice(i,1);
i--;
}
}
if (LocationArr.length<1)
{
CreateDefaultPassage(Data);
}
ParseAll();
if (DupsTitles.length>0)
{
var str = Lang_FoundDuplicates+':<p>';
DupsTitles = ArrayUnique(DupsTitles);
var i = 0;
for (var key in DupsTitles)
{
str += DupsTitles[key];
if (i!=DupsTitles.length-1)
{
str += ', ';
}
i++;
}
str += "</p><div class='button' id='item1'>OK</div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){ShowWind(false);});
ShowWind(true);
}
}
function GetCurrPassage(MyText,currentPos)
{
var StartTit, StartPar, EndPar, Title;
var Ret = [];
var myRe, myMatch;
var selData = GetSelection(editor);
if (!currentPos)
{
currentPos = selData['start'];
}
currentPos = currentPos+1;
if (!MyText)
{
MyText = GetEditorText();
}
myRe = /^[ \t]*?::/gm;
prev_lastIndex = 0;
while ((myMatch = myRe.exec(MyText)) != null)
{
myRe.lastIndex = myRe.lastIndex-myMatch[0].length+1;
if (myRe.lastIndex>currentPos)
{
break;
}
prev_lastIndex = myRe.lastIndex-1;
}
StartTit = prev_lastIndex;
EndPar = myRe.lastIndex-1;
if (EndPar<0)
{
EndPar = MyText.length;
}
myRe = /$/gm;
myRe.lastIndex = StartTit;
myMatch = myRe.exec(MyText);
StartPar = myRe.lastIndex;
Title = MyText.substr(StartTit,StartPar-StartTit).trim();
if (Title.substr(0,2)=='::')
{
Title = Title.substr(2).trim();
}
else
{
Title = '';
}
Ret.push(StartTit);
Ret.push(StartPar);
Ret.push(EndPar);
Ret.push(Title);
return Ret;
}
function DarkModeSwitch()
{
if (DarkMode)
{
DarkMode = false;
SetPrefValue('DarkMode','false');
var bg  = '#FFFFFF';
var bg2 = '#FFFFFF';
var fn = '#222222';
}
else
{
DarkMode = true;
SetPrefValue('DarkMode','true');
var bg  = '#34495e';
var bg2 = '#34495e';
var fn = '#EEEEEE';
}
editorBlock.style.backgroundColor = bg2;
editorTop.style.backgroundColor = bg;
editorTitle.style.backgroundColor = bg;
editor.style.color = fn;
editor.style.backgroundColor = bg;
if (editorColor)
{
editorColor.style.color = fn;
editorColor.style.backgroundColor = bg;
RefreshText(true);
}
}
function ColorModeSwitch()
{
if (ColorMode)
{
ColorMode = false;
SetPrefValue('ColorMode','false');
}
else
{
ColorMode = true;
SetPrefValue('ColorMode','true');
}
RefreshText(true);
}
function EditrGotoLocation(title,prevTitle)
{
title = title.trim();
if (EditorMode)
{
var myRe = new RegExp("^\\s*?::\\s*?"+title+"\\s*?(\\[::\\].*?)?$",'gim');
var myMatch = myRe.exec(GetEditorText());
if (myMatch != null)
{
var pos = myRe.lastIndex-myMatch[0].trim().length;
ScrollToPos(editor,pos);
if (editorColor && ColorMode)
{
Select(editor,pos,0);
}
else
{
Select(editor,pos,myMatch[0].trim().length);
}
}
else
{
NewLocation(title);
}
}
else
{
if (GetLocIndexByTitle(title)!=-1)
{
OpenEditor(false);
EditLocation(title);
}
else
{
NewLocation(title);
}
}
if (prevTitle)
{
if (prevTitle!='')
{
PrevTitle = prevTitle;
buttonBack.style.display = 'inline-block';
}
}
}
function EditrGotoLink()
{
var MyText = GetEditorText();
var myRe, myMatch;
var selData = GetSelection(editor);
var currentPos = selData['start'];
var Link = '';
myRe = LinksPattern;
myRe.lastIndex = 0;
while ((myMatch = myRe.exec(MyText)) != null)
{
if (myRe.lastIndex>currentPos)
{
Link = myMatch[1];
break;
}
}
if (myMatch==null || currentPos<=(myRe.lastIndex-myMatch[0].length))
{
myRe = DisplayPattern;
myRe.lastIndex = 0;
while ((myMatch = myRe.exec(MyText)) != null)
{
if (myRe.lastIndex>currentPos)
{
Link = myMatch[2];
break;
}
}
if (myMatch==null || currentPos<=(myRe.lastIndex-myMatch[0].length))
{
return;
}
}
Link = Link.toLowerCase().trim();
if (Link!='' && Link.substr(0,1)!="$" && Link.substr(0,5)!="file:" && Link.substr(0,5)!="файл:")
{
Link = Link.replace(/\{\s*\$[^{}]+?\}/g,'');
Link = Link.trim();
if (EditorMode)
{
EditrGotoLocation(Link,GetCurrPassage()[3]);
}
else
{
EditrGotoLocation(Link,editorTitle.value.trim());
}
}
}
function EditrBack()
{
if (PrevTitle.trim()!='')
{
EditrGotoLocation(PrevTitle);
}
HideBackButton();
}
function HideBackButton()
{
PrevTitle = '';
buttonBack.style.display = 'none';
}
function CancelChanges()
{
if (!editNewLoc)
{
var Loc = LastLocation();
editorTitle.value = Loc.Title;
SetEditorText(LocTextBak);
ScrollToPos(editor,0);
Select(editor,0,0);
}
else
{
editorTitle.value = '';
editorTitle.focus();
SetEditorText('');
ClearEditorColor();
SetColorButton(0);
}
}
function AddText(action)
{
var myText = GetEditorText();
myText = ReplaceLineEndings(myText);
var myTextLen = myText.length;
var selData = GetSelection(editor);
var pos = selData['start'];
var sbegin = 0;
var slen_bak = selData['end']-selData['start'];
var slen = 0;
for (var i=pos; i>=0; i--) {
if (i==0 || myText.substr(i-1,1)=='\n') {
sbegin = i;
break;
}
}
for (var i=pos+slen_bak; i<=myTextLen; i++) {
if (i>=myTextLen || myText.substr(i,1)=='\n') {
slen = i-sbegin;
break;
}
}
if (slen<1) {
slen = 1;
}
var s = '';
if (action==3) {
s = myText.substr(sbegin,slen);
s += '\n\n' + s;
} else {
var n = 0;
var added = false;
var removed = false;
var linesArr = Split(myText.substr(sbegin,slen),'\n');
for (var i=0; i<linesArr.length; i++) {
s = linesArr[i];
if (action==1) {
if (s!='') {
if (!EditorMode || (EditorMode && LTrim(s).substr(0,2)!='::')) {
linesArr[i] = '\t' + s;
added = true;
n++;
}
}
} else if (action==2) {
if (s!='' && (s.substr(0,1)=='\t' || s.substr(0,1)==' ')) {
linesArr[i] = Right(s,s.length-1);
removed = true;
n++;
}
}
}
s = linesArr.join('\n');
}
ToUndoBuffer();
SetEditorText(Left(myText,sbegin) + s + Right(myText,myText.length-sbegin-slen));
if (added) {
Select(editor,sbegin,slen+n);
} else if (removed) {
Select(editor,sbegin,slen-n);
} else {
Select(editor,pos,slen_bak);
}
}
function InsertText(text,noselect)
{
var oS = editor.scrollTop;
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentEnd = selData['end'];
var strLeft = GetEditorText().substr(0,currentPos);
var strRight = GetEditorText().substr(currentEnd);
SetEditorText(strLeft+text+strRight);
if (noselect)
{
Select(editor,currentPos+text.length,0);
}
else
{
Select(editor,currentPos,text.length);
}
editor.scrollTop = oS;
}
function RefreshText(color,event)
{
if (EditorVisible)
{
var selData = GetSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
if (editor.value=='')
{
editor.value = '\n'
SelectText(editor,0,0);
}
if (color)
{
var text = editor.value;
if (text=='')
{
RefreshText(false,event);
return;
}
var brackets = text.substr(0,currentPos).split('<').length-1;
brackets += text.substr(0,currentPos).split('>').length-1;
var ampersands = text.substr(0,currentPos).split('&').length-1;
text = text.replace(/&/g,'&amp;');
text = text.replace(/</g,'&lt;');
text = text.replace(/>/g,'&gt;');
if (ColorMode)
{
text = ColorText(text,currentPos+brackets*3+ampersands*4);
}
editorColor.innerHTML = text;
if (editor.value.substr(-1)=='\n')
{
editorColor.innerHTML += '\n';
}
editorColor.scrollTop = editor.scrollTop;
editor.style.zIndex = 90;
editorColor.style.zIndex = 91;
SelectText(editorColor,currentPos,currentLen);
}
else
{
if (event)
{
var k = GetKeyCode(event);
if (k==27)
{
return;
}
}
editorColor.style.zIndex = 90;
editor.style.zIndex = 91;
SelectText(editor,currentPos,currentLen);
}
}
}
function GetEditorText()
{
return editor.value;
}
function SetEditorText(text)
{
editor.value = text;
}
function ColorText(text,pos)
{
if (EditorVisible)
{
var dms = '';
if (DarkMode)
{
dms = '2';
}
var text1 = '';
var text2 = '';
var title = '';
if (EditorMode)
{
var passData = GetCurrPassage(text,pos);
var sbegin = 0;
var send = 0;
sbegin = passData[0];
title = text.substr(sbegin+2,passData[1]-sbegin);
send = passData[2];
text1 = "<span class=asm_disabled>"+text.substr(0,sbegin)+"</span>";
text2 = "<span class=asm_disabled>"+text.substr(send)+"</span>";
text = text.substr(sbegin,send-sbegin);
}
else
{
title = editorTitle.value;
}
if (!SimpleLocation(title))
{
text = text.replace(/^\s*?\* |^\s*?\*\s*?\*\s*?\*\s*?$/gm,"<span class=asm_format"+dms+">$&</span>");
text = text.replace(/''|\/\/|__|\{\{\{|\}\}\}|\=\=\=|\@\@\@|~~|\^\^|--|%%%/g,"<span class=asm_format"+dms+">$&</span>");
text = text.replace(/\[\[([\+\-\*]?)(.*?)(\|*)([^|\r\n]*?)\]\]/g,function(match,p1,p2,p3,p4){p4=p4.replace(/<span.*?>|<\/span>/g,'');return "<span class=asm_linkB"+dms+">[[</span><span class=asm_macros"+dms+">"+p1+"</span>"+p2+"<span class=asm_linkB"+dms+">"+p3+"</span><span class=asm_link"+dms+">"+p4+"</span><span class=asm_linkB"+dms+">]]</span>"});
var temp = '<>';
text = text.replace(DisplayPatternC,function(match,p1,p2,p3){return match.replace(p1,temp).replace(p3,"<span class=asm_link"+dms+">"+p3+"</span>").replace(temp,p1);});
var badlinks = [];
var Link;
var myMatch;
var found;
var myRe = /<span class=asm_link2?>(.+?)<\/span>/g;
var Titles = GetTitlesArray(true,false);
while ((myMatch = myRe.exec(text)) != null)
{
Link = myMatch[1].toLowerCase();
Link = Link.replace(/\{\s*[^{}]*?\}/g,'');
Link = Link.trim();
if (Link.indexOf("://")==-1 && Link.search(/(файл|file):/i)==-1)
{
found = false;
if (!EditorMode)
{
if (Link==editorTitle.value.toLowerCase().trim())
{
found = true;
}
}
if (!found)
{
for (var key in Titles)
{
if (Titles[key]==Link)
{
found = true;
break;
}
}
if (!found)
{
badlinks.push(Link);
}
}
}
}
badlinks = ArrayUnique(badlinks);
for (var key in badlinks)
{
text = text.replace(new RegExp("<span class=asm_link2?>(\\s*?"+EscapeRegExp(badlinks[key])+".*?)</span>",'gi'),"<span class=asm_error>$1</span>");
}
text = text.replace(/\{{1}\s*\$[^{}]+?\}/g,"<span class=asm_macros"+dms+">$&</span>");
text = text.replace(/(&lt;&lt;.*?(?!&lt;&lt;)(?!&gt;&gt;).*?&gt;&gt;)|(&lt;&lt;&gt;&gt;)/g,"<span class=asm_macros"+dms+">$&</span>");
text = text.replace(/(["'])(.*?(?!'').*?)(["'])/g,"$1<span class=asm_text"+dms+">$2</span>$3");
var myRe = new RegExp("(\\${1,2}[^"+BadVarsSymbs+"]+?(\\[[^\\[]*?\\])?)(["+BadVarsSymbs+"])",'g');
text = text.replace(myRe,"<span class=asm_var"+dms+">$1</span>$3");
text = text.replace(/^\s*?#.*?$/gm,function(match){return "<span class=asm_comment"+dms+">"+match.replace(/<span.*?>|<\/span>/g,'')+"</span>"});
}
if (EditorMode)
{
text = text.replace(/^((&lt;.+?&gt;)?\s*?){1,2}:(&lt;.+?&gt;)?:.*?$/gm,function(match){return "<span class=asm_title"+dms+">"+match.replace(/<span.*?>|<\/span>/g,'')+"</span>"});
}
return text1+text+text2;
}
}
function GetSelectedText()
{
obj = $(getFocusedId());
if (obj.value!==undefined)
{
var selData = GetSelection(obj);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
if (currentLen>0)
{
var text = obj.value.substr(currentPos,currentLen);
if (TargetDesktop)
{
FromJavascript('SelectedText::'+text);
}
return text;
}
else
{
return false;
}
}
else
{
return false;
}
}
function CutSelectedText()
{
obj = $(getFocusedId());
if (obj.value!==undefined)
{
var selData = GetSelection(obj);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
if (currentLen>0)
{
var text = obj.value.substr(currentPos,currentLen);
if (TargetDesktop)
{
FromJavascript('SelectedText::'+text);
}
var s = obj.value;
obj.value = s.substr(0,currentPos)+s.substr(currentPos+currentLen);
Select(obj,currentPos,0);
}
}
}
function PasteText(text)
{
if (text!='')
{
obj = $(getFocusedId());
if (obj.value!==undefined)
{
text = text.replace(/\[\[asmNewLine\]\]/g,"\n");
text = text.replace(/\[\[asmApostr\]\]/g,"'");
var selData = GetSelection(obj);
var currentPos = selData['start'];
var currentEnd = selData['end'];
var strLeft = obj.value.substr(0,currentPos);
var strRight = obj.value.substr(currentEnd);
obj.value = strLeft+text+strRight;
Select(obj,currentPos+text.length,0);
}
}
}
function SelectAll()
{
obj = $(getFocusedId());
if (obj.value!==undefined)
{
obj.select();
}
}
function UpdateCaret()
{
if (EditorVisible && getFocusedId()=='editorColor')
{
var selData = GetSelection(editorColor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
SelectText(editor,currentPos,currentLen);
var edst = editorColor.scrollTop;
RefreshText(true);
editorColor.scrollTop = edst;
editor.scrollTop = edst;
}
}
function Select(obj,SelStart,SelLength)
{
SelectText(obj,SelStart,SelLength);
if (editorColor && obj==editor)
{
RefreshText(true);
}
}
function ClearEditorColor()
{
if (editorColor)
{
editorColor.innerHTML = '';
}
}
var editor;
var editorColor;
var editorBlock;
var editorTitle;
var editorTop;
var message;
var maincanvas;
var maincanvas_w;
var maincanvas_h;
var maincanvasMaxWidth = 2600;
var maincanvasMaxHeight = 5000;
var canvasWrap;
var canvasWrap_w;
var canvasWrap_h;
var canvasWrapScrollLeft = 0;
var canvasWrapScrollTop = 0;
var ClickOnLoc;
var LocationArr = [];
var GroupArr = {};
var MouseX = 0;
var MouseY = 0;
var mouseOffsetX = 0;
var mouseOffsetY = 0;
var SelectedLocation = -1;
var ctx;
var pixel_ratio = 1;
var Cell = 8;
var MinimalWidth = 640;
var LocSizeDefault = 112;
var LocSize = LocSizeDefault;
var LocSizeSmall = 48;
var LocSizeMin = LocSizeSmall + Cell;
var LocSizeMax = 144;
var colorbutton;
var Colors = [];
Colors.push('#2980b9');
Colors.push('#34495e');
Colors.push('#7f8c8d');
Colors.push('#e74c3c');
Colors.push('#9b59b6');
Colors.push('#e67e22');
Colors.push('#16a085');
var SelLocColor = '#e67e22';
var LinesColor =  '#DDDDDD';
var toolbar;
var toolbarH;
var footer;
var mywindow;
var mywindow_inner;
var EditorVisible = false;
var WindowVisible = false;
var isMouseDown = false;
var editNewLoc = false;
var LocTitleH = 19;
var selectedColor = 0;
var buffer = document.createElement('canvas');
var EditorMode = false;
var fileNum = 0;
var BadVarsSymbs = "$!?.,:;<>{}|\(\)\\[\\]'\"@\^&/\\§`~±+\\-*%#=\\s";
var LinksPattern = /\[\[[\+\-\*]?.*?\|*([^|]+?)\]\]/g;
var DisplayPattern = /<<\s*(display|repeat|stop|input|goto)\s*['"]?([^'"]+?)['"]?[^'"]*?>>/gi;
var DisplayPatternC = /(&lt;&lt;[ \t]*)(display|repeat|stop|input|goto)[ \t]*['"]([^'"]+?)['"][^'"\r\n]*?&gt;&gt;/gi;
var UndoBuffer = [];
var isTouchDevice = false;
var previewWin;
var closePreview;
var PreviewVisible = false;
var mainDiv;
var SysVars = ['title','from','time','choice','gender'];
var LoadMerge = false;
var MenuItemsList = [];
var SelectedMenuItemNum = 0;
var EditrCaseField = false;
var EditrSelectedField = false;
var EditrRegExField = false;
var EditrFindField = '';
var EditrReplaceField = '';
var EditrTitlesRegExp = /^[ \t]*?::[ \t]?([ \t\S]+?)$/mg;
var editor_buttons;
var show_lines;
var DarkMode = false;
var ColorMode = true;
var buttonBack;
var PrevTitle = '';
var isShowLines = true;
var prevLastLocGroup = '';
var AuthLogin = '';
var AuthPassword = '';
var fontSize = 0;
var PlateVisible = false;
var myplate, platecontent, platetitle;
var isWebKit = 'WebkitAppearance' in document.documentElement.style;
var lastSearchQuery;
var LocTextBak = '';
var previewTheme;
var style_themes = [];
var style_current_theme = 0;
var style_view = 'mw';
var StyleText = '';
var html = '';
var tire = '—';
var tz = '#tz59_asm#';
var FilesLinks = [];
var StoryPath = '';
var maxFileSizeStoryData = 1024*1024;
var TargetWindows  = GetURLHashVar('TargetWindows');  if (TargetWindows===undefined)  {TargetWindows = false;}
var TargetMacOS    = GetURLHashVar('TargetMacOS');    if (TargetDesktop===undefined)  {TargetDesktop = false;}
var TargetLinux    = GetURLHashVar('TargetLinux');    if (TargetLinux===undefined)    {TargetLinux = false;}
var TargetChromeApp= GetURLHashVar('TargetChromeApp');if (TargetChromeApp===undefined){TargetChromeApp = false;}
var TargetDesktop  = false; if (TargetWindows || TargetMacOS || TargetLinux || TargetChromeApp) {TargetDesktop = true;}
var TargetAndroid  = GetURLHashVar('TargetAndroid');  if (TargetAndroid===undefined)  {TargetAndroid = false;}
var TargetIOS      = GetURLHashVar('TargetIOS');      if (TargetIOS===undefined)      {TargetIOS = false;}
var TargetBrowser  = true; if (TargetDesktop || TargetAndroid || TargetIOS) {TargetBrowser = false;}
var TargetFormat   = 'html';
var TargetFormatBak= TargetFormat;

startApp();
function Locations(Title,Text,Left,Top,ColorNum,ErrorLocation,Links,Vars)
{
this.Title = Title;
this.Text = Text;
this.Left = Left;
this.Top = Top;
this.ColorNum = ColorNum;
this.ErrorLocation = ErrorLocation;
Links = [];
this.Links = Links;
Vars = [];
this.Vars = Vars;
}
function RemoveComments(str)
{
return str.replace(/^\s*\#.*?$/gm,'').trim();
}
function ParseLocation(LocIndex)
{
if (SimpleLocation(LocationArr[LocIndex].Title))
{
return;
}
s = LocationArr[LocIndex].Text;
s = RemoveComments(s);
var myRe, myMatch, Link, Var;
LocationArr[LocIndex].Links = [];
myRe = LinksPattern;
myRe.lastIndex = 0;
while ((myMatch = myRe.exec(s)) != null)
{
Link = myMatch[1].toLowerCase();
Link = Link.replace(/\{\s*\$[^{}]+?\}/g,'');
Link = Link.trim();
if (Link!=LocationArr[LocIndex].Title.toLowerCase().trim() && Link.indexOf("://")==-1 && Link.search(/(файл|file):/i)==-1 && Link.substr(0,1)!="$")
{
LocationArr[LocIndex].Links.push(Link);
}
}
myRe = DisplayPattern;
while ((myMatch = myRe.exec(s)) != null)
{
Link = myMatch[2].toLowerCase().trim();
if (Link!=LocationArr[LocIndex].Title.toLowerCase().trim() && Link.substr(0,1)!="$")
{
LocationArr[LocIndex].Links.push(Link);
}
}
LocationArr[LocIndex].Links = ArrayUnique(LocationArr[LocIndex].Links);
var GoodLinks = 0;
for (j=0; j<LocationArr[LocIndex].Links.length; j++)
{
if (GetLocByTitle(LocationArr[LocIndex].Links[j]))
{
GoodLinks++;
}
}
if (GoodLinks==LocationArr[LocIndex].Links.length)
{
LocationArr[LocIndex].ErrorLocation = false;
}
else
{
LocationArr[LocIndex].ErrorLocation = true;
}
LocationArr[LocIndex].Vars = [];
var myRe2, myMatch2;
s = s.replace(/\[\[.*?(?!\]\]).*?\]?\]\]/g,"<<link $&>>");
myRe = /<<(.*?(?!<<)(?!>>).*?)>>/g;
myRe2 = new RegExp("(\\${1,2}[^"+BadVarsSymbs+"]+?)["+BadVarsSymbs+"]",'g');
while ((myMatch = myRe.exec(s)) != null)
{
macros = ' '+myMatch[1]+' ';
while ((myMatch2 = myRe2.exec(macros)) != null)
{
Var = myMatch2[1].trim();
Var = Var.replace(/^\$/,'');
if (Left(Var,1)!='$')
{
LocationArr[LocIndex].Vars.push(Var);
}
}
}
LocationArr[LocIndex].Vars = ArrayUnique(LocationArr[LocIndex].Vars);
}
function ParseAll()
{
for (i=0; i<LocationArr.length; i++)
{
ParseLocation(i);
}
}
function SimpleLocation(LocTitle)
{
LocTitle = LocTitle.toLowerCase().trim();
if (LocTitle=="storytitle" || LocTitle=="storyauthor" || LocTitle=="storysubtitle" || LocTitle=="storystyle" || LocTitle=="storystyle.bak" || LocTitle=="storydata")
{
return true;
}
else
{
return false;
}
}
function ForbiddenLocation(LocTitle)
{
if (LocTitle)
{
LocTitle = LocTitle.toLowerCase().trim();
if (LocTitle=="storystyle.bak" || LocTitle=="storydata")
{
return true;
}
else
{
return false;
}
}
}
function startApp()
{
document.addEventListener('dragstart', function(e) {e.preventDefault();});
if (TargetChromeApp) {
chrome_MessageListener();
}
if (TargetBrowser || TargetAndroid)
{
window.onbeforeunload = confirmExit;
}
if (!TargetDesktop)
{
if ('ontouchmove' in window)
{
isTouchDevice = true;
}
var fn = GetPrefValue('fileNum');
if (fn!='' && fn!='0')
{
fileNum = parseInt(fn,10);
}
}
if (TargetBrowser)
{
AuthLogin = GetPrefValue('login');
AuthPassword = GetPrefValue('password');
}
else if (TargetChromeApp)
{
GetPrefValue('login',function(value) {
AuthLogin = value;
});
GetPrefValue('password',function(value) {
AuthPassword = value;
});
}
else if (!TargetDesktop)
{
AuthLogin = GetPrefValue('AuthLogin');
AuthPassword = GetPrefValue('AuthPassword');
}
$('toolbutton_add').onclick = function() {NewLocation(false,true);};
$('toolbutton_act').onclick = function() {ActionsMenu();};
$('toolbutton_arr').onclick = function() {ArrangeButton();};
$('toolbutton_save').onclick = function() {SaveStoryButton(true);};
$('toolbutton_load').onclick = function() {SelectStory(true);};
$('toolbutton_new').onclick = function() {NewGameButton();};
$('toolbutton_run').onclick = function() {CompileButton();};
$('toolbutton_publ').onclick = function() {Publish();};
$('toolbutton_nav').onclick = function() {Navigator();};
$('toolbutton_help').onclick = function() {Help();};
$('closebuttonBig').onclick = function() {CloseEditorButton();};
$('linksbutton').onclick = function() {InsertLink();};
$('macrosbutton').onclick = function() {InsertMacro();};
$('editorTitle').onclick = function() {ShowWind(false);};
$('formatbutton').onclick = function() {FormatText();};
$('commentbutton').onclick = function() {CommentText();};
$('colorbutton').onclick = function() {SelectColor();};
$('editor').onclick = function() {ShowWind(false);};
$('message').onclick = function() {MessageHide();};
$('closebutton_wind').onclick = function() {ShowWind(false);};
$('maincanvas').onclick = function() {ShowWind(false);};
$('buttonBack').onclick = function() {EditrBack();};
$('buttonCompPass').onclick = function() {CompilePassage();};
$('buttonFindNext').onclick = function() {EditorFindNext();};
$('buttonAutoDop').onclick = function() {AutoDop();};
$('show_lines').onclick = function() {ShowLines();};
$('closePreview').onclick = function() {ClosePreview();};
if (TargetChromeApp) {
$('footer_info').onclick = function() {chrome_SelectFilesDir();};
}
if (TargetChromeApp) {
$('previewWin').src = 'preview.html';
}
mainDiv = $('mainDiv');
previewWin = $('previewWin');
closePreview = $('closePreview');
message = $('message');
toolbar = $('toolbar');
mywindow = $('wind');
mywindow_inner = $('windcontent');
footer = $('footer');
footer.style.display = 'block';
footer_status = $('footer_status');
footer_status.innerHTML = 'AXMA Soft';
maincanvas = $('maincanvas');
canvasWrap = $('canvasWrap');
if (!isTouchDevice)
{
ResizeCanvasDPI(maincanvasMaxWidth,maincanvasMaxHeight);
canvasWrap.style.overflow = 'auto';
}
editorBlock = $('editorBlock');
editor = $('editor');
if (!(isTouchDevice || !isWebKit))
{
var contextmenuVisible = false;
editorColor = document.createElement('pre');
editorBlock.appendChild(editorColor);
editorColor.id = 'editorColor';
editorColor.contentEditable = true;
editorColor.spellcheck = false;
editorColor.style.zIndex = '91';
editorColor.onkeydown = function(event) { if (!WindowVisible) { RefreshText(false,event); }; };
editorColor.onmousedown = function(event) { editor.focus();ShowWind(false); };
editorColor.oncontextmenu = function() { contextmenuVisible=true; RefreshText(false); };
window.onmouseup = function() { if (EditorVisible && !RightClick(event)) { if (!contextmenuVisible) { UpdateCaret(); } else { RefreshText(true); contextmenuVisible=false; }; }; };
editor.onkeyup = function() { RefreshText(true); };
editor.oncut = function(event) { if (contextmenuVisible) { setTimeout(function() { RefreshText(true); contextmenuVisible=false; },0); }; };
editor.onpaste = editor.oncut;
editor.oncopy = editor.oncut;
}
if (TargetChromeApp)
{
GetPrefValue('FontSize',function(value) {
fontSize = value;
SetFontEditor();
});
}
else if (!TargetDesktop)
{
fontSize = GetPrefValue('FontSize');
SetFontEditor();
}
else
{
GetPrefValue('FontSize');
}
editorTop = $('editorTop');
editorTitle = $('editorTitle');
editorTitle.placeholder = Lang_PassageTitle;
colorbutton = $('colorbutton');
editor_buttons = $('editor_buttons');
show_lines = $('show_lines');
buttonBack = $('buttonBack');
ctx = maincanvas.getContext('2d');
buffer.ctx = buffer.getContext('2d');
pixel_ratio = (window.devicePixelRatio || 1) / (ctx.webkitBackingStorePixelRatio || ctx.mozBackingStorePixelRatio || ctx.msBackingStorePixelRatio || ctx.oBackingStorePixelRatio || ctx.backingStorePixelRatio || 1);
if (pixel_ratio>1)
{
LocTitleH = LocTitleH * pixel_ratio;
Cell = Cell* pixel_ratio;
MinimalWidth = MinimalWidth * pixel_ratio;
LocSizeDefault = LocSizeDefault * pixel_ratio;
LocSize = LocSizeDefault * pixel_ratio;
LocSizeSmall = LocSizeSmall * pixel_ratio;
LocSizeMin = LocSizeMin * pixel_ratio;
LocSizeMax = LocSizeMax * pixel_ratio;
}
myplate = document.createElement('div');
document.body.appendChild(myplate);
myplate.id = 'myplate';
myplate.className = 'plate';
myplate.oncontextmenu = function() { return false; };
myplate.innerHTML = "<table id='platetop' border='0' cellpadding='0' cellspacing='0'><tr><td width='5%' align='left'><div id='plateclose' class='closebutton'></div></td><td width='90%' align='center'><div id='platetitle'></div></td><td width='5%'>&nbsp;</td></tr></table><div id='platecontent'></div>";
SetEvent('plateclose',function(){ShowPlate(false);});
platecontent = $('platecontent');
platetitle = $('platetitle');
var tbList = getElementsByClass('tooltext',toolbar);
for (var i=0;i<tbList.length;i++)
{
tbList[i].innerHTML = Lang_ToolButtons[i];
}
tbList = getElementsByClass('editor_button',footer);
for (var i=0;i<tbList.length;i++)
{
tbList[i].title = Lang_SmallButtons[i];
}
tbList = getElementsByClass('closebuttonBig linkbutton color',editorTop);
for (var i=0;i<tbList.length;i++)
{
tbList[i].title = Lang_EditorButtons[i];
}
if (TargetChromeApp)
{
GetPrefValue('ShowLines',function(value) {
if (value=='false') {
isShowLines = false;
}
});
}
else
{
if (GetPrefValue('ShowLines')=='false')
{
isShowLines = false;
}
}
ResizeEvent();
CanvasToTop();
if (TargetChromeApp)
{
chrome_LoadRecent();
}
else if (!TargetDesktop && LoadStory(fileNum)==false)
{
NewGame();
}
window.onkeydown = function (e) { keys(e,''); };
if (!isTouchDevice)
{
window.onkeypress = function (e) { keysPress(e,''); };
}
window.onkeyup = function (e) { keysUp(e,''); };
window.addEventListener('resize', ResizeEvent, false);
window.addEventListener('orientationchange', ResizeEvent, false);
VisualEventsStart();
setInterval(UpdateFooter, 1500);
if (TargetBrowser)
{
var vdate = 0;
loadData(CommandServer('com=versiondate'),function(vdate) {
setInterval('CheckUpdates('+vdate.trim()+');',3600000);
});
}
if (TargetDesktop || isLocalStorageAvailable)
{
setInterval(ToBackup, 300000);
}
if (TargetChromeApp)
{
GetPrefValue('EditorMode',function(value) {
if (value=='true') {
EditrShow();
}
});
GetPrefValue('ColorMode',function(value) {
if (value=='false') {
ColorModeSwitch();
}
});
GetPrefValue('DarkMode',function(value) {
if (value=='true') {
DarkModeSwitch();
}
});
}
else if (TargetDesktop)
{
GetPrefValue('EditorMode');
GetPrefValue('ColorMode');
GetPrefValue('DarkMode');
}
else
{
if (GetPrefValue('EditorMode')=='true')
{
EditrShow();
}
if (GetPrefValue('ColorMode')=='false')
{
ColorModeSwitch();
}
if (GetPrefValue('DarkMode')=='true')
{
DarkModeSwitch();
}
}
if (GetURLHashVar('QuickStart'))
{
QuickStart();
}
}
function selectTypeGame(type)
{
if (type=='ia') {
TargetFormat = 'audio';
style_view = false;
} else {
TargetFormat = 'html';
style_view = type;
}
if (type=='vis') {
style_current_theme = 10;
} else {
if (style_current_theme==10) {
style_current_theme = 0;
}
}
BackupStoryStyle();
NewGameButton();
}
function NewGameButton()
{
var selected = ' selected';
var selected1='', selected2='', selected3='', selected4='', selected5='';
if (TargetFormat=='audio') {
selected5 = selected;
} else {
if (style_view=='sw' || style_view===true) {
selected2 = selected;
} else if (style_view=='rpg') {
selected3 = selected;
} else if (style_view=='vis') {
selected4 = selected;
} else {
selected1 = selected;
}
}
var str = "";
str += "<div>"+Lang_GameType+"</div>";
str += "<div><select id='item1' style='margin:6px 0;font-size:13px;'>";
str += "<option"+selected1+" value='mw'>"+Lang_MultiView+"</option>";
str += "<option"+selected3+" value='rpg'>"+Lang_InterQuest+"</option>";
str += "<option"+selected4+" value='vis'>"+Lang_InterVisual+"</option>";
str += "<option"+selected2+" value='sw'>"+Lang_ClassicView+"</option>";
str += "<option"+selected5+" value='ia'>"+Lang_InterABook+"</option>";
str += "</select></div>";
if (TargetFormat=='html' && style_view!='vis') {
str += "<div style='margin-top:6px;'>"+Lang_GameTheme+"</div>";
str += "<div><select id='item2' style='margin:6px 0;font-size:13px;'>";
for (var i=0; i<style_themes.length; i++) {
selected1 = '';
if (i==style_current_theme) {
selected1 = selected;
}
str += "<option"+selected1+" value='"+i+"'>"+style_themes[i]+"</option>";
}
str += "</select></div>";
}
if (TargetFormat=='html') {
str += "<div class='menu_item' id='item3'>"+Lang_MenuNewStandart+"</div>";
}
str += "<div class='menu_item' id='item4'>"+Lang_MenuNewCompact+"</div>";
str += "<div class='menu_item' id='item5'>"+Lang_MenuNewClipboard+"</div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){selectTypeGame(this.value);},'change');
SetEvent('item2',function(){style_current_theme=parseInt(this.value);},'change');
SetEvent('item3',function(){AreYouSure('newGame',Lang_AYSCreateNewStory+' '+Lang_DontForgetSave);});
SetEvent('item4',function(){AreYouSure('newGameCompact',Lang_AYSCreateNewStory+' '+Lang_DontForgetSave);});
SetEvent('item5',function(){NewGameClipboard();});
ShowWind(true);
}
function newGameCompact()
{
NewGame(true);
}
function newGame()
{
NewGame();
}
function NewGame(compact)
{
if (TargetDesktop)
{
if (!TargetChromeApp) {
FromJavascript('NewGame');
} else {
chrome_SetCurrentEntry(null);
}
}
if (EditorVisible && !EditorMode)
{
OpenEditor(false);
}
if (LocSize>LocSizeSmall)
{
LocSize = LocSizeDefault;
}
SelectedLocation = -1;
LocationArr = [];
if (LangCode=='ru')
{
LocationArr.push(new Locations("StoryTitle","Название игры",328,Cell,1,false));
LocationArr.push(new Locations("StorySubtitle","Вставьте сюда краткое описание игры или её фабулу, уведомление пользователям или любой текст, который должен быть виден при запуске игры.",328,240,1,false));
LocationArr.push(new Locations("StoryAuthor","Автор игры",488,Cell,5,false));
if (TargetFormat=='html')
{
LocationArr.push(new Locations("StoryMenu","[[*Вступление|StorySubtitle]]\n[[[[File: http://sm.axmasoft.com/rsc/logo.png]]|http://sm.axmasoft.com]]",488,240,6,false));
}
if (!compact)
{
LocationArr.push(new Locations("Оформление параграфов","Это следующий параграф, сюда пользователь перейдёт по ссылке из первого параграфа.\nНу а дальше всё в ваших руках -- точнее, на кончиках пальцев...\nМожно менять «на лету» основную иллюстрацию.\n<<picture '[[File: http://sm.axmasoft.com/rsc/logo-big.png]]'>>\nАудио-файл формата mp3. Кнопка «Аудио» появится в панели инструментов.\n[[Файл: http://sm.axmasoft.com/rsc/kr.mp3]]\n%%%Примеры, как можно отформатировать текст%%%\n//Наклонный текст//\n''Жирный текст''\n__Подчёркнутый текст__\n{{{Моноширинный текст}}}\n===Этот текст выровнен по центру===\n@@@Этот текст выровнен по правому краю@@@\n#Это комментарий. Он не будет виден читателям.\nИзображение (поддерживаются форматы png, jpeg, gif):\n[[Файл: http://sm.axmasoft.com/rsc/logo.png]]\nГоризонтальная линия:\n----\nЭто -- тире, а не дефис и не минус\nНижний индекс: формула воды H~~2~~O\nВерхний индекс: площадь комнаты 25 м^^2^^\n* Первая строка списка\n* Вторая строка списка\n[[Несколько приёмов]], использование которых сделает игру интереснее.\n<<back>>\n[[Ссылка на первый параграф|start]]",Cell,240,0,false));
LocationArr.push(new Locations("Несколько приёмов","Создаём объект $ключ и выводим «Да», если ключ есть или «Нет», если ключ отсутствует: <<set $ключ=1>> <<if $ключ>> Да <<else>> Нет <<endif>>\n[[-Невозвратный переход|Start]]. После выбора этой ссылки читатель не сможет вернуться на предыдущую страницу кнопкой «Отменить».\nСсылка на параграф, содержимое которого будет выведено на этой же странице: [[+вывести текст начала игры|Start]].\nТекст, вставленный из другого параграфа: <<display 'StorySubtitle'>>\nСлучайное целое число от 0 до 6: <<random $случайно = 6>><<print $случайно>>.\nВыбор действия: <<choice 'Действие 1; Действие 2'>>\n# Номер выбранного действия хранится в системной переменной $$choice\n<<back>>",168,80,0,false));
}
LocationArr.push(new Locations("Start","Это первый параграф и первое, что увидит пользователь после краткого описания. Отсюда начинается написанная Вами игра.\nРазместив здесь ссылки на другие созданные параграфы, Вы дадите возможность пользователю [[продолжить|Оформление параграфов]] его приключение.\n<<picture '[[File: http://sm.axmasoft.com/rsc/back.jpg]]'>>",Cell,Cell,3,false));
}
else
{
LocationArr.push(new Locations("StoryTitle","Story Title",328,Cell,1,false));
LocationArr.push(new Locations("StorySubtitle","Here you can place a brief introduction, a summary of the plot, a disclaimer/warning, etc.",328,240,1,false));
LocationArr.push(new Locations("StoryAuthor","Story Author",488,Cell,5,false));
if (TargetFormat=='html')
{
LocationArr.push(new Locations("StoryMenu","[[*Introduction|StorySubtitle]]",488,240,6,false));
}
if (!compact)
{
LocationArr.push(new Locations("Formatting examples","This is the second passage, which readers proceed to by clicking a link in the first passage.\nWhat follows next? It is all in your hands... or, should we say, at your fingertips.\nYou can dynamically change the main picture.\n<<picture '[[File: http://sm.axmasoft.com/rsc/logo-big.png]]'>>\nAudio file (mp3 is supported). The 'Audio' button will appear on the toolbar.\n[[File: http://sm.axmasoft.com/rsc/kr.mp3]]\n%%%Examples of Text Formatting%%%\n//Text in italics//\n''Text in bold''\n__Underlined text__\n{{{Monospace font}}}\n===This text is center aligned.===\n@@@This text is right aligned.@@@\n#This is a comment. It will not be displayed to readers.\nImage (supported formats: png, jpeg, gif):\n[[File: http://sm.axmasoft.com/rsc/logo.png]]\nHorizontal separator:\n----\nEm dashes -- this is how you use them\nSubscript: the formula for water is H~~2~~O\nSuperscript: the area of the room is 25 m^^2^^\n* First item in the list\n* Second item in the list\n[[Some tricks]] to make your story even more fun.\n<<back>>\n[[Link to the first passage|start]]",Cell,240,0,false));
LocationArr.push(new Locations("Some tricks","Create a $key and display “Yes”, if the protagonist has the key, and “No”, if they do not: <<set $key=1>> <<if $key>> Yes <<else>> No <<endif>>\n[[-One way link|Start]]. After following this link readers will not be able to go back to the previous page via the Back button.\nClicking this link will display new text under the current passage without clearing the screen: [[+Display the beginning of the story|Start]].\nThis is a citation from another passage: <<display 'StorySubtitle'>>\nRandom number from 0 to 6: <<random $rndnum = 6>><<print $rndnum>>.\nChoose action: <<choice 'Action 1; Action 2'>>\n# See system variable $$choice\n<<back>>",168,80,0,false));
}
LocationArr.push(new Locations("Start","This is the first passage and it will be the first thing your reader will see under the subtitle. Here your story begins.\nBy linking to other passages you will enable your reader to [[continue|Formatting examples]] their adventure.\n<<picture '[[File: http://sm.axmasoft.com/rsc/back.jpg]]'>>",Cell,Cell,3,false));
}
if (compact)
{
LocationArr[1].Text = '';
if (TargetFormat=='html')
{
LocationArr[3].Text = '';
LocationArr[4].Text = '';
}
else
{
LocationArr[3].Text = '';
}
}
if (pixel_ratio>1)
{
for (var key in LocationArr)
{
LocationArr[key].Left = LocationArr[key].Left*pixel_ratio;
LocationArr[key].Top = LocationArr[key].Top*pixel_ratio;
}
}
ParseAll();
UpdateFooter();
if (LocSize<=LocSizeSmall || compact)
{
ArrangeByName();
}
else
{
SelectNPaint();
}
fileNum = 0;
if (EditorMode)
{
EditrReload();
}
CanvasToTop();
}
function NewGameClipboard()
{
var str = ''
str += "<textarea id='PasteClipboard' placeholder='"+Lang_MenuNewClipboardMess+".' style='width:95%;height:130px;outline:none;resize:none;font-size:11px;padding:4px;border:none;border-radius:3px;'></textarea>";
str += "<div class='menu_item' id='item1'>OK</div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){newGameClipboard();ShowWind(false);});
ShowWind(true);
$('PasteClipboard').focus();
document.body.oncontextmenu = function() {if(getFocusedId()=='PasteClipboard') {return true;} else {return false;}}
}
function newGameClipboard()
{
var data = $('PasteClipboard').value;
if (data.trim()!='')
{
LoadStory(0,false,data);
ArrangeByName();
}
}
function UpdateFooter()
{
if (EditorMode)
{
var n = (GetEditorText().split(EditrTitlesRegExp).length-1)/2;
footer_status.innerHTML = Lang_Passages+': '+ n;
}
else
{
if (!EditorVisible)
{
if (!isMouseDown)
{
var errors = 0;
for (var key in LocationArr)
{
if (LocationArr[key].ErrorLocation)
{
errors++;
}
}
footer_status.innerHTML = Lang_Passages+': '+LocationArr.length+', '+Lang_PassagesErrors+': '+errors;
}
}
else
{
footer_status.innerHTML = Lang_Passages+': '+LocationArr.length;
}
}
}
function ClearCanvas()
{
maincanvas.width = maincanvas.width;
ctx.fillStyle = '#34495e';
ctx.fillRect(0,0,maincanvas_w,maincanvas_h);
}
function ClearBuffer()
{
buffer.width = buffer.width;
}
function FromBuffer()
{
ctx.drawImage(buffer,0,0);
}
function ResizeEvent()
{
if (!PreviewVisible)
{
toolbarH = toolbar.offsetHeight;
canvasWrap_w = window.innerWidth;
canvasWrap_h = (window.innerHeight-toolbarH-footer.offsetHeight);
if (isTouchDevice)
{
ResizeCanvasDPI(canvasWrap_w,canvasWrap_h-4);
}
else
{
canvasWrap.style.width = canvasWrap_w+"px";
canvasWrap.style.height = canvasWrap_h+"px";
}
maincanvas_w = maincanvas.width;
maincanvas_h = maincanvas.height;
buffer.width  = maincanvas_w;
buffer.height = maincanvas_h;
if (EditorVisible)
{
ResizeEditor();
}
else
{
ResizeCanvas();
}
}
}
function ResizeEditor()
{
var padding = 10;
var width = 850+padding*2;
if ((window.innerWidth-width)<padding*2)
{
width = window.innerWidth-padding*2;
}
editor.style.width = width+'px';
editor.style.left = (window.innerWidth-width-padding*2)/2+'px';
editorBlock.style.top = toolbarH+'px';
editorBlock.style.height = canvasWrap_h+'px';
var edH = editorBlock.offsetHeight-editorTop.offsetHeight-padding*2;
editor.style.height = edH-1+'px';
if (editorColor)
{
editorColor.style.top = editor.style.top;
editorColor.style.left = editor.style.left;
editorColor.style.width = editor.style.width;
editorColor.style.height = editor.style.height;
}
}
function ResizeCanvas()
{
maincanvas.style.top = toolbarH+'px';
if (maincanvas_w<MinimalWidth)
{
LocSize = LocSizeSmall;
show_lines.style.display = 'none';
Arrange();
}
else
{
show_lines.style.display = 'inline-block';
if (LocSize<=LocSizeSmall)
{
LocSize = LocSizeDefault;
Arrange();
}
}
SelectNPaint();
}
function GetLocByTitle(Title)
{
Title = Title.split('[::]')[0].trim().toLowerCase();
for (var key in LocationArr)
{
if (LocationArr[key].Title.toLowerCase().trim()==Title)
{
return LocationArr[key];
}
}
return false;
}
function GetLocIndexByTitle(Title)
{
Title = Title.split('[::]')[0].trim().toLowerCase();
for (var i=0;i<LocationArr.length;i++)
{
if (LocationArr[i].Title.toLowerCase().trim()==Title)
{
return i;
}
}
return -1;
}
function Paint(move)
{
GroupArr = {};
var Loc, coords;
for (var i=0;i<LocationArr.length;i++)
{
Loc = LocationArr[i];
coords = Loc.Left+':'+Loc.Top;
if (GroupArr[coords])
{
GroupArr[coords].push(i);
}
else
{
GroupArr[coords] = [i];
}
}
if (move)
{
var lastLocGroup = GetGroup(LastLocation().Left,LastLocation().Top).toString();
if (prevLastLocGroup != lastLocGroup)
{
if (prevLastLocGroup!='')
{
Message(Lang_Grouped);
}
prevLastLocGroup = lastLocGroup;
ClearBuffer();
DrawLocations(buffer.ctx);
}
ClearCanvas();
DrawLines(move);
}
else
{
ClearCanvas();
if (LocSize>LocSizeSmall && isShowLines)
{
DrawLines();
}
else
{
DrawLines(true);
}
ClearBuffer();
DrawLocations(buffer.ctx);
}
FromBuffer();
DrawLastLocation();
}
function DrawLocations(g)
{
if (LocationArr.length>0)
{
for (var i=0;i<LocationArr.length-1;i++)
{
DrawLocation(g,i);
}
}
}
function DrawLastLocation()
{
if (LocationArr.length>0)
{
DrawLocation(ctx,LocationArr.length-1);
DrawBorder(ctx,LocationArr.length-1,SelLocColor);
}
}
function GetGroup(x,y)
{
var ret = GroupArr[x+':'+y];
if (ret)
{
return ret;
}
else
{
return false;
}
}
function isGroup(Loc)
{
if (Loc)
{
if (GroupArr[Loc.Left+':'+Loc.Top].length>1)
{
return true;
}
else
{
return false;
}
}
else
{
return false;
}
}
function DrawLocation(g,i)
{
var locfontsize;
if (LocSize<=LocSizeSmall)
{
locfontsize = 9;
}
else
{
locfontsize = 10;
}
locfontsize = locfontsize * pixel_ratio;
var cl;
var Loc = LocationArr[i];
var LocGroup;
var GroupList = GetGroup(Loc.Left,Loc.Top);
g.font = locfontsize+'px sans-serif';
if (GroupList.length>1)
{
if (i==GroupList[GroupList.length-1])
{
g.fillStyle = '#FFFFFF';
g.fillRect(Loc.Left,Loc.Top,LocSize,LocSize);
var title;
var h = 14 * pixel_ratio;
var n = Math.floor((LocSize-h*2)/h);
var b = 0;
for (var j=GroupList.length-1;j>=0;j--)
{
LocGroup = LocationArr[GroupList[j]];
title = LocGroup.Title;
g.fillStyle = Colors[LocGroup.ColorNum];
lineText(g,title,Loc.Left+4,h+Loc.Top+b*h,LocSize-8*pixel_ratio,16*pixel_ratio,16*pixel_ratio);
if (b>=n)
{
break;
}
b++;
}
var s = GroupList.length;
var w = g.measureText(s).width;
var padding = 4 * pixel_ratio;
g.fillStyle = '#2980b9';
g.fillRect(Loc.Left+LocSize-(w+padding*2)-2*pixel_ratio,Loc.Top+LocSize-15*pixel_ratio,w+padding*2,14*pixel_ratio);
g.fillStyle = '#FFFFFF';
g.fillText(s,Loc.Left+LocSize-w-padding-2*pixel_ratio,Loc.Top+LocSize-padding-1*pixel_ratio,LocSize);
DrawBorder(g,i,LinesColor);
}
}
else
{
g.fillStyle = '#FFFFFF';
g.fillRect(Loc.Left,Loc.Top,LocSize,LocSize);
g.fillStyle = Colors[Loc.ColorNum];
if (LocSize<=LocSizeSmall)
{
g.fillRect(Loc.Left,Loc.Top,LocSize,LocSize);
}
else
{
g.fillRect(Loc.Left,Loc.Top,LocSize,LocTitleH);
}
g.fillStyle = '#FFFFFF';
if (LocSize>LocSizeSmall)
{
lineText(g,Loc.Title,Loc.Left+4*pixel_ratio,Loc.Top+14*pixel_ratio,LocSize-8*pixel_ratio,16*pixel_ratio,16*pixel_ratio);
}
else
{
lineText(g,Loc.Title.replace(/\s/g,'\n'),Loc.Left+4*pixel_ratio,Loc.Top+12*pixel_ratio,LocSize-8*pixel_ratio,LocSize-12*pixel_ratio,11*pixel_ratio)
}
if (LocSize>LocSizeSmall)
{
g.fillStyle = '#3a3a3a';
lineText(g,Loc.Text.trim(),Loc.Left+4*pixel_ratio,Loc.Top+31*pixel_ratio,LocSize-8*pixel_ratio,LocSize-34*pixel_ratio,12*pixel_ratio);
}
if (Loc.ErrorLocation)
{
g.globalAlpha = 0.25;
g.fillStyle = '#e74c3c';
g.fillRect(Loc.Left,Loc.Top,LocSize,LocSize);
}
g.globalAlpha = 1;
DrawBorder(g,i,LinesColor);
}
}
function DrawBorder(g,i,cl)
{
var Loc = LocationArr[i];
g.strokeStyle = cl;
g.lineWidth = 2;
g.beginPath();
g.rect(Loc.Left+1,Loc.Top+1,LocSize-2,LocSize-2);
g.stroke();
}
function DrawLines(move)
{
var cl, x1, y1, x2, y2;
ctx.globalAlpha = 1;
var coords = [];
var coord = '';
for (var i=0;i<LocationArr.length;i++)
{
Loc = LocationArr[i];
for (var j=0;j<Loc.Links.length;j++)
{
if (!move || (i==LocationArr.length-1 && !isGroup(Loc)) )
{
LocTarget = GetLocByTitle(Loc.Links[j]);
if ( LocTarget && !(Loc.Left==LocTarget.Left && Loc.Top==LocTarget.Top) )
{
x1 = LocTarget.Left+LocSize/2;
y1 = LocTarget.Top+LocSize/2;
x2 = Loc.Left+LocSize/2;
y2 = Loc.Top+LocSize/2;
coord = [x1,y1,x2,y2].join(';');
if (coords.indexOf(coord)==-1)
{
coords.push(coord);
if (i==LocationArr.length-1 && SelectedLocation!=-1 && !isGroup(Loc))
{
cl = SelLocColor;
}
else
{
cl = LinesColor;
}
ctx.strokeStyle = cl;
ctx.fillStyle = cl;
DrawOtr(x1,y1,x2,y2);
}
}
}
}
}
}
function lineText(context,text,marginLeft,marginTop,maxWidth,maxHeight,lineHeight)
{
var lines = text.split('\n');
var line_n = 0;
var maxLines = Math.floor(maxHeight/lineHeight);
var maxCharsInLine = Math.floor(maxWidth/context.measureText('e').width);
for (var l=0; l<lines.length; l++)
{
if (line_n>maxLines)
{
break;
}
var line = lines[l].trim();
if (line!='')
{
if (line.length>maxCharsInLine)
{
line = line.substr(0,maxCharsInLine-1)+'…';
}
context.fillText(line, marginLeft, marginTop, maxWidth);
marginTop += lineHeight;
line_n++;
}
}
}
function ArrangeButton()
{
var str = '';
if (!EditorVisible)
{
if (LocationArr.length>0)
{
str += "<div class='menu_item' id='item1'>"+Lang_MenuArrangeByTitle+"</div>";
str += "<div class='menu_item' id='item2'>"+Lang_MenuArrangeByColor+"</div>";
str += "<div class='menu_item' id='item3'>"+Lang_MenuArrangeBySize+"</div>";
}
str += "<div class='menu_item' id='item4'>"+Lang_SourceCode+"</div>";
str += "<div class='menu_item' id='item5'>"+Lang_ThemeEdit+"</div>";
if (LocationArr.length>0)
{
str += "<div class='menu_item' id='item6'>"+Lang_FilesList+"</div>";
if (maincanvas_w>=MinimalWidth)
{
str += "<div style='margin-top:10px;'><span class='button' id='item7' style='float:left;'>"+Lang_MenuArrangeZoomOut+"</span><span class='button' id='item8' style='float:right;'>"+Lang_MenuArrangeZoomIn+"</span></div><div style='clear:both;'></div>";
}
}
}
else
{
if (EditorMode)
{
str += "<div class='menu_item' id='item9' style='margin-bottom:10px;'>"+Lang_MenuArrangeByTitle+"</div>";
str += "<div class='menu_item' id='item10'>"+Lang_GraphicMode+"</div>";
}
else
{
str += "<div class='menu_item' id='item11'>"+Lang_SourceCode+"</div>";
}
str += "<div class='menu_item' id='item12'>"+Lang_SwitchTheme+"</div>";
if (editorColor)
{
var sc = Lang_OnColorMode;
if (ColorMode)
{
sc = Lang_OffColorMode;
}
str += "<div class='menu_item' id='item13'>"+sc+"</div>";
}
str += "<div style='margin-top:10px;'><span class='button' id='item14' style='float:left;'>"+Lang_MenuArrangeZoomOut+"</span><span class='button' id='item15' style='float:right;'>"+Lang_MenuArrangeZoomIn+"</span></div><div style='clear:both;'></div>";
}
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){ShowWind(false);Arrange(1);});
SetEvent('item2',function(){ShowWind(false);Arrange(2);});
SetEvent('item3',function(){ShowWind(false);Arrange(3);});
SetEvent('item4',function(){ShowWind(false);EditrShowHide();});
SetEvent('item5',function(){ShowWind(false);ThemeEditor();});
SetEvent('item6',function(){FileDataWind('all',false);});
SetEvent('item7',function(){ShowWind(false);ChangeZoom(false);ArrangeButton();});
SetEvent('item8',function(){ShowWind(false);ChangeZoom(true);ArrangeButton();});
SetEvent('item9',function(){AreYouSure('ArrangeEditor',Lang_ArrangeByTitle);});
SetEvent('item10',function(){ShowWind(false);EditrShowHide();});
SetEvent('item11',function(){ShowWind(false);EditrShowHide();});
SetEvent('item12',function(){ShowWind(false);DarkModeSwitch();});
SetEvent('item13',function(){ShowWind(false);ColorModeSwitch();});
SetEvent('item14',function(){FontSize(false);});
SetEvent('item15',function(){FontSize(true);});
ShowWind(true);
}
function ArrangeEditor()
{
EditrSave();
Arrange(1,true);
EditrReload();
}
function ArrangeByName()
{
Arrange(1);
}
function Arrange(SortType,nopaint)
{
var LocTitle = [];
var LocNum = [];
var LocNum2 = [];
var LocTextLen = [];
var N = LocationArr.length-1;
if (!SortType || SortType==0)
{
for (var i=0;i<=N;i++)
{
LocNum.push(i);
}
}
if (SortType==1 || SortType==2)
{
for (var i=0;i<=N;i++)
{
LocTitle.push(LocationArr[i].Title);
LocNum.push(i);
}
array_multisort(LocTitle,LocNum,'SORT_NUMERIC');
var LocTitle = [];
}
if (SortType==2)
{
for (var j=0;j<Colors.length;j++)
{
for (var i=0;i<=N;i++)
{
if (LocationArr[LocNum[i]].ColorNum==j)
{
LocNum2.push(LocNum[i]);
}
}
}
LocNum = LocNum2;
}
if (SortType==3)
{
for (var i=0;i<=N;i++)
{
LocTextLen.push(LocationArr[i].Text.length);
LocNum.push(i);
}
array_multisort(LocTextLen,LocNum,'SORT_NUMERIC');
LocTextLen = [];
LocNum.reverse();
}
var LocationArrSorted = [];
for (var i=0;i<LocNum.length;i++)
{
LocationArrSorted.push(LocationArr[LocNum[i]]);
}
LocationArr = LocationArrSorted;
var x = Cell;
var y = Cell;
var spacing  = LocSize + Cell*2;
var in_line  = Math.floor(canvasWrap_w*pixel_ratio/spacing);
var in_col   = Math.floor(maincanvas_h/spacing);
var in_group = Math.ceil(N/(in_line*in_col));
var n = 0;
var l = 0;
for (var i=0; i<=N; i++)
{
LocationArr[i].Left = x;
LocationArr[i].Top = y;
n++;
if (n>=in_group)
{
x = x + spacing;
l++;
n = 0;
}
if (l>=in_line)
{
x = Cell;
y = y + spacing;
l = 0;
}
}
if (!nopaint)
{
SelectNPaint();
}
if (!isTouchDevice)
{
canvasWrap.scrollLeft = 0;
canvasWrap.scrollTop = 0;
}
}
function SaveLocation(update)
{
var loctitle = editorTitle.value.trim();
if (loctitle=='')
{
return false;
}
if (NoTitle() || TitleExist(editorTitle.value))
{
Message(Lang_BadPassageTitle);
editorTitle.focus();
return false;
}
var loctext = GetEditorText();
loctext = loctext.replace(/[\r\n]+/g,"\n");
loctext = loctext.trim();
if (editNewLoc)
{
editNewLoc = false;
var x, y;
if (OverCanvas())
{
x = MouseX-LocSize/2;
y = MouseY-LocSize/2;
if (x<Cell) { x = Cell; }
if (y<Cell) { y = Cell; }
if (x+LocSize+Cell>maincanvas_w) { x = maincanvas_w-LocSize-Cell; }
if (y+LocSize+Cell>maincanvas_h) { y = maincanvas_h-LocSize-Cell; }
}
else if (LocationArr.length>0)
{
x = LastLocation().Left+Cell*2;
y = LastLocation().Top+Cell*2;
}
else
{
x = Cell;
y = Cell;
}
LocationArr.push(new Locations(loctitle,loctext,x,y,selectedColor));
}
else
{
LastLocation().Title = loctitle;
LastLocation().Text = loctext;
}
if (update)
{
ParseAll();
SetEditorText('');
ClearEditorColor();
editorTitle.value = '';
UpdateFooter();
Paint();
}
else
{
ParseLocation(LocationArr.length-1);
}
}
function NoTitle()
{
if ((editorTitle.value.trim()=='' && GetEditorText().trim()!='') || editorTitle.value.indexOf('"')!=-1)
{
return true;
}
else
{
return false;
}
}
function TitleExist(s)
{
if (s.indexOf('[::]')!=-1)
{
return true;
}
s = s.toLowerCase().trim();
var n;
if (editNewLoc)
{
n = LocationArr.length;
}
else
{
n = LocationArr.length-1;
}
for (i=0; i<n; i++)
{
if (LocationArr[i].Title.trim().toLowerCase()==s)
{
return true;
}
}
return false;
}
function CloseEditorButton()
{
if (!EditorMode)
{
HideBackButton();
}
OpenEditor(false);
}
function OpenEditor(show,forceOpen)
{
ShowWind(false);
if (show==false)
{
if (!EditorMode)
{
if (NoTitle() || TitleExist(editorTitle.value))
{
Message(Lang_BadPassageTitle);
editorTitle.focus();
return false;
}
$('navToolText').innerHTML = Lang_Navigator;
SaveLocation(true);
LocTextBak = '';
editorBlock.style.display = 'none';
editor_buttons.style.display = 'none';
ShowLinesButton();
EditorVisible = false;
ClearUndoBuffer();
VisualEventsStart();
SelectNPaint();
}
else
{
EditrShowHide();
}
editorTitle.blur();
editor.blur();
}
else
{
if (EditorVisible)
{
if (NoTitle() || (editNewLoc && TitleExist(editorTitle.value)))
{
editorTitle.focus();
return;
}
ResizeEditor();
SaveLocation(true);
AddLocation();
}
else
{
var Loc = LastLocation();
if (!forceOpen && ClickOnLoc && isGroup(Loc))
{
OpenGroup(Loc);
}
else
{
if (ForbiddenLocation(Loc.Title) && ClickOnLoc && LocationArr.length>0)
{
Message(Lang_EditNo);
return;
}
$('navToolText').innerHTML = Lang_FindInPassages;
VisualEventsStop();
editorBlock.style.display = 'block';
editor_buttons.style.display = 'inline';
show_lines.style.display = 'none';
EditorVisible = true;
ResizeEditor();
if (ClickOnLoc && LocationArr.length>0)
{
editNewLoc = false;
editorTitle.value = Loc.Title;
LocTextBak = NormalizeLineEndings(Loc.Text);
SetEditorText(LocTextBak);
SetColorButton(LastLocation().ColorNum);
if (!isTouchDevice)
{
Select(editor,0,0);
}
}
else
{
AddLocation();
}
}
}
}
UpdateFooter();
}
function NormalizeLineEndings(text)
{
return text.replace(/[\r\n]+/g,'\n\n').trim();
}
function OpenGroup(Loc)
{
var GroupList = GetGroup(Loc.Left,Loc.Top);
var title, color, error;
var str = "<div class='menu_no' id='item1'>"+Lang_SplitAll+"</div>";
var titles = [];
var colors = [];
var errors = [];
for (var key in GroupList)
{
title = LocationArr[GroupList[key]].Title.toLowerCase();
color = Colors[LocationArr[GroupList[key]].ColorNum];
error = LocationArr[GroupList[key]].ErrorLocation;
titles.push(title);
colors.push(color);
errors.push(error);
}
array_multisort(titles,colors,errors);
for (var key in titles)
{
title = titles[key];
color = colors[key];
error = errors[key];
str += NavMenuItem(title,color,error,true);
}
titles = [];
colors = [];
errors = [];
mywindow_inner.innerHTML = str;
SetEventsByAttr(1);
SetEventsByAttr(2,'forevent2');
SetEvent('item1',function(){ShowWind(false);SplitGroup();});
ShowWind(true);
}
function SplitGroup(title)
{
if (!title)
{
var Loc = LastLocation();
var GroupList = GetGroup(Loc.Left,Loc.Top);
var i = 0;
for (var key in GroupList)
{
Loc = LocationArr[GroupList[key]];
Loc.Left += Cell*2*i;
Loc.Top  += Cell*3*i;
i++;
}
}
else
{
var i = GetLocIndexByTitle(title);
var Loc = LocationArr[i];
var x = Loc.Left - Cell*2;
if (x<Cell)
{
x = Loc.Left + Cell*2;
}
Loc.Left = x;
var y = Loc.Top + Cell*3;
if (y>maincanvas_h-LocSize-Cell)
{
y = Loc.Top - Cell*3;
}
Loc.Top = y;
LocationArr.push(Loc);
LocationArr.splice(i,1);
}
SelectNPaint();
}
function AddLocation()
{
editNewLoc = true;
selectedColor = 0;
SetColorButton(selectedColor);
SetEditorText('');
editorTitle.value = '';
editorTitle.focus();
}
function NewLocation(title,menu)
{
if (menu)
{
if (!EditorVisible)
{
var s = "<div class='menu_item' id='item1'>"+Lang_PurePassage+"</div>";
if (TargetFormat=='html')
{
s += "<div class='menu_item' id='item2'>"+Lang_CreateStoryStyle+"</div>";
}
s += "<div class='menu_item' id='item3'>"+Lang_FileSound+"</div>";
if (TargetFormat=='html')
{
s += "<div class='menu_item' id='item4'>"+Lang_FileImage+"</div>";
}
mywindow_inner.innerHTML = s;
SetEvent('item1',function(){NewLocation();});
SetEvent('item2',function(){ShowWind(false);if(!CreateStoryStyle()){Message(Lang_StoryStyleAlready)};});
SetEvent('item3',function(){ShowWind(false);AddFileData('audio');});
SetEvent('item4',function(){ShowWind(false);AddFileData('image');});
ShowWind(true);
}
else
{
NewLocation();
}
return;
}
if (!EditorMode)
{
ClickOnLoc = false;
OpenEditor();
if (title)
{
editorTitle.value = title;
editor.focus();
}
}
else
{
SetEditorText(RTrim(GetEditorText()) + '\n\n:: ');
if (title)
{
SetEditorText(GetEditorText() + title + '\n\n');
}
ScrollToPos(editor,GetEditorText().length);
Select(editor,GetEditorText().length,0);
}
}
function RemoveLocation()
{
if (LocationArr.length>0)
{
var q;
if (EditorVisible)
{
q = Lang_AYSRemoveThisPassage;
}
else
{
var s = LastLocation().Title;
q = Lang_AYSRemovePassage+' \"'+s+'\"?';
}
AreYouSure('removeLocation',q);
}
}
function removeLocation()
{
if (EditorVisible)
{
if (editNewLoc)
{
editorBlock.style.display = 'none';
EditorVisible = false;
VisualEventsStart();
return;
}
else
{
editorTitle.value = '\n[ASMnoTitle]\r';
}
OpenEditor(false);
}
LocationArr.length--;
ParseAll();
SelectNPaint();
}
function VisualEventsStart()
{
if (isTouchDevice)
{
document.ontouchmove = TouchMove;
document.ontouchstart = MouseDown;
document.ontouchend = MouseUp;
}
else
{
if (TargetWindows)
{
canvasWrap.style.overflow = 'auto';
ResizeCanvasDPI(maincanvasMaxWidth,maincanvasMaxHeight);
canvasWrap.scrollLeft = canvasWrapScrollLeft;
canvasWrap.scrollTop = canvasWrapScrollTop;
maincanvas.style.display = 'block';
Paint();
}
}
document.onmousemove = MouseMove;
document.onmouseup = MouseUp;
document.onmousedown = function (e) {MouseDown(e);}
document.ondblclick = function() {if (OverCanvas()){OpenEditor();}}
document.body.oncontextmenu = function() {MouseUp(); ActionsMenu(); return false;}
}
function VisualEventsStop()
{
if (isTouchDevice)
{
document.ontouchmove = '';
document.ontouchstart = '';
document.ontouchend = '';
}
else
{
if (TargetWindows)
{
canvasWrapScrollLeft = canvasWrap.scrollLeft;
canvasWrapScrollTop = canvasWrap.scrollTop;
var visibleClip = maincanvas.getContext('2d').getImageData(canvasWrapScrollLeft,canvasWrapScrollTop,canvasWrap_w,canvasWrap_h);
canvasWrap.style.overflow = 'hidden';
ResizeCanvasDPI(canvasWrap_w,canvasWrap_h);
ctx.putImageData(visibleClip,0,0);
}
}
document.onmousedown = '';
document.onmouseup = '';
document.onmousemove = '';
document.ondblclick = '';
document.body.oncontextmenu = function() {if (EditorVisible && (getFocusedId()=='editor' || getFocusedId()=='editorTitle' || getFocusedId()=='findField' || getFocusedId()=='replaceField')) {return true;} else {return false;} }
}
function confirmExit()
{
return Lang_DontForgetSave;
}
function OverCanvas()
{
if (MouseY>=0 && MouseY<=maincanvas.height)
{
return true;
}
else
{
return false;
}
}
function MoveLocation()
{
if (OverCanvas())
{
if (isMouseDown && SelectedLocation!=-1)
{
maincanvas.style.cursor = 'move';
var x = MouseX-mouseOffsetX;
var y = MouseY-mouseOffsetY;
if (x<Cell) { x = Cell; }
if ((x+LocSize+Cell)>maincanvas_w) { x = maincanvas_w-LocSize-Cell; }
if (y<Cell) { y = Cell; }
if ((y+LocSize+Cell)>(maincanvas_h)) { y = maincanvas_h-LocSize-Cell; }
SetLocPosition(x,y);
Paint(true);
}
}
}
function SetLocPosition(x,y)
{
if (!x && !y)
{
return false;
}
var Loc = LastLocation();
if (!x)
{
x = Loc.Left;
}
if (!y)
{
y = Loc.Top;
}
x = Math.round(x / Cell) * Cell;
y = Math.round(y / Cell) * Cell;
var GroupList = GetGroup(Loc.Left,Loc.Top);
if (GroupList.length>1)
{
for (var i=0;i<GroupList.length;i++)
{
LocationArr[GroupList[i]].Left = x;
LocationArr[GroupList[i]].Top =  y;
}
}
else
{
Loc.Left = x;
Loc.Top =  y;
}
}
function MouseMove(event)
{
event = fixEvent(event);
MouseX = (event.pageX+canvasWrap.scrollLeft)*pixel_ratio;
MouseY = (event.pageY-toolbarH+canvasWrap.scrollTop)*pixel_ratio;
MoveLocation();
}
function TouchMove(event)
{
if (event.targetTouches.length==1)
{
var touch = event.targetTouches[0];
MouseX = (touch.pageX)*pixel_ratio;
MouseY = (touch.pageY-toolbarH)*pixel_ratio;
MoveLocation();
}
}
var mylatesttap = 0;
var myprevtapX = 0;
var myprevtapY = 0;
function DoubleTap(mylasttapX,mylasttapY)
{
var now = new Date().getTime();
var timesince = now - mylatesttap;
mylatesttap = now;
if ( timesince<=500 && timesince>0 && Math.abs(mylasttapX-myprevtapX)<22 && Math.abs(mylasttapY-myprevtapY)<22 )
{
isMouseDown = false;
myprevtapX = 0;
myprevtapY = 0;
return true;
}
else
{
myprevtapX = mylasttapX;
myprevtapY = mylasttapY;
return false;
}
}
function RightClick(e)
{
var isRightMB;
e = e || window.event;
if ("which" in e)
{
isRightMB = e.which == 3;
}
else if ("button" in e)
{
isRightMB = e.button == 2;
}
if (!isRightMB)
{
return false;
}
else
{
return true;
}
}
function MouseDown(e)
{
if (!RightClick(e))
{
isMouseDown = true;
}
if (isTouchDevice)
{
if (e.targetTouches.length==1 && OverCanvas() && DoubleTap(e.targetTouches[0].pageX,e.targetTouches[0].pageY))
{
OpenEditor();
return false;
}
TouchMove(event);
}
if (OverCanvas())
{
var i = 0;
var GroupList = [];
for (i=LocationArr.length-1; i>=0; i--)
{
Loc = LocationArr[i];
if (MouseX>=Loc.Left && MouseX<=(Loc.Left+LocSize) && MouseY>=Loc.Top && MouseY<=(Loc.Top+LocSize))
{
mouseOffsetX = MouseX - Loc.Left;
mouseOffsetY = MouseY - Loc.Top;
ClickOnLoc = true;
LocationArr.push(Loc);
LocationArr.splice(i,1);
SelectedLocation = LocationArr.length-1;
if (!isGroup(Loc))
{
footer_status.innerHTML = LastLocation().Title;
}
else
{
footer_status.innerHTML = Lang_PassagesGroup;
}
Paint(true);
break;
}
else
{
ClickOnLoc = false;
SelectedLocation = -1;
}
}
return false;
}
ClickOnLoc = false;
}
function MouseUp()
{
maincanvas.style.cursor = 'default';
isMouseDown = false;
if (OverCanvas())
{
SelectNPaint();
}
SelectedLocation = -1;
prevLastLocGroup = '';
UpdateFooter();
}
var confirm_func;
function AreYouSure(func,text)
{
if (!text) {
text = Lang_AreYouSure;
}
text += "<div style='margin-top:20px;'><span class='button' id='item1' style='float:left;'>"+Lang_NO+"</span><span class='button' id='item2' style='float:right;'>"+Lang_YES+"</span></div>";
mywindow_inner.innerHTML = text;
SetEvent('item1',function(){ShowWind(false);});
if (typeof func=='string') {
SetEvent('item2',function(){ShowWind(false);window[func]();});
} else {
SetEvent('item2',function(){ShowWind(false);func();});
}
ShowWind(true);
}
function AlertMessage(text,callback)
{
text += "<div style='margin-top:20px;'><span class='button' id='item1'>OK</span></div>";
mywindow_inner.innerHTML = text;
if (!callback) {
SetEvent('item1',function(){ShowWind(false);});
} else {
SetEvent('item1',function(){ShowWind(false);callback();});
}
ShowWind(true);
}
function keysPreventDefault(event)
{
if (WindowVisible)
{
var curtag = getFocusedTag();
if (curtag!='input' && curtag!='textarea')
{
event.preventDefault();
}
}
}
function keysPress(event)
{
if (!EditorVisible && !WindowVisible && !PreviewVisible && !PlateVisible)
{
if (!event.ctrlKey && !event.metaKey)
{
var k = GetKeyCode(event);
if ( (k>=48 && k<=57) || (k>=65 && k<63232) || k>63277 || k==60 || k==62 || k==36 )
{
event.preventDefault();
lastSearchQuery = String.fromCharCode(k);
Navigator(true);
}
}
}
}
function keysUp(event)
{
keysPreventDefault(event);
var k = GetKeyCode(event);
if (!PreviewVisible)
{
if (k==27)
{
if (WindowVisible)
{
ShowWind(false);
}
else if (PlateVisible)
{
ShowPlate(false);
}
else if (EditorVisible)
{
CloseEditorButton();
}
}
if (k==115)
{
event.preventDefault();
event.returnValue = false;
EditrShowHide();
}
}
else
{
if (k==27)
{
ClosePreview();
}
}
}
function keys(event)
{
keysPreventDefault(event);
if (PlateVisible && !WindowVisible)
{
event.preventDefault();
event.cancelBubble = true;
event.returnValue = false;
return false;
}
if (!PreviewVisible)
{
var k = GetKeyCode(event);
if (k==27)
{
event.preventDefault();
event.cancelBubble = true;
event.returnValue = false;
return false;
}
if (!EditorVisible && !WindowVisible && k==32)
{
event.preventDefault();
ClickOnLoc = true;
OpenEditor(true);
}
if (!EditorVisible && !WindowVisible  && (k==46 || k==8))
{
if (!isGroup(LastLocation()))
{
RemoveLocation();
}
else
{
Message(Lang_UnableRemoveGroup);
}
}
if (k==13)
{
if (getFocusedId()=='editorTitle')
{
RestoreCaret();
event.preventDefault();
}
else if (WindowVisible && MenuItemsList.length>0)
{
MenuItemsList[SelectedMenuItemNum].dispatchEvent(new CustomEvent('click'));
event.preventDefault();
}
}
if (k==9)
{
event.preventDefault();
event.returnValue = false;
if (WindowVisible)
{
MenuItemNext();
}
else if (!EditorVisible)
{
if (event.shiftKey)
{
if (LocationArr.length>0)
{
var i = 0;
LocationArr.push(LocationArr[i]);
LocationArr.splice(i,1);
SelectNPaint();
}
}
else
{
Navigator();
}
}
else if (EditorVisible && getFocusedId()=='editorTitle')
{
RestoreCaret();
}
else if (EditorVisible && getFocusedId()=='editor')
{
var selData = GetSelection(editor);
var currentLen = selData['end']-selData['start'];
if (currentLen>0)
{
FormatText();
}
else
{
AutoDop(event);
}
}
}
if (k==40)
{
if (WindowVisible)
{
event.preventDefault();
event.returnValue = false;
MenuItemNext();
}
}
if (k==38)
{
if (WindowVisible)
{
event.preventDefault();
event.returnValue = false;
MenuItemPrev();
}
}
if (k==112)
{
event.preventDefault();
event.returnValue = false;
Help();
}
if ((event.ctrlKey || event.metaKey) && k==83)
{
event.preventDefault();
event.returnValue = false;
SaveStoryButton();
}
if ((event.ctrlKey || event.metaKey) && k==79)
{
event.preventDefault();
event.returnValue = false;
SelectStory();
}
if ((event.ctrlKey || event.metaKey) && k==78)
{
event.preventDefault();
event.returnValue = false;
NewGameButton();
}
if ((event.ctrlKey || event.metaKey) && k==77)
{
if (EditorVisible && getFocusedId()=='editor')
{
event.preventDefault();
event.returnValue = false;
InsertMacro();
}
}
if ((event.ctrlKey || event.metaKey) && k==73)
{
if (EditorVisible)
{
event.preventDefault();
event.returnValue = false;
InsertPicture();
}
}
if ((event.ctrlKey || event.metaKey) && k==66)
{
event.preventDefault();
event.returnValue = false;
Publish();
}
if ((event.ctrlKey || event.metaKey) && k==65 && !EditorVisible)
{
event.preventDefault();
event.returnValue = false;
ArrangeButton();
}
if (!event.shiftKey && (((event.ctrlKey || event.metaKey) && k==82) || k==116))
{
event.preventDefault();
event.returnValue = false;
if (!EditorVisible)
{
CompileButton();
}
else
{
CompilePassage();
}
}
if (EditorVisible && (((event.ctrlKey || event.metaKey) && event.shiftKey && k==82) || event.shiftKey && k==116))
{
event.preventDefault();
event.returnValue = false;
CompileButton();
}
if ((event.ctrlKey || event.metaKey) && k==80)
{
event.preventDefault();
event.returnValue = false;
NewLocation();
}
if ((event.ctrlKey || event.metaKey) && k==75)
{
if (EditorVisible && getFocusedId()=='editor')
{
event.preventDefault();
event.returnValue = false;
CommentText();
}
}
if ((event.ctrlKey || event.metaKey) && k==70)
{
if (!EditorVisible)
{
event.preventDefault();
event.returnValue = false;
Navigator(true);
}
else
{
event.preventDefault();
event.returnValue = false;
FindNReplace();
}
}
if (((event.ctrlKey || event.metaKey) && k==71) || k==114)
{
if (EditorVisible)
{
event.preventDefault();
event.returnValue = false;
EditorFindNext();
}
}
if ((event.ctrlKey || event.metaKey) && k==72)
{
if (EditorVisible)
{
event.preventDefault();
event.returnValue = false;
EditorReplaceNext();
}
}
if ((event.ctrlKey || event.metaKey) && k==76)
{
if (EditorVisible)
{
event.preventDefault();
event.returnValue = false;
EditrGotoLink();
}
}
if ((event.ctrlKey || event.metaKey) && k==68)
{
event.preventDefault();
event.returnValue = false;
if (EditorVisible)
{
AddText(3);
}
else
{
DuplicateLocation();
}
}
if ((event.ctrlKey || event.metaKey) && k==90)
{
if (!EditorVisible)
{
event.preventDefault();
event.returnValue = false;
}
else
{
if (UndoBuffer.length>0)
{
event.preventDefault();
event.returnValue = false;
FromUndoBuffer();
ClearUndoBuffer();
}
}
}
if ((event.ctrlKey || event.metaKey) && k==221)
{
if (EditorVisible && getFocusedId()=='editor')
{
event.preventDefault();
event.returnValue = false;
AddText(1);
}
}
if ((event.ctrlKey || event.metaKey) && k==219)
{
if (EditorVisible && getFocusedId()=='editor')
{
event.preventDefault();
event.returnValue = false;
AddText(2);
}
}
if ((event.ctrlKey || event.metaKey) && (k==187 || k==107))
{
event.preventDefault();
event.returnValue = false;
if (EditorVisible)
{
FontSize(true);
}
else
{
if (maincanvas_w>=MinimalWidth)
{
ChangeZoom(true);
}
}
}
if ((event.ctrlKey || event.metaKey) && (k==189 || k==109))
{
event.preventDefault();
event.returnValue = false;
if (EditorVisible)
{
FontSize(false);
}
else
{
ChangeZoom(false);
}
}
if (event.shiftKey && k==13)
{
if (EditorVisible)
{
event.preventDefault();
event.returnValue = false;
InsertText('<<br>>',true);
}
}
if ((event.ctrlKey || event.metaKey) && k==74)
{
if (TargetFormat=='audio' && !EditorVisible)
{
event.preventDefault();
event.returnValue = false;
AreYouSure('PublishToFile');
}
}
}
}
function SelectNPaint(move)
{
SelectedLocation = LocationArr.length-1;
Paint(move);
SelectedLocation = -1;
}
function CompileButton()
{
Message(Lang_PleaseWait,true);
setTimeout (function() { Compile(true,-1); }, 1);
}
function CompilePassage()
{
if (!EditorMode)
{
if (NoTitle() || TitleExist(editorTitle.value))
{
Message(Lang_BadPassageTitle);
editorTitle.focus();
return false;
}
Compile(true,LocationArr.length-1);
}
else
{
var ret = GetCurrPassage();
var Title = ret[3];
if (Title!='')
{
EditrSave();
var LocIndex = GetLocIndexByTitle(Title);
if (LocIndex!=-1)
{
Compile(true,LocIndex);
return;
}
}
Message(Lang_NoPassage);
}
}
function Help(item)
{
if (item==1)
{
var helpWin = WindowOpen('../profile.php');
}
else if (item==2)
{
var helpWin = WindowOpen('http://sm.axmasoft.com/'+LangCode+'/help.html');
}
else if (item==3)
{
var helpWin = WindowOpen('../lib.php');
}
else if (item==4)
{
var helpWin = WindowOpen('http://sm.axmasoft.com/download.php');
}
else if (item==5)
{
QuickStart();
}
else
{
var str = '';
str += "<div class='menu_item' id='item1'>"+Lang_MenuQuickStart+"</div>";
str += "<div class='menu_item' id='item2'>"+Lang_MenuUserManual+"</div>";
str += "<div class='menu_item' id='item3'>"+Lang_MenuProfile+"</div>";
str += "<div class='menu_item' id='item4'>"+Lang_MenuToLibrary+"</div>";
if (TargetBrowser) {
str += "<div class='menu_item' id='item5'>"+Lang_MenuDesktopVersion+"</div>";
} else {
str += "<div class='menu_item' id='item6'>"+Lang_ChangeLang+"</div>";
}
if (TargetChromeApp) {
str += "<div style='margin-top:16px;'>"+Lang_Version+" "+chrome.runtime.getManifest().version+"</div>";
}
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){ShowWind(false);Help(5);});
SetEvent('item2',function(){ShowWind(false);Help(2);});
SetEvent('item3',function(){ShowWind(false);Help(1);});
SetEvent('item4',function(){ShowWind(false);Help(3);});
SetEvent('item5',function(){ShowWind(false);Help(4);});
if (TargetChromeApp) {
SetEvent('item6',function() {
AreYouSure('chrome_ChangeLanguage',Lang_AppWillBeRestarted);
});
}
else if (TargetDesktop) {
SetEvent('item6',function(){ShowWind(false);FromJavascript('ChangeLanguage');});
}
ShowWind(true);
}
}
var messageTimeOut;
function Message(text,nohide)
{
text = text.toString();
text = text.trim();
if (text!='')
{
clearTimeout(messageTimeOut);
message.innerHTML = text;
message.style.display = 'block';
if (!nohide) {
messageTimeOut = setTimeout(MessageHide, 1500);
}
}
}
function MessageHide()
{
message.style.display = 'none';
}
function Account()
{
var profileWin = WindowOpen('../profile.php');
}
function SaveStoryButton(dialog)
{
if (!TargetDesktop && fileNum==0)
{
AreYouSure('SaveStory',Lang_AYSSaveNewStory);
}
else
{
SaveStory(dialog);
}
}
function SaveStory(dialog,cloudexport)
{
if (!TargetDesktop)
{
ToBackup();
}
if (EditorMode)
{
EditrSave();
}
else if (EditorVisible)
{
SaveLocation(false);
}
var Data = CreateSaveData();
if (TargetDesktop)
{
if (!cloudexport)
{
var com = 'SaveStory';
if (dialog)
{
com = 'SaveStoryDialog';
}
if (!TargetChromeApp) {
FromJavascript(com+'::'+Data);
} else {
chrome_Save(com,Data);
}
return;
}
else
{
fileNum = 0;
}
}
if (navigator.onLine)
{
Message(Lang_Saving,true);
saveData(CommandServer('com=save&value='+fileNum),Data,function(response) {
Data = '';
if (response=='register') {
Message(Lang_NeedSignIn);
SignInWindow(function(){
SaveStory(dialog,cloudexport);
});
} else if (response!='') {
fileNum = parseInt(response,10);
SetPrefValue('fileNum',fileNum);
Message(Lang_Saved);
}
});
}
else
{
Message(Lang_NeedOnline);
}
}
function CreateSaveData(woInfo)
{
var Data = '';
var Separator = '[::]';
if (!woInfo)
{
Data = LocSize/pixel_ratio+Separator+'0'+Separator+'0'+Separator+style_current_theme+Separator+style_view+Separator+TargetFormat+'\n';
}
for (var key in LocationArr)
{
var Location = LocationArr[key];
Data = Data + ":: " + Location.Title;
if (!woInfo)
{
Data = Data + Separator + Location.Left/pixel_ratio + "-" + Location.Top/pixel_ratio + "-" + Location.ColorNum;
}
Data = Data + '\n\n';
var LocText = Location.Text.replace(/[\r\n]/g,'\n\n');
Data = Data + RTrim(LocText) + '\n\n';
}
return Data;
}
function LoadStory(fn,merge,Data)
{
if (EditorVisible && !EditorMode) {
OpenEditor(false);
}
var parseFile = function(fn,merge,Data) {
if (Data!='') {
if (!merge) {
fileNum = fn;
SetPrefValue('fileNum',fileNum);
LocationArr.length = 0;
}
var Separator = '[::]';
Data = Data.replace(/[\r\n]/g,'\n');
var DataArr = Data.split('\n');
for (var i=0; i<DataArr.length; i++) {
if (i==0) {
var PrefsArr = DataArr[0].trim().split(Separator);
if (PrefsArr.length>1 && !isNaN(+PrefsArr[0])) {
if (!merge) {
LocSize = parseInt(PrefsArr[0],10)*pixel_ratio;
if (LocSize<LocSizeMin) {
LocSize = LocSizeMin;
}
if (LocSize>LocSizeMax) {
LocSize = LocSizeMax;
}
if (!LocSize) {
LocSize = LocSizeDefault;
}
style_current_theme = parseInt(PrefsArr[3],10);
if (isNaN(+style_current_theme) || style_current_theme<0 || style_current_theme>=style_themes.length) {
style_current_theme = 0;
}
if (PrefsArr[4]=='true') {
style_view = 'sw';
} else if (PrefsArr[4]=='false') {
style_view = 'mw';
} else {
style_view = PrefsArr[4];
}
if (PrefsArr[5]=='audio') {
TargetFormat = 'audio';
} else {
TargetFormat = 'html';
}
}
continue;
}
}
var line = DataArr[i];
if (line.trim()!='') {
if (line.trim().substr(0,2)=="::") {
var TitleArr = line.trim().split(Separator);
if (TitleArr.length<2) {
TitleArr.push('0-0-0');
}
var Title = TitleArr[0].substr(2).trim();
TitleArr = TitleArr[1].trim().split('-');
if (merge && GetLocByTitle(Title)) {
Title = Title+' 2';
while (GetLocByTitle(Title)) {
Title = Title+' 2';
}
}
var x, y, color;
x = TitleArr[0];
if (!isNaN(+x)) {
x = parseInt(x,10)*pixel_ratio;
} else {
x = Cell;
}
y = TitleArr[1];
if (!isNaN(+y)) {
y = parseInt(y,10)*pixel_ratio;
} else {
y = Cell;
}
color = TitleArr[2];
if (!isNaN(+color)) {
color = parseInt(color,10);
} else {
color = 0;
}
LocationArr.push(new Locations(Title,'',x,y,color,false,[]));
} else {
if (LocationArr.length>0) {
line = RTrim(line);
LastLocation().Text = LastLocation().Text+line+'\n';
}
}
}
}
if (LocationArr.length<1) {
CreateDefaultPassage(Data);
} else {
Message(Lang_Loaded);
}
Data = '';
if (EditorMode) {
EditrReload();
} else {
ParseAll();
if (merge) {
ArrangeByName();
}
UpdateFooter();
ResizeCanvas();
}
CanvasToTop();
return true;
} else {
return false;
}
}
if (!Data) {
if (navigator.onLine) {
if (fn!='Inventory') {
SelectedLocation = -1;
Message(Lang_Loading,true);
loadData(CommandServer('com=load&value='+fn),function(Data) {
if (TargetChromeApp && !merge) {
chrome_SetCurrentEntry(null);
}
Data = Data.trim();
parseFile(fn,merge,Data);
if (GetURLHashVar('Run') && !merge) {
CompileButton();
}
});
} else {
Message(Lang_Loading,true);
loadData('http://hyperbook.ru/editor/Inventory-3_ru.SM',function(Data) {
Data = Data.trim();
merge = true;
parseFile(fn,merge,Data);
});
}
if (TargetDesktop) {
FromJavascript('NewGame');
}
} else {
Message(Lang_NeedOnline);
}
return;
}
parseFile(fn,merge,Data);
}
function CreateDefaultPassage(text)
{
LocSize = LocSizeDefault;
LocationArr.push(new Locations("Start",text,Cell,Cell,3,false));
}
function SelectStory(menu,cloudimport)
{
if (TargetChromeApp) {
if (!cloudimport) {
chrome_SelectFile(true);
return;
}
} else if (TargetDesktop) {
if (!cloudimport) {
FromJavascript('SelectStory');
return;
}
}
var str = '';
if (menu) {
if (navigator.onLine) {
str+= "<div class='menu_item' id='item1'>"+Lang_MenuSelectStory+"</div>";
str+= "<div class='menu_item' id='item2'>"+Lang_MenuMergeStory+"</div>";
if (LangCode=='ru') {
str+= "<div class='menu_item' id='item3'>"+Lang_MenuAddInventory+"</div>";
}
}
if (isLocalStorageAvailable) {
var backup = localStorage.getItem("backup");
if (backup!=null && backup!='') {
backup = '';
str+= "<div class='menu_item' id='item4'>"+Lang_MenuFromBackUp+"</div>";
}
}
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){LoadMerge=false;SelectStory();});
SetEvent('item2',function(){LoadMerge=true;SelectStory();});
SetEvent('item3',function(){AreYouSure('AddInventory');});
SetEvent('item4',function(){ShowWind(false);FromBackup();});
ShowWind(true);
} else {
if (navigator.onLine) {
loadData(CommandServer('com=filelist'),function(Data) {
Data = Data.trim();
if (Data=='') {
Message(Lang_NoSavedFiles);
} else if (Data=='register') {
SignInWindow(function(){SelectStory(menu,cloudimport);});
} else {
var DataArr = [];
DataArr = Data.split('\n');
for (var i=0; i<DataArr.length; i++) {
var DataLineArr = [];
DataLineArr = DataArr[i].split('\t');
var fn = parseInt(DataLineArr[0],10);
str+= "<div class='menu_item' forevent='"+fn+"'>"+DataLineArr[1]+'</div>';
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(13);
ShowWind(true);
}
});
} else {
Message(Lang_NeedOnline);
}
}
}
function AddInventory()
{
LoadStory('Inventory');
}
function ShowWind(show)
{
if (show)
{
if (!EditorVisible)
{
VisualEventsStop();
}
else
{
RestoreCaret();
}
mywindow.style.display = 'block';
CreateMenuItemsList();
SelectMenuItem(-1,true);
WindowVisible = true;
SelectedLocation = -1;
maincanvas.style.cursor = 'default';
mywindow_inner.scrollTop = 0;
}
else
{
if (mywindow.style.display!='none')
{
MenuItemsList  = [];
mywindow_inner.innerHTML = '';
mywindow.style.display = 'none';
WindowVisible = false;
if (!EditorVisible && !PlateVisible)
{
VisualEventsStart();
}
else
{
if (!isTouchDevice)
{
RestoreCaret();
}
}
}
}
}
function Publish()
{
if (navigator.onLine)
{
var str = '';
if (TargetFormat=='html')
{
str += "<div class='menu_item' id='item1'>"+Lang_MenuPublishFile+"</div>";
str += "<div class='menu_item' id='item2'>"+Lang_MenuPublishInet+"</div>";
}
if (TargetDesktop)
{
str += "<div class='menu_item' id='item3'>"+Lang_SaveToCloud+"</div>";
}
str += "<div class='menu_item' id='item4'>"+Lang_SignIn+"</div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){AreYouSure('PublishToFile');});
SetEvent('item2',function(){AreYouSure('PublishToInet',Lang_MenuPublishInetSure);});
SetEvent('item3',function(){AreYouSure('SaveToCloud');});
SetEvent('item4',function(){SignInWindow();});
ShowWind(true);
}
else
{
Message(Lang_NeedOnline);
}
}
function SaveToCloud()
{
ShowWind(false);
SaveStory(false,true);
}
function PublishToFile()
{
if (AuthLogin=='' || AuthPassword=='')
{
SignInWindow(function(){PublishToFile();});
return;
}
Compile(false,-1);
}
function PublishToInet()
{
Message(Lang_PleaseWait,true);
setTimeout (function() { Compile(false,-1,true); }, 1);
}
function EditLocation(LocTitle)
{
LocTitle = LocTitle.toLowerCase();
var i = 0;
for (i=LocationArr.length-1; i>=0; i--)
{
Loc = LocationArr[i];
if (Loc.Title.toLowerCase()==LocTitle)
{
ClickOnLoc = true;
LocationArr.push(Loc);
LocationArr.splice(i,1);
SelectedLocation = -1;
OpenEditor(true,true);
break;
}
}
}
function Navigator(find)
{
if (!EditorMode)
{
if (LocationArr.length>0)
{
if (EditorVisible)
{
FindNReplace();
return;
}
var query = '';
if (find && lastSearchQuery)
{
query = lastSearchQuery;
}
var str = "<input type='search' class='search' id='searchNav' placeholder='"+Lang_FindInPassages+"' value='"+query+"' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' style='margin-bottom:4px;'><div id='resultNav'></div>";
mywindow_inner.innerHTML = str;
SetEvent('searchNav',function(){NavigatorSearch(event,this.value);},'keyup');
SetEvent('searchNav',function(){NavigatorSearch(event,this.value);},'click');
NavigatorSearch();
ShowWind(true);
}
else
{
Message(Lang_NoPassages);
}
document.body.oncontextmenu = function() {if(getFocusedId()=='searchNav') {return true;} else {return false;}}
if (find)
{
var queryfield = $('searchNav');
SelectText(queryfield,queryfield.value.length,0);
}
}
else
{
var LocNames = GetTitlesArray(true);
if (LocNames.length>0)
{
var title;
var str = '';
for (var key in LocNames)
{
title = LocNames[key];
str += "<div class='menu_item' forevent='"+title+"'>"+title+"</div>";
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(4);
ShowWind(true);
}
else
{
Message(Lang_NoPassages);
RestoreCaret();
}
}
}
function NavigatorSearch(event,query)
{
if (event)
{
var k = GetKeyCode(event);
if (k==40 || k==38 || k==9 || k==27 || k==13)
{
return;
}
}
if (!query)
{
query = '';
}
query = query.trim().toLowerCase();
lastSearchQuery = query;
var str = '';
var LocationArrResultTitle = [];
var LocationArrResultText = [];
var item;
for (i=0; i<LocationArr.length; i++)
{
item = [LocationArr[i].Title.toLowerCase(),LocationArr[i].ColorNum,LocationArr[i].ErrorLocation];
if (query!='')
{
if (LocationArr[i].Title.toLowerCase().indexOf(query)!=-1)
{
LocationArrResultTitle.push(item);
}
if (LocationArr[i].Text.toLowerCase().indexOf(query)!=-1)
{
LocationArrResultText.push(item);
}
}
else
{
LocationArrResultTitle.push(item);
}
}
LocationArrResultTitle.sort();
LocationArrResultText.sort();
var title, color, error;
var results = [LocationArrResultTitle,LocationArrResultText];
for (j=0; j<results.length; j++)
{
if (query && j==1 && LocationArrResultText.length>0)
{
str += "<div style='margin-top:10px;'>"+Lang_InText+"</div>";
}
var LocationArrResult = results[j];
for (i=0; i<LocationArrResult.length; i++)
{
title = LocationArrResult[i][0];
color = Colors[LocationArrResult[i][1]];
error = LocationArrResult[i][2];
str += NavMenuItem(title,color,error);
}
}
if (query!='' && LocationArrResultTitle.length==0 && LocationArrResultText.length==0)
{
str += '<p>'+Lang_NotFound2+'</p>';
}
LocationArrResultTitle.length = 0;
LocationArrResultText.length = 0;
var elem = $('resultNav');
elem.innerHTML = str;
SetEventsByAttr(1);
SetEventsByAttr(2,'forevent2');
CreateMenuItemsList(elem);
SelectMenuItem(-1,true);
}
function DuplicateLocation()
{
if (LocationArr.length>0)
{
var lastLoc = LastLocation();
var newTitle = lastLoc.Title+' 2';
while (TitleExist(newTitle))
{
newTitle = newTitle+' 2';
}
var newLeft = lastLoc.Left + Cell*2;
var newTop = lastLoc.Top + Cell*2;
if ((newLeft+LocSize+Cell)>maincanvas_w)
{
newLeft = lastLoc.Left - Cell*2;
}
if ((newTop+LocSize+Cell)>maincanvas_h)
{
newTop = lastLoc.Top - Cell*2;
}
LocationArr.push(new Locations(newTitle,lastLoc.Text,newLeft,newTop,lastLoc.ColorNum,false));
ParseAll();
SelectNPaint();
}
}
function ActionsMenu()
{
if (LocationArr.length>0)
{
if (!EditorVisible && isGroup(LastLocation()))
{
var Loc = LastLocation();
OpenGroup(Loc);
}
else
{
var str = '';
if (EditorVisible && !EditorMode)
{
str += "<div class='menu_item' id='item1'>"+Lang_MenuUndo+"</div>";
}
if (EditorVisible)
{
str += "<div class='menu_item' id='item2'>"+Lang_MenuFindNReplace+"</div>";
str += "<div class='menu_item' id='item3'>"+Lang_MenuGotoLink+"</div>";
}
if (!EditorVisible)
{
str += "<div class='menu_item' id='item4'>"+Lang_MenuEdit+"</div>";
}
if (!(editNewLoc && EditorVisible) && !EditorMode)
{
str += "<div class='menu_item' id='item5'>"+Lang_MenuDelete+"</div>";
}
if (!EditorVisible)
{
str += "<div class='menu_item' id='item6'>"+Lang_MenuDuplicate+"</div>";
str += "<div class='menu_item' id='item7'>"+Lang_MenuLinked+"</div>";
}
str += "<div class='menu_item' id='item8'>"+Lang_MenuTest+"</div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){ShowWind(false);AreYouSure('CancelChanges','"+Lang_AYSUndoChanges+"');});
SetEvent('item2',function(){ShowWind(false);FindNReplace();});
SetEvent('item3',function(){ShowWind(false);EditrGotoLink();});
SetEvent('item4',function(){ClickOnLoc=true;OpenEditor(true);ShowWind(false);});
SetEvent('item5',function(){RemoveLocation();});
SetEvent('item6',function(){DuplicateLocation();ShowWind(false);});
SetEvent('item7',function(){ShowWind(false);LinkedPassages();});
SetEvent('item8',function(){ShowWind(false);CompilePassage();});
ShowWind(true);
}
}
else
{
Message(Lang_NoPassages);
}
}
function LinkedPassages()
{
var str = '<b>'+Lang_MenuOutLinks+'</b><p>';
var tmpArr = [];
var link = '';
var color, error;
var loc = LastLocation();
var myloc;
for (i=0; i<loc.Links.length; i++)
{
link = loc.Links[i].trim().toLowerCase();
tmpArr.push(link);
}
tmpArr = ArrayUnique(tmpArr);
if (tmpArr.length>0)
{
for (i=0; i<tmpArr.length; i++)
{
link = tmpArr[i];
myloc = GetLocByTitle(link);
color = Colors[myloc.ColorNum];
error = myloc.ErrorLocation;
str += NavMenuItem(link,color,error);
}
}
else
{
str += Lang_No;
}
str += '<p><b>'+Lang_MenuInLinks+'</b></p>';
var myTitle = loc.Title.toLowerCase();
tmpArr = [];
for (i=0; i<LocationArr.length-1; i++)
{
loc = LocationArr[i];
for (j=0; j<loc.Links.length; j++)
{
link = loc.Links[j].trim().toLowerCase();
if (link==myTitle)
{
tmpArr.push(loc.Title.toLowerCase());
}
}
}
tmpArr = ArrayUnique(tmpArr);
if (tmpArr.length>0)
{
for (i=0; i<tmpArr.length; i++)
{
link = tmpArr[i];
myloc = GetLocByTitle(link);
color = Colors[myloc.ColorNum];
error = myloc.ErrorLocation;
str += NavMenuItem(link,color,error);
}
}
else
{
str += Lang_No;
}
tmpArr = [];
mywindow_inner.innerHTML = str;
SetEventsByAttr(1);
SetEventsByAttr(2,'forevent2');
ShowWind(true);
}
function ClosePreview()
{
closePreview.style.display = 'none';
if (!TargetChromeApp) {
previewWin.contentWindow.stopAllEvents();
previewWin.contentWindow.document.open();
previewWin.contentWindow.document.close();
} else {
chrome_SendMessage('');
}
previewWin.style.display = 'none';
PreviewVisible = false;
html = '';
mainDiv.style.display = 'block';
if (!EditorVisible)
{
VisualEventsStart();
}
else
{
if (!isTouchDevice)
{
RestoreCaret();
}
}
ResizeEvent();
}
function ReloadApp()
{
document.location.reload(true);
}
function CheckUpdates(vdate)
{
if (navigator.onLine) {
loadData(CommandServer('com=checkupdates&value='+vdate),function(answer) {
if (answer.trim()=='yes') {
AreYouSure('ReloadApp',Lang_UpdateFound+'<br>'+Lang_ReloadApp);
}
});
}
}
function CreateMenuItemsList(elem)
{
if (!isTouchDevice)
{
if (!elem)
{
elem = mywindow_inner;
}
MenuItemsList = getElementsByClass('menu_item button',elem);
SelectedMenuItemNum = 0;
}
}
function SelectMenuItem(sn,top)
{
if (MenuItemsList.length>0)
{
for (var key in MenuItemsList)
{
SetMenuItemStyle(MenuItemsList[key]);
}
if (sn!=-1)
{
var selitem = MenuItemsList[sn];
selitem.scrollIntoView(top);
}
else
{
var selitem = MenuItemsList[0];
}
SetMenuItemStyle(selitem,true);
if (sn==0)
{
mywindow_inner.scrollTop = 0;
}
}
}
function SetMenuItemStyle(item,select)
{
if (select)
{
item.style.backgroundColor = '#3498db';
item.style.color = '#FFFFFF';
}
else
{
item.style.backgroundColor = '#EEEEEE';
item.style.color = '#222222';
}
}
function MenuItemNext()
{
if (SelectedMenuItemNum<MenuItemsList.length-1)
{
SelectedMenuItemNum++;
}
else
{
SelectedMenuItemNum = 0;
}
SelectMenuItem(SelectedMenuItemNum,false);
}
function MenuItemPrev()
{
if (SelectedMenuItemNum>0)
{
SelectedMenuItemNum--;
}
else
{
SelectedMenuItemNum = MenuItemsList.length-1;
}
SelectMenuItem(SelectedMenuItemNum,true);
}
function ChangeZoom(increase)
{
if (LocationArr.length>0 && maincanvas_w>=MinimalWidth)
{
if (increase)
{
var NewSize = LocSize + Cell;
if (NewSize>LocSizeMax)
{
NewSize = LocSizeMax;
}
}
else
{
var NewSize = LocSize - Cell;
if (NewSize<LocSizeMin)
{
NewSize = LocSizeMin;
}
}
for (var key in LocationArr)
{
var Location = LocationArr[key];
Location.Left = Location.Left+(NewSize/LocSize-1)*Location.Left;
Location.Left = Math.round(Location.Left/Cell)*Cell;
Location.Top = Location.Top+(NewSize/LocSize-1)*Location.Top;
Location.Top = Math.round(Location.Top/Cell)*Cell;
}
LocSize = NewSize;
SelectNPaint();
}
}
function SetColorButton(n)
{
colorbutton.style.display = 'inline-block';
colorbutton.style.backgroundColor = Colors[n];
}
function SetColor(n)
{
selectedColor = n;
if (!editNewLoc)
{
LastLocation().ColorNum = n;
}
}
function SelectColor()
{
if (!EditorMode)
{
var str = '';
var sel;
var loccolor = 0;
if (!editNewLoc)
{
loccolor = LastLocation().ColorNum;
}
else
{
loccolor = selectedColor;
}
for (i=0; i<Colors.length; i++)
{
if (i==loccolor)
{
sel = 'border: 2px solid #222222;';
}
else
{
sel = 'padding: 2px;';
}
str += "<span class='color' forevent='"+i+"' style='"+sel+"background-color:"+Colors[i]+"'>"+"</span>";
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(3);
ShowWind(true);
}
else
{
Message(Lang_NotAvailInEditor);
}
}
function LastLocation()
{
if (LocationArr.length>0)
{
return LocationArr[LocationArr.length-1];
}
else
{
return false;
}
}
function CenterLocation(title)
{
var i = GetLocIndexByTitle(title);
var Loc = LocationArr[i];
if (isTouchDevice)
{
Loc.Left = Math.round((maincanvas_w-LocSize)/2 / Cell) * Cell;
Loc.Top =  Math.round((maincanvas_h-LocSize)/2 / Cell) * Cell;
}
else
{
canvasWrap.scrollLeft = Loc.Left+LocSize/2-canvasWrap_w/2;
canvasWrap.scrollTop = Loc.Top+LocSize/2-canvasWrap_h/2;
}
LocationArr.push(Loc);
LocationArr.splice(i,1);
SelectNPaint();
}
function ShowLines()
{
if (isShowLines)
{
isShowLines = false;
Message(Lang_LinesOff);
SetPrefValue('ShowLines','false');
}
else
{
isShowLines = true;
Message(Lang_LinesOn);
SetPrefValue('ShowLines','true');
}
SelectNPaint();
}
function NavMenuItem(title,color,error,group)
{
if (!group)
{
var com = 'CenterLocation';
var sym = '•';
}
else
{
var com = 'SplitGroup';
var sym = '×';
}
str =  "<div class='menu_no' forevent2='"+com+'\n'+title+"' style='width:3%;float:left;'>"+sym+"</div>";
str += "<div class='menu_item' forevent='"+title+"' style='width:80%;float:right;border-left:5px solid "+color+";border-right:5px solid "+color+";'>";
if (error)
{
str += "<span style='color:#e74c3c'>"+title+"</span>";
}
else
{
str += title;
}
str += "</div><div style='clear:both;'></div>";
return str;
}
function ShowLinesButton()
{
if (maincanvas_w>=MinimalWidth)
{
show_lines.style.display = 'inline-block';
}
}
function FromBackup()
{
if (TargetChromeApp) {
chrome_FromStorage('backup',function(data) {
if (data) {
chrome_SetCurrentEntry(null);
LoadStory(0,false,data);
} else {
Message(Lang_NoBackUp);
}
});
return;
}
if (!TargetDesktop) {
if (isLocalStorageAvailable) {
var data = localStorage.getItem('backup');
LoadStory(0,false,data);
}
}
}
function ToBackup()
{
if (!PreviewVisible) {
if (EditorMode) {
EditrSave();
} else if (EditorVisible) {
SaveLocation(false);
}
var data = CreateSaveData();
if (TargetChromeApp) {
chrome_ToStorage('backup',data);
return;
}
if (!TargetDesktop) {
if (isLocalStorageAvailable) {
localStorage.setItem('backup',data);
}
} else {
FromJavascript('SaveBackup::'+data);
}
}
}
function GetPrefValue(name,callback)
{
if (TargetBrowser)
{
return getCookie(name);
}
else if (TargetChromeApp)
{
chrome_FromStorage(name,callback);
}
else if (TargetDesktop)
{
FromJavascript('GetPrefValue::'+name);
}
else
{
if (isLocalStorageAvailable)
{
return localStorage.getItem(name);
}
}
}
function SetPrefValue(name,value)
{
if (TargetBrowser)
{
setCookie(name,value);
}
else if (TargetChromeApp)
{
chrome_ToStorage(name,value);
}
else if (TargetDesktop)
{
FromJavascript('SetPrefValue::'+name+'::'+value);
}
else
{
if (isLocalStorageAvailable)
{
localStorage.setItem(name,value);
}
}
}
function FromJavascript(com)
{
com = com.replace(/[\r\n]/g,'[[asmNewLine]]');
com = com.replace(/\t/g,'[[asmTabSymb]]');
var bak = document.title;
document.title = 'ASM_com::'+com;
document.title = bak;
}
function WindowOpen(s,name,wo)
{
var addr = '';
if (wo || s.substr(0,4)=='http')
{
addr = s;
}
else
{
addr = Lang_Server+'/'+s;
}
if (TargetChromeApp || !TargetDesktop)
{
if (!name)
{
name = '';
}
window.open(addr,name);
}
else
{
FromJavascript(addr);
}
}
function SignInWindow(callback)
{
if (TargetBrowser)
{
AuthLogin = GetPrefValue('login');
AuthPassword = GetPrefValue('password');
}
if (!AuthLogin) {AuthLogin = '';}
if (!AuthPassword) {AuthPassword = '';}
var str = Lang_SignIn+'<p></p>';
str += "<input id='auth_login' type='text' style='margin-bottom:14px;' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' placeholder='"+Lang_Login+"' value=\""+AuthLogin+"\"><br>";
str += "<input id='auth_password' type='password' style='margin-bottom:14px;' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false' placeholder='"+Lang_Password+"' value=\""+AuthPassword+"\">";
str += "<div class='button' id='item1'>OK</div>";
str += "<p class='plink' id='item2'>"+Lang_Register+"</p>";
mywindow_inner.innerHTML = str;
SetEvent('auth_login',function(){if (GetKeyCode(event)==9){$('auth_password').focus();}},'keydown');
SetEvent('auth_password',function(){if (GetKeyCode(event)==9){$('auth_login').focus();}},'keydown');
SetEvent('item1',function(){SignIn(callback);});
SetEvent('item2',function(){WindowOpen('login.php');});
ShowWind(true);
$('auth_login').focus();
document.body.oncontextmenu = function() {if(getFocusedId()=='auth_login' || getFocusedId()=='auth_password') {return true;} else {return false;}}
}
function SignIn(callback)
{
AuthLogin = $('auth_login').value.trim();
AuthPassword = $('auth_password').value.trim();
ShowWind(false);
if (TargetBrowser || TargetChromeApp)
{
SetPrefValue('login',AuthLogin);
SetPrefValue('password',AuthPassword);
}
else if (TargetDesktop)
{
FromJavascript('SignIn::'+AuthLogin+'\t'+AuthPassword);
}
else
{
SetPrefValue('AuthLogin',AuthLogin);
SetPrefValue('AuthPassword',AuthPassword);
}
if (callback)
{
callback();
}
}
function RestoreCaret()
{
if (!isTouchDevice)
{
var selData = getInputSelection(editor);
var currentPos = selData['start'];
var currentLen = selData['end']-selData['start'];
Select(editor,currentPos,currentLen);
}
}
function FontSize(more)
{
if (EditorVisible)
{
var step = 1;
if (more)
{
fontSize += step;
}
else
{
fontSize -= step;
}
SetFontEditor();
SetPrefValue('FontSize',fontSize+'');
}
}
function SetFontEditor()
{
var min = -3;
var max = 8;
if (!isNaN(+fontSize))
{
fontSize = parseInt(fontSize);
}
else
{
fontSize = 0;
}
if (fontSize>max)
{
fontSize = max;
}
if (fontSize<min)
{
fontSize = min;
}
editor.style.fontSize = 14+fontSize+'px';
if (editorColor)
{
editorColor.style.fontSize = editor.style.fontSize;
}
}
function ShowPlate(show)
{
ShowWind(false);
if (show)
{
myplate.style.display = 'block';
platecontent.scrollTop = 0;
PlateVisible = true;
if (!EditorVisible)
{
VisualEventsStop();
}
}
else
{
myplate.style.display = 'none';
platetitle.innerHTML = '';
platecontent.innerHTML = '';
PlateVisible = false;
if (!EditorVisible)
{
VisualEventsStart();
}
ResizeEvent();
}
}
function QuickStart(close)
{
if (!close)
{
platetitle.innerHTML = Lang_MenuQuickStart;
var s = Lang_QuickStart+"<p align='center'><span class='plink' id='item0'>"+Lang_Close+"</span></p>";
platecontent.innerHTML = s;
SetEvent('item0',function(){ShowPlate(false);});
SetEvent('item1',function(){WindowOpen('http://sm.axmasoft.com/ru/help.html');});
SetEvent('item2',function(){WindowOpen('http://hyperbook.ru/article.php');});
SetEvent('item3',function(){WindowOpen('http://hyperbook.ru/login.php');});
ShowPlate(true);
}
else
{
ShowPlate(false);
}
}
function CommandServer(com)
{
var comtext = 'command.php?login='+urlencode(AuthLogin)+'&password='+urlencode(AuthPassword)+'&'+com;
if (TargetBrowser)
{
return comtext;
}
else
{
return Lang_Server+'/editor/'+comtext;
}
}
function CanvasToTop()
{
canvasWrap.scrollLeft = 0;
canvasWrap.scrollTop = 0;
}
function ResizeCanvasDPI(w,h)
{
maincanvas.width = w * pixel_ratio;
maincanvas.height = h * pixel_ratio;
maincanvas.style.width = w + 'px';
maincanvas.style.height = h + 'px';
maincanvas.getContext('2d').setTransform(pixel_ratio,0,0,pixel_ratio,0,0);
}
function SetEventsByAttr(n,attr,event)
{
if (!attr) {
attr = 'forevent';
}
if (!event) {
event = 'click';
}
var elems = mywindow_inner.querySelectorAll('['+attr+']');
for (var i=0; i<elems.length; i++) {
var elem = elems[i];
var value = elem.getAttribute(attr);
elem.addEventListener(event,function(value){
return function() {
if (n==1) {
ShowWind(false);
EditLocation(value);
}
if (n==2) {
ShowWind(false);
value = value.split('\n');
if (value[0]=='CenterLocation') {
CenterLocation(value[1]);
}
if (value[0]=='SplitGroup') {
SplitGroup(value[1]);
}
}
if (n==3) {
ShowWind(false);
SetColorButton(value);
SetColor(value);
}
if (n==4) {
ShowWind(false);
EditrGotoLocation(value);
}
if (n==5) {
ShowWind(false);
OpenEditor(false);
EditLocation(value);
}
if (n==6) {
ShowWind(false);
EditrGotoLocation(value);
}
if (n==7) {
value = parseInt(value);
SelectTheme(value);
SelectedMenuItemNum = value;
}
if (n==8) {
ShowWind(false);
InsertMacro(value);
}
if (n==9) {
ShowWind(false);
InsertLink(value);
}
if (n==10) {
ShowWind(false);
InsertLink(value);
}
if (n==11) {
ShowWind(false);
FormatText(value);
}
if (n==12) {
ShowWind(false);
InsertText((value),true);
}
if (n==13) {
ShowWind(false);
LoadStory(value,LoadMerge);
}
if (n==14) {
event.stopPropagation();
}
if (n==15) {
ShowWind(false);
InsertText(value,true);
}
if (n==16) {
value = value.split('|');
if (value[3]=='true') {
value[3] = true;
} else {
value[3] = false;
}
EditFileData(value[0],value[1],value[2],value[3],value[4]); //(id,type,mytype,insert,dataurl)
}
if (n==17) {
ShowWind(false);
value = value.split('\t');
chrome_RestoreFile(value[0],value[1]);
}
}
}(value));
}
}
var noError;
var ErrorText;
var ErrorLocTitle;
function ErrorsCheck(MyText,MyTitle,TargetFormat)
{
var i, j, k, l;
var found;
var Patterns = [];
var Location;
var macros, bad, vari;
var myRe, myMatch;
i = 0;
for (var key in LocationArr)
{
if (LocationArr[key].Title.toLowerCase()==MyTitle.toLowerCase())
{
i++;
if (i>1)
{
ErrorsAddError(Lang_FoundDuplicates2+"[::]",MyTitle);
break;
}
}
}
Patterns = [];
Patterns.push(".*?(--).*?");
Patterns.push(".*?(&[0-9a-zA-Z#]+?;).*?");
Patterns.push(".*?(<[^<>]+?>).*?");
Patterns.push("\\$.*?");
found = false;
for (var i=0; i<Patterns.length; i++)
{
if (CompareRegEx(MyTitle,Patterns[i]))
{
found = true;
break;
}
}
if (found)
{
ErrorsAddError(Lang_Err_IncorrTitle+"[::]",MyTitle);
}
MyText = RemoveComments(MyText);
if (SimpleLocation(MyTitle))
{
Patterns = [];
Patterns.push(".*?<<.+?>>.*?");
found = false;
for (var i=0; i<Patterns.length; i++)
{
if (CompareRegEx(MyText,Patterns[i]))
{
found = true;
break;
}
}
if (found)
{
ErrorsAddError(Lang_Err_ForbExp+"[::]",MyTitle);
}
}
Patterns = [];
Patterns.push(".*?(<\\s*?script.*?>).*?");
Patterns.push(".*?(<\\s*?/\\s*?script\\s*?>).*?");
found = false;
for (var i=0; i<Patterns.length; i++)
{
if (CompareRegEx(MyText,Patterns[i]))
{
found = true;
break;
}
}
if (found)
{
ErrorsAddError(Lang_Err_ForbExp+"[::]",MyTitle);
}
i = CountFields(MyText,"<<")-1;
j = CountFields(MyText,">>")-1;
if (i<j)
{
ErrorsAddError(Lang_Err_AbsOpenMacro+"[::]<<",MyTitle);
}
if (i>j)
{
ErrorsAddError(Lang_Err_AbsClosMacro+"[::]>>",MyTitle);
}
Patterns = [];
Patterns.push("(set|remember)\\s+?\\$[^"+BadVarsSymbs+"]+?\\s*?(\\=|\\+|\\[)\\s*?.+?");
Patterns.push("(set|remember)\\s+?\\$[^"+BadVarsSymbs+"]+?\\.(push|unshift).+?");
Patterns.push("set\\s+?\\$\\$gender\\s*?\\=\\s*?[0-2]+?");
Patterns.push("(back|return|restart|else|endif|noaudio|br|silently|endsilently|endloop|break|continue|nop|endnop|tab)");
Patterns.push("(back|return|restart)\\s*?[\"'][^\"']*?[\"']");
Patterns.push("(if|elseif)\\s+?\\(.+?\\)");
Patterns.push("(if|elseif)\\s+?.+?");
Patterns.push("sound\\s*?[\"'].+?[\"']");
Patterns.push("(display|goto|sound)\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?\\[?.*?");
Patterns.push("(display|stop|goto)\\s+?[\"'][^\"']+?[\"']");
Patterns.push("repeat\\s+?[\"'][^\"']+?[\"']\\s*?[\\d\\.]+?");
Patterns.push("print\\s+?.+?");
Patterns.push("random\\s+?\\$[^"+BadVarsSymbs+"]+?\\s*?\\=\\s*?\\d+?");
Patterns.push("random\\s+?\\$[^"+BadVarsSymbs+"]+?\\s*?\\=\\s*?\\$[^"+BadVarsSymbs+"]+?");
Patterns.push("(loop|tab)\\s+?\\d+?");
Patterns.push("(loop|tab)\\s+?\\$[^"+BadVarsSymbs+"]+?[\\.|\\[]?.*?");
Patterns.push("picture\\s*?[\"']\\s*?\\[\\[\\s*?file:[^\"']+?\\.(png|jpg|jpeg|gif)+?\\s*?\\]\\]\\s*?[\"']");
Patterns.push("picture\\s*?[\"'][\"']");
if (TargetFormat=='html')
{
Patterns.push("endclass|clrscr");
Patterns.push("(title|image)\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?\\[?.*?");
Patterns.push("(title|image)\\s*?[\"'].*?[\"']");
Patterns.push("input\\s+?[\"'][^\"']+?[\"']\\s*?\\$[^"+BadVarsSymbs+"]+?");
Patterns.push("fade\\s*?\\=\\s*?\\d+?");
Patterns.push("style\\s+?[\"'][^\"']+?[\"'][\\s,]*?[\"'][^\"']+?[\"'][\\s,]*?[\"'][^\"']+?[\"']");
Patterns.push("class\\s+?[\"'][0-9a-zA-Z_\-]+?[\"']");
Patterns.push("class\\s+?\\$[^"+BadVarsSymbs+"]+?[\\.|\\[]?.*?");
Patterns.push("row\\s+?[\"'][^\"']*?[\"']\\s*?(noborder)?");
Patterns.push("row\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?(\\s+noborder)?");
Patterns.push("row\\s+?[\"'][^\"']*?[\"']\\s+?[\"'][^\"']*?[\"']\\s*?(noborder)?");
Patterns.push("row\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?(\\s+noborder)?");
Patterns.push("row\\s+?[\"'][^\"']*?[\"']\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?(\\s+noborder)?");
Patterns.push("row\\s+?\\${1,2}[^"+BadVarsSymbs+"]+?\\s+?[\"'][^\"']*?[\"']\\s*?(noborder)?");
Patterns.push("menu\\s*?[\"']\\s*?\\[\\[.+?\\]\\]\\s*?[\"']");
Patterns.push("menu\\s*?[\"']\\s*?[\"']");
Patterns.push("sprite\\s*?([\"'][^\"']+?[\"']|\\$[^"+BadVarsSymbs+"]+?)(\\s*?,\\s*?(\\+|-|@|\\d+(\\.\\d+)?|\\$[^"+BadVarsSymbs+"]+?)*?){0,7}");
Patterns.push("delete\\s*?([\"'][^\"']+?[\"']|\\$[^"+BadVarsSymbs+"]+?)?");
Patterns.push("choice\\s*?[\"'].+?[\"'](\\s*?\\${1,2}[^"+BadVarsSymbs+"]+?)?");
Patterns.push("filter\\s*?[\"'].*?[\"']");
}
myRe = /<<(.*?(?!<<)(?!>>).*?)>>/g;
while ((myMatch = myRe.exec(MyText)) != null)
{
found = false;
for (var i=0; i<Patterns.length; i++)
{
macros = myMatch[1].trim();
if (CompareRegEx(macros,Patterns[i]))
{
found = true;
break;
}
}
if (!found)
{
ErrorsAddError(Lang_Err_IncorrectMacro+"[::]"+myMatch[0],MyTitle);
}
else
{
ErrorsCheckVarsInMacro(macros,MyTitle,TargetFormat);
}
}
i = 0;
myRe = /<<\s*?class.+?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
i++;
}
j = 0;
myRe = /<<\s*?endclass\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
j++;
}
if (i!=j)
{
if (i<j)
{
ErrorsAddError(Lang_Err_ExcessMacro+"[::]<<endclass>>",MyTitle);
}
else
{
ErrorsAddError(Lang_Err_MissingMacro+"[::]<<endclass>>",MyTitle);
}
}
i = 0;
myRe = /<<\s*?nop\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
i++;
}
j = 0;
myRe = /<<\s*?endnop\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
j++;
}
if (i!=j)
{
if (i<j)
{
ErrorsAddError(Lang_Err_ExcessMacro+"[::]<<endnop>>",MyTitle);
}
else
{
ErrorsAddError(Lang_Err_MissingMacro+"[::]<<endnop>>",MyTitle);
}
}
i = 0;
myRe = /<<\s*?loop(.*?)>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
i++;
}
j = 0;
myRe = /<<\s*?endloop\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
j++;
}
if (i!=j)
{
if (i<j)
{
ErrorsAddError(Lang_Err_ExcessMacro+"[::]<<endloop>>",MyTitle);
}
else
{
ErrorsAddError(Lang_Err_MissingMacro+"[::]<<endloop>>",MyTitle);
}
}
i = 0;
myRe = /<<\s*?if(.*?)>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
i++;
if (myMatch[1].trim()=='' || CompareRegEx(myMatch[1].trim(),"\\(?\\s*\\)?"))
{
ErrorsAddError(Lang_Err_MissingCond+"[::]"+myMatch[0],MyTitle);
}
if (CompareRegEx(myMatch[1].trim(),".*?[^!<>=]\=[^=>].*?"))
{
ErrorsAddError(Lang_Err_OdnoRavno+"[::]"+myMatch[0],MyTitle);
}
}
k = 0;
myRe = /<<\s*?elseif(.*?)>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
k++;
if (myMatch[1].trim()=='' || CompareRegEx(myMatch[1].trim(),"\\(?\\s*\\)?"))
{
ErrorsAddError(Lang_Err_MissingCond+"[::]"+myMatch[0],MyTitle);
}
if (CompareRegEx(myMatch[1].trim(),".*?[^!<>=]=[^=>].*?"))
{
ErrorsAddError(Lang_Err_OdnoRavno+"[::]"+myMatch[0],MyTitle);
}
}
j = 0;
myRe = /<<\s*?endif\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
j++;
}
l = 0;
myRe = /<<\s*?else\s*?>>/gi;
while ((myMatch = myRe.exec(MyText)) != null)
{
l++;
}
if (i!=j)
{
if (i<j)
{
ErrorsAddError(Lang_Err_ExcessMacro+"[::]<<endif>>",MyTitle);
}
else
{
ErrorsAddError(Lang_Err_MissingMacro+"[::]<<endif>>",MyTitle);
}
}
if (i==0 && (k>0 || l>0))
{
ErrorsAddError(Lang_Err_MissingMacro+"[::]<<if>>",MyTitle);
}
myRe = /<<(.*?(?!<<)(?!>>).*?)>>/g;
while ((myMatch = myRe.exec(MyText)) != null)
{
if (InStr(myMatch[1],"$$")!=0)
{
if (InStr(myMatch[1],"$$title")==0 && InStr(myMatch[1],"$$from")==0 && InStr(myMatch[1],"$$time")==0 && InStr(myMatch[1],"$$gender")==0 && InStr(myMatch[1],"$$choice")==0)
{
ErrorsAddError(Lang_Err_UnknownSysVar+"[::]"+myMatch[0],MyTitle);
}
}
}
i = CountFields(MyText,"[[")-1;
j = CountFields(MyText,"]]")-1;
if (i<j)
{
ErrorsAddError(Lang_Err_AbsOpenLink+"[::][[",MyTitle);
}
if (i>j)
{
ErrorsAddError(Lang_Err_AbsClosLink+"[::]]]",MyTitle);
}
Patterns = [];
if (TargetFormat=='audio')
{
MyText = MyText.replace(/<<\s*?picture.*?>>/gi,'');
Patterns.push("[\\+\\*].*?");
Patterns.push(".*?(файл|file):.*?.(png|jpeg|jpg|png)\\s*?$");
}
Patterns.push("[\\+\\-\\*\\|]");
Patterns.push("[\\+\\-\\*]\\s*?\\|.*?");
Patterns.push(".*?\\|");
Patterns.push("\\|.*?");
myRe = /\[\[(.*?(?!\[\[)(?!\]\]).*?)\]\]/g;
while ((myMatch = myRe.exec(MyText)) != null)
{
found = false;
for (var i=0; i<Patterns.length; i++)
{
if (CompareRegEx(myMatch[1].trim(),Patterns[i]))
{
found = true;
break;
}
}
if (found)
{
ErrorsAddError(Lang_Err_IncorrectLink+"[::]"+myMatch[0],MyTitle);
}
}
if (TargetFormat=='audio')
{
MyText = MyText.replace(/<<\s*?(back|return|restart).*?>>/g,'[[link]]').replace(/\s/g,'');
var forbPass = ['storymenu','storystyle','storystyle.bak']
if (forbPass.indexOf(MyTitle.toLowerCase().trim())!=-1)
{
ErrorsAddError(Lang_Err_ForbPassage,MyTitle);
}
}
return noError
}
function ErrorsCheckVarsInMacro(macros,MyTitle,TargetFormat)
{
var bad = BadVarsSymbs.replace(".","");
var macrosSetPrint = false;
if (macros.search(/^(set|print)/i)!=-1)
{
macrosSetPrint = true;
}
macros = macros.replace(/^[a-zA-Z]+/,"");
macros = macros.replace(/["'][^"']*?["']/g,"");
if (TargetFormat=='audio' && macrosSetPrint && MyTitle.toLowerCase().substr(0,10)!='inventory_')
{
var myMatch = macros.match(/\.(replace|split|join|charAt|charCodeAt|substr|substring|slice)\s*?\(/);
if (myMatch != null)
{
ErrorsAddError(Lang_Err_ForbFunction+"[::]"+myMatch[1],MyTitle);
}
}
macros = macros.replace(/\.replace\s*?\(\s*?\/.+?\/[gim]*?\s*?,/g,"");
macros = " "+macros+" ";
var myRe = new RegExp("(\\$?[^"+bad+"]+?)["+bad+"]",'gi');
var myMatch;
while ((myMatch = myRe.exec(macros)) != null)
{
var vari = myMatch[1];
if (Left(vari,1)!="$" && Left(vari,1)!="." && Left(vari,5)!="Math." && CompareRegEx(vari,"\\d+\\.?\\d*",true)==false && CompareRegEx(vari,"(lt|gt|eq|neq|lte|gte|and|or|not|true|false|undefined|isNaN|NaN|null|noborder|parseInt|parseFloat|String\.fromCharCode)",true)==false)
{
ErrorsAddError(Lang_Err_MissingDolOrQuot+"[::]"+myMatch[1],MyTitle);
}
}
}
function ErrorsAddError(ErrorMessage,Title)
{
noError = false;
ErrorText.push(ErrorMessage);
ErrorLocTitle.push(Title.toLowerCase());
}
function ShowErrors(wotitle)
{
MessageHide();
var str = '';
for (var i=0; i<ErrorLocTitle.length; i++)
{
var err = ErrorText[i];
err = err.replace(/</g,"&lt;");
err = err.replace(/>/g,"&gt;");
err = err.split('[::]');
if (err[1])
{
err = err[0]+': '+err[1];
}
else
{
err = err[0];
}
if (!EditorMode)
{
str += "<div style='text-align:left;' forevent='"+ErrorLocTitle[i]+"' class='menu_item'>";
}
else
{
str += "<div style='text-align:left;' forevent2='"+ErrorLocTitle[i]+"' class='menu_item'>";
}
if (!wotitle)
{
str += "<div style='text-align:center;'>"+ErrorLocTitle[i]+"</div>";
}
str += "<div style='font-size:0.9em;'>"+err+"</div></div>";
mywindow_inner.innerHTML = str;
SetEventsByAttr(5);
SetEventsByAttr(6,'forevent2');
}
ErrorText = [];
ErrorLocTitle = [];
ShowWind(true);
}
function ErrorsClear()
{
noError = true;
ErrorText = [];
ErrorLocTitle = [];
}
var style_mainBgColor;
var style_toolbarFontColor;
var style_toolbarBackColor;
var style_windBgColor;
var style_fontColor;
var style_linkColor;
var style_headerColor;
var style_fontType;
var style_shadows;
var style_shadows_block;
var style_buttonRadius;
var style_borderWidth;
var style_borderImage;
var style_cursor;
var style_cursorHover;
var style_buttonBack;
var style_buttonConsole;
var style_buttonAudio;
var style_buttonPrefs;
var style_mainBgTexture;
var style_emptyImage = '#';
var	style_pictureDefault = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAC0lEQVQI12NgAAIAAAUAAeImBZsAAAAASUVORK5CYII=";
var themes_fonts = [
"'Trebuchet MS', 'Droid Sans', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', sans-serif",
"Georgia, 'Droid Serif', 'Times New Roman', Times, serif",
"Menlo, Monaco, 'Droid Sans Mono', 'Courier New', Courier, 'Lucida Console', monospace",
"Palatino, 'Palatino LT STD', 'Palatino Linotype', 'Book Antiqua', Georgia, 'Droid Serif', serif",
"Tahoma, Geneva, 'Droid Sans', sans-serif"
];
var style_buttonBack_standard = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAclBMVEUAAACvtru3t9yvtrqvtruvtruutruvtbqstrmvs7uutbqvtruutbyutbWvtruvtroAAACvtruttLm6wseyur+wt7y2vcIJCgqss7dtcXQCAgK4v8SmrLEVFhaFio55fYG+xsulq7CIjpJSVlhXWlxWWlwCEY1jAAAAEHRSTlMA/ALz99yab1AetmwmJrW1XIjPVQAAAOxJREFUKM91kuuWwiAMhMPSFlvbLrdEXLZW9/L+r2ig9Bw9yvzjmxDIACR9AIxqbqVsZzXmJRTed42LHtFH13Q9g8IP0qGwwRojBDp5YJT5kbwwlq7EDnuejsnhekLm8fJ7KQ5S3tPLVO/OWuvd8bIHgM4VftL/12Cy4zqAqcGNf2n9QzbLYDOBcmLnq0OfhFY4BUMUhZ/x9p10CyTiAC1SOpet065lJWxB+rBw/ZOW4GXd4FYr93ltNUf77vDP+nVHHjA8D2h4wHGLJGyR/D1GkkPMTt6xh1iPvfZQ1aetf4biwKSG9H0GNcHG70z9ILdeBrC4AAAAAElFTkSuQmCC";
var style_buttonConsole_standard = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAk1BMVEUAAACvtruvtrvAwMCvt7yvtruutruvtbqstrmutbmvs7uqqv+vtruutbqvtruvtruutruvtroAAACvtruttLmwt7yyur+6wscDAwO1vcK4wMW4v8RfY2VUWFqzu8BobG5cYGKss7ihp6xjZ2ozNTaqsbU8PkC9xMpJTE6YnqOSl5uKkJSAhYlwdXgeHyAODg+epKnTmoliAAAAEnRSTlMA/PMC+/eab1AmHgPctmy13LUWNecsAAABQklEQVQoz02S2ZqCMAyF2w6bCDpD09RC2RV3nfd/ukk+deC/geY0SbMIZi3EKt5mSmXbeMXHN18ijUJboTFY2TBKyfC2J8oaKYGQ0liVsML2jcMAoCmJBiBAt2FlLRJnJGBzaU+n9uIQpHEJ50kVSnDDtCuI3WloQKJKKVRkA8DfBxmJongMCIGN6J2h0dDsC7IR9JkcaBOuRGwp0LVYcHUgbSzySvZHzw4fl9H2sspFhro/1kuhO/YaM6GM7u19KdxLSqJYAByec/LngCxwKA1lO+duyYFDbStJiuuoDKbwjuyy+uHnaqK8ve7f+L7m53KB5KLN2HnfjUazwAVyS/j30NVEd+Br3JJXE1nwfhy9Z4Gb+Gm7Bmz3RNuAfrX9MyiN53qa6jPq96Dm0aIlcB7tvAwGwMzLMK9PTuvznf+vzx8JfyXPRcozbwAAAABJRU5ErkJggg==";
var style_buttonAudio_standard = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAkFBMVEUAAAC3t9yvtruvt7yvtruvtrqvtruvtruutruvtbqstrmutbmvs7uvtruvtrsAAACwt7yzur+6wcattLi+xcu1vcIEBAW4v8RXW167w8ifpapSVlg3OTsVFhapsLWNkpZrcHOmrbJhZWgbHByWnKCPlZlmam1xdXhbX2FJTE5HSkw+QEIwMzQjJSYPEBCXnaKOtJb0AAAADnRSTlMAAv379vHctppvUCYebPSrFOUAAAD/SURBVCjPdVLXcsQgDCSA250PSWCMu301Pfn/vwvGl0kmGfQEu6orsdUeGNsXWSplmhX78GV3vMyldoRITsu8DEzAd4lGwZU3LlAnOw8F/GBJqGDGKCXIHjy4+lvkK0ik2paM4mhDTJlQwG33Sadb51BxSkqfKtci+Juxb88APaISOvd9Sv8yE6o3aKpufoZBoUK5Z4UPQF3ZAaDRH+f5EXpthC5Y5jhNy9MAtY9YRphfoCPuMpYSp1cACIQz4/sVltZQyiTisYZ6I47VCS51XxmUUSKeKhS//S/+t93rd7vRAX8kmTZJmrskv0W8rCLqTcS47LFFRVcbP4bo+XwBieQg5aU8zYcAAAAASUVORK5CYII=";
var style_buttonPrefs_standard = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAqFBMVEUAAACvtrqqqtawtruutruvtruvtruutruvtbqstrmvs7uutbqvtruutbyutbX///+vtruvtroAAACyub6vtruwt726wscEBASqsba1vcK4v8S0u8C2vsMhIySss7iLkJSCh4tobG8TExTDytCnrbKhp6yepKiWnKBQU1VMT1FBREY2ODktLzAmJygPDxC7wsikqq+Rl5tzeHtYXF4aGxsYGRobGxwaGxzSUEOJAAAAEnRSTlMA8wP8+/fcmm9QHrZsJiYCtbXyCdbiAAABYUlEQVQoz31S2XLDIAzENUmcsylCgG878RXHdu72//+sQJNxnqphBtBqVtJKxNgHIWtvO3Wc6dZb2y95+heziQgAEQIxmS1eyCeZOwKpywG4S1E4c+2y8SsfKOccyhL0RcFfaac+cx/dEFV0GYZLpDB00Z8bZOGAC7WKoGWshUjV+ussNNVM0PAmu6Zn2vqmk7eQihkhmwnysGNv1oUcJxviCTeJMsYkY4+HvbIocYVHlgHl+1w74qwoslg/8j13gyWZApyvO8Z2leD8VJnX9QwwJQ6o2DAcTopzdToYsliBY4C7BYQBhAXuGjBUVT5oglJTiVJTDXllqLYBRZu8T5VKe5scafBly/VtuTKOpS3Xt+WuTYNH9mZH0+D6T5KGxe03k5L9tDFrrCRWRMS0iJKjDk6iIkXUIr5k91EFqZRpoNB/yj4OCusax0H9M9pxGTgfl+GFkI23NOuz9DbP9fkF+4wouziIgKEAAAAASUVORK5CYII=";
var style_buttonBack_light = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAS1BMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////+DmQsHAAAAGHRSTlMA/WYEA/YC+V5bDeq2alHXzZyOeUpALSqz+Pc0AAAAh0lEQVQoz42R6xKEIAhGoV2x+14r3v9JozIqyanjH+c7jgrAJZQ0ZXl+vn1VX6uekDtmbmRjc/T4UaG5Z0Tk9ihoPo/MP3gEaJejyyBiukfWO2+ylR6Ev5M0oupE1CHHDc91WqSvIijs48NSXpH4rphQIGmBakxL1GgTjdG2R8YMyo7WDvcmI/ZfCwTVGvKdAAAAAElFTkSuQmCC";
var style_buttonConsole_light = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAYFBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////98JRy6AAAAH3RSTlMA/gH6ArYHoJdNvl5ENyQOjX1TFQPs3lcrgms9GunbCL0flgAAAMlJREFUKM+dkElyxCAMRSXUgM3oeO4h0f1vGVVXRNnuXd5K9R9IIPgvN4C651yclOd8eth1WVb7mMAc8xyQ32DIMGtu4LkyEgqE/JPBaF4Dk94gDtVoo4iai0HatNPsGVmRstOHuXAW1qmweBSo4maG843hb/gMifA4/Nn+4fzxuZ0z7eMvyw37AhVSTMOCJOD3cFmW8fzGf6x3H3th3HWF2syk+5dwT2DOAqLvBB8/RInbOG6xqGgziiVEskXKCzX1fapwRQ+287+/kxL2cZ2lOgAAAABJRU5ErkJggg==";
var style_buttonAudio_light = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAWlBMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////9ZMre9AAAAHXRSTlMAAv4BIm476zgEx3UU9eLQvq6limdI57a1kl0NGCcOHP0AAACgSURBVCjPjVHZDoMwDGuO0hYYN+z0///mOlXaxaDzQ1XFSuw45j/Irxo9XqLNpi9GJExKfWc1fj/njCioBJx9U5JYr8CFCVo5jETycjIxs6dT77WEfbbU18uBGZ46wOriNDmoh8UxGCgMzQ18BZuIMyI4EWbGoHxMg0qOSB2BQtPWhzYKbxJ7o26DW4tv2t1fUNaRZEJcx54/VP60YvK4AyrkDA0nryxtAAAAAElFTkSuQmCC";
var style_buttonPrefs_light = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAY1BMVEUAAAD///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////+aRQ2gAAAAIHRSTlMA/QL6JAXIw597KiDrXDSn9e7i19CRY08UDK3ejHRHMnZITkIAAAD0SURBVCjPnZHpjsMgDISBcAQSyNUcTS+//1PuGJqq2/21tYQyfGMLxxZfhlJCiVDXAR/oj6iJ6g+kl4fZllbKdtnMY9GFKqEtjbutSEqq7D6S1YBspFYSAeNk0SY22JpBKqKKD7xZHR15Tp38uvqJlT8607iSCyyDhZx0wR4XOdyFUcqI+wDHeg3e5TfdOVers8sd7MXgpMNAOUAH3dw4qV+FQoi15/JbAwPWCVmnxDJlWbAwpd3xGuN1LO2apzPL6vWDkDN4GclA7yOhIT1n1Ti6dK5n3LvuQq4BLK/HYLbIY4+bCVH/2mFZ1P9W+0J/+bvzA9nHEd/Q6wn/AAAAAElFTkSuQmCC";
var style_buttonBack_sepia = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAclBMVEUAAAC3t7fTxqPUx6PUxqLUx6TTxqLSx6LTxqHSxqPQw6HRxJ7SxqHTx6PSxqMFAADTxqPSxaHe1LXaz63WyqfUyKQSBwLQw57Xy6jc0bHKu5WOdkwkEwmSelCpk2ni2bvJupN0Wzasl2ygil+ahFmtmW4C9Du0AAAAD3RSTlMAAvz28vvcmm9QJh62tWwLfySNAAAA5klEQVQoz3WS2ZICIQxFGaAXexFIQGBs1xn//xeFNK1VWtw3TkggN2FZP4wN/dwK0c79QEdW+NgJiMG5EEF0YwKF7yQ47r3RmnMHcpcQ8T2GRhu8oNFJTcB9gvk+Oq5NPN/Oa4Q7pJxRhsThqJTaIkGOqVQHzcoP6nbxmqpBl/4pHPGsBxqSdmJgPfCN/4OzWc400LMp8sKP9vqbdfXI48RaiydFWg5Fyx/alknrF/WhxVtZDdRLzdHD6fvxufpdanDLuePaHzVIllDkwxIyseTc3ybWba8Pqj7a+jKU9Zny+kyv9XkCEDgfrs06iOkAAAAASUVORK5CYII=";
var style_buttonConsole_sepia = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAjVBMVEUAAAC3t7fTxqPUx6PUx6TUx6LTxqLSx6LTxqHSxqPQw6HRxJ7SxqHTx6PSxqMFAADTxqPSxaHUyKTe1LXWyqfc0rHazq3YzKkJAADPwp2AZ0B9ZD3FtY2KcUiFbER2XDfh17huVDFWPSFPNx3b0K68qoG2o3mumm+kjmOTe1FgRydKMhobDQY0IBAxHg+x9tfQAAAAD3RSTlMAAvzz+/fcmm9QJh62tWzyrY2WAAABPUlEQVQoz02SV2KDQAxEyWJKDJjVVlMN7i25//GiMTF4vhY9SagF0FcQrNMiDsO4SNf4hF72LIm09Up5q6MkA5ns36FWQhBLCKXDbxDYN86siLxleaKVcRsQ9ndKkPHnbhy7szMklHvFZKER5Nr7tmRt760nYcKMUyV6Rb79hRXsh8lKJ1xnpCT5QwkjYLl3JFW0DlIOcJfyQxfHIWmQW1ENNXvO6odK2DyIvayG3Qz40QyV9HEQKlnp22eqm+WfhABk2udif7YGAKkk2W4BHQcgVWEFE9egDTTSOLYLW6BcybLXyf8Kf4ly0SCHSNU3dd30SgKgQYwEz6rZsZoKbhjJNESAuu77ugbAEN9jl+S7x+Hw6DzJaezvRUlzGvf78WTk/6KW1RrNMvNql2M4GiJznI9hOZ8c55PP5/MHHokg7u0yZCsAAAAASUVORK5CYII=";
var style_buttonAudio_sepia = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAkFBMVEUAAADUx6PTxqO3t7fTx6PUx6TTxqLSx6LTxqHSxqPQw6HSxqHTx6PSxqPVxKLMxJkFAADTxqPUyKTf1LXXy6nRxJ/SxaHd0rLi2Lnazq3Wyqd5XzkJAACynnRyWTRQOB6QeU4mFQrOwJrDsorLvJa6qH+JcEiDakIuGw0YCwXFtY1aQSR8YzxnTi1lTCs5JRJMZ/k7AAAAEHRSTlMA9PwC/vrcmm9QJra1bB4eAj5jhAAAARNJREFUKM8sztmWwyAIBmBxsjXdEFCjbbam66zv/3ZjPOWOD34Oaq0PpdpmVwFUu6bNrXr7sQb2QiSeoT4mePsGmLQujCm0JoZNouz7IGVS54y1rigl7BOu+4G0SR7FMIs1mkLOHEBWt2G5xcd98eS0wCGdqrlcXew88BNxoOhKrtOfQIWzl2hGPHWf/Q+OhgqCVjVcOuIufCGe/OvZTzh4q7lRW6/j5f494jklphn7X1xE+62qxEnaxjzwdv7rcWIrlQKK11fSPLh2D7ydh84S/A+nBG6jeLBbzgN3rjyac+EeFEPzICJIlCBBIg0LEgZ+lEDUggQiP+5gxxVRuKMWIsOHnBj4wOKI5MMNSj7c8OQDAJyZJGHecyPrAAAAAElFTkSuQmCC";
var style_buttonPrefs_sepia = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAApVBMVEUAAADTxqKqqqrUxqLTxqLTx6PTxqLSx6LSxqPQw6HTx6PUx6TUx6PSxqHTx6PVxKLMxJn///8FAADWyqfVyKTe1LUIAADPwZzTxqPazq3d0rHSxaI4IxHYzKrXy6ltVDHb0K7Qw57Ku5WvmnCmkGVELRcsGQweDwfm3sHf1bXGtY7CsYm6qH6Ib0dROR4kEwm1oniWf1SPdk15YDpiSSlbQiQQBgGRTGvaAAAAEnRSTlMA+wPybvbcmlAm/vz3trUeHgI47B8QAAABZ0lEQVQoz1VSh3LDIAyF2tmzQhiM8YgdO/HKTv7/0wq0aRIddyA98U7jEWtfhEz85XAwGC79iXPJX3w9mnOVICaKz0frJ/JNxh5HSiPGIkqRe2MTcvl+yOhURqxpWCSnlIW+CZozDpHGKLLTbnfKBMYUw7FFVh6bsU5kyR3gnmSiM663MlQjTuNrUKYajOm0DK4x5SNT5wCncQlvViZTHEyIz2ebcAsQAFSVu7bZZsZ9slBU7i8moLd5vtXmcdlLqhZkyFh+PgLs2lrKut0BHM85Y0PiMaEtQ1qLKBJ1asm0YJ4FKgdwC3AHVAawVG1vCRolpWosad9aqqWiuO9NXlkIUZTm0e+RquVbuYHWgSs3tOXaBmV8gDc7xNI26EaSgr5VEARQ3TSkvyMha48hFnm2OZjkTZYXiMxbP8ceolDF41EogeFz7K9Fdd1rUR+rTZKP1do/KycGKZ0YVi7/JZ+Flc/iXz4/pDApm4FGfcUAAAAASUVORK5CYII=";
if (LangCode=='ru') {style_themes.push('Асфальт');} else {style_themes.push('Asphalt');}
if (LangCode=='ru') {style_themes.push('Классика');} else {style_themes.push('Classic');}
if (LangCode=='ru') {style_themes.push('Хакер');} else {style_themes.push('Hacker');}
if (LangCode=='ru') {style_themes.push('Фентези');} else {style_themes.push('Fantasy');}
if (LangCode=='ru') {style_themes.push('Подземелье');} else {style_themes.push('Dungeon');}
if (LangCode=='ru') {style_themes.push('Море');} else {style_themes.push('Sea');}
if (LangCode=='ru') {style_themes.push('Заметки');} else {style_themes.push('Notes');}
if (LangCode=='ru') {style_themes.push('Апельсин');} else {style_themes.push('Orange');}
if (LangCode=='ru') {style_themes.push('Даэдра');} else {style_themes.push('Daedra');}
if (LangCode=='ru') {style_themes.push('Лес');} else {style_themes.push('Forest');}
if (LangCode=='ru') {style_themes.push('Визуальная новелла');} else {style_themes.push('Visual novel');}
function SetTheme(themeNum)
{
if (isNaN(+themeNum) || themeNum<0 || themeNum>=style_themes.length)
{
themeNum = 0;
}
style_current_theme = themeNum;
style_mainBgColor = "#222222";
style_toolbarFontColor = "#FFFFFF";
style_toolbarBackColor = "#3a3a3a";
style_windBgColor = "#3a3a3a";
style_fontColor = "#FFFFFF";
style_linkColor = "#8EC5F9";
style_headerColor = "#FFFFFF";
style_fontType = themes_fonts[0];
style_shadows = "0 0 5px #000000";
style_shadows_block = true;
style_buttonRadius = "16";
style_borderWidth = "18";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIAAAAyAQMAAAAk8RryAAAABlBMVEUAAACzs7P//+bLAAAAAXRSTlMAQObYZgAAAGpJREFUGNO90LERgCAQRNHvEJhRAYVQjiXQgZR2dEIJdnB6zsg5RpoIwQtvd9HzyfD1n1S7GVQ3c4ZsRkhmgmhmmM0CoR52mERoAnRBK7C6zVx+0+8LoCOf57X8Vx/v531Dpdz34NOKz/13FrKVvL8EnkUAAAAASUVORK5CYII=";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAB9VBMVEUAAAAAAAAAAAAAAAD///////8AAAAAAAAAAAAAAAAAAADFxcZkZGQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEBAQAAAAAAACTk5P9/f0AAABXW3qWlpb5+fkAAAAHBwcAAABSU3RNTXBcXnzMzMyYmJi6uroAAAAAAAD9/f0AAAD8/PwAAAD8/PxDQ2hgY4FjZIJpaYdtbYpxcY2pqbCkpKTn5+m6urpMTEzMzMwAAAAAAACdnZ0AAABDQ0NfX18AAADr6+s8SW0lN2AmJlFlaoaAgpd2do9eXn56epOGiJ61tbibm6y1tbu/v7+goKCwsLCYmJjR0dK+vstycnLNzc3S0tpXV1eHh4dMTEy0tLScnJz///+lpaUWFhZTU1OJiYl8fHzf399jY2MAAACrq6syMjLv7++amprt7e3S0tKoqKgAAAD///+9vb1paWlpaWlycnItLS3a2tri4uJ8fHx7e3tLS0urq6v///9bW1vV1dX///8AAAB0dHRvb28dHR27u7umpqYQQmsddJYAADMJJVIHGkkgfZ4fepsXWX8GGEgbao0YYIQBBTgUUHcQP2gOOWMccJIaaIwXXIELLloJI1EIDj8YX4QWVnwTS3MRRm4MM14IIE4ONmEGFkUDDD0bbI8ZYoYZYYYLKVYOJlMdHUqNUKhSAAAAg3RSTlMAAQgEqVUDEAYfDNqBSjIpFTEdGEdCIxOviBL7sV9aNy77+/rUo5N1aWdTQSAV+/n17+zr4NW4uKqojH12YlQ7OR7+/v729PPx6+ro4+DU1NHOzcjHwsC+t7GwraWkop2cmpmYlZGQj46Mh4eFgIB8enJyb2ppaF9bUExIQzw5NywpKMDsqM0AAAIVSURBVDjLbdP3e5pAGAdwgRYBKxARqLGOWnc0q9k73XvvvffeeymmiQlqEjXLNJ1/Z+/CtXly8P7Ifbjv897da0v77vvSNE3RNEHYrKrBs9njG2ZIkqFoS7F2jQbEF1W1cwwSJqC13nz6QWElOxImoOstVx684QWXnQTCDPSRSr6l+/pLuQ4JHFTLpcVKc2fkkRsJHHwrzRUyleZwZB0SOMgXZyfGMrmm8HogWChwkJkeHwdikwMJilgNRjIzk9l/wmkSEExkR3GBg6+G8ELBGwID/8UGXCAAxdRssbJxRWARo9nJqem5hTwS9UigLrJw+cdYYX6hpmPCOIfv4O/CfHGxnMvrWshx9qEcdBkp6CR/zhSK5d9bvE3exj+aFgqffzwkSMvvA90F2Hvrjr1t7QfbDx3Yv3Nb6MgtOWiEoNss/druOHzs5Omui1cvR84cb9sXHahTRA4BvZar7ek81XXh2r1Yv9858LwvGu0LCCkRZkCgLS3t7r50o6enN56UA0FBEAYHPykpCUQgoDV23H0S6431O4cEVhJVVRUlSQSPGEVo1RN3Xvjjcb+TB7kkA4okOQ7OAQKtt1+9T/qTbj4FewcfCQJMEgXWIXh9rtrx7CMvu2VeqV95TcRyGZO1y/PWJQQCQUWE66YaThxNfBZZQWBR36Z6l2gg7aJLUjmStllWmmZIzo66siyCohjQHWW5wV9UN5XTbM6XJwAAAABJRU5ErkJggg==";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAACBFBMVEUAAAAAAAAAAAD///////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABkZGQAAAAAAAAAAAAAAAAEBARPT3HGxsaTk5MGBgYAAAAAAABYWnvExMWWlpYAAAAAAABcX35jZYNra4hxcY3Ozs+6uroAAAD///8AAAAAAAD9/f0AAAAAAAD8/Pxubm4AAAAAAAAAAAAAAADs7Oy1tbqpqbDIyMmhoaHn5+m6urpMTEzMzMyVlZXu7u75+fkAAABpaWmdnZ309PT+/v4AAABUVFSxsbE+VHspRXEmJlFFRWliZ4VcY4JCQmhmb4yAhJl2do9eXn56epOHiqCbm6y/v7+wsLCmpqaYmJi+vstycnLNzc3S0tpXV1eHh4dMTEy0tLScnJz///+lpaUWFhadnZ1TU1OJiYl8fHzf399jY2Orq6syMjKamprS0tKoqKgAAAD///+9vb1ycnItLS3a2tri4uJ8fHx7e3tLS0urq6s5OTn////V1dX///8AAABXV1cdHR3///////8aap0AADMKKl0mm84POWwvvO8kkcQzy/4xxvkttukrrN8aaJsPPXEKJ1oCCDwuu+4llckWWYwFF0sqqd0jjcAggrUeeq0ZZJcXXpESSX0MMWUJDT4yyfwnntEdc6YbbaEVVYgTToIJI1YvvvEsr+Irq94qptkhg7Ygf7IdHUp8jacvAAAAgnRSTlMAAQSpVQMRCB8GFTAKgUkNMhhC+9qvNion+9uxYUf69e7r0ZOPhnVpZ1NMQTo4IyIcGuTg2Ne4uKqopY6KfXt2YlxYTyj+/v78+vr59vTz8evq49TR0c7Ix8LAvrexsK2lpKKfnZyamZiRkI6Hh4WAgHJyb2ppaF9bVVBIQzw4LB4RNdDCygAAAh9JREFUOMtt02dX4kAUBmCS3QAJcSFhlwQXFgSkCQr23rf33nvvvXcjQUEgUsQCa9vun3SGjOd4Qt6PyXNm5s7cq4kOPR2KEgROEBimUUtDfV39YJSkdSROqIqtW4S63sFRitIaSSSqgODseRlq1DNaJKqAJDnvPhsxcXqtDohqII2LGWfX/besAQklyC/M50RHx81+OxJK8DM3+z8lOlzdNUgoQWZuJjYxJjpad9RsqxIQjKeyU3+A2LUdCRxTgLFYMj6lEAownYAirSYQmJSFFQoTEptBfLIiloHYuSGU4EdiujCTEq21CoG2AP9/JQvZ2ZWMWSFQFYl4srA4UVxeKUkKId/D7+Tf7L+ludyCmJEEc+vVftYQkQW6ycVYMbUq7j6w37pvTRDMruvPwzxT6Q/0FsWl1T2Hm1va2k6fOtl8ZK/Z9ZBF14Fecz59qPbMuYuXO2/du9N95XzLCfeIoYmiESiX0qXjHZc6b3ieeG3DwfevB9zuAQunp+AeEAj5/LGu2w88nj5fgA0bOJ7/EvoKO5DGERAOtj9+4e3z2oJhTs9QMAwDmxhtIZQvPHpj8/lsQUsTZdSRIDoaBM4Bavued58DwwG7pXJw8BHDwCThuDxKH66V21+FTKydNTVu6iasEnmyjvZ+jHAWC6xLbS6++c/6Ryk9z4OycNXZ++RvoLVUhAHnIzSq+U6QtFFrpGFV6sFwnATV4aoLrAPYB5aZtyfIXwAAAABJRU5ErkJggg==";
style_buttonBack = style_buttonBack_standard;
style_buttonConsole = style_buttonConsole_standard;
style_buttonAudio = style_buttonAudio_standard;
style_buttonPrefs = style_buttonPrefs_standard;
style_mainBgTexture = "data:image/jpeg;base64,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";
switch (themeNum)
{
case 1:
style_mainBgColor = "#DDDDDD";
style_toolbarFontColor = "#FFFFFF";
style_toolbarBackColor = "#555555";
style_windBgColor = "#FFFFFF";
style_fontColor = "#555555";
style_linkColor = "#5A8BBC";
style_headerColor = "#666666";
style_fontType = themes_fonts[1];
style_shadows = "2px 2px 5px #888888";
style_shadows_block = true;
style_buttonRadius = "6";
style_borderWidth = "10";
style_borderImage = style_emptyImage;
style_cursor = style_emptyImage;
style_cursorHover = style_emptyImage;
style_buttonBack = "data:image/png;base64,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";
style_buttonConsole = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAACbGlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS4xLjIiPgogICA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPgogICAgICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgICAgICAgICB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPgogICAgICAgICA8eG1wOkNyZWF0b3JUb29sPkFjb3JuIHZlcnNpb24gMy4zPC94bXA6Q3JlYXRvclRvb2w+CiAgICAgIDwvcmRmOkRlc2NyaXB0aW9uPgogICAgICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgICAgICAgICB4bWxuczp0aWZmPSJodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyI+CiAgICAgICAgIDx0aWZmOkNvbXByZXNzaW9uPjU8L3RpZmY6Q29tcHJlc3Npb24+CiAgICAgICAgIDx0aWZmOllSZXNvbHV0aW9uPjcyPC90aWZmOllSZXNvbHV0aW9uPgogICAgICAgICA8dGlmZjpYUmVzb2x1dGlvbj43MjwvdGlmZjpYUmVzb2x1dGlvbj4KICAgICAgPC9yZGY6RGVzY3JpcHRpb24+CiAgIDwvcmRmOlJERj4KPC94OnhtcG1ldGE+CpnklSUAAAMuSURBVEgNtVVNS1tBFJ2XRE2FIC4UaZu4cmXsP+jHTqErlQZK6Uroql10Y7ooSnFl21W76koqcSO4tN100RSxBUWpgk0s1ggpDU1EQtSYrzc9Z3wvhPfGxE0HbmZy58459575eIaUUvzP5msFHolEroRCobuBQOCW1+u9Zppm6eTk5Ec2m10+Pj7+vri4WGuGYTSrYHJy8j7AX/n9/qvGeUO4VGvQm5lM5mMikXi6sLDw8yISLQGxpqamXgSDwecej0fQ4FJGICaFSpShmszm5ubY3NzcNx2JliAajT4cGBh4D0kMgqOXJMW4XgEIZK1WM2Ain8+nNjY2bsZisbSTxON0TExMBCgLwBS4z+cTtLa2trrZPhCTXHR1dfX39fU9w355nXgugu7u7jFo3stsmTmNgO3t7RImOjo62NOvKmMc5YOc9wDe4yRwnSKcljuW3rY8zFzCDJKhGZVKhfsgIZOKQbzs7OzsAfENEGQaSVwECLrOAJLYPbNFVaxCVKtVJQu1txKxewMxIbWo4cclEbKqWvMsnbdQSWONlUQcE5xzbHY8SE1rXO9cFZyeniaxZhhm2EcRCyVkURJhpfKjVxIRH8axPDs7+1VHtgauCnK53AcussEpCa1UKgkAiGKxKLgH9NkxjD88PNxHIomWBEdHR58RvGZnxcwIBlBZLpcFjNXwDpBAZU6iVCoVGxwc/NuSAG9LeWdn5wmyLQDEALi6TJTIypw9/ayCc8bBwcEqKn83PT3t2gOXRMxgfn5+bX19/QGegRwAVAUAq/f0wQR7gH9NJpOPlpaW/jiz53/XJtNJeXBKliHH7aGhodfhcHgEL6eBqtTzgMsmCoVCcWVl5S0kfYOqf3OdtnGDmtnMzMwINtjE8yx3d3fN7e1tube3J+Px+P7o6Ghvs7Wc00rUmAmydenKeT4R6Jp+CxinlYgTmla/UJq5C10tK2hYef524KLBZ48bpvXDlgTY2JJuKY5qBa+sXZUuRPlaSpROp1dnZ2cf4xsRxckJ4ibz+f6ytbX1Egj5C5GtCe0XTbdofHy8H0d3GGe/gKf7E45mVhfn9F2awLnwsv//AfZ2Ms7jYoK5AAAAAElFTkSuQmCC";
style_buttonAudio = "data:image/png;base64,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";
style_buttonPrefs = "data:image/png;base64,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";
style_mainBgTexture = style_emptyImage;
break;
case 2:
style_mainBgColor = "#222222";
style_toolbarFontColor = "#FFFFFF";
style_toolbarBackColor = "#222222";
style_windBgColor = "#222222";
style_fontColor = "#00CC00";
style_linkColor = "#e74c3c";
style_headerColor = "#00CC00";
style_fontType = themes_fonts[2];
style_shadows = "none";
style_shadows_block = false;
style_buttonRadius = "0";
style_borderWidth = "10";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACQAAAAkAQMAAADbzgrbAAAABlBMVEUAAAAaxgQuGyu4AAAAAXRSTlMAQObYZgAAACdJREFUCNdjqGBgYHzA8IeBgfkDw38ggJIQEaisAQMDw4FBRBLhZgA6eTdRaHQ1IAAAAABJRU5ErkJggg==";
style_cursor = style_emptyImage;
style_cursorHover = style_emptyImage;
style_buttonBack = style_buttonBack_light;
style_buttonConsole = style_buttonConsole_light;
style_buttonAudio = style_buttonAudio_light;
style_buttonPrefs = style_buttonPrefs_light;
style_mainBgTexture = style_emptyImage;
break;
case 3:
style_mainBgColor = "#693F24";
style_toolbarFontColor = "#FFFFFF";
style_toolbarBackColor = "#693F24";
style_windBgColor = "#DCC096";
style_fontColor = "#000000";
style_linkColor = "#643B0F";
style_headerColor = "#3D270E";
style_fontType = themes_fonts[3];
style_shadows = "0 0 8px #693F24";
style_shadows_block = true;
style_buttonRadius = "2";
style_borderWidth = "12";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACEAAAAhCAMAAABgOjJdAAAAgVBMVEUAAABdNR1hNx4AAAAAAABKJBFVMRw6IRIwGw/iwI3au4qQelpxYEeqiGJbMx3uzJeRb026mW9EHgyYg2Gmgl2hflmZdlN8bFFZNB5OJxQ8FwfqyJS5mG+0kGeujGWMaUmCYEFINSRQNSJBLB0VBwK+nXJZPyo5FQYdCwS/nXJZPiqHD1KcAAAABHRSTlMA/f4B87d6vAAAASxJREFUOMutk8uWwiAMQIUJVbBKS+nbvmvt+P8fOOEgi64yC+85bOCekEByAooT7Ft2RbJ5hXMA1tlvvnc0NpZrKduET8DuHgYTT1opdc7eaGR5kyg1Nl0JVeSpoOyaUamkyTM0rjoxxWByHUMtPDXEOjdDYRJ9dYZUhRFWyRh+00ea4npCLJUVplDyYwxCFM7ob57eGYUQw8doR2Mx4PEWd7U1Y+uMDPPBpF72kKl9YfpYg8t05p3GwixfgDMPh4Vbt9nxGY11KmOkXAAuAYDFb04r/OfVz6yqn31d8QucAnDhVd0/64qdnXGPRHoTETsYLBK3VET3YDyC8eMIxsMbxxh4iivE+KJB50HXQr8HAf0v9N/S/UH3GN2ndK/T80LPHDm3GzX7244GxR/iGy9djlnlLAAAAABJRU5ErkJggg==";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAA7VBMVEUAAADIzMP/5claWjoCAgJGPjgBAQADAwANDQwAAAAXFxUDAwEDBAMaGhkODg0BAQAKCghVQ2sAAADZ2dk7OzsxMC84PD0JCQc+Pj4KCwhJSVQEBAICAgI6NzBmZmbS0tLR0dHOzs7ExMXLy8u8vLzGxsawsLC5ubmlpaV1dXXIyMjCwsJlZWWamppqamoPDw7Kysqzs7OXl5dSUlIrKioBAQG2traysrKSkpKMjIx+fn1wcHBiYmJLS0tHRka/v7+srKyqqqqioqKEhIR4eHdcXFxDQkI5ODghISGWlpYwLy0ZGRiop7JTUl9WVlYSe3RjAAAAH3RSTlMAAgEBKwRWDrjVq4DV1Kupci0dBvHx5ePV1Y+NjU8P6gBz0wAAAZdJREFUOMvdkteS2yAUhg+oOfZma3oDNUB1Lcly716X3U3y/o8TNPJ44CIvsN8NM/Bxfs4ZAK7r2oYW1C4GYAQXbiuxXpoGNOB2ywKV08SjYWWrFXQGpU+J/+GdBM5gXRh7lJDnRVGMXx9Pr/eyDtKUbrqNKXGnVRHzdCS+3KGHB4yUMKczW4fSKJeszydk+OkXwloXBlzlmyZll8eUUjKsQcewZMqmqRF6xKXU/2PrAjIsp7Nn0iAeaeA38pba8L1pOl/nzCdnko4DliKY7xdVF7r5yiW0NdIUq7O0F4JNrz4fhnJeLZMUdGFbBHy+5xfh9+HUcdSIaJ2Eq8R3z0I/3AZzppS4qbIiCX33Ing8mD2BQp1nQ2lIpbE8LwiCXBN6x6hkqzCO434/lsecs6U+rNtj9CyCIEk8eZ0LthnLR2r8OP6dlmI0GjEmxkJkh28YdHrXg/0siqJZmk+y7KmWk9RA4JiPDYP5rsymAxN00MX/vox20cefoGEghDDC56yXl56NkS40Z5YBLXdm+4v+C4ZWeGP8A9wzObf5MbrGAAAAAElFTkSuQmCC";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABBVBMVEUAAADVxpT09P+/38MCAgIBAQABAQEQEA8EBAD/9u0oGigaGxgEAwEJCQcODg0BAQADAwILCwoAAAAAAABJSUk6OThDSEsLCwhKSkogICAgHx5XV2RHRDpFZXWIO453d3f////8/Pz6+vrv7+/39/fJycl5eXnt7e3X19fV1dX+/v719fXz8/Ph4eGQkI/x8fHq6urk5OTi4uLb29vQ0NC4uLiAgH9kZGM0MzIBAQLm5ube3t65ubmqqqqZmZiNjY1bW1tWVVUTExPExMSwsLCgn5+JiYmDg4NtbGxSUVC8vLy1tbVzc3JGREM6OTcmJiYeHh0ODg7My9iysrJlZHNIR0crLCsShDi9AAAAIHRSTlMAAQIDK1bVuA4FBKuA1aqpjXIdEPHx5ePV1NSPTzArDw8+1JQAAAGiSURBVDjL3dPXcqMwFIDhI8Ax2E6yyfYuiiimd+PudU/P7ibv/yhRBsagSZ4g3wVc6B8d0ADASVG0oMSXNwQIwcFZTpKFgMqVuqs9TgxZzcs9OHjFINVkbH84oqCC2GBlyBhfzuP439397d053Ydjkq6+dGSsTHPTcTOTfPkpSRLipDoQ2xtLpUW6sGx3gkeffksIUOM5ERxlCS3weuvIch/HBbBQh05JfLqHqmJF7mv/W2zAoY7YvrL8PsYqfuaeAqDmC58LgnisWxquBO330GkEwtd53oVuNlRwvyx0nY6ttebEmh5/3o+0ah1PdGCDZRy5+i46BH/3t22xMeKblwRqEGhKFdj+cnhlQe00H8eGbyuKXAWGG249aCiy8SjwNU1RNFqpahhFGRP0rr3UHPqO49i2ow7DkWst2MM6e5hdkigMDCp0iZms6FEw3l3vpikxKYusCLnYf0fA6p0MdpuZ52307eRi7BU8BwwOROH+DzXQ1+l4OhCAxfGHWYvZevbxBzAQz/OIR9Wsm5teCzEDyn+ABqVfwsvPls2r6xvzBP+ePdLD+03oAAAAAElFTkSuQmCC";
style_buttonBack = style_buttonBack_sepia;
style_buttonConsole = style_buttonConsole_sepia;
style_buttonAudio = style_buttonAudio_sepia;
style_buttonPrefs = style_buttonPrefs_sepia;
style_mainBgTexture = "data:image/jpeg;base64,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";
break;
case 4:
style_mainBgColor = "#222222";
style_toolbarFontColor = "#ffcc00";
style_toolbarBackColor = "#404040";
style_windBgColor = "#232323";
style_fontColor = "#c0c0c0";
style_linkColor = "#ffcc00";
style_headerColor = "#ffff00";
style_fontType = themes_fonts[0];
style_shadows = "0 0 5px #000000";
style_shadows_block = true;
style_buttonRadius = "6";
style_borderWidth = "16";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAMAAABg3Am1AAAAdVBMVEUAAAAxMTEkJCQmJiYoKCgiIiIaGhogICAsLCwqKipZWVkuLi4dHR1UVFRxcXFISEg9PT05OTk0NDRlZWVNTU1RUVEWFhZ+fn4QEBB5eXlsbGxERER1dXVpaWlBQUGJiYldXV1gYGCEhIQwMDCPj49iYmKWlpbt3VElAAAAAXRSTlMAQObYZgAAA/dJREFUSMeFVdcSozAMdK+42xBqKEn+/xNP3L0fHhjGsnZkid1Z5Ibf57eU4Tca7/3ql/M9FD+8P5/3WPbix2EY3vD64qZxnGZUPr878PmMw7ks47IM51TOYXwDYAL0OZznsPhlN8fqTbZo+P2m8Xy/fZm8H4ZlHBcz/d5+WMqxjucC0WnyBYpNZd80QbB17nCmttZMMW43Be6zRxcxTjHPrhzHuLQtOOM2RSRyCV9UKziM2xzm4NrXz1ipjK2yWM3Tt7pltzjlNGNKOGptyxbv4WLqsnGLc0yqF1oppZXNNjc4tUTahPOMGSMo7IAIu5WSEKIZV5Z2r54L0XWSQ4j3fde/BEs5x4sCwB8u5g0TSBBCvF5Siu5fTic7CHavl+iFJBZjrKXkAGg1ZiqEhENIgxfOGekkgQIQEoSJniicoaMbsLnig4Y4+u+qpsWKKcOo7usaoXv5f4CpKWPNcEbjGqFVrfkDYLZWUbgjMlkxzRR7qLC6OWJNOoFsNkeNF3uoEOa5bVBCIzOeU5gVfQDkZtYKWRp93uucMpMPVyrfcmRKJENLgf6zZez/AN8qZvzvWKsLKYaL/h+QApDKMhxQNWVvNdD+AWD2LVlOLDLOmYCZeD2M9dvyTQ2FmgtZU949APxaQVCMahRavSgl/QNgKWFzlUgQUAwZZy0eyHc4HA0WgqIWQqiZd/iBrWEzhfWSQfumJdpL9UC+Y12NkpShth5BSaHZw5TK0ZIGHaNlnBKV/Il8Lmyb5SA/UEbFDMT9wKU5b5F1nQZqRKxUS09srXO0VFKrUDnM3MZIyANbvVOMaCpRdbvbDRMPgGMyMVqtCZqraVnL7oGt0dUtKQI3d24/IsjhoQJuAQPlCEcmmIavi3YPEnVGMZgmu7mUcLg68TBW57S18KAd/kjYtXjoYTdVKc1iBJKUddq6J/Kte8VXhqsgB1bUlODqSQ9gHAkrjsazFIMpfQCcpX0Pxbse+QWgl7LhOy3jUKbPuS7v9xf8FpxxBBf1g8dpGCY/GXZzNOEMA2CbHxcw5KX8Ph6y/Tj597mCX4PvVjUvNbh4gUIlwml9r9g6E9zqJ79+3vvcwLLX7+gNWPnwGcw+ljTfLOWgh8MvjVEbsM452+i+e0zmnGLa7v9jXJ3x1Q4Hetusopyi6WiWgH1imywjFGOcU62WXVgRgpMFjnIcZhM1o9YSfTuFppcmlCnGbrJwShnvJNyXME2EgP2F6W2xOGmQfk1KU6oZUB1SpBR9f+fAV8BMxEuAndHbSvlVVd9TBPmgTwqeJToBWWDQnPK/+WB8HZxQ2b0AIInqXiAb4CzvIAKrhwXphHb3hmtQCcMXlOkhn5L7Q/gf+vFTE+yZDhsAAAAASUVORK5CYII=";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAACGVBMVEUAAAAAAAD///////8AAAAAAAAAAAAAAAAAAAAAAAAAAACPj48AAADU1NQAAAAAAAAAAACysrIGBgbX19e0tLT+/v4AAAAAAAAAAAAAAAD29vZeXVhRUVFkY2BsbGydnZ0AAADs7OwAAAAAAAAAAAD+/v4AAABKSkoAAAAAAABERERMTExgX1uGhYBpaGZ2dnZxcXG0tLSoqKjKyspJSUnZ2dm5ubm8vLwAAADMzMwAAADk5OQAAAAAAAD///8AAACEhIQAAACqqqoAAABbVDcmJiZORR9nY1NqaGBZWFVxbmJeXl68vLy6urqrq6uhoaGSkpJubm68vLyqqqp+fn7d3d1VVVXf39+3t7epqalKSkpJSUl5eXmIiIgVFRVwcHDJyclaWlr///8sLCyPj4+qqqokJCSCgoK2trYAAABhYWEAAADAwMBlZWX///+xsbGfn58rKyuLi4sAAAD4+Pj////19fWysrL////e3t4AAABfX19ubm7///8WFhYAAAAYGBjOzs6FagAAAADrvADCmwAzKQD+ywD3xgDXrABLPAC0kACiggAxJwALCABJOgHjtQC6lQBrVgDUqQCwjQCDaQB9ZAByWwBcSQBGOAANDQ37yQDnuQC2kgCZegCQcwCJbQCBZwB3XwBAMwA5LgAsIwAhGgAaFQAdHR1SQgHtvgDbrwDQpgDGngDFngCXeQBjTwBhTgARDgAUpD+PAAAAgnRSTlMAAalVEAgCHwQGFVoKzAwnGJAqyZSHVEQzIhr5+fbt3pqRjXZpXVxORjb7+fjy8vHr5+HHrZyYkn12cG5kS0JBNy4nHf7+/vz7+fjx49rX19fOycnFw8K7u7u7uLGwrauqqKWkn5yMi4mFhISBgYB8d3FwamlmYlZQTU1DQTw5OTUV52hIuAAAAgRJREFUOMt10+Vy20AQAOColUVRLFWpMa0Zk4aZqczMzMzMjew2cW3FEHKYmZ8w55xlz+SindEf6Rvt7t1uhtH50mnEQGQoRIOmSOM0UgShRHbuEIs0H1tUNE1gSkA0WWtbcLUsUCBJJmt1E49DgQJpMh4qKH/+ncRVCYGC6b7xaLzg9uNMKFAw1Tsc7O7cm70LChSEerr8/9vSAgF/uyMdHQoCgjZ/2AdEYB8UFIaCwXYoslABge8fFDmyQEFCDI0CsRsKFAy0+2JdY/EjiIAAfJ8PxyIr0dAeWWwtMhxbGgqORiekLQKew0J4MRIc6Yn2BUKSmBbpk1z2B8fWOvcfOpyTOyuKeVmVma1QpO5iZPVgfnGJ2Xzq5IniwgN52a8EHs5H8jZ7544eP33m4rWysju3blwpLTFXfpOTALA+EZjMP3f+6k2L7Zm95t2bF48eVLz+bWBYOgnEmZnCy9fvWSxPauq0Xz06XaPb/VMgmUQOCMTcC/cf2mxVDq3O2yxwHAceksfZFOgvrXhqr7LX1f8SSJ5JBM8zYIgpOYWp/K3L4XDV6w2MWkWDULEg5D34fLf/UrVb69J69JuFg5cYRhAURSVX6YfmmPWTXudp1BvS0wRMahuNH87WNglebzPoa/vNaXj/hSE5DrQFi0LjD4szrTyoj1DYb4Jm1TjoevsfbAAzLJR3GN8ufAAAAABJRU5ErkJggg==";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAACH1BMVEUAAAAAAAD///////8AAAAAAAAAAAAAAAAAAACPj48AAAAAAAAAAAAAAAAAAADT09OysrIAAAAAAAAAAABfX1m1tbUFBQUGBgYAAAAAAADW1tb+/v4AAAD8/PwAAAD29vZNTU1kZGBpaWidnZ3a2toAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABERERSUlKGhoBtbW1xcXG0tLSpqalJSUnQ0NAAAAC9vb3w8PAAAABjY2Pk5OQAAABPT0////+EhIQAAAAAAABbWzcmJiZOTh9nZ1NqamBZWVVxcWJ2dnZeXl53d3e8vLypqam6urqhoaGSkpJubm68vLyqqqrHx8fNzc1+fn5VVVXf39+3t7epqal5eXmIiIhGRkYVFRVwcHBNTU1aWlr///8sLCyPj4+qqqrc3Nzk5OQAAAC6urokJCSCgoK2trbAwMD///+xsbHJycmfn5/Pz88rKyuLi4sAAAD4+Pj///+ysrL///8+Pj7e3t5fX19ubm7///8dHR2ampq9vb3Ozs4AAADr6wDCwgCFhQBKSgC1tQA0NAD+/gD39wDX1wAMDABHRwGCggDT0wC6ugCjowAwMACwsACGhgB9fQAyMgANDQ37+wDn5wDj4wDh4QDGxgCZmQCQkACEhAB0dABxcQBtbQBpaQBiYgBdXQBAQAA5OQAsLAAhIQAaGgAdHR1SUgHt7QDb2wCfnwCXlwCLiwB4eABbWwDCVoQVAAAAg3RSTlMAAalVBAMQHghaCQYMFhLMkChDIfmVNzQYFMqHaV5UGvn28d7GjXx1XExGKiT7+fLt6+fhuqWdlY+Fg25kTkI3Lhz+/v78+/n48/Hu49za19fOycnIxsXCu7u7sbCvrauqqKWkn5yXlpWRjIuJgYB8d3d1cXBwaWZWUE5NQ0E8LCsjFWetwacAAAIUSURBVDjLddP3V9NQFAdwokkKaRJsUpKUTroXguw93Hvvvffee6UlVZp0IaCAiEzFrX+giUmbc/rs97d33uec+8a9ZU77VbsTklNWIj3WFVa7E0aQUmTpEl4WLzAjjEClAF/f1vkcxY2aAIEous/d7OVQVYBAXJBG3M1XnhjQckWA4POfqWnJ3Xy5QhUg+PTx/Y+UVONdpgoQ5IaH4okPQk21JgAwksokk6DQwauB+FgsmRgQzKqAIRC8G5TFG8FcqQkAxF4XCRAAAgBvB2PpoZS0rrJKFUVA3v82ls78nMktz4viQ46nJxLZXzPz4j9BYorQ3+H7eDqTnRyenhNyIu8qCP0lJ+LZ1Ky0cs1ac+0XnndVX6joV6rof5GdnF1d19Do8WzbsrVhwyqX99ozTukP7Tfnfk99Xb95+459TUeOnzh2+ODuRk/rY6WIBsR5YaFu196moy2+dv8d262OS2eqbrC0Bc8DfnFx04FDp1rOtwe6HKEwRVEPu5mIDOQaKuBrvacv+nx+m4Ni+iImJVGak0vkweie1o6AP9AVYiIGzkKSFoIgLHITF0rUn70dtNmCIZYmcMwoB8NwvDAHD06O7r/e7Qg6wiyNYrAyPhCEIDAMa6P01Lqx7T5LhSk2SurdBClRF857Ozt7TQzTRxPY/yen5+4jwmCKGgjt3mBeYijRz5F4OVJivhEjhqParcD8Ba5oluH+mSqBAAAAAElFTkSuQmCC";
style_mainBgTexture = "data:image/jpeg;base64,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";
break;
case 5:
style_mainBgColor = "#c0e0f0";
style_toolbarFontColor = "#ffffff";
style_toolbarBackColor = "#399fd2";
style_windBgColor = "#DDDDDD";
style_fontColor = "#222222";
style_linkColor = "#004f8e";
style_headerColor = "#FC9600";
style_fontType = themes_fonts[1];
style_shadows = "0 0 5px #004f8e";
style_shadows_block = true;
style_buttonRadius = "4";
style_borderWidth = "24";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEgAAABIBAMAAACnw650AAAAElBMVEUAAAB0dHQFT478lgADT47///8IePh+AAAAAXRSTlMAQObYZgAAAWRJREFUSMftloGNwyAMRbkN7sddoJEYwGKCk5IFULL/KgeoNnFELQbIl2gaeCUfR7UdMKGAxInK+PZRBgJRxRwOzIGY4YqoQLSsTe/0+SK3r63pjzkAMssWogI1VeNcZmKbtRBX6CiDEEih++MU4gbluOYRtG/HrlCMcbzTcRwNap7OPDa+nx9PWM+q/CYY8WtvCxs4kO6UcI00kuxkPcG8EfVkTwej6+l6MPk3XPRD12BCILIQC9SMa2AslOTd0TQEkYV0ukL94BbqEeMADc39dPJjwvVxwUinxXi5jiSLkxDqFWMIFQLrdSTZYeJxj/HH+GNcoIm/OVgSwz1h9IoXqCdmC8ksYyaJeelQS80k5KToCuUy4CV7gQhO2VjWWFdWW4BM/UH1lHMukFfKChTPZtwpiktsCyu88qo7wSnU4skt+f10TvOgcWKnDVEITkMjt4DTGsn8fJMFt1tLYJ5r/DChf0eK+flk8ZQvAAAAAElFTkSuQmCC";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABR1BMVEUAAAAODg4AAAAAAAACAgIAAAABAQHExMQiIiK9vb0CAgIBAQEBAQEBAQEDAwMCAgIDAwMCAgICAgIAAAAAAADFxcXFxcWmpqa/v7+ysrInJycCAgIEBAQGBgYCAgICAgIEBAQCAgIFBQUBAQEFBQUBAQEAAAAAAAAPDw+4uLhsbGxbW1tFRUW/v7+UlJTCwsKfn5/R0dE3Nze6uroqKioCAgKqqqojIyNbW1sdHR0YGBgCAgIMDAwiIiIBAQEFBQUAAAAICAiEhIRycnJOTk5jY2NTU1Ozs7OLi4vLy8uQkJA2NjYjIyOJiYkZGRmSkpJDQ0MrKysCAgIxMTEeHh4BAQESEhISEhIZGRk2NjYDAwMXFxcBAQEREREYGBgEBAQAAAAAAAAEBARtbW0BAQGwsLCysrKvr6+0tLR+fn6qqqqdnZ0cHBweHy8lAAAAZHRSTlMAAgMB/QRR+Ar9tYiDPSX6p19UOSv7+fn39/DX08uhmI18d29bWBwZFv7+/v38+/r6+fn39fLx7+zs5trXu7hwMRH+/v38/Pv7+vr6+fPy8e3q6ubl4t7Y0MjFta+mopFnSUgHvlKsVwAAAYxJREFUOMt90ld7gjAUBuATAoiAuLd11NW93dbRvffeIwGp/f/Xjbc1mDse3nzkfAEgmAcBJizp9dc/UQQv4gl5ktDtw5Vmb4LY/BksHrUNZ/FIB1bEnpoB5JxgWq65FwUwH2TtsGVS13wGIb4I3UTd1DRjC7rTMYqJKhOD9eMPkHjvEfhrUbdFZ+MbDnUIEGisLFrUHW16nITcWFqm1L10H3ISnrsDF7WWtxzqYCLUtiPUCs91FCY4SAJjajtCWR3pb4QxN6Ok7rpYYTsa218sAR4Xfd9ebGDG1vKQbZU5KQiUzML6rLl/rSYNwNzGILcWD1dPkjP8SgEjyG+c10jqi2m+YJVViEiIz3ASaLqiiqJIWvzSEXRJV0kRJq6CvAwMPg3g0yuKXpIsAeIAPYAkmCajr6RBEDi/hgaSANpIeANsx1iCofYRQjjnZbPc6iHOjbznAEkSyClydmo/lP9nYFx+8gNb7Bz11dX6+LAYep2M7JHTvuLlcJhgs3KurKBntYICgec3tcCe/wB2Jjhrv+suoQAAAABJRU5ErkJggg==";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABOFBMVEUAAAAAAAAAAAARERECAgL///////8CAgIAAAABAQH///8DAwMCAgICAgIAAAACAgIDAwMBAQECAgIBAQEAAAAiIiLw8PBLS0s5OTkBAQEEBAQBAQEFBQUCAgICAgIAAAAAAAAPDw8PDw8eHh57e3teXl7IyMji4uKysrL7+/vr6+sDAwM1NTUxMTEoKCghISEYGBgREREFBQUJCQkDAwMwMDAaGhoGBgYCAgIAAAACAgIAAAAAAAAEBASMjIyPj49nZ2eDg4NwcHC7u7vU1NTBwcHc3Nzb29vZ2dkvLy/5+fkiIiLn5+e+vr56enpZWVk7OzsCAgJzc3NDQ0MoKCgBAQEhISFKSkoeHh4DAwMBAQEEBAQFBQUAAAAAAAAAAAD8/PwBAQH///+pqanj4+PPz8+Xl5coKCggSyIsAAAAYHRSTlMAAgMB/fz5hgRQ94tZVBv6tX1ePjoI/Pr02da3p6GYcCYXEgz+/fv5+ff38vDv7Obb19LLxbukd25kSC0qIwX9/fz8+/r6+fn5+fby8fHu7erq6ebl4tDIta+nkXA2My+NUfz3AAABgElEQVQ4y33S13qCQBAF4FmWKoi912i6vURjTe+998SFaN7/DTLmMi5yBR//nh0OCxDMgABzLrX105krglfrRW2e8NnyfuVzjtj4lqSDWtZZPIWtkWw3F4A4J4xG8tZLHigf6LY8wozF9JDwRf9+RULhWlKcxjCiU2GtRTqg8t4T8JcOcQwrHHWoQwB3+RiFdFQJOAmtvDwVy9W+kwhU92QUqw2sgy96NRuFPEnkUXCQCtnGBIVrMTWklHIzTO+OC+vYVnC9YQKdFbnXXRRrkQzoMZOTQiCUXjq1pPCdN54Fym0MfJF16/wkluNXCpTAZvSixOpd1HyBpRaYyFhy4CRIu5AQRZHF+KXjFuw9FGcoboK8DApJBaDrEUUPi5tAOEBxkxBk2HSXFAgC52jooAqgT4XHjStmEgbeHCGE+jz4LQ9Kj/NH3nxAVBW0OjtbtR8X/mdQajb9f3dtdinLt0EQZjb5SqS1gJZKGtfjcfEDyOyceUPRFSME7ueW14/PvxWPNQwuF8qJAAAAAElFTkSuQmCC";
style_buttonBack = style_buttonBack_light;
style_buttonConsole = style_buttonConsole_light;
style_buttonAudio = style_buttonAudio_light;
style_buttonPrefs = style_buttonPrefs_light;
style_mainBgTexture = "data:image/jpeg;base64,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";
break;
case 6:
style_mainBgColor = "#F0EEC8";
style_toolbarFontColor = "#e74c3c";
style_toolbarBackColor = "transparent";
style_windBgColor = "rgba(255,255,255,0.5)";
style_fontColor = "#222222";
style_linkColor = "#597bd5";
style_headerColor = "#e74c3c";
style_fontType = themes_fonts[4];
style_shadows = "none";
style_shadows_block = false;
style_buttonRadius = "16";
style_borderWidth = "16";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwAgMAAAAqbBEUAAAACVBMVEUAAABuSTznTDyBjUBgAAAAAXRSTlMAQObYZgAAADJJREFUKM9jCEUCqJwABjhgReOsWgVhrlqFl7NCC8JZoYXCId4AhKWjHHTOoI4fnKkKAGNaRP3D5CNTAAAAAElFTkSuQmCC";
style_cursor = style_emptyImage;
style_cursorHover = style_emptyImage;
style_buttonBack = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAOVBMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC8dlA9AAAAE3RSTlMAJOESBUXpy7CJaWDRqJIrQTUwV/4llwAAAHNJREFUKM+NkksOgCAMBR8KouAHvf9hJd3pBHGWbTJtXqsHwUnXKeAmp3EIqPuYpG0XWGYprx71MIxVdqBe5SYD82IykKJXqbI3R2vTNZsM7JuUY2tTp88GVQkqDOe6RaSKbNT/SEyEEBF771A8bf8Z+D43/YwDbRmKeNwAAAAASUVORK5CYII=";
style_buttonConsole = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAATlBMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADEoqZZAAAAGnRSTlMAoF4GQZBkC9HbG/v06cOpe0m7sDcjhHFWL7Z5hcwAAAC2SURBVCjPpZBbEsIgDEUvhKSAFuhT3f9GbXGUKJ+ej+RmzoRhgg+PYvK+4pe4hN3e95A2fLGFQjXI+ICCvLzjPQxKyNJyvimRXMvOK3E912+8nn64KnEBRLiEWeQcGmPENJ9hnhBHJRaL4XiuFslKTJ6QpqMnUHBQZEOFAS60GGgo+9UDfg2G8G0uxw/ieDT8CuuZve0E2FS4E5utxE44U3F/bDTzKh30uhV6zGxtYvSQGGPb+ATACQYBPghAygAAAABJRU5ErkJggg==";
style_buttonAudio = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAAP1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACzJYIvAAAAFXRSTlMA4QIe/OghjthRNMKueGpaKxeBPg8e9cGEAAAAjElEQVQoz4WS2w7DIAxDHWi4Fijt/v9bhxBa1QVUP3KEMU4gpLLCTJ9905jocJZnwHMBTcBJAQ1I+1jxBKoJxtmeh/yIFXbeujK6yFFBU+FcybQrZAbQwSXA8DFsf4B8bYf2xD/wHJJFvATQyV5xCZZWy8fvuHjEXX/wrkSJSmSJ77XLQb2PVi7Dcn2+pFQFsb/STEIAAAAASUVORK5CYII=";
style_buttonPrefs = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAAASFBMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACrhKybAAAAGHRSTlMAIhmU6312YAnl3ZpS8cvAt6qjh25HQSrR7OpaAAAAsklEQVQoz3WSNxLDMAwEF2AUSWWJ/v9PXXA0diFsi3wHfuh1KS/IVOskLwEXIDj+ya4sqc4d+lzTUlx+cne5S1AADeWW/akbnXOLseUxjcFyA9Fv57n5CNwLg1RgXwVA1h1KYlADcc1oSkpe47ObuFmzF5o/Dt8Qn3V2AjqFTttQ/4GPV7ZGD5NyVSCepAPgSJwRqJcZMFtZw8117QNtSWwRbdmHUW4Y5YZRtrX2M5jv8wUBHAox7hbaKQAAAABJRU5ErkJggg==";
style_mainBgTexture = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGkAAACfCAMAAADNlwcMAAAANlBMVEXw7sjw7crx7s7x7svx7cfv7sL17czt7c7q8Mbt78bq8M7t78nEw8u9xcnm8cjDw8bm8sK3x8GNR1hkAAABO0lEQVR42u3Yza6DIBiE4QFR4Xh+7/9mT5qyaDSEaj8mWuZpu+nmDQujDr5Y8MuCmQUTCwILsSQidhILPAsCC/FMIwsWFvyw4JsFLkNrKqnUUWlTVEkllXYLLIgs2iMuske4ldAK0kpwjTYKuApYUUmlDkqpQHvE6faIWPnZwEeF9oj6HlGHV6mkkkobKqmkUpnP5if5zO2FdNCBJxYWfLIgsmBhwcgCf9DrG0uzK1kllTooTSzbzTIzv08hsrzlij0edObniD8WuMxnza7gNy4FnekSZ2KUhswZUUmlnku+YM7M9gqkRrRHXHWPCAXjeJY9YvdegcHdP49uf/jM7koeSvx8+xJK/k4llZqVJhaUpMxsr8CyEltB6a6uPaLXPWKoMHuTU4lQCjrTyc9k9iankkodlHyF2V7xDwOO5fb67rYUAAAAAElFTkSuQmCC";
break;
case 7:
style_mainBgColor = "#f1c40f";
style_toolbarFontColor = "#FFFFFF";
style_toolbarBackColor = "#d35400";
style_windBgColor = "rgba(230,126,34,0.9)";
style_fontColor = "#FFFFFF";
style_linkColor = "#FFFF66";
style_headerColor = "#FFFFFF";
style_fontType = themes_fonts[0];
style_shadows = "-2px -2px 0 #FFFFFF inset,2px 2px 0 #FFFFFF inset";
style_shadows_block = false;
style_buttonRadius = "5";
style_borderWidth = "0";
style_borderImage = style_emptyImage;
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAABUFBMVEUAAADe3t4JCQkRERHe3t7BwcELCwsCAgIDAwOAgIDd3d3Pz880NDQUFBQAAAAAAAABAQGsrKx3d3clJSU+Pj4dHR0yMjIZGRkoKCgdHR0PDw8WFhYMDAwWFhYICAgICAgKCgoLCwsPDw8AAAAAAAAXFxc0NDQSEhIAAAAeHh5XV1fR0dEAAAC1tbVEREQREREsLCxDQ0MnJydAQEBISEgpKSkeHh4fHx8aGhoAAAAAAAAiIiIxMTEODg4aGhoAAAAiIiJ4eHhmZmbe3t7d3d3b29vc3NzZ2dnHx8cQEBDT09OLi4tnZ2dcXFxNTU1ISEjV1dWwsLClpaWfn5+IiIiAgIBiYmI/Pz/Ly8u/v7+8vLy5ubmzs7Orq6uoqKiioqKWlpaEhIRwcHAhISEdHR0YGBjExMSTk5OSkpJ0dHRra2tUVFRRUVFDQ0M8PDwpKSlCcaapAAAAQ3RSTlMA/AMB/gbNKRUP/v750o9lWv7+/vjp6OLY1MfFv7yzpJt+dG5WU1BFQD8WCwf++fn29fXy5uDPtJGGhYF3XE02JSAFw/pwUQAAAYxJREFUKM9VkdV26zAQRefYDmOZmeEyM4xkCEOTlLn3lvv/b53YWU261/KDZ0szRxLR91fLZJCPaVKXb8nC0JoYKYo2TMPo2LM9HIc3DCmaFApRl5EXQHmeKLZOPydSy7Hs0kbQMVMC9kf+LKXGZl/Xj4bGb1ufg5HmTB1cCCcP+7x+4PocibdrgYkMV+LbF1dA/0BOJwYaOJluC/nmGuDGeULnzk6Z855bSa6QnyU65qLyUGA+9nZUbsvF5WRIMotcLILr3h7zfYkVs61bP4Jmf6cPwOU7pXZqrGzWqKVi7Vgy/40T3/Vq3EGp1kq7vp6NfPkPVd1m2/aFtpoLImJTpffhC1fxExpbs0Q0fwNdbeZZPykbhxNya+N9cMBOzw5luR9+E82Vd2E73INiNbhKZmjx44kDrXoNihkSol/fVWHpZ9PTEksO+WvyNAcJG6AcHISJfLW5MHxkQXfWY//lJ/KRTZGZ4j9YSk4HtzC1Gjxy0C87mo9bGjo/mtmkLobcf7pcbJYG09H23yPTflcTLrtRWgAAAABJRU5ErkJggg==";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAMAAADXqc3KAAABa1BMVEUAAAAAAAARERH///////8WFhYGBgaAgID19fU8PDwVFRUMDAwWFhYAAAAAAAAHBwcAAAAAAADg4ODx8fHv7+/Hx8cqKipFRUVNTU1JSUkhISEuLi4cHBwLCwsZGRkKCgoJCQkMDAwREREAAAAAAAAaGhobGxv09PQAAABVVVXQ0NDPz8+Li4syMjItLS08PDxTU1M5OTkeHh4vLy8aGhojIyMiIiIZGRkjIyMeHh4AAAAAAAAXFxcmJiYAAAA4ODg+Pj46OjojIyMiIiIAAAAGBgYpKSmHh4dcXFwODg6xsbH////+/v78/Pz5+fn7+/vy8vLm5ubLy8tYWFji4uK5ubmdnZ2YmJiTk5N2dnZubm5oaGhSUlJISEglJSUSEhLr6+vY2NjV1dXFxcXAwMC9vb22trasrKypqamgoKCCgoJ5eXleXl4fHx/t7e3d3d3a2trBwcGhoaGHh4eFhYVzc3NVVVVNTU0vLy8fQuFdAAAAS3RSTlMAAwH9/P1aEP750M3Cj2VBKBYIBf7+/v339OnY1MG8s6SbdG5WU0oLBwT+/v329erm5uPg4NPPyLSRhoWDgXp3Uk9CPDYtJSAZEg358J37AAABjUlEQVQoz02QZXMrMQxFtSuFuSkzw2NmfmXZG2YqM/PPr7LbNjkz/uB7RtcaA/x2/QUDbEwTWvz0lfr/iZFQtGEaxoOtb9BuYMaQ0IR4B7R49wzpZhxg1QPTQf8fj3tqxmkMZwiff1iaGhwce7mT6/98VPvqPGmO7iAfBnxbXY1LxMNj6nz13zHR11615rpCfNGd1J3dRSqMNIWcX0XkYv1EJ+sHzHlXyuubBnuXlY9Z9N6VmHcbpyqZT9HeUFx2FjlZQc41Nph7MqyYdaLmtsvm5kY2iau3Sp1tsdKscdvvkVyGFnotte7a5geUqkWa+bI7+m0PVXqNtbaF5vKECM9w5k3gOKX4iQTlx6Rq/Ih0+vqCE09K03lQfm2oiyxkq21Ccer9IsCP6jppi7ldqJ4ImB2TnwoWJVS7wXIYhNj3vjSybgmNhZCsZQDMBw+SpB+VsmjTD2Cr2Ym3OUbHaMR07xewkaHoaCVLrKSRsqXhSNwRdp97YF9y0vsD4VloIR8dC1Ur5UxfKNa83QMq4GHoZJZOFAAAAABJRU5ErkJggg==";
style_buttonBack = style_buttonBack_light;
style_buttonConsole = style_buttonConsole_light;
style_buttonAudio = style_buttonAudio_light;
style_buttonPrefs = style_buttonPrefs_light;
style_mainBgTexture = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEASABIAAD/4QCURXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgExAAIAAAAUAAAATodpAAQAAAABAAAAYgAAAAAAAABIAAAAAQAAAEgAAAABQWNvcm4gdmVyc2lvbiA0LjUuMgAAA6ABAAMAAAABAAEAAKACAAQAAAABAAAA+qADAAQAAAABAAAA+gAAAAD/7QA4UGhvdG9zaG9wIDMuMAA4QklNBAQAAAAAAAA4QklNBCUAAAAAABDUHYzZjwCyBOmACZjs+EJ+/+IMWElDQ19QUk9GSUxFAAEBAAAMSExpbm8CEAAAbW50clJHQiBYWVogB84AAgAJAAYAMQAAYWNzcE1TRlQAAAAASUVDIHNSR0IAAAAAAAAAAAAAAAAAAPbWAAEAAAAA0y1IUCAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARY3BydAAAAVAAAAAzZGVzYwAAAYQAAABsd3RwdAAAAfAAAAAUYmtwdAAAAgQAAAAUclhZWgAAAhgAAAAUZ1hZWgAAAiwAAAAUYlhZWgAAAkAAAAAUZG1uZAAAAlQAAABwZG1kZAAAAsQAAACIdnVlZAAAA0wAAACGdmlldwAAA9QAAAAkbHVtaQAAA/gAAAAUbWVhcwAABAwAAAAkdGVjaAAABDAAAAAMclRSQwAABDwAAAgMZ1RSQwAABDwAAAgMYlRSQwAABDwAAAgMdGV4dAAAAABDb3B5cmlnaHQgKGMpIDE5OTggSGV3bGV0dC1QYWNrYXJkIENvbXBhbnkAAGRlc2MAAAAAAAAAEnNSR0IgSUVDNjE5NjYtMi4xAAAAAAAAAAAAAAASc1JHQiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhZWiAAAAAAAADzUQABAAAAARbMWFlaIAAAAAAAAAAAAAAAAAAAAABYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAAt4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9kZXNjAAAAAAAAABZJRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAABZJRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAZGVzYwAAAAAAAAAuSUVDIDYxOTY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAuSUVDIDYxOTY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGRlc2MAAAAAAAAALFJlZmVyZW5jZSBWaWV3aW5nIENvbmRpdGlvbiBpbiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAACxSZWZlcmVuY2UgVmlld2luZyBDb25kaXRpb24gaW4gSUVDNjE5NjYtMi4xAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB2aWV3AAAAAAATpP4AFF8uABDPFAAD7cwABBMLAANcngAAAAFYWVogAAAAAABMCVYAUAAAAFcf521lYXMAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAKPAAAAAnNpZyAAAAAAQ1JUIGN1cnYAAAAAAAAEAAAAAAUACgAPABQAGQAeACMAKAAtADIANwA7AEAARQBKAE8AVABZAF4AYwBoAG0AcgB3AHwAgQCGAIsAkACVAJoAnwCkAKkArgCyALcAvADBAMYAywDQANUA2wDgAOUA6wDwAPYA+wEBAQcBDQETARkBHwElASsBMgE4AT4BRQFMAVIBWQFgAWcBbgF1AXwBgwGLAZIBmgGhAakBsQG5AcEByQHRAdkB4QHpAfIB+gIDAgwCFAIdAiYCLwI4AkECSwJUAl0CZwJxAnoChAKOApgCogKsArYCwQLLAtUC4ALrAvUDAAMLAxYDIQMtAzgDQwNPA1oDZgNyA34DigOWA6IDrgO6A8cD0wPgA+wD+QQGBBMEIAQtBDsESARVBGMEcQR+BIwEmgSoBLYExATTBOEE8AT+BQ0FHAUrBToFSQVYBWcFdwWGBZYFpgW1BcUF1QXlBfYGBgYWBicGNwZIBlkGagZ7BowGnQavBsAG0QbjBvUHBwcZBysHPQdPB2EHdAeGB5kHrAe/B9IH5Qf4CAsIHwgyCEYIWghuCIIIlgiqCL4I0gjnCPsJEAklCToJTwlkCXkJjwmkCboJzwnlCfsKEQonCj0KVApqCoEKmAquCsUK3ArzCwsLIgs5C1ELaQuAC5gLsAvIC+EL+QwSDCoMQwxcDHUMjgynDMAM2QzzDQ0NJg1ADVoNdA2ODakNww3eDfgOEw4uDkkOZA5/DpsOtg7SDu4PCQ8lD0EPXg96D5YPsw/PD+wQCRAmEEMQYRB+EJsQuRDXEPURExExEU8RbRGMEaoRyRHoEgcSJhJFEmQShBKjEsMS4xMDEyMTQxNjE4MTpBPFE+UUBhQnFEkUahSLFK0UzhTwFRIVNBVWFXgVmxW9FeAWAxYmFkkWbBaPFrIW1hb6Fx0XQRdlF4kXrhfSF/cYGxhAGGUYihivGNUY+hkgGUUZaxmRGbcZ3RoEGioaURp3Gp4axRrsGxQbOxtjG4obshvaHAIcKhxSHHscoxzMHPUdHh1HHXAdmR3DHeweFh5AHmoelB6+HukfEx8+H2kflB+/H+ogFSBBIGwgmCDEIPAhHCFIIXUhoSHOIfsiJyJVIoIiryLdIwojOCNmI5QjwiPwJB8kTSR8JKsk2iUJJTglaCWXJccl9yYnJlcmhya3JugnGCdJJ3onqyfcKA0oPyhxKKIo1CkGKTgpaymdKdAqAio1KmgqmyrPKwIrNitpK50r0SwFLDksbiyiLNctDC1BLXYtqy3hLhYuTC6CLrcu7i8kL1ovkS/HL/4wNTBsMKQw2zESMUoxgjG6MfIyKjJjMpsy1DMNM0YzfzO4M/E0KzRlNJ402DUTNU01hzXCNf02NzZyNq426TckN2A3nDfXOBQ4UDiMOMg5BTlCOX85vDn5OjY6dDqyOu87LTtrO6o76DwnPGU8pDzjPSI9YT2hPeA+ID5gPqA+4D8hP2E/oj/iQCNAZECmQOdBKUFqQaxB7kIwQnJCtUL3QzpDfUPARANER0SKRM5FEkVVRZpF3kYiRmdGq0bwRzVHe0fASAVIS0iRSNdJHUljSalJ8Eo3Sn1KxEsMS1NLmkviTCpMcky6TQJNSk2TTdxOJU5uTrdPAE9JT5NP3VAnUHFQu1EGUVBRm1HmUjFSfFLHUxNTX1OqU/ZUQlSPVNtVKFV1VcJWD1ZcVqlW91dEV5JX4FgvWH1Yy1kaWWlZuFoHWlZaplr1W0VblVvlXDVchlzWXSddeF3JXhpebF69Xw9fYV+zYAVgV2CqYPxhT2GiYfViSWKcYvBjQ2OXY+tkQGSUZOllPWWSZedmPWaSZuhnPWeTZ+loP2iWaOxpQ2maafFqSGqfavdrT2una/9sV2yvbQhtYG25bhJua27Ebx5veG/RcCtwhnDgcTpxlXHwcktypnMBc11zuHQUdHB0zHUodYV14XY+dpt2+HdWd7N4EXhueMx5KnmJeed6RnqlewR7Y3vCfCF8gXzhfUF9oX4BfmJ+wn8jf4R/5YBHgKiBCoFrgc2CMIKSgvSDV4O6hB2EgITjhUeFq4YOhnKG14c7h5+IBIhpiM6JM4mZif6KZIrKizCLlov8jGOMyo0xjZiN/45mjs6PNo+ekAaQbpDWkT+RqJIRknqS45NNk7aUIJSKlPSVX5XJljSWn5cKl3WX4JhMmLiZJJmQmfyaaJrVm0Kbr5wcnImc951kndKeQJ6unx2fi5/6oGmg2KFHobaiJqKWowajdqPmpFakx6U4pammGqaLpv2nbqfgqFKoxKk3qamqHKqPqwKrdavprFys0K1ErbiuLa6hrxavi7AAsHWw6rFgsdayS7LCszizrrQltJy1E7WKtgG2ebbwt2i34LhZuNG5SrnCuju6tbsuu6e8IbybvRW9j74KvoS+/796v/XAcMDswWfB48JfwtvDWMPUxFHEzsVLxcjGRsbDx0HHv8g9yLzJOsm5yjjKt8s2y7bMNcy1zTXNtc42zrbPN8+40DnQutE80b7SP9LB00TTxtRJ1MvVTtXR1lXW2Ndc1+DYZNjo2WzZ8dp22vvbgNwF3IrdEN2W3hzeot8p36/gNuC94UThzOJT4tvjY+Pr5HPk/OWE5g3mlucf56noMui86Ubp0Opb6uXrcOv77IbtEe2c7ijutO9A78zwWPDl8XLx//KM8xnzp/Q09ML1UPXe9m32+/eK+Bn4qPk4+cf6V/rn+3f8B/yY/Sn9uv5L/tz/bf///8AAEQgA+gD6AwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6g4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrCw8TFxsfIycrS09TV1tfY2drh4uPk5ebn6Onq8fLz9PX29/j5+v/EAB8BAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKC//EALURAAIBAgQEAwQHBQQEAAECdwABAgMRBAUhMQYSQVEHYXETIjKBCBRCkaGxwQkjM1LwFWJy0QoWJDThJfEXGBkaJicoKSo1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoKDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uLj5OXm5+jp6vLz9PX29/j5+v/bAEMAAQEBAQEBAgEBAgMCAgIDBAMDAwMEBQQEBAQEBQYFBQUFBQUGBgYGBgYGBgcHBwcHBwgICAgICQkJCQkJCQkJCf/bAEMBAQEBAgICBAICBAkGBQYJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCQkJCf/dAAQAEP/aAAwDAQACEQMRAD8A/uP4r+f/AOPnjj4heJ/iXrdh42vJz9jvZ4I7QuwhhSNyEVI87cbcHdgk5znmul/aZ+LHjLxv8VtX0++vpEsdIvJbS0t4HZIkWFim/CkZkYjLMeewwBXFXWuWHxP021tPEE3keJrVBBFfzOBFfQoP3cVy7H5J0HyRzH5XXCSEEBq/55eJM9jjL4Wldcr7/F5f5f5n+m/g14W1OHowzjGctT2sVe0daV9U7t6q2krK603jdnkFXLXUNQsN32G4lg3/AHvKkZM/XaRn8aiuba5srh7S8jaKWM7XRwVZSOoIPIqCvg9Uz+pWoVI90/mhzu8jF5CWYnJJOT+dfoj+xFZ6zeeG/EV7oQ33WlXMEscROFmSaNhLCfr5asp7MPevzsFftJ+xN8OrnwP8IP8AhIbwbrnxE4vwg4KwhNsKknuRlvbd7V9NwhgpVsYmnblTd1un0a807H4Z9InOaWD4clCVuacoqK72d38uVNP1PO/i78JvDXxvsH8Y+BSLXxHBEzT2xGGuCgA2SdAHB+VH/i6Hpx+ctxb3Nncy2V7E8M0LmOSOQFWVlOCrA8gjuK+2/Ffxw1vwZ4yfXog0R1C5kaaFcfKiHADLwGweOxPJzmvTP7P+Av7SCjUdQQWWqpEDNcwSLA5YnG0h/v46jeuQOhr3eHeJMLxFh44lfu6rbV5LljOzautXyy0u07eWx8bwfxTmXDeFgsxpSqYRr3ZL3nT7xe1430Wt+3Y/Ozw/aC61RZZBE0Vopu5VmJCtHBh2TgHlgNoHcnFQa3q994g1q717U233F7M88h/2nJOB7DoB2Ar761D9kzSNO8H67pPgnVRqOtXXleV55RNkMcgdojszhpCvLkAZAGAM18j6x8Dvi/ochS/8OXxGcBoo/OU/Roywr28dkOKoU4pxbW7tqr3atpfVfqfpvDPiTkmZ4mpXhWjFq0YqT5W1ZNtJ2dm9P+3fQ8sjjeWRYYwWZ2CqB1JJwB+dWtStpLHVLrT5uHt5pI2HujFT+or2fRf2bvjZrMEd7a6K9sjn5WuZI4GXB4YqzbwPwzXs93+xh4zuNalmvtdso4J2DiV1kaRncbnG3gZ3bsHdyOcDtnQ4cxtSDlGk91vp+djszLxSyHC11GrjIWs72fNqmv5b/dvufEtfcXwZ8DwftE/DSTQfGrTxy+HbhYLDUogGkELrua3JYEOqYHB+7kc8c9b4U/Zq+Dnh7XJdH8bX11qN3bJ5zrOPs1tsAByAhLMMc/e7Hik8bfG74babpsPhLQo20230+crGdPI8tYwSD+7TZlmXkDdnPU45rows6WAjKpiJKbd4+zTvdq102k1G2931W6Py7jPxB/tzkwuQ0pupFqSq8tuVd4reXMrpppJp7PY9I13w34Q8GWui/CbQT9kskkEt04+aWR24VpCBku3JyRxkYAAArw/9rW8t/Dnh/Qfh5pIkSBpJbuTcSQwT5VBbuQzE+3Fdp4Y17QPEni3Ttd8Nm6vrKd4ypuh+/ZgNrlgTgFWB6HGBwa+d/wBqzUdRufi/dWF1M7wWtvbiBCSUQPGGYqOg3NyfXj0royTN4YjLsxx8qajN1I0lbVxgkny325Xy7Lre+lj5jw7yStLiDDUsRJtwjKpLm3c7tXa35k5ddrHzbVm0vLuwnF1YytFIAVyvcMMFSOhUjgg8EdarUV8pGTTunqf17UpxnFxmrpmhcpaXBWXTkMbMPnh6gN3MZ6le4U8r05HNV7K7uLC8h1GzYLNbussbEZAZCGU/TIFQq7xOJY2KspBDA4II5BB7VtmfTtemIunitL2TJ83hYZWP/PQDiNif4wNh/iC9a3gnOV4u0vu+7s/w9NjzqqdGHJNOVPq92vXq156vvfdN8R3Flfa5cappsYggvG+0JEDxGZOXQeyvuA9sVlTZ/dqTnbGo/TP9a7/wL8MfFHxB8RDwnpsX2e4CNN5k4ZUEakK5DAEHrlccEjrzXM+JdGl8N67daJfyQz3Fq/luYJBLEGH8O4feK9D2B45xXZicDiPZ/WqkLRk7X6X3aPOy/NcH7ZZfRmpThG9t3y7J38+5mWNlLfTrAjxxBv8AlpM4jjGOuWbj8sn2r0LR734d+C9UgvrmJ/E9zAwkIR2tbNXUgrglDLLgjkkIp9DXmbOzkM5zjp7fT0puc81jhMd7D3oRXN3etvlt96flY7cflTxS5K02oPeMXy39ZL3vua87n7EfDD4n6N8YPCMus2MHlSIzQXNtKQ21yNwGcYZWByDj14yK6T4c63e+F/BesaPq4Xz9AWWYBDkeUyNMuD3AIIr4n/YtvbpPEWvaYv8Ax7vbQzMPR1kKg/iGNfZugR2V98UtY0G4TMOoaaEmXJIfoD6Y+ViO9frXE2YSx2WYDOKz/e8zpvs1JS/9uhF9Ete5/BHiLwvQyzMMZltNfu48s11a2dvulJdel2fhA8z3LG5kOWlJc/VuT+ppvTrX058ff2fdH+BmsJbza/FeQ3ZL2lokZ+2CIHAMv/LNVH3Q+fmI4Xrj51/tJ4TiwRbcDoy/NJ/32Rn/AL521/MmMwNTD1HSraNfM/vTIeJcNmeFhi8tvKnLZtOK/FX020TV+okenssYuL8m3iPTI+Z/ZFOCfqcKO57Gi5QuTGCF7bsZx79qR3eRzLKxZj1Zjkn6k1pxRRaeoub1Q8pAaOFhkD0aQencJ1bvhevNZPY9eU5Q1m7t7Jf197enofsj+zX8S9A8JfBrwv4e+JWt2tnqd5G32SC6mVZjbvI32cEMcgFMbc4yMAV9k5FfzLXM897O9xeO0skpy7ucs31J5/w7V+gXhf8AbP8AG2meGtO024S0nkt7WGJpJGO9yiAFm4+8SMn3r9JynjSnGHs6sXaKSVtW/U/jjxB+jfi62KeMy+qnKpKUpq1lFt3Sjbpq1r287H//0P088TxzxeJdSjuiWlW8uA5PUsJWB/WsPrwa/SP4gfsZX2o69PrNhczTSalcSTO6BQiPKd7FkYDam4nGGOBXzn4y/ZV+J3hSd47aEX6oCwMakEqOpB5U/wDfQr/m9zLJsZh5y9vRlFJ2u4u33q6/E/2Q4a8V8ixdKnTjiIqVlo9PXf8AU8b0RvDGrafLpXiS4ksbqMZtLza0sXHWGdFy4Q9UkQEocgqyn5cHV9E1TQp47fVITEZo1mibqksT/dkjccOh7Ed+DgggV7/TtQ0q6ay1OF7eZeqSKVP69R7jivQPA17rHiqew+FEyteWWpXkaQIE8yW1llcB5rbptJGTImdjqCWAIDDkoONa0OvRrr2T/wA/zPrsRUlhYyxdKXNS3km9Erayi/xcXo9007pnwc8BXfxL+JekeD7ZFdLidXuN2dot4jvmLYwcFAV4I5IFftV8Zn07wl8Lv7F0cCyRjDbW0UIKBVQg7F2/dARfpjivPv2cP2ZLP4DXOoa5q2oR6jqd4BAkqoY0jgBztUMSdznBY57AD38X/az+LFjePHpWhSMWtfMgVv4TKSA7L67AMZ9TxXt8S4eeXZFVwaaWIxHuRV/5tG9OkYtyfofx/wAVcRrjPi3DYfLbyw9GzTs7N7t2drK9o6q9k31PiPx3rS614jmkhbdFD+6U9c7T8x/E5rlrS8u7GcXNlK0Mi9GQ4I/GoTI7f6w7vrz+vWrsFraXNqWScJchgFhcEBwe6SfdB/2Wx7E9K3yjJ6WFwkMFT1jFW16+vm92f2NRoUsLho0ZL3UrbXXz8vPY9n8E/tA/EPwSJLiweC4JK+YJY13MvIGWGD1NegaL+1jr+nax/bl/bzXMjKVeNpv3TDtwAMY6jFfKrQXGnX7WmoRtC8bbJUcEMAeDkfSoY4CbpYJOMNhvoOtehThOmoRpTlFQd0k2kn3te34Hx+P8O8ixcp1auHi+ZXutLq3lb1+4+yb39sjxbDe3BtbIAs2Ar7cKFJ6DB9cE98DvXk+s/H/xZqiSRpGAsshmYSu0g8w/xY+UfT0rwpmkuJdwBLSNwBySSeAB1NWJrZrGZoL1cTIcGP8Aun0bHQjuBz64oxsJ4r/eZymvOUmtd9L2/AjLfDTIMG17LDR57LvfT5/idTrfjfxX4kLXXiC/mmD9E3FQ/bnHO0dOSc9B3xx7yNI2WxgcADgAegFNd2dtzHmm0U6UYaRVj7fA5bRw8VGlBR8krJeh9i/AS+0z+z9Ml1Z2SC2uHDleoKsWUn2yRnHOK3P2pvhT4q1vxZB478L2b39rc28cM/k4JjeMkK5GfuMhHzdBjmvEvgvq0ccl3os3zZK3CL03AfK4/LFfdnxwso/EHwC1V9KULGLOKdUX5hsiZHZfcBQQfpX2Phfg6WIwuaZbWS91qqraSbs+tmraW8nJ90fzLxZjMRk/FlHE0XbnlyvmV42m12ael779D8udV0U6NKtvd3drLJg70tpROYyP4XZRs3fRj9ahtrnR7eDL2rXM+7gyviLb6GNAGJ/4Hj2NZh9ulW9P07UNWvE0/SoHuZ5PuxxjLYHU+wHcngV82p3qfuY+i3/Pr8j+mKlBKjfFT21bvyr8Ht6v1NVfEt5C4eytbK3KggFLaNjz7yByfrUS67rkykm62pnJISMDPsFUc+1Nlh0/S0EMwS7uwQWCvugQDPyHb/rG9cNtHTLHOKd5f32q3CyXTBiBtVVVURFHZVUBVA9gPWtqtepBcspu/ZPT567+n4HFDCUKr5o0ly/zO1/le/3tr0Z9a/Dz9pXVvDOmWvhi0017izC/Z7WS4lJkabG5ixChAgJGI0Axu5Ynmvl3xB4gu/EWsXPiEhY/tDGQoqRr5ef4cIijA/vY5785qpY6rLY6nY3e/MdhIjJnoFV97cf7RyT9ag1C2l0vVZ7RThoJWVWHcA5Uj2IwR6g162a8SYvF4WFGrNuMHborafj1t5I+eyHgzAYDG1K+HpKM6iv1d9Xfe9nrFu3V9iqZA330U574wf0xXYrq/hjXNieJrZ7OVQF+1WCJubGADLA21HIH8SFGPfJqne2dnrls+t6HGsUsabruzXgqR96aFe8R6so5jOf4MEcqOeRXkuc6Lto4vyun/XyaPooUaOMjfWM46aOzi/yf4p6PU/TH9lTwd4T0Tw7qHiLw7qq6tJfSrFI4iaEwrECRGyMSwY7tx7HjGRzX0b4ctZJvitPdSOGWHTVCKOq+ZJzn64yK+bP2O9Bl0z4b3WtThl/tS8Z03DAMcKiMMPqd35V0nhn4v6XonxQ1O/1ORntpotjJEu5gFbELYJHUK351+lcfcT4HK+G8vji0qcZzT0bskr+873dryi9Xpda6H8OccZVi8Zm+YqhN1ZRTV2ld25VaySWlmlZLbzPg39sSw1mx/aC1t9X3FbkQS2zHobcxKqhfZWVh9c18xV+7nxe+Cngb9pTwhZ6j5z2t4ieZY3yJ8yK/VHQ43ISOVyCDyCOc/lV8YvgXf/Ae9sbDV9Rt9V1HUd7W0NtHIAiIQokcP1YscIgBGQSScYP4nxDklRTljabUqUve5k19rX576WP6E8GvFfLsZl2HyefuYmnHk5LPXkVrp2slZXd7NO+m1/DFRNOQSTAG4bBVCPuDqGYH+I9VU9Op7CsxmZ2LuSzMcknkknqSa9y8LfAD4jeL7pf3IhMnzuHJeUAnklB3JPcjnrX1V4Z/Yn3lG1SN3AHzNLLtBOM52RjOCeOvFfPYLAYrGf7lRlNbe6m183t+J95nPiZkmWSf1rEJz7LX5Ly+Z+cYHIzX374E8C+C7zwRo13d2ELyy2Nu7sVGSzRKST9TXrVz+xb4PaOIWqxMpbErbpVKjPVcMckD1xXU2n7L72FpFY6br0yW0KLHEoLcIowo6noK+ry/gzN4uSnhX06w/WR+cZ145ZFiVB0qsla/2X+nof/R/YXSPGnjLQJBLour3tqy9PLnkUce27H519B+DP2vfil4cKweITFrlsOomXypse0sY5P+8pr5Xor/AJ+MHm+Kw7vRqNfPT7tvwP8Aa/O+CMozGLjjcPGV+trP5SVmvkz9BkvfgX+0YJbCPdp+sSx/utOutkatIq4zDKoxz32kE/3etdF+zD8LfBfwk8bas3iGXGtSYitBcqq+VBjc4V87S7nGTxlAMdTX5sp5gYPESrIQwYEgqR0ORyMHvX6K/Cn46eEfindL8OfGFj9nvr22igW/dlL3U0Kj73yjZIxyUGSD0Jz10wmCo4jExxFBRp1k3pb3Z3T6fZle3vbeTtY/APEfgfMMsyyrhcvqTnhJL3o3TlTSad1f4o23Xxaau12vX/2hvjD4NtPDt74TkO+dJEDOcbFZSHwhzy/8IHY5zX5V+Idav/E2pm+lQ7T8saDkIueB9T1J7mvcfj98NNT8M+ILm6hna6httoZSMbEYAh1HZefm9D3x0+bP618NhMvnXzCpmeYWeIV4WW0Em/d83rrLr0SR974L8H5dgMsjXwFTnctW30bSvpfS+h2958NPiFYWS6lcaNdG3dd4ljTzUwf9qIsPw61yFzZXtm3l3sEkJPaRGQn8GAqun7v/AFfy/wC7x/KpWnuJOHkdvQFif619PJx6f1+B+tYani4/xpxl6RcfzlI6DRPEMGnbrTXLKLVbN12+VMSssY9YJl+aNh26oejKRX6M+Hv2Svht4w8CWXiTQru5jub7SBHBNPGow0oys8kQx+9CHYcNgjnGea/OLw7rdzoGqR6nBd3VuUI3G1k8uVgD90Ochc+uDj0NfoS/7a+nweEoEh03brE1jPMiFmeCKVHKwxuSFZt8alywwAcDvke3lE8Pyy9u0fgPjLgc/dag+HoSTb95xlo9OsWrW0bbWj0Urux8UeLfhx4r8I+Ob3wXodlfXE1k5RZlt3WSVQOZUC52xtztIPI6nOa87u9J1XT5DBfWs0Dr1WSNlI/Aiu88catqvxAu7z4lySo011LuvLSJ5Ga2IAHmBWzi3c9GBIRsq2MqT5uZJD1ZvzNeTiIwU3yLTp6H7Dwu8XLDweInF1Ekp6O/Olrd377WVrO60aECOeik/QGtfQvDniHxRfjSvDVhcahckZ8q3jaRgB1JCg4Huaymmnf/AFkjH6k1PHqGpQOJLe5mjYdCkjKfzBrGLV/ePosT9YdN+xspdL3a+drX+9HuPh74KfG7w9qsOuSeHp7ZIGG9rh4YVKHhhukkUcivvD4TXcEtpfeEtRG8ozMY2ZWQow2uoxnPPXHBBr8mrq7vNQfzNRmkuW9ZXaQ/+PE19ofs6aP4on+Ht74y0NmLaNfGMIOcwmNXfaPRSTkehOOld2UZosszWlm2HpuSgmpq6bcGtbKyu47pdbH4D4vcPYqrl31nMa0L3UVywcdW9Ltzl102W9+h5Z8bfgVP8J7sanb3cc2kXUpS2DNi4BwW8spjBCj+MHGMZGeD4jNq0/8AZo0ixzb2xAMqqeZm/vSMACw7Kv3V7DOSf1O8Y+EtC/aB+HyWs7C0u4XMkLr85gmAIww4JR16juORyK/Nbx58NfGXw31BrLxTZtEmcR3CZeCQeqSAY/A4YdxX2fGGQQpcuY5Ur4aqk1JXe+tn1j6P0e1j2fCzjenmdBYLNaieJpu1nZXts10k+73vr5vhKkU7YWf+/wDIPp1P9KSOKaaRYYEMjuwVVUZLMTgAepJ4Fauu6cmj6i+jiZZ3tsLMyfdE2P3iKf4gjfLu7kEjjFfAxpy5XPotPmz9nq14e0jRvq9beS/4NjHroNbt5Vs9Kv5FwLizABPVvJkeLP5KAM+lc/XRSJLd+EobrcG+xXTwFR1WOdfMQn2Lq4HofrW2GSlGcba20+TX6XOXMJctWjO+nNZ/NO3/AJNYwoJ57WdLq1dopY2DI6HDKw6EHsa+mPhz8OfDnx41i3urOM6RNY7G1iOFcQTIxIWS2xkRySEfOhwo5dPSvGfh/wDD/wASfEnxDH4f8Nxbm4M0zZ8qBO7ue3sOrHge36reFvC/hH4K+CV0+y+WGL5pZDjzbiYjkn1Y9FXoo4HAr9C4A4b+sKeKx1lhY6yctm1rpta32n2066fjnjFxzSy9Rw+Bk/rb0XL9lP8AmXW/2U1e/vK1tU8c69oPwy8Dm0sttjHDbmK1RFO1FTAOP90HjJ5PrX5WWnji8j8XyeJZsmO4bbJGP+efQAe6jBHvXp/x7+Kt1431p9KtzsghIEqozFC69FGT26tjq30rxTw/4d1PxLfix05eBy8jfdQep/oP8j4TxFzzD8QValWsrYdRcIrVe51l5OTs1orJRVrq4/CvgWnlmV1MXmXx1VeV+i9e739T9EfhH8aJvC1o0cnmanps8a+QiuB5bA5O3d0ByQR613/ivwpYeKvEY+LXxGgttNsLSxaJWkkYTQQ5LAnjAcs3I75C8muG8K+FvCf7PPgaXxh42IIix5ESlZGuJJFBGARjzCemDhQMnivk/wCIX7R/jX4l2t3oOuRW8Gj3ZUi2jTLxlGDI/mn5mZSMnI2nkYHGPF4J4VnleWrDZzWlUim3TpyS0Sba5+rT7X22Xb8fwHB1TOM1q4zIYctNPlnVvZtO11BbOVt3tfdq+vbeOv2or6GAeGfg7GdLsLcGMXsqBrqUEkll3AiMEnPdj/s9K+cdX8feO9fYtrWtX1zu5Ie4kI/IMB+lctJHJFIYpRgj+R6H6GmV7GJzKvU9xu0VoorRJLSyWx/UvD3AmU5dTSwtFN9ZNJyb7uT11+7sjp/D/irX9Ag1G00u7lii1G0ktp1DsAytg56/e44PXBI71z4urtQFWVwB2DHH86jjBJYDqVNMrilVk0otuyPoaOAowqznGKTla/nbQ//S/WCgAk4HWiptwji8rHzMcsfbsB/M1/zzn+60pW2LNzcw/Z1sbRcRrhnf+KR/U+ijoq9uSeTxRVmRhJGxVlIIKnBBHQgjkH3pKKbbbuZUcPGEeRf8Ofoh8KPH/wDwvXwNeeEPFrJ/bWjRCVJ9oZrmEAqcqeSxOFkx1yDjOa+JfH/hG68G+IpNNmieKJ/niVwQyg9UIPOV6fTFTfDHxlcfD/x9pXi2EnZazr5wH8UL/LKp/wCAEn6gV9hftW+E9OmjF3pMKBoYvtasinL7mJcliTuJHzD0H4V1ZxNVKMcylL3otQn/AHk/gl6rZvTTe+lvwfCQXDXEywdBWw2JTaXSMl8SXldp+XM0rW1+A1GRuPAo3ADC/iTTevJorkP33lvuXtL0651nVLXR7MZmvJo4I8f3pWCD9TWn4kniuPFN0loAIIpjbQgEYEUB8lPb7qgk+pJqlpd1qGjyR+ILEmOSGQpE+Okm3O4e6A5HocGsogbdvtiruuWx5ioyqYl1LrlSsvVv3vyj+JqpLq3hvVpFhY293atJA44PrHIjDkMrDIYHIIpNP0m81Zmh0qPzZkUv5Kn94yjr5a9XI67RlscgGpdeuFvNR/tBTk3MUUr/APXRkAkz/wADBP41ko7xussTFHQhlZSQQRyCCOQQeQR0puydnsFCM6lBVoWjUaV3b8H1tdvrpcb0JU8EdQetFdXrs+teKln8cXkfmEyRQXkygDM7Idsjj+9KEJY9C4b1rlKmcUnpsdOBxTq07yspLSSTvZ9V/l3Vn1Cv0g+Ft7L8K/2U5tV1EeTca5JcTQqeGMcihQ3sPLTcPqPWvhT4e+ANe+Jviq38H+HlHnXOS8jZ2RRL9+R8dlz9SSAOTX21+2B420bTvDWn/DDSCHmtNscrBcbQkartHp8uM44GQOxrlzWo6eAqckuWdS1OP/bz95rzjG8vlfofiXizXjmWY4Dhyn7zlNVJpbxjHa/k2/w8z54+F3xc1XSL5rjU3B+xKbgTHJwgZV2Oo+8PnA9QK+7vDPxY8M+K7JLLWREEnRi7sUa2bHb5ic5HYivyUjmktrV0jOPtI2t/uKwOPxZR+VXNH1/WdBl83Sbl4SeqjlT9VPBr1uG88x+RyvlUkoPWUJawf6xdla6+47uMvB/B5tKVaj7klt/Xrc/WYfBL4TzSjWdF0e0trkoxguYV4R2BCyIobZuU/MpxwRXxlffsdfE23Zhp97p90q8KWkkjZh6kFDgn6n61zvhP9onxVoypZSiQKT/y7NgZ/wCub5H5EV6hpv7S0MGoPqU1+xmZAhE8RdQM5wuz5Rz1x1r7bMvETJMZGlHH5fUg1v7PlcVdatWab2VrxT8z86y7IOM8jq1FhantLpfFeeivbu0tXs/kee2f7JPxcnuhBdCxto/4pWuN4A/3UUsfyr6V8Ffs0/DTwRayxeMrlNWuLuMJItwwihA3BhsjDBshgMMWz6YzXH6L+0Le+KvFFtoWl6qsVxfyLFCGhKw+Y3CLkqxG48ZPHqRUnxLTWfDOtSweMriGO/lRDFJKxNuzycRo8iqfKUkFdxXap+9gHcPFr+IGR5dGNXLcBUqTlLlTrWjHW/qrvoml69Hx57nXE+NqxwGZV1R5lzcsItSdnvrro+zt5H0Fdaz8PfhPoaWmiwW9tbK25oLXaPlxlpGx1IHdjz0zXwl8V/j7N4vvJl8PmRUb5UdxtEadtq93I6sce1ej+LvgF8d7zQ7rVWVZIBYRzLY2jKZWnkKq8BVSQ/lgs24MQwAwMmvje8sJfD2rS6brEAku7RzHLAx+RJF6q5U/NtPDKD14zRxbxJmebRjRx8FSoppqnH4dP5nZc3pZJW26nveFPCOSVKk8Wq6xFZdnfs9U/lre19LvYy1jLDzZSVT1PJP09T/k1+hX7PvgKy8I+E7jx34zskWwW2e5d5v4Qg3AhCPm+XIz03HA7V8afDbw3e+O/H1lpe0SNLIGbjCgDoMAYC5xwOgr73/aK+IfhLwJ4FX4SX1vLJLqtkY2+yMo8hARiUh+u5xkLkbgDyK83h7L6VWtLEV9KdLXXZyfwxej06vqm4s9jxgzXE4qvh+HsKm51XefLvGC3ttrZN67221PhX4sfFfxB8WdfOpaoxisoSws7QfchQ/zdgBub8BgcV5dWjeabPawJeoyz28hwssZyA3911PzI3+yw+mRzWdXn4uvVq1HUrO8nqft+RYDCYXCww2BilTjokunr597633Jy7TRhTyYxgHvt9Pw7e1QU5HaNxIhwRyDSuADlejDPHb2/Cue56cVyuy2CPG7n0P8qZT0OG6ZyCPzFMFBSi+Zs//T/Xq1tbez086rqSbjKCttETjex6ysOvlp26B2wOgasYnPJq3eXU99dPd3sm92wPl6AKNqqOwVQAAB0AqGOYRSrKkaMUOcONwP1B4I9Riv+euUk7JbH+4+EhVUXUqayfTouyX+dtXr2Sr7l9Rx156UqkMMoc/TmvYbP46/EbSbQWGgTWemw4wUtLG2iDD/AGv3ZJ/E1gaj8Std1ljLrNnpVzMQR5z6fbiT6koqgn6iuqdLDW92b/8AAf8A7b9DzaWNzRy/eYeKj5VG3817NL8WcBHBPduLa0jeaST5VSNSzMTwAAOTmv1p+IJns/hPofh7Xo/9Lmgt45lbJ2tHCBJz0zk4/P0r8pIdW1e0vxqum3D292rb43gPlFX7bRHtC89hxX66/E+DWG+HljLqk8ayIsBnQp8zz7Rnaw+7htxPHNdbi/7DzB03ry+Vra33a6X9Ol3ofhvjlVn9fyyNVK3NJ7u9/d8ttvXrZH5B3ts1leTWT9YZHj/75Yj+lVq98+LXwV8a+FGTxrHaveaTqqC6WeBGbyWkG4xyqMlSOzfdI754rynwV4am8Z+KLPw5bNhZ3LTOOfLgjG+aQ+gSMMfrgd68TDUKnLBSWrSf3n7flXFWCxOXvMKdVShFXk10tun2a10Y28kSHwnY2DY82W4muyM/MEZI4kLDtu2MR6jB6EZ5sVe1O+XU9Rn1CMbY5nLIv91OiL/wFAB+FUGYKpY9ACfyrWbu9D18toShRSnu7t+rd7fK9jd1l7SS20w26BHWyjSTAxllkkG78VxmsOt7xJYXOlasdLugA9vDAhAyBzCj9/XdWCSByeKdX4mTlFnhoSi7p6r56/qdH4e8QDRVvbK8QzWOowGC5jBwcAh45FPZ4pAGX8V6Ma0vBnw88W+PfFCeEPDdqZrwtiQ9I4l4y8jchVGc+p6AE8V6F8Hv2fPGnxb1GGUW8llopOZr6RSqlO6wg4MjnoMfKOpPav0r8SfGL4ZfCe0ey0mKOZoAiTeRtRVEa7Rvkxh2AAXHJzxTr4nC4WisRmNT2cL2V95PtFbt+ST66H4lx/4pf2djZYHIqSr4ma95J6QasouVtL2umrp6RTexy2heEfAP7KXw/nv0kjuNYuUHmXE3yNcOpHyr12RJnOB+JJNfln468V3vjPxNd69fuzNJI+N3UfMck+5OT+navQ/GPjXVvjJrv2SXUxE2SIVv5PLWUgnYolPyKf7qvtXPO7JrPs/gj8TdU+yagNIuFhuHmEztG37lrbmUyccBlGYyMh84UmvMw8cTmFWOOq03CMVaEN+VPeUrfblp/hWm7k3n4f5JRyKpUzHPsQni6usnLSys2kr9LLpppbyPJ7nAl2D+FVUj0IUAj86hVWdgiDJParSwM+26vMwxzfOCV+YgnkqpxnnI6gZHWi6uLd3MdhGYocAYY7nb3dhgZPoAAO3qe+3U/c6Nd2VOmr+fRf5vyXzsRl1iRoouWYbXYdMd1H9T36dOsGSOlJRSOunTUTS0fWNQ8P6xaa/pRUXVjMlxCWG5d8bBlyO4yOlfpp8Zrax+JPgrQfGpsPtFpqFkj3s9vkJtkCkJuyWUq24BiPl71+XNfqB+zDHfeKP2atV0C9yI4Z7uG3YnjbtWUD1AEhNcGeZRLMMtr4Km7Stzxemk4ax37tJPra5+BeO1COEWDz2Gkqc+R7p8s076rX3Wrr7z38fFz4VeCvAcWopfgWNjYRzRwgM1ybdHFujCM4dvnG3J789Oa/Jv4g+JLB/H2peItH0jT2sNRkea0L20hilidifMCu+N5PEmBw4IwK53x3I48X3PzkgoiAE/dVkB2j2yc49ayLDX3TTG0DVw91p58x4o93NvO4A86LPAJwA6n5XXrggMPXwXEzzLBUcRbl5oxl82k/luT4c+EtLJVLHU5yquqtU3b3Xrpa15J9W0mr6J2v8AXf7JVhbXPiw+IzBFas5kjUQqVT5U4GCW6lj3HSvn7486/N4k+MHiC/l3YiumtkDdVS3AjA+nyk/jX1n+yI9xpvgjV9ZgO5YI5G4Pyl0LOB69F9OK/PJ55buRry4YvJMS7E8kljuJ/M110ZyjlUFJ/HUnL/wH3F+Xkd/BOEVfi3McW/8Al1GNNeju/wBAVijbh9CD0I96DtPTj9abRXmH7lyq9xcEdaliAcNGTg4yvuR2/EfriogSAR2Palxj5lPA5+lNCkm1bYVPvfQH+VMr3T4X/APxp8UI/wC3LFFtdJIlUXbsCvnKpAjCqS/3yATjgZIyeKrX/wAK7jSr+fS5bTz2tpGiMi3tuocoSpYK0gIBxnBAIr6HC8J5jWoxrwpPlls+/n6eZ8RjfEfJsPiJ4apXXNHdLWz10fn3W667n//U/WdzbyP+6XygexJYf4/zqJlK9fz7H6VYa2mkt3v4IyYUYKxHIRmBIB9M4OCeuMdarLwfrX/PQ092f7oUZJr3Ht07CUU5ihGRx+tBVgu4jj1pG6kjo/BWmtrHjTRtJUZ+031tHj2Mq5/Sv1T+PWq+Vb29qLlViDNJLBgBgACVcnPIxkY6Z75r4n/ZO8AXXiz4mQ+JZYz9g0LMzuRwZyCIkHvklz6Ae4r039qXx/O8k2lJujZl+zRxsF3KCAZmypOQeADnvXqZs3S4fnSd1KvNRW+y1b9LX737Wvb+Y/EP/hW4vwuX4d39lFuXk5f5Kz8jP+HP7XOraDFFpWpEG2RuFmBceX/dWQfMgx0yCB9K+gvDXxg+GWo2bNf6TBbzaokkF7d2MUajy5MgfOuZG4I3Z5zz7V+UFbOhNepeSPp8rQvHBNLlWK8Rxls/pXwkcszXCW/srFtJbRqLnjutE37y2tu7Ju1rn1HFHgXkuIjOvQTpt6uztff9ddU9bH2VqPwH/Z1vbCeTQ/Emp2L2vQTxLMZeoHlpsQnp6jqM4qa1/Y58K6lHp72PjWJl1AriJ7dRKysMlVCyn58cYIwDXyfbfEXxZbLsa4WUf9NEUn8xg1qW/wAWfFNrMtzAIUkQhldVZWDDoQQ2c152GzXiWnUtiMLRnH+7KUeur18tEtNdWFXgziulDkwuYy/7eUJdO7gnvr+R+gfxV/Zl+GHjnxbNqUWuSaLeWtrCbtAqOnlIvlxud+0K21ADg84Bx6834Z8Bfs3/AAot7LW7qKTxDdXMZmSa6EcgiKdMxDCRlj0yCR1zivk3wf8AGSAeK4rv4k2pvdMmb/STC7rMuf8AlpncS+3up6jODnFfZfxW/Zz0nxP4VOueApGltpo1uYogTwpQsGTd8z7sjCsOme9enmGc8RypSxdDC042erTdScY3vzclop2Ss7Nu7ulpY/H85y7H5PLD5LnuPqxoSSSaso6KzjzJ82js7PS1lqeb/Fb9ro3xi0rwWJIIFVRKsThSWH3l85QcKOgCj6+lfGPiTxP/AMJEQ88l1ncW2PIjRjPoqonPuSa5u/sLrS7uSwvU2SRnBH9R7HtVdUz8zcAd/wDCujB5dSrTWPrT9tUeqm9bL+4lpFf4UvO5/SnCPh3lGUUY/UIf9vX1fn5/iXLOLTpbuOG9eZYnbDGKMSvz2VCy7ifTNftx8DLCHwz8K9M0efUJbwWcPzPchUlhU5dYpVDuEMSEKVLHaBX4fJNJE26AmNhwGU4PPv1rtW8Q+I/C+maXoljeyQIm/UWijO0LJeJ5Z3YxuLQBcg9nI9a+0yrHqg5Scbnyni/4d4niKjRwlOuoWbdmk72T1vo10W7WqfTX2z9p3WfAOr/F59c04zalb3FtCzS213GYJQoKYiYRuUCkEMOfmyRjNfN9/NoE0+7TLa4t4/7skySkfj5Sfyq/omsWVrayeHNaiD6bPKrM8aKZ4GXjzIWPtw6H5XXjhgrDnpY/KkKKwkUE7XXOGA7jPI+h5HeuLFYj2snUstT7bhHh+OX4enl7cn7OKSfNK0l0dr2T7rp00Hf6CQCfMPr90Afjzn8hWjpMnh6HUYpdehubm1B/eRQSJC7D2kZXA/75rHormTtrY+sq4VTg4OT17Oz+9ar5O57dP4y+BVumdI8FXU7lcf6bqkhQH12xKpP5ivvH9k74g+G/Gfg/VPB+h6JBoX9nYd47d2eOUThhv3SZcv8ALhixOeMHsPygr9Cv2EraK0h8VeJLnAihW2iZj6KJJG/TFe3lOLbr8srJNO+iWlvQ/n3xz4SwdHhqtX5pynFxceapUlq5JbSk1s30PjDx3O1j44muogGa2aFwDyCYwrdPqK5zxBbx2uu3cUC7YjKzxj/Yk+dMf8BYVrePby3v/GmpXdoMRvO20ZzgDt+HSs3X7VLae0aOXzVmsbWUH0LRgMv/AAFgR+FfD8J0PZ5Th6faEP8A0lI/ZsoXJDDcys3C1vlF/hr+J9x/sZ6roos9Q0m+VPPlkKoWOCwYAtGB33Dn8K+ZviL8EvGHgLxBdQajBFa6a1xItrcz3ESRvFu+Q8tuyFI3AKSPSug/Zq8SaJofj9bLxGqtaXSbgSGJSWL5kYbSDn8+K7b9sfS538ead4xgmFzYalZLHA6ncivAx3qD0Gdwb8TX3eAnTqZXKnVs5U5XSTSlaW7krPS+ia9Oh+P4f63l/HFXD0p8sMTFNuSbTcU7KOq1+Le/XR3PnfTtD8H75/8AhINfWBYpCifY7WW6Mqr/ABgkxKqt/DuO71Aq8NO+EjSADWNWCYO4mwhJz2xi5/nXntFeV9Yja3s1+P8An/kftNXKK83d4ma9FD/5C/3s39WHhGOQp4f+3SqP47oxR5/4BGGx/wB9mk8M3el2PiKwv9Yj3WsNxG8wyMbAwLZBVgRjquDkcVSgsoWtXvLyYQoBiNQN0kjeir2Ud3OAO2TxUulabJqrzlmCQ2dvJcyH0VMAD6s7Kv406cpqpGaSvul/n5evQipGgsNOlObaSs3d9dNOl/Tr06H6b6t8e/hl8MPCNxpngD7IkxtFvLKxMTJBN5rZwpUEK453KSGBGDX50ax4v07WtWutYu9A0wS3czzPtSQDdIxY4HmdMmuTZJnshctlgknl/wCyNwLgD3JDGoPMf0U+5XmvsM949xuMlFq0IpaJJW/G/p8j4HhLwjyvL4VHO9Wc3eUpN3022t663eu9rH//1f1v0/UJ9Oldo/mjlQxSxn7skbdVP4gFT2YAjpVEgD7pyD0PqP8APWkqxBLEqvDOu5H5yPvKw6MP5Edx7gV/z03bVmz/AHPnTUG6sFq9/P8A4b/gdivVzT7LUNSv4dN0lGkurmRYokXktI5CqMfU10+kfDrx9rzW40XRry7W6UPFJFCzRupOMiTATGepJ4719p/Cn4W+Gfgbnxj8SbuJ/ECxkw28eJVs1ZfvtyAXx1bICjhSSSa9HC5a3+8xD5Ka3lLReivu30R8Lxl4h4LLcNJUpKpWekYR1bfmltFdW/Ra6HrfiDxh4d+EXg+TwVo9tFZT2tvH58kUYSAO6DzXGCCWJ5HqSPSvzK8UeIZvFmoy6gylFh4iU9os4598nJPufQV1XxO+IV/411CZ4WJs/M8x5AMeZIx+83oP7qn/APV5bFIIm3OMqRhh7H/OR715WJzDEY/EfW8ToldQja3LG+l/7zVrvysfPeGPh/8A2Xh3i62teesu9+v366Pa/qR1ajd7aBpIzhpg0R/3CBu/Pp+dQSIYpGiY/dOM/wBaluAVMaN95Y1BH15/kavY/Wq1p8sej1/r8CvTkVnYIgyScAChVZ2CIMk9AKlD+QpWJsuwIZh0APUKffufwHqQ0q1Gvdjv/W44GO3Gflkk/NV/ox/T69vrH9nD9ozU/AniZtE8dXtzeaPqBChpGaZoJywCuMkkI2cOB04IHBr5FqSGWWCZJ4G2PGysrehUgg/mK3w2KnRmpwZ8zxZwfg84wNTBYyPNzLfqn0adtLeXzv1+xf2qvhVF4P13+1NK5iZfNUHlljLEMp/3G6H+6favjckk5Jz9a/UXxPe6h8ZPgfoHjV7VL24MEn9ozQgL5e0FJV2Zzjd8xAyB16GvzI1Swm0vUp9Nn+9A5T6gdD+I5r43LHHB5liMpgrQVp09GlySSbS7qMrrTRXS6HwHglntavl0sBjXetRbjLv7ra9fvtuVreCO6uI7aZxEkjBWc9EUnBY/Qc1u+L9XtfEPijUNXsIzDbTzN5CHqsKgJGD7hFGar21ij+Hr7UycNFLbQr/218xj/wCixWJX1jbS5fn+Z+sUoRqYmVXrC8fLVRk/0+7zNnXIbZLmG5skMcVzbwyhSc4Yrsk5/wCuisap2F1DazhruBLqE8PE5Khh7MvzKw6hh0PUEcG/fWlwmhaZfyMpjm+0RoB1XypBuDfi+R7GsOnJ63QYKnGeG9nJ3SvHd391tb73037nQ+IdO0aymgufDtxJcWV1EJE84KssT5KyRSBfl3Iw4I4ZSrYGcDnq7LwbY6XrdxdeG7/Cz30BWxlZtqpeIQ0SsTxtmAMRz0LKe1ccVZWKOpVgSCpGCCOCCOxHenOP2ktGZ5bXcZSwk23KFtX1T2eno0/NXsroSv0o8L6Rb/Cz9kUa1ayiO41tUvLpzwWE33Y178RqB+frXgX7Nn7PuofErXIPF3ii38vw3ZPvYycC6dOka9Mxg/fbpgbRyTj3n9ojQfib8VoHsfhvYNeaFp8vkYikijTdEo3lFZl3Lk4BHAC4HWubNqclg3h0m5104K26i178+9lG+vdpdT+f/E/izBZlm2GyONaMadOanVk2lFWekG3pzdbX003a0/NySR5C80pyzEux9zya0tZjFvqDWQyPISOM5GDkIM5H1Jr1iP8AZx+N0l9b6fN4bu4xcyLH5hCFEDHBZ2DEKAOSTivY/jH+yr8UB8Qr/UvBWnf2jpt/L50TpLGjIWA3K6uykYOcEZBFenSyysqbcYPS2lv0P1HFeJOQ0sdSoPF07OM3fnjbRxsm72Td3bvZnx5p99c6ZexahZnbLC4dT7jt9D0NfpB4f0jwD8fvg9beD4rhrS/RjcxuRuaK5+bcQOMpjIZeOCOc4rzXwN+xlqL41H4s6tDpEKAym2t3SSVo1+8WkPyIB3Kh69T8WeIfg38CorLS/h6iW9xA6yyXCSeYJlcDckjZZpSwwTjATtjpWH1+hlVWOMxfLr7ji9ZSjJq6ilrdWuvR37r8g8R+NMBnOJo4bIJSniacuaM4L3Fa+7ekl6JrV+h+evivwbrvgzxZceC9cjEd5BJsznCOp+7IrHH7th827sOvINZOo21jp1y1tZ3Ud8UODLGrCIn1TeFZh6EqoPoa/T/xbpunftD/AA6m0a1mtrTWiBNAVG7KxMGVWcjcI3zzgnnB5wRX5f61o2q+HtWuNC1yB7W8tXMcsUgwysP5g9QRwRyK9HMMJRjTjicJLnpT1Uu391+a63Sfkfp3hzxzPOoSo4z3K9PSUO+3vrTZu6snZdW7ozSSTluT61pRXF7Z6XNCgKRagAjH+8kThiPcb9ufce1ZypJIwihG53IVR6seAPzr1b4z6faeH/Gg8GWAAi0GztrE47yqnmTsfdpZGJrgpUX7OVXtZfN3/RM+8xuMg8VSwLV+ZOT8lBx19eaUbfPscVp+tC38M6p4ekjVlvGtp0c4yj27sMj6pIwNc5XUeDbO01HXf7PvSAk9rdrk9mFvIyn8GUVzSAOgc9SM/wCeampdwjJ+a/X9TqwUYrE1acFq+WT+a5f/AGw//9b9hLvwZ4ysLtrC80e+jnU4KNby5z/3zXr3gP8AZq+JXjO5ibU7f+w7F+WuL0bHKjr5cJw7nHqAPU1+kHl/Fa3tysOq2twcEYeIp9MMuMHv0618/fEix+J/lxf2movbkKVty5zGSeWGY1znA7jkd8V/gTxFlVTKqSxFbB1pLTdRUVdpe9KLnbfTTc/0xo+NWZ5h/s+GdKlJ6c13J+sYyUV97a9T1vW/H3hL4GeF7PwjZjzIdOtUjQtIAFx3cAElmJ3YUc57V+bXxN+K2ufEXUpJp5GEDnngBnx93cB0Ufwr275Nc/4w/wCExmu/P8WwyYXIQgYiX/dK5UfzqHT/ABB4Xg0wWGqeHbe6lXOLhLm5hkOf7wV2Q49lFeJjc4rZrKNWvNci+GK1jH5pe813e3RI+14C8N8LlC+vuLxFaW8k49dW/eklvvr/AJnNWeoX2nSNLYzNEXUo+OjKeqsDkMD6EEVrWNvomtXEFlJLFpMjDa00hdrZm7FsB3iz3IDIOuFFUbqbRJY/9EtJYG9fP8wfkYwf1qnbi0M6LdebsJ+bywrN/wABBIBP1qYys0nqj9brUVNOryuEu6tf7k2n5J3+89p0j9nv4r+I4LW603S2kjnuhaNIrBowu0MtwJB8rwMp4kQkZXHUivJtS0u+srp31KGS1V5HC+ahRm2NtO1WwTjoewPGa/YL9mCx0Tw18L7fRrO+llcytM1tdPEZrUzncsLLEzKmQCwXrycgHgfE/wC1l4t+Hvivx/Ya7oB/taMW/kTTwXZETGF2Bh2hCUZc7iyn5g3TvXt4zKoQoKqnrpufz7wN4tZlmHEFXKZUuanHmSlFW2u1fmdvesra7LS+58k3dxbyYhsYzFCAAdx3O57l2GBz2UDA9+tU629RvNAuZN+nac1ov90XLyj83TNZWbX+6+e3zDH8q8GW/wDX+R/ReDbjBLka9Wm/m03+bIaME9K0bCbSIbyObUYJbiFWBkiSQRlh6B9rFc/7pr2GX4hfB63t1XSfh9AZlGN95qN1Mv1Krsz+daUqUZbyS+/9EcGZZtiaMlGjhZ1L9U4JL15pxf3Jn1z+xGl3r3w18T+GLwg2ZudkeTnBnhxIMdQOFPvz718MfFO0jsfFsqdG8tTIe25cqT+S19+fsd/FfTPEc+oeArTQLHRmgjF4rWAdVlG4I28SM7FxkYYscj0rjtF/Zlf4m+PT4u1S/hTSLS+kgntgrGaT7PIW25+6FfIBOcgZwK8/OcudXNMvnR1fJVi3tpem+vY/mPKOLKeR8U5ni83j7KL5Zcu921p8N1eVr72vufCOrLNpFsnhp12spS5uMj5vNdPlQn0jRsY/vM3oK56vXfHnwy+K9rr+p65rugagqz3c8jSi3dkO6RiCCgIwQRjtivK5bK/thvu7eWJR1Lxso/UCvQr05Rk00z+leHc1wtfDRnSqxlKWrs09Xv8A8Dy0Ll1Iy6VZWu7I/ezYz0LsE/lHWVXufxJ+Efi/w1oXhnXE024e2vdJtzI0cTP5c+XZkkCglSVZSMjn6g1W8Bfs9fFr4iTD+yNKktLbqbm+DW8QGccbhuY+yqa2lgqvPyKLb9DzcFxtlMMD9cqYiEYXlq5L+Z/j+J4rwOW6d6/Qv4Wfsa2+vrpXi7x7dMltPbQ3E+nKrLI0pGWWRy3CngsANxJI4rrvC/7PXwn+A6W/i34kXjazqsZ328WwLEHXHMcX8RUnO+RsDrgGvKfin+1Kde1CawsHlewEgH2aBhGGVTnLy/NlvoCB6GvLzHPsPl83QcPbV9P3cX8N9nN7RXrfTZM/FuI+O814mqRwnCfNCmr81W1r30tC6uvV8rv1W79k+PH7QekeBdGk8BeD7byHiDWjptVAEUbCkajIVQP4vwAyePj/AE74zWf2RtJnFza21ww81FbdEccAsqkE4z6GvJde1bw7rOoTakI79JJTn97PFNj2z5UfFcqfK7FvbIH+NfI55wXDN5/WM2m5VNUuVuKjF/ZitNO7avLd22X3HBfhNl2AwCw1SD53rKT3cu+ja06H6O6H8WfiWl/b3GmPca3a2Vm13GLZPNt7iCMBGAaLBaRQwzG2JBj7vHPH+Mf2hfHPh7WdR0/VFubW8vYIJZ4RGE8iN0zGoLn92dr4Y9c8E5Fdd+xDokVo2qa9Y6lcG3ulWFrWaDyYpJ1O4yRN5jeYY1G0kKCN3Pt2f7aV/wCErnwCNMup2fULa5glaC1liEiB1dY3njYFjFkkDHRiCDX0lPw2h9T53jK1ld2dTy5d1rbl0tfTU/AZPK6PFayV4SNSN1HmUXF6tPWLVtHo7Wu7a20Ph7xf8b9e8R2ttBA7eZaxiBZHCsEiXoiDAz7sfbjvXi9zdXN7Obm7kaWRurMcn862bqbwg9qqWlrepOB8zvcRMpPrtEKkfTcaxP8AQz0MgGf9knH1yP5Vll3D2DwcnPDwSk93q5P1lLV/Nn9gcP5RhcDS5MPR5Fr2/Rs9d+E/xg1j4Z6krxkS2xDJhl3GMP1KjuO5H5eh+1NR8HfDT49+BtNTUtVhOubPKtr2Hmbd94RyA/6xQODnGDnBB6/mcotiPmLr+AP9RXvHgb4hfCfwRFDeWula5PqK4LML6GOJ3HQ7Ej456dT7mvo8qxEsNOTjZwe8ZN8urV2ktpOy1PzPxG4KdWrHMspjOGJT3go62TtzOTStrrZtPqup7R8NP2SvF+lfEWz1LxbNZy6Vps6zsYJd7TtGdyJsKgplsFt3b1zXyH48v9X1Pxpq2q+IYpLa7uryaWRJVKMrM5+XDegwPwr7m+G/xd8T2d1fa3faY0Iv38wx3EgLMSc7/lVSDg45UD06V6tq2veN/GugHVP7KsbiyDHEcsQuHODjIWQHP4CurD53lmOwzw+DjP2kHKUoxi6jstLtqyslbrpdrzPgsFx5nWXZrLFZvCFS8YwTUlBLq0rc1229e9lslY/OX4TfDbxF8UPFC6NoAKRpHI010QTFCNjBdxH95sKB15JxxXRS/s3/ABvglaE+Hp32EruR4ipxxlTvGQexr9EfDGi/EKPTYwt9DpEJORbw28SbV5ydqqAD0/rW8dF+ImePEK4/690r7LLODcRWw0KjwVZ31/5dRetvsyqXXz1POzjx4zOOMnPCSpRhZKz55bX15lFXvf00Vurf/9f+hbxn+1L8T7fxB5ehS21tbpb2+YzAsgMjwpJI2W5+8xAHYeta/gj9sPxJaXywfECzivLVjgy2qeVMnvsyVce3yn3r5O123eB7J5X3yT2VvK+cZG5SFHH+wF68+tYdf4Pz4+ziliXVVd73s9Vbtbb7j/YTDeFXD+IwEKMsNHa3MtJXXXmWu+up+uU+leA/i34XGr+C5LaXI2rIi7VB6tHKmAVJzyGGR7ivgP4xfCS98C30l7bw+XFw0sancqBujof7hPB9D+lP4BfEJvh58Q7a7u5/J029/cX24/IIyDtkYf8ATNsHPpkd6/Sjxp4U0n4h+Ho77T5I5yYi8Eke10mR14Ut3Vq9nM+G6PEGClnGV01DFwspQjpGaXl3s/dfdcrb0PxaricbwRnMcPKo54aeqb7Po+l1b8b21aPxxEbDl+B6n+lKJWVv3JKHsV6/n1rs/H3g648H601qwbyJCxjL/eUg4ZG/2lP6Vw+13+SPhjwCeg9z7CvyfDYmNWCnA/q3BYyliqEcRTd4yV0dcNQ1fw3oVhbWF1JAbqUap5cZ2hWjJjt5OMHdgOwPYEVV0rX30truxuYlubDUNouYCACwVtytG5BMci5Oxx6kEFSRT/GOrWGs+IJLjSUKWcEUFrbg8Ex28SxKxHYttLY96o6nFbGz0++tECCW2CSY6GWF2R2+rDaT9a7uazfK9F/wx42EwtOdKH1inaVXV9Gn8STtreNtHe6avdMz7yK1jupRYSGW33kRuy7WK/w7l7NjqOmenFVqtWd29lcLOqJIB96ORdyOPRhxkfQgjqCDW1r1r4fMFpqnhtpFjnRlntpiGe3mQ8qHGN8bqQ0bEBuobJXJjlunJHr/AFqVKpChNNp6KXml17N232e2jsnzdFFA54rM9I+5v2E7At441zW24S1sEiLdh5su7+UdcL4w+N17o3jvU7fTPPjsvtj3EfkSshDs27dt4BPoa92+DGh2fgD9ljU/HEUvkXmuBpZJG4Plo5iSNfqgYg9yxNfnxPrk19NM+qRrdLKTjzCd8Q3FgIn6qBnG3BX2rxOJ8opZhWo4DEpqNODndNr3pysrNNPRRd+mqP5w4fyqhnnEeZ5lOHPTi40knprBJSa6PW9tvxPsjS/2otfZpb067LGZR5ZE6DahPIKgKVBwOD9a7nSv2pbm00WDTJbyyuhE/wA887FpHGd2DuON2MgEg49K+TPAvwin+JeoRaZ4Dv0uhLGzXKTKIp7RlUlGkTcRJEz4TzIycbvmVTgE8cfAr4k+ANKtLvxFp0kaPA91cyrh4LbD7Ajyrld2ADgddwAyc149HgHMaLdfCZhXS13kprWzdrp+Wu/nuZY7grg2rilgakoxqNr3WlGWztpaL6O3e8X1V/uS9/a0sr7xLEvhvymt/KdFt3kBeSQ4O9igONoBwAec8n05/wCLfxE+LnhP4d6f4u1bAt7i7DxS2sxD5lVnjD4JAjHIUcnOAa/PXQ9Yt9DvobmGMO6SKXmbkhAfmCKeFyOpOW9Nvf8AVvwbo2lfGH4H33gDUJPMxGYIiQoMTKA8LrtJJCtg7iATyKzo5di8Tm/1DMMZUnCpCUopNQXMmrx91J25eVpJq2vd3+G434KyvhirhcTHDqVHmSm5K8nHVNrXTfrq9NrK35Y+JfHfifxUdmq3TmEZxEGYqMnJ6kk8nP17Vx9SzwT2109jcqVnjdo2TvvQlWGMZ4INNwq/e5Pp/jX1eBy6hhYeyw8VFeX6+frqf1/gcLh6FNU8NFKPRL8/+CNAZjhRmlyF6cn35FBYkY7enam12HXZvc7Hw/q2uaNbX3iixu3glihNijjlv9LBV0TP3QIlc8YxxjrVSPxVrS67beJbxkvbiKJIG+1KJklijXy/LlVvvqYwFOTnGCCCAakv77Tk8HaZoVod1wJ7i8uWHQGQJHCn1VELH034rMSC0l8OzTKn+kW9zGS3PMUqMuMezqP++q3lOStGL21+e58rSwdCXPiK1Je83DZX5b8qV/5ZPX5jdUj0gtHdaKzCOVN0kL5LQPkhkDfxJ3RuuOG+YZOVSqxVtynBFdXPa+H9V0KbV9PJs9QtSnn2nWKWJjtM0LE7lKtjzIzkAHcpwCBFudux67rfVIwpzu4t2vva70v17K+vm+r5iGGa5mS2t0MkkhCqq8kk9AK+tvhP8HZHvY0KpNqTIZMuf3cIGOFODz6t69MCuY+Cnw8udRuINUeMfarw7LYPwEQ9XP1/l9a+/wC10zwT8MbE65qV1FZ7lWJ57mUIjN1wN3TJ7DtXVwjwdieI8U0ny4Wm7VJXs31cYuz20v2v2PwnxZ8SnRby3Au8321u/wDL835Hkms6t4D+BtnHrPjGYXmrSxHyrKPa7Ekn5kyPlXHBduOuM9K+evFX7XvjnVraO38KWkOjEbvMc4uGYdE271AXaOvByfQcV86eNrrxPfeLL698Y+b/AGjNIXk83rg/d29vLx9wrlcdDiuXr6WfE88BTeX5PH2FGOll8T/vSlvzPra3Y+k4W8Ist9nDGZnavVet3rBabRjs4+t776HpqfGn4uxzG4XxJf7m6/vePyxgfgK7eH9qL4zQwpEdQgfaANz20ZY4HUnjJPc1890V49LiXMYX5MRNf9vP/M/QK/BGTVbe0wlN2/uR/wAj/9D9dNbaE6pJHasXihCQoT1KxqFH8qy/pzUtxJHJcSSQghGZmUHrgkkZ/Ctm50u60TT7a/v4ws2oRebbITllhJK+cR23EER56jLdNpP/AD4OMqkpT6K7f3/1Y/3Fp1o4elTpv4nol1bt/lqylfwQWMn2O3nS5+VWkkjzt3EZKKSBnYeGPQsDjgA19lfsf/EOW21G6+GupzEwzqbmyVj92Rf9ai/7y/OB6qfWviStzwxqWraR4l0/U9BLC9huYjBt5JfeAAB33dMd817nC+fTy/MKeLgtE7NLrF7r/LzsfN8b8KQzTJ6uCqu8mrqT6SWz8uz8rn3T+0/4Kgms5tSt1JaRTdLwOJI+HAx/eXk18L6VZRXGl6peyEZtreMp7tJPGn/oJNfrZ8WdN0rV/Dz295JHHdRK0kSM4BYcLIAD1+Wvye1q0u/Dlv8A8I5MuyWcR3FxkYOPmMKeoAU7yO5YZ6CtOOcjjgM9xUKduSdpqzvZyXvJ9nzJyt2a72PynwRz+eJyv6jf3oyj/wCAp3fysrerscvW5JbTf8IrBqJYGP7ZNCF7q3lxPn6EdPoaw60nBj0aFd3+tmkcr6bFVQT9cmvl49T9/wAYpc1Pl/m/R/pcza6rwa2gya1/ZviUiO0v4ntjOxwLeRx+6n+kcgXd/sFq5WkJAGT0HrThLldy8fhPb0ZUbtX6rddmvNPUs3lnd6feS6ffp5c8DtHIoIIDKcEAjgjPQjgjmvpD9nj9nzVvizrMWu61EYPDlpJmaVsj7QV6xR9Mgnh26KMgfN09n+EP7Hth4u8PaJ438d3U1rHNEZJ7BU2PIm9jFvkLZUNHtyAoOMYIr1f43/HrSfhZoz+BPBVsLSe2UQRhNgQRhRgRKucDB5YgY7DNdGOq0MvprE4y7u7QitZSe6X4a3slZttI/nTi7xcxGYz/ALB4Z9/ESvGc9ows+VuPfumrpK1rvbxn9q/4q6fJfxeANAEc2n20aoYY2KwkjqR5ZHA4VcHGAfWvhNniLllj2g9gxIH55NW7+9vdXvZNSvW3ySHLHoBjoB6ACqm7YR5fUdT/AIV4uXUayU62Jd6k3zStsnsoryikkvS+7Z+wcC8HUMky6GCpO7S953er6vc+wP2Rf+EIsfiDaavrrGy1DDW9kz3Q/wBJlnBTYtsI95G3J3ltuQOp6fVX7SfxS+HN/wCArjwXFJbavqGqwedZW2ZHjlMUu07ZISMSBkbYNwJZcex/LXw/HPALzxDHI8R06HejoSG86U+VCAw5HJLdeimsRJpo4ovKcobd8xlSQVJ+bKkcg5GcjvzX1dDNnSoeyUd/6/zPzbiDwepZnn6zeeIleny6a6NXkldttK7i2k+rs46W1xrdl9h+w/2TZEjI80ifzfxPm4z/AMBr6w/Zb8eaZZ+K7CLWmKyW2YFIIA2yjYjNk8qucH04NfKOta2viPbqV8h/tV3c3M+Rtuc4IkYdFl6hiBh+CcNknN0rU7vRtQj1KybbJE34H1U/WvhuJsoliY06tGzqUpKcL7c0Xez8ns/v6H6BxHwlDNsrq4OonCUk+rdnqtG+j6Oy6XSei++f2zPDWqeEzDrnhGwgsNO1Z3Gp3VtEgmluWI2iWQDeEdQeAQGbO7JxX57V+rfwt+LvhD40+AZvBHxGdJGutlnsYHfKZOFB2ghXQgEPxng9c18E/Gn4LeI/g34kbT79WuNNnYmzvMfLIv8Adbssij7y9+o4r6d5nRzCn9cw3pKL+KEu0l0/J7q6aZ+ZeCnEf1Lm4ZzSPJiIN2b/AOXi8m93FaW/lS7O3jVbnhnTIdZ8SafpVx/q7m4jjbt8pYZ/SsOt7S2n0q0bxMigsrtbW5YZHmshLPjv5aHI9GZT2rGmryR+9ZvUksPKNN2k1Zer2MHdv+f+9z+dbmj2t1dWmprb4KxWZmkB/uxyxcj3BP8AOsIAAADoOK1dPMsVve3Mb7QLcxtjuJWVMflmiDs9S8yj+5tHuvzVjLro/COkvrfiS009cbS++TPI8teWz7EcfjXOV9Afs/8AhufXddnaBQZHKW6ZOB83zuSe2ABXnZjOqqTjQV5ysorvJuy/Fnm8V5nHB5dVxEnay/PQ+8vh94b0rwh4afxfrMYjaOF5gcHMNuq5IC+u0Z+nFfmv8UvidrvxT8Sy61qjMlqjMtnbfwwxE8cd3Ycu3UnjoBX33+0X8U9G8AeFv+EMa0e5m1q1mhVY5BH5UO3yy5JDc5OFGOcHJr8zrHR7zVAF0sLM/QRl0SQnH8KswLf8Bz6fX9g4yw1HLMJQ4dy56QV5pbylZavu3v1+Vkfg/gjlSm63EOYxs5O0JStZR1vbtrpd2623YkurXtzpsGjXsjS29qWMAblot+NwQnkIcZ2dM8jBzmlPbz2xUTKQJFDoezqejKe4/keDzTJI5oJGhuEaN0OGVwVYH0IOCK19M1C3C/2XrG57GTPQbngY/wDLWIeoP3lHDjIPOCPzZPnlaq9drv8AXy6eXof0NOPsYe0w8bx3aXW+ra8+vn6mLRV2/wBPudNuvst0ACQHVhyro3Kuh7qw5B/kc16jpHwol1XSrXVBehBcxJLt2ZxvUNjPtmiGEqSbilqjSpmlCEYzlLSWx//R/WWFYjJvuAWiTlwOMj+7n36frVjUNRvtXvnvr5vMmkx0HACgKqqOyqoCqB0AAr6V+OfhH4L+FbKPTvA1wRqizec8bSOySQzZwUfaUIjK7QoZdvIOSK+YnaVP3ePLB7DIz+PU/niv8Ac8yupgKzwk5qVrN8rur+e2q7Pz7n+1XDPEFLNaKx9KlKN7pc6s7d1vo9HdaPTsej6Z4f8Ahrp9kl54z1uea4YBvsOlRCR1zztkuJcRK3qF3Y6dRXJajqelpqS3XhK2l01IiDGzTtLOCDw3mYQK3ptUY9awKRiFUsegGfyrkrY5SioU4KNuq3+9tv7rLyPTw2TNVHUr1ZTb6NpRXlyqyf8A29zPzP1e+H+rz/Eb4D2mta6gur02kyO7AbjJAWTfkjhiFBJHc18MeL/hF8WtZ1O88X2uiXt9ZXT+YlxEnmBkwAuFUlsADHTtX234b0HVfh78BtP0fTzG0zQh7lsnj7Tl22dMkbgP6V84aT+0Nc6FdNb6fqtzaLbb4ERgWiCglflUbl9wSMiu/wAZOIfqONwnt8NUqVFRjz8kU3rs9fitaSeu5/LPAmIzHD4vG4jIKcJw9pJJSUtI3urcuqvp8lsfLl14T8W2KNJe6Tewon3me2mUD6kpXtnin4IePYfhZ4U8XaZpNzcLPb3H2mOKJmliL3DPEzxgb9rIRzjjvjivddI/al1PRtKfSodXhlYuzedch3lGTkqC3GM9Mg4zxXrcP7ROveLtSs7zwZZ77eFHZ49zP57bOTkAHYn3gF5z1PavzfLvEHLZ0v3tKtGb5VyunK929bO1ny7va62u9D6fiPj/AIqhUo1Vg4RjCTbbk7SXK1Z3UbX5tN9Vrpv8MeBv2bvjD48u1itdIl0+3PJub9WgjA9gR5jf8BX8a+xvDX7OPwk+CVvb+LPiLctrupK/7iIpiLzV5wkIJ3FT3kOBxwDXdWUfxV8aILLxGZ7Owu5zcm4jXa8ZRRsUfMCqZAIBHJya8/1bUf2edKhmXV/F6zXSOWkeOXznLk/NhEVgSe5yT3r7GhSzGvhZVsBh1B20lWko676Q66NNPmt56a/A5/4gZ3nNX6rOq40+sMPFybT096e+uqaVtOmpxXxp+PXjLWdam8J6ZY3sW5F22axkZDqGBlYfe9cDgDgnrXwnqt3qF5fyXmrOZJjwS3QY7Dtge3Ffed18YP2Yo7h7KNdSkQoUE6RyFM/3lDMG3e5XHtXRaZ40/Zl8WWz6emrixklKbTdxGEgqu3AaRSmGPzMN3J9BXl5ZwRiqdeeLqVadWtLeUqt3192N4pRV+kUlrqfZcKcQvIMPGFLKakY2V3yNPzb0k/Oz17t9PzZLM/JOabX6TeIv2ZvDOo+GP7dsGhvptpcm2ARXQd43RuSBzg5z0r5C8Q/BzULRvN0F2mRsExSjbIAT1B4B/T8aeYYipgakaWY03TcldXs196bX32/E/WOGfFrKMzuqcuVp21/r80jzldWgtvCMnh2JP3t3dx3MrntHDGyxIPq0js3/AAGoNJMc1jqVg6gs9uJkbGSGgcOcH3jLg+tL4oit4PE+pQ2n+pS6mWPjHyK5C8fQU/wrZjUNfhsfNEJmjnQMcYyYXwOf7x4/GvRjrJRvfofVuNOOBliVpf8AeO/dWevokl6I58jtXW2F/o+qaXNpGvR4vEi/0G8U7WV05EMwxiRHGVVj86EjkrwOQQ7kVvUA0pGQVPfjiphNxPWzDARrxs201qmt0/8ALutmtHobeg6/qnhvUU1LS5DHIhBIyQDg5wcfoeoNfpD4E+OHgr4ueEZPBXxWjW6guAEZ3A3IR08zHzKynlZV+pxzX58eMJ9K1dbPxVpgjie8iWO8gTjy7uJQrsF7JMoEq443Fx1FWvht4B8Y/EbxTDoPgqNjcjDyT5KpBHnBkkcdF9hyx4ANeVisqxMMSsTls+Wrs9LxmuinG6va+jTTj3tofl3HnCmWZzl6x+OfsJ0tee9nBrfXyez66NXuj6u179jSw1O5in+GHii1uobosYobz7+E+9teLdv255ygNcT8a/gJ418Hw+H/AAn4W0671e0s7F5Z7m2hd1e7mlZpmIXOMKsYUHnaB717v8UdB8M/AnwnZXVnq11Lr8ESrI6vsaUNkM+APk3MNoAPTrnBNeM6B+03rFrZQ6dFq91ZJCcqsih/+Al8MSvsa8XF8cVsJOrh8ZgZNxsnKi+dJ6O1nZru100Wu5+UcLZ5xVjY0sxwdb6xRpuSXPBxbdmua8Vd6PRta3d9T5Dn0/UrWZre7tpoZF6q8bKR9QQK9a8DfDbxB4y+GHinxDoVo91Jps1ltWNSzuoaQzBMDkoCrMo5wK+ttH/am1O21KbVNQ1C0vlnTakTt5caYPBRRz7HOc+vFdQ37VekQeEmsrJ7W31V3YmWIxrCqls71XJJYjgg9+c9qnLvEbJ5qc6qqxaUvddN3dtFa11d7pffY+n4g8QeJ6lKNGhl6UuaD5udtaSUmmuVO2nK356XPzl8P+BvGviq+Gm+HdIvLuYnG1IXwP8AeYgKuP8AaIr7f+DXws8Q/DXxRZ+HvGIWG8lZrzbbv5gA2kKrsBj+E5xke/NaHiP9shftks2nmMRNA8Swx75CHbH7zf8AKNy4+XsOetc78HPiPqHjrxVbalqtxcAwyrao7tvdkKkqrE9VJJDda9fKuIsJicywio0puCq025yjyRspx6S5W79+ltbHlcW57xNmOXVZYzDRo0VF6XcpN2dtdFZdkvmeeftjGb/hZdiGz5f9mx7PTmWTdj8a+SyAeCM1+gH7ZvhS5uNL0rxtbJujtGe1uGxyqykNGT7bgRn1I9a/P1ZI2+6wP0NfqPiVg5Uc6rKf2rNejS/W6+R+peDGYU8Rw5h1T3heL8mm/wA1Z/M6CXxDfXsUUGsqt+sChIzOWMiID90SKwfb6AkgdgKgeytbuCS90qQKUJLW0jfvVXsVOAJFHfGG9VxzWcIJSNxG0erfKP1xSCIMPvIfxr4/27l/EV/z+/f5PQ+8WEpwd8O+XyW33bfNWd+psWcsmo2n9h3RPmxAm0z2YnLxc84k6r6P/vGvp3w9LYW2gWNtLcMrR28SkByACEAIxivnjw1eQ6Tqdq85s9SgndUltZY5JSFyOVAVXVh/C0bhvev1kg8ZfDJ4UcXunMCoOcI2eOucHP1yc1+k8DcNwzCnOcqyg42Wttd7df626H4n4mcW18srU4UaDlGV3pda6X+y93r663d3b//S/WZSZEMR5Iyy/wAyPx6/X60xJCo24DL6Hp+Hp+FIjFGDr1BzW1q+kx6ba6feRS+YL+BpiMY2FZXj29eeEBz71/z2QhKSc103/r5n+5dSdOE1Tl9rb1s2/TRGjpvhK68Q6ebrwy63lzF/rrIHFyo/vxof9cnrsyy91xyXeAvBt/8AEDxhY+EtOB3XUmJWA/1cSnMrn0Crn8cDvXIglWWRSVKEMGBwVI6EEcgj1r9dfghpS3HgjS/GetWEMGualaJ9ruRGqTTKCdjSEAElk2sc9Tya+64H4Wo5xiVSd48lnLqpK/Ts381u1a1j8t8S+NMVw9gnUVp+0vGD2cZWb1/mivk1one7ao/GnWE0HwrFo9tErJsLJk/MogUbAB054Gc1+Tk0F1DcyW95GyTBiHR1KsGPUFSMg19R/tEeNlvdQudLtWBe8kJkKkkeShwOGORvx06cGui/Z7+DWqeNVPibxM5FvCU8iSVN8iqAfuO3I4OQpyAMHg4r5HPs6rZ1nFbFUItuT5KaVvhhfW9lZNtyu9r210v8bwHXpcL5A8xxrSU9bPdvpbf+lcZ8Fv2dL3xTPb+JvGtu0dkF3srEKpCjqc9Txz/COSTngdV8Qv2hfBXw+lk0D4K2sM99GnkSam4LxpjqIweJGHduE9N1eefHf45f2vbD4XfD65ZdCsV8ia4B+a7ZTyNwx+6B7fx9fu4FfKNdSqUcFFQo2lV6zdnZ9VDTRLvuz1uHuB8XntRZrxE37N/BR1UVHo5ru/5fvvsu3vPiV8QdR1oa/qGtXs11k/M0zBcMMMNgITaQSCMYx2rinia3drZhtMZKkemOKaQCMHvV/U7lb27+2L1kRN/++qhWP4kZ/GvKqVpTV5ybfnr6n7RhcBRw01DD01GLVtEltstOmr/plCjPaig4AyeBWJ6Z7j8CfjBefCbxK1zezTS6PJFIZ7NDkPJj92UDEKr7sfNkfLnOa/QDwT40+GP7Q1rNf28ckV/bQmJ7eYgSwq5JEibTtYZ6MPoQO/5deINA/wCEb02zsdRUx6ncA3M0R4MMLgeQjr2dhmQg8hWXPJxWb4e8R674S1iDxB4buns7y3OUkTr7qR0ZT3U8GvpsFnHsYrB42CnS6xavv67Nduj7M/EOMfC3B56p5ngJunXfwyWila61tupfzb2s9Vo/rj4xfBi5NxcNDC6zwuUhuihVJwoBCsemcHg9j7V8h6fbyQX832hWje0imkYEcho1PB9PmIFfp38JPjL4a+PWkN4Z8TwJBrVuhdoNzCKYAY82IA87TyUOSvUZHNeUy/ASfXfFmtaOkiw3moaRcw2zSK0aTzK8ZRxkZAIQhs/Xmvn8Rw99Qr0/qkufD1W+W+8H/K/LovPR7o+M4W8SMTllOtlOfx5J0117bNrura6fLrb4Ext+UduKK96sP2Y/jtqGof2f/wAI9LAQSDJNJEkQx337jkfQH6V9SeCP2PPAvhlU1b4tawl7IoL/AGSF/JgzGMupbPmSbe4Xb7jmt45fUs5T91Lq9F+J+pcReMfD+XU+d4hVJPaNO05P7tF82j56/Zw+BMvxjvdTk1fzrTSIYDF9qjVcm5LqyrHvBBKqG3ccAgd6+7b7U/hr+zB4IbTPCMcDyB/9IEkymYvtJEk3G5vQKAMZwoFeG/Ev9pfw34b0IeBvhzaf2daQjYi2+1H8vnICgYiDdc5L/nXxP4i1298cOtw1z+/MmFs2zliejiQna7Hvu2kds182+J6uJX1bJo2i1rXenypxkve8pW5equfkFbhjOOKsX9czlyo4NyTVPV6JaOTXfu9F5db/AMS/iNrvxL1eTWtXmd1WQhVY9AfunHbuAOw49z5qoLMFU4LED8zirSxS2141pdo0bZMUiMMFSeMMD0IOKjthi4HmDOzLMP8AdBP9MV3YLBQoU1Tp/O+rb6tt6tt6tvdn9K5XgqGDw/1fCxShFe7bsJcMDMQoICfIM8HC8dP1qGpIoprmZYIUaSSRsKqjLMx7ADkk1K8QtZGimAaVDgrnKqR1yeh+g49T2rq1Z6PtI00qe7tt+v8AwRqIscQnmGS33E9R/ePt6ev0r2r4FeJZNG8Yh5cNkJKq9swtnaB2+UmvD2ZnYu5yT1Naei6pNomrW+rQfegcNj1HcfiOK5Mepuk/ZO0lqvVO6/FI8fPsm+uYKrh56uSa/wAvxP1Z+O+seIE+Dt/rXgllZikcjtsWQ/Zif3pVWDKSAecg4Ge9flrqfi/xTrNuLTVL6SaMEEKQgAI6fdUV+mnwY17w/qmlTadI6ubsBo1dyUliccqEJ25HO7Ayc1+ffxh+G198MPGtzorxt9hmZpbGU8h4Schc/wB6P7rD2B71+tcUZzXznK8NntN2Ulyzim2oyT66K1+t121d03+BeCUsJhcXXyXE017VPmjJpXasrpelrr1eiszyw8nJ61JDDNcTJbW6l5JGCoo6licAD8adb29xdzpa2qGSSQ4VR/M+gHUk8AcniuhvbnStGQ2Ph+Vri4KlJ7sqAuSCGS27hCODIfmYdNqk5/OKNBNc83aK/HyX9aH9G4vGuMlRpK839y832X4vp1tVuru306F9L0hldj8s10By/YpGeqxA5GRy/U4XC1dh1O7t4UgjmdVQBQA7AADjAArF0jTX1fVrTSIuDdTRw5HYOwUkfQGtPVvFF62qXLaTbRJamV/JUoMiPcdoP4Yr0sLGNVOVSSitl/XzX3ng5jfDtU6UHOW8n112b9bPyVtND//T/WCvbdD+G/ir4kfDuw1HwdZG8udKuriynRCqsY5Ss8bDcQCFLOp544NfU+j/ALGPgy0vfO1jV7y+gH/LJESHPpl13H8sV9LaBo/gn4c6GmhaN9n0+ziJIQuMlzyzMzHczEcknnGO1f4n8P8AhdXpuc82kqdNqz95X3TTvqlquvmrH+oHGnjtg3GmsmTnUjK93FqNrNNNaSej9Ot9LHyL8JP2Tru01KDxD8TmiZYSHTT4z5gZh085x8pUH+Bc57nHB9s+OHxS0zwTobWtrdPHdqwDJFgEgg/Jkjgng8dBycV2+oN458cWctr4QsmtbXPy3k0nlCZcfwDBO1vXuPSvhn4yfCvxadTDauskN6i/JHK26KRfWN+mfU5Pvivg/EHxPyzI8IsqyuEo0qrtKvaVuqspab7KXw2bsmtT88yPFviPOYVs/wATH3doK2i3tbWy73u+70OF8DWMnxW8Zq2oabbMRs3NH5wLOcBARvYHoSflxgdK+vvjX4w8DfCjwPd/CeG7vV1DV7d28222s8Ic4DuCygK+0rtXkrk/Xgv2WPDdrpVlP4n11Ps8en+dcTl8qV8sEKf+AgMcHr718d/EDxlefEDxpqPjC8ypvZi0aH+CJfljT/gKAD614OQ1YYfCyx8UuabcIeUErSat/M9L9lufpbyKGfcSSwqk1hsLZ2Tvef2VrfTRv8LanJNHtGQQV7Ef54+lMpQSOlO+Vz/d/l/9auE/oxXS1GVN+7a3Zejqcj3B4P5HmoiCDg9adEyLIpkO1c/MfQd/0osFRaXXQZXsXw0j0rw5oOr/ABT1eJLiXSjFbaZDKN0b38+Srsp+8IEBkx0zjNeU6jZyabqFxp8xBaCRk3DowB4YezDBHsaWa9vWsotKeUm3t2eRI/4RJIBvbH94hQM9cACuvC1vY1OdrVXt69/lv6o8XOcE8fho0KcrQm1d94XTaX+Je76Nsjvb291K9m1LUZnuLi4dpJZZDl3djlmY9yTVailAJ6Vyt31Z7dOnGEVCCskbPhzxDqfhLX7PxPozmO6sJVmjI7leqn1DDKkdwa/Wf4rOqafpXjjTmNlf4UKCSJNkiZ2geqbuc+tfkCHKNmIkH+8OD+HpX2r+zR4qn8aaTqXwi8QytOyQteaZJIdzxMnEiAsenIIHbLfh6lGDxWBr5ZFe9NXj5SjqvNN2sn3Pwjxr4YdWNHOopWo3U+7hKyfyjq2uzb9b/iz9o3xFok1nDYatb3s2mJhVt3HyuF8s+aZly8mM569+9fLmueP/ABV4lkMMJ8pJWJxEeSXOTlz0yeuMVJ8WdDu/Dvj29sbtPKkYrIy5BwxHzDI44YGvN/avzrAcJ4OahWxKlUktf3knKzslpF6JqyW19F2PreC+CMqw+Fp4rC04tySd7Lrr0snq2dxqXw1+IWlQC71DRrtYiARKsfmIQeh3x7lOfrXHXFpd2rmG9geFv7siMp/JgKhQmL/VEp/u8fyqXzrmQjMjsR0yxP8AOvsrRWkf6/A+8wtPFx/jTjL0i4/nKR0mjeIdPt4303xTZLqNnKu0v927gA/jt5euQP4HDI3QgdR+kU/7HvgDxH4YbUvDt7Nb3uoafbRxzzRgorAIzzGIYIkmUYcbuCWx1r84vBvii58J65DrKXN0ixOrPHaS+U8oU52GQhgqnoeGJBxjuPvrWv23LWHwz5elaci64+nxXIDbmtkndxuhYfK52xHeGBAJ+XiveymphuSX1h39T+e/F7A8RfXMP/q5CUdfeal7t7x3i1ZJWvJ6p9T4Z1jwR4s0jxVqPhjQtOv2eykeMnyHWZo1ONzBc7VcDIAOCO5riLnTdSsnMV3bSwsvBV42Uj8xXZeMJ77xNPP8Qo5xImoTySXECSO72kjNna4bkRsTmJs4I+XhhiuD8yXOd7fma8SqoqTUduh+3cOyxE6EZVJRcrJS0d+Zb3d+/ltqrqwBJCMhSR64Nbvh3wp4o8X3p07wrp1zqM6jJS3jZyB6kgYH4kViNNO2CzsSOmSeKsQ6lqds2+1upomxjKSMp/Qiphy397Y9nFrEum1QcVLpe7XzWl/vR9U/D3wL8ZfBALeI9GuLHT4T5qzTtHGYnX5gVDPlgT2A619dSaz4B+Lemx+EfGFn5jTDOyUAASActFIpyreh4PbnpX5LXNzc3r+bfSvcP/elYufzYmvtP4OaN4u8Q+H7K0srS5uNShkZy7Y2rECphJY42sOc7jyMVz4bjSrw9ifaYNKUKzSnSkuZz1+ykt7N30tbVn87+JvB04U45pjK0Y1E9HBOHK7XvdzlfVatW6dWcV8avgxqHwc0l5/C8k15pmqMUuLhkHmQRqQUgZl42OeWbADFQpHr8uAEjI5r9kdM8dC8kGkeKNPksZX3RzNOu2AkDkZYYO7sD+ZrmNT+BHwQ8ZGS7TSrbe/3pLKQxHJHXETbf0r9or8GYDOmsTkGIjy7ckrpp7taq/XVNaO+p4nCPjhPLqPsM7oylK/8SNnzdr6paJWunqktOp+ZHgHTbm/8Qm6tiFGnW1zfOx/hW3iZgc/7+0fjXHRHbGqk8gCv1Dsf2V/h7pEeox6Td6hb/wBpWr2jfvVbZHIyswXKZOdoHzZ4zXHN+xf4LLEprWoAHoNkJ/8AZa4cX4T5v7GFOEE2m2/eXWyW9uiPtcF485J9Zq1akpKLUUlyu+l29r9Xb5H/1P3V1z9rX4halbB40CCfflTI20bTjgIE4/GvC/EnxQ8a+KFaLULwxxN96OEbAf8AeOdzfia4+7Xy7KzTPJR3/BpCB/6Dms6v+bDMMRVxM+fEzlN95Nyf4tn+2WR8G5Vg/ew2HjHV9OzaW9z6r+E/7Wfjj4Q+BT4P0a2j1BxdGWOS9kkdIoSqgxIikEAsC33sDJwK/Qjwl8SNB/ap+Fs1roghs9atmj+0WszFjBJuHzqwGTG65CsB/snkV+JlfX37GkM2ifEt/iNqt/FpOhaTBJFeXNxIIopHnXEcGWIDNkeZgZICg45Gfey3H1MZF5ZivepVIuLXZNWvfy/rofkXi54X5TTwNbO8JH2eJhLnUld88r/C4315norLf1d/qDVon0r4H+O7VHdbi3g8p8EbMbwh2nGcnBBz2xX5xHrX622Ol2XxK8A+LNNsdVhm0zVGuEtpUIaONXJkExIA+UnB69B+X5S3llp9rdvbQ38dyqMV82KOTY2O671ViD2JAzX1FHLPq+VYOMPhUWlstpPzfS3/AANlPgbm9OTxtKd/ac8ZPRveCWumjuno/wAdzLoqUpD/AAS7v+AkUeXFkDzBz14PFcLWtj+gParz+5jQ5HB5HoaNoPKcj070vlgnAdeuMnI/pStEyvsVlY+qsCPzpC9pG+hqam6XaWV4rZLwJDJkdHh+T8fk2HP+FZGGkf5ecmvvn9mL4S+GviF8NNTl8RWUKyvcvAtwzgsg8vAlRGJCSLvYbgBuAGc4r5G+J3hmw8F+NdQ8H6N58lrYyeWstwm2WbAGXIAA25ztxwRzX1Oa8M1qGCpZjJpxqdr6P+k/+CfnXDHHmExWZYjJKKfPRvvta/ddrpJb+mp5+yhe+fp/jQWYjB6DtVmKynkuFtnKRFu8rBFHuSTxU95p0dm2wXdtOf8ApjIzj89oFfMKDtzH3/1ukpKDldmbXvX7Mc08Pxy0EwZ+Z5lfH9wwvurw6KO0IJuJSpxwFTcfzJAr6i/ZI17wfo/xNFnq9vIdQv4mgsbksCiMRlk2Y4Z1GA2T0xjnNevw/BPHUbyt7y/B7fPY+N8SsY4ZBjOSm5P2cla3dNX1tdK93bojJ/ac0W50rxmJbvaZHeUMUyV+YiReTz0avmqv0Y/aB+GXiL4meKp4tNe2szp8S3TyXMhRPsyrseQsqt9372MZx71+f2o2umQ3/wBh0G5fUVHAmETRhz6xo2X2+hYAn+6K8WWE9hUrU18MZzSv2Un+nU8Hwk4oo4rKKOHcr1IxXMknZX1V3sr9FfXWy0dsrYQMtx6eppSy42oOvXNdVbeBPGN4vmRafKAehfCf+hEVrRfC3xlK20wRx/78q/0zXnTznCR0lVj96P0SrnuCg/frR+9f5nM+F9Dk8TeJtP8ADsXH224SJj6IT85/BAT+FRXdxP4g8RPcW6gNfXG2JBgACRtkaDtgDav4V2UPgXx7oDvf2EAaby3jDQurMqyKUcryCCVJUEDIya87ngvbCbypo3gmjwQHBUgjocH0NdeHzChWjalJP0Zy4XFQxOJnVoVIu0Uo63s9XJtdn7v3E9pe6hpM8otn8t3R7eZSMhkbh0YHgg46HuARggGprLSL/VIpX0qPz3hXe8SHMmzuyp951H8W3JUckY5p+vTQXGtXN1bf6ud/OGO3mgOR+BYiqFrdXVjdRX1hK8E8DiSOWNiro68hlYcgg9CK6m7Oz2O5KdSj7eilGcknqvwfX/L8HBRXTa7/AGjraSeOZoVWK9uWimaMBUFzsEjjaPu+YDvAxjO4DpW58O/CzeIdXFxPGZILcg7QM+ZJ/CmO/qf/AK9ePn+cUcuws8XX+GKv69kvN7LzMqud06eEeKq6cu6vez6q/r/mehfCj4Saz4guo71LN7u5IDxQAcRqekkmcAc9M9OvWv2B8GeGbLwf4et9DtFAMSjzGHO+QjLMT3yf0wK8B+CuiT+A/D2r+O/GIOn2oiDkzDaVhhDO7sOoHoOpx9K/PL4u/tffEfxv4yj1PwTez6Jpeny77OGI7Xkxx5lx1Dlh/wAszlFBxgnmvJ8P28NQXEGbc3t66soNfBBSdlFPa6s23vofxzxHl+cceZtUwmClFUaO8m3y3a0Wl7u90rLu2ev/ALXn7UN7qOpz/Cr4ezvBb2cpTULpcq8k0bcwx55CIw+Zv4jwPlBz8q6N8fvHWkgCTypyP48NE5+rRkA/lXVfEaPSPjR4aufjP4bgW112x2DxFYRfdIb5Uv4QefLY4WUfwtgnuT8zVfEspYjFe3qu/WLXbpbqrdfO5/RHhxwZlFHKI4D6ulODtUUtZc+l7tbp6OLWjjy2PrXT/wBr/wAe2UaxvDvCtvGJmPbGDuU8e3TPNWV/bC8bKoXyZOPWfP8A7JXyDRWP9t4/lUPrFSy29+f+Z9E/C/IG2/qsfx/zP//V/TbWUkhuIoJFK7LeHaD3DRh8j2O4kfWsmu++JFnqGn+IodM1OJoZ7TTtPidGGCCLSI8j6GsDT/D17daadeugbfTVfyzdMpKM5GfLjHHmPjnap4HLFRzX/NhVoy9rKMelz/cbLcxpfUqVecklJL5trp3b6Jb9DLtLX7QxeRtkUeDI+M4HoB3Y9FHc+wJFrUNWuL+GCxyVtLXPkQ5yqbuWb3d+rP1PToABXurpZVW3tlMcKHKqTkk4wWY92P5AcD3pVk5WVonfCjzyVSottl28/V/gtF1v+qn7J1+niD4WXHhWIgTXNpJCCW/iAeLCrjnAYEnPpxX53S281nM9lcrtlgYxuD1DIdrA/QivqP8AYr8Vz2niSXw5Gkbu7gpvzuVJRiQqfUFR9c1xP7R3hm58MfGTWopY/LivpBew4GAUnGTj6PuB9xX6Jk85Vslg+tKco/KXvL8z8O4Pl9Q4vzDL5bVUqi+Ta/KX4M8Poo74HX0p5QocScH071zn7m3YZz2pxHGWP4dTQWJGOn04q1p1hPqd7FYWw+aQnnsqqCzMfZVBJ9hTjFt2RlWqqEHUm7Jas+jv2dPix4M+FsupN4htrjzb4IFmiO8ER5IQx4AH+/knkDGMmvMviz42HxA8Z3HiOK7up7eQAQrdKq+TH1EaKhICrkj1J5JOc15sGR5QY/uE4Xtx2ra0RdFuvN0nWn+zeeP3N1yRDKvQSAf8sn+6+OV4YZwQfoanEWKrYGGWNpU4u66a6799+p8OuEcFhMyq59FSdWSSfWy0Tst9Etl6JPYrXOg61ZabHrU9pKtlK21LkKWhZv7okXK59s5rLyT1rRttR1nRxPY2lxLbLJlJokchHx2ZQdrexIPtVzSpPDc7G28QxzQhvu3Npgsh9XhbCuvqFZG9zXiKEJO0Xb1/r8/vPqI4mvThKpVXOunLvb0f6Nt9EYNe2fs5aVc6v8bPD8VsM/Z52uXPokMbEn88D8a8rj0PULy/+waJG+pFm2xtaxu+/PT5du5SfRgCK++/gp8Lrz4OeH5/FHiiAt4j1tPstpZofnijbkhip4YnBfH3QAOpNepk+FarqvU0hB3b9HolvdyeiSu23omfB+KHF2GwmUVaEJJ1asXGMevvKzbT1SitW3a1rbnpOmeObK18Wa3e+NoTcedbvYpaLyrAvtMW44AUjOScZya53wB+zroGj281zLHLYvM7lYwVeVE3ZUGQ5yAOAAB6nmu8h0fwx8KfCV14r8XGOV7dPMlkIDYJ+7HED1ZmwB3JPpX59/Ej9oL4hfEOSW0Fy2maY+QLS2O3K+kkg+dz68hfavfwXC1DLcNGtxV+9nO8o0k9uZ3bqS0u9kkrRWvuu2n878H8O5hm1WrTyOXs6Xuqc23yvlWijHq93rrrrJX1/RWwb4Q6S66ALzTHuEPKzTQvKWHrubOfb9Kr6t8Tvgv4Nuv7E1HVNPtJoSQYVG8xk9Qditt685r8fiiEYKj8hVqRHMSXeDhyULdi69efXBBr38N4m0sPTUcFgacLaLTZdtEvLqfoz+jxQlVTxONnJPfRJ373bfn0+Z+uieK/gZ4li/d6lpM4c7j+9jjbPTPJVhWT4r+DvgbVtBkmhtpb3cMxKjLIGLcLtLDAUeoPTvX5MEA/eAP1r1zwP8ZfiL8O/DU+keF7pYrR50YeZGJRGxDEqm7IXeFyRjtkY5zy1uK8nzJyjmmBinZ2nCK5k7bq+vnvv03OTMfA3HYGMauT41uSa92V4rfuv8j1v4lfs4+IdOb+1dPVUURIuxMtEPLQLywGVJA5LA5OTmvlW/06+0q6ax1GIwyp1VvfoQe4PqK+8vhT+1j/AGzqMHhz4lQRQNcMI0vYcpHuPAEqEkKGPG4HA7gDmtv4t/AGTXLKe/s9rOsjvEIUO+JDlsEE/Mo6FR9RXzmecIxhRWPyOUq9H7aa9+npu9nJeib82j0+HvEHMslxUcq4lioq3uy6NbaPrbrd39Nn8E6Prv2LS9Q8P3cfm2epIu5c4Mc8RJhmX3QkqR/EjMOuK/Qb9lDwrFpfiBUMe42lo8jvjIEshUE59cZA9hXyZ4O+BnjLUlu/E2rwC10nSGDSTsQRNICNscQ6sWJGScAD34r77/Zpdo7rWJHgHl7Ii0+cbcFvkx1ORk/hX4lmuP8ArHEuVYBv3Lup3vZPl+V09dup5vjhxFhamV4mngKik9Oez05nZel+Xdelzy79sn9ofwZH4Y1f4K6QZrrVbhY47iSIqIbciRZCjsTlmKjBVRxnk9q/KA9a7H4i+Jv+Ez8f634sAIXUb6e4UHghHc7On+ziuNr28+zaeLxDnLZaL0u7H7B4W8C0MgyiGFpX5pWlO/8AO0k/RK1kv1Oq8F+Mtb8BeIYfEmgsnmxho3jlXfFNDINskMqdGjdeGH4jkCk8W2mhpqX9p+FQy6ZeZkhic5e3P8du5/iMZOFb+NNrdSQOWpwLY2A8E9PevMVd+z9m9t/T/h/8j7aWXU/rP1uGkrWfmul/NNuz6Xa6jaKsXVpdWN1JZXsTwzQsUkjdSrow6qynBBHoaqllHBYfmKxatozvi+ZXjsf/1v7QfE/wZ+FvjHXB4n8T6DZX9+qqvnTxBmIX7obPDY7ZzX46/ta+I9X1D4yX/hy/i+w2Gh4tNPtQojjSHAYyIuAP3p+YkDoAO1fuzXNa74Y8NeIYHOv6dbX2wEL9ohSXHHbeDiv8Gc/ydYrDuMXy63dlv67ep/VvhD4h1MpzSNXEQdZKLjFOTXJe2sdJJaXWiWjZ/NmCG+6c/Tmlr9XPjV4B8C2uktLbaLYRsHOCttED+YWuW+A/gDwJq3hfU7jVdEsLp0chWltonIG0cAspIr8rlklqns+b8P8Agn9yUPElTwf1v2Hy5v15f0Pz58D+K7vwT4ntvEFqzDyjhwpwSh649x1HuBX656/4L+H/AMc/hbZa/dxz3kun2rPBLbPtnZ1j3NEGYMGEhxwwPOCOa8n8G/D3wDJ4rms5ND09ofIY7DbRFcjvjbivbvhVaWuleONR0rS4ktrWG1j8uGJQkacr91VwB+Ar67gaisNjo0prmjWvFp+Sck/wt8/I/EPGPP8A2s6eY4ZOnVpa8ylun00S7vrs2rH5PT6vM1s9lYxrZwOfmRRmRh/dklYb2x6fKv8AsisqvRfi9BBbfFTxHBbIsaLqNxhVAAHzk8Acda86rLEpqo4t7O33H9RZNKnPDQrQjbnSffddXu/VhXptmieCvBcup3qD+0/EluYbIZ+aGxLETzsMcGfb5Ufqm9uhGfLpTiFyP7p/lXqnxk+T4g3MCcJFbWUaKOiotpDhVHYDsBW2H92nKqt1p999fw/XocGcXrYmjgn8MryfnyONl6NtN90mtmebWsogu4rgjIjkR8f7rA/0qTUXik1K5kg/1bTSMv8AulyR+lVKc/32+p/nXJzaWPc9gva+062/yO58IeEJPHVvcaRozj+2bdTNbW5OPtcajMkUf/TZB8yD+NcgcgZ4VlZGMbgqykggjBBHUEHoR6Vb065ubLUra8s5GimimjZHQlWVgwwQRyCPUV9DftX2dpafGK4NpEkRmtLeWTYoXfIync7Y6se5PJr0Pq0Z4V11o4tJ+d7v71+J8rDNatHOv7PbvCcHNd4tNJrzTvddnfVppKP9lXU9S0z4wW0trK0Vp9lumvcEhPISItl/919uPfp1r9APDDR+J724+IOtAKuWW0DkbYoE6t/st1yfrXw3+ztDEvg3xheKoEoFjFvx82x5GLLnrtJAyOhwK+hPiHdXNv8Asv6hLbyMjbNmVJB2tOAVyOxBwR3FfYcBYpSzCGHrR5oUqU66XRyUlFX32s2vN36H81+MGHjis8ko+7KTp0r+TXM30195L0W+p4t+0F8V/CPxbgtvDXhK+lRtPmZx5w8q1uyRtGxyeGX+AuFVsnBBxXyDc2tzZyeVdRtG3Iwwx061EeWwa7nw0qzeD/ElvMN8cEME0SnkJIZlQuoPAYr8pI5xx0rw8zzSrm+KliMRpO3TayWis/8AP5XP6KybJKXD2Ep4TCtyp8yVna95PV3SXrZryTStbhK0IJbyewk0xZP3UZN0qHpvVQrED12fyrPrW0AA69YqehnQEeoJwR+IOK8DDK81Hvp959bmT5aMqltY6r5amSK9R8X2tponw88LaKg/0q+SfVrg+ombyYAfXEcZI/3j615eo5ArsfGru8+khyTjSLEDPYeWeK7cFJRoVpW1aS9LtP8AS3zZ5ua03UxmGg3onKXraLSX/k1/VI42vvH9m/48anrV/H8NfHc/2nz08uyuZPvkqP8AUyN/ESudjHnjBJyK+Dq6LwfLLD4w0iWFijLfW2CpwR+9XvXpcJZ7XwGOhUovRtJro03az/TseP4hcLYTNcsqUsTHWKbi+sX3X3arqj9DviHod34XubjTNHMlvpl9sPl7hskZPmxjkkIfWvS/2bv7Va31K1ktQdOmILXBOCZQApQevynPt+NdV8TreCTwffySIrNGAVJAJU7x09K6P4CRRR/D2ExqF33ExbAxk+YRk+vAAr5Pizw6w+XeIlKphajjHklNRSVkm2uRdFG8m9En+Z/DmeZ5Kpw3Lnim3JRb6vS/N66JdT8W/jJ8Jbr4X/EC/wDCdve2t/FCxeMxTIZVjc5RZYyQySAYBHfqODXlzaLrCnBtZemeFJ4/CtXxxLLc+NdZnuGMjvqF0WZjkk+c/JJ61zLKqnCjH0r5LHez9tNQjZXfX/gH+gPD8MZ9QouvVU5csW3y2u7LWykXhpOqsQq20pJ6fI3+FWNO8O6/rN4unaTYXN1O52iKKF3Yn0wqmsY8fMOtdj8OPGnjHRfG+nxaPq17aK1wikQzyRgqzDIwrDg9xV5fhY16qp7HPxRndbLsHUxSSk0m0tunqz9YfhD+zVpfiv4dadc/tDaLDe67FlUkd2W5FsMeTHcSRMPMdBx8xYhcKTkV9QWnws+Gtjax2Vr4f05IoUVEX7LEcKowByueB612tkSbdCepUfyq1X7pQy2jSioqKe2rSu/V9T/L3OONsyxleVWVWUU22oxbUY3d7RjeyXkj/9k=";
break;
case 8:
style_mainBgColor = "#200";
style_toolbarFontColor = "#f00";
style_toolbarBackColor = "#000";
style_windBgColor = "#000";
style_fontColor = "#999";
style_linkColor = "#f00";
style_headerColor = "#900";
style_fontType = themes_fonts[1];
style_shadows = "0 0 5px #000000";
style_shadows_block = true;
style_buttonRadius = "4";
style_borderWidth = "10";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAMAAAAM7l6QAAACLlBMVEUAAAAoAAAqAAAuAAAdAABeAABZAAA9AAAlAAAfAAATAABCAAA3AABlAABgAABUAABNAAA0AAAXAABrAABiAAAxAACbJyd6AAB2AABQAABIAAA/AAAbAAAZAAALAACWAABtAABXAABEAAA6AAAiAACbPDyHPDydAABuAABLAAAPAADKhoa+goKePDxzAABwAAC2j4+xgoKmbGx8QEB5PDyIKCh4IyOgEBCNAACKAAB/AACohYWcbGyXa2uSQEBmPz+BPj6XPT10PT2PPDyEPDxtIiJzHBxcHByiAACBAAB1AABnAADRlZXPj4/Hj4/Cj4/UhobChoa5hoashoakgoLBamqcSkqGSkp5SkqsQECFQECxPT2iPT2NPT24PDynNjafNjaRKCiTIiKHIiKvICBWHh57EhKLERGCEBCSAACHAACFAAB8AAAGAAC9lpbglZWqkpK8j4+mj4/DgoK3goKzbGysbGymZ2ejYWGuWVm4SkqVSkqMSkptSkqKQECmPj6rPDxhPDyvNjaZNjaTNjaRNjaCNjaiKCiOIiJzIiKQHByAHBxqHBxjHBymEBCaEBCTEBCKDw+rAACTAAB+AADZj4+/hoajhoaWhobMgoK3bGyQbGy5YWGtYWHBWVmjWVmoSkqjSkphSkrOQEDBPT1ZPT1LPT2jNjaaNjaWNjZ3NjZjKCilIiJhIiJCHByPExNeERFVERFBERF3EBByEBBoEBC4Dg6PDg5uDg7MAACyIoB8AAAAAXRSTlMAQObYZgAAAx9JREFUKM9NzlN7HGEAhuFvbM+sBmsz6904jVPbsZPatm3bNv5de9Tte71n98kDNm5ctGhxPL5o58/NmxZk5l2fP3/B5g2bltTX31iyuPEX2LV7VS7X0tz8xV+X2Ho7kdiWqKu//+Bp0+rVLblPBEiOhLvL1+w4FLibvZNdtqNhe2M238FpV50K4TXAOlulu3uy3Ncn7dm7YuXK/csf7V++djTNqYoS1TWwpkNiKc2H+NHRzq4zI2fOdnadbRsqezAhonIQeDZi53qmcCZAcr264PXgfph8/l4gMCziFFkQLDoqv6+EyxHRifsJVzQKQWyy6NI4DHN4SZA8LfgEXVX6fAiLwmhMlil5/bCnUnWkPU4YfKyaOOYUeSdCy7QcsGhTtsYlSU2lMNVFApdJ0wEIJiEIIk0UskgNRqFZmqJkFDUosDZ01HbJYVcJBPxb8PAJ25go+kk/COX7C996FcVP1fjFnCO20qQSlRDQ3HK0MD7jwCCyxodDrwoXpjHJJ4F8e//JC5hLj0o1DgVfD15mpnmJBKH247aLKZwitBrn8ycGS2E7iyCgNThQ+Nrr1Qm2xsk5A4MXFZyQAqA1OWCbcPsC8H8cTL45WeJIxIuA9cFjxe9TDAMbNV77t/wSz3mjLGhv7387odFUjK7xmvXHbaV0BKNksCZ0zFYqp5lZtMZzDr0sTLjtAq2BDXMX19+ML1y47PKWLZnMvHnzMwsyc+cujC+5FY/v/AF2PTzQlDu4atVnYmmi7u+3Lt1Wd293U251c9OBcRc4eErFxeq1qxzbmM027mhoaNi+bN1wFWEdbg8Pg+ApRWE8dt1FPdm3csXjPctX7NvbOlRxXwmH3T0QaOvgVdElYoL1YfR8V+e5rvPnOtcN2+2pnim+FwGH3oXTnJfhhVnZ6MFURtB90ZYOfDIluN08C9pOd1cjIotYFoTCJEP28bjYWsQdKXd42kGAtiF7RVRnBAY16JhBMDBMEKEiH8HxGQcvgTGPJ+1kOGfEb8ZoKIAgEISOwbqu+ZxenwxcAdlAKdoyaMqIUTAMwTCqmZYVM03UjP0BUFymBv3mU28AAAAASUVORK5CYII=";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAACDVBMVEUAAAAAAAD///////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACPj48AAAAAAADT09O0tLQAAAAAAAAGBgaxsbEAAACPj48GBgZsbGxfX19ra2v+/v4AAAD8/PwAAAAAAAAAAAAAAABZWVldXV1kZGSfn5/W1tba2toAAAD19fUAAABKSkoAAAAAAADy8vJCQkJSUlJMTEx2dnZxcXGoqKjU1NRJSUkAAADg4OC9vb24uLjw8PAAAAAAAAD///9mZmaEhISqqqoAAABPT0+EhISGhoazs7O8vLy0tLScnJy6urqrq6uSkpJubm68vLyqqqrHx8fNzc1+fn5VVVXf39+3t7epqalKSkpJSUl5eXmIiIgVFRVwcHDJyclaWlr///8sLCzW1taPj4+qqqoAAAAkJCSCgoK2trYAAABhYWEAAADAwMBlZWX///+xsbHJycmfn5/Pz88rKyuLi4vg4OCUlJSysrL////e3t7///8WFhb////Ozs4AAAAODg4AAAAtLS1UVFS0tLSrq6sLCwvOzs5zc3NmZmY9PT2Hh4eBgYF+fn53d3ddXV1ISEg5OTkDAwPk5OTR0dGgoKBQUFAzMzMrKysnJycgICAWFhYJCQno6Ojh4eHLy8u/v7+oqKikpKSQkJBNTU1KSkolJSUdHR3u7u7V1dXGxsbBwcGZmZmOjo4/Pz8uLi4Sr+EjAAAAf3RSTlMAAalVEQQIAx8GFQoMWScYzJNFNiqPa1sx/fbuh3ZeVCIeE/359drKxo1qXE5MQRn8+fnx6+HKrZ2XlZWPfWRCQjcnHPn27urj4+La19fOycnIxsXCu7u7u7ixsK2rqqilpKCfnJWMi4mFhISBgYB8d3d1cXBsWFZQTTw5HhUPHcZa7gAAAh5JREFUOMt10+dy2kAQAGCTSAgkghKkKHQIGEI1jh33bqf33nvvvTdEQAiI6SWAHXc7Pc+YiyWZGR/sjP5ovtndu9ttCpgfmgMyEE0NYsi4zWgOoBjWiKxdwwIxKleimKwRYA39L0YRlVIUMMhmDT1PR0hEEDDIBqtVw9X7w5sRORB1wMRSqLTouHh7nSBgwC3G5+f/OlwbBQGD4Jd0jE+VHFskAYMiP5eIZfRAKGABQOjT+GzkFx8XBSqrAyLT07PJuL5TFBD4HJ76PhVJxpvXbxAEDKJfc3lBbFoWEMjnvk1GZ/iMXRJwhslcPvw7Vd4qiVVN/oxGf4QL4LBcTdRAMVmIzBTmxnWpdIhjt0uidlELukSCT6XtMV1zeoll2zr7Xo5JAgCu3GqPFRd27OpwOp379+1pL7e5HtGkNB/gNSulPxnd7gMHj5/p6rp04dypIx17e4elIgBkq5XKzsPHTp/vdt8zMcyzB7eu9z3RWnFCAuwE137y7JXuG3dMgxa/RtPS4vO9oxW4WAMAttV17abbPcBYNJSWVqvV4FOQCIFKgDvae9c0YBr0U7SCxG02G06SOBjilRKGnudehvH6KSuukitByAkQ0h68vhw88dhn8YL0y42DnzIZhqEoKq7SW6O+/5UWtKa11qYJmJVtfO855BmhKer/uepvzpDnDa4AbeMqoSk4PhAIPkbaVHKswX5jSkKFfCSU9RP8A8O+k0UOi7+wAAAAAElFTkSuQmCC";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAB/lBMVEUAAAAAAAD///////8AAAAAAAAAAAAAAAAAAACPj48AAAAEBATU1NQAAAAAAAAAAAAAAACysrK0tLQAAAAFBQUGBgYAAAAAAABbV1f+/v4AAAAAAAD8/PwAAAAAAABRUVFgXFxkYmLa2toAAAAAAAAAAAAAAABKSkoAAAAAAAAAAADy8vJERERsYWFMTEyFgIB2dnZpaWltbW1xcXG0tLSpqamfn5/KyspJSUnQ0NC5ubng4OC8vLzw8PBjY2PMzMzk5OT8/Pz///8AAACEhIQAAABbNzdKHx9nU1NeXl68vLycnJyfn5+pqam6urqSkpJubm68vLyqqqp+fn5VVVXf39+3t7epqalKSkpJSUl5eXmIiIgVFRVwcHBaWlr///8sLCyPj48AAACqqqoAAAAAAAAkJCSCgoK2trbAwMD///+xsbGfn58rKyuLi4uysrL////e3t5fX19ubm7///+ampq9vb3////Ozs4tAAAOAAAAAABUAAC0AAA9AADOAABzAABPAAALAABJAQGqAACAAAB+AAB3AABdAAA5AAAfAAADAADjAADRAADJAAClAACgAACPAACIAABlAAAzAAApAAAlAAAWAAAJAAAhISHnAADhAAC/AACsAABsAAAODg4MDAzuAADVAADBAACrAACZAACCAABpAABZAABCAADvNVPVAAAAeXRSTlMAAalVBAgCHxBaBijLChYSDJCUVTc0GBT5h2lhXkQi+fj1xo2CeXNOTEYtGfv6+fLx7+3r5+HYx62lmJeSj4N2bmdCQTcc/v788ePi3dza187JycXCu7u7u7ixsK2rqKWkn52cnJWMi4mBgHx3cXBWUE1DQTwrIx4VaWIyqgAAAhNJREFUOMt10/dz2jAUB3DcGuNAbNMyHGbLXtnN3rN777333i1xARto2JuEJE3Tvf7Lmsiu7yr4/ibd5/R00nuygPm+OYDwkTXJjGGXwRxAMawZ2bqF4cVruRLFkGaA6R2feoWrlYKAQSbTMzo5p8eBgEFmoVbrGb73QoHL6wIGK+lgddk2fLcFCBhEl8McF7TZtwEBg4WlWIVNVm2tgoBBkGNL5V+pnaKAwbvFQvwHGxYEijQAH9fXCwlJQOB9KP81H0+E20QBg8iHbO57XWwHAgK57Je1SIFNSQI6YS2bC5WSSztE8d8lv0Ui+VCxXElFNwUFhAi4RDEeL5YWN5KxYJSRhPSSG+WfbDK2e29HW/sqw3S2jrXMgyrCX/z5XeH2dPX1HxwYOHS4r3u10/5ApQf9AX6zGk517D9y9OS5oaHLly6cGew/MPYcFAH9UEunu46fOHvxpsPpchsfTdy5MfJQZ6UJETAr0e7T569ev+V0mzRen8Uy6/Fo/Qq6XgMApt1+7bbD4TRqLFq/CkShpwhUBJ8HRyZcLrfJq+X3aYqiSZKk+Sb+V6J39LHJaDR5dVZSLVfykRN8xDl4euXTqUmPRqPx6TZfmN9EEAxDUVQYpZeGfeNPdBbfrM5KSd2E1AMWb6aPTc2ptFq/lSQaT87M9DNSwV+bVINLwXlL4OS8nlLLMVnjYEpCjeOEsvEBfwHzoIzGd/h+PgAAAABJRU5ErkJggg==";
style_mainBgTexture = "data:image/jpeg;base64,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";
break;
case 9:
style_mainBgColor = "#73AC37";
style_toolbarFontColor = "#ffffff";
style_toolbarBackColor = "#945034";
style_windBgColor = "rgba(47,91,12,0.8)";
style_fontColor = "#ffffff";
style_linkColor = "#D5AB81";
style_headerColor = "#ffffff";
style_fontType = themes_fonts[3];
style_shadows = "0 0 5px #000000";
style_shadows_block = true;
style_buttonRadius = "16";
style_borderWidth = "12";
style_borderImage = "data:image/png;base64,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";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAA7VBMVEUAAADIzMP/5claWjoCAgJGPjgBAQADAwANDQwAAAAXFxUDAwEDBAMaGhkODg0BAQAKCghVQ2sAAADZ2dk7OzsxMC84PD0JCQc+Pj4KCwhJSVQEBAICAgI6NzBmZmbS0tLR0dHOzs7ExMXLy8u8vLzGxsawsLC5ubmlpaV1dXXIyMjCwsJlZWWamppqamoPDw7Kysqzs7OXl5dSUlIrKioBAQG2traysrKSkpKMjIx+fn1wcHBiYmJLS0tHRka/v7+srKyqqqqioqKEhIR4eHdcXFxDQkI5ODghISGWlpYwLy0ZGRiop7JTUl9WVlYSe3RjAAAAH3RSTlMAAgEBKwRWDrjVq4DV1Kupci0dBvHx5ePV1Y+NjU8P6gBz0wAAAZdJREFUOMvdkteS2yAUhg+oOfZma3oDNUB1Lcly716X3U3y/o8TNPJ44CIvsN8NM/Bxfs4ZAK7r2oYW1C4GYAQXbiuxXpoGNOB2ywKV08SjYWWrFXQGpU+J/+GdBM5gXRh7lJDnRVGMXx9Pr/eyDtKUbrqNKXGnVRHzdCS+3KGHB4yUMKczW4fSKJeszydk+OkXwloXBlzlmyZll8eUUjKsQcewZMqmqRF6xKXU/2PrAjIsp7Nn0iAeaeA38pba8L1pOl/nzCdnko4DliKY7xdVF7r5yiW0NdIUq7O0F4JNrz4fhnJeLZMUdGFbBHy+5xfh9+HUcdSIaJ2Eq8R3z0I/3AZzppS4qbIiCX33Ing8mD2BQp1nQ2lIpbE8LwiCXBN6x6hkqzCO434/lsecs6U+rNtj9CyCIEk8eZ0LthnLR2r8OP6dlmI0GjEmxkJkh28YdHrXg/0siqJZmk+y7KmWk9RA4JiPDYP5rsymAxN00MX/vox20cefoGEghDDC56yXl56NkS40Z5YBLXdm+4v+C4ZWeGP8A9wzObf5MbrGAAAAAElFTkSuQmCC";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABBVBMVEUAAADVxpT09P+/38MCAgIBAQABAQEQEA8EBAD/9u0oGigaGxgEAwEJCQcODg0BAQADAwILCwoAAAAAAABJSUk6OThDSEsLCwhKSkogICAgHx5XV2RHRDpFZXWIO453d3f////8/Pz6+vrv7+/39/fJycl5eXnt7e3X19fV1dX+/v719fXz8/Ph4eGQkI/x8fHq6urk5OTi4uLb29vQ0NC4uLiAgH9kZGM0MzIBAQLm5ube3t65ubmqqqqZmZiNjY1bW1tWVVUTExPExMSwsLCgn5+JiYmDg4NtbGxSUVC8vLy1tbVzc3JGREM6OTcmJiYeHh0ODg7My9iysrJlZHNIR0crLCsShDi9AAAAIHRSTlMAAQIDK1bVuA4FBKuA1aqpjXIdEPHx5ePV1NSPTzArDw8+1JQAAAGiSURBVDjL3dPXcqMwFIDhI8Ax2E6yyfYuiiimd+PudU/P7ibv/yhRBsagSZ4g3wVc6B8d0ADASVG0oMSXNwQIwcFZTpKFgMqVuqs9TgxZzcs9OHjFINVkbH84oqCC2GBlyBhfzuP439397d053Ydjkq6+dGSsTHPTcTOTfPkpSRLipDoQ2xtLpUW6sGx3gkeffksIUOM5ERxlCS3weuvIch/HBbBQh05JfLqHqmJF7mv/W2zAoY7YvrL8PsYqfuaeAqDmC58LgnisWxquBO330GkEwtd53oVuNlRwvyx0nY6ttebEmh5/3o+0ah1PdGCDZRy5+i46BH/3t22xMeKblwRqEGhKFdj+cnhlQe00H8eGbyuKXAWGG249aCiy8SjwNU1RNFqpahhFGRP0rr3UHPqO49i2ow7DkWst2MM6e5hdkigMDCp0iZms6FEw3l3vpikxKYusCLnYf0fA6p0MdpuZ52307eRi7BU8BwwOROH+DzXQ1+l4OhCAxfGHWYvZevbxBzAQz/OIR9Wsm5teCzEDyn+ABqVfwsvPls2r6xvzBP+ePdLD+03oAAAAAElFTkSuQmCC";
style_buttonBack = style_buttonBack_sepia;
style_buttonConsole = style_buttonConsole_sepia;
style_buttonAudio = style_buttonAudio_sepia;
style_buttonPrefs = style_buttonPrefs_sepia;
style_mainBgTexture = "data:image/jpeg;base64,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";
break;
case 10:
style_mainBgColor = "#222222";
style_toolbarFontColor = "#ffcc00";
style_windBgColor = "rgba(0,0,0,0.7)";
style_fontColor = "#FFFFFF";
style_linkColor = "#ffcc00";
style_headerColor = "#ffcc00";
style_fontType = themes_fonts[4];
style_shadows_block = true;
style_buttonRadius = "24";
style_borderWidth = "4";
style_borderImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAMAAABhq6zVAAAAWlBMVEXRtQDQuADSswDUrQDWpwDTrwDWowDWoQDOvmzOvl3NvonPvEDQujDQuBPVqgDSmwDNvoLOvnDNvnvNvnfNvnPOvmHOvFPPvUnPvULQuRvUngDLlwDQuCXOkQDDboa7AAAAcklEQVQI1xXKVxaDMBBDUU1xN2DTA8n+t5nxn965QowxFTj1Yfmhbz1VFvVW2PajgEVm9Q/SWQngYQG2QEQ8CI5Bn4nYiShmN8DcEmoEmgzBCCp2rPd1XhVLUIfpPvY1FryPF/6mlnNMeJcwc+k5t7X9AU5pBMET9JaUAAAAAElFTkSuQmCC";
style_cursor = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABYlBMVEUAAAA/Pz82Njb///8vLy////8tLS03NzeJiYklJSX39/ciIiJBQUEzMzMzMzOZmZn///8mJiY3Nzc0NDTg4OA2NjZMTEwsLCxubm5GRkZWVlYzMzM0NDQzMzM0NDQzMzMyMjIxMTEzMzMyMjIyMjIpKSk0NDTn5+f9/f09PT3Jycns7Oza2tpycnKrq6tDQ0OysrJcXFwzMzMzMzMyMjIxMTEzMzMxMTEzMzMxMTFAQECioqLZ2dkdHR03Nzfv7+/r6+vS0tJCQkIzMzOYmJi8vLy4uLhRUVEwMDDp6emioqJpaWmLi4uGhoZlZWXGxsaQkJB6enpZWVk0NDSFhYU2NjaXl5c2NjYzMzMyMjI1NTUyMjIyMjIyMjIyMjI1NTUzMzMrKys5OTn////7+/s6OjokJCQ3Nzc0NDSampomJiYxMTEtLS39/f04ODg5OTkvLy8qKir4+PgsLCwjIyMeHh7+hwJIAAAAY3RSTlMAA/H7Bfr47eL79/r201T+9/b19fT08/Px8fDvz7u1X0o7KhYR/vn29PTz8vDw7+/u7evdkXNoYkMmCPj39/b19fT09PPy8vHx8PDw7+/v7ezs6ebk5OPgyMbBrJ2AeiIZDAnUBOCsAAABsElEQVQ4y33SZ1vqMBQH8JTYXluGcEH2Bvce13X33su91yEk0lrA8f2N9YmPJOJpkhc5v+f/ojnoODeYr6Ae1LVyzVZz7egJ8dFOJVim3F28uXo57aeDpa7ieb0PJiOMLXHRFeDPBqMhIRRwBhfwN2G7i0h7DPRzUKvB+6jTfCGECvg37nPIAdJU4vMAX0Mf2mQeiRAF8D2WapN9IVTAD+urSfaEUAE/h2dMkuNCAn0e8AT+4yc7PULICQDnEJ4wSbCKtA7wj4N7gf+/pnOVjoRGL66JNi/9k+FuK4A3eRfrujX0anzVYcsPwLNGr/cnAayJdHyk3zAZzZzIoAZv3wFOXrdsSimbLSIJXIAV+A0QM9xsIVQqryCtE2AYm2oELMA/2xvendaZsA6xQJvUJwFGI+QQrYi2AFeb3785NHqZ1kFPO1vKWM06M4FLUlggkVGALyYpyI+VdQ2bLqKqbyCJIRxvZeWEUoZGQ+gUzbuJGMAPPynKEdWlyu2klWn9F0A4ZecloHmbrzwbGdanp+wgkku7O4rUSMb9NhGzrVawOdAicwtqQ+QsB9luSLm/ARNReAP/nQ5NAAAAAElFTkSuQmCC";
style_cursorHover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAABZVBMVEUAAABDQ0M0NDQ2NjI1NDI3NjIzMzP/1gAlKDb3xgI0NDM2NjKKdB3/1AD/0AAiJjdsXiQzMzMzMzIyMjL/zQAmKTZCPi/gtAfouwUsLjRMRixZUClEPy6wkRM0MzM0NDIzNDMyMjMzMzMyMjItLjQ0NDMtLjX/0QD9ygA3NjLJpA5HQi1TSyu6mBHsvgRyYiPasAmOdhwzMzNZTyo2NTKHcR4zMzMyMzMyMjMxMTIzMzMxMjQzMzMzMzMzMzMyMjIxMTH/3AAvMDQrLTSihxcdIzlBPS/ZsAk+PDDrvQXvwQRBPjDSqguYfxk8OjAxMjOihhdlWSaGcR4zMzOqjBbGoA56aSE0MzOXfhoyMjIyMjQyMjQyMjIwMDU1NTUrKyv/////1gAkKDc6OTaagBkmKTc2NjYwMTU3NjQ4ODYtLzX/0wArLTU6OTT/2AD/0AAyMzYpLDY2NTP/4wD/zQAeJDn/2QAjmktHAAAAYHRSTlMAAwb18O3+/fv39fLj/Pv68dO9Fvn29vT08/Py7+/PtV9KKhH5+ff39PPz8vLy8vDw7uvp4uLdx5FzaGJVU0M8Jvr5+Pj39/f19fX09PPy8fDv7+3t7ezm46ydgHo6Igwu7hV9AAABs0lEQVQ4y33SZ1PbQBAG4JPiqIF7740WeocQWnpC772sfFhdxqb9ftBgcYNuzI6kD7eP3pubWxSN9BajiEUdK4JV/fjqAyGpQ2nFV+ksPtV+5YNK7yURFLi5P4gr3DIRFJD5kZ+ctkgEBeDpMK1q3xDTEYC8IRn6VyIoAI3xLsM+RwxNvrwCkFPrFp5HiOkEQB7NWfiMFl0uAFMcFvApERQAcyAr4AgRFIDbwG4Qz7FtQUAD3kT/mGCHq++Erz5BADz4j34rs1FHEDDDAynz++YPfQ69S+jm3b8DMTH1d3zK4FYRy7QLfa53y87uAOJYNvNP6hM4Zf+aBLSBvPYf+NCjqiqKxk2WEeMBppjYa0CyTyuUFlcq7XMSwJujuXpCBH+oNe0suX0XzNwn/1h2LS/DYBxfINZtu6B2kto2NMkaDkAga0xTYzVpTCUsXFrA8UETRgRc8l5WQe9pvkxb1dcK+SGWUQvehBWfJi0hFs3r6eQt7ATtsjeiuhx1Jq1yV5uQoX+oWfQAxnmdp6hsDcTyuWaY9AhxPmWtJ5QJqtidbbrCeku1ZxfohpuzGr6LLFHrzy8SdsLD0lZqAAAAAElFTkSuQmCC";
style_buttonBack = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAMAAABg3Am1AAAAVFBMVEUAAAAAAAABAQAAAAEBAwMaBw4AAAEBAAAAAQABAQEBAQABBAMBAQEEAQEHAQgAAAD////19fXs6+xDQ0M9Pj0sKywwMDCysrOlpaUfHx/l5uW8vLzhDUXHAAAAD3RSTlMA9OauaA3bx8KNeFtJNBjKe016AAABO0lEQVRIx5VW25aDIAw04r3aAIvabv//P9fD0lIhkpN58WXmQCZmQpVjHtXQAXSDGueKQ9sowC+AatoSfQLMANOlpKmRRN2Q9HuPl+jvOX+psYB6Sfk3wCLglvCRxUmxAC+A5ave0/23l9avjagjVn7y58foA2YlvPr4n/A9dszRhP7WBF8bytz/nk8UX1skMPkDgOLrB+lUGytI+PaJeFGFIvkrklCHAAR8hGO+GH6CuRpFfBwrFfkFmMfzXcTgv1uZH20bqs5/9zI7NqYLJlle8BtskgrEVwpFr4Y9IBT9ttWVFTbaOmKmsFupcTOKFEc8g0QB/vcWKNR5gBw/QMmIOnZE0xBwTAjkMeOYmMmDzDFB5tHnCktGJR3G627MvlJhLI176UKRryz5UpSvXflilz8d5I8T8fPnD0myayNfQ66jAAAAAElFTkSuQmCC";
style_buttonConsole = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAMAAABg3Am1AAAAdVBMVEUAAAAAAAAAAAABAwMaBw4AAQEBAQABAQAAAAEAAAEBAAEAAQEBAQABBAMBAQEEAQEHAQgAAAD///8REhK7urmAf4AZGBnw8PAwMDBsbGw3ODji4uLPz88qKiqJiIhdXl1TU1PDxMOtrK2WlpZAQECgoKEGBwdlGHtXAAAAEXRSTlMA9cVoDe7n5Nuyqo14W0k0GKX4OrsAAAGKSURBVEjHpdbbcqswDAVQ24SWW0i3CDEEcmmS9v8/8XROO5ZEcJhM1rM9shCWbO7VZeEyazNXlLVZskpyC8HmyerR8irFnbSKbkneMOstMXM+HKLch7mzeccD7xszsbZ4yK4n67FI7dhYSM3u0HWHXaNjbES+6vxd/zV6Ij9+nTqVB2fu5PLjQMHQHsBc+P5gp5GU8QSW/NVX1Kv3NOF7UcHfmldiPc0QMar/AVI+v6cZnvNIVzqDLc266CxyDkARHCI3xnDNjhTRcvWMqRGMFPGJoDYlAk8RA4LSFAgoxiMojHsugjPZczlkxiK4UsQRgZUb9hTRyQ0Z2CfN2kIeyWEphO/UnSggtDSjh1Bw4WJ/RwupNDWUfiBl6KHUfz8fu11JuO6h2PB7s28SvqHlfIEYCZhI+IqynQAtXXETYI0AreI2s3wkbjPTLLYClES1ymUu0ozPbXDWzTjS7s8UnFW7jw6U28XTD3+5QVg/GFnNYf+ja9TIem0oLo/dlwb78tPhpcfJ08+ff4UFe7qTeKPjAAAAAElFTkSuQmCC";
style_buttonAudio = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAMAAABg3Am1AAAAdVBMVEUAAAAAAAAcFhwBAQAAAAABAwMAAQEAAAEAAAEBAAEBAQEBAQABBAMCAgIEAQEHAQgXChAAAAD////v8O8FBQXd3N0REhH49/dKSksiIiK8vLx1dHRiYmIzMjLl5uXPzs6MjIyop6afoKA5ODiIiIctLS1TU1P4ylzxAAAAEXRSTlMA9QfmxWju27KqjXhbSTQYEOwosOkAAAGNSURBVEjHlVbpmoIwDGw5lkvQBKkI4r2+/yNuytpyo5kfBf1mvnaGNK2YwEnjyAukDLwoTh3xATs/lNCDDP3dCt1JfmCCn8RZovsuzML1ZyVbDxbhbaf8zIUVuNmYv5GwCrkZ8eEjBopMfhbIrOfXhS/gWufOQj77cVYmXX9CLWsabljcXwOR/57AnfAPSOMBCedTf1H/UyRj/oOo9Li2CqwUWCRtvY3r5zcnWuvh8TzTa3Hs6mo34+CoSaj5CkBdtUINXIRDvirwLaiwqPfaD1ZgEJJlOeRfUMOYLhoo6WGdS0ekw+grtIJae8kbuJLOppuK2K79fsYOrfqUa66isTSsWESGn2MfXWA1PBHvRhAJUxY3nBHACfECD5rHlocI3m/5rGBPfytFv42JQJiQcFYAFFoDWmViYgvYS2Kb5sfafbhb/s2HY5cGu/i45b26gY4zG2h9izbTLcptAkttxprG4jVoM0uN7GUaWQl9+OxWyW3G3HbPPVD4Rxb/UOQfu/yDnX914F9O2NefP39lfM3WWZekAAAAAElFTkSuQmCC";
style_buttonPrefs = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAMAAABg3Am1AAAAgVBMVEUAAAAAAAAcFhwBAQABAwMAAQEAAAEBAAAAAQAAAAEBAAEBAQEBAQABBAMBAQEEAQEHAQgXChAAAAD///8TExP19PSjo6IEBATV1dXu7u3i4uFERUasrKwgIB+WlpaNjoxXWFm8vLxMTExgX18/Pz8wMTGzs7OCgYJ8fXxtbW0qKip12iDsAAAAEnRSTlMA9QfmaO7bx8Kyqo14W0k0GBAY7GJBAAACGklEQVRIx6VW6XKDIBBWa9OYpKYREA8Uj9oc7/+AdWA5AmEymXx/1GUX9tsLIw9Jvt9tN3G82e72eRI9wSnN4sJCnKWnKIzk8Fl4+DwkIfX0o3iIj/Shyc9XEcTXj69/hO0Dhxxd/W/gGkL87eg767jrsCO6szg6+3cjQmPnnHG0+Lr+92hF7/LQzBM3PrRGK2rqxkpFN1WS3xYLAhwJcPnV/qr1FA5QDvGmGa9Lex4RYDy3y3VsGk7BKXnEAfSvQqlqkIWmEo8rqBxEvan6KVEQpaqr0x2DsMEdi6xQ+Avp/2mVbKVscjZojbpaUevP2WQvifJCg8M6KSe8hnMqCQisFObR3tRPDdvrDWeQEFNX+2in9XskMRSek7222EVb+dKeFWVWWGCK9rmVgm20Ec+LSReswD4mhRch2EQySARpYNsAGzmRYQID9NwASQNwqSdOVr3sk166BKTpXFa6bqjuC11fVTlTIG3CyhRrnzMzYbUS1xGIx6QkUwPudCZxVmno82veYkpxy4n2UiN/XHzNyNhoUjNYxQflHegg74TMbqAFBbHoBrJbtA8b9LpF7SFwkYyJrUlkgV/0ELDHDGY1qfg0iBRCuoaJV6Rm2B4zhgUd5k4kpASu8mseqGIQGpWYQJ8FRiUMY7dvmDeMw+N+qVYCiz/uwxfKbZpu7oXyzpXlX4p0hXMpvnXtPr/YX/x1eOvn5OXfn3/w+Ibw3Z3WrwAAAABJRU5ErkJggg==";
style_mainBgTexture = "";
}
}
function ToStyle(str)
{
StyleText = StyleText + str + '\n' + '\n';
}
function SetStyleText()
{
StyleText = '';
if (style_view=='vis')
{
style_mainBgTexture = style_emptyImage;
}
ToStyle("*, #body {");
ToStyle("  -webkit-text-size-adjust:none; -webkit-tap-highlight-color:rgba(0,0,0,0); -webkit-focus-ring-color:rgba(0,0,0,0); outline:none; -webkit-touch-callout:none; -moz-user-select:none; -o-user-select:none; -khtml-user-select:none; -webkit-user-select:none; -ms-user-select:none; user-select:none; -webkit-user-drag:none; user-drag:none;");
ToStyle("}");
ToStyle("html, #body {margin:0; height:100%; overflow:hidden;}");
ToStyle("#body, input {font-size:95%;}");
ToStyle("#body, .input_text, .input_butt {font-family:"+style_fontType+";}");
ToStyle(".input_text, .input_butt, #printCont, #printAdd, #printImage, .wind, .button, .buttonno, .buttonback, .dialogButton {background-color:"+style_windBgColor+"; color:"+style_fontColor+";}");
ToStyle("#body {");
ToStyle("  background-image:url("+style_mainBgTexture+");");
ToStyle("  background-repeat:repeat;");
ToStyle("  background-color:"+style_mainBgColor+";");
ToStyle("  word-wrap:break-word;");
ToStyle("}");
ToStyle("#toolbar, #toolbar *, #menu, #menu *, h1 {background-color:"+style_toolbarBackColor+"; color:"+style_toolbarFontColor+";}");
ToStyle("#toolbar {height:36px; position:absolute; top:0; left:0; width:100%; padding:6px 0; overflow:hidden; z-index:1;}");
ToStyle("#toolbar, #menu {opacity:0.85;}");
ToStyle("#page, #printCont, #printImage, #printAddCont, #printAdd, #printAddText, #console, #toolbar, .wind, #menu, .button, .buttonno, .buttonback, .input_text {-moz-box-sizing:border-box; -webkit-box-sizing:border-box; box-sizing:border-box;}");
ToStyle("#page {display:none; height:100%; max-height:100%; width:100%; max-width:600px; text-align:left; padding:0; margin:0 auto;}");
ToStyle("#printTitle {display:block; white-space:nowrap; overflow:hidden; text-overflow:ellipsis; line-height:100%;}");
var shadows = 'none';
if (style_shadows_block)
{
shadows = style_shadows;
}
ToStyle("#printCont, #printImage, #menu, #toolbar {box-shadow:"+shadows+";}");
ToStyle("#printCont {position:absolute; top:36px; bottom:36px; left:0; right:0; padding:10px; overflow:auto; -webkit-overflow-scrolling:touch;}");
ToStyle("#printImage {display:none; position:absolute; top:36px; bottom:36px; left:0; right:0;}");
ToStyle("#clickImage {width:100%; height:100%; object-fit:cover; max-width:none;}");
ToStyle("#print, #printAdd, #menu {line-height:1.4em;}");
ToStyle("#printCont, #printAdd, .wind {-webkit-backdrop-filter:blur(8px); backdrop-filter:blur(8px);}");
ToStyle("#print {margin:0 auto; max-width:600px;}");
ToStyle("#menu {height:36px; max-height:36px; margin:0; padding:3px 3px 3px 20px; font-size:13px; overflow:auto; -webkit-overflow-scrolling:touch; position:absolute; left:0; right:0; bottom:0; text-align:center; z-index:98; line-height:1.1em;}");
ToStyle("#menuTable {height:100%;}");
ToStyle("#menu span {background-color:inherit; border:none; border-radius:0; padding:0; display:inline-block; margin:0 4px 0 0; max-width:100px; white-space:nowrap; overflow:hidden; text-overflow:ellipsis;}");
ToStyle("#menu img {display:inline; max-height:20px; vertical-align:middle;}");
ToStyle("#printAddCont {display:none; padding:56px 20px; position:absolute; left:0; bottom:0; top:0; right:0; z-index:100;}");
ToStyle("#printAdd {width:100%; max-width:668px; margin:0 auto; padding:10px; max-height:100%; overflow:auto; -webkit-overflow-scrolling:touch; box-shadow:"+style_shadows+";}");
ToStyle("#printAddText {text-align:left;position:relative;}");
ToStyle(".wind {display:none; position:absolute; width:240px; margin-top:-110px; margin-left:-120px; height:auto; max-height:220px; left:50%; top:50%; z-index:101; padding:12px; text-align:center; overflow:auto; -webkit-overflow-scrolling:touch; font-size:15px; box-shadow:"+style_shadows+"; line-height:1.6em;}");
ToStyle(".plink, .plinkno, a:link, a:visited, .button, .buttonno, .buttonback, .dialogButton {color:"+style_linkColor+"; text-decoration:none; -webkit-tap-highlight-color:rgba(128,128,128,0.2);}");
ToStyle(".wind .plink, .wind .plinkno {color:inherit;}");
ToStyle(".button, .buttonno, .buttonback, .dialogButton {display:table; font-size:0.95em; border-width:2px; border-style:solid; border-color:"+style_fontColor+"; padding:3px 8px; margin:0 4px 2px 0; max-width:100%; word-break:break-all; border-radius:"+style_buttonRadius+"px; -webkit-tap-highlight-color:rgba(128,128,128,0.2);}");
ToStyle(".dialogButton {display:inline-block; padding:2px 0px; margin:0; min-width:30px; width:44%;}");
ToStyle(".plinkno, .buttonno {opacity:0.6;}");
ToStyle("a:hover, a:active, .plink:hover, .plink:active, #menu span:hover, #menu span:active, .button:hover, .button:active, .buttonback:hover, .buttonback:active, .dialogButton:hover, .dialogButton:active, #showImageButton:hover {-webkit-transition:opacity 0.5s ease; transition:opacity 0.5s ease; opacity:0.8;}");
ToStyle(".author {text-align:center; letter-spacing:0.12em;}");
ToStyle("tt, #console {font-family:Menlo, Monaco, 'Droid Sans Mono', 'Courier New', Courier, 'Lucida Console', monospace;}");
ToStyle(".input_text {-webkit-appearance:none; margin:0; padding:4px; width:90%; border-width:0;}");
ToStyle(".input_text, #console, tt {-moz-user-select:text; -o-user-select:text; -khtml-user-select:text; -webkit-user-select:text; -ms-user-select:text; user-select:text;}");
ToStyle(".input_butt {-webkit-appearance:none; margin:0; text-align:right; width:10%; padding:4px; border:none;}");
ToStyle(".myinput, .trow {border-width:2px; border-style:solid; border-color:"+style_fontColor+";}");
ToStyle(".myinput {padding:2px; width:100%; margin:0 auto;}");
ToStyle(".trow, .trow_nb {word-wrap:break-word; table-layout:fixed; margin-bottom:4px;}");
ToStyle(".trow_nb {border:none;}");
ToStyle(".rcol {border-left-width:2px; border-left-style:solid; border-left-color:"+style_fontColor+";}");
ToStyle(".hr {color:"+style_fontColor+"; background-color:"+style_fontColor+"; width:100%; border:none; height:2px;}");
ToStyle(".h1 {font-size:1.3em; text-align:center; padding:12px; display:block; font-weight:normal;}");
ToStyle("h6 {font-size:1.4em; text-align:center; margin:0;}");
ToStyle(".header {color:"+style_headerColor+";}");
ToStyle(".header {font-size:1.3em; text-align:center; padding:8px; display:block;}");
ToStyle("img {max-width:100%; max-height:100%; border:none; margin:0; padding:0;}");
ToStyle(".file {max-width:100%; display:block; margin:0.2em auto;}");
ToStyle(".video {position:relative; padding-bottom:56.25%; padding-top:30px; height:0; overflow:hidden;}");
ToStyle(".video iframe, .video object, .video embed {position:absolute; top:0; left:0; width:100%; height:100%;}");
ToStyle("li {margin-left:0px; list-style-type:circle;}");
ToStyle(".choice {margin-left:1.75em; position:relative;}");
ToStyle(".choice .plink {margin: 0.5em 0;}");
ToStyle(".choice .plink:before {content:'✓'; position:absolute; left:-1.5em; visibility:hidden; color:"+style_fontColor+"; -webkit-transition:opacity 0.5s ease; transition:opacity 0.5s ease; opacity:0;}");
ToStyle(".choice .selected:before {visibility:visible; opacity:1;}");
ToStyle("#console {position:absolute; top:0; right:0; left:0; z-index:99; opacity:0.85; padding:0; color:#000000; background-color:#FFFFFF; height:0; overflow:hidden; -webkit-overflow-scrolling:touch; font-size:12px; -webkit-transition:height 0.5s ease; transition:height 0.5s ease;}");
ToStyle("body {scrollbar-face-color:"+style_fontColor+"; scrollbar-track-color:"+style_windBgColor+";}");
ToStyle("{}::-webkit-scrollbar");
ToStyle("{");
ToStyle("	width:6px;");
ToStyle("	height:6px;");
ToStyle("}");
ToStyle("{}::-webkit-scrollbar-corner");
ToStyle("{");
ToStyle("	background:transparent;");
ToStyle("}");
ToStyle("{}::-webkit-scrollbar-track");
ToStyle("{");
ToStyle("	background:transparent;");
ToStyle("}");
ToStyle("{}::-webkit-scrollbar-thumb, .scrollbar");
ToStyle("{");
ToStyle("	background-color:"+style_fontColor+"; border-radius:6px; opacity:0.7;");
ToStyle("}");
ToStyle("{}::-webkit-scrollbar-thumb:hover");
ToStyle("{");
ToStyle("	opacity:0.6;");
ToStyle("}");
ToStyle(".imgbutton {display:inline-block; width:24px; height:24px; background-size:24px;}");
ToStyle(".imgbutton:hover {opacity:0.8;}");
ToStyle("#showImageButton {position:absolute; background:#FF0000; bottom:0; left:0; width:20px; height:36px; font-size:20px; overflow:hidden; text-align:center; z-index:100; background: no-repeat center center url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABIAAAASBAMAAACk4JNkAAAAMFBMVEUAAAD////F5P9/wkbW7P9nvEgSiUa02//95ASr1/+Zy/mHvvB1seg9tEz90QTf8f96IAwnAAAAAXRSTlMAQObYZgAAAF1JREFUCNdjwAoEwUAAyJLoeHPmzBkI6/Xu3bshrFerVq0CseTeWc6cORPEEi9XKi8vB7GElEAAxBJxAQEQSyzt/7e0/yCW7N203LtpIJZoKAiAWMLGICCAZC9+AAD1sR3QeboaSAAAAABJRU5ErkJggg==);}");
ToStyle("#audioButton {display:none; opacity:0.5; background-image:url("+style_buttonAudio+");}");
ToStyle("#BackButton {background-image:url("+style_buttonBack+");}");
ToStyle("#ConsoleButton {background-image:url("+style_buttonConsole+");}");
ToStyle("#PrefsButton {background-image:url("+style_buttonPrefs+");}");
ToStyle("#body, .choice .plink:before {cursor:url("+style_cursor+"),default;}");
ToStyle("a, .plink, .button, .buttonback, .dialogButton, .input_butt, #printImage, .imgbutton, #showImageButton {cursor:url("+style_cursorHover+"),pointer;}");
ToStyle("  #printCont, #printAdd, #printImage, #menu, .wind {border-width:"+style_borderWidth+"px; border-style:solid; border-color:transparent; border-image:url("+style_borderImage+") "+style_borderWidth+" round;}");
ToStyle("#shadowlayer {position:absolute; left:0; top:0; right:0; bottom:0; background-color:rgba(0,0,0,0.3); z-index:1; box-shadow: inset 0px 0px 64px 16px rgba(0,0,0,0.5);}");
ToStyle(".sprite {position:absolute; max-width:none; max-height:none; opacity:0;}");
if (TargetFormatBak=='html')
{
if (style_view!='vis')
{
ToStyle("@media only screen and (max-width:599px), only screen and (max-height:599px) {");
ToStyle("  #printCont, #printImage, #menu, .h1 {border-width:0; border-image:none;}");
ToStyle("}");
}
if (style_view=='sw')
{
ToStyle("@media only screen and (min-width:600px) and (min-height:600px) {");
ToStyle("  #body, input {font-size:100%;}");
ToStyle("  #printHelper {position:absolute; top:0; bottom:0; left:0; right:0; padding:56px 20px;}");
ToStyle("  #printCont {position:static; margin:0 auto; max-width:728px; height:100%;}");
ToStyle("  #print {max-width:100%;}");
ToStyle("  #printImage {top:112px; bottom:112px; left:76px; right:76px;}");
ToStyle("  #menu {border-width:0; border-image:none;}");
ToStyle("}");
}
else if (style_view=='rpg')
{
ToStyle("@media only screen and (min-width:600px) and (min-height:600px) {");
ToStyle("  #body, input {font-size:100%;}");
ToStyle("  #page {max-width:1024px; margin:0 auto; position:relative}");
ToStyle("  #printCont {position:absolute; top:73%; left:20px; right:20px; bottom:20px;}");
ToStyle("  #print {max-width:none;}");
ToStyle("  #printAddCont {padding:80px 50px 44px 50px;}");
ToStyle("  #printAdd {padding:12px;}");
ToStyle("  #printImage {display:block; overflow: hidden;position:absolute; top:56px; left:20px; right:30%; bottom:30%;}");
ToStyle("  #menu {position:absolute; top:56px; left:72%; right:20px; bottom:30%; height:auto; max-height:none; font-size:0.9em; line-height:1.6em; opacity:1; padding:3px; z-index:0;}");
ToStyle("  #menuTable {height:auto;}");
ToStyle("  #menu span {display:block; float:none; margin:0 auto; white-space:normal; word-break:break-all; max-width:none;}");
ToStyle("  #menu img {float:none; display:block; margin:0 auto; max-height:36px;}");
ToStyle("  #spacer, #showImageButton {display:none;}");
ToStyle("}");
}
else if (style_view=='vis')
{
ToStyle("  #body, input {font-size:130%; letter-spacing:1px; text-shadow: #222 1px 0px, #222 1px 1px, #222 0px 1px, #222 -1px 1px, #222 -1px 0px, #222 -1px -1px, #222 0px -1px, #222 1px -1px;}");
ToStyle("  #console {text-shadow:none; letter-spacing:normal;}");
ToStyle("  #page {max-width:none; margin:0 auto; position:relative;}");
ToStyle("  #toolbar, #toolbar * {background-color:rgba(0,0,0,0); letter-spacing:3px; box-shadow:none;}");
ToStyle("  #printCont {padding:10px 20px; position:absolute; top:auto; left:0; right:0; bottom:0; width:100%; min-width:0; max-width:55em; height:auto; max-height:33%; margin: 0 auto; border:none; z-index:1;}");
ToStyle("  #print {max-width:none;}");
ToStyle("  #printAddCont {padding:80px 50px 44px 50px; z-index:2;}");
ToStyle("  #printAdd {padding:12px; max-width:800px; max-height:65%;}");
ToStyle("  #printImage {display:block; overflow:hidden; position:absolute; top:0; left:0; right:0; bottom:0; border:none;  background-color:rgba(0,0,0,0); cursor:url("+style_cursor+"),default;}");
ToStyle("  #menu {position:absolute; top:56px; left:auto; right:10px; bottom:auto; max-width:240px; height:auto; max-height:50%; font-size:0.9em; line-height:1.6em; opacity:1; padding:0; border:none; box-shadow:none; z-index:0;}");
ToStyle("  #menu, #menu *, .myinput *, h1 {background-color:rgba(0,0,0,0);}");
ToStyle("  #menuTable {height:auto;}");
ToStyle("  #menu span {display:block; text-align:right; float:none; margin:0 auto; white-space:normal; word-break:break-all; width:auto; max-width:none;}");
ToStyle("  #menu img {display:block; float:right; clear:both; max-height:64px;}");
ToStyle("  .button, .buttonno, .buttonback {opacity:1; min-width:10em; width:auto; padding:5px 1em; margin:0 auto 1em auto; text-align:center; background-color:rgba(32,32,32,0.6); -webkit-transition:background-color 500ms ease; transition:background-color 500ms ease;}");
ToStyle("  .button:hover, .buttonno:hover, .buttonback:hover {opacity:1; background-color:rgba(0,0,0,0.8); -webkit-transition:background-color 500ms ease; transition:background-color 500ms ease;}");
ToStyle("  .typer_1 {opacity:0; visibility:hidden;}");
ToStyle("  .typer_2 {opacity:1; visibility:visible; -webkit-transition:opacity 600ms ease; transition:opacity 600ms ease;}");
ToStyle("  #buttonsList {position:absolute; width:80%; margin: 0 auto; max-height:50%; top:auto; left:0; right:0; bottom:40%; overflow:auto; -webkit-overflow-scrolling:touch;}");
ToStyle("  #printCont, #buttonsList, #menu, #toolbar {-webkit-transition:opacity 0.5s ease; transition:opacity 0.5s ease;}");
ToStyle("  #spacer, #showImageButton {display:none;}");
ToStyle("  .video {margin:0 auto; padding:0; max-width:300px; height:200px;}");
ToStyle("@media only screen and (max-width:599px), only screen and (max-height:599px) {");
ToStyle("  #body, input {font-size:90%;}");
ToStyle("  #printAddCont {padding:36px 20px 44px 20px;}");
ToStyle("  #menu img {max-height:32px;}");
ToStyle("}");
}
else
{
ToStyle("@media only screen and (min-width:600px) and (min-height:600px) {");
ToStyle("  #body, input {font-size:100%;}");
ToStyle("  #page {max-width:1024px; padding:56px 20px 20px 20px; margin:0 auto;}");
ToStyle("  #printCont {float:left; width:68%; height:100%; min-height:100%; max-height:100%; margin:0; padding:12px; position:static;}");
ToStyle("  #print {max-width:none;}");
ToStyle("  #printAddCont {padding:80px 50px 44px 50px;}");
ToStyle("  #printAdd {padding:12px;}");
ToStyle("  #printImage, #menu {float:right; width:30%; position:static; margin:0;}");
ToStyle("  #printImage {display:block; min-height:300px; height:300px; max-height:300px;}");
ToStyle("  #menu {height:auto; max-height:39%; font-size:1.15em; line-height:1.6em; opacity:1; padding:3px;}");
ToStyle("  #menuTable {height:auto;}");
ToStyle("  #menu span {display:block; float:none; margin:0 auto; white-space:normal; word-break:break-all; max-width:none;}");
ToStyle("  #menu img {float:none; display:block; margin:0 auto; max-height:36px;}");
ToStyle("  #spacer {float:right; width:30%; height:3%;}");
ToStyle("  #showImageButton {display:none;}");
ToStyle("}");
ToStyle("@media only screen and (min-width:600px) and (min-height:600px) and (orientation:portrait) {");
ToStyle("  #printCont {float:none; width:100%; min-height:65%; height:65%; max-height:65%; margin-bottom:3%;}");
ToStyle("  #printImage, #menu {width:49%; margin:0; min-height:32%; height:32%; max-height:32%;}");
ToStyle("  #printImage {float:left;}");
ToStyle("  #menu {float:right;}");
ToStyle("  #menuTable {height:100%;}");
ToStyle("  #spacer {display:none;}");
ToStyle("}");
}
}
else
{
ToStyle("#printCont, #printImage, #menu, .h1 {border-width:0; border-image:none;}");
ToStyle("#menu {display:none;}");
ToStyle("#printCont {bottom:0;}");
}
return StyleText;
}
function AnimateCSS()
{
StyleText = '';
ToStyle(".effect_fade {");
ToStyle("-webkit-animation-name:effect_fade;");
ToStyle("-moz-animation-name:effect_fade;");
ToStyle("-ms-animation-name:effect_fade;");
ToStyle("-o-animation-name:effect_fade;");
ToStyle("animation-name:effect_fade;");
ToStyle("");
ToStyle("-webkit-animation-fill-mode:both;");
ToStyle("-moz-animation-fill-mode:both;");
ToStyle("-ms-animation-fill-mode:both;");
ToStyle("-o-animation-fill-mode:both;");
ToStyle("animation-fill-mode:both;");
ToStyle("");
ToStyle("-webkit-animation-duration:1000ms;");
ToStyle("-moz-animation-duration:1000ms;");
ToStyle("-ms-animation-duration:1000ms;");
ToStyle("-o-animation-duration:1000ms;");
ToStyle("animation-duration:1000ms;");
ToStyle("}");
ToStyle(".effect_fade_fast {");
ToStyle("-webkit-animation-name:effect_fade;");
ToStyle("-moz-animation-name:effect_fade;");
ToStyle("-ms-animation-name:effect_fade;");
ToStyle("-o-animation-name:effect_fade;");
ToStyle("animation-name:effect_fade;");
ToStyle("");
ToStyle("-webkit-animation-fill-mode:both;");
ToStyle("-moz-animation-fill-mode:both;");
ToStyle("-ms-animation-fill-mode:both;");
ToStyle("-o-animation-fill-mode:both;");
ToStyle("animation-fill-mode:both;");
ToStyle("");
ToStyle("-webkit-animation-duration:400ms;");
ToStyle("-moz-animation-duration:400ms;");
ToStyle("-ms-animation-duration:400ms;");
ToStyle("-o-animation-duration:400ms;");
ToStyle("animation-duration:400ms;");
ToStyle("}");
ToStyle("");
ToStyle("@-webkit-keyframes effect_fade {");
ToStyle("0% {opacity:0;}	");
ToStyle("100% {opacity:1;}");
ToStyle("}");
ToStyle("");
ToStyle("@-moz-keyframes effect_fade {");
ToStyle("0% {opacity:0;}	");
ToStyle("100% {opacity:1;}");
ToStyle("}");
ToStyle("");
ToStyle("@-ms-keyframes effect_fade {");
ToStyle("0% {opacity:0;}	");
ToStyle("100% {opacity:1;}");
ToStyle("}");
ToStyle("");
ToStyle("@-o-keyframes effect_fade {");
ToStyle("0% {opacity:0;}	");
ToStyle("100% {opacity:1;}");
ToStyle("}");
ToStyle("");
ToStyle("@keyframes effect_fade {");
ToStyle("0% {opacity:0;}	");
ToStyle("100% {opacity:1;}");
ToStyle("}");
ToStyle(".effect_bounce {");
ToStyle("-webkit-animation-name:effect_bounce;");
ToStyle("-moz-animation-name:effect_bounce;");
ToStyle("-ms-animation-name:effect_bounce;");
ToStyle("-o-animation-name:effect_bounce;");
ToStyle("animation-name:effect_bounce;");
ToStyle("");
ToStyle("-webkit-animation-fill-mode:both;");
ToStyle("-moz-animation-fill-mode:both;");
ToStyle("-ms-animation-fill-mode:both;");
ToStyle("-o-animation-fill-mode:both;");
ToStyle("animation-fill-mode:both;");
ToStyle("");
ToStyle("-webkit-animation-duration:.7s;");
ToStyle("-moz-animation-duration:.7s;");
ToStyle("-ms-animation-duration:.7s;");
ToStyle("-o-animation-duration:.7s;");
ToStyle("animation-duration:.7s;");
ToStyle("}");
ToStyle("");
ToStyle("@-webkit-keyframes effect_bounce {");
ToStyle("0% {");
ToStyle("opacity:0;");
ToStyle("-webkit-transform:scale(.3);");
ToStyle("}");
ToStyle("");
ToStyle("50% {");
ToStyle("opacity:1;");
ToStyle("-webkit-transform:scale(1.05);");
ToStyle("}");
ToStyle("");
ToStyle("70% {");
ToStyle("-webkit-transform:scale(.9);");
ToStyle("}");
ToStyle("");
ToStyle("100% {");
ToStyle("-webkit-transform:scale(1);");
ToStyle("}");
ToStyle("}");
ToStyle("");
ToStyle("@-moz-keyframes effect_bounce {");
ToStyle("0% {");
ToStyle("opacity:0;");
ToStyle("-moz-transform:scale(.3);");
ToStyle("}");
ToStyle("");
ToStyle("50% {");
ToStyle("opacity:1;");
ToStyle("-moz-transform:scale(1.05);");
ToStyle("}");
ToStyle("");
ToStyle("70% {");
ToStyle("-moz-transform:scale(.9);");
ToStyle("}");
ToStyle("");
ToStyle("100% {");
ToStyle("-moz-transform:scale(1);");
ToStyle("}");
ToStyle("}");
ToStyle("");
ToStyle("@-ms-keyframes effect_bounce {");
ToStyle("0% {");
ToStyle("opacity:0;");
ToStyle("-ms-transform:scale(.3);");
ToStyle("}");
ToStyle("");
ToStyle("50% {");
ToStyle("opacity:1;");
ToStyle("-ms-transform:scale(1.05);");
ToStyle("}");
ToStyle("");
ToStyle("70% {");
ToStyle("-ms-transform:scale(.9);");
ToStyle("}");
ToStyle("");
ToStyle("100% {");
ToStyle("-ms-transform:scale(1);");
ToStyle("}");
ToStyle("}");
ToStyle("");
ToStyle("@-o-keyframes effect_bounce {");
ToStyle("0% {");
ToStyle("opacity:0;");
ToStyle("-o-transform:scale(.3);");
ToStyle("}");
ToStyle("");
ToStyle("50% {");
ToStyle("opacity:1;");
ToStyle("-o-transform:scale(1.05);");
ToStyle("}");
ToStyle("");
ToStyle("70% {");
ToStyle("-o-transform:scale(.9);");
ToStyle("}");
ToStyle("");
ToStyle("100% {");
ToStyle("-o-transform:scale(1);");
ToStyle("}");
ToStyle("}");
ToStyle("");
ToStyle("@keyframes effect_bounce {");
ToStyle("0% {");
ToStyle("opacity:0;");
ToStyle("transform:scale(.3);");
ToStyle("}");
ToStyle("");
ToStyle("50% {");
ToStyle("opacity:1;");
ToStyle("transform:scale(1.05);");
ToStyle("}");
ToStyle("");
ToStyle("70% {");
ToStyle("transform:scale(.9);");
ToStyle("}");
ToStyle("");
ToStyle("100% {");
ToStyle("transform:scale(1);");
ToStyle("}");
ToStyle("}");
ToStyle("");
ToStyle(".effect_scale {");
ToStyle("-webkit-animation:effect_scale .4s ease-out both;");
ToStyle("-moz-animation:effect_scale .4s ease-out both;");
ToStyle("-ms-animation:effect_scale .4s ease-out both;");
ToStyle("-o-animation:effect_scale .4s ease-out both;");
ToStyle("animation:effect_scale .4s ease-out both;");
ToStyle("}");
ToStyle("");
ToStyle("@-webkit-keyframes effect_scale {");
ToStyle("from { opacity:0; -webkit-transform:scale(.7); }");
ToStyle("}");
ToStyle("@-moz-keyframes effect_scale {");
ToStyle("from { opacity:0; -moz-transform:scale(.7); }");
ToStyle("}");
ToStyle("@-ms-keyframes effect_scale {");
ToStyle("from { opacity:0; -ms-transform:scale(.7); }");
ToStyle("}");
ToStyle("@-o-keyframes effect_scale {");
ToStyle("from { opacity:0; -o-transform:scale(.7); }");
ToStyle("}");
ToStyle("@keyframes effect_scale {");
ToStyle("from { opacity:0; transform:scale(.7); }");
ToStyle("}");
ToStyle(".effect_flip {");
ToStyle("-webkit-transform-origin:50% 50%;");
ToStyle("-webkit-animation:effect_flipx .5s both ease-out;");
ToStyle("-moz-transform-origin:50% 50%;");
ToStyle("-moz-animation:effect_flipx .5s both ease-out;");
ToStyle("-ms-transform-origin:50% 50%;");
ToStyle("-ms-animation:effect_flipx .5s both ease-out;");
ToStyle("-o-transform-origin:50% 50%;");
ToStyle("-o-animation:effect_flipx .5s both ease-out;");
ToStyle("transform-origin:50% 50%;");
ToStyle("animation:effect_flipx .5s both ease-out;");
ToStyle("}");
ToStyle("");
ToStyle("@-webkit-keyframes effect_flipx {");
ToStyle("from { -webkit-transform:translateZ(-1000px) rotateY(90deg); opacity:0.2; }");
ToStyle("}");
ToStyle("@-moz-keyframes effect_flipx {");
ToStyle("from { -moz-transform:translateZ(-1000px) rotateY(90deg); opacity:0.2; }");
ToStyle("}");
ToStyle("@-ms-keyframes effect_flipx {");
ToStyle("from { -ms-transform:translateZ(-1000px) rotateY(90deg); opacity:0.2; }");
ToStyle("}");
ToStyle("@-o-keyframes effect_flipx {");
ToStyle("from { -o-transform:translateZ(-1000px) rotateY(90deg); opacity:0.2; }");
ToStyle("}");
ToStyle("@keyframes effect_flipx {");
ToStyle("from { transform:translateZ(-1000px) rotateY(90deg); opacity:0.2; }");
ToStyle("}");
return StyleText;
}
var style_badImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAhUlEQVQ4y7VUQQrAIAzzOf57H/Af+gt/UPC+FXoQVxe3RSEopSQprQ1hxymlxJzzcd1VYe/4Od8CrYfGZoQw35TaABmVO2fi5FdEeFP2nLmEIFHdJIOsCKNSEGTaROAUO3sYB/nlzCFOC4TpzaBzCOkl05pCH5sdg839evTlsGN9cRcs85zfmkXOXkhCxwAAAABJRU5ErkJggg==";
var speechServicePrefix = 's_';
var speechStrings = [];
function speechHash(s,add)
{
s += '';
s = s.replace(/\\\\n/g,'\n');
s = s.replace(/^(\n)+/,'');
s = s.replace(/(\n)+$/,'');
s = s.replace(/(&nbsp;)+/g,' ');
s = s.replace(/\t+/g,'');
s = s.trim();
if (s=='') {return '';};
var bad = [':',';','.',',','…','–','_','‾','¯','¦','´','!','¡','?','¿','(',')','[',']','{','}','|',"'",'"','«','»','‘','’','‚','"','”','„','•','!!!','!!','???','??','?!','!?','--'];
var hash = '';
if (bad.indexOf(s)==-1) {
if (add) {
if (!isNaN(s)) {
found = true;
}
else {
var found = false;
for (var key in speechStrings) {
if (speechStrings[key][1].toLowerCase()==s.toLowerCase()) {
found = true;
break;
}
}
}
}
if (!isNaN(s) || s=='-') {
hash = speechServicePrefix+s;
}
else {
hash = md5(s.toLowerCase());
}
}
else {
return '';
}
if (add && !found) {
speechStrings.push([hash,s]);
}
if (!add) {
return hash;
}
}
function speechStringsAdd(macros)
{
var match = macros.match(/'[^']*?'/g);
if (match)
{
for (var key in match)
{
var text = match[key].substr(1).substr(0,match[key].length-2).trim();
if (text!='')
{
speechHash(text,true);
macros = macros.replace(match[key],"'"+'<asmtext>'+text+'</asmtext>'+"'");
}
}
}
return macros;
}
function speechRemoveTags(macros)
{
return macros.replace(/(asm_var\d+)/g,"speechRemoveTags($1)");
}
function speechTop(TestMode)
{
ToHTML("<html lang='"+LangCode+"'>");
ToHTML("<head>");
ToHTML("<title>"+Lang_InterABook+"</title>");
ToHTML("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
ToHTML("<meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no'>");
ToHTML("<meta name='mobile-web-app-capable' content='yes'>");
ToHTML("<meta name='apple-mobile-web-app-capable' content='yes'>");
ToHTML("<meta name='apple-mobile-web-app-status-bar-style' content='black'>");
ToHTML("<meta name='apple-mobile-web-app-title' content='"+Lang_InterABook+"'>");
ToHTML("<meta http-equiv='cleartype' content='on'>");
ToHTML("<meta http-equiv='X-UA-Compatible' content='IE=edge'>");
speechStyle();
ToHTML("</head>");
ToHTML("<body onselectstart='return false;' onload='NewGame(true);' onmousedown='return false;' oncontextmenu='showPrefs();return false;'>");
ToHTML("<audio id='musicAudio' preload loop style='display:none;'></audio>");
ToHTML("<table id='page' width='100%' height='100%' border='0' cellspacing='0' cellpadding='0' onclick='showPrefs(true);'>");
ToHTML("<tr>");
ToHTML("<td width='100%' height='100%' align='center' valign='middle' style='padding:16px;'>");
if (TestMode)
{
ToHTML("<div id='console'></div>");
}
ToHTML("<audio id='speechAudio' preload='none' style='display:none;'></audio>");
ToHTML("<div id='print'></div>");
ToHTML("<div id='audioButton' style='display:none;'></div>");
ToHTML("<span id='speechButtonMenu' onclick='event.stopPropagation();showPrefs();'></span>");
ToHTML("<div id='speechActionBlock'>");
ToHTML("<div id='speechAuthor'></div>");
ToHTML("<div id='speechTitle'></div>");
ToHTML("<span id='speechButton' onclick='speechPlayPause();'></span>");
ToHTML("<div class='progress' id='speechProgressBar'><div id='speechProgressValue'></div></div>");
ToHTML("<div id='speechActionButtons'>");
ToHTML("<span class='speechActionButton'>1</span><span class='speechActionButton'>2</span><span class='speechActionButton'>3</span><br><span class='speechActionButton'>4</span><span class='speechActionButton'>5</span><span class='speechActionButton'>6</span><br><span class='speechActionButton'>7</span><span class='speechActionButton'>8</span><span class='speechActionButton'>9</span><br><span class='speechActionButton' noborder onclick='speechReplay();'>↻</span><span class='speechActionButton' zero onclick='speechCommandsPlay();'>0<span style='font-size:0.8em;'>…</span></span><span class='speechActionButton' noborder onclick='speechBack();'>←</span>");
ToHTML("</div>");
ToHTML("</div>");
ToHTML("<div id='prefs' class='wind' onclick='event.stopPropagation();'></div>");
ToHTML("<div id='dialog' class='wind' onclick='event.stopPropagation();'></div>");
ToHTML("</td>");
ToHTML("</tr>");
ToHTML("</table>");
}
function speechBottom()
{
ToHTML("} catch (error) {");
ToHTML("}");
ToHTML("<\/script>");
ToHTML("</body>");
ToHTML("</html>");
}
function speechScript(TestMode)
{
ToHTML("<noscript style='position:absolute;top:40%;left:0;right:0;padding:5px;text-align:center;color:#FFFFFF;background-color:#222222;'>"+Lang_JSrequires+"</noscript>");
ToHTML("<script type='text/javascript'>");
ToHTML("try {");
ToHTML("function $(id) {return document.getElementById(id);}");
ToHTML("var pageDiv = $('page');");
ToHTML("var speechActionBlock = $('speechActionBlock');");
ToHTML("var speechActionButtons = $('speechActionButtons');");
ToHTML("var printDiv = $('print');");
ToHTML("var prefsDiv = $('prefs');");
ToHTML("var dialogWind = $('dialog');");
ToHTML("var pictureDefault = '"+style_pictureDefault+"';");
ToHTML("var audioButton = $('audioButton');");
if (TestMode)
{
ToHTML("var consoleDiv = $('console');");
}
ToHTML("var speechAuthor = $('speechAuthor');");
ToHTML("var speechTitle = $('speechTitle');");
ToHTML("var speechAudio = $('speechAudio');");
ToHTML("var speechButton = $('speechButton');");
ToHTML("var speechButtonMenu = $('speechButtonMenu');");
ToHTML("var speechProgressBar = $('speechProgressBar');");
ToHTML("var speechProgressValue = $('speechProgressValue');");
ToHTML("function progressValue(n)");
ToHTML("{");
ToHTML("this.value = n;");
ToHTML("speechProgressValue.style.width = n/this.max*100+'%';");
ToHTML("}");
ToHTML("function progressMax(n)");
ToHTML("{");
ToHTML("this.max = n;");
ToHTML("}");
ToHTML("function progress()");
ToHTML("{");
ToHTML("this.value = progressValue;");
ToHTML("this.max = progressMax;");
ToHTML("this.progressValue = progressValue;");
ToHTML("}");
ToHTML("var speechProgress = new progress();");
ToHTML("var speechIndex, speechDuration, speechListEnded, speechListPreloaded, speechDurationDefined;");
ToHTML("var speechList=[], speechCommandsList=[];");
ToHTML("var speechServicePrefix = 's_';");
ToHTML("window.onkeydown = function(e) { e.preventDefault(); };");
ToHTML("window.onkeyup = function(e) { keysUp(e); };");
ToHTML("var speechFormat='ogg';");
ToHTML("if (!!(speechAudio.canPlayType && speechAudio.canPlayType('audio/mpeg;').replace(/no/, ''))) {");
ToHTML("speechFormat='mp3';");
ToHTML("}");
ToHTML("speechAudio.addEventListener('play',function()");
ToHTML("{");
ToHTML("if (speechDurationDefined)");
ToHTML("{");
ToHTML("speechSetPlayButton('speechPauseButton');");
ToHTML("}");
ToHTML("},false);");
ToHTML("speechAudio.addEventListener('pause',function()");
ToHTML("{");
ToHTML("if (speechDurationDefined)");
ToHTML("{");
ToHTML("speechSetPlayButton('speechPlayButton');");
ToHTML("}");
ToHTML("},false);");
ToHTML("speechAudio.addEventListener('error',function() {speechError(this.src);},false);");
ToHTML("function speechPassage(autoplay)");
ToHTML("{");
ToHTML("speechSetCommands(true);");
ToHTML("speechListPlay(autoplay);");
ToHTML("}");
ToHTML("function speechReplay()");
ToHTML("{");
ToHTML("PrintLocation(CurLocation.Title,true);");
ToHTML("speechPassage(true);");
ToHTML("}");
ToHTML("function speechListPlay(autoplay)");
ToHTML("{");
ToHTML("if (speechList.length>0)");
ToHTML("{");
ToHTML("speechDuration = 0;");
ToHTML("speechProgress.max = speechList.length;");
ToHTML("speechProgress.progressValue(0);");
ToHTML("speechAudio.pause();");
ToHTML("speechIndex = 0;");
ToHTML("speechSetPlayButton('speechPlayButton');");
ToHTML("if (autoplay)");
ToHTML("{");
ToHTML("speechGetDuration();");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function speechGetDuration()");
ToHTML("{");
ToHTML("speechSetPlayButton('speechLoadButton');");
ToHTML("speechDurationDefined = false;");
ToHTML("speechAudio.src = speechGetAudioPath(speechList[speechIndex]);");
ToHTML("speechAudio.load();");
ToHTML("speechAudio.play();");
ToHTML("if (!isFirefox) {");
ToHTML("speechAudio.pause();");
ToHTML("}");
ToHTML("speechProgress.progressValue(speechProgress.value+1);");
ToHTML("speechAudio.addEventListener('durationchange',speechDurationChange,false);");
ToHTML("}");
ToHTML("function speechDurationChange()");
ToHTML("{");
ToHTML("speechSetPlayButton('speechLoadButton');");
ToHTML("speechDuration += speechAudio.duration;");
ToHTML("var speechDurInt = speechDuration>0 ? 0 : 0;");
ToHTML("setTimeout(function() {");
ToHTML("speechAudio.src = '';");
ToHTML("if (speechIndex<speechList.length-1)");
ToHTML("{");
ToHTML("speechIndex++;");
ToHTML("speechGetDuration();");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("speechDurationDefined = true;");
ToHTML("speechAudio.removeEventListener('durationchange',speechDurationChange,false);");
ToHTML("speechSetPlayButton('speechPlayButton');");
ToHTML("speechPlayList();");
ToHTML("}");
ToHTML("}, speechDurInt);");
ToHTML("}");
ToHTML("function speechPlayList(time)");
ToHTML("{");
ToHTML("if (!time)");
ToHTML("{");
ToHTML("speechAudio.src = '';");
ToHTML("time = 0;");
ToHTML("speechProgress.progressValue(0);");
ToHTML("speechProgress.max = speechDuration;");
ToHTML("speechIndex = 0;");
ToHTML("}");
ToHTML("speechListEnded = false;");
ToHTML("speechAudio.src = speechGetAudioPath(speechList[speechIndex]);");
ToHTML("speechAudio.load();");
ToHTML("speechAudio.play();");
ToHTML("speechAudio.addEventListener('timeupdate',function(){speechProgress.progressValue(time+speechAudio.currentTime)},false);");
ToHTML("speechAudio.addEventListener('ended',speechPlayEnded,false);");
ToHTML("}");
ToHTML("function speechPlayEnded()");
ToHTML("{");
ToHTML("if (speechIndex<speechList.length-1)");
ToHTML("{");
ToHTML("speechIndex++;");
ToHTML("speechPlayList(speechProgress.value);");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("speechAudio.removeEventListener('ended',speechPlayEnded,false);");
ToHTML("speechListEnded = true;");
ToHTML("speechProgress.progressValue(speechDuration);");
ToHTML("speechSetPlayButton('speechReplayButton');");
ToHTML("}");
ToHTML("}");
ToHTML("function speechPlayPause()");
ToHTML("{");
ToHTML("if (speechDurationDefined)");
ToHTML("{");
ToHTML("if (speechListEnded && speechAudio.ended)");
ToHTML("{");
ToHTML("speechPlayList();");
ToHTML("}");
ToHTML("else if (speechAudio.paused)");
ToHTML("{");
ToHTML("speechAudio.play();");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("speechAudio.pause();");
ToHTML("}");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("speechGetDuration();");
ToHTML("}");
ToHTML("}");
ToHTML("function speechCommandInput(text)");
ToHTML("{");
ToHTML("if ( [1,2,3,4,5,6,7,8,9].indexOf(text)!=-1 )");
ToHTML("{");
ToHTML("if (text<=speechCommandsList.length)");
ToHTML("{");
ToHTML("text = speechCommandsList[text-1][0];");
ToHTML("}");
ToHTML("}");
ToHTML("text = (text+'').trim().toLowerCase();");
ToHTML("var found = false;");
ToHTML("for (var i=0; i<speechCommandsList.length; i++)");
ToHTML("{");
ToHTML("if (text==speechCommandsList[i][0])");
ToHTML("{");
ToHTML("found = true;");
ToHTML("break;");
ToHTML("}");
ToHTML("}");
ToHTML("if (found)");
ToHTML("{");
ToHTML("speechAudio.pause();");
ToHTML("eval(speechCommandsList[i][1].replace(/&quot;/g,'\"'));");
ToHTML("speechPassage(true);");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("}");
ToHTML("}");
ToHTML("function speechBack()");
ToHTML("{");
ToHTML("Back(true);");
ToHTML("}");
ToHTML("function speechSetCommands(add)");
ToHTML("{");
ToHTML("if (speechCommandsList.length<1)");
ToHTML("{");
ToHTML("speechCommandsList = [ [speechServicePrefix+'replay','NewGame();'] ];");
ToHTML("}");
ToHTML("var btns = getElementsByClass('speechActionButton',document.body);");
ToHTML("for (var i=0; i<9; i++)");
ToHTML("{");
ToHTML("var btn = btns[i];");
ToHTML("if (i>speechCommandsList.length-1)");
ToHTML("{");
ToHTML("btn.onclick = function(){};");
ToHTML("btn.setAttribute('disabled','');");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("btn.onclick = function(){ speechCommandInput(parseInt(this.innerHTML,10)); };");
ToHTML("btn.removeAttribute('disabled');");
ToHTML("}");
ToHTML("}");
ToHTML("if (!PrevLocation)");
ToHTML("{");
ToHTML("btns[11].setAttribute('disabled','');");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("btns[11].removeAttribute('disabled');");
ToHTML("}");
ToHTML("if (!add)");
ToHTML("{");
ToHTML("speechList = [];");
ToHTML("}");
ToHTML("if (speechCommandsList.length>1)");
ToHTML("{");
ToHTML("speechList.push(speechServicePrefix+'youcan');");
ToHTML("}");
ToHTML("for (var i=0; i<speechCommandsList.length; i++)");
ToHTML("{");
ToHTML("speechList.push(speechServicePrefix+(i+1));");
ToHTML("speechList = speechList.concat(speechCommandsList[i][0]);");
ToHTML("}");
ToHTML("}");
ToHTML("function speechCommandsPlay()");
ToHTML("{");
ToHTML("speechSetCommands();");
ToHTML("speechListPlay(true);");
ToHTML("}");
ToHTML("function speechGetAudioPath(name)");
ToHTML("{");
ToHTML("return 'audio/'+name+'.'+speechFormat;");
ToHTML("}");
ToHTML("function speechSetPlayButton(classn)");
ToHTML("{");
ToHTML("if (classn)");
ToHTML("{");
ToHTML("speechButton.className = classn;");
ToHTML("}");
ToHTML("}");
ToHTML("function speechSplit(s)");
ToHTML("{");
ToHTML("var strings = [];");
ToHTML("s = s.toString().replace(/<[[asmBSlash]]/asmtext>/g,'<asmtext>');");
ToHTML("var parts = s.split('<asmtext>');");
ToHTML("for (var key in parts) {");
ToHTML("var part = parts[key].trim();");
ToHTML("if (part!='') {");
ToHTML("if (isNaN(part)) {");
ToHTML("strings.push(part);");
ToHTML("} else {");
ToHTML("strings = strings.concat(speechNumbers(part));");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("return strings;");
ToHTML("}");
ToHTML("function speechNumbers(part)");
ToHTML("{");
ToHTML("asmSys_gender = parseInt(asmSys_gender,10);");
ToHTML("var strings = [];");
ToHTML("part = parseInt(part,0);");
ToHTML("if (part==0) {");
ToHTML("strings.push('0');");
ToHTML("} else {");
ToHTML("if (part<0) { strings.push('-'); };");
ToHTML("part = Math.abs(part);");
ToHTML("parts_t = [part];");
ToHTML("if (part>1000 && part<=999999) {");
ToHTML("parts_t = [(part-part%1000)/1000,part%1000];");
ToHTML("}");
ToHTML("for (var i=0; i<parts_t.length; i++) {");
ToHTML("part = parts_t[i];");
ToHTML("if (!(i==parts_t.length-1 && part==0)) {");
ToHTML("if (part<=20 || [30,40,50,60,70,80,90,100,200,300,400,500,700,800,900,1000,1000000].indexOf(part)!=-1) {");
ToHTML("strings.push(part.toString());");
ToHTML("} else if (part>20 && part<=99) {");
ToHTML("strings = strings.concat(speechDigits(part,10));");
ToHTML("} else if (part>100 && part<=999) {");
ToHTML("var digits = speechDigits(part,100);");
ToHTML("if (digits.length==2) {");
ToHTML("strings.push(digits[0]);");
ToHTML("if (LangCode!='ru') {");
ToHTML("strings.push('and');");
ToHTML("}");
ToHTML("if (digits[1]>=20) {");
ToHTML("strings = strings.concat(speechDigits(digits[1],10));");
ToHTML("} else {");
ToHTML("strings = strings.concat(digits[1]);");
ToHTML("}");
ToHTML("}");
ToHTML("} else {");
ToHTML("strings.push('more');");
ToHTML("}");
ToHTML("}");
ToHTML("if (parts_t.length>1 && i==0) {");
ToHTML("var tishP = '1000';");
ToHTML("if (LangCode=='ru')");
ToHTML("{");
ToHTML("if (strings[strings.length-1]=='1') {");
ToHTML("strings[strings.length-1] = '1a';");
ToHTML("}");
ToHTML("if (strings[strings.length-1]=='2') {");
ToHTML("strings[strings.length-1] = '2e';");
ToHTML("}");
ToHTML("var tish1 = parseInt(parts_t[0].toString().substr(-1),10);");
ToHTML("var tish2 = parseInt(parts_t[0].toString().substr(-2),10);");
ToHTML("if ((tish2<=10 || tish2>=20) && tish1>=2 && tish1<=4) {");
ToHTML("tishP = '1000i';");
ToHTML("}					");
ToHTML("else if (tish1!=1) {");
ToHTML("tishP = '1000ch';");
ToHTML("}");
ToHTML("}");
ToHTML("strings.push(tishP);");
ToHTML("}");
ToHTML("}");
ToHTML("if (LangCode=='ru') {");
ToHTML("var last = strings[strings.length-1];");
ToHTML("var gnd = '';");
ToHTML("if (last=='1' && asmSys_gender==1) {");
ToHTML("gnd = 'a';");
ToHTML("}");
ToHTML("else if (last=='1' && asmSys_gender==2) {");
ToHTML("gnd = 'o';");
ToHTML("}");
ToHTML("else if (last=='2' && asmSys_gender==1) {");
ToHTML("gnd = 'e';");
ToHTML("}");
ToHTML("strings[strings.length-1] += gnd;");
ToHTML("}");
ToHTML("}");
ToHTML("return strings;");
ToHTML("}");
ToHTML("function speechDigits(s,d)");
ToHTML("{");
ToHTML("var digits = [];");
ToHTML("var ost = s%d;");
ToHTML("var d = s-ost;");
ToHTML("if (d!=0) { digits.push(d.toString()); };");
ToHTML("if (ost>0) { digits.push(ost.toString()); };");
ToHTML("return digits;");
ToHTML("}");
ToHTML("function speechError(src)");
ToHTML("{");
ToHTML("AlertMessage('"+Lang_ErrorLoadFile+":<br>'+src);");
ToHTML("}");
ToHTML("function speechRandom()");
ToHTML("{");
ToHTML("if (speechCommandsList.length>0)");
ToHTML("{");
ToHTML("speechCommandInput(Math.floor(Math.random()*(speechCommandsList.length))+1);");
ToHTML("}");
ToHTML("}");
ToHTML("function showSpeechBlock(close) {");
ToHTML("if (close) {");
ToHTML("speechActionBlock.className = 'blockHide';");
ToHTML("}");
ToHTML("else {");
ToHTML("speechActionBlock.className = 'blockShow';");
ToHTML("}");
ToHTML("}");
ToHTML("function getElementsByClass(classList,node)");
ToHTML("{");
ToHTML("var node = node || document,");
ToHTML("list = node.getElementsByTagName('*'), ");
ToHTML("length = list.length,  ");
ToHTML("classArray = classList.split(/[[asmBSlash]]s+/), ");
ToHTML("classes = classArray.length, ");
ToHTML("result = [],i,j;");
ToHTML("for (var i=0; i<length; i++) {");
ToHTML("for(var j=0; j<classes; j++)  {");
ToHTML("if(list[i].className.search('[[asmBSlash]][[asmBSlash]]b' + classArray[j] + '[[asmBSlash]][[asmBSlash]]b') != -1) {");
ToHTML("result.push(list[i]);");
ToHTML("break;");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("return result;");
ToHTML("}");
ToHTML("function keysUp(event)");
ToHTML("{");
ToHTML("var key = keyCode(event);");
ToHTML("if (key==32) { speechPlayPause(); };");
ToHTML("if (key==13 || key==106 || key==56) { speechReplay(); };");
ToHTML("if (key==8 || key==51)  { speechBack(); };");
ToHTML("if (key==27) { showPrefs(); };");
ToHTML("if (key==82) { speechRandom(); };");
ToHTML("if (key==48 || key==96) { speechCommandsPlay(); };");
ToHTML("var keyN = 0;");
ToHTML("if (key>=49 && key<=57)  {keyN=key-49+1;}");
ToHTML("if (key>=97 && key<=105) {keyN=key-97+1;}");
ToHTML("if (keyN>0)");
ToHTML("{");
ToHTML("speechCommandInput(keyN);");
ToHTML("}");
ToHTML("}");
ToHTML("function speechRemoveTags(asmvar) {");
ToHTML("if (asmvar) {");
ToHTML("if (!isNaN(asmvar)) {");
ToHTML("return asmvar;");
ToHTML("}");
ToHTML("else if (isArray(asmvar)) {");
ToHTML("for (var key in asmvar) {");
ToHTML("var item = asmvar[key];");
ToHTML("if (item && isNaN(item)) {");
ToHTML("item = item.replace(/<asmtext>/g,'');");
ToHTML("asmvar[key] = item.replace(/<[[asmBSlash]]/asmtext>/g,'');");
ToHTML("}");
ToHTML("}");
ToHTML("return asmvar;");
ToHTML("}");
ToHTML("else {");
ToHTML("asmvar = asmvar.replace(/<asmtext>/g,'');");
ToHTML("return asmvar.replace(/<[[asmBSlash]]/asmtext>/g,'');");
ToHTML("}");
ToHTML("}");
ToHTML("else {");
ToHTML("return asmvar;");
ToHTML("}");
ToHTML("}");
ToHTML(speechHash.toString());
}
function speechStyle()
{
ToHTML("<style type='text/css'>");
ToHTML("html");
ToHTML("{");
ToHTML("width: 100%;");
ToHTML("height: 100%;");
ToHTML("overflow: hidden;");
ToHTML("}");
ToHTML("body");
ToHTML("{");
ToHTML("font-size: 22px;");
ToHTML("color: #000000;");
ToHTML("text-shadow: #FFFFFF 1px 0px, #FFFFFF 1px 1px, #FFFFFF 0px 1px, #FFFFFF -1px 1px, #FFFFFF -1px 0px, #FFFFFF -1px -1px, #FFFFFF 0px -1px, #FFFFFF 1px -1px;");
ToHTML("background-color: #FFFFFF;");
ToHTML("background-repeat: no-repeat;");
ToHTML("background-size: cover;");
ToHTML("background-position: center;");
ToHTML("-webkit-transition: background-image 0.7s ease-in-out;");
ToHTML("transition: background-image 0.7s ease-in-out;");
ToHTML("word-wrap: break-word;");
ToHTML("width: 100%;");
ToHTML("height: 100%;");
ToHTML("overflow: auto;");
ToHTML("-webkit-overflow-scrolling: touch;");
ToHTML("margin: 0;");
ToHTML("padding: 0;");
ToHTML("-moz-box-sizing: border-box; box-sizing: border-box;");
ToHTML("font-family: Arial, Helvetica, sans-serif;");
ToHTML("-moz-user-select: none;");
ToHTML("-o-user-select: none;");
ToHTML("-khtml-user-select: none;");
ToHTML("-webkit-user-select: none;");
ToHTML("-ms-user-select: none;");
ToHTML("user-select: none;");
ToHTML("-webkit-text-size-adjust: none;");
ToHTML("-webkit-tap-highlight-color: rgba(0,0,0,0);");
ToHTML("-webkit-touch-callout: none;");
ToHTML("-webkit-user-drag: none;");
ToHTML("user-drag: none;");
ToHTML("cursor: default;");
ToHTML("text-align: center;");
ToHTML("}");
ToHTML("#page");
ToHTML("{");
ToHTML("display: none;");
ToHTML("}");
ToHTML(".blockHide");
ToHTML("{");
ToHTML("visibility: hidden; opacity: 0; -webkit-transition: visibility 0s linear 0.4s, opacity 0.4s linear; -moz-transition: visibility 0s linear 0.4s, opacity 0.4s linear; transition: visibility 0s linear 0.4s, opacity 0.4s linear;");
ToHTML("}");
ToHTML(".blockShow");
ToHTML("{");
ToHTML("visibility: visible; opacity: 1; -webkit-transition: opacity 0.4s linear; -moz-transition: opacity 0.4s linear; transition: opacity 0.4s linear;");
ToHTML("}");
ToHTML("#speechButton");
ToHTML("{");
ToHTML("display: inline-block;");
ToHTML("width: 64px;");
ToHTML("height: 64px;");
ToHTML("margin: 20px 0 0 0;");
ToHTML("}");
ToHTML("#speechButtonMenu");
ToHTML("{");
ToHTML("background: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACQAAAAkAgMAAACcbnALAAAACVBMVEUAAAAAAAD///+D3c/SAAAAAXRSTlMAQObYZgAAABtJREFUGNNjwA9CYQCJFbUKApbiZ2HTO3JtAwDOcGeZujZyUAAAAABJRU5ErkJggg==') no-repeat center center;");
ToHTML("background-size: contain;");
ToHTML("width: 36px;");
ToHTML("height: 36px;");
ToHTML("position: absolute;");
ToHTML("top: 10px;");
ToHTML("right: 20px;");
ToHTML("margin: 0;");
ToHTML("}");
ToHTML(".speechActionButton, .wind");
ToHTML("{");
ToHTML("border: solid 3px rgba(0,0,0,0.7);");
ToHTML("text-align: center;");
ToHTML("text-decoration: none;");
ToHTML("}");
ToHTML(".speechActionButton");
ToHTML("{");
ToHTML("display: inline-block;");
ToHTML("background-color: rgba(255,255,255,0.85);");
ToHTML("width: 55px;");
ToHTML("height: 55px;");
ToHTML("line-height: 55px;");
ToHTML("border-radius: 50%;");
ToHTML("font-size: 1em;");
ToHTML("margin: 5px;");
ToHTML("}");
ToHTML(".speechActionButton[noborder]");
ToHTML("{");
ToHTML("border-color: transparent;");
ToHTML("background-color: transparent;");
ToHTML("}");
ToHTML(".speechActionButton[noborder]:hover");
ToHTML("{");
ToHTML("border: solid 3px #222222;");
ToHTML("}");
ToHTML(".speechActionButton[disabled]");
ToHTML("{");
ToHTML("opacity: 0.3;");
ToHTML("}");
ToHTML(".speechActionButton[disabled]:hover");
ToHTML("{");
ToHTML("background-color: rgba(255,255,255,0.85);");
ToHTML("border-color: rgba(0,0,0,0.7);");
ToHTML("}");
ToHTML(".speechActionButton[disabled][noborder]:hover");
ToHTML("{");
ToHTML("background-color: transparent; border-color: transparent;");
ToHTML("}");
ToHTML(".speechActionButton:hover,.speechActionButton:active");
ToHTML("{");
ToHTML("background-color: rgba(255,255,255,1);");
ToHTML("border-color: rgba(0,0,0,1);");
ToHTML("}");
ToHTML(".speechActionButton[disabled]:hover,.speechActionButton[disabled]:active");
ToHTML("{");
ToHTML("text-shadow: inherit;");
ToHTML("}");
ToHTML("#speechButton,#speechButtonMenu,.progress,progress>div");
ToHTML("{");
ToHTML("opacity: 0.85;");
ToHTML("}");
ToHTML("#speechButton:hover,#speechButtonMenu:hover");
ToHTML("{");
ToHTML("opacity: 1;");
ToHTML("}");
ToHTML(".speechLoadButton");
ToHTML("{");
ToHTML("background: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAAAe1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD///8AAABN3XYBAAAAJ3RSTlMAPvjy4hy+JEQKWK4vznI47NaOfkoQqJMW0niiYzOXXufFuIdoT9t1UNr/AAADGklEQVR42uzYyZKCQAyA4QyILC64gOCCuKff/wlnqo1mLNQubQIXPm96oET5Kx3odDqdTqcT/7QpiGGrWpWBv1QtGvkAkaNa40QAEC7+vdHQ62YRwp+ip1pyGYK2un//RvCFxnCV9ukf8dOMYKS0QwokcPUbboSNiOhyAdzNlDb1sQH+VGlHYB7FYIsNoPSNPAC2ceiuoLiEfoASHlAM+h4K8+gfv4ZHQ4rBHIXNldajBLAxfVCgqHjHCXiU060ZoKgBJSCHCoqBc0JBJ4cTUJFJx4ATkAGrxmCGYug7Lj14qqT7k6CQ8/UCTgnPhbcYoIx8z1PAc4VkDDgBBbAXMYhRQNzjBLySisWAE9BP4Y1korQSa7dR2iSBt2b0oNQeA/9CDzmwdzHIUJOYAgxKpblnrNXZVVoEJuFaJAaUgHUIBkKTAU8BJnxM2MUoOQVYxMBiCgjBjCcDtak7AQGYmY8J9gcBM3+ktG3DCWCRqvGsFrm8C6gwTAb7vMYpAMyqk8EKra14F1BhjsEktk7AxJAAmxjYTwHmGDiWMbifeuFjR45Bgwmw3hnYJ4CVLh8TrHcB3+Cdgf0uwMBqMrCfAozHhF2BXyl4CvhSfuAY2O8CzGreGZycD6eAmncGvAuw4f22awcpCMNQGIRLQbEFobTFSlFX9v5XdPkWAUP6kV3mEMP/JrmfPhO2UIAwnm0Ga1+wAiqcCc84BACQASgg1wy8BcAyqLwCvBmkLQCAZhAtAJBm8IkcKKRnwootwGWgCnAZgAKMPXlNyLaAvUtxGTxqK8Cbwbe0BVgz8EOguBlsdgi4DKbjrwImWAHQDKAFUDPwFhAUBUTMgb4McocAKACaAbQAWAagAFsGoADjFWcCtwBsBtYCfBnsuAL8THijArwZQAswblM0A2wBvgxCAeWHgDeD2VpA4M3ggBYgy+CKh4Avg0UV4M0AW4AvAz0EvBlgC3AZXKwF+DKYQQFA/DPoe2sB3gxMAb4MUAF+JpgCXAbxLwDgZgArAJn1EPBmYApwRl0Bvgy0BfgyMAU4i+VAZxi6RqPRaBg/q+u2nVYpT/sAAAAASUVORK5CYII=') no-repeat center center;");
ToHTML("background-size: contain;");
ToHTML("}");
ToHTML(".speechPlayButton");
ToHTML("{");
ToHTML("background: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAAAe1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD///8AAABN3XYBAAAAJ3RSTlMAPvLi+By+JEQKWK4vznI47NaOfkoQqJMW0niiYzOXXufFuIdoT9tT1PE9AAACuklEQVR42uWb23KbMBRFBQYDbh1fY8DFoZck6P+/sNNOhiN7P/ASL8bR/oI1nm2dJSG5MJu6cLOm9OtV5WZM4n12+u7AKID370/PDowCeP+7dGQUwKd/cgdGAbxf946MAvj0B1JGBbAsnhrHRQH4MiqAT2ugjAqQrdMRoe2xldEAfLnajQTZ6eKgGEAyFC+plRFYGQVgGI5LP2Z5dExCgCE//BwJtnumjAbwP5tv2YjAjGkD+MirldEDK6MCDEWdBWWUH+F+AJa3X8TKqACW5rCwMsqYBgD+ldEHZXSSuwFYuveRQJwRAbCVUZwRANAyLscyAgAWWxnNGQkALaM5IwRg6XbijCzAzZhuAADJSlZGGGBozltbGescB9AxjQJoGTMrIwggzkgDiDPiAOqMAIDkEpSxXVU0gDojDKDOeH4GALSMMqZRAHVGGkCdEQdQZ8QB1BlxAN3A8ABDI86IAqgzAgCaV3FGCsDKuBVnBAG0jASAOqOMaRRAnZEGUGfEAdQZaQB1RhxAnREH0DLSAOqMOICeM9IAujLiAOqMNIA6Iw6gzkgD6Aam4gHyfQBw4AH6YD04XWYuYYWXMPgbpvuC/BvqVCz5pXifhtv32IbR1RFOX+FCcl6EQhKbkhX1vFLateHn/9g2Jjdbs9g2p0U97/a824n5kACXkx/TdugRjY5d+pBKxy4MUNQydlGAvhXn4wDU+VgAdT4UQMtHAmj5YvtodfPZLrYPl9fOF93H6+uj2NguMIRXONI6j+0Si17jme8ikzgfe5Wr7d11vv5ltrclf51veuwCABPOBwBMOB8AoM5nIQCOSPkUYPqcbzqPf7ldzvnuG33gsJj1gUP5mc73eI9c7vXM52EeOs381AtZ+R7gud88r28TOeeDk8g5H5wELp8CyNiFk8ArnwLIUQuczUvh6PwFQFe+UCO5cScAAAAASUVORK5CYII=') no-repeat center center;");
ToHTML("background-size: contain;");
ToHTML("}");
ToHTML(".speechPauseButton");
ToHTML("{");
ToHTML("background: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACABAMAAAAxEHz4AAAAMFBMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD///8AAACqmna6AAAADnRSTlMAAz5t1ilni35gMxzn5lr21CEAAACPSURBVGje7dmxCYAwFIRhxQXcJG7iBpaiU9g5gmu4gatYWQkWQRBEUIhJYUgatRD8r3zv+Ba4QCUW+zVJHFgJI6siQ3UHeBVYmvxM0fuArdaVsnIA66Qzpj5g7kyndQDSPAc/kJmOAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALg7WD6eTD+5+v4XOACkxhZpp2ZcfwAAAABJRU5ErkJggg==') no-repeat center center;");
ToHTML("background-size: contain;");
ToHTML("}");
ToHTML(".speechReplayButton");
ToHTML("{");
ToHTML("background: url('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAMAAAD04JH5AAAAh1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD///8AAAAHx0GQAAAAK3RSTlMA+Rbw6M42kOD1SdajXL63WMOnT5VSKyUuxxGMbrCrYjoaiIJoQCAJnHN4kA9UmgAACF5JREFUeNq8l+liqjAQhRNC2EEQ1+Iutdrk/Z/v1kwoCAlKpPf7cZdaySQz5+SATDg689spmlCbYM4xsekkOt3mzhH9Bz5W8SwJCVdAwmQWrz7QH2LlM+pzAcZc8vh/n87mFvoTjvOrh+u18A/16rj5iXf9gxocNxQPhz801J+GroNG5Cvf+p1+T93zZh+nabzfnN0pzEWjOn+bf421fJrhxsMJjW7B4shaHBfBLaKkUQPO0lFKyDPxTHisNy0d1oNTRh6vfz/L3+/9Dv9u3otSiz3FSiP79xjwznlPdwWpdoOzcsleZFlmuPoeKd5QRBByiR8FbBDB1OeSMDA1PRdXu4gcNhgn8uUpYNfIHp01B/D2wIz4nFTzsz6gwcREzhHNmTE5lRWQeKj2q+MnZ/YWZ1K14WtQ+7dy+5nD3kR2EvPtADUsM7l+wUbAxXIQluhFFhSOzU7ZKKQ2PI8u0EusQvn7KzYSK7mjcPXS+h7H0DM2GtYWnum9UMFFVjtjo3KFOaCXp+6/hvULNjIFPHdtPdG/1F/BRuck1djvB257/+OfgYt62EOV1yEai+eBs3xtDuB090iLQ+CUhsy3zTkm3nr3HVzYM6C/5KA14PsAYk4H6U+IVmDvyic1WFQssP7oGwBuD/Mfr3ozEDU8yS0rm/eMQQ6enTJmdgLiHziLWQ8xrJErHSCE6gZ6nF0vLsn6TgFOOVS5QQFBmg0kSjLq+Q8vRni6YFog5BcaBXBilL6Wn7GbgYKgBrtkOg6wTjet78S3z8yYxWZC6lZEWimdRYk71GLOOSjwHZzC+20E1WYpKn5l3roDIAPlnW2xYVwKwqs26GYxEB9nj3dCqrZAKywHh48pBj1ykvYaYoqaJOL157MrmvA4PIF5Tyr4FKJ5OAI4lagT680uxstEziIJNNKFfrcl4B9U1k0ckyBczYGjlqIvGt7wAPGDKWsx41j82IBvWQFVd3AqNuy0YkjQboC0lZwZcJMVRGohQDR5uAV4wh45hhyg7J0z2GgNmYfH2oR+KBUNAG6GCUygvt1LzhtmJCKzt+ycEgAfGTCBDLhT3h4iRlxlBzxFs6AtgMk7AiwiyLVK9KxGEEl1DQCHMiHmgrXSrcSEQA9mUIxKAQDmE2bEVMZg1gWOfSaiKBXXZ9uCHtmb3dAE7h11D36g93i68kEDneDUJLQM34awbgpK4UUrhFAMCUXTAAAbJpULzKFKCJDA4moEaOcOaGEvDI/gDlF9mVZDkHAMItQ3AGRqgnzXuqmFiHmCEDjupmVBXXBu6kYaFW2kG0MvAp0CajJmwgYaqPDSQKbjOTRJ1YAxpLiEjJhqNMrnaMPxg8oCn2vwLsyARAzBSWFFobgs0en+V9IcTg2Yu0Y60A5Bct/6CUUQhnobAJilsxSMTGfUEZoIOfYpAICbdTgHokkFsNcJouBztQL6SE3MUHefnoUDIrtpAzNtA8zT2RrCj0ahNiKgsKcNgCZ8G8hATGHMOuzFXCGxZlzH0H7shaEXbjR5BaNGb8++XUNgz/Yj/nApbkUBN50+/nVvtcupg0B0EvOtSa7ko0Zbo86NrYX3f747slI0zYIQOp25559jUyIssLvnnPsX8CWO9JU/V1B/BGqKveYFYAkmvkZyMedLsECO+QbJppwHYSK34URdENLZCNFtyJNheRCNEAVITeL6IIKj2OBBx0exvIzGgMZxT2ci54driF9GJ56aId87YG52quvYYyeZkIzxidRVrhOShi/1xAk7QFkY0VloF9gmaFP+hUxKxzjyKDS7fpC9pkxKoQ6/oEEgw8NxWn4RTZKSedPboIfr6EBnIErRwoRcxy1Fhyqk6BrUs/YAWhbQUPSpcrRNXyPrZ1ycEmVxuuFNwqme8OAh2bhpeZ4py/NtBy+JRBBk43bYLBjD4pjcGhQiCJKplmbF5iUFe5iAHAkw0arM4K/wKWC97TWA5xR/7tizbYLW/0MqUlH7Np2naNPdGMwabn6E7rVOjAp4lqgalYAKWrXIPjKms+UmhtnTt2qhIFuj6wjXmREg34H9jd2RLPTRdv04bzEn9D4N2vWCsFjhS3nFyez3M8kDooSFjrIBvCzhDUzioNZRNmPuMvMUUxAFQtfVPrn/Cg1pBT3kTEPbSWQgrANKWI/8i7ZrVLRd8Y24ZKh2YxBzwErtvRC9MTF+ZUBcvsOsYY9EIg7Yom6Vsx9L6hYrKSq4oUZynkoGLbqsgODjgBYhcfBFXncbXEgCtOUIREPfx1LcnJaXif++uZQ8uAEfGvqeYAKGSEUJS/H4oov7ob0N4rdDDwIGIbQPeiMBA+Ckk3Cc6/RB3Z4my64oy6JbjiUcqxd1z+paEH0HXxt1AjYU3thQIHD3qcjUVxSPtUkZM/yCnCrQlOn9aB7Hg4yn+KPsm8IYcAYhOppAvdWHOhnPAHwyEDLt5km5/Jwkj94KQLLvDaVc9mK243Ah3TLgY4OYjXxmR32dJsRsKHLPIP85R1mT9/1Vznc2kPN5SpG1EB3SH4AQS6olnbdUPv6J8aHlr5F4b9fIHDgSdK63z8h6gUy2B57fh+fnhdWlU2GzibS6FdLuiDpCJKTd7e+I23MpbjeU93u1q/Dn8fdiYHC47UbWHWZP/1oYHLZGFo+da4vH7t3a5NJQazTS5DLH5lMOlo3yN3FTdpG90Ql8VgeL4VeQJYLRabbVi5havVYzrV4A/yTNboWl2e3kO7P7Bav8Gbtfjtj9nBgeA43h8fCXuDU8guWzeNLySR4tnwVYPl0g20+YXmtpeq3JNzN0us/+I9svh/+rxmfAtor11u+4Qo4dZ+b3uMTM72UM5vcfhx81r1f7fwL2/+Rq/3+tIqt5/wcxl77VYrZdaQAAAABJRU5ErkJggg==') no-repeat center center;");
ToHTML("background-size: contain;");
ToHTML("}");
ToHTML(".progress");
ToHTML("{");
ToHTML("border-radius: 10px;");
ToHTML("height: 4px;");
ToHTML("width: 100%;");
ToHTML("max-width: 220px;");
ToHTML("background-color: #FFFFFF;");
ToHTML("margin: 20px auto;");
ToHTML("overflow: hidden;");
ToHTML("text-align: left;");
ToHTML("border: solid 2px #000000;");
ToHTML("}");
ToHTML(".progress>div");
ToHTML("{");
ToHTML("height: inherit;");
ToHTML("width: 0;");
ToHTML("background-color: rgba(0,0,0,0.6);");
ToHTML("border-radius: inherit;");
ToHTML("}");
ToHTML("#speechTitle,#speechAuthor");
ToHTML("{");
ToHTML("overflow: hidden;");
ToHTML("padding: 8px 0;");
ToHTML("font-weight: bold;");
ToHTML("}");
ToHTML("#speechTitle");
ToHTML("{");
ToHTML("font-size: 1em;");
ToHTML("height: 1em;");
ToHTML("}");
ToHTML("#speechAuthor");
ToHTML("{");
ToHTML("font-size: 0.8em;");
ToHTML("height: 0.8em;");
ToHTML("}");
ToHTML(".wind");
ToHTML("{");
ToHTML("-moz-box-sizing: border-box;");
ToHTML("box-sizing: border-box;");
ToHTML("border-radius: 10px;");
ToHTML("background-color: #EEEEEE;");
ToHTML("display: none;");
ToHTML("position: absolute;");
ToHTML("width: 240px;");
ToHTML("margin-top: -100px;");
ToHTML("margin-left: -120px;");
ToHTML("height: auto;");
ToHTML("max-height: 220px;");
ToHTML("left: 50%;");
ToHTML("top: 50%;");
ToHTML("z-index: 101;");
ToHTML("padding: 12px;");
ToHTML("text-align: center;");
ToHTML("overflow: auto;");
ToHTML("-webkit-overflow-scrolling: touch;");
ToHTML("font-size: 0.8em;");
ToHTML("line-height: 1.6em;");
ToHTML("}");
ToHTML(".wind:hover");
ToHTML("{");
ToHTML("border-color: #000000;");
ToHTML("}");
ToHTML("@media only screen and (max-width:520px), only screen and (max-height:520px)");
ToHTML("{");
ToHTML("body");
ToHTML("{");
ToHTML("font-size: 20px;");
ToHTML("}");
ToHTML("#speechButton");
ToHTML("{");
ToHTML("width: 32px;");
ToHTML("height: 32px;");
ToHTML("}");
ToHTML("#speechButtonMenu");
ToHTML("{");
ToHTML("width: 18px;");
ToHTML("height: 18px;");
ToHTML("}");
ToHTML(".speechActionButton");
ToHTML("{");
ToHTML("width: 45px;");
ToHTML("height: 30px;");
ToHTML("line-height: 30px;");
ToHTML("}");
ToHTML("}");
ToHTML("#console");
ToHTML("{");
ToHTML("-moz-user-select:text; -o-user-select:text; -khtml-user-select:text; -webkit-user-select:text; -ms-user-select:text; user-select:text; -moz-box-sizing:border-box; -webkit-box-sizing:border-box; box-sizing:border-box; font-family:Menlo, Monaco, 'Droid Sans Mono', 'Courier New', Courier, 'Lucida Console', monospace; position:absolute; top:0; right:0; left:0; z-index:99; opacity:0.85; padding:0; color:#222222; background-color:#FFFFFF; height:0; overflow:hidden; -webkit-overflow-scrolling:touch; font-size:12px; -webkit-transition:height 0.5s ease; -moz-transition:height 0.5s ease; -ms-transition:height 0.5s ease; -o-transition:height 0.5s ease; transition:height 0.5s ease;");
ToHTML("}");
ToHTML(".dialogButton {-moz-box-sizing: border-box; box-sizing: border-box; border-radius: 6px; padding: 2px; background-color:#CCCCCC; width:45%; min-width:45%; display:inline-block;}");
ToHTML(".dialogButton:hover {color: #EEEEEE; background-color: #222222; text-shadow: none;}");
ToHTML(".wind .plink {padding: 1px; display:block; width:100%;}");
ToHTML(".wind .plink:hover {color: #EEEEEE; background-color: #222222; text-shadow: none; border-radius: 6px;}");
ToHTML(AnimateCSS());
ToHTML("</style>");
}
var Newline;
function ToHTMW(str)
{
html += str;
}
function ToHTML(str)
{
html += str+'\n';
}
function Format1(LocText)
{
var dest = '';
dest = TargetFormat=='html' ? '<h6>*&nbsp;*&nbsp;*</h6>' : '';
LocText = LocText.replace(/^\s*?\*\s*?\*\s*?\*\s*?$/gm,dest);
dest = TargetFormat=='html' ? '$1<i>$2</i>' : '$1$2';
LocText = LocText.replace(/([^:]|^)\/\/(.*?(?!\/\/).*?)\/\//g,dest);
dest = TargetFormat=='html' ? '<tt>$1</tt>' : '$1';
LocText = LocText.replace(/{{{([^{}]+?)}}}/g,dest);
LocText = ReplaceInText(LocText,"''","\"\"","<<",">>");
dest = TargetFormat=='html' ? '<b>$1</b>' : '$1';
LocText = LocText.replace(/''(.*?(?!'').*?)''/g,dest);
dest = TargetFormat=='html' ? '<u>$1</u>' : '$1';
LocText = LocText.replace(/__((.*?(?!__).*?))__/g,dest);
dest = TargetFormat=='html' ? '<div align=center>$1</div>' : '$1';
LocText = LocText.replace(/===((.*?(?!===).*?))===/g,dest);
dest = TargetFormat=='html' ? '<div align=right>$1</div>' : '$1';
LocText = LocText.replace(/@@@((.*?(?!@@@).*?))@@@/g,dest);
dest = TargetFormat=='html' ? '<div class=header>$1</div>' : '$1';
LocText = LocText.replace(/%%%((.*?(?!@@@).*?))%%%/g,dest);
dest = TargetFormat=='html' ? '<br>' : '\n';
LocText = LocText.replace(/<<\s*br\s*>>/g,dest);
return LocText;
}
function Format2(LocText)
{
var dest = '';
dest = TargetFormat=='html' ? '<sub>$1</sub>' : '$1';
LocText = LocText.replace(/~~((.*?(?!~~).*?))~~/g,dest);
dest = TargetFormat=='html' ? '<sup>$1</sup>' : '$1';
LocText = LocText.replace(/\^\^((.*?(?!\^\^).*?))\^\^/g,dest);
dest = TargetFormat=='html' ? "<hr class='hr'>" : '';
LocText = LocText.replace(/^\s*----\s*$/gm,dest);
LocText = LocText.replace(/--/g,tire);
dest = TargetFormat=='html' ? '<ul><li>$1</li></ul>' : '$1';
LocText = LocText.replace(/^\s*[*#]\s+(.+?)$/gm,dest);
return LocText;
}
function ReplaceReturns(LocText)
{
LocText = ReplaceInText(LocText,"\n","","<<nop>>","<<endnop>>");
LocText = LocText.replace(/[\r\n]+/g,Newline);
return LocText;
}
function normalText(s)
{
s = s.replace(/\&(.+?)\:html_code/g,"&$1;");
s = s.replace(new RegExp(tz,'g'),";");
s = s.replace(/\[\[asmQuot\]\]/g,'"');
s = s.replace(/\[\[asmBSlash\]\]/g,'\\');
return s;
}
function Compile(TestMode,OnlyPassageNum,PublishToInet)
{
var isPrint = false;
if (TestMode || TargetFormat=='html')
{
Newline = '<p></p>';
}
else
{
Newline = '[[asmBSlash]][[asmBSlash]]n';
}
TargetFormatBak = TargetFormat;
if (TestMode)
{
TargetFormat = 'html';
}
ShowWind(false);
if (TargetFormat=='audio')
{
speechStrings = [];
}
if (EditorMode)
{
if (OnlyPassageNum==-1)
{
EditrSave();
}
}
else if (EditorVisible)
{
if (OnlyPassageNum!=-1 && editNewLoc)
{
OnlyPassageNum = LocationArr.length;
}
SaveLocation(false);
}
if (LocationArr.length<1 || OnlyPassageNum>LocationArr.length-1)
{
Message(Lang_NoPassage);
TargetFormat = TargetFormatBak;
return;
}
var StoryStyleIndex = GetLocIndexByTitle('StoryStyle');
var TimeToFade = 500;
var LocNum = [];
if (OnlyPassageNum==-1)
{
for (var i=0; i<LocationArr.length; i++)
{
if (!ForbiddenLocation(LocationArr[i].Title))
{
LocNum.push(i);
}
}
}
else
{
LocNum.push(OnlyPassageNum);
var s = LocationArr[OnlyPassageNum].Text;
var myMatch;
var myRe = DisplayPattern;
var i;
while ((myMatch = myRe.exec(s)) != null)
{
i = GetLocIndexByTitle(myMatch[2]);
if (i>=0)
{
LocNum.push(i);
}
}
LocNum.sort();
for (var i=LocNum.length-1; i>=1; i--)
{
if (LocNum[i]==LocNum[i-1])
{
LocNum.splice(i,1);
}
}
}
ErrorsClear();
for (var i=0; i<LocNum.length; i++)
{
var Location = LocationArr[LocNum[i]];
ErrorsCheck(Location.Text,Location.Title,TargetFormatBak);
}
if (!noError)
{
if (OnlyPassageNum==-1)
{
ShowErrors();
}
else
{
ShowErrors(true);
}
TargetFormat = TargetFormatBak;
return;
}
FilesLinks = [];
var Vars = [];
var loop_n = 0;
for (var i=0; i<LocationArr.length; i++)
{
for (var j=0; j<LocationArr[i].Vars.length; j++)
{
Vars.push(LocationArr[i].Vars[j].toLowerCase());
}
}
Vars = ArrayUnique(Vars);
Vars = ArraySortLen(Vars);
html = '';
if (TargetFormat=='html')
{
ToHTML (ThemeTop(false,TestMode,OnlyPassageNum));
ToHTML("function isMobile() {");
ToHTML("if (getComputedStyle(showImageButton,null).display=='none')");
ToHTML("{");
ToHTML("return false;");
ToHTML("}");
ToHTML("return true;");
ToHTML("}");
ToHTML("function Resize() {");
ToHTML("if (isMobile()) {");
ToHTML("printImageBlock.style.display = 'none';");
ToHTML("}");
ToHTML("else {");
ToHTML("printImageBlock.style.display = 'block';");
ToHTML("}");
ToHTML("}");
ToHTML("window.addEventListener('resize', Resize, false);");
ToHTML("window.addEventListener('orientationchange', Resize, false);");
ToHTML("document.addEventListener('dragstart', function(e){e.preventDefault();});");
ToHTML("var tmpdiv = document.createElement('div');");
ToHTML("tmpdiv.innerHTML = '<!--[if lt IE 10]><i></i><![endif]-->';");
ToHTML("var isIeLessThan10 = (tmpdiv.getElementsByTagName('i').length==1);");
ToHTML("if (isIeLessThan10) {document.onselectstart = function(){return false;}};");
ToHTML("window.onkeyup = function(e) {keysUp(e);};");
ToHTML("window.onkeydown = function(e) {keysDown(e);};");
ToHTML("printImage.onerror = function() {badImageShow(this);};");
if (style_view!='vis') {
ToHTML("function rgb2hex(rgb) {");
ToHTML("rgb = rgb.match(/^rgba?[[[asmBSlash]]s+]?[[asmBSlash]]([[[asmBSlash]]s+]?([[asmBSlash]]d+)[[[asmBSlash]]s+]?,[[[asmBSlash]]s+]?([[asmBSlash]]d+)[[[asmBSlash]]s+]?,[[[asmBSlash]]s+]?([[asmBSlash]]d+)[[[asmBSlash]]s+]?/i);");
ToHTML("return (rgb && rgb.length === 4) ? '#'+('0'+parseInt(rgb[1],10).toString(16)).slice(-2)+('0'+parseInt(rgb[2],10).toString(16)).slice(-2)+('0'+ parseInt(rgb[3],10).toString(16)).slice(-2) : '';");
ToHTML("}");
ToHTML("var windElems = [prefsDiv,dialogWind,printAdd];");
ToHTML("for (var key in windElems) {");
ToHTML("var elem = windElems[key];");
ToHTML("var bg = getComputedStyle(elem,null).backgroundColor;");
ToHTML("if (bg.substr(0,3)=='rgb') {");
ToHTML("elem.style.backgroundColor = rgb2hex(bg);");
ToHTML("}");
ToHTML("}");
}
if (style_view=='vis') {
ToHTML("var typer = {");
ToHTML("tags: ['img','button','input','u','textarea','hr','li','td','tr','th','table','thead','tfoot','tbody','video','audio'],");
ToHTML("classes: ['button','buttonno','buttonback','myinput'],");
ToHTML("class1: 'typer_1',");
ToHTML("class2: 'typer_2',");
ToHTML("spans: [],");
ToHTML("nextInt: undefined,");
ToHTML("typing: false,");
ToHTML("start: function() {");
ToHTML("this.spans = [];");
ToHTML("clearInterval(this.nextInt); this.nextInt = undefined;");
ToHTML("this.typing = true;");
ToHTML("printDiv.style.visibility = 'hidden';");
ToHTML("var text = printDiv.innerHTML;");
ToHTML("var textNodes = this.textNodesUnder(printDiv);");
ToHTML("var tag;");
ToHTML("for (i=0; i<textNodes.length; i++) {");
ToHTML("var node = textNodes[i];");
ToHTML("var words = node.textContent;");
ToHTML("if (node.nodeType==3) {");
ToHTML("var parentNode = node.parentNode;");
ToHTML("var parentNodeTag = this.getTag(parentNode);");
ToHTML("tag = '';");
ToHTML("if (parentNodeTag=='a') {");
ToHTML("tag += '<span class='+this.class1+'>'+words+'</span>';");
ToHTML("} else {");
ToHTML("words = words.split(' ');");
ToHTML("for (j=0; j<words.length; j++) {");
ToHTML("var word = words[j];");
ToHTML("if (word!='') {");
ToHTML("tag += '<span class='+this.class1+'>'+word+'</span>';");
ToHTML("}");
ToHTML("if (j<words.length-1) {");
ToHTML("tag += ' ';");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("var newNode = document.createElement('span');");
ToHTML("newNode.innerHTML = tag;");
ToHTML("parentNode.replaceChild(newNode,node);");
ToHTML("} else if (this.tags.indexOf(this.getTag(node))!=-1 || this.classes.indexOf(node.className)!=-1) {");
ToHTML("var classes = node.className;");
ToHTML("if (classes=='') {");
ToHTML("node.className = this.class1;");
ToHTML("} else {");
ToHTML("node.className += ' '+this.class1;");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("printDiv.style.visibility = 'visible';");
ToHTML("this.spans = [].slice.call(printDiv.getElementsByClassName(this.class1));");
ToHTML("if (this.spans.length>0) {");
ToHTML("this.next(0,this);");
ToHTML("} else {");
ToHTML("this.stop();");
ToHTML("}");
ToHTML("},");
ToHTML("next: function(i,cont) {");
ToHTML("this.nextInt = setTimeout(function() {");
ToHTML("var classes = cont.spans[i].className;");
ToHTML("cont.spans[i].className = classes.replace(cont.class1,cont.class2);");
ToHTML("i++;");
ToHTML("if (i<cont.spans.length) {");
ToHTML("typer.next(i,cont);");
ToHTML("} else {");
ToHTML("cont.typing = false;");
ToHTML("cont.nextInt = undefined;");
ToHTML("clearInterval(cont.nextInt);");
ToHTML("setTimeout(function() {cont.stop();}, 600);");
ToHTML("}");
ToHTML("}, 250);");
ToHTML("},");
ToHTML("stop: function() {");
ToHTML("var buttonsList = $('buttonsList');");
ToHTML("if (buttonsList && buttonsList.style.display=='none') {");
ToHTML("buttonsList.style.display = 'block';");
ToHTML("Animate(buttonsList,'effect_scale');");
ToHTML("}");
ToHTML("if (this.typing) {");
ToHTML("clearInterval(this.nextInt);");
ToHTML("this.typing = false;");
ToHTML("for (i=0; i<this.spans.length; i++) {");
ToHTML("var classes = this.spans[i].className;");
ToHTML("this.spans[i].className = classes.replace(this.class1,this.class2);");
ToHTML("}");
ToHTML("}");
ToHTML("},");
ToHTML("textNodesUnder: function(node) {");
ToHTML("var all = [];");
ToHTML("for (node=node.firstChild;node;node=node.nextSibling){");
ToHTML("if (node.nodeType==3 || this.tags.indexOf(this.getTag(node))!=-1 || this.classes.indexOf(node.className)!=-1) {");
ToHTML("all.push(node);");
ToHTML("}");
ToHTML("else {");
ToHTML("all = all.concat(this.textNodesUnder(node));");
ToHTML("}");
ToHTML("}");
ToHTML("return all;");
ToHTML("},");
ToHTML("getTag: function(node) {");
ToHTML("return node.tagName.toLowerCase();");
ToHTML("}");
ToHTML("};");
}
ToHTML("var currentStyles = [];");
ToHTML("function isInputActive() {");
ToHTML("var tag = document.activeElement.tagName.toLowerCase();");
ToHTML("if (tag=='input' || tag=='textarea') {return true;} else {return false;};");
ToHTML("}");
ToHTML("function keysDown(event) {");
ToHTML("if (isInputActive()) {return;};");
ToHTML("var key = keyCode(event);");
ToHTML("if (key==32) {event.preventDefault();}");
ToHTML("}");
ToHTML("function keysUp(event) {");
ToHTML("if (isInputActive()) {return;};");
ToHTML("if (showCover) {showPrintAdd(false); return;};");
ToHTML("var key = keyCode(event);");
ToHTML("if (key==27) {closeWinds(); return;}");
if (style_view!='vis') {
if (style_view!='rpg') {
ToHTML("if (key==32) {showImageToggle(); return;}");
}
} else {
ToHTML("if (key==32) {typer.stop(); return;}");
}
ToHTML("if (key==8)  {Back(true); return;}");
ToHTML("var keyN = -1;");
ToHTML("if (key>=49 && key<=57)  {keyN=key-49;}");
ToHTML("if (key>=97 && key<=105) {keyN=key-97;}");
ToHTML("if (keyN>=0) {");
ToHTML("var elems = menuDiv.querySelectorAll('*');");
ToHTML("var clickElems = [];");
ToHTML("for (var i=0; i<elems.length; i++) {");
ToHTML("if (elems[i].onclick) {");
ToHTML("clickElems.push(elems[i]);");
ToHTML("}");
ToHTML("}");
ToHTML("if (clickElems[keyN]) {clickElems[keyN].click();};");
ToHTML("}");
ToHTML("}");
if (style_view=='vis') {
ToHTML("function togglePrint(force) {");
ToHTML("if (printDiv.textContent.trim()=='') {");
ToHTML("printContDiv.style.visibility = 'hidden';");
ToHTML("} else {");
ToHTML("printContDiv.style.visibility = 'visible';");
ToHTML("}");
ToHTML("var opacity = window.getComputedStyle(printContDiv,null).getPropertyValue('opacity');");
ToHTML("var objs = [printContDiv,toolbarDiv,$('menu'),$('buttonsList')];");
ToHTML("for (var i in objs) {");
ToHTML("var obj = objs[i];");
ToHTML("if (obj) {");
ToHTML("if (force || opacity==0) {");
ToHTML("obj.style.pointerEvents = 'auto';");
ToHTML("obj.style.opacity = 1;");
ToHTML("} else {");
ToHTML("obj.style.pointerEvents = 'none';");
ToHTML("obj.style.opacity = 0;");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("}");
}
ToHTML("function showImageToggle()");
ToHTML("{");
ToHTML("if (isMobile()) {");
ToHTML("if (getComputedStyle(printImageBlock,null).display=='none') {printImageBlock.style.display='block';} else {printImageBlock.style.display='none';}");
ToHTML("}");
ToHTML("else {");
ToHTML("if (getComputedStyle(printAddCont,null).display=='none') {showImage();} else {showPrintAdd(false);}");
ToHTML("}");
ToHTML("}");
ToHTML("function showImageForce()");
ToHTML("{");
ToHTML("if (isMobile()) {");
ToHTML("printImageBlock.style.display = 'block';");
ToHTML("}");
if (style_view=='vis') {
ToHTML("Animate(printImage,'effect_fade');");
}
ToHTML("}");
ToHTML("function goodImageShow(img)");
ToHTML("{");
ToHTML("if(isTouchDevice) {");
ToHTML("mybody = document.body;");
ToHTML("mybody.style.display = 'none';");
ToHTML("scrollDiv(mybody);");
ToHTML("mybody.style.display = 'block';");
ToHTML("}");
ToHTML("}");
ToHTML("function badImageShow(img)");
ToHTML("{");
if (TestMode && TargetChromeApp) {
ToHTML("var src = img.src.trim();");
ToHTML("if (/^https?:[[asmBSlash]]/[[asmBSlash]]//.test(src)) {");
ToHTML("img.src = badImage;");
ToHTML("} else {");
ToHTML("if (/preview[[asmBSlash]].html#$/.test(src)) {");
ToHTML("return;");
ToHTML("}");
ToHTML("if (img.id) {");
ToHTML("var id = img.id;");
ToHTML("} else {");
ToHTML("var id = 'localImg_'+Math.random();");
ToHTML("img.id = id;");
ToHTML("}");
ToHTML("img.src = pictureDefault;");
ToHTML("img.onload = function() {");
ToHTML("MessageFromFrame('GetDataURL='+src+'\t'+id);");
ToHTML("this.onload = null;");
ToHTML("};");
ToHTML("}");
} else {
ToHTML("img.src = badImage;");
}
ToHTML("}");
ToHTML("function showImage()");
ToHTML("{");
ToHTML("if (!isMobile()) {");
ToHTML("printAddText.innerHTML = \"<img src='\"+printImage.src+\"' style='height:600px;margin:0;padding:0;' border='0' onerror='badImageShow(this)'>\";");
ToHTML("printAddText.innerHTML+=spritesDiv.innerHTML;");
ToHTML("showPrintAdd(true,false,true);");
ToHTML("}");
ToHTML("else {");
ToHTML("printImageBlock.style.display = 'none';");
ToHTML("}");
ToHTML("}");
ToHTML("var printAddTextAlign = printAddText.style.textAlign;");
ToHTML("function showPrintAdd(show,bounce,center)");
ToHTML("{");
ToHTML("if (show) {");
ToHTML("closeWinds();");
ToHTML("showShadow(true);");
ToHTML("if (center) {printAddText.style.textAlign='center';} else {printAddText.style.textAlign=printAddTextAlign;}");
ToHTML("var inner = printAddText.innerHTML;");
ToHTML("inner = inner.replace(/(<p><[[asmBSlash]]/p>)*?<span class=\"buttonback\"[^<]+?<[[asmBSlash]]/span>(<p><[[asmBSlash]]/p>)*?/gi,'').trim();");
ToHTML("if (inner=='') {return;}");
ToHTML("printAddText.innerHTML = inner;");
ToHTML("printAddCont.style.display = 'block'; scrollDiv(printAdd); if (isAnimation && animation_enable) { var effect='effect_scale'; if (bounce) {effect='effect_bounce';}; Animate(printAdd,effect); } else { fade(printAddText); }");
ToHTML("}");
ToHTML("else {");
ToHTML("if (showCover) {musicAudio.play();};");
ToHTML("showShadow(false);");
ToHTML("printAddCont.style.display = 'none';");
ToHTML("}");
ToHTML("}");
ToHTML("function MenuInit()");
ToHTML("{");
ToHTML("var StoryMenu = GetLocation('StoryMenu');");
ToHTML("if (StoryMenu!=false)");
ToHTML("{");
ToHTML("if (StoryMenu.Text.search(/<span class='(button|plink)/)!=-1)");
ToHTML("{");
ToHTML("menuDiv.innerHTML='';");
ToHTML("eval(StoryMenu.Text);");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("var fontSize = 1;");
ToHTML("function ChangeFontSize(){");
ToHTML("if (fontSize<1.2){");
ToHTML("fontSize = fontSize + 0.1;");
ToHTML("}");
ToHTML("else {");
ToHTML("fontSize = 0.8");
ToHTML("}");
ToHTML("fade(printDiv);");
ToHTML("printDiv.style.fontSize = fontSize+'em';");
ToHTML("printAdd.style.fontSize = fontSize+'em';");
ToHTML("}");
ToHTML("function scrollDiv(obj,bottom) {");
ToHTML("if (!obj) {");
ToHTML("obj = printContDiv;");
ToHTML("}");
ToHTML("if (bottom) {");
ToHTML("obj.scrollTop = obj.scrollHeight;");
ToHTML("}");
ToHTML("else {");
ToHTML("obj.scrollTop = 0;");
ToHTML("}");
ToHTML("}");
}
else
{
speechTop(TestMode);
speechScript(TestMode);
}
ToHTML("var isWebKit = 'WebkitAppearance' in document.documentElement.style;");
ToHTML("var isFirefox = navigator.userAgent.toLowerCase().indexOf('firefox') > -1;");
ToHTML("var LangCode = '"+LangCode+"';");
ToHTML("function closeWinds() {");
if (TargetFormat=='html')
{
ToHTML("showPrintAdd(false);");
}
ToHTML("showPrefs(true);");
ToHTML("showShadow(false);");
ToHTML("dialogWind.style.display = 'none';");
ToHTML("}");
ToHTML("function showShadow(show)");
ToHTML("{");
ToHTML("if (show) {");
ToHTML("if (!$('shadowlayer')) {");
ToHTML("var shadow = document.createElement('div');");
ToHTML("shadow.id='shadowlayer'; document.body.appendChild(shadow);");
ToHTML("}");
ToHTML("} else {");
ToHTML("var shadow = $('shadowlayer');");
ToHTML("if (shadow) {");
ToHTML("document.body.removeChild(shadow);");
ToHTML("}");
ToHTML("showCover = false;");
if (TargetFormat=='html') {
ToHTML("printAddCont.style.display = 'none';");
}
ToHTML("}");
ToHTML("}");
ToHTML("function keyCode(event) {");
ToHTML("return event.keyCode ? event.keyCode : event.charCode ? event.charCode : event.which;");
ToHTML("}");
ToHTML("var isTouchDevice = false;");
ToHTML("if ('ontouchmove' in window)");
ToHTML("{");
ToHTML("isTouchDevice = true;");
ToHTML("}");
ToHTML("if (isTouchDevice) {");
ToHTML("try {");
ToHTML("var ignore = /:hover/;");
ToHTML("for (var i = 0; i < document.styleSheets.length; i++) {");
ToHTML("var sheet = document.styleSheets[i];");
ToHTML("if (!sheet.cssRules) {");
ToHTML("continue;");
ToHTML("}");
ToHTML("for (var j = sheet.cssRules.length - 1; j >= 0; j--) {");
ToHTML("var rule = sheet.cssRules[j];");
ToHTML("if (rule.type === CSSRule.STYLE_RULE && ignore.test(rule.selectorText)) {");
ToHTML("sheet.deleteRule(j);");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("catch(e) {");
ToHTML("}");
ToHTML("}");
ToHTML("var musicAudio = $('musicAudio');");
ToHTML("musicAudio.addEventListener('play',function() {");
ToHTML("audioPlayEvent();");
ToHTML("},false);");
ToHTML("musicAudio.addEventListener('pause',function() {");
ToHTML("audioPauseEvent();");
ToHTML("},false);");
ToHTML("musicAudio.addEventListener('durationchange',function() {");
ToHTML("audioPlayEvent();");
ToHTML("},false);");
ToHTML("musicAudio.addEventListener('error',function() {");
if (TestMode && TargetChromeApp) {
ToHTML("MessageFromFrame('GetDataURL='+musicAudio.src+'\t'+'musicAudio');");
} else {
ToHTML("if (this.src.substr(-4)=='.mp3') {");
ToHTML("AlertMessage('"+Lang_ErrorLoadFile+"<br>'+this.src);");
ToHTML("}");
}
ToHTML("},false);");
ToHTML("var audio_enable = true;");
ToHTML("var animation_enable = true;");
ToHTML("var showCover = false;");
ToHTML("var TimeToFade = "+TimeToFade+";");
ToHTML("var isAnimation= false;");
ToHTML("if (document.body.style.WebkitAnimation!==undefined || document.body.style.MozAnimation!==undefined || document.body.style.OAnimation!==undefined || document.body.style.msAnimation!==undefined || document.body.style.KhtmlAnimation!==undefined || document.body.style.animation!==undefined)");
ToHTML("{");
ToHTML("isAnimation = true;");
ToHTML("}");
ToHTML("function tbAnimate(obj) {");
ToHTML("Animate(obj,'effect_scale');");
ToHTML("}");
ToHTML("function prefixedEventListener(element, type, callback) {");
ToHTML("var pfx = ['webkit', 'moz', 'MS', 'o', ''];");
ToHTML("for (var p=0; p<pfx.length; p++) {");
ToHTML("if (!pfx[p]) type = type.toLowerCase();");
ToHTML("element.addEventListener(pfx[p]+type, callback, false);");
ToHTML("}");
ToHTML("}");
ToHTML("function Animate(obj,animID)");
ToHTML("{");
ToHTML("if (isAnimation) {");
ToHTML("var objclass = obj.className.split(' ')[0];");
ToHTML("obj.className = objclass+' '+animID;");
ToHTML("prefixedEventListener(obj,'AnimationEnd',function() {");
ToHTML("obj.className = objclass;");
ToHTML("}, false);");
ToHTML("}");
ToHTML("}");
ToHTML("var confirm_func;");
ToHTML("function AreYouSure(func,text)");
ToHTML("{");
ToHTML("showPrefs(true);");
if (TargetFormat=='audio')
{
ToHTML("showSpeechBlock(true);");
}
ToHTML("confirm_func = func;");
ToHTML("if (!text)");
ToHTML("{");
ToHTML("text = '"+Lang_AreYouSure+"';");
ToHTML("}");
ToHTMW("	text += [[asmQuot]]<div style='margin-top:16px;'><span class='dialogButton' onclick=[[asmBSlash]][[asmQuot]]showShadow(false);dialogWind.style.display='none';");
if (TargetFormat=='audio')
{
ToHTMW("  showSpeechBlock();");
}
ToHTML("[[asmBSlash]][[asmQuot]] style='float:left;'>"+Lang_NO+"</span><span class='dialogButton' style='float:right;' onclick='window[confirm_func]();event.stopPropagation();'>"+Lang_YES+"</span></div>[[asmQuot]];");
ToHTML("dialogWind.innerHTML = text;");
ToHTML("showShadow(true);");
ToHTML("dialogWind.style.display = 'block';");
ToHTML("Animate(dialogWind,'effect_fade_fast');");
ToHTML("}");
ToHTML("function AlertMessage(text)");
ToHTML("{");
if (TargetFormat=='audio')
{
ToHTML("showSpeechBlock(true);");
}
ToHTMW("	text += [[asmQuot]]<div style='margin-top:16px;'><span class='dialogButton' onclick=[[asmBSlash]][[asmQuot]]showShadow(false);dialogWind.style.display='none';");
if (TargetFormat=='audio')
{
ToHTMW("	showSpeechBlock();");
}
ToHTML("[[asmBSlash]][[asmQuot]]>OK</span></div>[[asmQuot]];");
ToHTML("dialogWind.innerHTML = text;");
ToHTML("showShadow(true);");
ToHTML("dialogWind.style.display = 'block';");
ToHTML("Animate(dialogWind,'effect_fade_fast');");
ToHTML("}");
ToHTML("var isLocalFile;");
ToHTML("if (location.href.substr(0,7)=='file://')");
ToHTML("{isLocalFile=true;}");
ToHTML("else");
ToHTML("{isLocalFile=false;}");
ToHTML("function isLocalStorageAvailable() {");
ToHTML("if (!window.localStorage) {");
ToHTML("return false;");
ToHTML("}");
ToHTML("else {");
ToHTML("return true;");
ToHTML("}");
ToHTML("}");
ToHTML("function isArray(inputArray) {");
ToHTML("return inputArray && !(inputArray.propertyIsEnumerable('length')) && typeof inputArray==='object' && typeof inputArray.length==='number';}");
ToHTML("function LocNotFound(loc)");
ToHTML("{");
if (OnlyPassageNum==-1)
{
if (TestMode)
{
ToHTML("AlertMessage('"+Lang_Location+"<br>[[asmQuot]]'+loc+'[[asmQuot]]<br>"+Lang_NotFound+"');");
}
else
{
ToHTML("AlertMessage('"+Lang_Location+" "+Lang_NotFound+"');");
}
}
ToHTML("}");
ToHTMW("var storydata;");
var data = GetLocByTitle('StoryData');
if (data)
{
data = data.Text.split('\n');
ToHTMW("storydata = [];var obj;");
for (var key in data)
{
var line = data[key];
if (line.indexOf('::')==-1)
{
continue;
}
line = line.split('::');
line[0] = line[0].toLowerCase().trim();
line[1] = line[1].trim();
var datatype = line[1].substr(0,11);
if (datatype=='data:image/')
{
datatype = "Image";
}
else if (datatype=='data:audio/')
{
datatype = "Audio";
}
else
{
continue;
}
ToHTMW ("obj=new "+datatype+"();obj.src='"+line[1]+"';storydata.push(['"+line[0]+"',obj]);");
}
data = '';
}
ToHTML("function GetFileData(id) {");
ToHTML("if (/^http(s)?:[[asmBSlash]]/[[asmBSlash]]/.+?$/.test(id)) {");
ToHTML("var ret = {};");
ToHTML("ret.src = id;");
ToHTML("return ret;");
ToHTML("}");
ToHTML("id = id.trim().toLowerCase();");
ToHTML("for (var key in storydata) {");
ToHTML("if (storydata[key][0]==id) {");
ToHTML("return storydata[key][1];");
ToHTML("}");
ToHTML("}");
ToHTML("return '';");
ToHTML("}");
ToHTML("function prefsFill()");
ToHTML("{");
ToHTML("prefsDiv.innerHTML='';");
ToHTML("if (!showCover) {");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]AreYouSure(&quot;NewGame&quot;,&quot;"+Lang_StartAgain+"&quot;);event.stopPropagation();[[asmBSlash]][[asmQuot]]>"+Lang_HTMLRestart+"</span></div>';");
ToHTML("}");
ToHTMW("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]SaveLoadWind(true);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLSave+"</span></div>");
ToHTML("<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]SaveLoadWind(false);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLLoad+"</span></div>';");
ToHTML("if (audio_enable)");
ToHTML("{");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]audio_enable=false;stopMusic();showPrefs(true);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLSoundOff+"</span></div>';");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]audio_enable=true;playMusic();showPrefs(true);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLSoundOn+"</span></div>';");
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("if (isAnimation)");
ToHTML("{");
ToHTML("if (animation_enable)");
ToHTML("{");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]animation_enable=false;showPrefs(true);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLAnimOff+"</span></div>';");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]animation_enable=true;showPrefs(true);[[asmBSlash]][[asmQuot]]>"+Lang_HTMLAnimOn+"</span></div>';");
ToHTML("}");
ToHTML("}");
ToHTML("prefsDiv.innerHTML+='<div><span class=plink onclick=[[asmBSlash]][[asmQuot]]ChangeFontSize();[[asmBSlash]][[asmQuot]]>"+Lang_HTMLTextSize+"</span></div>';");
}
ToHTML("}");
ToHTML("function showPrefs(close)");
ToHTML("{");
ToHTML("if(close || prefsDiv.style.display!='none' || dialogWind.style.display!='none') {");
ToHTML("showShadow(false);");
ToHTML("dialogWind.style.display = 'none';");
ToHTML("prefsDiv.style.display = 'none';");
if (TargetFormat=='audio')
{
ToHTML("showSpeechBlock();");
}
ToHTML("}");
ToHTML("else {");
ToHTML("showShadow(true);");
if (TargetFormat=='audio')
{
ToHTML("showSpeechBlock(true);");
}
ToHTML("prefsFill();");
ToHTML("prefsDiv.style.display = 'block';");
ToHTML("Animate(prefsDiv,'effect_fade_fast');");
ToHTML("}");
ToHTML("}");
ToHTML("function setCookie(name, value)");
ToHTML("{");
ToHTML("var expires = new Date();");
ToHTML("expires.setTime(expires.getTime()+31536000000);");
ToHTML("document.cookie = name + '=' + escape(value) + '; expires=' + expires.toGMTString() + '; path=/';");
ToHTML("}");
ToHTML("function getCookie(name)");
ToHTML("{");
ToHTML("var cookie = ' ' + document.cookie;");
ToHTML("var search = ' ' + name + '=';");
ToHTML("var setStr = null;");
ToHTML("var offset = 0;");
ToHTML("var end = 0;");
ToHTML("if (cookie.length>0)");
ToHTML("{");
ToHTML("offset = cookie.indexOf(search);");
ToHTML("if (offset != -1)");
ToHTML("{");
ToHTML("offset += search.length;");
ToHTML("end = cookie.indexOf(';', offset);");
ToHTML("if (end == -1)");
ToHTML("{");
ToHTML("end = cookie.length;");
ToHTML("}");
ToHTML("setStr = decodeURIComponent(cookie.substring(offset, end));");
ToHTML("}");
ToHTML("}");
ToHTML("if (setStr==null)");
ToHTML("{");
ToHTML("setStr = '';");
ToHTML("}");
ToHTML("return(setStr);");
ToHTML("}");
ToHTML("function fade(element)");
ToHTML("{");
ToHTML("if (TimeToFade==0 || !element) {return;}");
ToHTML("var eid = element.id;");
ToHTML("element.style.opacity = '0';");
ToHTML("if (element.FadeState == null)");
ToHTML("{");
ToHTML("if (element.style.opacity == null");
ToHTML("|| element.style.opacity == ''");
ToHTML("|| element.style.opacity == '1')");
ToHTML("{");
ToHTML("element.FadeState = 2;");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("element.FadeState = -2;");
ToHTML("}");
ToHTML("}");
ToHTML("if (element.FadeState == 1 || element.FadeState == -1)");
ToHTML("{");
ToHTML("element.FadeState = element.FadeState == 1 ? -1 : 1;");
ToHTML("element.FadeTimeLeft = TimeToFade - element.FadeTimeLeft;");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("element.FadeState = element.FadeState == 2 ? -1 : 1;");
ToHTML("element.FadeTimeLeft = TimeToFade;");
ToHTML("setTimeout([[asmQuot]]animateFade([[asmQuot]] + new Date().getTime() + [[asmQuot]],'[[asmQuot]] + eid + [[asmQuot]]')[[asmQuot]], 33);");
ToHTML("}");
ToHTML("}");
ToHTML("function animateFade(lastTick, eid){");
ToHTML("var curTick = new Date().getTime();");
ToHTML("var elapsedTicks = curTick - lastTick;");
ToHTML("var element = $(eid);");
ToHTML("if(element.FadeTimeLeft <= elapsedTicks){");
ToHTML("element.style.opacity = '1';");
ToHTML("element.style.filter = 'alpha(opacity = 100)';");
ToHTML("element.FadeState = element.FadeState == 1 ? 2 : -2;");
ToHTML("return;");
ToHTML("}");
ToHTML("element.FadeTimeLeft -= elapsedTicks;");
ToHTML("var newOpVal = element.FadeTimeLeft/TimeToFade;");
ToHTML("newOpVal = 1 - newOpVal;");
ToHTML("element.style.opacity = newOpVal;");
ToHTML("element.style.filter = 'alpha(opacity = ' + (newOpVal*100) + ')';");
ToHTML("setTimeout([[asmQuot]]animateFade([[asmQuot]] + curTick + [[asmQuot]],'[[asmQuot]] + eid + [[asmQuot]]')[[asmQuot]], 33);");
ToHTML("}");
if (TargetFormat=='audio')
{
ToHTML("!function(a){'use strict';function b(a,b){var c=(65535&a)+(65535&b),d=(a>>16)+(b>>16)+(c>>16);return d<<16|65535&c}function c(a,b){return a<<b|a>>>32-b}function d(a,d,e,f,g,h){return b(c(b(b(d,a),b(f,h)),g),e)}function e(a,b,c,e,f,g,h){return d(b&c|~b&e,a,b,f,g,h)}function f(a,b,c,e,f,g,h){return d(b&e|c&~e,a,b,f,g,h)}function g(a,b,c,e,f,g,h){return d(b^c^e,a,b,f,g,h)}function h(a,b,c,e,f,g,h){return d(c^(b|~e),a,b,f,g,h)}function i(a,c){a[c>>5]|=128<<c%32,a[(c+64>>>9<<4)+14]=c;var d,i,j,k,l,m=1732584193,n=-271733879,o=-1732584194,p=271733878;for(d=0;d<a.length;d+=16)i=m,j=n,k=o,l=p,m=e(m,n,o,p,a[d],7,-680876936),p=e(p,m,n,o,a[d+1],12,-389564586),o=e(o,p,m,n,a[d+2],17,606105819),n=e(n,o,p,m,a[d+3],22,-1044525330),m=e(m,n,o,p,a[d+4],7,-176418897),p=e(p,m,n,o,a[d+5],12,1200080426),o=e(o,p,m,n,a[d+6],17,-1473231341),n=e(n,o,p,m,a[d+7],22,-45705983),m=e(m,n,o,p,a[d+8],7,1770035416),p=e(p,m,n,o,a[d+9],12,-1958414417),o=e(o,p,m,n,a[d+10],17,-42063),n=e(n,o,p,m,a[d+11],22,-1990404162),m=e(m,n,o,p,a[d+12],7,1804603682),p=e(p,m,n,o,a[d+13],12,-40341101),o=e(o,p,m,n,a[d+14],17,-1502002290),n=e(n,o,p,m,a[d+15],22,1236535329),m=f(m,n,o,p,a[d+1],5,-165796510),p=f(p,m,n,o,a[d+6],9,-1069501632),o=f(o,p,m,n,a[d+11],14,643717713),n=f(n,o,p,m,a[d],20,-373897302),m=f(m,n,o,p,a[d+5],5,-701558691),p=f(p,m,n,o,a[d+10],9,38016083),o=f(o,p,m,n,a[d+15],14,-660478335),n=f(n,o,p,m,a[d+4],20,-405537848),m=f(m,n,o,p,a[d+9],5,568446438),p=f(p,m,n,o,a[d+14],9,-1019803690),o=f(o,p,m,n,a[d+3],14,-187363961),n=f(n,o,p,m,a[d+8],20,1163531501),m=f(m,n,o,p,a[d+13],5,-1444681467),p=f(p,m,n,o,a[d+2],9,-51403784),o=f(o,p,m,n,a[d+7],14,1735328473),n=f(n,o,p,m,a[d+12],20,-1926607734),m=g(m,n,o,p,a[d+5],4,-378558),p=g(p,m,n,o,a[d+8],11,-2022574463),o=g(o,p,m,n,a[d+11],16,1839030562),n=g(n,o,p,m,a[d+14],23,-35309556),m=g(m,n,o,p,a[d+1],4,-1530992060),p=g(p,m,n,o,a[d+4],11,1272893353),o=g(o,p,m,n,a[d+7],16,-155497632),n=g(n,o,p,m,a[d+10],23,-1094730640),m=g(m,n,o,p,a[d+13],4,681279174),p=g(p,m,n,o,a[d],11,-358537222),o=g(o,p,m,n,a[d+3],16,-722521979),n=g(n,o,p,m,a[d+6],23,76029189),m=g(m,n,o,p,a[d+9],4,-640364487),p=g(p,m,n,o,a[d+12],11,-421815835),o=g(o,p,m,n,a[d+15],16,530742520),n=g(n,o,p,m,a[d+2],23,-995338651),m=h(m,n,o,p,a[d],6,-198630844),p=h(p,m,n,o,a[d+7],10,1126891415),o=h(o,p,m,n,a[d+14],15,-1416354905),n=h(n,o,p,m,a[d+5],21,-57434055),m=h(m,n,o,p,a[d+12],6,1700485571),p=h(p,m,n,o,a[d+3],10,-1894986606),o=h(o,p,m,n,a[d+10],15,-1051523),n=h(n,o,p,m,a[d+1],21,-2054922799),m=h(m,n,o,p,a[d+8],6,1873313359),p=h(p,m,n,o,a[d+15],10,-30611744),o=h(o,p,m,n,a[d+6],15,-1560198380),n=h(n,o,p,m,a[d+13],21,1309151649),m=h(m,n,o,p,a[d+4],6,-145523070),p=h(p,m,n,o,a[d+11],10,-1120210379),o=h(o,p,m,n,a[d+2],15,718787259),n=h(n,o,p,m,a[d+9],21,-343485551),m=b(m,i),n=b(n,j),o=b(o,k),p=b(p,l);return[m,n,o,p]}function j(a){var b,c='';for(b=0;b<32*a.length;b+=8)c+=String.fromCharCode(a[b>>5]>>>b%32&255);return c}function k(a){var b,c=[];for(c[(a.length>>2)-1]=void 0,b=0;b<c.length;b+=1)c[b]=0;for(b=0;b<8*a.length;b+=8)c[b>>5]|=(255&a.charCodeAt(b/8))<<b%32;return c}function l(a){return j(i(k(a),8*a.length))}function m(a,b){var c,d,e=k(a),f=[],g=[];for(f[15]=g[15]=void 0,e.length>16&&(e=i(e,8*a.length)),c=0;16>c;c+=1)f[c]=909522486^e[c],g[c]=1549556828^e[c];return d=i(f.concat(k(b)),512+8*b.length),j(i(g.concat(d),640))}function n(a){var b,c,d='0123456789abcdef',e='';for(c=0;c<a.length;c+=1)b=a.charCodeAt(c),e+=d.charAt(b>>>4&15)+d.charAt(15&b);return e}function o(a){return unescape(encodeURIComponent(a))}function p(a){return l(o(a))}function q(a){return n(p(a))}function r(a,b){return m(o(a),o(b))}function s(a,b){return n(r(a,b))}function t(a,b,c){return b?c?r(b,a):s(b,a):c?p(a):q(a)}'function'==typeof define&&define.amd?define(function(){return t}):a.md5=t}(this);");
}
ToHTML("function Locations(Index, Title, Text, Parsed)");
ToHTML("{");
ToHTML("this.Index = Index;");
ToHTML("this.Title = Title;");
ToHTML("this.Text = Text;");
ToHTML("this.Parsed = Parsed;");
ToHTML("}");
ToHTML("function Trim(str) {return str.replace(/^[[asmBSlash]]s+|[[asmBSlash]]s+$/g,'');}");
ToHTML("function EscapeRegExp(str) {return str.replace(/[[[asmBSlash]]-[[asmBSlash]][[[asmBSlash]]][[asmBSlash]]/[[asmBSlash]]{[[asmBSlash]]}[[asmBSlash]]([[asmBSlash]])[[asmBSlash]]*[[asmBSlash]]+[[asmBSlash]]?[[asmBSlash]].[[asmBSlash]][[asmBSlash]][[asmBSlash]]^[[asmBSlash]]$[[asmBSlash]]|]/g,'[[asmBSlash]][[asmBSlash]]$&');}");
ToHTML("function NthField(str,delim,n,caseSens)");
ToHTML("{");
ToHTML("var cs = 'i';");
ToHTML("if (caseSens)");
ToHTML("{");
ToHTML("cs = '';");
ToHTML("}");
ToHTML("n--;");
ToHTML("var arr = str.split(new RegExp(EscapeRegExp(delim),cs));");
ToHTML("if (n>(arr.length-1))");
ToHTML("{");
ToHTML("return '';");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("return arr[n];");
ToHTML("}");
ToHTML("}");
ToHTML("function SortNumberArray(x,y) {return x-y;}");
ToHTML("function bitTest(bit){num=this;return ((num>>bit)%2!=0);}");
ToHTML("function bitSet(bit){num=this;return num|1<<bit;}");
ToHTML("function bitClear(bit){num=this;return num &~(1<<bit);}");
ToHTML("function bitToggle(bit){num=this;return num.bitTest(bit)?num.bitClear(bit):num.bitSet(bit);}");
ToHTML("Number.prototype.bitTest = bitTest;");
ToHTML("Number.prototype.bitSet = bitSet;");
ToHTML("Number.prototype.bitClear = bitClear;");
ToHTML("Number.prototype.bitToggle = bitToggle;");
ToHTML("String.prototype.bitTest = bitTest;");
ToHTML("String.prototype.bitSet = bitSet;");
ToHTML("String.prototype.bitClear = bitClear;");
ToHTML("String.prototype.bitToggle = bitToggle;");
ToHTML("function CopyArray(myarr) {return [].concat(myarr);}");
ToHTML("function CopyObject(myobj)");
ToHTML("{");
ToHTML("if (isArray(myobj)) { return CopyArray(myobj); }");
ToHTML("else { return myobj; }");
ToHTML("}");
ToHTML("var Location = [];");
ToHTML("var PrevLocation;");
ToHTML("var CurLocation;");
ToHTML("var PrevImage = pictureDefault;");
ToHTML("var PrevFilter = '';");
ToHTML("var PrevTitle;");
ToHTML("var PrevMenu;");
ToHTML("var PrevAudio;");
ToHTML("var PrevSprites = '';");
ToHTML("function getAsmSys_time()");
ToHTML("{");
ToHTML("var d = new Date();");
ToHTML("return Math.round(d.getTime()/1000);");
ToHTML("}");
ToHTML("function getAsmSys_titleCur()");
ToHTML("{");
ToHTML("return CurLocation.Title;");
ToHTML("}");
ToHTML("function getAsmSys_titlePrev()");
ToHTML("{");
ToHTML("return PrevLocation.Title;");
ToHTML("}");
ToHTML("var asmSys_gender = 0;");
ToHTML("var asmSys_choice = 0;");
if (TestMode)
{
ToHTML("function toConsole()");
ToHTML("{");
ToHTML("consoleDiv.innerHTML = '<div><b>'+CurLocation.Title+'</b></div>';");
ToHTML("for (var i=0; i<=(Location.length-1); i++)");
ToHTML("{");
ToHTML("if (window['eventNum'+i]!==undefined)");
ToHTML("{consoleDiv.innerHTML +='<i>'+Location[i].Title+'</i>;[[asmBSlash]]t';}");
ToHTML("}");
ToHTML("consoleDiv.innerHTML += '<br>';");
for (var i=0; i<Vars.length; i++)
{
ToHTML("if(asm_var"+i+"!==undefined)");
ToHTML("{");
ToHTML("var prnvar = '"+Vars[i]+"='+asm_var"+i+";");
ToHTML("prnvar = prnvar.replace(/<[^<>]+>/g,'');");
ToHTML("if(asm_var"+i+"!=asm_cur"+i+")");
ToHTML("{");
ToHTML("prnvar = '<span style=[[asmBSlash]][[asmQuot]]color:#CC0033;[[asmBSlash]][[asmQuot]]>'+prnvar+'</span>';");
ToHTML("}");
ToHTML("if(isArray(asm_var"+i+"))");
ToHTML("{");
ToHTML("prnvar = '<u>'+prnvar+'</u>';");
ToHTML("}");
ToHTML("consoleDiv.innerHTML += prnvar+';[[asmBSlash]]t';");
ToHTML("}");
}
ToHTML("}");
ToHTML("var consoleShow = false;");
ToHTML("function showConsole()");
ToHTML("{");
ToHTML("consoleDiv.style.backgroundColor = '#EEEEEE';");
ToHTML("if(consoleShow)");
ToHTML("{");
ToHTML("consoleShow = false;");
ToHTML("consoleDiv.style.overflow = 'hidden';");
ToHTML("consoleDiv.style.height = '0';");
ToHTML("consoleDiv.style.marginTop = '0';");
ToHTML("consoleDiv.style.padding = '0';");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("consoleShow = true;");
ToHTML("consoleDiv.style.overflow = 'auto';");
ToHTML("consoleDiv.style.height = '100px';");
ToHTML("consoleDiv.style.marginTop = '36px';");
ToHTML("consoleDiv.style.padding = '5px';");
ToHTML("}");
ToHTML("}");
}
ToHTML("function GetLocation(Title)");
ToHTML("{");
ToHTML("for (var key in Location)");
ToHTML("{");
ToHTML("Loc = Location[key];");
ToHTML("if (Trim(Loc.Title.toLowerCase())==Trim(Title.toLowerCase()))");
ToHTML("{");
ToHTML("return Loc;");
ToHTML("}");
ToHTML("}");
ToHTML("return false;");
ToHTML("}");
ToHTML("function GetIndexLocation(Title)");
ToHTML("{");
ToHTML("if (!Title) {return -1;}");
ToHTML("Title = Title.toString();");
ToHTML("var LocIndex = 0;");
ToHTML("for (var key in Location)");
ToHTML("{");
ToHTML("Loc = Location[key];");
ToHTML("if (Trim(Loc.Title.toLowerCase())==Trim(Title.toLowerCase()))");
ToHTML("{");
ToHTML("return LocIndex;");
ToHTML("}");
ToHTML("LocIndex++;");
ToHTML("}");
ToHTML("return -1;");
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("function FlipPage()");
ToHTML("{");
if (style_view=='vis') {
ToHTML("var buttonsList = $('buttonsList');");
ToHTML("if (buttonsList) {pageDiv.removeChild(buttonsList);}");
ToHTML("buttonsList = document.createElement('div');");
ToHTML("buttonsList.id = 'buttonsList';");
ToHTML("pageDiv.appendChild(buttonsList);");
ToHTML("var buttons = [].slice.call(printDiv.querySelectorAll('.button, .buttonno, .buttonback'));");
ToHTML("for (var i=0; i<buttons.length; i++) {");
ToHTML("buttonsList.appendChild(buttons[i]);");
ToHTML("}");
ToHTML("togglePrint(true);");
}
ToHTML("if (isAnimation && animation_enable && TimeToFade>0) {");
if (style_view!='vis') {
ToHTML("Animate(printDiv,'effect_flip');");
} else {
ToHTML("buttonsList.style.display = 'none';");
ToHTML("Animate(printContDiv,'effect_flip');");
ToHTML("printContDiv.addEventListener('click',function() {");
ToHTML("typer.stop();");
ToHTML("});");
ToHTML("typer.start();");
}
ToHTML("}");
ToHTML("else {fade(printDiv);}");
ToHTML("}");
}
ToHTML("function ToText(s,fromstart,div)");
ToHTML("{");
ToHTML("if (!div) {");
ToHTML("div = printDiv;");
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("if (fromstart) {");
ToHTML("div.innerHTML = s;");
ToHTML("} else {");
ToHTML("div.innerHTML += s;");
ToHTML("}");
ToHTML("var innr = div.innerHTML;");
ToHTML("if (innr.substr(0,3)=='<p>') {");
ToHTML("div.innerHTML = innr.replace(/^(<p><[[asmBSlash]]/p>)+/i,'');");
ToHTML("}");
}
else
{
ToHTML("if (fromstart) { speechList = []; speechCommandsList = []; }");
ToHTML("var myRe = /<span.+?onclick='(.+?)'>(.+?)<[[asmBSlash]]/span>/;");
ToHTML("if ((myMatch = myRe.exec(s)) != null) {");
ToHTML("var myMatch = myMatch;");
ToHTML("var strings = speechSplit(myMatch[2].trim().toLowerCase());");
ToHTML("var hashes = [];");
ToHTML("for (var key in strings) {");
ToHTML("var hash = speechHash(strings[key]);");
ToHTML("if (hash) {");
ToHTML("hashes.push(hash);");
ToHTML("}");
ToHTML("}");
ToHTML("speechCommandsList.push([ hashes,myMatch[1] ]);");
ToHTML("return;");
ToHTML("}");
ToHTML("var strings = speechSplit(s);");
ToHTML("for (var key in strings) {");
ToHTML("var hash = speechHash(strings[key]);");
ToHTML("if (hash) {");
ToHTML("speechList.push(hash);");
ToHTML("}");
ToHTML("}");
}
ToHTML("}");
ToHTML("function PrintLocation(Title,replay)");
ToHTML("{");
ToHTML("if (!Title) {return false;}");
if (TargetFormat=='audio')
{
ToHTML("Animate(speechActionButtons,'effect_scale');");
}
ToHTML("Title = Title.toString();");
ToHTML("if (Title.search(/:[[asmBSlash]]/[[asmBSlash]]//)!=-1)");
ToHTML("{");
ToHTML("window.open(Title);");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("var Loc = GetLocation(Title);");
ToHTML("if (Loc!=false)");
ToHTML("{");
ToHTML("closeWinds();");
ToHTML("if (!replay) {");
ToHTML("for (var i=0; i<"+Vars.length+"; i++)");
ToHTML("{");
ToHTML("window['asm_prev'+i]=CopyObject(window['asm_cur'+i]);");
ToHTML("window['asm_cur'+i]=CopyObject(window['asm_var'+i]);");
ToHTML("}");
ToHTML("PrevLocation = CurLocation;");
ToHTML("CurLocation = Loc;");
if (TargetFormat=='html')
{
ToHTML("$('BackButton').style.opacity = 1;");
ToHTML("PrevImage = printImage.src;");
ToHTML("PrevFilter = printImage.style.filter;");
ToHTML("PrevTitle = printTitle.innerHTML;");
ToHTML("PrevMenu = menuDiv.innerHTML;");
ToHTML("PrevAudio = musicAudio.src;");
ToHTML("PrevSprites = spritesDiv.innerHTML;");
ToHTML("}");
ToHTML("showPrintAdd(false);");
ToHTML("scrollDiv();");
}
else
{
ToHTML("PrevImage = document.body.style.backgroundImage;");
ToHTML("PrevAudio = musicAudio.src;");
ToHTML("}");
}
ToHTML("if (Loc.Parsed==1)");
ToHTML("{");
ToHTML("ToText('',true);");
ToHTML("eval(Loc.Text);");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("ToText(Loc.Text,true);");
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("FlipPage();");
}
if (TestMode)
{
ToHTML("toConsole();");
}
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("LocNotFound(Title);");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function DisplayLocation(Title)");
ToHTML("{");
ToHTML("if (!Title) {return false;}");
ToHTML("Title = Title.toString();");
ToHTML("if (CurLocation.Title.toLowerCase()==Title.toLowerCase() && printDiv!=printAddText) {return;}");
ToHTML("if (Title.search(/:[[asmBSlash]]/[[asmBSlash]]//)!=-1)");
ToHTML("{}");
ToHTML("else");
ToHTML("{");
ToHTML("var Loc = GetLocation(Title);");
ToHTML("if (Loc!=false)");
ToHTML("{");
ToHTML("if (Loc.Parsed==1)");
ToHTML("{");
ToHTML("eval(Loc.Text);");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("ToText(Loc.Text);");
ToHTML("}");
if (TestMode)
{
ToHTML("toConsole();");
}
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("LocNotFound(Title);");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function NoBack()");
ToHTML("{");
ToHTML("PrevLocation = '';");
if (TargetFormat=='html')
{
ToHTML("PrevImage = pictureDefault;");
ToHTML("PrevFilter = '';");
ToHTML("PrevSprites = '';");
ToHTML("PrevTitle = '';");
ToHTML("PrevMenu = '';");
ToHTML("PrevAudio = '';");
ToHTML("$('BackButton').style.opacity = 0.4;");
}
else
{
ToHTML("PrevImage = 'none';");
}
ToHTML("}");
ToHTML("function Back(Undo)");
ToHTML("{");
ToHTML("if (PrevLocation)");
ToHTML("{");
ToHTML("if (Undo)");
ToHTML("{");
ToHTML("for (var i=0; i<"+Vars.length+"; i++)");
ToHTML("{");
ToHTML("window['asm_var'+i]=CopyObject(window['asm_prev'+i]);");
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("printImage.style.filter = PrevFilter;");
ToHTML("printImage.src = PrevImage;");
ToHTML("spritesDiv.innerHTML = PrevSprites;");
ToHTML("printTitle.innerHTML = PrevTitle;");
ToHTML("menuDiv.innerHTML = PrevMenu;");
}
else
{
ToHTML("document.body.style.backgroundImage = PrevImage;");
}
ToHTML("if (musicAudio.src!=PrevAudio) {musicAudio.src = PrevAudio; playMusic();};");
ToHTML("}");
ToHTML("PrintLocation(PrevLocation.Title);");
ToHTML("NoBack();");
if (TargetFormat=='audio')
{
ToHTML("speechPassage(true);");
}
ToHTML("}");
ToHTML("}");
ToHTML("function audioPlayEvent() {");
ToHTML("if (audio_enable && (musicAudio.src.substr(-4)=='.mp3' || musicAudio.src.substr(0,15)=='data:audio/mp3;')) {");
ToHTML("audioButton.style.display = 'inline-block';");
ToHTML("audioButton.style.opacity = 1;");
ToHTML("} else {");
ToHTML("audioButton.style.display = 'none';");
ToHTML("}");
ToHTML("}");
ToHTML("function audioPauseEvent() {");
ToHTML("audioButton.style.opacity = 0.5;");
ToHTML("}");
ToHTML("function PlayPauseButton() {");
ToHTML("if (musicAudio.paused) {");
ToHTML("musicAudio.play();");
ToHTML("} else {");
ToHTML("musicAudio.pause();");
ToHTML("}");
ToHTML("}");
ToHTML("function playMusic() {");
ToHTML("if (audio_enable) {");
ToHTML("musicAudio.play();");
ToHTML("}");
ToHTML("}");
ToHTML("function stopMusic() {");
ToHTML("audioButton.style.display = 'none';");
ToHTML("if (musicAudio.src) {");
ToHTML("musicAudio.pause();");
ToHTML("musicAudio.currentTime = 0;");
ToHTML("}");
ToHTML("}");
ToHTML("var Events = [];");
ToHTML("function startEvent(locTitle,timerSec)");
ToHTML("{");
ToHTML("var locIndex = GetIndexLocation(locTitle);");
ToHTML("if (locIndex!=-1)");
ToHTML("{");
ToHTML("stopEvent(locTitle);");
ToHTML("if (window['eventNum'+locIndex]===undefined)");
ToHTML("{");
ToHTML("Events.push(GetIndexLocation(locTitle)+':'+timerSec);");
ToHTML("window['eventNum'+locIndex] = setInterval (function() { DisplayLocation(locTitle); },timerSec*1000);");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function stopEvent(locTitle)");
ToHTML("{");
ToHTML("var locIndex = GetIndexLocation(locTitle);");
ToHTML("if (locIndex!=-1)");
ToHTML("{");
ToHTML("clearInterval(window['eventNum'+locIndex]);");
ToHTML("window['eventNum'+locIndex] = undefined;");
ToHTML("for (var i=0; i<=(Events.length-1); i++)");
ToHTML("{");
ToHTML("var curEvent = Events[i]; var curEventArr = curEvent.split(':'); curEvent = curEventArr[0];");
ToHTML("if (curEvent==GetIndexLocation(locTitle)) { Events.splice(i,1); break; }");
ToHTML("}");
ToHTML("");
ToHTML("}");
ToHTML("}");
ToHTML("function stopAllEvents()");
ToHTML("{");
ToHTML("Events.length = 0;");
ToHTML("for (var i=0; i<=(Location.length-1); i++)");
ToHTML("{");
ToHTML("if (window['eventNum'+i]!==undefined)");
ToHTML("{");
ToHTML("clearInterval(window['eventNum'+i]);");
ToHTML("window['eventNum'+i] = undefined;");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function choice(elem,variable,n)");
ToHTML("{");
ToHTML("if (elem.className=='plink selected') {");
ToHTML("elem.className = 'plink';");
ToHTML("window[variable] = 0;");
ToHTML("} else {");
ToHTML("var elems = elem.parentNode.getElementsByClassName('plink selected');");
ToHTML("for (var i=0; i<elems.length; i++) {");
ToHTML("elems[i].className ='plink';");
ToHTML("}");
ToHTML("elem.className ='plink selected';");
ToHTML("window[variable] = n;");
ToHTML("}");
ToHTML("}");
for (var i=0; i<LocNum.length; i++)
{
var Location = LocationArr[LocNum[i]];
var LocTitle = Location.Title;
if (TargetFormat=='audio' && ForbiddenLocation(LocTitle))
{
continue;
}
var LocText = Location.Text.trim();
if (!SimpleLocation(LocTitle))
{
LocText = RemoveComments(LocText);
LocText = LocText.replace(/(<<\s*?row.+?>>)\s+?(<<\s*?row)/gi,"$1$2");
LocText = LocText.replace(/(<<\s*?row.+?>>)\s+?(<<\s*?row)/gi,"$1$2");
LocText = LocText.replace(/&([0-9a-zA-Z#]+?);/g,"&$1:html_code");
LocText = LocText.replace(/;/g,"&#59;");
LocText = LocText.replace(/&(.+?)\:html_code/g,"&$1;");
var myMatch;
var myRe = /\[\[\s*(file|файл)\:\s*(.+?)\s*\]\]/gi;
while ((myMatch = myRe.exec(LocText)) != null)
{
var s = myMatch[0].replace(/\\/g,"/");
LocText = LocText.replace(myMatch[0],s);
}
LocText = LocText.replace(/\\/g,"&#92;");
LocText = LocText.replace(/<</g,"&ltl;&ltl;");
LocText = LocText.replace(/>>/g,"&gtl;&gtl;");
LocText = LocText.replace(/>/g,"&gt;");
LocText = LocText.replace(/</g,"&lt;");
LocText = LocText.replace(/&ltl;&ltl;/g,"<<");
LocText = LocText.replace(/&gtl;&gtl;/g,">>");
LocText = LocText.replace(/<<\s*?menu\s*?[\"'](.*?)[\"']\s*?>>/gi,"<<menu>>$1<<endmenu>>");
LocText = LocText.replace(/<<menu>>(\[\[.+?\]\])<<endmenu>>/gi,function(match,p1){return '<<menu>>'+p1.replace(/\]\][^\[\]]+?\[\[/g,']][[').replace(/^[^\[]+?\[\[/g,'[[').replace(/\]\][^\]]+?$/g,']]')+'<<endmenu>>';});
LocText = Format1(LocText);
LocText = LocText.replace(/'/g,"&#39;");
LocTitle = LocTitle.replace(/'/g,"&#39;");
LocText = LocText.replace(/"/g,"&quot;");
LocTitle = LocTitle.replace(/"/g,"&quot;");
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*(https?\:\/\/.+?)\.mp3\s*\]\]/gi,"<<audio '$2.mp3'>>");
if (TargetFormat=='html')
{
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*(https?\:\/\/.+?)\.(png|jpg|jpeg|gif)\s*\]\]/gi,"<img class='file' src='$2.$3' border='0' onload='goodImageShow(this)' onerror='badImageShow(this)'>");
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*https?\:\/\/(www\.)?vimeo\.com\/(\d+?)\s*\]\]/gi,"<div class='video'><iframe frameborder='0' src='http://player.vimeo.com/video/$3'></iframe></div>");
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*https?\:\/\/.+?([^\/\=\s]+?)\s*\]\]/gi,"<div class='video'><iframe frameborder='0' src='http://www.youtube.com/embed/$2?rel=0'></iframe></div>");
LocText = LocText.replace(/\[\[\s*?(file|файл)\:\s*?http:\/\/(.+?[a-zA-Z0-9_]+?)\s*?\]\]/gi,"$2");
}
if (!TestMode) {
var myMatch;
if (TargetFormat=='html') {
var myRe = /\[\[\s*(file|файл)\:\s*(.+?)\.(png|jpg|jpeg|gif|mp3)\s*\]\]/gi;
} else {
var myRe = /\[\[\s*(file|файл)\:\s*(.+?)\.(mp3)\s*\]\]/gi;
}
while ((myMatch = myRe.exec(LocText)) != null) {
if (myMatch[2]!='#') {
FilesLinks.push(myMatch[2]+"."+myMatch[3]);
}
}
} else {
if (TargetDesktop) {
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*(.+?)\s*\]\]/gi,"[[file:"+StoryPath+"$2]]");
}
}
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*(.+?)\.(png|jpg|jpeg|gif)\s*\]\]/gi,"<img class='file' src='$2.$3' border='0' onload='goodImageShow(this)' onerror='badImageShow(this)'>");
LocText = LocText.replace(/\[\[\s*(file|файл)\:\s*(.+?)\.(mp3)\s*\]\]/gi,"<<audio '$2.mp3'>>");
LocText = LocText.replace(/\[\[\s*(file|файл)\:.*?\]\]/gi,"");
LocText = LocText.replace(/^\s*?\[\[([^\[]*?(?!\]\])[^\[]*?)\]\]\s*?$/gm,"<<link [[]] $1>>");
LocText = LocText.replace(/\[\[(.*?(?!\]\]).*?)\]\]/g,"<<link $1>>");
LocText = Format2(LocText);
}
LocText = ReplaceReturns(LocText);
var ParsedLocation;
var ParsedLocText = '';
if (SimpleLocation(LocTitle))
{
ParsedLocation = 0;
ParsedLocText = LocText.replace(/"/g,'&quot;');
ParsedLocText = ParsedLocText.replace(/--/g,tire);
ParsedLocText = ParsedLocText.replace(/- /g,tire+' ');
if (TargetFormat=='audio')
{
speechHash(normalText(ParsedLocText),true);
}
}
else
{
ParsedLocation = 1;
var s = '';
s = LocText.replace(/<<\s*?/g,'\r<<');
s = s.replace(/>>/g,'>>\r');
var parts = s.split('\r');
for (var p=0; p<parts.length; p++)
{
var part = parts[p];
if ((part.substr(0,2)=='<<' && part.substr(-2)=='>>'))
{
var macros = parts[p].substr(2,parts[p].length-4).trim();
macros = macros.replace(/\s+/g,' ');
macros = macros.replace(/&#39;/g,"'");
macros = macros.replace(/&quot;/gi,"'");
macros = macros.replace(/"/g,"'");
macros = macros.replace(/\$\$time/gi,"getAsmSys_time()");
macros = macros.replace(/\$\$title/gi,"getAsmSys_titleCur()");
macros = macros.replace(/\$\$from/gi,"getAsmSys_titlePrev()");
macros = macros.replace(/\$\$gender/gi,"asmSys_gender");
macros = macros.replace(/\$\$choice/gi,"asmSys_choice");
for (var lc=Vars.length-1; lc>=0; lc--)
{
macros = ReplaceAll(macros,"$"+Vars[lc],"$asm_var"+lc);
}
macros = macros.replace(/&lt;br&gt;/gi,"<br>");
macros = macros.replace(new RegExp("(\\$[^"+BadVarsSymbs+"]+?)\\.sort\\(\\*\\)",'g'),"$1.sort(SortNumberArray)");
if (macros.substr(0,5).toLowerCase()=="link ")
{
macros = macros.replace(/link/i,'').trim();
var linkclass = 'plink';
if (macros.substr(0,4)=='[[]]')
{
if (macros.indexOf('<img')==-1)
{
linkclass = 'button';
}
macros = macros.replace(/\[\[\]\]/i,'').trim();
}
var m1, m2, fields, linkType;
linkType = Left(macros,1);
if (linkType=='*' || linkType=='+' || linkType=='-')
{
macros = macros.substr(1);
}
else
{
linkType = '';
}
if (TargetFormat=='audio')
{
if (linkType=='+' || linkType=='*')
{
linkType = '';
}
}
fields = CountFields(macros,'|');
if (fields>1)
{
m1 = macros.split('|'); m1.pop(); m1 = m1.join('|').trim();
m2 = NthField(macros,'|',fields).trim();
}
else
{
m1 = macros.trim();
m2 = m1;
m1 = m1.replace(/\{.*?\}/g,'');
}
if (m2.substr(0,1)=='$' || m2.substr(0,10)=='getAsmSys_')
{
if (m2.substr(0,1)=='$')
{
m2 = m2.substr(1);
if (TargetFormat=='audio')
{
m2 = speechRemoveTags(m2);
}
}
}
else
{
m2 = '&quot;'+m2+'&quot;';
}
var evt = "'>";
if (TargetFormat=='html')
{
evt = tz + "event.stopPropagation()'>";
}
if (linkType=='*')
{
macros = "<span><span class='"+linkclass+"' onclick=' printDiv=printAddText"+tz+"ToText(&quot;&quot;,true)"+tz+"DisplayLocation("+m2+")"+tz+"printDiv=document.getElementById(&quot;print&quot;)"+tz+"showPrintAdd(true)"+tz+"scrollDiv(printAdd)"+evt;
}
else if (linkType=='+')
{
macros = "<span><span class='"+linkclass+"' onclick='this.parentNode.innerHTML=&quot;<span class="+linkclass+"no>&quot;+this.innerHTML+&quot;</span>&quot;"+tz+"scrollDiv(printContDiv,true)"+tz+"ToText(&quot;"+Newline+"&quot;)"+tz+"DisplayLocation("+m2+")"+tz+"fade(printDiv)"+evt;
}
else if (linkType=='-')
{
macros = "<span><span class='"+linkclass+"' onclick='PrintLocation("+m2+")"+tz+"NoBack()"+evt;
}
else
{
macros = "<span><span class='"+linkclass+"' onclick='PrintLocation("+m2+")"+evt;
}
var mydiv = '';
if (LocTitle.toLowerCase()=="storymenu")
{
mydiv = ',menuDiv';
}
if (m1.substr(0,1)=='$' || m1.substr(0,10)=='getAsmSys_')
{
if (m1.substr(0,1)=='$')
{
m1 = m1.substr(1);
}
macros = "ToText([[asmBSlash]][[asmQuot]]"+macros+"[[asmBSlash]][[asmQuot]]+"+m1+"+'</span></span>',false"+mydiv+");";
}
else
{
macros = "ToText([[asmBSlash]][[asmQuot]]"+macros+m1+"</span></span>[[asmBSlash]][[asmQuot]],false"+mydiv+");";
if (TargetFormat=='audio')
{
speechHash(m1,true);
}
}
macros = macros.replace(/\{(.*?)'(.*?)'(.*?)\}/g,"{$1&quot;$2&quot;$3}");
var myRe = /\{\s*?([^{}]+?)\s*?\}/g;
var myMatch;
macros = macros.replace(/\$/g,'');
while ((myMatch = myRe.exec(macros)) != null)
{
macros = macros.replace(myMatch[0],'');
macros = macros.replace(/ onclick='/g," onclick='"+myMatch[1]+tz);
}
parts[p] = macros;
}
else if (macros.substr(0,6).toLowerCase()=="audio ")
{
macros = macros.replace(/audio/i,'').trim();
parts[p] = "musicAudio.src="+macros+";playMusic();";
}
else if (macros.substr(0,7).toLowerCase()=="noaudio")
{
parts[p] = "stopMusic();";
}
else if (macros.substr(0,8).toLowerCase()=="display ")
{
macros = macros.replace(/display/i,'').trim();
if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
}
if (macros.substr(-1,1)=="'")
{
macros = macros.substr(0,macros.length-1);
}
macros = macros.trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace("$","");
macros = macros.replace("[$","[");
if (TargetFormat=='audio')
{
macros = speechRemoveTags(macros);
}
parts[p] = "DisplayLocation("+macros+");";
}
else
{
parts[p] = "DisplayLocation('"+macros+"');";
}
}
else if (macros.substr(0,5).toLowerCase()=="goto ")
{
macros = macros.replace(/goto/i,'').trim();
if (Left(macros,1)=="'")
{
macros = Right(macros,macros.length-1);
}
if (Right(macros,1)=="'")
{
macros = Left(macros,macros.length-1);
}
macros = macros.trim();
if (macros.substr(0,1)=='$' || macros.substr(0,10)=='getAsmSys_')
{
macros = macros.replace('$','');
macros = macros.replace('[$','[');
if (TargetFormat=='audio')
{
macros = speechRemoveTags(macros);
}
parts[p] = "PrintLocation("+macros+");fade(printDiv);NoBack();";
}
else
{
parts[p] = "PrintLocation('"+macros+"');fade(printDiv);NoBack();";
}
parts[p] += "throw new Error('GOTO macro exception.');";
}
else if (macros.substr(0,4).toLowerCase()=="set ")
{
macros = macros.replace(/set/i,'').trim();
macros = macros.replace(/\$/g,'');
if (TargetFormat=='audio')
{
macros = speechStringsAdd(macros);
}
parts[p] = macros+";";
}
else if (macros.substr(0,6).toLowerCase()=="print ")
{
macros = macros.replace('print','').trim();
macros = macros.replace(/\$/g,'');
if (TargetFormat=='audio')
{
isPrint = true;
macros = speechStringsAdd(macros);
}
parts[p] = "ToText("+macros+"+'');";
}
else if (macros.substr(0,2).toLowerCase()=="if" || macros.substr(0,6).toLowerCase()=="elseif")
{
macros = ReplaceAll(macros,"$","");
macros = ReplaceAll(macros,"&lt;","<");
macros = ReplaceAll(macros,"&gt;",">");
macros = ReplaceAll(macros,"<>","!=");
macros = ReplaceAll(macros,"=>",">=");
macros = ReplaceAll(macros," eq ","==");
macros = ReplaceAll(macros," neq ","!=");
macros = ReplaceAll(macros," lt ","<");
macros = ReplaceAll(macros," gt ",">");
macros = ReplaceAll(macros," lte ","<=");
macros = ReplaceAll(macros," gte ",">=");
macros = ReplaceAll(macros," and ","&&");
macros = ReplaceAll(macros," or ","||");
macros = ReplaceAll(macros,"(not ","(!");
macros = ReplaceAll(macros," not "," !");
if (Left(macros,2).toLowerCase()=="if")
{
macros = macros.replace(/if/i,'');
if (TargetFormat=='audio')
{
macros = speechRemoveTags(macros);
}
parts[p] = "if("+macros.trim()+"){";
}
else
{
macros = macros.replace(/elseif/i,'');
if (TargetFormat=='audio')
{
macros = speechRemoveTags(macros);
}
parts[p] = "}else if("+macros.trim()+"){";
}
}
else if (macros.toLowerCase()=="else")
{
parts[p] = "}else{";
}
else if (macros.toLowerCase()=="endif")
{
parts[p] = "}";
}
else if (macros.substr(0,5).toLowerCase()=="loop ")
{
macros = macros.replace(/loop/i,'').trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace(/\$/,'').trim();
}
var s = "loop_asm"+loop_n;
parts[p] = "for(var "+s+"=1;"+s+"<="+macros+";"+s+"++){";
loop_n++;
}
else if (macros.toLowerCase()=="endloop")
{
parts[p] = "}";
}
else if (macros.toLowerCase()=="break")
{
parts[p] = "break;";
}
else if (macros.toLowerCase()=="continue")
{
parts[p] = "continue;";
}
else if (macros.toLowerCase()=="silently" || macros.toLowerCase()=="endsilently")
{
parts[p] = '';
}
else if (macros.substr(0,7).toLowerCase()=="random ")
{
macros = macros.replace(/random/i,'').trim();
macros = macros.replace(/\$/g,'');
parts[p] = NthField(macros,"=",1).trim()+"=Math.round(Math.random()*"+NthField(macros,"=",2).trim()+");";
}
else if (macros.substr(0,6).toLowerCase()=="style ")
{
parts[p] = '';
}
else if (macros.substr(0,5).toLowerCase()=="class")
{
macros = macros.replace(/class/i,'').trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace(/\$/,'').trim();
parts[p] = "ToText([[asmBSlash]][[asmQuot]]<span class='[[asmBSlash]][[asmQuot]]+"+macros+"+[[asmBSlash]][[asmQuot]]'>[[asmBSlash]][[asmQuot]]);";
}
else
{
macros = macros.replace(/'/g,'');
parts[p] = "ToText([[asmBSlash]][[asmQuot]]<span class='"+macros+"'>[[asmBSlash]][[asmQuot]]);";
}
}
else if (macros.substr(0,8).toLowerCase()=="endclass")
{
macros = macros.replace(/endclass/i,'').trim();
parts[p] = "ToText('</span>');";
}
else if (macros.substr(0,6).toLowerCase()=="clrscr")
{
parts[p] = "ToText('',true);showPrintAdd(false);";
}
else if (macros.substr(0,7).toLowerCase()=="restart")
{
macros = macros.replace(/restart/i,'').trim();
macros = macros.replace(/'/g,'').trim();
if (macros=='')
{
macros = Lang_HTMLRestart;
}
if (TargetFormat=='audio')
{
speechHash(macros,true);
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]<span class='plink' onclick='NewGame();'>"+macros+"</span>[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,4).toLowerCase()=="fade")
{
macros = macros.replace(/fade/i,"").trim();
macros = macros.replace(/=/g,"").trim();
macros = parseInt(macros,10);
if (macros<0)
{
macros = '0';
}
else if (macros>0 && macros<100)
{
macros = '100';
}
else if (macros>5000)
{
macros = '5000';
}
parts[p] = "TimeToFade="+macros+";";
}
else if (macros.substr(0,4).toLowerCase()=="row ")
{
macros = macros.replace(/row/i,'').trim();
macros = macros.replace(/ \$/g," '$");
var NoBorder;
var m1 = '';
var m2 = '';
if (Right(macros,8).toLowerCase()=='noborder')
{
NoBorder = true;
macros = Left(macros,macros.length-8);
m2 = 'trow_nb';
}
else
{
NoBorder = false;
m2 = 'trow';
}
s = '';
var brd = '';
var bld = '';
var fls = CountFields(macros," '");
if (fls>2)
{
fls = 2;
}
if (fls==1)
{
bld = " style='font-weight:bold'";
}
for (rowN=1; rowN<=fls; rowN++)
{
m1 = NthField(macros," '",rowN).trim();
if (Left(m1,1)=="$")
{
m1 = m1.replace('$','');
m1 = "[[asmBSlash]][[asmQuot]]+"+m1+"+[[asmBSlash]][[asmQuot]]";
}
else if (Left(m1,1)!="$" && Left(m1,1)!="'")
{
m1 = "'" + m1;
}
if (m1.trim()=="'")
{
m1 = "''";
}
if ((rowN%2)==0 && NoBorder==false)
{
brd = " class='rcol'";
}
else
{
brd = '';
}
m1 = m1.trim();
if (Left(m1,1)=="'" && Right(m1,1)=="'")
{
m1 = Mid(m1,2,m1.length-2);
}
s = s + "<td"+bld+" width='"+Math.round(100/fls)+"%'"+brd+" align='center' valign='top'>"+m1+"</td>";
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]<table class='"+m2+"' width='100%' cellspacing='0' cellpadding='6'><tr>"+s+"</tr></table>[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,6).toLowerCase()=="repeat")
{
macros = macros.replace(/repeat/i,'');
macros = macros.replace(/'/,'');
var m1 = NthField(macros,"'",1).trim();
var m2 = NthField(macros,"'",2).trim();
if (parseFloat(m2)<0.5)
{
m2 = "0.5";
}
parts[p] = "startEvent('"+m1+"',"+m2+");";
}
else if (macros.substr(0,4).toLowerCase()=="stop")
{
macros = macros.replace(/stop/i,'');
macros = macros.replace(/'/g,'');
parts[p] = "stopEvent('"+macros+"');";
}
else if (macros.substr(0,5).toLowerCase()=="input")
{
macros = macros.replace(/input/i,'');
macros = macros.replace(/\$/g,'');
var m1 = NthField(macros,"'",2).trim();
var m2 = NthField(macros,"'",3).trim();
macros = macros.replace(/'/g,'');
parts[p] = "var value="+m2+";if(!value && value!=0){value='';}else{value=value.toString();};var focus='';if(!isTouchDevice){focus='autofocus';};ToText([[asmBSlash]][[asmQuot]]<div class='myinput'><form action='' onsubmit='if(Trim(this.childNodes[0].value)!=&quot;&quot;){"+m2+"=Trim(this.childNodes[0].value);if(!isNaN("+m2+")){"+m2+"=parseFloat("+m2+");} PrintLocation(&quot;"+m1+"&quot;);} return false;'><input class='input_text' type='text' onclick='event.stopPropagation();' autocapitalize='off' size='10' value='[[asmBSlash]][[asmQuot]]+value+[[asmBSlash]][[asmQuot]]'[[asmBSlash]][[asmQuot]]+focus+[[asmBSlash]][[asmQuot]]><input class='input_butt' type='submit' value='✓'></form></div>[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,7).toLowerCase()=="picture")
{
macros = macros.replace(/picture.+?src='/i,'').trim();
macros = macros.replace(/' border=.+?$/i,'').trim();
if (TargetFormat=='html')
{
if (macros.substr(0,7)=="picture")
{
parts[p] = "printImage.src=pictureDefault;printImage.onload=function(){printImage.src=pictureDefault;this.onload=null;};";
}
else
{
parts[p] = "printImage.src=pictureDefault;printImage.onload=function(){printImage.src='"+macros+"';this.onload=null;};";
}
parts[p] += "showImageForce();";
}
else
{
if (macros.substr(0,7)=="picture")
{
parts[p] = "document.body.style.backgroundImage=[[asmQuot]]url('[[asmQuot]]+pictureDefault+[[asmQuot]]')[[asmQuot]];";
}
else
{
parts[p] = "document.body.style.backgroundImage=[[asmBSlash]][[asmQuot]]url('"+macros+"')[[asmBSlash]][[asmQuot]];";
}
}
}
else if (macros.toLowerCase()=="menu")
{
parts[p] = "menuDiv.innerHTML='';printDiv=menuDiv;";
}
else if (macros.toLowerCase()=="endmenu")
{
parts[p] = "printDiv=$('print');";
}
else if (macros.substr(0,5).toLowerCase()=="title")
{
macros = macros.replace(/title/i,'').trim();
if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
}
if (macros.substr(-1,1)=="'")
{
macros = macros.substr(0,macros.length-1);
}
macros = macros.trim();
if (macros.substr(0,1)=='$' || macros.substr(0,10)=='getAsmSys_')
{
macros = macros.replace("$","");
macros = macros.replace("[$","[");
parts[p] = "printTitle.innerHTML="+macros+";";
}
else
{
parts[p] = "printTitle.innerHTML=[[asmBSlash]][[asmQuot]]"+macros+"[[asmBSlash]][[asmQuot]];";
}
}
else if (macros.substr(0,5).toLowerCase()=="sound")
{
macros = macros.replace(/sound/i,'').trim();
if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
}
if (macros.substr(-1,1)=="'")
{
macros = macros.substr(0,macros.length-1);
}
macros = macros.trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace("$","");
macros = macros.replace("[$","[");
}
else
{
macros = "'"+macros+"'";
}
parts[p] = "if(audio_enable){var sound=GetFileData("+macros+");if(sound){sound.play();}};";
}
else if (macros.substr(0,5).toLowerCase()=="image")
{
macros = macros.replace(/image/i,'').trim();
if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
}
if (macros.substr(-1,1)=="'")
{
macros = macros.substr(0,macros.length-1);
}
macros = macros.trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace("$","");
macros = macros.replace("[$","[");
}
else
{
macros = "'"+macros+"'";
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]<img class='file' border='0' onload='goodImageShow(this)' onerror='badImageShow(this)' src=[[asmBSlash]][[asmQuot]]+GetFileData("+macros+").src+[[asmBSlash]][[asmQuot]]>[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,6).toLowerCase()=="sprite")
{
macros = macros.replace(/sprite/i,'').trim();
var params = '';
if (macros.substr(0,18)=="'<img class='file'")
{
macros = macros.match(/src=('.*?')/)[1];
}
else if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
macros = macros.split(/'\s*?,/,2);
if (macros[1]!==undefined)
{
params = macros[1];
macros = macros[0].trim();
macros = "'"+macros+"'";
}
else
{
macros = macros[0].trim();
macros = "'"+macros;
}
}
else if (macros.substr(0,1)=='$')
{
var n  = macros.indexOf(',');
if (n!=-1)
{
params = macros.substr(n+1);
macros = macros.substr(0,n);
}
macros = macros.replace("$","");
macros = macros.replace("[$","[").trim();
}
params = params.trim();
if (params!='')
{
params = params.split(',',7);
for (var key in params)
{
var param = params[key].trim();
if (param=='' || param=='@') {param = 'undefined';}
if (param.substr(0,1)=='$')
{
param = param.replace("$","");
param = param.replace("[$","[").trim();
}
params[key] = param;
}
params = ','+params.join(',');
}
parts[p] = "drawSprite("+macros+params+");";
}
else if (macros.substr(0,6).toLowerCase()=="delete")
{
macros = macros.replace(/delete/i,'').trim();
if (macros=="")
{
parts[p] = "clearSprites();";
}
else
{
if (macros.substr(0,18)=="'<img class='file'")
{
macros = macros.match(/src=('.*?')/)[1];
}
else if (macros.substr(0,1)=="'")
{
macros = macros.substr(1);
macros = macros.split(/'\s*?,/,2);
if (macros[1]!==undefined)
{
params = macros[1];
macros = macros[0].trim();
macros = "'"+macros+"'";
}
else
{
macros = macros[0].trim();
macros = "'"+macros;
}
}
else if (macros.substr(0,1)=='$')
{
var n  = macros.indexOf(',');
if (n!=-1)
{
params = macros.substr(n+1);
macros = macros.substr(0,n);
}
macros = macros.replace("$","");
macros = macros.replace("[$","[").trim();
}
parts[p] = "clearSprite("+macros+");";
}
}
else if (macros.substr(0,3).toLowerCase()=="tab")
{
macros = macros.replace(/tab/i,'').trim();
if (macros.substr(0,1)=='$')
{
macros = macros.replace(/\$/,'').trim();
}
if (macros=='' || macros=='4')
{
parts[p] = "ToText('&nbsp;&nbsp;&nbsp;&nbsp;');";
}
else
{
var s = "loop_asm"+loop_n;
parts[p] = "for(var "+s+"=1;"+s+"<="+macros+";"+s+"++){ToText('&nbsp;');}";
loop_n++;
}
}
else if (macros.substr(0,4).toLowerCase()=="back")
{
var mymatch = macros.match(/'(.*?)'/);
if (mymatch && mymatch[1].trim()!='')
{
macros = mymatch[1].trim();
}
else
{
macros = Lang_Back;
}
if (TargetFormat=='audio')
{
speechHash(macros,true);
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]"+Newline+"<span class='buttonback' onclick='Back(true)'>"+macros+"</span>"+Newline+"[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,6).toLowerCase()=="return")
{
var mymatch = macros.match(/'(.*?)'/);
if (mymatch && mymatch[1].trim()!='')
{
macros = mymatch[1].trim();
}
else
{
macros = Lang_Return;
}
if (TargetFormat=='audio')
{
speechHash(macros,true);
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]"+Newline+"<span class='buttonback' onclick='Back(false)'>"+macros+"</span>"+Newline+"[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,6).toLowerCase()=="choice")
{
macros = macros.replace(/choice/i,'').trim();
macros = macros.split("'");
var myVar = 'asmSys_choice';
if (macros[2]) {
myVar = macros[2].trim().substr(1);
}
macros = macros[1].replace(/'/g,'').trim();
var opts = macros.split('&#59;');
parts[p] = myVar+"=0;ToText([[asmBSlash]][[asmQuot]]<div class='choice'>[[asmBSlash]][[asmQuot]]);";
var n = 1;
for (var opt in opts) {
var text = opts[opt].trim();
if (text) {
parts[p] += "ToText([[asmBSlash]][[asmQuot]]<div class='plink' onclick=choice(this,'"+myVar+"',"+n+")>"+text+"</div>[[asmBSlash]][[asmQuot]]);";
n++;
}
}
parts[p] += "ToText([[asmBSlash]][[asmQuot]]</div>[[asmBSlash]][[asmQuot]]);";
}
else if (macros.substr(0,6).toLowerCase()=="filter")
{
macros = macros.replace(/filter/i,'').trim();
macros = macros.replace(/\s+/g,' ');
macros = macros.replace(/^'/,''); macros = macros.replace(/'$/,'').trim();
if (macros) {
var vals = macros.split(' ');
macros = '';
for (var val in vals) {
if (vals[val]) {
macros += vals[val] + ' ';
}
}
parts[p] = "printImage.style.filter='"+macros+"';";
} else {
parts[p] = "printImage.style.filter='';";
}
}
else
{
parts[p] = '';
}
if (parts[p].substr(-1)!=';')
{
parts[p] = parts[p]+';';
}
}
else
{
if (parts[p]!='')
{
parts[p] = parts[p].replace(/- /g,tire+" ");
if (TargetFormat=='audio')
{
speechHash(normalText(parts[p]),true);
}
parts[p] = "ToText([[asmBSlash]][[asmQuot]]"+parts[p]+"[[asmBSlash]][[asmQuot]]);";
}
}
}
if (parts.length>0)
{
ParsedLocText = ';'+parts.join('');
}
else
{
ParsedLocText = '';
}
}
if (ParsedLocText==';')
{
ParsedLocText = '';
}
ToHTML("Location.push(new Locations("+i+",[[asmQuot]]"+LocTitle+"[[asmQuot]],[[asmQuot]]"+ParsedLocText+"[[asmQuot]],"+ParsedLocation+"));");
}
for (var i=0; i<Vars.length; i++)
{
ToHTML("var asm_var"+i+";");
ToHTML("var asm_cur"+i+";");
ToHTML("var asm_prev"+i+";");
}
if (TargetFormat=='html')
{
ToHTML("function drawSprite(id,x,y,width,height,opacity,origin,fade) {");
ToHTML("if (id.substr(0,7)!='http://' && id.substr(0,8)!='https://') {");
ToHTML("mysprite = new Image();");
ToHTML("mysprite.src = GetFileData(id).src;");
ToHTML("if (!mysprite) {return;};");
ToHTML("} else {");
ToHTML("var mysprite = new Image();");
ToHTML("mysprite.src = id;");
ToHTML("mysprite.onerror = function() {badImageShow(this);};");
ToHTML("}");
ToHTML("mysprite.onload = function() {");
ToHTML("if (origin===undefined || origin<1) {origin = 1;}");
ToHTML("else if (origin>4) {origin = 4;}");
ToHTML("if (fade===undefined || fade<0) {fade = 0;}");
ToHTML("else if (fade>10000) {fade = 10000;}");
ToHTML("if (width===undefined && height===undefined) {width = 'auto'; height = '100%';}");
ToHTML("else if (width===undefined && height!==undefined) {width = 'auto';}");
ToHTML("else if (height===undefined && width!==undefined) {height = 'auto';}");
ToHTML("if (x===undefined && y===undefined) {x = 0; y = 0; mysprite.style.cssText = 'margin:auto;bottom:0;right:0;display:table;';}");
ToHTML("if (x===undefined && width=='auto') {x = 0; mysprite.style.cssText = 'margin-left:auto;margin-right:auto;right:0;display:table;';}");
ToHTML("else if (x===undefined && width!='auto') {x = (100-width)/2;}");
ToHTML("if (y===undefined && height=='auto') {y = 0; mysprite.style.cssText = 'margin-top:auto;margin-bottom:auto;bottom:0;display:table;';}");
ToHTML("else if (y===undefined && height!='auto') {y = (100-height)/2;}");
ToHTML("var l = 'left'; var t = 'top';");
ToHTML("if (origin==2) {");
ToHTML("l = 'right';");
ToHTML("}");
ToHTML("else if (origin==3) {");
ToHTML("t = 'bottom';");
ToHTML("}");
ToHTML("else if (origin==4) {");
ToHTML("l = 'right';");
ToHTML("t = 'bottom';");
ToHTML("}");
ToHTML("if (x!='auto') {");
ToHTML("mysprite.style[l] = x+'%';");
ToHTML("}");
ToHTML("if (y!='auto') {");
ToHTML("mysprite.style[t] = y+'%';");
ToHTML("}");
ToHTML("if (width!='auto') {");
ToHTML("mysprite.style.width = width+'%';");
ToHTML("}");
ToHTML("if (height!='auto') {");
ToHTML("mysprite.style.height = height+'%';");
ToHTML("}");
ToHTML("if (opacity===undefined || opacity>100) {opacity = 100;}");
ToHTML("else if (opacity<0) {opacity = 0;}");
ToHTML("mysprite.className = 'sprite';");
ToHTML("spritesDiv.appendChild(mysprite);");
ToHTML("mysprite.style.transition = 'opacity '+fade+'ms ease';");
ToHTML("setTimeout(function() {mysprite.style.opacity = opacity/100;},100);");
ToHTML("showImageForce();");
ToHTML("this.onload = null;");
ToHTML("}");
ToHTML("}");
ToHTML("function clearSprite(id) {");
ToHTML("var sprites = spritesDiv.getElementsByTagName('IMG');");
ToHTML("for (var i=0; i<sprites.length; i++) {");
ToHTML("var sprite = sprites[i];");
ToHTML("if (sprite.src==GetFileData(id).src) {");
ToHTML("spritesDiv.removeChild(sprite);");
ToHTML("break;");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("function clearSprites() {");
ToHTML("spritesDiv.innerHTML = '';");
ToHTML("}");
}
ToHTML("var b64chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';");
ToHTML("function base64encode(str) {");
ToHTML("str = unescape(encodeURIComponent(str));");
ToHTML("var b64encoded = '';");
ToHTML("var chr1, chr2, chr3;");
ToHTML("var enc1, enc2, enc3, enc4;");
ToHTML("for (var i=0; i<str.length;) {");
ToHTML("chr1 = str.charCodeAt(i++);");
ToHTML("chr2 = str.charCodeAt(i++);");
ToHTML("chr3 = str.charCodeAt(i++);");
ToHTML("enc1 = chr1 >> 2;");
ToHTML("enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);");
ToHTML("enc3 = isNaN(chr2) ? 64:(((chr2 & 15) << 2) | (chr3 >> 6));");
ToHTML("enc4 = isNaN(chr3) ? 64:(chr3 & 63);");
ToHTML("b64encoded += b64chars.charAt(enc1) + b64chars.charAt(enc2) + b64chars.charAt(enc3) + b64chars.charAt(enc4);");
ToHTML("}");
ToHTML("return b64encoded;");
ToHTML("}");
ToHTML("function base64decode(str) {");
ToHTML("var b64decoded = '';");
ToHTML("var chr1, chr2, chr3;");
ToHTML("var enc1, enc2, enc3, enc4;");
ToHTML("str = str.replace(/[^a-z0-9+/=]/gi, '');");
ToHTML("for (var i=0; i<str.length;) {");
ToHTML("enc1 = b64chars.indexOf(str.charAt(i++));");
ToHTML("enc2 = b64chars.indexOf(str.charAt(i++));");
ToHTML("enc3 = b64chars.indexOf(str.charAt(i++));");
ToHTML("enc4 = b64chars.indexOf(str.charAt(i++));");
ToHTML("chr1 = (enc1 << 2) | (enc2 >> 4);");
ToHTML("chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);");
ToHTML("chr3 = ((enc3 & 3) << 6) | enc4;");
ToHTML("b64decoded = b64decoded + String.fromCharCode(chr1);");
ToHTML("if (enc3 < 64) {");
ToHTML("b64decoded += String.fromCharCode(chr2);");
ToHTML("}");
ToHTML("if (enc4 < 64) {");
ToHTML("b64decoded += String.fromCharCode(chr3);");
ToHTML("}");
ToHTML("}");
ToHTML("return decodeURIComponent(escape(b64decoded));");
ToHTML("}");
ToHTML("function urlencode(str)");
ToHTML("{");
ToHTML("str = (str+'').toString();");
ToHTML("return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/[[asmBSlash]](/g, '%28').replace(/[[asmBSlash]])/g, '%29').replace(/[[asmBSlash]]*/g, '%2A').replace(/%20/g, '+');");
ToHTML("}");
ToHTML("var filename = Trim(window.location.pathname.substring(window.location.pathname.lastIndexOf('/')+1));");
ToHTML("if (!filename) {filename='test';}");
ToHTML("var data_spl = '[[asmBSlash]]t';");
ToHTML("var data_splArr = String.fromCharCode(11);");
ToHTML("function inLibNow()");
ToHTML("{");
ToHTML("if (!navigator.onLine) {return false;}");
ToHTML("var myhost = window.location.hostname;");
ToHTML("if (myhost=='hyperbook.ru' || myhost=='ifiction.net') {");
ToHTML("if (filename!='test') {");
ToHTML("return true;");
ToHTML("}");
ToHTML("else {");
ToHTML("return false;");
ToHTML("}");
ToHTML("}");
ToHTML("else {");
ToHTML("return false;");
ToHTML("}");
ToHTML("}");
ToHTML("function getXmlHttp(){");
ToHTML("var xmlhttp;");
ToHTML("try {");
ToHTML("xmlhttp = new ActiveXObject('Msxml2.XMLHTTP');");
ToHTML("} catch (e) {");
ToHTML("try {");
ToHTML("xmlhttp = new ActiveXObject('Microsoft.XMLHTTP');");
ToHTML("} catch (E) {");
ToHTML("xmlhttp = false;");
ToHTML("}");
ToHTML("}");
ToHTML("if (!xmlhttp && typeof XMLHttpRequest!='undefined') {");
ToHTML("xmlhttp = new XMLHttpRequest();");
ToHTML("}");
ToHTML("return xmlhttp;");
ToHTML("}");
ToHTML("function getServerData(url)");
ToHTML("{");
ToHTML("var xmlhttp = getXmlHttp();");
ToHTML("xmlhttp.open('GET', url, false);");
ToHTML("xmlhttp.send(null);");
ToHTML("return xmlhttp.responseText;");
ToHTML("}");
ToHTML("function putServerData(url,data)");
ToHTML("{");
ToHTML("var xmlhttp = getXmlHttp();");
ToHTML("xmlhttp.open('POST', url, false);");
ToHTML("xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');");
ToHTML("xmlhttp.send(data);");
ToHTML("if(xmlhttp.status == 200)");
ToHTML("{");
ToHTML("return xmlhttp.responseText;");
ToHTML("}");
ToHTML("else");
ToHTML("{");
ToHTML("return '';");
ToHTML("}");
ToHTML("}");
if (TargetFormat=='audio')
{
ToHTML("function FromJavascript(com) {");
ToHTML("com = com.replace(/[\r\n]/g,'[[asmNewLine]]');");
ToHTML("com = com.replace(/\t/g,'[[asmTabSymb]]');");
ToHTML("var bak = document.title;");
ToHTML("document.title = 'ASM_com::'+com;");
ToHTML("document.title = bak;");
ToHTML("}");
}
ToHTML("function SavedList(save,savesList)");
ToHTML("{");
ToHTML("var save_slots = 6;");
if (TestMode && TargetChromeApp) {
ToHTML("if (!savesList) {");
ToHTML("MessageFromFrame('GetSavesList='+filename+'&'+save);");
ToHTML("} else {");
} else {
ToHTML("savesList = [];");
ToHTML("var item;");
if (TargetFormat=='html') {
ToHTML("if (inLibNow()) {");
ToHTML("var url = 'http://'+window.location.hostname+'/command.php?login='+urlencode(getCookie('login'))+'&password='+urlencode(getCookie('password'))+'&com=savedstorylist&value='+filename;");
ToHTML("savesList = getServerData(url).split(',');");
ToHTML("}");
ToHTML("else {");
ToHTML("for (var i=1; i<=save_slots; i++) {");
ToHTML("if (isLocalStorageAvailable()) {");
ToHTML("item = localStorage.getItem(i+'_'+filename);");
ToHTML("}");
ToHTML("else {");
ToHTML("item = getCookie(i+'_'+filename);");
ToHTML("}");
ToHTML("if (item) {savesList.push('1');} else {savesList.push('');}");
ToHTML("}");
ToHTML("}");
} else {
ToHTML("FromJavascript('GetSavesList');");
}
}
ToHTML("var com = '';");
ToHTML("if (save) {com='SaveStory';} else {com='LoadStory';}");
ToHTML("var str = '';");
ToHTML("for (var i=1; i<=save_slots; i++) {");
ToHTML("if (savesList[i-1]) {");
ToHTML("if (save) {var savep='"+Lang_Overwrite+"';} else {var savep='"+Lang_HTMLLoad+"';}");
ToHTML("str += '<div><span class=plink onclick=\"showPrefs(true);'+com+'('+i+');\">'+savep+'</span></div>';");
ToHTML("} else {");
ToHTML("if (save) {");
ToHTML("str += '<div><span class=plink onclick=\"showPrefs(true);'+com+'('+i+');\">"+Lang_HTMLSave+"</span></div>';");
ToHTML("} else {");
ToHTML("str += '<div>"+Lang_Empty+"</div>';");
ToHTML("}");
ToHTML("}");
ToHTML("}");
ToHTML("return str;");
if (TestMode && TargetChromeApp) {
ToHTML("}");
}
ToHTML("}");
ToHTML("function SaveSuccess()");
ToHTML("{");
ToHTML("AlertMessage('"+Lang_StorySaved+"');");
ToHTML("}");
ToHTML("function SaveError()");
ToHTML("{");
ToHTML("AlertMessage('"+Lang_StoryNotSaved+"');");
ToHTML("}");
ToHTML("function SaveStory(slot_num)");
ToHTML("{");
if (TargetFormat=='html')
{
ToHTML("if (slot_num===undefined) { slot_num = 1; };");
ToHTML("var status = '';");
ToHTML("if (inLibNow()) {");
ToHTML("status = SaveStoryLib(SaveStoryData(),slot_num);");
ToHTML("}");
ToHTML("else {");
ToHTML("status = SaveStoryLocal(SaveStoryData(),slot_num);");
ToHTML("}");
if (!(TestMode && TargetChromeApp)) {
ToHTML("if (status!='') {");
ToHTML("SaveSuccess();");
ToHTML("}");
ToHTML("else {");
ToHTML("SaveError();");
ToHTML("}");
}
}
else
{
ToHTML("if (slot_num===undefined) { slot_num = -1; };");
ToHTML("FromJavascript('SaveStory::'+slot_num+'::'+SaveStoryData());");
}
ToHTML("}");
ToHTML("function SaveStoryLib(data,slot_num)");
ToHTML("{");
ToHTML("var url = 'http://'+window.location.hostname+'/command.php?login='+urlencode(getCookie('login'))+'&password='+urlencode(getCookie('password'))+'&com=savestory&value='+filename+'&slot='+slot_num;");
ToHTML("return putServerData(url,SaveStoryData());");
ToHTML("}");
ToHTML("function SaveStoryLocal(data,slot_num)");
ToHTML("{");
if (TestMode && TargetChromeApp) {
ToHTML("MessageFromFrame('SaveStory='+slot_num+'_'+filename+'&'+data);");
} else {
ToHTML("var savedOk = false;");
ToHTML("if (isLocalStorageAvailable()) {");
ToHTML("localStorage.setItem(slot_num+'_'+filename,data);");
ToHTML("if (localStorage.getItem(slot_num+'_'+filename)==data) {savedOk=true;}");
ToHTML("} else {");
ToHTML("setCookie(slot_num+'_'+filename,data);");
ToHTML("if (getCookie(slot_num+'_'+filename)==data) {savedOk=true;}");
ToHTML("}");
ToHTML("if (savedOk) {");
ToHTML("return 'ok';");
ToHTML("}");
ToHTML("else {");
ToHTML("return '';");
ToHTML("}");
}
ToHTML("}");
ToHTML("function LoadStory(slot_num)");
ToHTML("{");
if (TargetFormat=='html')
{
ToHTML("if (slot_num===undefined) { slot_num = 1; };");
ToHTML("var data = '';");
ToHTML("if (inLibNow()) {");
ToHTML("data = LoadStoryLib(slot_num);");
ToHTML("}");
ToHTML("else {");
ToHTML("data = LoadStoryLocal(slot_num);");
ToHTML("}");
if (!(TestMode && TargetChromeApp)) {
ToHTML("if (data) {");
ToHTML("LoadStoryData(data);");
ToHTML("}");
ToHTML("else {");
ToHTML("AlertMessage('"+Lang_StoryNotLoaded+"');");
ToHTML("}");
}
}
else
{
ToHTML("if (slot_num===undefined) { slot_num = -1; };");
ToHTML("FromJavascript('LoadStory::'+slot_num);");
}
ToHTML("}");
ToHTML("function LoadStoryLib(slot_num)");
ToHTML("{");
ToHTML("var url = 'http://'+window.location.hostname+'/command.php?login='+urlencode(getCookie('login'))+'&password='+urlencode(getCookie('password'))+'&com=loadstory&value='+filename+'&slot='+slot_num;");
ToHTML("return getServerData(url);");
ToHTML("}");
ToHTML("function LoadStoryLocal(slot_num)");
ToHTML("{");
if (TestMode && TargetChromeApp) {
ToHTML("MessageFromFrame('LoadStory='+slot_num+'_'+filename);");
} else {
ToHTML("if (isLocalStorageAvailable()) {");
ToHTML("return localStorage.getItem(slot_num+'_'+filename);");
ToHTML("} else {");
ToHTML("return getCookie(slot_num+'_'+filename);");
ToHTML("}");
}
ToHTML("}");
ToHTML("function SaveLoadWind(save,savesList)");
ToHTML("{");
ToHTML("if (!savesList) {");
ToHTML("var inner = SavedList(save);");
ToHTML("} else {");
ToHTML("var inner = SavedList(save,savesList);");
ToHTML("}");
ToHTML("if (inner) {");
ToHTML("prefsDiv.innerHTML = inner;");
ToHTML("prefsDiv.style.display = 'block';");
ToHTML("Animate(prefsDiv,'effect_fade_fast');");
ToHTML("}");
ToHTML("}");
ToHTML("function SaveStoryData()");
ToHTML("{");
ToHTML("var data = CurLocation.Title+data_spl+PrevLocation.Title+data_spl;");
if (TargetFormat=='html')
{
ToHTML("data = data + fontSize.toString() + '[::]';");
ToHTML("data = data + animation_enable + '[::]';");
}
else
{
ToHTML("data = data + '[::]';");
ToHTML("data = data + '[::]';");
}
ToHTML("data = data + audio_enable + '[::]';");
ToHTML("data = data + currentStyles.join(data_splArr) + '[::]';");
ToHTML("data = data + musicAudio.src + '[::]';");
if (TargetFormat=='html')
{
ToHTML("data = data + printImage.src + data_splArr + printImage.style.filter + '[::]';");
ToHTML("data = data + spritesDiv.innerHTML + '[::]';");
ToHTML("data = data + menuDiv.innerHTML + '[::]';");
ToHTML("data = data + printTitle.innerHTML + '[::]';");
}
else
{
ToHTML("data = data + document.body.style.backgroundImage + '[::]';");
ToHTML("data = data + '[::]';");
ToHTML("data = data + '[::]';");
ToHTML("data = data + '[::]';");
}
ToHTML("for (var i=0; i<=(Events.length-1); i++) {");
ToHTML("data = data + Events[i] + ',';");
ToHTML("}");
ToHTML("data = data + '[::]';");
if (TargetFormat=='html')
{
ToHTML("data = data + TimeToFade.toString() + '[::]';");
}
else
{
ToHTML("data = data + '0' + '[::]';");
}
ToHTML("data = data + asmSys_gender + '[::]';");
ToHTML("data = data + asmSys_choice;");
ToHTML("data = data + data_spl;");
ToHTML("for (var i=0; i<"+Vars.length+"; i++) {");
ToHTML("if (isArray(window['asm_cur'+i])) {");
ToHTML("data = data + data_splArr + window['asm_cur'+i].join(data_splArr);");
ToHTML("} else {");
ToHTML("data = data + window['asm_cur'+i];");
ToHTML("}");
ToHTML("if (i<("+(Vars.length-1)+")) {");
ToHTML("data = data + data_spl;");
ToHTML("}");
ToHTML("}");
ToHTML("return base64encode(data);");
ToHTML("}");
ToHTML("function SetVarType(myVar)");
ToHTML("{");
ToHTML("if (myVar=='undefined') { return undefined; }");
ToHTML("if (myVar=='null') { return null; }");
ToHTML("if (myVar=='NaN') { return NaN; }");
ToHTML("if (myVar=='') { return ''; }");
ToHTML("if (myVar.substr(0,1)==data_splArr) { return myVar.substr(1).split(data_splArr); }");
ToHTML("if (!isNaN(+myVar)) { myVar = parseFloat(myVar); }");
ToHTML("if (myVar=='true') { myVar=true; }");
ToHTML("if (myVar=='false') { myVar=false; }");
ToHTML("return myVar;");
ToHTML("}");
ToHTML("function LoadStoryData(data)");
ToHTML("{");
ToHTML("try {");
ToHTML("closeWinds();");
ToHTML("stopAllEvents();");
ToHTML("data = base64decode(data);");
ToHTML("var data_arr = data.split(data_spl);");
ToHTML("CurLocation = GetLocation(data_arr.shift());");
ToHTML("PrevLocation = GetLocation(data_arr.shift());");
ToHTML("var PrevLocationBak = PrevLocation;");
ToHTML("var prefs_arr = data_arr.shift().split('[::]');");
if (TargetFormat=='html')
{
ToHTML("fontSize = parseFloat(prefs_arr[0]);");
ToHTML("printDiv.style.fontSize = fontSize+'em';");
ToHTML("printAdd.style.fontSize = fontSize+'em';");
ToHTML("if (prefs_arr[1]=='true') {animation_enable=true;} else {animation_enable=false;};");
}
ToHTML("if (prefs_arr[2]=='true') {audio_enable=true;} else {audio_enable=false;};");
ToHTML("currentStyles = prefs_arr[3].split(data_splArr);");// массив изменённых стилей
ToHTML("musicAudio.src = prefs_arr[4]; playMusic();");
if (TargetFormat=='html')
{
ToHTML("var imgData = prefs_arr[5].split(data_splArr);");
ToHTML("printImage.src = imgData[0];");
ToHTML("printImage.style.filter = imgData[1];");
ToHTML("menuDiv.innerHTML = prefs_arr[7];");
ToHTML("printTitle.innerHTML = prefs_arr[8];");
}
else
{
ToHTML("document.body.style.backgroundImage = prefs_arr[5];");
}
ToHTML("var eventsArr = prefs_arr[9].split(',');");
if (TargetFormat=='html')
{
ToHTML("TimeToFade = parseFloat(prefs_arr[10]);");
}
ToHTML("asmSys_gender = SetVarType(prefs_arr[11]);");
ToHTML("asmSys_choice = SetVarType(prefs_arr[12]);");
ToHTML("for (var i=0; i<(eventsArr.length-1); i++)");
ToHTML("{");
ToHTML("var eventArr = eventsArr[i].split(':');");
ToHTML("startEvent(Location[eventArr[0]].Title,eventArr[1]);");
ToHTML("}");
ToHTML("for (var i=0; i<="+(Vars.length-1)+"; i++)");
ToHTML("{");
ToHTML("var cur_var = data_arr.shift();");
ToHTML("cur_var = SetVarType(cur_var);");
ToHTML("if (isArray(cur_var))");
ToHTML("{");
ToHTML("for (var j=0; j<cur_var.length; j++) { cur_var[j]=SetVarType(cur_var[j]); }");
ToHTML("}");
ToHTML("window['asm_var'+i] = CopyObject(cur_var);");
ToHTML("window['asm_cur'+i] = CopyObject(cur_var);");
ToHTML("}");
ToHTML("showCover = false;");
ToHTML("PrintLocation(CurLocation.Title);");
if (TargetFormat=='html')
{
ToHTML("spritesDiv.innerHTML = prefs_arr[6];");
}
ToHTML("NoBack();");
if (TargetFormat=='audio')
{
ToHTML("speechPassage(true);");
}
ToHTML("} catch(e) {}");
ToHTML("}");
ToHTML("function NewGame(first)");
ToHTML("{");
ToHTML("currentStyles = [];");
ToHTML("TimeToFade = "+TimeToFade+";");
ToHTML("printImage.style.filter = '';");
ToHTML("asmSys_gender = 0;");
ToHTML("asmSys_choice = 0;");
ToHTML("closeWinds();");
ToHTML("ToText('',true);");
ToHTML("stopMusic();");
if (TargetFormat=='html')
{
ToHTML("MenuInit();");
ToHTML("printAddText.innerHTML = '';");
ToHTML("printTitle.innerHTML = '';");
ToHTML("printImage.src = pictureDefault;");
ToHTML("clearSprites();");
ToHTML("scrollDiv();");
}
else
{
ToHTML("document.body.style.backgroundImage=[[asmQuot]]url('[[asmQuot]]+pictureDefault+[[asmQuot]]')[[asmQuot]];");
}
ToHTML("CurLocation = '';");
ToHTML("NoBack();");
ToHTML("for (var i=0; i<"+Vars.length+"; i++)");
ToHTML("{");
ToHTML("window['asm_var'+i]=undefined;");
ToHTML("window['asm_cur'+i]=undefined;");
ToHTML("window['asm_prev'+i]=undefined;");
ToHTML("}");
ToHTML("stopAllEvents();");
if (OnlyPassageNum==-1)
{
ToHTML("if (GetLocation('Start')==false) {");
ToHTML("PrintLocation('StartPassages');");
ToHTML("} else {");
ToHTML("PrintLocation('Start');");
ToHTML("}");
ToHTML("NoBack();");
}
else
{
ToHTML("PrintLocation('"+LocationArr[OnlyPassageNum].Title+"');");
}
if (OnlyPassageNum==-1)
{
ToHTML("var StoryAuthor = GetLocation('StoryAuthor');");
ToHTML("if (StoryAuthor!=false)");
ToHTML("{");
ToHTML("if (Trim(StoryAuthor.Text)!='')");
ToHTML("{");
ToHTML("StoryAuthor.Text=StoryAuthor.Text.replace(/<[^<>]+?>/g,' ');");
if (TargetFormat=='html')
{
ToHTML("printAddText.innerHTML=[[asmQuot]]<div class='author'>[[asmQuot]]+StoryAuthor.Text+'</div>';");
}
else
{
ToHTML("speechAuthor.innerHTML = StoryAuthor.Text;");
}
ToHTML("}");
ToHTML("}");
ToHTML("var StoryTitle = GetLocation('StoryTitle');");
ToHTML("if (StoryTitle!=false)");
ToHTML("{");
ToHTML("StoryTitle.Text=StoryTitle.Text.replace(/<[^<>]+?>/g,' ');");
ToHTML("try {");
ToHTML("document.getElementsByTagName('title')[0].innerHTML=StoryTitle.Text;");
ToHTML("} catch (error) {}");
if (TargetFormat=='html')
{
ToHTML("printAddText.innerHTML+=[[asmQuot]]<h1 class='h1'>[[asmQuot]]+StoryTitle.Text+'</h1>';");
}
else
{
ToHTML("speechTitle.innerHTML = StoryTitle.Text;");
}
ToHTML("}");
ToHTML("var StorySubtitle = GetLocation('StorySubtitle');");
ToHTML("if (StorySubtitle!=false)");
ToHTML("{");
if (TargetFormat=='html')
{
ToHTML("printAddText.innerHTML+='<p>'+StorySubtitle.Text+'</p>';");
}
else
{
ToHTML("var intro = speechHash(StorySubtitle.Text.replace(/[[asmBSlash]][[asmBSlash]]n/g,'[[asmBSlash]]n'));");
ToHTML("if (intro) { speechList.unshift(intro); };");
}
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML("printAddText.innerHTML+='<div style=[[asmQuot]]text-align:center;font-size:75%;[[asmQuot]]><a class=[[asmQuot]]plink[[asmQuot]] href=[[asmQuot]]http://sm.axmasoft.com/[[asmQuot]] target=[[asmQuot]]_blank[[asmQuot]] onclick=[[asmQuot]]event.stopPropagation();[[asmQuot]]>"+Lang_CreatedBy+" AXMA&nbsp;Story&nbsp;Maker</a></div>';");
ToHTML("showPrintAdd(true,true);");
ToHTML("showCover = true;");
}
else
{
ToHTML("pageDiv.style.display = 'table';");
ToHTML("speechPassage(!first);");
}
}
ToHTML("}");
if (TargetFormat=='html')
{
ToHTML (ThemeBottom());
}
else
{
speechBottom();
}
if (TargetFormat=='html')
{
html = html.replace(/\&([0-9a-zA-Z#]+?);/g,"&$1:html_code");
html = html.replace(/ToText\s*\(/g,"ToText(");
var parts = html.split(';');
html = parts[0];
var s = "ToText(";
for (var i=1; i<parts.length; i++)
{
if (Left(parts[i].trim(),s.length)==s
&& Left(parts[i-1].trim(),s.length)==s
&& parts[i-1].trim().search(/(?!,true)\)$/)!=-1
&& parts[i].trim().search(/(?!,true)\)$/)!=-1
&& parts[i-1].trim().search(/,menuDiv\)$/)==-1
&& parts[i].trim().search(/,menuDiv\)$/)==-1
)
{
html = RTrim(html);
if (parts[i-1].substr(-7)==',false)')
{
html = html.substr(0,html.length-7);
}
else
{
html = html.substr(0,html.length-1);
}
html += parts[i].replace(s,"+");
}
else
{
html = html + ';' + parts[i];
}
}
}
html = normalText(html);
html = html.replace(/[\r\n]/g,"");
html = Lang_License + html;
MessageHide();
if (TargetFormat=='audio' && !TestMode)
{
var str = '';
var service = [];
if (LangCode=='ru')
{
if (isPrint)
{
service = [
['1a','Одна'],['1o','Одно'],['2e','Две'],
['10','Десять'],['11','Одиннадцать'],['12','Двенадцать'],['13','Тринадцать'],['14','Четырнадцать'],['15','Пятнадцать'],['16','Шестнадцать'],['17','Семнадцать'],['18','Восемнадцать'],['19','Девятнадцать'],
['20','Двадцать'],['30','Тридцать'],['40','Сорок'],['50','Пятьдесят'],['60','Шестьдесят'],['70','Семьдесят'],['80','Восемьдесят'],['90','Девяносто'],
['100','Сто'],['200','Двести'],['300','Триста'],['400','Четыреста'],['500','Пятьсот'],['600','Шестьсот'],['700','Семьсот'],['800','Восемьсот'],['900','Девятьсот'],['1000','Тысяча'],['1000i','Тысячи'],['1000ch','Тысяч'],['1000000','Миллион'],['more','Более миллиона'],
['-','Минус']
];
}
service.push(['0','Ноль'],['1','Один'],['2','Два'],['3','Три'],['4','Четыре'],['5','Пять'],['6','Шесть'],['7','Семь'],['8','Восемь'],['9','Девять'],['impossible','Невозможно выполнить эту команду'],['replay','Начать заново'],['youcan','Ваш выбор:'],['undo','Отменить последнюю команду']);
}
else
{
if (isPrint)
{
service = [		['10','Ten'],['11','Eleven'],['12','Twelve'],['13','Thirteen'],['14','Fourteen'],['15','Fifteen'],['16','Sixteen'],['17','Seventeen'],['18','Eighteen'],['19','Nineteen'],
['20','Twenty'],['30','Thirty'],['40','Forty'],['50','Fifty'],['60','Sixty'],['70','Seventy'],['80','Eighty'],['90','Ninety'],
['100','One Hundred'],['200','Two Hundred'],['300','Three Hundred'],['400','Four hundred'],['500','Five Hundred'],['600','Six Hundred'],['700','Seven Hundred'],['800','Eight Hundred'],['900','Nine Hundred'],['1000','Thousand'],['1000000','One Million'],['more','More than a million'],
['-','Minus']
];
}
service.push(['0','Zero'],['1','One'],['2','Two'],['3','Three'],['4','Four'],['5','Five'],['6','Six'],['7','Seven'],['8','Eight'],['9','Nine'],['and','and'],['impossible','Unable to execute this command'],['replay','Start again'],['youcan','Your choice:'],['undo','Undo the last command']);
}
var serviceCount = service.length;
speechStrings = service.concat(speechStrings);
for (var i=0; i<speechStrings.length; i++)
{
if (i<serviceCount)
{
var fname = speechServicePrefix+speechStrings[i][0];
}
else
{
var fname = speechStrings[i][0];
}
str += ':: '+fname+'\n'+speechStrings[i][1]+'\n\n';
}
speechStrings = [];
}
if (TestMode)
{
TargetFormat = TargetFormatBak;
VisualEventsStop();
mainDiv.style.display = 'none';
previewWin.style.display = 'block';
closePreview.style.display = 'block';
if (!(TestMode && TargetChromeApp)) {
html = html.replace(/<body(.*?)>/,"<body$1 onkeyup='if(event.keyCode==27){parent.ClosePreview();}'>");
previewWin.contentWindow.document.open();
previewWin.contentWindow.document.write(html);
previewWin.contentWindow.document.close();
} else {
html = html.replace(/<body(.*?)>/,"<body$1 onkeyup=\"if(event.keyCode==27){MessageFromFrame('ClosePreview');}\">");
chrome_SendMessage(html);
}
PreviewVisible = true;
}
else
{
if (!PublishToInet)
{
Message(Lang_PleaseWait,true);
saveData(CommandServer('com='+TargetFormat),html,function(response) {
response = response.trim();
MessageHide();
if (response=='register')
{
SignInWindow();
}
else if (response=='needpaid')
{
AlertMessage(Lang_NeedPaidAccount+"<p></p><div class='plink' id='item0'>"+Lang_BuyPaidAccount+'</div>');
SetEvent('item0',function(){ShowWind(false);WindowOpen('http://sm.axmasoft.com/buy.php');});
}
else if (response=='')
{
Message(Lang_Error2);
}
else
{
var downloadWin = WindowOpen(response,'asm_html');
if (TargetFormat=='audio')
{
saveData(CommandServer('com=audiotext'),str,function(response) {
var downloadWin2 = WindowOpen(response,'asm_iabook_text');
});
}
}
});
}
else
{
if (!CheckRequired())
{
return false;
}
if (!TargetDesktop)
{
var storytitle = GetLocByTitle('StoryTitle');
if (storytitle!=false)
{
storytitle = storytitle.Text.replace(/<[^<>]+?>/g,' ').trim().toLowerCase();
}
var files = saveData(Lang_Server+'/user_files_asm.php','login='+urlencode(AuthLogin)+'&password='+urlencode(AuthPassword)).split('\n');
var file_id = false;
for (var key in files)
{
var item = files[key].split('[:73143:]');
var id = item[0].trim();
var title = item[1].trim().toLowerCase();
if (title==storytitle)
{
file_id = id;
break;
}
}
if (file_id)
{
SendPub(file_id);
}
else
{
AreYouSure('SendPub',Lang_WantNewPub);
}
}
else
{
var s = GetLocByTitle('StoryStyle');
if (s)
{
s = s.Text;
var myMatch;
var myRe = /url\(['"]?\s*?(.+?)\s*?['"]?\)/g;
while ((myMatch = myRe.exec(s)) != null)
{
var link = myMatch[1];
if (link!='' && link.substr(0,11)!='data:image/' && link.substr(0,7)!='http://' && link.substr(0,8)!='https://' && link.substr(0,12)!='javascript:;' && link.substr(0,1)!=';')
FilesLinks.push(link);
}
}
FilesLinks = ArrayUnique(FilesLinks);
if (!TargetChromeApp) {
FromJavascript('PublishToInet::'+html+'[[asmSplitterPubData]]'+LocationArr.length+'[[asmSplitterPubData]]'+FilesLinks.join('[[asmSplitterFilesData]]'));
} else {
chrome_PublishToLib(html,FilesLinks,LocationArr.length,GetLocByTitle('StoryTitle').Text.trim());
}
}
}
}
}
function CheckRequired()
{
var CheckLoc = ['Start','StoryTitle','StoryAuthor','StorySubtitle'];
for (var key in CheckLoc)
{
var loc = GetLocByTitle(CheckLoc[key]);
if (!loc || loc.Text.trim()=='')
{
AlertMessage(Lang_MissReqPass+' <b>'+CheckLoc[key]+'</b>');
return false;
}
}
return true;
}
function SendPub(file_id)
{
if (!file_id)
{
file_id = '';
}
var data =
'login='+urlencode(AuthLogin)
+'&password='+urlencode(AuthPassword)
+'&content='+urlencode(html)
+'&source='+urlencode(CreateSaveData())
+'&file_id='+urlencode(file_id)
+'&title='+urlencode(GetLocByTitle('StoryTitle').Text)
+'&subtitle='+urlencode(GetLocByTitle('StorySubtitle').Text)
+'&author='+urlencode(GetLocByTitle('StoryAuthor').Text)
+'&lang='+LangCode
+'&passages='+LocationArr.length
+'&code=ASM_online';
if (data.length>(1024*1024*10))
{
AlertMessage(Lang_FileTooLarge);
return false;
}
Message(Lang_PleaseWait,true);
setTimeout (function()
{
var answer = saveData(Lang_Server+'/post.php',data);
html = '';
data = '';
if (answer=='' || answer.substr(0,5)=='error')
{
AlertMessage(Lang_ErrorPub);
}
else
{
AlertMessage(Lang_SuccessPub+"<p></p><a href='"+answer+"' target='_blank' id='item0'>"+answer+"</a>");
SetEvent('item0',function(){ShowWind(false);});
}
}, 1);
}
var ThemeElements = [ ['audioButton',Lang_Audio,'button'], ['BackButton',Lang_UNDO,'button'], ['ConsoleButton',Lang_Console,'button'], ['PrefsButton',Lang_HTMLPrefs,'button'], ['body',Lang_GlobalSettings,'body'], ['printCont',Lang_PrintBlock,'block'], ['printImage',Lang_PrintImage,'image'], ['menu',Lang_SpecialBlock,'special'], ['toolbar',Lang_SpecialBlock,'special'] ];
var CurrentSelectedBlock;
var ThemeCheckImageSize;
var ThemeImageRuleBorder;
function RestoreStoryStyle()
{
ShowWind(false);
BackupStoryStyle();
ThemePreview();
}
function CreateStoryStyle()
{
if (GetLocIndexByTitle('StoryStyle')==-1) {
LocationArr.push(new Locations("StoryStyle",SetStyleText(),maincanvas_w-LocSize-Cell,maincanvas_h-LocSize-Cell,6,false));
SelectNPaint();
return true;
} else {
return false;
}
}
function handleFile(files)
{
if (window.FileReader) {
var file = files[0];
$('fileSelector').value = '';
var reader = new FileReader();
reader.onload = function() {
ThemeSetImage(reader.result);
}
reader.readAsDataURL(file);
}
}
function ThemeChangeImage(id)
{
CurrentSelectedBlock = id;
if (TargetChromeApp) {
chrome_SelectImage();
} else {
if (TargetDesktop) {
FromJavascript('SelectImage');
} else {
$('fileSelector').click();
}
}
}
function ThemeSetImage(data)
{
var img = new Image();
img.src = data;
img.onload = function()
{
if (!ThemeCheckImageSize || (img.width==24 && img.height==24)) {
var rule = 'background-image';
if (ThemeImageRuleBorder) {
rule = 'border-image';
}
SetThemeStyle('#'+CurrentSelectedBlock,rule,img.src,true);
ThemePreview();
} else {
AlertMessage(Lang_SelectImageSize+' 24x24px');
}
}
}
function ShowHideButton(id,show)
{
if (show) {
show = 'inline-block';
} else {
show = 'none';
}
SetThemeStyle('#'+id,'display',show,false);
}
var styleBegin;
function GetThemeStyle(id,rule,bgimage)
{
var StoryStyleIndex = GetLocIndexByTitle('StoryStyle');
var style = '';
if (GetLocIndexByTitle('StoryStyle')!=-1) {
style = LocationArr[StoryStyleIndex].Text;
} else {
style = SetStyleText();
}
style = style.replace(/background-color/g,'background-polor');
if (rule=='background-color') {
rule = 'background-polor';
}
if (bgimage) {
re = new RegExp(('('+id+'[^}]*?\\{[^}]*?'+rule+'\\s*?:\\s*?url\\s*?\\(\\s*?)([^)]+?)\\s*?\\)'),'mi');
} else {
re = new RegExp(('('+id+'[^}]*?\\{[^}]*?'+rule+'\\s*?:\\s*?)([^;}]+?)\\s*?;'),'mi');
}
var match = style.match(re);
if (match && match[2]) {
styleBegin = matchIndex(match,2);
return match[2].replace(/"/g,"'");
} else {
return false;
}
}
function matchIndex(match,n)
{
var ix = match.index;
for (var i=1; i<n; i++) {
ix+= match[i].length;
}
return ix;
}
function SetThemeStyle(id,rule,value,bgimage)
{
value = value.replace(/"/g,"'").trim();
CreateStoryStyle();
var StoryStyleIndex = GetLocIndexByTitle('StoryStyle');
var style = LocationArr[StoryStyleIndex].Text;
var myvalue = GetThemeStyle(id,rule,bgimage);
if (myvalue!=false) {
style = style.substr(0,styleBegin)+value+style.substr(styleBegin+myvalue.length);
} else {
var re = new RegExp(('('+id+'[^}]+?\s*?\\{)()'),'mi');
var match = style.match(re);
var newrule = rule+':'+value+';';
if (bgimage) {
newrule = rule+':url('+value+');';
}
if (match) {
styleBegin = matchIndex(match,2);
style = style.substr(0,styleBegin)+newrule+' '+style.substr(styleBegin);
} else {
style = style.trim()+'\n\n'+id+' {'+newrule+'}';
}
}
LocationArr[StoryStyleIndex].Text = style;
SelectNPaint();
}
function ChangeTheme()
{
var str = '';
for (var i=0; i<style_themes.length; i++) {
str += "<div class='menu_item' forevent='"+i+"'>"+style_themes[i]+"</div>";
}
mywindow_inner.innerHTML = str;
SetEventsByAttr(7);
ShowWind(true);
SelectMenuItem(style_current_theme,true);
}
function SelectTheme(themeNum)
{
BackupStoryStyle();
SetTheme(themeNum);
SelectMenuItem(style_current_theme,true);
ThemePreview();
}
function BackupStoryStyle()
{
var i = GetLocIndexByTitle('StoryStyle');
if (i!=-1) {
var j = GetLocIndexByTitle('StoryStyle.bak');
if (j!=-1) {
LocationArr[j].Text = LocationArr[i].Text;
LocationArr.splice(i,1);
} else {
LocationArr[i].Title = 'StoryStyle.bak';
}
SelectNPaint();
}
}
function ThemeEditor(close)
{
if (TargetFormat=='audio') {
Message(Lang_NotForABook);
return;
}
if (!close) {
platetitle.innerHTML = Lang_ThemeEdit;
var s = '';
s+="<input type='file' id='fileSelector' accept='image/png,image/jpeg,image/gif' style='display:none;'>";
s+="<div style='width:100%;text-align:center;'>";
if (style_view!='vis') {
s+="<span class='button' id='item1' style='margin-bottom:8px;width:auto;'>"+Lang_ChangeTheme+"</span>&nbsp; ";
}
s+="<span class='button' id='item2' style='margin-bottom:8px;width:auto;'>"+Lang_GlobalSettings+"</span>&nbsp; ";
s+="<span class='button' id='item3' style='margin-bottom:8px;width:auto;'>"+Lang_RestoreStoryStyle+"</span>";
s+="</div>";
platecontent.innerHTML = s;
SetEvent('fileSelector',function(){handleFile(this.files);},'change');
SetEvent('item1',function(){ChangeTheme();});
SetEvent('item2',function(){PropertyEditor('body');});
SetEvent('item3',function(){AreYouSure('RestoreStoryStyle');});
ShowPlate(true);
ThemePreview();
} else {
ShowPlate(false);
}
}
function ResizePreviewIframe() {
if (previewTheme) {
previewTheme.style.height = (myplate.offsetHeight-previewTheme.offsetTop-15)+'px';
}
}
function ThemePreview()
{
var s = ThemeTop(true,false,-1)+ThemePreviewBody()+ThemeBottom(true);
if (!$('previewTheme')) {
previewTheme = document.createElement('iframe');
platecontent.appendChild(previewTheme);
previewTheme.id = 'previewTheme';
previewTheme.style.width = '100%';
previewTheme.frameBorder = '0';
previewTheme.scrolling = 'auto';
previewTheme.style.border = '1px solid #AAAAAA';
ResizePreviewIframe();
window.onresize = function() { ResizePreviewIframe(); };
window.addEventListener('orientationchange',function() { ResizePreviewIframe(); },false);
if (!TargetChromeApp) {
previewTheme.src = 'about:blank';
} else {
previewTheme.src = 'preview.html';
previewTheme.onload = function() { previewTheme.contentWindow.postMessage(s,'*'); };
}
}
if (!TargetChromeApp) {
previewTheme.contentWindow.document.open();
previewTheme.contentWindow.document.write(s);
previewTheme.contentWindow.document.close();
} else {
previewTheme.contentWindow.postMessage(s,'*');
}
}
var ThemeHTML = '';
function ThemeTop(preview,TestMode,OnlyPassageNum)
{
var AppName = 'AXMA Story Maker';
if (TargetFormatBak=='html') {
SetTheme(style_current_theme);
} else {
SetTheme(1);
}
ThemeHTML = '';
if (preview) {
ToTheme("<!DOCTYPE html>");
}
ToTheme("<html lang='"+LangCode+"'>");
ToTheme("<head>");
ToTheme("<title>"+AppName+"</title>");
ToTheme("<link rel='apple-touch-icon' href='http://sm.axmasoft.com/rsc/apple-touch-icon.png' />");
ToTheme("<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>");
ToTheme("<meta name='viewport' content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no'>");
ToTheme("<meta name='mobile-web-app-capable' content='yes'>");
ToTheme("<meta name='apple-mobile-web-app-capable' content='yes'>");
ToTheme("<meta name='apple-mobile-web-app-status-bar-style' content='black'>");
ToTheme("<meta http-equiv='X-UA-Compatible' content='IE=edge'>");
ToTheme("<style type='text/css'>");
var StoryStyleIndex = GetLocIndexByTitle('StoryStyle');
if (StoryStyleIndex!=-1) {
ToTheme(LocationArr[StoryStyleIndex].Text);
} else {
ToTheme(SetStyleText());
}
ToTheme(AnimateCSS());
ToTheme("</style>");
ToTheme("</head>");
if (preview) {
ToThemW("<body id='body' onload=\"pageDiv.style.display='block';\" oncontextmenu='return false;' onkeyup=\"if(event.keyCode==27){");
if (!TargetChromeApp) {
ToThemW("if (parent.WindowVisible){parent.ShowWind(false);}else{parent.ShowPlate(false);}");
} else {
ToThemW("MessageFromFrame('CloseThemeEditor');");
}
ToTheme("}\">");
} else {
ToThemW("<body id='body' onload=[[asmQuot]]page.style.display='block';NewGame(true);[[asmQuot]] onclick='showPrefs(true);' oncontextmenu='");
if (OnlyPassageNum==-1) {
ToThemW("showPrefs();");
}
ToThemW("return false;'>");
}
if (OnlyPassageNum==-1) {
ToTheme("<div id='toolbar'>");
} else {
ToTheme("<table style='width:100%;height:36px;background-color:#FFFFFF;color:#000000;opacity:0.7;text-align:center;position:absolute;top:0;left:0;right:0;z-index:200;overflow:hidden;' cellpadding='0' cellspacing='0'><tr><td valign='middle' width:100%;>"+LocationArr[OnlyPassageNum].Title+"</td></tr></table>");
ToTheme("<div id='toolbar' style='display:none;'>");
}
ToThemW("<table width='100%' cellpadding='0' cellspacing='0' border='0' style='table-layout:fixed;'><tr>");
ToThemW("<td width='20%' align='left'><nobr>");
ToThemW("<div class='imgbutton' style='margin-left:8px;");
if (preview) {
ToThemW("display:inline-block;");
}
ToThemW("' onclick='tbAnimate(this);Back(true);' id='BackButton'></div>");
if (preview || TestMode) {
ToThemW("<div class='imgbutton' style='margin-left:8px;' onclick='tbAnimate(this);showConsole();' id='ConsoleButton'></div>");
}
ToThemW("</nobr></td>");
ToThemW("<td width='60%' align='center' valign='middle'><div id='printTitle'>");
if (preview) {
ToThemW("Title text");
}
ToThemW("</div></td>");
ToThemW("<td width='20%' align='right'><nobr>");
ToThemW("<div class='imgbutton' style='margin-right:8px;");
if (preview) {
ToThemW("display:inline-block;opacity:1;");
}
ToThemW("' onclick='tbAnimate(this);PlayPauseButton();' id='audioButton' ></div>");
ToThemW("<div class='imgbutton' style='margin-right:8px;' onclick='tbAnimate(this);showPrefs();event.stopPropagation();' id='PrefsButton'></div>");
ToThemW("</nobr></td>");
ToThemW("</tr></table>");
ToTheme("</div>");
if (TestMode) {
ToTheme("<div id='console'></div>");
}
ToThemW("<audio id='musicAudio' preload loop style='display:none;'></audio>");
ToThemW("<div id='page'>");
if (style_view=='sw') {
ToThemW("<div id='printHelper'>");
}
ToThemW("<div id='printCont'><div id='print'></div></div>");
if (style_view=='sw') {
ToThemW("</div>");
}
if (style_view!='vis') {
ToThemW("<div id='printImage'");
if (style_view!='rpg') {
ToThemW(" onclick='showImage();");
}
ToThemW("event.stopPropagation();'>");
} else {
ToThemW("<div id='printImage' onclick='togglePrint();event.stopPropagation();'>");
}
ToThemW("<div style='height:100%;width:100%;text-align:center;'><span style='display:inline-block;height:100%;vertical-align:middle;'></span><img id='clickImage' src='"+style_emptyImage+"' style='vertical-align:middle;margin:0;border:none;' alt='' border='0'><div id='spritesDiv' style='position:relative;top:-100%;height:100%;overflow:hidden;'></div></div>");
ToThemW("</div>");
ToThemW("<div id='spacer'></div><div id='menu'><div id='menuTable' style='display:table;border-spacing:0;width:100%;min-width:100%;border:none;'><div style='display:table-row;'><div id='menuInner' style='display:table-cell;padding:0;width:100%;text-align:center;vertical-align:middle;'></div></div></div>");
if (!preview) {
ToThemW("<div id='showImageButton' onclick='showImageToggle();'></div>");
}
ToThemW("</div></div>");
if (!preview) {
ToTheme("<div id='printAddCont' onclick=\"if(prefsDiv.style.display=='none' && dialogWind.style.display=='none'){showPrintAdd(false);}\"><div id='printAdd'><div id='printAddText'></div></div></div>");
}
ToTheme("<div id='prefs' class='wind' onclick='event.stopPropagation();'></div>");
ToTheme("<div id='dialog' class='wind' onclick='event.stopPropagation();'></div>");
ToTheme("<noscript style='position:absolute;top:40%;left:0;right:0;padding:5px;text-align:center;color:#FFFFFF;background-color:#222222;'>"+Lang_JSrequires+"</noscript>");
ToTheme("<script type='text/javascript'>");
ToTheme("try {");
ToTheme("function $(id) {return document.getElementById(id);}");
ToTheme("var printTitle = $('printTitle');");
ToTheme("var printDiv = $('print');");
ToTheme("var printContDiv = $('printCont');");
ToTheme("var printAdd = $('printAdd');");
ToTheme("var printAddCont = $('printAddCont');");
ToTheme("var printAddText = $('printAddText');");
ToTheme("var pageDiv = $('page');");
ToTheme("var menuDiv = $('menuInner');");
ToTheme("var toolbarDiv = $('toolbar');");
ToTheme("var prefsDiv = $('prefs');");
ToTheme("var printImage = $('clickImage');");
ToTheme("var spritesDiv = $('spritesDiv');");
ToTheme("if (!('objectFit' in document.body.style)) {printImage.style.width='auto';printImage.style.height='auto';printImage.style.maxWidth='100%';};");
ToTheme("var printImageBlock = $('printImage');");
ToTheme("var showImageButton = $('showImageButton');");
ToTheme("var consoleDiv = $('console');");
ToTheme("var dialogWind = $('dialog');");
ToTheme("var audioButton = $('audioButton');");
ToTheme("var badImage = '"+style_badImage+"';");
ToTheme("var pictureDefault = '"+style_pictureDefault+"';");
if (preview) {
if (style_view=='sw') {
ToTheme("printImageBlock.style.display = 'none';");
} else {
ToTheme("printImage.src = pictureDefault;");
}
}
if (!preview) {
ToTheme("(function () {");
ToTheme("'use strict';");
ToTheme("function FastClick(layer, options) {");
ToTheme("var oldOnClick;");
ToTheme("options = options || {};");
ToTheme("this.trackingClick = false;");
ToTheme("this.trackingClickStart = 0;");
ToTheme("this.targetElement = null;");
ToTheme("this.touchStartX = 0;");
ToTheme("this.touchStartY = 0;");
ToTheme("this.lastTouchIdentifier = 0;");
ToTheme("this.touchBoundary = options.touchBoundary || 10;");
ToTheme("this.layer = layer;");
ToTheme("this.tapDelay = options.tapDelay || 200;");
ToTheme("if (FastClick.notNeeded(layer)) {");
ToTheme("return;");
ToTheme("}");
ToTheme("function bind(method, context) {");
ToTheme("return function() { return method.apply(context, arguments); };");
ToTheme("}");
ToTheme("var methods = ['onMouse', 'onClick', 'onTouchStart', 'onTouchMove', 'onTouchEnd', 'onTouchCancel'];");
ToTheme("var context = this;");
ToTheme("for (var i = 0, l = methods.length; i < l; i++) {");
ToTheme("context[methods[i]] = bind(context[methods[i]], context);");
ToTheme("}");
ToTheme("if (deviceIsAndroid) {");
ToTheme("layer.addEventListener('mouseover', this.onMouse, true);");
ToTheme("layer.addEventListener('mousedown', this.onMouse, true);");
ToTheme("layer.addEventListener('mouseup', this.onMouse, true);");
ToTheme("}");
ToTheme("layer.addEventListener('click', this.onClick, true);");
ToTheme("layer.addEventListener('touchstart', this.onTouchStart, false);");
ToTheme("layer.addEventListener('touchmove', this.onTouchMove, false);");
ToTheme("layer.addEventListener('touchend', this.onTouchEnd, false);");
ToTheme("layer.addEventListener('touchcancel', this.onTouchCancel, false);");
ToTheme("if (!Event.prototype.stopImmediatePropagation) {");
ToTheme("layer.removeEventListener = function(type, callback, capture) {");
ToTheme("var rmv = Node.prototype.removeEventListener;");
ToTheme("if (type === 'click') {");
ToTheme("rmv.call(layer, type, callback.hijacked || callback, capture);");
ToTheme("} else {");
ToTheme("rmv.call(layer, type, callback, capture);");
ToTheme("}");
ToTheme("};");
ToTheme("layer.addEventListener = function(type, callback, capture) {");
ToTheme("var adv = Node.prototype.addEventListener;");
ToTheme("if (type === 'click') {");
ToTheme("adv.call(layer, type, callback.hijacked || (callback.hijacked = function(event) {");
ToTheme("if (!event.propagationStopped) {");
ToTheme("callback(event);");
ToTheme("}");
ToTheme("}), capture);");
ToTheme("} else {");
ToTheme("adv.call(layer, type, callback, capture);");
ToTheme("}");
ToTheme("};");
ToTheme("}");
ToTheme("if (typeof layer.onclick === 'function') {");
ToTheme("oldOnClick = layer.onclick;");
ToTheme("layer.addEventListener('click', function(event) {");
ToTheme("oldOnClick(event);");
ToTheme("}, false);");
ToTheme("layer.onclick = null;");
ToTheme("}");
ToTheme("}");
ToTheme("var deviceIsAndroid = navigator.userAgent.indexOf('Android') > 0;");
ToTheme("var deviceIsIOS = /iP(ad|hone|od)/.test(navigator.userAgent);");
ToTheme("var deviceIsIOS4 = deviceIsIOS && (/OS 4_[[asmBSlash]]d(_[[asmBSlash]]d)?/).test(navigator.userAgent);");
ToTheme("var deviceIsIOSWithBadTarget = deviceIsIOS && (/OS ([6-9]|[[asmBSlash]]d{2})_[[asmBSlash]]d/).test(navigator.userAgent);");
ToTheme("var deviceIsBlackBerry10 = navigator.userAgent.indexOf('BB10') > 0;");
ToTheme("FastClick.prototype.needsClick = function(target) {");
ToTheme("switch (target.nodeName.toLowerCase()) {");
ToTheme("case 'button':");
ToTheme("case 'select':");
ToTheme("case 'textarea':");
ToTheme("if (target.disabled) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("break;");
ToTheme("case 'input':");
ToTheme("if ((deviceIsIOS && target.type === 'file') || target.disabled) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("break;");
ToTheme("case 'label':");
ToTheme("case 'video':");
ToTheme("return true;");
ToTheme("}");
ToTheme("return (/[[asmBSlash]]bneedsclick[[asmBSlash]]b/).test(target.className);");
ToTheme("};");
ToTheme("FastClick.prototype.needsFocus = function(target) {");
ToTheme("switch (target.nodeName.toLowerCase()) {");
ToTheme("case 'textarea':");
ToTheme("return true;");
ToTheme("case 'select':");
ToTheme("return !deviceIsAndroid;");
ToTheme("case 'input':");
ToTheme("switch (target.type) {");
ToTheme("case 'button':");
ToTheme("case 'checkbox':");
ToTheme("case 'file':");
ToTheme("case 'image':");
ToTheme("case 'radio':");
ToTheme("case 'submit':");
ToTheme("return false;");
ToTheme("}");
ToTheme("return !target.disabled && !target.readOnly;");
ToTheme("default:");
ToTheme("return (/[[asmBSlash]]bneedsfocus[[asmBSlash]]b/).test(target.className);");
ToTheme("}");
ToTheme("};");
ToTheme("FastClick.prototype.sendClick = function(targetElement, event) {");
ToTheme("var clickEvent, touch;");
ToTheme("if (document.activeElement && document.activeElement !== targetElement) {");
ToTheme("document.activeElement.blur();");
ToTheme("}");
ToTheme("touch = event.changedTouches[0];");
ToTheme("clickEvent = document.createEvent('MouseEvents');");
ToTheme("clickEvent.initMouseEvent(this.determineEventType(targetElement), true, true, window, 1, touch.screenX, touch.screenY, touch.clientX, touch.clientY, false, false, false, false, 0, null);");
ToTheme("clickEvent.forwardedTouchEvent = true;");
ToTheme("targetElement.dispatchEvent(clickEvent);");
ToTheme("};");
ToTheme("FastClick.prototype.determineEventType = function(targetElement) {");
ToTheme("if (deviceIsAndroid && targetElement.tagName.toLowerCase() === 'select') {");
ToTheme("return 'mousedown';");
ToTheme("}");
ToTheme("return 'click';");
ToTheme("};");
ToTheme("FastClick.prototype.focus = function(targetElement) {");
ToTheme("var length;");
ToTheme("if (deviceIsIOS && targetElement.setSelectionRange && targetElement.type.indexOf('date') !== 0 && targetElement.type !== 'time') {");
ToTheme("length = targetElement.value.length;");
ToTheme("targetElement.setSelectionRange(length, length);");
ToTheme("} else {");
ToTheme("targetElement.focus();");
ToTheme("}");
ToTheme("};");
ToTheme("FastClick.prototype.updateScrollParent = function(targetElement) {");
ToTheme("var scrollParent, parentElement;");
ToTheme("scrollParent = targetElement.fastClickScrollParent;");
ToTheme("if (!scrollParent || !scrollParent.contains(targetElement)) {");
ToTheme("parentElement = targetElement;");
ToTheme("do {");
ToTheme("if (parentElement.scrollHeight > parentElement.offsetHeight) {");
ToTheme("scrollParent = parentElement;");
ToTheme("targetElement.fastClickScrollParent = parentElement;");
ToTheme("break;");
ToTheme("}");
ToTheme("parentElement = parentElement.parentElement;");
ToTheme("} while (parentElement);");
ToTheme("}");
ToTheme("if (scrollParent) {");
ToTheme("scrollParent.fastClickLastScrollTop = scrollParent.scrollTop;");
ToTheme("}");
ToTheme("};");
ToTheme("FastClick.prototype.getTargetElementFromEventTarget = function(eventTarget) {");
ToTheme("if (eventTarget.nodeType === Node.TEXT_NODE) {");
ToTheme("return eventTarget.parentNode;");
ToTheme("}");
ToTheme("return eventTarget;");
ToTheme("};");
ToTheme("FastClick.prototype.onTouchStart = function(event) {");
ToTheme("var targetElement, touch, selection;");
ToTheme("if (event.targetTouches.length > 1) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("targetElement = this.getTargetElementFromEventTarget(event.target);");
ToTheme("touch = event.targetTouches[0];");
ToTheme("if (deviceIsIOS) {");
ToTheme("selection = window.getSelection();");
ToTheme("if (selection.rangeCount && !selection.isCollapsed) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (!deviceIsIOS4) {");
ToTheme("if (touch.identifier && touch.identifier === this.lastTouchIdentifier) {");
ToTheme("event.preventDefault();");
ToTheme("return false;");
ToTheme("}");
ToTheme("this.lastTouchIdentifier = touch.identifier;");
ToTheme("this.updateScrollParent(targetElement);");
ToTheme("}");
ToTheme("}");
ToTheme("this.trackingClick = true;");
ToTheme("this.trackingClickStart = event.timeStamp;");
ToTheme("this.targetElement = targetElement;");
ToTheme("this.touchStartX = touch.pageX;");
ToTheme("this.touchStartY = touch.pageY;");
ToTheme("if ((event.timeStamp - this.lastClickTime) < this.tapDelay) {");
ToTheme("event.preventDefault();");
ToTheme("}");
ToTheme("return true;");
ToTheme("};");
ToTheme("FastClick.prototype.touchHasMoved = function(event) {");
ToTheme("var touch = event.changedTouches[0], boundary = this.touchBoundary;");
ToTheme("if (Math.abs(touch.pageX - this.touchStartX) > boundary || Math.abs(touch.pageY - this.touchStartY) > boundary) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("return false;");
ToTheme("};");
ToTheme("FastClick.prototype.onTouchMove = function(event) {");
ToTheme("if (!this.trackingClick) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (this.targetElement !== this.getTargetElementFromEventTarget(event.target) || this.touchHasMoved(event)) {");
ToTheme("this.trackingClick = false;");
ToTheme("this.targetElement = null;");
ToTheme("}");
ToTheme("return true;");
ToTheme("};");
ToTheme("FastClick.prototype.findControl = function(labelElement) {");
ToTheme("if (labelElement.control !== undefined) {");
ToTheme("return labelElement.control;");
ToTheme("}");
ToTheme("if (labelElement.htmlFor) {");
ToTheme("return $(labelElement.htmlFor);");
ToTheme("}");
ToTheme("return labelElement.querySelector('button, input:not([type=hidden]), keygen, meter, output, progress, select, textarea');");
ToTheme("};");
ToTheme("FastClick.prototype.onTouchEnd = function(event) {");
ToTheme("var forElement, trackingClickStart, targetTagName, scrollParent, touch, targetElement = this.targetElement;");
ToTheme("if (!this.trackingClick) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if ((event.timeStamp - this.lastClickTime) < this.tapDelay) {");
ToTheme("this.cancelNextClick = true;");
ToTheme("return true;");
ToTheme("}");
ToTheme("this.cancelNextClick = false;");
ToTheme("this.lastClickTime = event.timeStamp;");
ToTheme("trackingClickStart = this.trackingClickStart;");
ToTheme("this.trackingClick = false;");
ToTheme("this.trackingClickStart = 0;");
ToTheme("if (deviceIsIOSWithBadTarget) {");
ToTheme("touch = event.changedTouches[0];");
ToTheme("targetElement = document.elementFromPoint(touch.pageX - window.pageXOffset, touch.pageY - window.pageYOffset) || targetElement;");
ToTheme("targetElement.fastClickScrollParent = this.targetElement.fastClickScrollParent;");
ToTheme("}");
ToTheme("targetTagName = targetElement.tagName.toLowerCase();");
ToTheme("if (targetTagName === 'label') {");
ToTheme("forElement = this.findControl(targetElement);");
ToTheme("if (forElement) {");
ToTheme("this.focus(targetElement);");
ToTheme("if (deviceIsAndroid) {");
ToTheme("return false;");
ToTheme("}");
ToTheme("targetElement = forElement;");
ToTheme("}");
ToTheme("} else if (this.needsFocus(targetElement)) {");
ToTheme("if ((event.timeStamp - trackingClickStart) > 100 || (deviceIsIOS && window.top !== window && targetTagName === 'input')) {");
ToTheme("this.targetElement = null;");
ToTheme("return false;");
ToTheme("}");
ToTheme("this.focus(targetElement);");
ToTheme("this.sendClick(targetElement, event);");
ToTheme("if (!deviceIsIOS || targetTagName !== 'select') {");
ToTheme("this.targetElement = null;");
ToTheme("event.preventDefault();");
ToTheme("}");
ToTheme("return false;");
ToTheme("}");
ToTheme("if (deviceIsIOS && !deviceIsIOS4) {");
ToTheme("scrollParent = targetElement.fastClickScrollParent;");
ToTheme("if (scrollParent && scrollParent.fastClickLastScrollTop !== scrollParent.scrollTop) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("}");
ToTheme("if (!this.needsClick(targetElement)) {");
ToTheme("event.preventDefault();");
ToTheme("this.sendClick(targetElement, event);");
ToTheme("}");
ToTheme("return false;");
ToTheme("};");
ToTheme("FastClick.prototype.onTouchCancel = function() {");
ToTheme("this.trackingClick = false;");
ToTheme("this.targetElement = null;");
ToTheme("};");
ToTheme("FastClick.prototype.onMouse = function(event) {");
ToTheme("if (!this.targetElement) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (event.forwardedTouchEvent) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (!event.cancelable) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (!this.needsClick(this.targetElement) || this.cancelNextClick) {");
ToTheme("if (event.stopImmediatePropagation) {");
ToTheme("event.stopImmediatePropagation();");
ToTheme("} else {");
ToTheme("event.propagationStopped = true;");
ToTheme("}");
ToTheme("event.stopPropagation();");
ToTheme("event.preventDefault();");
ToTheme("return false;");
ToTheme("}");
ToTheme("return true;");
ToTheme("};");
ToTheme("FastClick.prototype.onClick = function(event) {");
ToTheme("var permitted;");
ToTheme("if (this.trackingClick) {");
ToTheme("this.targetElement = null;");
ToTheme("this.trackingClick = false;");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (event.target.type === 'submit' && event.detail === 0) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("permitted = this.onMouse(event);");
ToTheme("if (!permitted) {");
ToTheme("this.targetElement = null;");
ToTheme("}");
ToTheme("return permitted;");
ToTheme("};");
ToTheme("FastClick.prototype.destroy = function() {");
ToTheme("var layer = this.layer;");
ToTheme("if (deviceIsAndroid) {");
ToTheme("layer.removeEventListener('mouseover', this.onMouse, true);");
ToTheme("layer.removeEventListener('mousedown', this.onMouse, true);");
ToTheme("layer.removeEventListener('mouseup', this.onMouse, true);");
ToTheme("}");
ToTheme("layer.removeEventListener('click', this.onClick, true);");
ToTheme("layer.removeEventListener('touchstart', this.onTouchStart, false);");
ToTheme("layer.removeEventListener('touchmove', this.onTouchMove, false);");
ToTheme("layer.removeEventListener('touchend', this.onTouchEnd, false);");
ToTheme("layer.removeEventListener('touchcancel', this.onTouchCancel, false);");
ToTheme("};");
ToTheme("FastClick.notNeeded = function(layer) {");
ToTheme("var metaViewport;");
ToTheme("var chromeVersion;");
ToTheme("var blackberryVersion;");
ToTheme("if (typeof window.ontouchstart === 'undefined') {");
ToTheme("return true;");
ToTheme("}");
ToTheme("chromeVersion = +(/Chrome[[asmBSlash]]/([0-9]+)/.exec(navigator.userAgent) || [,0])[1];");
ToTheme("if (chromeVersion) {");
ToTheme("if (deviceIsAndroid) {");
ToTheme("metaViewport = document.querySelector('meta[name=viewport]');");
ToTheme("if (metaViewport) {");
ToTheme("if (metaViewport.content.indexOf('user-scalable=no') !== -1) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (chromeVersion > 31 && document.documentElement.scrollWidth <= window.outerWidth) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("}");
ToTheme("} else {");
ToTheme("return true;");
ToTheme("}");
ToTheme("}");
ToTheme("if (deviceIsBlackBerry10) {");
ToTheme("blackberryVersion = navigator.userAgent.match(/Version[[asmBSlash]]/([0-9]*)[[asmBSlash]].([0-9]*)/);");
ToTheme("if (blackberryVersion[1] >= 10 && blackberryVersion[2] >= 3) {");
ToTheme("metaViewport = document.querySelector('meta[name=viewport]');");
ToTheme("if (metaViewport) {");
ToTheme("if (metaViewport.content.indexOf('user-scalable=no') !== -1) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("if (document.documentElement.scrollWidth <= window.outerWidth) {");
ToTheme("return true;");
ToTheme("}");
ToTheme("}");
ToTheme("}");
ToTheme("}");
ToTheme("if (layer.style.msTouchAction === 'none') {");
ToTheme("return true;");
ToTheme("}");
ToTheme("return false;");
ToTheme("};");
ToTheme("FastClick.attach = function(layer, options) {");
ToTheme("return new FastClick(layer, options);");
ToTheme("};");
ToTheme("if (typeof define == 'function' && typeof define.amd == 'object' && define.amd) {");
ToTheme("define(function() {");
ToTheme("return FastClick;");
ToTheme("});");
ToTheme("} else if (typeof module !== 'undefined' && module.exports) {");
ToTheme("module.exports = FastClick.attach;");
ToTheme("module.exports.FastClick = FastClick;");
ToTheme("} else {");
ToTheme("window.FastClick = FastClick;");
ToTheme("}");
ToTheme("}());");
ToTheme("window.addEventListener('load', function() {");
ToTheme("    FastClick.attach(document.body);");
ToTheme("}, false);");
}
if (TargetChromeApp) {
ToTheme("function MessageFromFrame(s) {");
ToTheme("	window.parent.parent.postMessage(s,'*');");
ToTheme("}");
ToTheme("window.addEventListener('message', function(e) {");
ToTheme("	eval(e.data);");
ToTheme("});");
}
return ThemeHTML;
}
function ThemePreviewBody()
{
ThemeHTML = 'function $(id) {return document.getElementById(id);}';
ToTheme("printDiv.innerHTML = \"<div class='header'>Header</div><p></p><span class='plink'>Lorem ipsum dolor sit amet</span>, consectetur adipiscing elit. Duis eu dolor at nisl lobortis tincidunt. Proin ac dolor convallis, molestie erat nec, vestibulum dui. Nulla quis arcu ut justo varius mattis. Proin velit massa, sollicitudin et posuere fringilla, sollicitudin vitae lacus. Mauris laoreet erat eros, sit amet feugiat eros venenatis sit amet.<p></p>Sed consectetur, elit eu malesuada semper, <span class='plink'>ligula velit</span> consequat risus, in tincidunt metus ligula vel sapien. Phasellus pharetra ipsum ut urna accumsan, ac interdum mi volutpat. Curabitur semper leo ut sapien euismod lacinia. Mauris non tortor sodales, auctor risus id, rutrum massa. Praesent leo lacus, sollicitudin ac elit id, pulvinar pharetra nisl. Phasellus eu odio in neque tristique ornare. Quisque ac arcu imperdiet, accumsan purus vitae, lacinia leo. Fusce tellus nisl, elementum vel justo eu, eleifend consectetur erat. Mauris porta in sapien ut hendrerit. Vestibulum egestas tempus scelerisque.<p></p>Nullam vel neque ut lorem hendrerit euismod. Aenean lectus ligula, dignissim vel ultricies ut, dictum non nisl. Duis blandit porta purus, ac pellentesque purus condimentum a. Nunc pulvinar pretium orci nec eleifend. Proin non elit porttitor, iaculis ligula id, auctor tellus. Proin nec felis ut turpis ultrices congue in vel sapien. <span class='plink'>Pellentesque</span> commodo dolor vel cursus interdum. Nunc sem nisl, molestie vel lobortis vel, tincidunt eu lectus. Donec quis nulla varius mauris volutpat ornare. Phasellus venenatis nibh libero, nec maximus neque imperdiet eget.<p></p><div class='button'>Button 1</div><p></p><div class='button'>Button 2</div>\";");
ToTheme("menuDiv.innerHTML = \"<span class='plink'>Menu Item 1</span><span class='plink'>Menu Item 2</span><span class='plink'>Menu Item 3</span>\";");
ToTheme("var ThemeElements="+JSON.stringify(ThemeElements));
ToTheme("for (var key in ThemeElements) {");
ToTheme("	var id = ThemeElements[key][0];");
ToTheme("	$(id).onclick = (function(id) {");
if (!TargetChromeApp) {
ToTheme("return function(e){parent.PropertyEditor(id);e.stopPropagation();};");
} else {
ToTheme("return function(e){MessageFromFrame('PropertyEditor='+id);e.stopPropagation();};");
}
ToTheme("	})(id);");
ToTheme("}");
return ThemeHTML;
}
function ThemeBottom(preview)
{
ThemeHTML = '';
ToTheme("} catch (error) {");
ToTheme("  printDiv.innerHTML='"+Lang_Error+":<p>'+error.name+'<br>'+error.message;");
ToTheme("}");
ToTheme("<\/script>");
ToTheme("</body>");
ToTheme("</html>");
return ThemeHTML;
}
function ToTheme(str)
{
ThemeHTML += str+'\n';
}
function ToThemW(str)
{
ThemeHTML += str;
}
function ThemeChangeColor(id,bg,color,link,text)
{
var rule = 'color';
if (bg) {
rule = 'background-color';
}
SetThemeStyle(id,rule,color,false);
if (link) {
}
if (text && id=='#printCont') {
SetThemeStyle('.button','border-color',color,false);
SetThemeStyle('.myinput','border-color',color,false);
SetThemeStyle('.rcol','border-left-color',color,false);
SetThemeStyle('.hr','background-color',color,false);
SetThemeStyle('.hr','color',color,false);
SetThemeStyle('.scrollbar','background-color',color,false);
SetThemeStyle('.choice .plink:before','color',color,false);
}
ThemePreview();
}
function ThemeSetShadow(checked)
{
var shadow = 'none';
if (checked) {
shadow = style_shadows;
}
SetThemeStyle('#printCont','box-shadow',shadow,false);
ThemePreview();
}
function ThemeSetValue(value,max)
{
value = parseInt(value,10);
if (isNaN(+value) || value<0) {
value = 0;
} else if (value>max) {
value = max;
}
return value;
}
function ThemeSetBorder(elem)
{
if (elem) {
var rule = 'border-width';
var id = '#printCont';
var value = ThemeSetValue(elem.value,24);
elem.value = value;
SetThemeStyle(id,rule,value+'px',false);
var StoryStyleIndex = GetLocIndexByTitle('StoryStyle');
var style = LocationArr[StoryStyleIndex].Text;
style = style.replace(/(border-image\s*?:\s*?url\s*?\(.*?\)\s*?)(\d+?)(\s+?.+?[;}])/gmi,'$1'+value+'$3');
LocationArr[StoryStyleIndex].Text = style;
SelectNPaint();
ThemePreview();
}
}
function ThemeSetRadius(elem)
{
if (elem) {
var rule = 'border-radius';
var id = '.button';
var value = ThemeSetValue(elem.value,24);
elem.value = value;
SetThemeStyle(id,rule,value+'px',false);
ThemePreview();
}
}
function PropertyEditor(id)
{
var name = '';
var found = false;
for (var key in ThemeElements) {
if (ThemeElements[key][0]==id) {
found = true;
name = ThemeElements[key][1];
type = ThemeElements[key][2];
break;
}
}
if (!found) {
return;
}
var menu_button = " class='menu_item'";
var str = '<div><b>'+name+'</b></div><p></p>';
var myipnput = "<input style='text-align:center;' autocomplete='off' autocorrect='off' autocapitalize='off' spellcheck='false'";
if (type=='button') {
var checked = '';
var disabled = '';
if (id!='BackButton') {
disabled = ' disabled';
}
checked = ' checked';
if (GetThemeStyle(id,'display')=='none') {
checked = '';
}
str += "<div"+menu_button+" id='item1'>"+Lang_ChangeButton+" (24х24px)</div><p></p><label>"+Lang_Show+"&nbsp;<input id='item2' type='checkbox'"+checked+disabled+"></label>";
} else if (type=='body') {
str += "<div>"+Lang_Font+"</div>";
var font_cur = GetThemeStyle('#body','font-family',false);
str += "<select id='item3'>";
for (var i=0; i<themes_fonts.length; i++) {
var font = themes_fonts[i];
var fname = font.split(',')[0].replace(/['"]/g,'').trim();
var selected = '';
if (font==font_cur)
{
selected = ' selected';
}
str += "<option value=\""+font+"\""+selected+">"+fname+'</option>';
}
str += "</select>";
str += "<p></p><div>"+Lang_MainColor+"</div>";
str += myipnput+" type='color' id='item4' value=\""+GetThemeStyle('#body','background-color',false)+"\"><p></p>";
var checked = '';
if (GetThemeStyle('#printCont','box-shadow',false)!='none') {
checked = ' checked';
}
var disabled = '';
if (GetThemeStyle('#printAdd','box-shadow',false)=='none') {
disabled = ' disabled';
}
str += "<div><label>"+Lang_ShadowsStatBlocks+"&nbsp;<input id='item5' type='checkbox'"+checked+disabled+"></label></div><p></p>";
str += "<div"+menu_button+" id='item6'>"+Lang_SetTexture+"</div><p></p>";
str += "<div"+menu_button+" id='item7'>"+Lang_RemoveTexture+"</div>";
} else if (type=='block') {
str += "<div>"+Lang_ColorBack+"</div>";
str += myipnput+" id='item8' type='color' value=\""+GetThemeStyle('#printCont','background-color',false)+"\"><p></p>";
str += "<div>"+Lang_ColorText+"</div>";
str += myipnput+" id='item9' type='color' value=\""+GetThemeStyle('#printCont','color',false)+"\"><p></p>";
str += "<div>"+Lang_ColorLink+"</div>";
str += myipnput+" id='item10' type='color' value=\""+GetThemeStyle('.plink','color',false)+"\"><p></p>";
str += "<div>"+Lang_ColorHeader+"</div>";
str += myipnput+" id='item11' type='color' value=\""+GetThemeStyle('.header','color',false)+"\"><p></p>";
str += "<div>"+Lang_ButtonRadius+"</div>";
str += myipnput+" id='item12' type='number' min='0' max='24' value=\""+parseInt(GetThemeStyle('.button','border-radius',false),10)+"\"><p></p>";
str += "<div>"+Lang_FrameWidth+"</div>";
str += myipnput+" id='item13' type='number' min='0' value=\""+parseInt(GetThemeStyle('#printCont','border-width',false),10)+"\"><p></p>";
str += "<div"+menu_button+" id='item14'>"+Lang_FrameSet+"</div><p></p>";
str += "<div"+menu_button+" id='item15'>"+Lang_FrameRemove+"</div>";
} else if (type=='special') {
str += "<div>"+Lang_ColorBack+"</div>";
str += myipnput+" id='item16' type='color' value=\""+GetThemeStyle('#toolbar','background-color',false)+"\"><p></p>";
str += "<div>"+Lang_ColorText+"</div>";
str += myipnput+" id='item17' type='color' value=\""+GetThemeStyle('#toolbar','color',false)+"\"><p></p>";
} else if (type=='image') {
str += Lang_NoProperties+'<p></p>';
}
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){ThemeImageRuleBorder=false;ThemeCheckImageSize=true;ThemeChangeImage(id);});
SetEvent('item2',function(){ShowHideButton(id,this.checked);},'change');
SetEvent('item3',function(){SetThemeStyle('#body','font-family',this.value,false);ThemePreview();},'input');
SetEvent('item4',function(){ThemeChangeColor('#body',true,this.value);},'input');
SetEvent('item5',function(){ThemeSetShadow(this.checked);},'change');
SetEvent('item6',function(){ThemeImageRuleBorder=false;ThemeCheckImageSize=false;ThemeChangeImage('body');});
SetEvent('item7',function(){SetThemeStyle('#body','background-image',style_emptyImage,true);ThemePreview();});
SetEvent('item8',function(){ThemeChangeColor('#printCont',true,this.value);},'input');
SetEvent('item9',function(){ThemeChangeColor('#printCont',false,this.value,false,true);},'input');
SetEvent('item10',function(){ThemeChangeColor('.plink',false,this.value,true);},'input');
SetEvent('item11',function(){ThemeChangeColor('.header',false,this.value);},'input');
SetEvent('item12',function(){ThemeSetRadius(this);},'input');
SetEvent('item13',function(){ThemeSetBorder(this);},'input');
SetEvent('item14',function(){ThemeImageRuleBorder=true;ThemeCheckImageSize=false;ThemeChangeImage('printCont');});
SetEvent('item15',function(){SetThemeStyle('#printCont','border-image',style_emptyImage,true);ThemePreview();});
SetEvent('item16',function(){ThemeChangeColor('#toolbar',true,this.value);},'input');
SetEvent('item17',function(){ThemeChangeColor('#toolbar',false,this.value,false,true);},'input');
ShowWind(true);
}
function AddFileData(type,dataurl)
{
var tp = "";
if (type=='audio')
{
tp = "audio/mpeg";
}
else if (type=='image')
{
tp = "image/png,image/jpeg,image/gif";
}
var s = "<input type='file' id='fileSelector' accept='"+tp+"' style='display:none;' onchange=\"handleDataFile(this.files,'"+type+"');\">";
if (dataurl)
{
s += "<div id='file_preview'>"+FileDataPreview(type,dataurl)+"</div>";
s += "<p>"+Lang_FileName+"<br>";
s += "<input id='filename' type='text'>";
s += "<p><span class='button' id='item1' style='float:left;'>"+Lang_HTMLSave+"</span><span class='button' id='item2' style='float:right;'>"+Lang_FileAnother+"</span>";
}
else
{
s += "<div class='menu_item' id='item3'>"+Lang_FileSelect+"</div>";
}
mywindow_inner.innerHTML = s;
SetEvent('image_preview',function(){WindowOpen(this.src,'',true);});
SetEvent('show_image_size',function(){showImageSize(dataurl,this);});
SetEvent('item1',function(){SaveFileData(dataurl,$('filename').value,type);});
SetEvent('item2',function(){ShowWind(false);AddFileData(type);});
SetEvent('item3',function(){SelectFile(type);});
if (!dataurl)
{
SelectFile(type);
}
}
function FileDataPreview(type,dataurl)
{
var view = "";
if (type=='audio')
{
view = "<audio controls><source src='"+dataurl+"' type='audio/mp3'></audio>";
}
else if (type=='image')
{
view = "<img id='image_preview' src='"+dataurl+"'><p class='plink' id='show_image_size'>"+Lang_ShowSize+"</p>";
}
return view;
}
function showImageSize(dataurl,elem)
{
var img = new Image();
img.src = dataurl;
img.onload = function() {
elem.onclick = function() {};
elem.className = '';
elem.innerHTML = img.width+'х'+img.height+'px, '+Math.ceil(img.src.length/1024)+'KB';
};
}
function FileDataList(type,onlynames)
{
var data = GetLocByTitle('StoryData');
if (data)
{
data = data.Text.replace(/\n+/,'\n').trim().split('\n');
}
var list = [];
for (var key in data)
{
var line = data[key];
if (line.indexOf('::')==-1)
{
continue;
}
line = line.split('::');
var id = line[0].trim().toLowerCase();
if (onlynames)
{
list.push(id);
}
else
{
var dataurl = line[1].trim();
var mytype = line[1].split(':')[1].split(';')[0].split('/')[0];
if (type!='all')
{
if (mytype==type)
{
list.push([id,dataurl,mytype]);
}
}
else
{
list.push([id,dataurl,mytype]);
}
}
}
return list.sort();
}
function FileDataWind(type,insert)
{
var s = '';
if (!insert)
{
s += "<div style='margin-bottom:8px;font-weight:bold;'>";
if (type=='all')
{
s += Lang_FilesAll+"</div>";
s += "<span class='button' id='item1' style='float:left;'>"+Lang_FilesImages+"</span>";
s += "<span class='button' id='item2' style='float:right;'>"+Lang_FilesSounds+"</span>";
}
if (type=='image')
{
s += Lang_FilesImages+"</div>";
s += "<span class='button' id='item3' style='float:left;'>"+Lang_FilesSounds+"</span>";
s += "<span class='button' id='item4' style='float:right;'>"+Lang_FilesAll+"</span>";
}
if (type=='audio')
{
s += Lang_FilesSounds+"</div>";
s += "<span class='button' id='item5' style='float:left;'>"+Lang_FilesImages+"</span>";
s += "<span class='button' id='item6' style='float:right;'>"+Lang_FilesAll+"</span>";
}
s += "<div style='clear:both'></div>";
}
var list = FileDataList(type);
if (list.length>0)
{
for (var key in list)
{
var id = list[key][0];
var dataurl = list[key][1];
if (type!='all')
{
var mytype = type;
}
else
{
var mytype = list[key][2];
}
var view = id+'<br>';
if (mytype=='image')
{
view += "<img src='"+dataurl+"'>";
}
if (mytype=='audio')
{
view += "<audio controls forevent><source src='"+dataurl+"' type='audio/mp3'></audio>";
}
if (insert)
{
s += "<div class='menu_item' forevent2='"+id+"'>"+view+"</div>";
}
else
{
s += "<div class='menu_item' forevent2='"+id+"|"+type+"|"+mytype+"|"+insert+"|"+dataurl+"'>"+view+"</div>";
}
s += "<div style='clear:both;'></div>";
}
mywindow_inner.innerHTML = s;
SetEvent('item1',function(){FileDataWind('image',false);});
SetEvent('item2',function(){FileDataWind('audio',false);});
SetEvent('item3',function(){FileDataWind('audio',false);});
SetEvent('item4',function(){FileDataWind('all',false);});
SetEvent('item5',function(){FileDataWind('image',false);});
SetEvent('item6',function(){FileDataWind('all',false);});
SetEventsByAttr(14);
if (insert) {
SetEventsByAttr(15,'forevent2');
} else {
SetEventsByAttr(16,'forevent2');
}
ShowWind(true);
if (!insert) {
SelectedMenuItemNum = 2;
SelectMenuItem(SelectedMenuItemNum,true);
}
}
else
{
ShowWind(false);
Message(Lang_FilesNo);
}
}
function EditFileData(id,type,mytype,insert,dataurl)
{
var s = "<div class='menu_item' id='item1'>"+Lang_Return+"</div>";
s += ""
s += "<p>"+FileDataPreview(mytype,dataurl)+"</p>";
s += "<input type='text' id='filename' style='margin-bottom:8px;' value='"+id+"'>";
s += "<span style='float:left;' class='button' id='item2'>"+Lang_FileRename+"</span>";
s += "<span style='float:right;' class='button' id='item3'>"+Lang_FileRemove+"</span>";
s += "<div style='clear:both;'></div>";
mywindow_inner.innerHTML = s;
SetEvent('show_image_size',function(){showImageSize(dataurl,this);});
SetEvent('item1',function(){FileDataWind(type,insert);});
SetEvent('item2',function(){ChangeFileData(id,type,insert,$('filename').value);});
SetEvent('item3',function(){ChangeFileData(id,type,insert,false);});
ShowWind(true);
}
function ChangeFileData(id,type,insert,name)
{
var ok = false;
var dataPassage = GetLocByTitle('StoryData');
if (dataPassage)
{
id = id.trim().toLowerCase()
data = dataPassage.Text.replace(/\n+/,'\n').trim().split('\n');
for (i=0; i<data.length; i++)
{
var line = data[i];
if (line.indexOf('::')==-1)
{
continue;
}
line = line.split('::');
var myid = line[0].trim().toLowerCase();
var mydataurl = line[1].trim();
if (myid==id)
{
if (name===false)
{
data.splice(i,1);
ok = true;
}
else
{
name = NameFileData(name);
if (name)
{
data[i] = name+'::'+mydataurl;
ok = true;
}
}
break;
}
}
if (ok)
{
dataPassage.Text = data.join('\n');
SelectNPaint();
FileDataWind(type,insert);
}
}
}
function AddFileWind(type,result)
{
var fileSize = result.length;
if (fileSize>maxFileSizeStoryData) {
AlertMessage(Lang_FileTooLarge+'<br>('+fileSize+'>'+maxFileSizeStoryData+')');
} else {
AddFileData(type,result);
ShowWind(true);
if ($('filename')) {
$('filename').focus();
}
}
}
function handleDataFile(files,type)
{
if (window.FileReader) {
var file = files[0];
$('fileSelector').value = '';
var reader = new FileReader();
reader.onload = function() {
AddFileWind(type,reader.result);
}
reader.readAsDataURL(file);
}
}
function SelectFile(type)
{
if (TargetChromeApp) {
chrome_SelectDataFile(type);
} else {
if (TargetDesktop) {
if (type=='image') {
FromJavascript('SelectFileImage');
} else if (type=='audio') {
FromJavascript('SelectFileAudio');
}
} else {
$('fileSelector').click();
}
}
}
function CreateStoryData()
{
if (GetLocIndexByTitle('StoryData')==-1)
{
LocationArr.push(new Locations("StoryData",'',maincanvas_w-LocSize-Cell,maincanvas_h-LocSize-Cell,6,false));
SelectNPaint();
return true;
}
else
{
return false;
}
}
function SaveFileData(dataurl,name,type)
{
name = NameFileData(name);
if (name)
{
CreateStoryData();
GetLocByTitle('StoryData').Text += '\n' + name + '::' + dataurl;
SelectNPaint();
ShowWind(false);
}
}
function NameFileData(name)
{
name = name.replace(/::/g,':').replace(/[\r\n"']+/g,'').trim().toLowerCase();
if (name=='')
{
$('filename').focus();
Message(Lang_InputFileName);
return false;
}
var storydataLoc = GetLocByTitle('StoryData');
if (storydataLoc)
{
var list = FileDataList(false,true);
var namelc = name.toLowerCase();
for (var key in list)
{
if (list[key].split('::')[0].trim().toLowerCase()==namelc)
{
$('filename').focus();
Message(Lang_FileNameExists);
return false;
}
}
}
return name;
}
var chrome_CurrentEntry = null;
var chrome_FilesDirectory = null;
var chrome_OS = null;
var chrome_RecentFiles = [];
if (TargetChromeApp) {
chrome.runtime.getPlatformInfo(function(info) {
chrome_OS = info.os;
});
}
function chrome_InsertMedia(picture) {
if (!chrome_FilesDirectory) {
AlertMessage(Lang_NeedFilesDir,function() {
chrome_SelectFilesDir(function() {
chrome_AddFile(picture);
});
});
} else {
chrome_AddFile(picture);
}
}
function chrome_SelectFilesDir(callback,callbackCancel) {
chrome.fileSystem.chooseEntry({type: 'openDirectory'}, function(directoryEntry) {
if (chrome.runtime.lastError) {
if (callbackCancel) {
callbackCancel();
}
return;
}
chrome_FilesDirectory = directoryEntry;
chrome_SetFilesDirInfo();
if (chrome_RecentFiles.length>0 && chrome_RecentFiles[0][0]==chrome.fileSystem.retainEntry(chrome_CurrentEntry)) {
var dirID = null;
if (chrome_FilesDirectory) {
dirID = chrome.fileSystem.retainEntry(chrome_FilesDirectory);
}
chrome_RecentFiles[0][1] = dirID;
chrome_SaveRecent();
}
if (callback) {
callback();
}
});
}
function chrome_AddFile(picture) {
var exts = ['png','gif','jpg','jpeg'];
var decs = Lang_Images;
if (!picture) {
exts.push('mp3');
decs = decs+' / '+Lang_AudioMP3;
}
chrome.fileSystem.chooseEntry(
{
type: 'openFile',
accepts: [{
description: decs,
extensions: exts
}],
acceptsAllTypes: false
},
function(entry) {
if (chrome.runtime.lastError) {
return;
}
var insertFile = function(copy) {
if (copy) {
entry.copyTo(chrome_FilesDirectory);
}
if (picture) {
InsertLink("<<picture '[[File: "+chrome_FilesDirectory.name+"/"+entry.name+"]]'>>");
} else {
InsertLink("[[File: "+chrome_FilesDirectory.name+"/"+entry.name+"]]");
}
}
chrome_FilesDirectory.getFile(
entry.name,
{},
function(existsEntry) {
chrome.fileSystem.getDisplayPath(existsEntry,function(displayPath1) {
chrome.fileSystem.getDisplayPath(entry,function(displayPath2) {
if (displayPath1!=displayPath2) {
existsEntry.remove(function() {
insertFile(true);
});
} else {
insertFile(false);
}
});
});
},
function() {
insertFile(true);
}
);
}
);
}
function chrome_SelectDataFile(type) {
var exts;
if (type=='image') {
var desc = Lang_Images;
exts = ['png','gif','jpg','jpeg'];
}
if (type=='audio') {
var desc = Lang_AudioMP3;
exts = ['mp3'];
}
chrome.fileSystem.chooseEntry({
type: 'openFile',
accepts: [{
description: desc,
extensions: exts
}],
acceptsAllTypes: false
},
function(entry) {
if (chrome.runtime.lastError) {
return;
}
entry.file(function(file) {
var reader = new FileReader();
reader.onload = function() {
AddFileWind(type,reader.result);
}
reader.readAsDataURL(file);
});
});
}
function chrome_SelectImage() {
chrome.fileSystem.chooseEntry({
type: 'openFile',
accepts: [{
description: Lang_Images,
extensions: ['png','gif','jpg','jpeg']
}],
acceptsAllTypes: false
},
function(entry) {
if (chrome.runtime.lastError) {
return;
}
entry.file(function(file) {
var reader = new FileReader();
reader.onload = function() {
ThemeSetImage(reader.result);
}
reader.readAsDataURL(file);
});
});
}
function chrome_SelectFile(menu,merge) {
if (menu) {
var str = '';
str += "<div class='menu_item' id='item01'>"+Lang_FileSelect+"</div>";
str += "<div class='menu_item' id='item02'>"+Lang_MenuMergeStory+"</div>";
if (navigator.onLine) {
if (LangCode=='ru') {
str+= "<div class='menu_item' id='item03'>"+Lang_MenuAddInventory+"</div>";
}
str += "<div class='menu_item' id='item04'>"+Lang_LoadFromCloud+"</div>";
}
str += "<div class='menu_item' id='item05'>"+Lang_MenuFromBackUp+"</div>";
if (chrome_RecentFiles.length>0) {
str += "<hr>";
for (var i in chrome_RecentFiles) {
var item = chrome_RecentFiles[i];
if (item && typeof item=='object' && item.length==2) {
var name = item[0].split(':')[1].replace(/\.sm$/,'').trim();
str += "<div class='menu_item' forevent='"+item[0]+"\t"+item[1]+"'>"+name+"</div>";
}
}
}
mywindow_inner.innerHTML = str;
SetEvent('item01',function(){ShowWind(false);chrome_SelectFile(false,false);});
SetEvent('item02',function(){ShowWind(false);chrome_SelectFile(false,true);});
SetEvent('item03',function(){ShowWind(false);AreYouSure('AddInventory');});
SetEvent('item04',function(){ShowWind(false);SelectStory(false,true);});
SetEvent('item05',function(){ShowWind(false);FromBackup();});
SetEventsByAttr(17);
ShowWind(true);
} else {
chrome.fileSystem.chooseEntry({
type: 'openFile',
accepts: [{
extensions: ['sm']
}],
acceptsAllTypes: false
},
function(entry) {
chrome_LoadFile(entry,merge);
});
}
}
function chrome_LoadFile(entry,merge) {
if (chrome.runtime.lastError) {
return;
}
if (entry.file) {
Message(Lang_Loading);
entry.file(function(file) {
var reader = new FileReader();
reader.onload = function() {
if (!merge) {
chrome_SetCurrentEntry(entry);
}
LoadStory(0,merge,reader.result);
}
reader.readAsText(file);
},
function() {
chrome_ErrorLoadFile(entry);
});
} else {
chrome_ErrorLoadFile(entry);
}
}
function chrome_ErrorLoadFile(entry) {
Message(Lang_ErrorLoadFile);
NewGame(true);
for (var i in chrome_RecentFiles) {
var item = chrome_RecentFiles[i];
if (item && typeof item=='object' && item.length==2 && item[0]==chrome.fileSystem.retainEntry(entry)) {
chrome_RecentFiles.splice(i,1);
chrome_SaveRecent();
break;
}
}
}
function chrome_Save(com,data) {
if (com=='SaveStory') {
if (chrome_CurrentEntry) {
chrome_WriteFile(chrome_CurrentEntry,data);
ToBackup();
} else {
chrome_Save('SaveStoryDialog',data);
}
}
if (com=='SaveStoryDialog') {
var save = function(data) {
chrome.fileSystem.chooseEntry({
type: 'saveFile',
suggestedName: path,
accepts: [{
extensions: ['sm']
}],
acceptsAllTypes: false
},
function(entry) {
chrome_WriteFile(entry,data);
ToBackup();
});
}
var path = Lang_DefaultFileName+'.sm';
if (chrome_CurrentEntry) {
chrome.fileSystem.getDisplayPath(chrome_CurrentEntry,function(displayPath) {
path = displayPath;
save(data);
});
} else {
save(data);
}
}
}
function chrome_WriteFile(entry,data) {
if (chrome.runtime.lastError) {
return;
}
Message(Lang_Saving);
var blob = new Blob([data], {
type: 'text/plain',
endings: 'transparent'
});
entry.createWriter(function(writer) {
writer.onwrite = function(e) {
writer.onwrite = null;
writer.write(blob);
}
writer.onwriteend = function(e) {
chrome_SetCurrentEntry(entry);
Message(Lang_Saved);
};
writer.onerror = function(e) {
Message(Lang_StoryNotSaved);
};
writer.truncate(blob.size);
}, function(){Message(Lang_StoryNotSaved);});
}
function chrome_ToStorage(key,value,callback) {
var obj = {};
obj[key] = value;
chrome.storage.local.set(obj,callback);
}
function chrome_FromStorage(key,callback) {
chrome.storage.local.get(key,function(result) {
var value = result[key];
callback(value);
});
}
function chrome_GetDataURL(path,callback) {
if (chrome_FilesDirectory) {
chrome_FilesDirectory.getFile(
decodeURIComponent(path),
{},
function(entry) {
entry.file(
function(file) {
var reader = new FileReader();
reader.onload = function() {
callback(reader.result);
}
reader.onerror = function() {
callback(false);
}
reader.readAsDataURL(file);
},
function() {
callback(false);
}
);
},
function() {
callback(false);
}
);
} else {
callback(false);
}
}
function chrome_SetCurrentEntry(entry) {
chrome_CurrentEntry = entry;
if (chrome_CurrentEntry) {
var fileID = chrome.fileSystem.retainEntry(chrome_CurrentEntry);
var tmpArr = [];
for (var i in chrome_RecentFiles) {
var item = chrome_RecentFiles[i];
if (item && typeof item=='object' && item.length==2) {
item = item[0];
if (item.split(':')[1]!=fileID.split(':')[1]) {
tmpArr.push(chrome_RecentFiles[i]);
}
}
}
chrome_RecentFiles = tmpArr.slice();
var dirID = null;
if (chrome_FilesDirectory) {
dirID = chrome.fileSystem.retainEntry(chrome_FilesDirectory);
}
chrome_RecentFiles.unshift([fileID,dirID]);
var max = 8;
if (chrome_RecentFiles.length>max) {
chrome_RecentFiles = chrome_RecentFiles.slice(0,max);
}
chrome_SaveRecent();
var title = entry.name.replace(/\.sm$/,'').trim();
document.title = title+' | AXMA Story Maker';
} else {
chrome_CurrentEntry = null;
chrome_FilesDirectory = null;
document.title = Lang_DefaultFileName+' | AXMA Story Maker';
}
chrome_SetFilesDirInfo();
}
function chrome_LoadRecent() {
chrome_FromStorage('recent_files',function(data) {
if (data) {
chrome_RecentFiles = data;
if (chrome_RecentFiles.length>0) {
chrome_RestoreFile(chrome_RecentFiles[0][0],chrome_RecentFiles[0][1]);
} else {
NewGame();
}
} else {
NewGame();
}
});
}
function chrome_SaveRecent() {
chrome_ToStorage('recent_files',chrome_RecentFiles);
}
function chrome_RestoreFile(id,dirID) {
if (id) {
chrome_FilesDirectory = null;
chrome.fileSystem.isRestorable(id,function(isRestorable) {
if (isRestorable) {
chrome.fileSystem.restoreEntry(id,function(entry) {
chrome_CurrentEntry = entry;
chrome_LoadFile(chrome_CurrentEntry,false);
if (dirID) {
chrome.fileSystem.isRestorable(dirID,function(isRestorable) {
if (isRestorable) {
chrome.fileSystem.restoreEntry(dirID,function(entry) {
chrome_FilesDirectory = entry;
chrome_SetFilesDirInfo();
});
}
});
}
});
}
});
}
}
function chrome_PublishToLib(html,FilesLinks,locsCount,storyTitle) {
if (AuthLogin.trim()=='' || AuthPassword.trim()=='') {
SignInWindow(function() {
chrome_PublishToLib(html,FilesLinks,locsCount,storyTitle);
});
} else {
var xmlhttp = getXmlHttp();
var params = 'login='+encodeURIComponent(AuthLogin)+'&password='+encodeURIComponent(AuthPassword);
xmlhttp.open('POST',Lang_Server+'/user_files_asm.php',true);
xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
xmlhttp.onreadystatechange = function() {
if (xmlhttp.readyState==4) {
if(xmlhttp.status==200) {
var answer = xmlhttp.responseText.trim();
var str = '';
str += "<p>"+Lang_AboutPub+"</p>";
str += "<p><select id='item0' style='font-size:13px;margin-bottom:10px;'><option value=''>"+Lang_PubNew+"</option>";
if (answer) {
str += "<option disabled>─────────────────────────────</option>";
answer = answer.split('\n');
for (var i in answer) {
var id = answer[i].split('[:73143:]');
selected = '';
if (id[1].toLowerCase()==storyTitle.toLowerCase()) {
selected = ' selected';
}
str += "<option value='"+id[0]+"'"+selected+">"+id[1]+"</option>";
}
}
str += "</select></p>";
str += "<div style='margin-top:10px;'><span class='button' id='item1' style='float:left;'>"+Lang_Send+"</span><span class='button' id='item2' style='float:right;'>"+Lang_SignIn+"</span></div><div style='clear:both;'></div>";
mywindow_inner.innerHTML = str;
SetEvent('item1',function(){chrome_PublishSend(html,FilesLinks,locsCount,$('item0').value);});
SetEvent('item2',function(){SignInWindow(PublishToInet);});
ShowWind(true);
}
}
};
xmlhttp.send(params);
}
}
function chrome_PublishSend(html,FilesLinks,locsCount,id) {
if (id) {
var newPub = false;
var mess = Lang_WantUpdPub;
} else {
var newPub = true;
var mess = Lang_WantNewPub;
}
AreYouSure(
function() {
var post = function() {
var formData = new FormData();
var postData = function() {
Message(Lang_PleaseWait);
formData.append('login',AuthLogin);
formData.append('password',AuthPassword);
formData.append('files',FilesLinks.length);
formData.append('content',html);
formData.append('source',CreateSaveData(true));
formData.append('title',GetLocByTitle('StoryTitle').Text);
formData.append('subtitle',GetLocByTitle('StorySubtitle').Text);
formData.append('author',GetLocByTitle('StoryAuthor').Text);
formData.append('code','ASM_ChromeApp');
formData.append('lang',LangCode);
formData.append('passages',LocationArr.length);
if (id) {
formData.append('file_id',id);
}
var xmlhttp = getXmlHttp();
xmlhttp.open('POST',Lang_Server+'/post2.php',true);
xmlhttp.upload.onprogress = function(event) {
Message(Lang_Sending+'...'+Math.ceil(event.loaded/event.total*100)+'%');
}
xmlhttp.onreadystatechange = function() {
if (xmlhttp.readyState==4) {
if(xmlhttp.status==200) {
MessageHide();
var answer = xmlhttp.responseText.trim();
if (answer=='') {
AlertMessage(Lang_ErrorPub+'<br>'+Lang_ErrorReadCode+': 1');
} else if (answer.substr(0,5)=='error') {
AlertMessage(Lang_ErrorPub+'<br>'+Lang_ErrorReadCode+': '+answer.substr(5));
} else {
AlertMessage(Lang_SuccessPub+"<p></p><a href='"+answer+"' target='_blank'>"+answer+"</a>");
}
} else {
AlertMessage(Lang_ErrorPub+'<br>'+Lang_ErrorReadCode+': 2');
}
}
};
xmlhttp.send(formData);
}
if (FilesLinks.length>0) {
FilesLinks = ArraySortLen(FilesLinks);
FilesLinks.reverse();
var errorRead = function(code) {
MessageHide();
AlertMessage(Lang_ErrorReadMedia+'<br>'+FilesLinks[i]+'<br>'+Lang_ErrorReadCode+': '+code);
}
var totalSize = 0;
var i = 0;
var readFile = function() {
Message(Lang_ReadMedia+'...'+(i+1)+'/'+FilesLinks.length);
chrome_FilesDirectory.getFile(
decodeURIComponent(FilesLinks[i].replace(/^[^\/]+?\//,'')),
{},
function(entry) {
entry.file(
function(file) {
var reader = new FileReader();
reader.onload = function() {
var fileSize = reader.result.length;
if (fileSize>10*1024*1024) {
MessageHide();
AlertMessage(Lang_ErrorFileSize+'<br>'+FilesLinks[i]);
} else {
totalSize += fileSize;
if (totalSize<=20*1024*1024) {
formData.append('file'+(i+1),FilesLinks[i]);
formData.append('filecontent'+(i+1),reader.result);
i++;
if (i<FilesLinks.length) {
readFile();
} else {
postData();
}
} else {
MessageHide();
AlertMessage(Lang_ErrorTotalSize);
}
}
}
reader.onerror = function() {
errorRead(1);
}
reader.readAsBinaryString(file);
},
function() {
errorRead(2);
}
);
},
function() {
errorRead(3);
}
);
}
readFile();
} else {
postData();
}
};
if (FilesLinks.length>0 && !chrome_FilesDirectory) {
AlertMessage(Lang_NeedFilesDir,function() {
chrome_SelectFilesDir(function() {
post();
});
});
} else {
post();
}
},
mess
);
}
function chrome_SendMessage(s) {
previewWin.contentWindow.postMessage(s,'*');
}
function chrome_MessageListener() {
window.addEventListener('message', function(e) {
var com = e.data;
if (com=='ClosePreview') {
ClosePreview();
} else if (com=='CloseThemeEditor') {
if (WindowVisible) {
ShowWind(false);
} else {
ShowPlate(false);
}
} else if (/^PropertyEditor=/.test(com)) {
var id = com.replace(/^[^=]+?=/,'');
PropertyEditor(id);
} else if (/^GetDataURL=/.test(com)) {
if (!chrome_FilesDirectory) {
chrome_SendMessage("AlertMessage('"+Lang_NeedFilesDir+"');");
}
com = com.replace(/^[^=]+?=/,'');
if (com.substr(0,19)=='chrome-extension://') {
com = com.split('\t');
var path = com[0].split('/');
path.shift(); path.shift(); path.shift(); path.shift();
path = path.join('/');
if (!path) {
return;
}
var id = com[1];
chrome_GetDataURL(path,function(data) {
var type = 'image';
if (path.substr(-4)=='.mp3') {
type = 'audio';
}
if (!data) {
if (type=='audio') {
data = '';
} else {
data = style_badImage;
}
}
var com = "var elem=$('"+id+"');if(elem){elem.src='"+data+"';";
if (type!='audio') {
chrome_SendMessage(com+'}');
} else {
chrome_SendMessage(com+"playMusic();}");
}
});
}
} else if (/^GetSavesList=/.test(com)) {
com = com.replace(/^[^=]+?=/,'').split('&');
var key = com[0];
var save = com[1];
var i = 1;
var savesList = [];
var getslot = function() {
chrome_FromStorage(i+'_'+key,function(result) {
if (result) {
savesList.push('1');
} else {
savesList.push('');
}
i++;
if (i<=6) {
getslot();
} else {
chrome_SendMessage("SaveLoadWind("+save+","+JSON.stringify(savesList)+");");
}
});
}
getslot();
} else if (/^SaveStory=/.test(com)) {
com = com.replace(/^[^=]+?=/,'').split('&');
var slot = com[0];
var data = com[1];
chrome_ToStorage(slot,data,function() {
chrome_SendMessage('SaveSuccess();');
});
} else if (/^LoadStory=/.test(com)) {
com = com.replace(/^[^=]+?=/,'');
chrome_FromStorage(com,function(data) {
if (data) {
chrome_SendMessage("LoadStoryData('"+data+"');");
}
});
}
}, false);
}
function chrome_SetFilesDirInfo() {
if (!chrome_FilesDirectory) {
$('footer_info').innerHTML = Lang_MenuSetFilesDir;
$('footer_info').title = '';
} else {
chrome.fileSystem.getDisplayPath(chrome_FilesDirectory,function(displayPath) {
displayPath = displayPath.replace(/\\/g,'/');
$('footer_info').innerHTML = displayPath;
$('footer_info').title = displayPath;
});
}
}
function chrome_ChangeLanguage() {
if (LangCode=='en') {
var value = 'ru';
} else {
var value = 'en';
}
chrome_ToStorage('lang_code',value,function() {
chrome_ToStorage('win_state','maximized',function() {
chrome_ToStorage('first_start',false,function() {
chrome.runtime.reload();
});
});
});
}
