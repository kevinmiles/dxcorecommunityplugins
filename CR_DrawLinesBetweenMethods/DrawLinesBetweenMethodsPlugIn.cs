using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;

namespace CR_DrawLinesBetweenMethods
{
	/// <summary>
	/// Summary description for DrawLinesBetweenMethodsPlugIn.
	/// </summary>
	public class DrawLinesBetweenMethodsPlugIn: StandardPlugIn
	{
		#region private fields...
		//private System.ComponentModel.Container components = null;
		#endregion

		// constructor...
		#region DrawLinesBetweenMethodsPlugIn
		public DrawLinesBetweenMethodsPlugIn()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
		}
		#endregion

		// CodeRush-generated code
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			loadSettings();
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// DrawLinesBetweenMethodsPlugIn
			// 
			this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.DrawLinesBetweenMethodsPlugIn_OptionsChanged);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
		#endregion

		private void DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement(DevExpress.CodeRush.Core.EditorPaintLanguageElementEventArgs ea)
		{
			if (ea.LanguageElement is Method || ea.LanguageElement is Property || ea.LanguageElement is Class)
			{		
				int line;                                        
				if(_drawLineAtEndOfMethod)
					line = ea.LanguageElement.Range.End.Line + 1;
				else
				{
					line = ea.LanguageElement.Range.Start.Line;

					// do we need to negotiate attributes or comments?
					LanguageElement prev = ea.LanguageElement.PreviousSibling;
					while(prev != null && 
						(prev.ElementType == LanguageElementType.Comment 
						|| prev.ElementType == LanguageElementType.XmlDocComment 
						|| prev.ElementType == LanguageElementType.AttributeSection ))
					{
						//Skip: Comment, AttributeSection, XmlDocComment
						//Debug.WriteLine("Skipping " + prev.ElementType + "  \"" + prev.ToString() + "\"");
						line = prev.Range.Start.Line;

						prev = prev.PreviousSibling;
					}
				}
				
				int leftPos;                                                        
				if(_fullWidth)
					leftPos = 0;
				else if (_drawLineAtEndOfMethod)
					leftPos = ea.PaintArgs.GetPoint(ea.LanguageElement.Range.End).X;
				else
					leftPos = ea.PaintArgs.GetPoint(ea.LanguageElement.Range.Start).X;
				
				Point linePos = ea.PaintArgs.GetPoint(line, 1);
				int yPos = linePos.Y;
				Point leftPt = new Point(leftPos, yPos);
				Point rightPt = new Point(ea.PaintArgs.TextView.Width, yPos);

				Rectangle rect = new Rectangle(leftPos, yPos, ea.PaintArgs.TextView.Width, 6);
				
				if (_drawShadow)
				{
					Color shadowColor = Color.FromArgb(0x33, _lineColor);
					using (LinearGradientBrush brush = new LinearGradientBrush(rect, shadowColor, Color.Transparent, 90, true))
					{
						ea.PaintArgs.Graphics.FillRectangle(brush, rect);
					}
				}
				
				using (Pen pen = new Pen(_lineColor, _lineWidth))
				{
					pen.DashStyle = _lineDashStyle;
					ea.PaintArgs.Graphics.DrawLine(pen, leftPt, rightPt);
				}
				
			}
		}


		bool _fullWidth = false;
		DashStyle _lineDashStyle = DashStyle.Solid;
		int _lineWidth = 1;
		Color _lineColor = Color.Silver;
		bool _drawLineAtEndOfMethod = false;
		bool _drawShadow = true;
		bool _enabled = true;


		private void DrawLinesBetweenMethodsPlugIn_OptionsChanged(DevExpress.CodeRush.Core.OptionsChangedEventArgs ea)
		{
			loadSettings();
		}

		void loadSettings()
		{
			using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
			{
				_fullWidth = storage.ReadBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
				_lineDashStyle = (DashStyle)storage.ReadEnum("DrawLinesBetweenMethods", "LineDashStyle", typeof(DashStyle), _lineDashStyle);
				_lineWidth = storage.ReadInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
				_lineColor = storage.ReadColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
				_drawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
				_drawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
				_enabled = storage.ReadBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);

				this.EditorPaintLanguageElement -= DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement;
				if (_enabled)
					this.EditorPaintLanguageElement += DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement;
			}
		}

	}
}