using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_ExtractHqlNamedQuery
{
  public partial class OptExtractHqlNamedQuery : OptionsPage
  {
    public OptExtractHqlNamedQuery()
    {
      InitializeComponent();
      comboFindHqlFileStrategy.SelectedIndex = 0;
    }

    private void OptExtractHqlNamedQuery_PreparePage(object sender, OptionsPageStorageEventArgs ea)
    {
      loadSettings();
    }

    private void OptExtractHqlNamedQuery_CommitChanges(object sender, CommitChangesEventArgs ea)
    {
      saveSettings();
    }

    private bool enabled = true;
    private int findHqlFileStrategy = 0;
    private string hqlNamedQueryFileName = "NamedQueries.hbm.xml";

    void loadSettings()
    {
      try
      {
        using (DecoupledStorage storage = OptExtractHqlNamedQuery.Storage)
        {
          enabled = storage.ReadBoolean("ExtractHqlNamedQuery", "Enabled", true);
          findHqlFileStrategy = storage.ReadInt32("ExtractHqlNamedQuery", "FindHqlFileStrategy", 0);
          hqlNamedQueryFileName = storage.ReadString("ExtractHqlNamedQuery", "HqlNamedQueryFileName", "NamedQueries.hbm.xml");
        }

        chkEnabled.Checked = enabled;
        comboFindHqlFileStrategy.SelectedIndex = findHqlFileStrategy;
        textHqlNamedQueryFileName.Text = hqlNamedQueryFileName;
        
      }
      catch (Exception ex)
      {
        ExtractHqlNamedQuery.ShowException(ex);
      }

    }

    void saveSettings()
    {
      try
      {
        if (chkEnabled.Checked && string.IsNullOrEmpty(textHqlNamedQueryFileName.Text))
          throw new Exception("Hql Named Queries File Name cannot be empty");

        enabled = chkEnabled.Checked;
        findHqlFileStrategy = comboFindHqlFileStrategy.SelectedIndex;
        hqlNamedQueryFileName = textHqlNamedQueryFileName.Text;

        using (DecoupledStorage storage = OptExtractHqlNamedQuery.Storage)
        {
          storage.WriteBoolean("ExtractHqlNamedQuery", "Enabled", enabled);
          storage.WriteInt32("ExtractHqlNamedQuery", "FindHqlFileStrategy", findHqlFileStrategy);
          storage.WriteString("ExtractHqlNamedQuery", "HqlNamedQueryFileName", hqlNamedQueryFileName);
        }
      }
      catch (Exception ex)
      {
        ExtractHqlNamedQuery.ShowException(ex);
      }
    }

    private void OptExtractHqlNamedQuery_Load(object sender, EventArgs e)
    {
      // Enable controls with Enabled checkbox
      mainPanel.DataBindings.Add("Enabled", chkEnabled, "Checked");
    }
  }
}