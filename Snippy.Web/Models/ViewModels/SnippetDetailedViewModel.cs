namespace Snippy.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Infrastructure;
    using Snippy.Models;

    public class SnippetDetailedViewModel : SnippetViewModel, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string Code { get; set; }

        public string LanguageName { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<CommentMinifiedViewModel> Comments { get; set; }

        public DateTime CreatedOn { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Snippet, SnippetDetailedViewModel>()
                .ForMember(s => s.LanguageName, opt => opt.MapFrom(s => s.Language.Name))
                .ForMember(s => s.AuthorName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(s => s.Comments, opt => opt.MapFrom(s => s.Comments.OrderByDescending(c => c.CreatedOn)));
        }
    }
}