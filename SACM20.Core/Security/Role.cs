using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core.Security
{
    public class Role : IRole
    {
        Guid m_id = Guid.Empty;
        string m_title = string.Empty;
        List<IPrivilege> m_privilege = null;
        List<IUser> m_users = null;

        internal Role() : base()
        {
            this.m_id = Guid.NewGuid();
            this.m_privilege = new List<IPrivilege>();
            this.m_users = new List<IUser>();
        }

        internal Role(string name) : base()
        {
            this.m_id = Guid.NewGuid();
            this.m_title = name;
            this.m_privilege = new List<IPrivilege>();
            this.m_users = new List<IUser>();
        }

        internal Role(string name, IPrivilege[] privileges) : base()
        {
            this.m_id = Guid.NewGuid();
            this.m_title = name;
            this.m_privilege = new List<IPrivilege>();
            this.m_privilege.AddRange(privileges);
            this.m_users = new List<IUser>();
        }

        public Guid Id
        {
            get { return this.m_id; }
            internal set { this.m_id = value; }
        }

        public string Name
        {
            get { return this.m_title; }
            set { this.m_title = value; }
        }
        public string Description { get; set; }

        public IPrivilege[] Privileges
        {
            get
            {
                object locklist = new object();
                lock (locklist)
                    return new List<IPrivilege>(this.m_privilege).ToArray();
            }
        }

        public IUser[] Users
        {
            get
            {
                object locklist = new object();
                lock (locklist)
                    return new List<IUser>(this.m_users).ToArray();
            }
        }

        internal StateResult AppendPrivilege(IPrivilege privilege)
        {
            object locklist = new object();
            lock (locklist)
                this.m_privilege.Add(privilege);
            return StateResult.Success;
        }
    }
}
