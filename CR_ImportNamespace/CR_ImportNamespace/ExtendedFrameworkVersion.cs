using DevExpress.CodeRush.StructuralParser;
namespace CR_ImportNamespace
{
  public enum ExtendedFrameworkVersion
  {
    Unknown = 0,
    Version10 = 1,
    Version11 = 2,
    Version20 = 3,
    Version30 = 4,
    Version35 = 5,
    Version40 = 6,

    Version35ClientProfile = 7,
    Version40ClientProfile = 8,

    Version30Silverlight = 9,
    Version40Silverlight = 10,

    Version40SilverlightWindowsPhone = 11
  }

  public static class ExtendedFrameworkVersionUtil
  {
    public static FrameworkVersion ToFrameworkVersion(ExtendedFrameworkVersion version)
    {
      if (version == ExtendedFrameworkVersion.Version35ClientProfile)
        return FrameworkVersion.Version35;

      if (version == ExtendedFrameworkVersion.Version40ClientProfile)
        return FrameworkVersion.Version40;

      return (FrameworkVersion)version;
    }

    public static ExtendedFrameworkVersion FromFrameworkVersion(FrameworkVersion version)
    {
      return (ExtendedFrameworkVersion)version;
    }

    public static ExtendedFrameworkVersion GetClientProfileVersion(ExtendedFrameworkVersion version)
    {
      if (version == ExtendedFrameworkVersion.Version35)
        return ExtendedFrameworkVersion.Version35ClientProfile;

      if (version == ExtendedFrameworkVersion.Version40)
        return ExtendedFrameworkVersion.Version40ClientProfile;

      return version;
    }

    public static ExtendedFrameworkVersion GetSilverlightVersion(ExtendedFrameworkVersion version)
    {
      if (version == ExtendedFrameworkVersion.Version30)
        return ExtendedFrameworkVersion.Version30Silverlight;

      if (version == ExtendedFrameworkVersion.Version40)
        return ExtendedFrameworkVersion.Version40Silverlight;

      return version;
    }

    public static ExtendedFrameworkVersion GetWindowsPhoneProfileVersion(ExtendedFrameworkVersion version)
    {
      if (version == ExtendedFrameworkVersion.Version40)
        return ExtendedFrameworkVersion.Version40SilverlightWindowsPhone;
      return version;
    }
  }
}