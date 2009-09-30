using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace DX_ContextLab
{
	/// <summary>
	/// DXCore plugin that displays the contexts that are currently fulfilled.
	/// Useful in debugging other plugins.
	/// </summary>
	[SubMenu("&Diagnostics")]
	[Title("Context Lab")]
	public partial class ContextWindow : ToolWindowPlugIn
	{
		/// <summary>
		/// Gets a value indicating if the tool window is enabled.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if the window is enabled/shown, <see langword="false" />
		/// if not.
		/// </value>
		public bool ToolWindowEnabled { get; private set; }

		/// <summary>
		/// Starts/stops the polling behavior based on the checkbox value.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="System.EventArgs" /> that contains the event data.</param>
		private void ContextPolling_CheckedChanged(object sender, EventArgs e)
		{
			this.InitializePolling();
		}

		/// <summary>
		/// Handles the hide event for the tool window.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ContextWindow_WindowHide(object sender, EventArgs e)
		{
			this.ToolWindowEnabled = false;
			this.DisablePolling();
		}

		/// <summary>
		/// Handles the show event for the window.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ContextWindow_WindowShow(object sender, EventArgs e)
		{
			this.ToolWindowEnabled = true;
			this.InitializePolling();
		}

		/// <summary>
		/// Updates the context list manually when the action executes.
		/// </summary>
		/// <param name="ea">An <see cref="DevExpress.CodeRush.Core.ExecuteEventArgs" /> that contains the event data.</param>
		private void EvaluateContextAction_Execute(ExecuteEventArgs ea)
		{
			this.UpdateContextList();
		}

		/// <summary>
		/// Determines if the manual context evaluation action is enabled.
		/// </summary>
		/// <param name="ea">An <see cref="DevExpress.CodeRush.Core.QueryStatusEventArgs" /> that contains the event data.</param>
		private void EvaluateContextAction_QueryStatus(QueryStatusEventArgs ea)
		{
			if (!this.ToolWindowEnabled)
			{
				ea.Status = EnvDTE.vsCommandStatus.vsCommandStatusUnsupported;
			}
			else
			{
				ea.Status = EnvDTE.vsCommandStatus.vsCommandStatusEnabled | EnvDTE.vsCommandStatus.vsCommandStatusSupported;
			}
		}

		/// <summary>
		/// Finalizes the plug in.
		/// </summary>
		public override void FinalizePlugIn()
		{
			base.FinalizePlugIn();
		}

		/// <summary>
		/// Initializes the plug in.
		/// </summary>
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			this.WindowHide += new EventHandler(ContextWindow_WindowHide);
			this.WindowShow += new EventHandler(ContextWindow_WindowShow);
		}

		/// <summary>
		/// Initializes the polling feature based on the state of the checkbox.
		/// </summary>
		public void InitializePolling()
		{
			if (this.contextPolling.Checked)
			{
				this.EnablePolling();
			}
			else
			{
				this.DisablePolling();
			}
		}

		/// <summary>
		/// Enables the polling feature for checking satisfied contexts.
		/// </summary>
		public void EnablePolling()
		{
			this.pollingTimer.Enabled = true;
		}

		/// <summary>
		/// Disables the polling feature for checking satisfied contexts.
		/// </summary>
		public void DisablePolling()
		{
			this.pollingTimer.Enabled = false;
		}

		/// <summary>
		/// Updates the context list when the timer ticks.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void PollingTimer_Tick(object sender, EventArgs e)
		{
			this.UpdateContextList();
		}

		/// <summary>
		/// Updates the list of contexts that are currently fulfilled.
		/// </summary>
		public void UpdateContextList()
		{
			if (!this.ToolWindowEnabled)
			{
				return;
			}
			this.contextList.Items.Clear();
			ContextServices currentContext = CodeRush.Context;
			string[] allContexts = currentContext.GetAllContextPaths("All");
			List<string> satisfied = new List<string>();
			foreach (string context in allContexts)
			{
				if (currentContext.Satisfied(context, false))
				{
					satisfied.Add(context);
				}
			}
			this.contextList.Items.AddRange(satisfied.ToArray());
		}
	}
}