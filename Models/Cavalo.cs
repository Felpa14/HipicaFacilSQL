using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace HipicaFacilSQL.Models
{
    public class Cavalo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Idade { get; set; }
        public string Peso { get; set; }
        public string Proprietario { get; set; }
        public string ?ImagemPath { get; set; } // Caminho da imagem
        public List<string> ?DocumentosPaths { get; set; } = new List<string>(); // Caminhos dos documentos

        public ICollection<Navigation> ?Navigations { get; set; }
    }
}
