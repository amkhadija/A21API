using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace A21API.Models
{
    //public enum NomCours { Art, Sport, Titulaire, Anglais }
    public class Enseignant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Nom { get; set; }

        [Required]
        [StringLength(25)]
        public string Prenom { get; set; }
        
        [Required]
        public int NHeures { get; set; }  //nombre d'heure de travail assigné

        [Required]
        [RegularExpression("^(Art|Sport|Titulaire|Anglais)$")]
        public String Cours { get; set; }
        public ICollection<CrenoHoraire> CrenoHoraires { get; set; }

        public Enseignant()
        {
            CrenoHoraires = new List<CrenoHoraire>();
        }

    }
}
