using System;
using System.Collections.Generic;
using System.Text;

namespace SACM.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SACM.Core.Security;

    internal class DataContext : DbContext
    {
        protected string m_connectionString = string.Empty;

        public DataContext(string connectionString) : base()
        {
            this.m_connectionString = connectionString;
        }
        public DataContext(string connectionString, DbContextOptions options) : base(options)
        {
            this.m_connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(m_connectionString);
    }
}
