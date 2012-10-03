using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using DevExpress.CodeRush.Core;

namespace CR_ForceProjectConverter
{
  public partial class UnresolvedReferences : Form
  {
    public UnresolvedReferences()
    {
      InitializeComponent();
    }

    public UnresolvedReferences(List<string> unresolvedAssemblies)
    {
      InitializeComponent();

      listBoxControl1.Items.Clear();
      listBoxControl1.Items.AddRange(unresolvedAssemblies.ToArray());
    }

    private void simpleButton1_Click(object sender, EventArgs e)
    {
      string rootDirectory;

      if (Environment.Is64BitOperatingSystem)
      {
        rootDirectory = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\DevExpress\DXperience\v12.1\", "RootDirectory", null) as String;
      }
      else
      {
        rootDirectory = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\DevExpress\DXperience\v12.1\", "RootDirectory", null) as String;
      }

      if (String.IsNullOrEmpty(rootDirectory))
      {
        MessageBox.Show("Could not retreive the DXperience root folder.");
        return;
      }

      string projectConverterPath = Path.Combine(rootDirectory, @"Tools\DXperience\ProjectConverter.exe");

      if (!File.Exists(projectConverterPath))
      {
        MessageBox.Show("Could not find the ProjectConverter tool.");
        return;
      }

      string targetDirectoryForConversion = Path.GetDirectoryName(CodeRush.Source.ActiveSolution.FilePath);
      Process.Start(projectConverterPath, "\"" + targetDirectoryForConversion + "\"");
    }
  }
}
