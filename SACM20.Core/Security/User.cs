using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core.Security
{
    public class User : IUser
    {
        Guid m_id = Guid.Empty;
        string m_login = string.Empty;
        string m_password = string.Empty;
        List<IRole> m_roles = null;

        internal User() : base()
        {
            this.m_id = Guid.NewGuid();
            this.m_roles = new List<IRole>();
        }

        internal User(string login, string password) : base()
        {
            this.m_id = Guid.NewGuid();
            this.m_login = login;
            this.m_password = password;
            this.m_roles = new List<IRole>();
        }

        internal void AppendToRole(IRole role)
        {
            object locklist = new object();
            lock (locklist)
                this.m_roles.Add(role);
        }

        public Guid Id {
            get { return this.m_id; }
            internal set { this.m_id = value; }
        }
        public string Login { get { return this.m_login; } }

        public string Password { get { return this.m_password; } }

        public string Email { get; set; }
        public string Phone { get; set; }

        public IRole[] Roles
        {
            get
            {
                object locklist = new object();
                lock (locklist)
                    return new List<IRole>(this.m_roles).ToArray();                
            }
        }
    }
}
