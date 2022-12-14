using System.ComponentModel.DataAnnotations;

namespace StoragePeople.Models.Enterties
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string? LanguageName { get; set; }
        public List<Person> LanguagePeople { get; set; } = new List<Person>();
    }
}
