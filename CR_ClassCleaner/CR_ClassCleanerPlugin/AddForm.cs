//////////////////////////////////////////////////////////////////
//CR_ClassCleaner plugin provides organization capabilties to 
//Visual Studio when used with the DXCore framework.
//Copyright (C) 2006  John Luif
//
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation version 2
//of the License.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace CR_ClassCleaner
{
	public partial class AddForm : Form
	{
		public event EventHandler<CodeGroupTypeEventArgs> CodeGroupAdded;

		struct GroupType
		{
			private Type group;

			private string groupName;

			/// <summary>
			/// Initializes a new instance of the GroupType class.
			/// </summary>
			/// <param name="groupType"></param>
			/// <param name="name"></param>
			public GroupType(Type groupType, string name)
			{
				group = groupType;
				groupName = name;
			}

			/// <summary>
			/// Gets or sets the group.
			/// </summary>
			/// <value>The group.</value>
			public Type Group
			{
				get { return group; }
			}


			/// <summary>
			/// Gets or sets the name of the group.
			/// </summary>
			/// <value>The name of the group.</value>
			public string GroupName
			{
				get { return groupName; }
			}
		}

		public AddForm()
		{
			InitializeComponent();

			PopulateCodeGroups();
		}

		private void AddClicked(object sender, EventArgs e)
		{
			if (CodeGroupAdded != null)
			{
				CodeGroupTypeEventArgs args =
					 new CodeGroupTypeEventArgs(
						  (Type)groupTypesComboBox.SelectedValue);

				CodeGroupAdded(this, args);
			}

			Close();
		}

		private void PopulateCodeGroups()
		{
			List<GroupType> codeGroups = new List<GroupType>();

			Assembly assembly = Assembly.GetExecutingAssembly();
			Type[] classTypes = assembly.GetTypes();

			foreach (Type item in classTypes)
			{
				if (item.BaseType == typeof(CodeGroup))
				{
					string itemName =
						 item.ToString().Replace(item.Namespace, string.Empty);

					GroupType group = new GroupType(item, itemName.TrimStart('.'));
					codeGroups.Add(group);
				}
			}

			SetCodeGroupComboDataSource(codeGroups);
		}

		private void SetCodeGroupComboDataSource(List<GroupType> codeGroups)
		{
			groupTypesComboBox.DataSource = codeGroups;
			groupTypesComboBox.DisplayMember = "GroupName";
			groupTypesComboBox.ValueMember = "Group";
			if (groupTypesComboBox.Items.Count > 0)
				groupTypesComboBox.SelectedValue = codeGroups[0].Group;
		}
	}
}