using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core
{
    using System.Data;
    using System.Data.Common;
    using SACM.Core.Security;
    public class DataManager : IDataManager
    {
        DbConnection m_connection = null;
        ISecurityManager m_securityManager = null;
        DataContext m_context = null;
        public DataManager(DbConnection connection)
        {
            this.m_connection = connection;
            if (this.m_connection.State != ConnectionState.Open)
            {
                try
                {
                    this.m_connection.Open();
                    this.m_context = new DataContext(connection.ConnectionString);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            
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
