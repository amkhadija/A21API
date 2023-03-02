using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A21API.Models
{
    public class EmploiTemps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string? Annee_Scolaire { get; set; }
        public string? Nom_Ecole { get; set; }
        public int? Groupe { get; set; }
        public int? Locale { get; set; }

        public ICollection<CrenoHoraire> CrenoHoraires { get; set; }

        public EmploiTemps()
        {
            CrenoHoraires = new List<CrenoHoraire>();
        }
    }
}
