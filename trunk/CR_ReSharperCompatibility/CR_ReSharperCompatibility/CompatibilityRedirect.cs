using System;

namespace CR_ReSharperCompatibility
{
  public class CompatibilityRedirect
  {
    public string ButtonText { get; set; }
    public string Command { get; set; }
    public string Parameters { get; set; }
    public string Description { get; set; }
    public string AvailabilityHint { get; set; }
    public bool IsAvailable { get; set; }
    public string Shortcut { get; set; }

    public CompatibilityRedirect(string buttonText, string command, string parameters, string description, string availabiltyHint, bool isAvailable)
      : this(buttonText, command, parameters, description, String.Empty, availabiltyHint, isAvailable)
    {
    }
    public CompatibilityRedirect(string buttonText, string command, string parameters, string description, string shortcut, string availabiltyHint, bool isAvailable)
    {
      ButtonText = buttonText;
      Command = command;
      Parameters = parameters;
      Description = description;
      AvailabilityHint = availabiltyHint;
      IsAvailable = isAvailable;
    }
  }
}
