namespace Snippy.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(
               new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Label> Labels { get; set; }

        public virtual IDbSet<Language> Languages { get; set; }

        public virtual IDbSet<Snippet> Snippets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snippet>()
                .HasRequired(s => s.Author)
                .WithMany(a => a.Snippets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Snippet>()
                .HasMany(u => u.Labels)
                .WithMany(s => s.Snippets)
                .Map(uf =>
                {
                    uf.MapLeftKey("SnippetId");
                    uf.MapRightKey("LabelId");
                    uf.ToTable("SnippetLabels");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
