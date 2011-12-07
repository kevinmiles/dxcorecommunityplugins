using System.Windows.Forms;
using System;

namespace CR_ImportNamespace
{
  public partial class FrmCollectingClasses : Form, IAssemblyScanProgress
	{
		public FrmCollectingClasses()
		{
			InitializeComponent();
		}
		
		public string PerformingScanText
		{
      get { return lblPerformingScan.Text; }
      set { lblPerformingScan.Text = value; }
		}
    public int Maximum
    {
      get { return progress.Maximum; }
      set { progress.Maximum = value; }
    }
    public int Value
    {
      get { return progress.Value; }
      set
      {
        if (progress.Value == value)
          return;
        progress.Value = value;
        if (IsHandleCreated)
          progress.Refresh();
      }
    }
    public string StatusText
    {
      get { return lblScanStatus.Text; }
      set
      {
        if (lblScanStatus.Text == value)
          return;
        lblScanStatus.Text = value;
        if (IsHandleCreated)
          lblScanStatus.Refresh();
      }
    }

    public void Start(int fileCount)
    {
      Maximum = fileCount;
    }

    public void UpdateProgress(int fileIndex, string text)
    {
      StatusText = String.Format("Scanning {0}", text);
      Value = fileIndex;
    }

    public void Stop()
    {
      Close();
      Dispose();
    }
  }
}