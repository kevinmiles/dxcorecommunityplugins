using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_NavigateToDefinition
{
  public partial class OptNavigateToDefinition : OptionsPage
  {
    public OptNavigateToDefinition()
    {
      InitializeComponent();
    }

    private void OptNavigateToDefinition_PreparePage(object sender, OptionsPageStorageEventArgs ea)
    {
      loadSettings();
    }

    private void OptNavigateToDefinition_CommitChanges(object sender, CommitChangesEventArgs ea)
    {
      saveSettings();
    }

    private bool enabled = true;
    private bool dropMarker = true;
    private bool showBeacon = true;
    private bool useGoToDef = true;

    void loadSettings()
    {
      try
      {
        using (DecoupledStorage storage = OptNavigateToDefinition.Storage)
        {
          enabled = storage.ReadBoolean("NavigateToDefinition", "Enabled", true);
          dropMarker = storage.ReadBoolean("NavigateToDefinition", "DropMarker", true);
          showBeacon = storage.ReadBoolean("NavigateToDefinition", "ShowBeacon", true);
          useGoToDef = storage.ReadBoolean("NavigateToDefinition", "UseGoToDef", true);
        }

        chkEnabled.Checked = enabled;
        chkDropMarker.Checked = dropMarker;
        chkShowBeacon.Checked = showBeacon;
        chkUseGoToDef.Checked = useGoToDef;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }

    }

    void saveSettings()
    {
      try
      {
        enabled = chkEnabled.Checked;
        dropMarker = chkDropMarker.Checked;
        showBeacon = chkShowBeacon.Checked;
        useGoToDef = chkUseGoToDef.Checked;

        using (DecoupledStorage storage = OptNavigateToDefinition.Storage)
        {
          storage.WriteBoolean("NavigateToDefinition", "Enabled", enabled);
          storage.WriteBoolean("NavigateToDefinition", "DropMarker", dropMarker);
          storage.WriteBoolean("NavigateToDefinition", "ShowBeacon", showBeacon);
          storage.WriteBoolean("NavigateToDefinition", "UseGoToDef", useGoToDef);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private void OptNavigateToDefinition_Load(object sender, EventArgs e)
    {
      // Enable controls with Enabled checkbox
      mainPanel.DataBindings.Add("Enabled", chkEnabled, "Checked");
    }
  }
}