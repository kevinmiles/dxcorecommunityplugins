using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CR_ReSharperCompatibility
{
  public partial class FrmResharperCompatibility : Form
  {
    #region private fields...
    private string _Command;
    private bool _PersistResponse;
    private CompatibilityResult _Result;
    private string _Parameters;
    #endregion

    // private methods...
    #region AddRedirectControls
    private void AddRedirectControls(Redirects redirects)
    {
      const int INT_TopMargin = 5;
      const int INT_SpaceBetweenButtons = 4;

      int yPos = lblOptions.Bottom + INT_TopMargin;

      int longestButtonTextWidth = 0;
      foreach (CompatibilityRedirect redirect in redirects)
      {
        Graphics graphics = Graphics.FromHwnd(Handle);
        SizeF rect = graphics.MeasureString(redirect.ButtonText, Font);
        int thisWidth = (int)Math.Ceiling(rect.Width);
        if (thisWidth > longestButtonTextWidth)
          longestButtonTextWidth = thisWidth;
      }
      const int INT_ExplanationLabelMargin = 8;   // Distance between disabled button and explaning label.
      int xPos = 8;
      int buttonWidth = longestButtonTextWidth + 16;
      int longestWidth = buttonWidth;
      foreach (CompatibilityRedirect redirect in redirects)
      {
        RedirectButton button = new RedirectButton(redirect.Command, redirect.Parameters);
        pnlOptions.Controls.Add(button);
        button.Text = redirect.ButtonText;
        button.Enabled = redirect.IsAvailable;
        button.Left = xPos;
        button.Width = buttonWidth;
        
        toolTip1.SetToolTip(button, redirect.Description);
        button.Top = yPos;
        button.Click += new EventHandler(redirectButton_Click);

        if (!redirect.IsAvailable)
        {
          Label explanationLabel = new Label();
          pnlOptions.Controls.Add(explanationLabel);
          explanationLabel.Top = yPos;
          explanationLabel.Left = button.Right + INT_ExplanationLabelMargin;
          explanationLabel.AutoSize = true;
          explanationLabel.Text = redirect.AvailabilityHint;
          explanationLabel.ForeColor = SystemColors.GrayText;
          if (longestWidth < explanationLabel.Right)
            longestWidth = explanationLabel.Right;
        }

        yPos += button.Height + INT_SpaceBetweenButtons;
      }
      pnlOptions.Height = yPos;
      pnlOptions.Width = longestWidth;
    }
    #endregion
    #region ClearFields
    private void ClearFields()
    {
      _Command = String.Empty;
      _Parameters = String.Empty;
      _Result = CompatibilityResult.Close;
      _PersistResponse = false;
    }
    #endregion

    // constructors...
    #region FrmResharperCompatibility(string message, Redirects redirects, bool allowPersistResponse)
    public FrmResharperCompatibility(string title, string message, Redirects redirects, bool allowPersistResponse)
    {
      InitializeComponent();

      ClearFields();

      int optionsHeight = 0;
      int optionsWidth = 0;
      
      if (redirects != null)
      {
        AddRedirectControls(redirects);
        chkAlwaysPerformThisAction.Visible = allowPersistResponse;
        optionsHeight = pnlOptions.Height;
        optionsWidth = pnlOptions.Width;
        pnlOptions.Dock = DockStyle.Fill;
      }
      else
      {
        pnlOptions.Visible = false;
        chkAlwaysPerformThisAction.Visible = false;
      }

      if (optionsWidth > ClientSize.Width)
        Width += optionsWidth - ClientSize.Width;

      lblTitle.Text = title;
      lblCompatibilityNote.Text = message;

      int desiredHeight = pnlTop.Height + optionsHeight + pnlBottom.Height;
      int deltaHeight = desiredHeight - ClientSize.Height;

      Height += deltaHeight;
      MinimumSize = new Size(Width, Height);
    }
    #endregion
    #region FrmResharperCompatibility()
    public FrmResharperCompatibility()
    {
      InitializeComponent();
    }
    #endregion

    // event handlers...
    #region redirectButton_Click
    void redirectButton_Click(object sender, EventArgs e)
    {
      RedirectButton redirectButton = sender as RedirectButton;
      if (redirectButton == null)
        return;
      _Command = redirectButton.Command;
      _Parameters = redirectButton.Parameters;
      _Result = CompatibilityResult.ExecuteCommand;
      _PersistResponse = chkAlwaysPerformThisAction.Visible && chkAlwaysPerformThisAction.Checked;
      Close();
    }
    #endregion

    // public properties...
    #region Command
    public string Command
    {
      get
      {
        return _Command;
      }
    }
    #endregion
    #region Parameters
    public string Parameters
    {
      get
      {
        return _Parameters;
      }
    }
    #endregion
    #region PersistResponse
    public bool PersistResponse
    {
      get
      {
        return _PersistResponse;
      }
    }
    #endregion
    #region Result
    public CompatibilityResult Result
    {
      get
      {
        return _Result;
      }
    }
    #endregion

    private void btnClose_Click(object sender, EventArgs e)
    {
      _Result = CompatibilityResult.Close;
      Close();
    }

    private void lblCompatibilityNote_ContentsResized(object sender, ContentsResizedEventArgs e)
    {
      RichTextBox richTextBox = sender as RichTextBox;
      if (richTextBox == null)
        return;
      //richTextBox.SuspendLayout();
      int newHeight = e.NewRectangle.Height + 5;
      pnlTop.Height = richTextBox.Top + newHeight;
      richTextBox.Height = newHeight;
      
      //richTextBox.ResumeLayout();
      int deltaHeight = richTextBox.Height - newHeight;
      Height += deltaHeight;
    }
  }
}
