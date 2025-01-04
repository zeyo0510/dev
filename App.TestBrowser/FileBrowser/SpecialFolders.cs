namespace FileBrowser {
  /// <summary>
  /// This enum is used to indicate which folder should be opened when initialising the browser.
  /// When 'Other' is chosen, the browser will start in the directory given by the StartUpDirectoryOther
  /// property.
  /// </summary>
  public enum SpecialFolders : uint {
    UserProfiles = 0x003e,
    DesktopDir_All = 0x0019,
    ApplicationData_All = 0x0023,
    MyDocuments_All = 0x002e,
    MyFavorites_All = 0x001f,
    MyMusic_All = 0x0035,
    MyPictures_All = 0x0036,
    StartMenu_All = 0x0016,
    MyVideos_All = 0x0037,

    Desktop = 0x0000,
    DekstopDir = 0x0010,
    MyComputer = 0x0011,
    MyFavorites = 0x0006,
    ApplicationData = 0x001c,
    MyDocuments = 0x000c,
    MyMusic = 0x000d,
    MyPictures = 0x0027,
    MyVideos = 0x000e,
    MyNetworkPlaces = 0x0012,
    MyDocumentsDir = 0x0005,
    StartMenu = 0x000b,

    ControlPanel = 0x0003,
    Printers = 0x0004,
    ProgramFiles = 0x0026,
    SendTo = 0x0009,
    System = 0x0025,
    Windows = 0x0024,
    RecycleBin = 0x000a,

    Other = 0x1000
  }
}