using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Domain.Hero
{
    public class Hero
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public string Name { get; set; }

        [JsonPropertyName("DisguiseName")]
        [Required(ErrorMessage = "Campo Nome do disfarce é obrigatório!")]
        public string DisguiseName { get; set; }

        [JsonPropertyName("Place")]
        [Required(ErrorMessage = "Campo Lugar é obrigatório!")]
        public string Place { get; set; }

        [JsonIgnore]
        [JsonPropertyName("Active")]
        public bool Active { get; set; }

    }
}
