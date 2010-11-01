using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Adornments;
using Platform = DevExpress.DXCore.Platform.Drawing;

namespace UnitTestErrorVisualizer
{
	public interface IRender
	{
		void Render(Platform.IDrawingSurface context, Platform.Rect baseDrawingRect);
	}
	public class EnhancedRangeArrow1 : RangeArrow
	{
		IRender extraDrawing;
		public EnhancedRangeArrow1(SourceRange start, SourceRange end, IRender extraDrawing)
			: base(start, end)
		{
			this.extraDrawing = extraDrawing;
		}

		protected override CodeViewAdornment CreateCodeViewAdornment(TextView textView)
		{
			EnhancedRangeArrowDocAdornment1 adornment = new EnhancedRangeArrowDocAdornment1(StartRange, EndRange, Color, extraDrawing);
			return (adornment.CreateTextViewAdornment(textView) as CodeViewAdornment);
		}
	}
	public class EnhancedRangeArrowDocAdornment1 : RangeArrowDocAdornment
	{
		IRender extraDrawing;
		Platform.Color localCopyColor;	//The color should probably be changed to protected so I don't need this hack
		public EnhancedRangeArrowDocAdornment1(SourceRange startRange, SourceRange endRange, Platform.Color color, IRender extraDrawing)
			: base(startRange, endRange, color)
		{
			this.extraDrawing = extraDrawing;
			localCopyColor = color;
		}
		protected override TextViewAdornment NewAdornment(string feature, IElementFrame binding)
		{
			EnhancedRangeArrowAdornment1 adornment = new EnhancedRangeArrowAdornment1(binding, extraDrawing);
			adornment.Color = localCopyColor;
			return adornment;
		}
	}
	public class EnhancedRangeArrowAdornment1 : RangeArrowAdornment
	{
		IRender extraDrawing;
		public EnhancedRangeArrowAdornment1(IElementFrame binding, IRender extraDrawing)
			: base(binding)
		{
			this.extraDrawing = extraDrawing;
		}
		public override void Render(Platform.IDrawingSurface context)
		{
			base.Render(context);
			extraDrawing.Render(context, GetViewElementFrameInfo().ElementFrameGeometry.ToLocation().Bounds);
		}
	}
	public class EnhancedRangeArrow2 : RangeArrow
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		public EnhancedRangeArrow2(SourceRange start, SourceRange end, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(start, end)
		{
			this.extraDrawing = extraDrawing;
		}

		protected override CodeViewAdornment CreateCodeViewAdornment(TextView textView)
		{
			EnhancedRangeArrowDocAdornment2 adornment = new EnhancedRangeArrowDocAdornment2(StartRange, EndRange, Color, extraDrawing);
			return (adornment.CreateTextViewAdornment(textView) as CodeViewAdornment);
		}
	}
	public class EnhancedRangeArrowDocAdornment2 : RangeArrowDocAdornment
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		Platform.Color localCopyColor;	//The color should probably be changed to protected so I don't need this hack
		public EnhancedRangeArrowDocAdornment2(SourceRange startRange, SourceRange endRange, Platform.Color color, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(startRange, endRange, color)
		{
			this.extraDrawing = extraDrawing;
			localCopyColor = color;
		}
		protected override TextViewAdornment NewAdornment(string feature, IElementFrame binding)
		{
			EnhancedRangeArrowAdornment2 adornment = new EnhancedRangeArrowAdornment2(binding, extraDrawing);
			adornment.Color = localCopyColor;
			return adornment;
		}
	}
	public class EnhancedRangeArrowAdornment2 : RangeArrowAdornment
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		public EnhancedRangeArrowAdornment2(IElementFrame binding, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(binding)
		{
			this.extraDrawing = extraDrawing;
		}
		public override void Render(Platform.IDrawingSurface context)
		{
			base.Render(context);
			extraDrawing(context, GetViewElementFrameInfo().ElementFrameGeometry.ToLocation().Bounds);
		}
	}
}
