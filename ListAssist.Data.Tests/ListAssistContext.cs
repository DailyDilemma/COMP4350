using System.Data.Entity;
using ListAssist.Data.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListAssist.Data
{
    public class ListAssistContext : DbContext
    {
        public ListAssistContext() : base("ListAssistContext") { }

        public DbSet<LAList> LALists { get; set; }
        public DbSet<LAListItem> LAListItems { get; set; }

        // This method allows you to override the default behaviors for how entity framework 
        // creates a database from the provided model classes. When overriding entity framework's
        // default behaviors you use what is known as the "Fluent API" to add additional details
        // about how things like keys and relationships are built.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Removes the convention whereby table names are automatically pluralized (ie. entity
            // framework will not longer add an "s" to table names)
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
