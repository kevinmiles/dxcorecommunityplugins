// <copyright file="SA1027TestCode.cs" company="ACME">
//	 Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

#pragma warning disable 1591

	/// <summary>
	/// Test code for SA1027 rule - tabs are not allowed.
	/// </summary>
	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1026 rule.")]
	public class SA1027TestCode
	{
		private string propertyName;

		public string PropertyName
		{
			get
			{
				return this.propertyName;
	    	}

			set
			{
				this.propertyName = value;
			}
		}
	}
}
