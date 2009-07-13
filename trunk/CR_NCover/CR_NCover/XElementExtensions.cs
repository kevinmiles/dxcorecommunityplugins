using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CR_NCover
{
	public static class XElementExtensions
	{
		public static int AttributeAsInt(this XElement element, string name)
		{
			return Convert.ToInt32(element.Attributes(name).FirstOrDefault().Value);
		}
	}
}
