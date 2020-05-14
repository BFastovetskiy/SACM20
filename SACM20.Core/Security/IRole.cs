using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SACM20.Core.Security
{
    public interface IRole
    {
        string Name { get; set; }

        string Description { get; set; }

        IPrivilege[] Privileges { get; }

        IUser[] Users { get; }

        /*
        StateResult AppendPrivilege(IPrivilege privilege);
        StateResult RemovePrivilege(IPrivilege privilege);
        StateResult AppendUser(IUser user);
        StateResult RemoveUser(IUser user);
        */
    }
}
