namespace Snippy.Web.Models.ViewModels
{
    using System;
    using System.Web;
    using AutoMapper;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Snippy.Models;

    public class CommentMinifiedViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool CanDelete { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentMinifiedViewModel>()
                .ForMember(n => n.AuthorName, opt => opt.MapFrom(n => n.Author.UserName))
                .ForMember(n => n.CanDelete, opt => opt.MapFrom(n => n.Author.UserName == HttpContext.Current.User.Identity.Name));
        }
    }
}