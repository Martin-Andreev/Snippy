namespace Snippy.Web.Models.ViewModels
{
    using AutoMapper;
    using Infrastructure;
    using Snippy.Models;

    public class LabelsMinifiedViewModel : LabelViewModel, IHaveCustomMappings
    {
        public int SnippetsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Label, LabelsMinifiedViewModel>()
                .ForMember(s => s.SnippetsCount, opt => opt.MapFrom(s => s.Snippets.Count));
        }
    }
}