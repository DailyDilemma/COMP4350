using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using ListAssist.Data.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListAssist.Data
{
    public class ListAssistContext : DbContext
    {
        public ListAssistContext() : base("ListAssistContext") { }

        public DbSet<LAList> LALists { get; set; }
        public DbSet<LAListItem> LAListItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // LAList Configuration
            modelBuilder.Entity<LAList>().HasKey(s => s.ID);
            modelBuilder.Entity<LAList>().Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // LAListItems Configuration
            modelBuilder.Entity<LAListItem>()
                        .HasRequired(l => l.LAList)
                        .WithMany(l => l.LAListItems)
                        .HasForeignKey(l => l.ListID);
                        
        }
    }    
}
