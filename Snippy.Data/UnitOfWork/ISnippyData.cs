namespace Snippy.Data.UnitOfWork
{
    using Models;
    using Repository;

    public interface ISnippyData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Label> Labels { get; }

        IRepository<Language> Language { get; }

        IRepository<Snippet> Snippets { get; }

        int SaveChanges();
    }
}
