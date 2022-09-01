using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Model
{
    public class HeroModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public string Name { get; set; }
        
        [JsonPropertyName("FirstName")]
        [Required(ErrorMessage = "Campo Primeiro Nome é obrigatório!")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        [Required(ErrorMessage = "Campo Ultimo Nome é obrigatório!")]
        public string LastName { get; set; }
        
        [JsonPropertyName("Place")]
        [Required(ErrorMessage = "Campo Lugar é obrigatório!")]
        public string Place { get; set; }
        
        [JsonIgnore]
        [JsonPropertyName("Active")]
        public bool Active { get; set; }

    }
}
