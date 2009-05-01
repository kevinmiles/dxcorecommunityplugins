using System;
using System.Windows.Forms;

namespace CR_ReSharperCompatibility
{
  public class RedirectButton : Button
  {
    public string Command { get; set; }
    public string Parameters { get; set; }

    public RedirectButton(string command, string parameters)
    {
      Command = command;
      Parameters = parameters;
    }
    public RedirectButton()
    {

    }

  }
}
