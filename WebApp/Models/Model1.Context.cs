//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TempDb1Entities : DbContext
    {
        public TempDb1Entities()
            : base("name=TempDb1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LoginInfo> LoginInfoes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Purchase_History> Product_Purchase_History { get; set; }
    }
}
