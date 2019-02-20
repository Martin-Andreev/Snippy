namespace Snippy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Label
    {
        private ICollection<Snippet> snippets;

        public Label()
        {
            this.snippets = new HashSet<Snippet>();
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual ICollection<Snippet> Snippets
        {
            get { return this.snippets; }
            set { this.snippets = value; }
        }
    }
}
