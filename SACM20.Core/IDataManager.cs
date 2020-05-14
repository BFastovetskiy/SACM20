using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core
{
    using System.Data.Common;
    using SACM20.Core.Security;
    public interface IDataManager
    {
        DbConnection DbConnection { get; }
        ISecurityManager GetSecurityManager();
    }
}
