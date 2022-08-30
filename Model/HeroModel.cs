using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
    public class HeroModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Place { get; set; }

    }
}
