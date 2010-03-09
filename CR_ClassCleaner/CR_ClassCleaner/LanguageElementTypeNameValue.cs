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
using System.Collections.Generic;

namespace CR_ClassCleaner
{
    internal class NameValueSelectectedComparer : Comparer<NameValueSelectected>
    {
        /// <summary>
        /// When overridden in a derived class, performs a comparison of two objects of the same type and returns a value indicating whether one object is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// Value Condition Less than zero x is less than y.Zero x equals y.Greater than zero x is greater than y.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">Type T does not implement either the <see cref="T:System.IComparable`1"></see> generic interface or the <see cref="T:System.IComparable"></see> interface.</exception>
        public override int Compare(NameValueSelectected x, NameValueSelectected y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    internal class NameValueSelectected
    {
        private string name;

        private bool selected;

        private int value;

        /// <summary>
        /// Initializes a new instance of the LanguageElementTypeNameValue class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public NameValueSelectected(int value, string name)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

    }
}