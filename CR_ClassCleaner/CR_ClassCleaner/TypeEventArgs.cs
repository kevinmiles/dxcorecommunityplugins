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

namespace CR_ClassCleaner
{
    public class CodeGroupTypeEventArgs : EventArgs
    {
        private Type codeGroupType;

        /// <summary>
        /// Initializes a new instance of the TypeEventArgs class.
        /// </summary>
        /// <param name="codeGroupType"></param>
        public CodeGroupTypeEventArgs(Type codeGroupType)
        {
            this.codeGroupType = codeGroupType;
        }

        /// <summary>
        /// Gets or sets the type of the code group.
        /// </summary>
        /// <value>The type of the code group.</value>
        public Type CodeGroupType
        {
            get { return codeGroupType; }
        }

    }
}