namespace OrganizationsWebApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .Property(e => e.Name)
                .IsFixedLength();
        }
    }
}
