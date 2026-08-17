// https://github.com/naudio/NAudio/blob/main/src/NAudio.Wasapi/CoreAudioApi/PropertyKeys.cs
/************************************************/
namespace AudioCore
{
  public static class PKEY
  {
    public static readonly PropertyKey PKEY_AudioEndpoint_FormFactor = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 0);

    public static readonly PropertyKey PKEY_AudioEndpoint_ControlPanelPageProvider = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 1);

    public static readonly PropertyKey PKEY_AudioEndpoint_Association = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 2);

    public static readonly PropertyKey PKEY_AudioEndpoint_PhysicalSpeakers = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 3);

    public static readonly PropertyKey PKEY_AudioEndpoint_GUID = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 4);

    public static readonly PropertyKey PKEY_AudioEndpoint_Disable_SysFx = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 5);

    public static readonly PropertyKey PKEY_AudioEndpoint_FullRangeSpeakers = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 6);

    public static readonly PropertyKey PKEY_AudioEndpoint_Supports_EventDriven_Mode = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 7);

    public static readonly PropertyKey PKEY_AudioEndpoint_JackSubType = new(new Guid("1DA5D803-D492-4EDD-8C23-E0C0FFEE7F0E"), 8);

    public static readonly PropertyKey PKEY_AudioEngine_DeviceFormat = new(new Guid(-241236403, 2092, 20007, 188, 115, 104, 130, 161, 187, 142, 76), 0);

    public static readonly PropertyKey PKEY_AudioEngine_OEMFormat = new(new Guid(-460911066, 15557, 19666, 186, 70, 202, 10, 154, 112, 237, 4), 3);

    public static readonly PropertyKey PKEY_DeviceInterface_FriendlyName = new(new Guid("026E516E-B814-414B-83CD-856D6FEF4822"),  2);
    public static readonly PropertyKey PKEY_Device_FriendlyName          = new(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"), 14);
    public static readonly PropertyKey PKEY_Device_DeviceDesc            = new(new Guid("A45C254E-DF1C-4EFD-8020-67D146A850E0"),  2);
    public static readonly PropertyKey PKEY_DeviceClass_IconPath         = new(new Guid("259ABFFC-50A7-47CE-AF08-68C9A7D73366"), 12);
  }
}
