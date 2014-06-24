using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MB.Models.POCO;

namespace Minibus.Models.Context
{
    public class MinibusContext : DbContext
    {
        public DbSet<Account_Types> Account_Types { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<users_roles> users_roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*******WANTS TO MAP TO TABLE BOOKINGINFOES, NEED TO SPECIFY*********/
            modelBuilder.Entity<UserInfo>().ToTable("UserInfos");
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        }

    }
}