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
        public DbSet<BookingInfo> BookingInfos { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<JourneyEnd> JourneyEnds { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<users_roles> users_roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*******WANTS TO MAP TO TABLE BOOKINGINFOES, NEED TO SPECIFY*********/
            modelBuilder.Entity<BookingInfo>().ToTable("BookingInfos");
            modelBuilder.Entity<UserInfo>().ToTable("UserInfos");
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        }

    }
}