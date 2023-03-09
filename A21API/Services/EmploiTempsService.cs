using A21API.Data;
using A21API.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace A21API.Services
{
    public class EmploiTempsService : IEmploiTempsService
    {
        private readonly A21APIContext _context;
        private List<string> _erreursValidation;

        public EmploiTempsService(A21APIContext context)
        {
            _context = context;
            _erreursValidation = new List<string>();
        }

        public async Task<List<EmploiTemps>> GetEmploiTemps()
        {
            return await _context.EmploiTemps.ToListAsync();
        }

        public async Task<EmploiTemps> GetEmploiTemps(int id)
        {
            return await _context.EmploiTemps.FindAsync(id);
        }

        public async Task<EmploiTemps> SaveEmploiTemps(EmploiTemps emploiTemps)
        {
            if (!EmploiDuTempsValide(emploiTemps))
            {
                return emploiTemps;
                //erruers dans erreurs
            }

            EmploiTemps empt;
            if (emploiTemps.ID == 0)
            {
                empt = await CreateEmploiTemps(emploiTemps);
            }
            else
            {
                empt = await UpdateEmploiTemps(emploiTemps);
            }

            if (empt != null)
            {
                foreach (var item in empt.CrenoHoraires)
                {
                    item.EmploiTemps = null;
                    item.Enseignant = null;
                }
            }
            return empt;
        }

        private async Task<EmploiTemps> CreateEmploiTemps(EmploiTemps emploiTemps)
        {
            // Create a new EmploiTemps object
            var newEmploiTemps = new EmploiTemps
            {
                Annee_Scolaire = emploiTemps.Annee_Scolaire,
                Nom_Ecole = emploiTemps.Nom_Ecole,
                Groupe = emploiTemps.Groupe,
                Locale = emploiTemps.Locale
            };
            foreach (var ch in emploiTemps.CrenoHoraires)
            {
                ch.ID = 0;
                ch.EmploiTemps = newEmploiTemps;
            }

            newEmploiTemps.CrenoHoraires = emploiTemps.CrenoHoraires;
            // Add the new EmploiTemps object to the context and save changes
            _context.EmploiTemps.Add(newEmploiTemps);
            await _context.SaveChangesAsync();

            return newEmploiTemps;
        }

        private async Task<EmploiTemps> UpdateEmploiTemps(EmploiTemps emploiTemps)
        {
            if (_context.EmploiTemps == null)
            {
                return null;
            }

            var et = _context.EmploiTemps.Find(emploiTemps.ID);
            if (et == null)
                return null;

            et.Locale = emploiTemps.Locale;
            et.Nom_Ecole = emploiTemps.Nom_Ecole;
            et.Annee_Scolaire = emploiTemps.Annee_Scolaire;
            et.Groupe = emploiTemps.Groupe;

            emploiTemps.CrenoHoraires.ToList().ForEach(ch =>
            {
                var item = _context.CrenoHoraires.FirstOrDefault(x => x.ID == ch.ID);
                if (item != null)
                {
                    item.Periode = ch.Periode;
                    item.Jours = ch.Jours;
                    item.EnseignantID = ch.EnseignantID;
                    item.EmploiTempsID = ch.EmploiTempsID;
                }
            });

            await _context.SaveChangesAsync();

            return _context.EmploiTemps.Find(emploiTemps.ID);
        }

        public async Task<bool> DeleteEmploiTemps(int id)
        {
            if (_context.EmploiTemps == null)
            {
                return false;
            }
            var emploiTemps = await _context.EmploiTemps.FindAsync(id);
            if (emploiTemps == null)
            {
                return false;
            }

            _context.EmploiTemps.Remove(emploiTemps);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool EmploiDuTempsValide(EmploiTemps emploiTemps)
        {
            return ValiderNombrePeriodeParEnseignantParJour(emploiTemps);
            // Valider nombre des cours successives
            // Valider nombre total des periodes par enseignant par emploi du temps
            // Valider nombre total des periodes par enseignant pour tous les enmploi du temps en cours
            // valides nombres d'un cours par emploi du temps
            //...
        }

        private bool ValiderNombrePeriodeParEnseignantParJour(EmploiTemps emploiTemps)
        {
            var groups = emploiTemps.CrenoHoraires
                            .GroupBy(x => new { x.Jours, x.EnseignantID })
                            .Select(g => new
                            {
                                Jours = g.Key.Jours,
                                Enseignant = g.Key.EnseignantID,
                                Creno = g.OrderBy(c => c.Periode)
                            });

            foreach (var g in groups)
            {
                if (g.Enseignant != null)
                {
                    if (g.Creno.Count() > 3)
                    {
                        _erreursValidation.Add($"l'enseignant {g.Enseignant} a plus que 3 periode le {g.Jours}");
                        return false;
                    }
                }
            }

            return true;
        }

        public List<string> getErrors()
        {
            return _erreursValidation;
        }
    }
}