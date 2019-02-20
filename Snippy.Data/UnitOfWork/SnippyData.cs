namespace Snippy.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Repository;

    public class SnippyData : ISnippyData
    {
        private ApplicationDbContext context;
        private IDictionary<Type, object> repositories;

        public SnippyData()
            : this(new ApplicationDbContext())
        {
        }

        public SnippyData(ApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Label> Labels
        {
            get
            {
                return this.GetRepository<Label>();
            }
        }

        public IRepository<Language> Language
        {
            get
            {
                return this.GetRepository<Language>();
            }
        }

        public IRepository<Snippet> Snippets
        {
            get
            {
                return this.GetRepository<Snippet>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return this.repositories[typeOfModel] as IRepository<T>;
        }
    }
}
