using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PrivilegeAttribute : Attribute
    {
        private string m_name;

        /// <summary>
        /// Name of privilege for granted on action
        /// </summary>
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }
    }
}
