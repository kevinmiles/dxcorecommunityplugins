namespace CR_Loop
{
	partial class PlugIn1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public PlugIn1()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            DevExpress.CodeRush.Core.Parameter parameter1 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.IntegerParameterType());
            DevExpress.CodeRush.Core.Parameter parameter2 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
            DevExpress.CodeRush.Core.Parameter parameter3 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.IntegerParameterType());
            DevExpress.CodeRush.Core.Parameter parameter4 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.IntegerParameterType());
            DevExpress.CodeRush.Core.Parameter parameter5 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
            this.txtCmdLoop = new DevExpress.CodeRush.Core.TextCommand(this.components);
            this.strLoopIterationValue = new DevExpress.CodeRush.Extensions.StringProvider(this.components);
            this.LoopLiteral = new DevExpress.CodeRush.Extensions.StringProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strLoopIterationValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopLiteral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // txtCmdLoop
            // 
            this.txtCmdLoop.CommandName = "Loop";
            this.txtCmdLoop.Description = "Calls a specified template a specified number of times.";
            parameter1.DefaultValue = 0;
            parameter1.Description = "The number of times to loop.";
            parameter1.Name = "Count";
            parameter1.Optional = false;
            parameter2.DefaultValue = "";
            parameter2.Description = "The template to expand once for each iteration through the loop.";
            parameter2.Name = "Template";
            parameter2.Optional = false;
            this.txtCmdLoop.Parameters.Add(parameter1);
            this.txtCmdLoop.Parameters.Add(parameter2);
            this.txtCmdLoop.Execute += new DevExpress.CodeRush.Core.ExecuteEventHandler(this.txtCmdLoop_Execute);
            // 
            // strLoopIterationValue
            // 
            this.strLoopIterationValue.Description = "Gets the value of the count for the Loop TextCommand";
            parameter3.DefaultValue = 0;
            parameter3.Description = "The offset to add to the counter value.";
            parameter3.Name = "Offset";
            parameter3.Optional = true;
            this.strLoopIterationValue.Parameters.Add(parameter3);
            this.strLoopIterationValue.ProviderName = "LoopIterationValue";
            this.strLoopIterationValue.Register = true;
            this.strLoopIterationValue.GetString += new DevExpress.CodeRush.Core.GetStringEventHandler(this.strLoopIterationValue_GetString);
            // 
            // LoopLiteral
            // 
            this.LoopLiteral.Description = "";
            parameter4.DefaultValue = 0;
            parameter4.Description = null;
            parameter4.Name = "Count";
            parameter4.Optional = false;
            parameter5.DefaultValue = "";
            parameter5.Description = null;
            parameter5.Name = "Text";
            parameter5.Optional = false;
            this.LoopLiteral.Parameters.Add(parameter4);
            this.LoopLiteral.Parameters.Add(parameter5);
            this.LoopLiteral.ProviderName = "LoopLiteral";
            this.LoopLiteral.Register = true;
            this.LoopLiteral.GetString += new DevExpress.CodeRush.Core.GetStringEventHandler(this.LoopLiteral_GetString);
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strLoopIterationValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopLiteral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.TextCommand txtCmdLoop;
        private DevExpress.CodeRush.Extensions.StringProvider strLoopIterationValue;
        private DevExpress.CodeRush.Extensions.StringProvider LoopLiteral;
	}
}