using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SACM.Core.Security
{
    public class SecurityManager : ISecurityManager
    {
        public AuditHandler Audit;

        private bool m_isUseAudit = false;
        private List<IRole> m_roles = null;
        private List<IUser> m_users = null;
        private IUser m_contextUser = null;
        private IDataManager m_dataManager = null;

        public event EventHandler OnCreateRole;
        private event CheckPermissionHandler OnCheckPermission;

        internal SecurityManager(IDataManager dataManager, bool isUseAudit = false)
        {
            this.m_dataManager = dataManager;
            this.m_isUseAudit = isUseAudit;
            this.m_roles = new List<IRole>();
            this.m_contextUser = new User("Admin", "admin");
            var role = new Role("Administrator", new Privilege[1] { new Privilege("sacm-create-role") });
            this.m_roles.Add(role);
            (this.m_contextUser as User).AppendToRole(role);
            this.OnCheckPermission += SecurityManager_CheckPermission;
        }

        internal StateResult Reload()
        {
            object lockObject = new object();
            lock (lockObject)
            {
                StateResult result = null;
                if (this.m_dataManager.DbConnection.State != System.Data.ConnectionState.Open)
                    try
                    {
                        this.m_dataManager.DbConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        result = StateResult.NotConnected;
                        result.Message = ex.Message;
                        result.ObjectType = this.GetType().Name;
                        result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        return result;
                    }
                var cmd = this.m_dataManager.DbConnection.CreateCommand();
                cmd.CommandText = "select id, login, password, phone, email from sacm.users";
                using (var dataReader = cmd.ExecuteReader())
                    while (dataReader.Read())
                    {
                        User user = new User(dataReader["login"].ToString(), dataReader["password"].ToString())
                        {
                            Id = Guid.Parse(dataReader["id"].ToString()),
                            Email = dataReader["email"].ToString(),
                            Phone = dataReader["phone"].ToString()
                        };

                        this.m_users.Add(user);
                    }
                cmd.CommandText = "select id, title, description from sacm.roles";
                using (var dataReader = cmd.ExecuteReader())
                    while (dataReader.Read())
                    {
                        Role role = new Role(dataReader["title"].ToString())
                        {
                            Id = Guid.Parse(dataReader["id"].ToString()),
                            Description = dataReader["description"].ToString()
                        };

                        this.m_roles.Add(role);
                    }
                result = StateResult.Success;
                result.ObjectType = this.GetType().Name;
                result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return result;
            }
        }

        #region Обработка внутренних событий
        private StateResult SecurityManager_CheckPermission(object sender, IEnumerable<PrivilegeAttribute> attributes, IUser user)
        {
            IEnumerator<PrivilegeAttribute> enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (user.Roles.Where(e => e.Privileges.Where(p => p.Name.ToLower() == enumerator.Current.Name.ToLower()).Any()).Count() > 0)
                    return StateResult.Success;
            }
            return StateResult.Forbidden;
        }
        #endregion

        /// <summary>
        /// Current user
        /// </summary>
        public IUser ContextUser { get { return this.m_contextUser; } }

        [Privilege(Name = "sacm-create-role")]
        public StateResult CreateRole(string name)
        {
            if (this.m_isUseAudit) { }
            StateResult result = null;
            if (this.OnCheckPermission == null)
            {
                result = StateResult.NotUsedModuleChangeProcessing;
                result.ObjectType = this.GetType().Name;
                result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return result;
            }

            result = this.OnCheckPermission(this, System.Reflection.MethodBase.GetCurrentMethod()
                .GetCustomAttributes<PrivilegeAttribute>(), this.m_contextUser);
            if (!result.Result)
                result = StateResult.Forbidden;
            else
            {
                IRole role = new Role(name);
                this.m_roles.Add(role);
                result = StateResult.Success;
                result.ObjectId = (role as IdClass).Id;
                result.ObjectType = role.GetType().Name;
            }
            result.ObjectType = this.GetType().Name;
            result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            this.OnCreateRole?.Invoke(this, new EventArgs());

            return result;
        }

        [Privilege(Name = "sacm-append-privilege-to-role")]
        public StateResult AppendPrivilegeToRole(IPrivilege privilege, IRole role)
        {
            return this.AppendPrivilegeToRole(privilege.Name, role.Name);
        }

        [Privilege(Name = "sacm-append-privilege-to-role")]
        public StateResult AppendPrivilegeToRole(IPrivilege privilege, string role)
        {
            return this.AppendPrivilegeToRole(privilege.Name, role);
        }

        [Privilege(Name = "sacm-append-privilege-to-role")]
        public StateResult AppendPrivilegeToRole(string privilege, string role)
        {
            if (this.m_isUseAudit) { }
            StateResult result = null;
            if (this.OnCheckPermission == null)
            {
                result = StateResult.NotUsedModuleChangeProcessing;
                result.ObjectType = this.GetType().Name;
                result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return result;
            }

            result = this.OnCheckPermission(this, System.Reflection.MethodBase.GetCurrentMethod()
                .GetCustomAttributes<PrivilegeAttribute>(), this.m_contextUser);
            if (!result.Result)
                result = StateResult.Forbidden;
            else
            {
                IRole role1 = this.m_roles.Where(r => r.Name == role).Single();
                if (role1 == null)
                {
                    result = StateResult.NotFound;
                }
                else
                {
                    if (role1.Privileges.Count(e => e.Name.ToLower() == privilege.ToLower()) == 0)
                        (role1 as Role).AppendPrivilege(new Privilege(privilege));
                    result = StateResult.Success;
                    result.ObjectId = (role1 as IdClass).Id;
                    result.ObjectType = role1.GetType().Name;
                }
            }
            result.ObjectType = this.GetType().Name;
            result.Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
            this.OnCreateRole?.Invoke(this, new EventArgs());

            return result;
        }

        [Privilege(Name = "sacm-remove-privilege-from-role")]
        public StateResult RemovePrivilegeFromRole(IPrivilege privilege, IRole role)
        {
            throw new NotImplementedException();
        }
    }
}
