using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SACM.Core.Security
{
    public delegate StateResult CheckPermissionHandler(object sender, IEnumerable<PrivilegeAttribute> attributes, IUser user);

    public delegate void AuditHandler(object sender, string log);
    public interface ISecurityManager
    {
        event EventHandler OnCreateRole;

        IUser ContextUser { get; }
        StateResult CreateRole(string name);

        StateResult AppendPrivilegeToRole(IPrivilege privilege, IRole role);
        StateResult AppendPrivilegeToRole(IPrivilege privilege, string role);
        StateResult AppendPrivilegeToRole(string privilege, string role);

        StateResult RemovePrivilegeFromRole(IPrivilege privilege, IRole role);

    }
}
