using System;
using System.Collections.Generic;
using System.Text;

namespace SACM20.Core.Security
{
    public interface IUser
    {
        string Login { get; }

        string Password { get; }

        string Email { get; set; }

        string Phone { get; set; }

        IRole[] Roles { get; }

        /*
        StateResult AppendToRole(IRole role);
        StateResult RemoveFromRole(IRole role);
        StateResult ResetPassword(string oldPassword, string newPassword);
        static IUser NewUser(string login, string password) => throw new NotImplementedException();
        static IUser NewUser(string login, string password, IRole role) => throw new NotImplementedException();
        static IUser NewUser(string login, string password, IRole[] roles) => throw new NotImplementedException();
        */
    }
}
