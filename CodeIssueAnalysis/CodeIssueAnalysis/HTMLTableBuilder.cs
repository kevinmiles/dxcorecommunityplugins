using System;
using System.Data;
using System.Text;
using DevExpress.DXCore.Controls.XtraGrid.Columns;
using DevExpress.DXCore.Controls.XtraGrid.Views.Grid;

namespace CodeIssueAnalysis
{
    class HTMLTableBuilder
    {
        readonly GridView view;
        /// <summary>
        /// Initializes a new instance of the HTMLTableBuilder class.
        /// </summary>
        public HTMLTableBuilder(GridView view)
        {
            this.view = view;
        }

        public string BuildHTMLTable(string colour)
        {

            StringBuilder table = new StringBuilder("");

            //builds the start of the table and css style
            BuildHTMLHeader(table, colour);

            int rowHandle;
            DataRow gridRow;
            for (int i = 0; i < view.RowCount; i++)
            {
                rowHandle = view.GetVisibleRowHandle(i);
                if (!view.IsGroupRow(rowHandle))
                {
                    gridRow = BuildHTMLNormalRow(table, rowHandle);
                }
                else
                {
                    BuildHTMLGroupRow(colour, table, rowHandle);
                }
            }

            table.AppendLine("</body></html>");
            return table.ToString();
        }

        private DataRow BuildHTMLNormalRow(StringBuilder table, int rowHandle)
        {
            DataRow gridRow = view.GetDataRow(rowHandle);

            table.AppendLine("<tr>");

            for (int k = 0; k < view.GetRowLevel(rowHandle); k++)
                table.AppendLine("<td></td>");

            foreach (GridColumn col in view.VisibleColumns)
            {
                if (!view.GroupedColumns.Contains(col))
                    table.Append(String.Format("<td>{0}</td>", gridRow.Field<object>(col.Caption)));
            }

            table.AppendLine("</tr>");
            return gridRow;
        }

        private void BuildHTMLHeader(StringBuilder table, string colour)
        {
            table.AppendLine(String.Format(@"<html><head>
                                         <style type='text/css'>
                                         body {{
                                             font-family:Calibri, Verdana, Arial, Helvetica, sans-serif;
                                             font-size:x-small;
                                         }}
                             
                             			table,th, td
                             			{{
                                             border-collapse:collapse;
                             				border: 1px solid {0};
                             			}}
                                         </style>
                                         </head>
                                         <body>
                                         <table><tr>", colour));

            for (int i = 0; i < view.GroupedColumns.Count; i++)
                table.AppendLine(String.Format("<td bgcolor='{0}'>&nbsp;&nbsp;&nbsp;</td>", colour));

            foreach (GridColumn col in view.VisibleColumns)
            {
                if (!view.GroupedColumns.Contains(col))
                    table.AppendLine(String.Format("<td bgcolor='{0}'><strong>{1}</strong></td>", colour, col.Caption));
            }

            table.AppendLine("</tr>");
        }

        private void BuildHTMLGroupRow(string colour, StringBuilder table, int rowHandle)
        {
            table.AppendLine("<tr>");

            // int tmp = view.GetRowLevel(rowHandle);
            for (int k = 0; k < view.GetRowLevel(rowHandle); k++)
                table.AppendLine("<td>&nbsp;&nbsp;&nbsp;</td>");

            int span = view.VisibleColumns.Count - view.GetRowLevel(rowHandle) + view.GroupedColumns.Count;

            table.AppendLine(String.Format("<td bgcolor='" + colour + "' colspan='{0}'><strong>{1}<strong></td></tr>", span, view.GetGroupRowDisplayText(rowHandle)));
        }


    }
}
