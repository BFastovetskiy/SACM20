using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core
{
    using System.Data.Common;
    using SACM.Core.Security;
    public interface IDataManager
    {
        DbConnection DbConnection { get; }
        ISecurityManager GetSecurityManager();
    }
}
