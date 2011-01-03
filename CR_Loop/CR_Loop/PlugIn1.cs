using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Loop
{
	public partial class PlugIn1 : StandardPlugIn
	{
		int _Counter;
    // DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			//
			// TODO: Add your initialization code here.
			//
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		private void txtCmdLoop_Execute(ExecuteTextCommandEventArgs ea)
		{
			int count = txtCmdLoop.Parameters["Count"].ValueAsInt;
			string template = txtCmdLoop.Parameters["Template"].ValueAsStr;
			_Counter = 0;
			CodeRush.Source.BeginUpdate();
			try
			{
				for (int i = 0; i < count; i++)
				{
					_Counter = i;
					CodeRush.Templates.ExpandTemplate(template);
				}
			}
			finally
			{
				CodeRush.Source.EndUpdate();
			}
		}

		private void strLoopIterationValue_GetString(GetStringEventArgs ea)
		{
			int offset = strLoopIterationValue.Parameters["Offset"].ValueAsInt;
			ea.Value = (_Counter + offset).ToString();
		}
	}
}