using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using AspCoreVue.Entities;
//Scaffold created by original entity diagram
namespace AspCoreVue.Contexts
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        ///<Summary> Updates The Removed Date Time of an entity, rather than fully deleting from database </Summary>
        ///<Summary> This Should be used on all entities where history needs to be recorded </Summary>
        public void SoftDelete(object entity)
        {
            entity.GetType().GetProperty("RemovedDate").SetValue(entity, DateTime.Now);
            base.Update(entity);
            base.SaveChanges();
        }


        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        public virtual DbSet<UserOrder> UserOrder { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}