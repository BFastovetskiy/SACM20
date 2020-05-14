using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core.Security
{
    public class Privilege : IPrivilege
    {
        string m_name = string.Empty;

        public Privilege(string name) : base()
        {
            this.m_name = name;
        }

        /// <summary>
        /// Name of privilege
        /// </summary>
        public string Name 
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }
    }
}
