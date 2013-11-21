using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Diagnostics.ToolWindows;
using DevExpress.CodeRush.PlugInCore;

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
		/// <remarks>
		/// <para>
		/// If any exception occurs while evaluating a context, the exception will
		/// be logged and it will be assumed that context is not fulfilled.
		/// </para>
		/// </remarks>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching the exception so it can be logged.")]
		public void UpdateContextList()
		{
			if (!this.ToolWindowEnabled)
			{
				return;
			}
			this.contextList.BeginUpdate();
			ContextServices currentContext = CodeRush.Context;
			string[] allContexts = currentContext.GetAllContextPaths("All");
			currentContext.BeginCheck();
			foreach (string context in allContexts)
			{
				try
				{
					if (context == "System\\Table Dimensions Are Set")
					{
						// This specific context has a side-effect where if
						// it isn't satisfied, a table dimension picker pops
						// up and locks up the IDE.
						continue;
					}
					if (currentContext.Satisfied(context, false))
					{
						if (!this.contextList.Items.Contains(context))
						{
							this.contextList.Items.Add(context);
						}
					}
					else
					{
						this.contextList.Items.Remove(context);
					}
				}
				catch (Exception e)
				{
					// For now, just log the error. In a future version, it might
					// be interesting to use a more robust control than a list box
					// and display contexts with problems in red or something.
					Log.SendException(String.Format(CultureInfo.InvariantCulture, "Exception while evaluating context '{0}'", context), e);
				}
			}
			currentContext.EndCheck();
			this.contextList.EndUpdate();
		}
	}
}