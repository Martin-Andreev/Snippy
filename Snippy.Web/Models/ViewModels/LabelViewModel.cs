namespace Snippy.Web.Models.ViewModels
{
    using Infrastructure;
    using Snippy.Models;

    public class LabelViewModel : IMapFrom<Label>
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}