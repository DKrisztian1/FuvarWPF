using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuvarClass
{
    class Fuvar
    {
        int taxiId;
        string indulas;
        int idotartam;
        double tavolsag;
        double viteldij;
        double borravalo;
        string fizetesModja;

        public Fuvar(int taxiId, string indulas, int idotartam, double tavolsag, double viteldij, double borravalo, string fizetesModja)
        {
            this.taxiId = taxiId;
            this.indulas = indulas;
            this.idotartam = idotartam;
            this.tavolsag = tavolsag;
            this.viteldij = viteldij;
            this.borravalo = borravalo;
            this.fizetesModja = fizetesModja;
        }

        public int TaxiId { get => taxiId; }
        public string Indulas { get => indulas; }
        public int Idotartam { get => idotartam; }
        public double Tavolsag { get => tavolsag; }
        public double Viteldij { get => viteldij; }
        public double Borravalo { get => borravalo; }
        public string FizetesModja { get => fizetesModja; }
    }
}
