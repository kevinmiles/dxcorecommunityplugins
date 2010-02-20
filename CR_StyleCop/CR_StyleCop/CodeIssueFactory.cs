namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using CR_StyleCop.CodeIssues;
    using Microsoft.StyleCop;

    internal class CodeIssueFactory
    {
        private Dictionary<string, ICodeIssue> handlers = new Dictionary<string, ICodeIssue>();
        private ICodeIssue nullHandler = new NullCodeIssue();

        public CodeIssueFactory()
        {
            ElementByNameCodeIssue markElementByItsNameCodeIssue = new ElementByNameCodeIssue();
            this.handlers.Add("SA1600", new SA1600CodeIssue()); // OK
            this.handlers.Add("SA1601", new SA1601CodeIssue()); // OK
            this.handlers.Add("SA1602", new SA1602CodeIssue()); // OK
            this.handlers.Add("SA1603", new SA1603CodeIssue()); // OK
            this.handlers.Add("SA1604", new SA1604CodeIssue()); // OK
            this.handlers.Add("SA1605", markElementByItsNameCodeIssue); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1606", new SA1606CodeIssue()); // OK
            this.handlers.Add("SA1607", new SA1607CodeIssue()); // OK
            this.handlers.Add("SA1608", new SA1608CodeIssue()); // OK
            this.handlers.Add("SA1609", new SA1609CodeIssue()); // OK
            this.handlers.Add("SA1610", markElementByItsNameCodeIssue); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1611", new SA1611CodeIssue()); // OK
            this.handlers.Add("SA1612", new SA1612CodeIssue()); // OK
            this.handlers.Add("SA1613", new SA1613CodeIssue()); // OK
            this.handlers.Add("SA1614", new SA1614CodeIssue()); // OK
            this.handlers.Add("SA1615", new SA1615CodeIssue()); // OK
            this.handlers.Add("SA1616", new SA1616CodeIssue()); // OK
            this.handlers.Add("SA1617", new SA1617CodeIssue()); // OK
            this.handlers.Add("SA1618", new SA1618CodeIssue()); // OK
            this.handlers.Add("SA1619", new SA1619CodeIssue()); // OK
            this.handlers.Add("SA1620", new SA1620CodeIssue()); // OK
            this.handlers.Add("SA1621", new SA1621CodeIssue()); // OK
            this.handlers.Add("SA1622", new SA1622CodeIssue()); // OK
            this.handlers.Add("SA1623", new SA1623CodeIssue()); // OK
            this.handlers.Add("SA1624", new SA1624CodeIssue()); // OK
            this.handlers.Add("SA1625", new SA1625CodeIssue()); // OK
            this.handlers.Add("SA1626", new SA1626CodeIssue()); // OK
            this.handlers.Add("SA1627", markElementByItsNameCodeIssue); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1628", new SA1628CodeIssue()); // OK
            this.handlers.Add("SA1629", new SA1629CodeIssue()); // OK
            this.handlers.Add("SA1630", new SA1630CodeIssue()); // OK
            this.handlers.Add("SA1631", new SA1631CodeIssue()); // OK
            this.handlers.Add("SA1632", new SA1632CodeIssue()); // OK
            this.handlers.Add("SA1633", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1634", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1635", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1636", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1637", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1638", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1639", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1640", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1641", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1642", new SA1642CodeIssue()); // OK
            this.handlers.Add("SA1643", new SA1643CodeIssue()); // OK
            this.handlers.Add("SA1644", new SA1644CodeIssue()); // OK
            this.handlers.Add("SA1645", new SA1645CodeIssue()); // OK
            this.handlers.Add("SA1646", new SA1646CodeIssue()); // OK
            this.handlers.Add("SA1647", new SA1647CodeIssue()); // OK
            this.handlers.Add("SA1500", new SA1500CodeIssue()); // OK
            this.handlers.Add("SA1501", new SA1501CodeIssue()); // OK
            this.handlers.Add("SA1502", new SA1502CodeIssue()); // OK
            this.handlers.Add("SA1503", markElementByItsNameCodeIssue); // TODO
            this.handlers.Add("SA1504", new SA1504CodeIssue()); // BUG
            this.handlers.Add("SA1505", new SA1505CodeIssue()); // OK
            this.handlers.Add("SA1506", new SA1506CodeIssue()); // DxCore bug
            this.handlers.Add("SA1507", new SA1507CodeIssue()); // OK
            this.handlers.Add("SA1508", new SA1508CodeIssue()); // OK
            this.handlers.Add("SA1509", new SA1509CodeIssue()); // OK
            this.handlers.Add("SA1510", new SA1510CodeIssue()); // OK
            this.handlers.Add("SA1511", new SA1511CodeIssue()); // OK
            this.handlers.Add("SA1512", new SA1512CodeIssue()); // OK
            this.handlers.Add("SA1513", new SA1513CodeIssue()); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1515", new SA1515CodeIssue()); // OK

        }

        public ICodeIssue GetIssueFor(Violation violation)
        {
            ICodeIssue handler = null;
            this.handlers.TryGetValue(violation.Rule.CheckId, out handler);
            return handler ?? this.nullHandler;
        }
    }
}
