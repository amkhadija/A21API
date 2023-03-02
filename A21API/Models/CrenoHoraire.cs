using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A21API.Models
{
    //public enum Jours { Lundi, Mardi, Mercredi, Jeudi, Vendredi}
    //public enum Periode { p1, p2, p3, p4, p5 }
    public class CrenoHoraire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey(nameof(Enseignant))]
        public int? EnseignantID { get; set; }
        public Enseignant? Enseignant { get; set; }

        [Required]
        [RegularExpression("^(Lundi|Mardi|Mercredi|Jeudi|Vendredi)$")]
        public String Jours { get; set; }

        [Required]
        [Range(1, 5)]
        public int Periode { get; set; }

        [Required]
        [ForeignKey(nameof(EmploiTemps))]
        public int EmploiTempsID { get; set; }
        public EmploiTemps? EmploiTemps { get; set; }

    }
}
