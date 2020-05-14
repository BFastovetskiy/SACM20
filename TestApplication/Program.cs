using System;
using Npgsql;

namespace TestApplication
{
    using SACM20.Core;
    using SACM20.Core.Security;
    class Program
    {
        static void Main(string[] args)
        {
            Npgsql.NpgsqlConnection connection = null;
            var csb = new NpgsqlConnectionStringBuilder();
            csb.Host = "localhost";
            csb.Port = 5432;
            csb.Username = "sacm";
            csb.Database = "sacm";
            csb.Password = "sacm";
            csb.ApplicationName = "Service Asset and Configuration Management";
            connection = new NpgsqlConnection(csb.ConnectionString);
            connection.Open();

            DataManager dataManager = new DataManager(connection);
            var manager = dataManager.GetSecurityManager();
            manager.OnCreateRole += Manager_RoleOnCreating;
            StateResult result = manager.CreateRole("Administrators");
            manager.CreateRole("Users");
            manager.AppendPrivilegeToRole(new Privilege("sacm-show-all-business-objects"), "users");
            System.Console.ReadLine();
        }

        private static void Manager_RoleOnCreating(object sender, EventArgs e)
        {
            System.Console.WriteLine("Enevt time: {0}", DateTime.Now);
            System.Console.WriteLine("Method: {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
            System.Console.WriteLine("Sender: {0}", sender);
            if (sender is ISecurityManager)
                System.Console.WriteLine("User: {0}", (sender as ISecurityManager).ContextUser.Login);
        }
    }
}
