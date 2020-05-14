using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core
{
    using System.Data.Common;
    using SACM20.Core.Security;
    public class DataManager : IDataManager
    {
        DbConnection m_connection = null;
        ISecurityManager m_securityManager = null;
        public DataManager(DbConnection connection)
        {
            this.m_connection = connection;
        }

        public ISecurityManager GetSecurityManager()
        {
            if (this.m_securityManager == null)
                this.m_securityManager = new SecurityManager(this);
            (this.m_securityManager as SecurityManager).Reload();
            return this.m_securityManager;
        }

        public DbConnection DbConnection { get { return this.m_connection; } }
    }
}
