﻿namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;

    internal class SA1401CodeIssue : ICodeIssue
    {
        private string[] modifiers = new string[] { "public", "internal", "protected" };

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            if (violation.Element == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }
            SourcePoint? startPoint = null;
            SourcePoint? endPoint = null;
            foreach (var token in violation.Element.ElementTokens.Where(t => t.LineNumber >= violation.Line))
            {
                if (string.IsNullOrEmpty(token.Text.Trim()))
                {
                    continue;
                }
                if (modifiers.Contains(token.Text))
                {
                    if (startPoint == null)
                    {
                        startPoint = new SourcePoint(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1);
                        endPoint = new SourcePoint(token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                        continue;
                    }
                    else
                    {
                        endPoint = new SourcePoint(token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                        continue;
                    }
                }
                SourceRange sourceRange = new SourceRange(startPoint.Value, endPoint.Value);
                ea.AddSmell(sourceRange, message, 10);
                return;
            }
        }
    }
}