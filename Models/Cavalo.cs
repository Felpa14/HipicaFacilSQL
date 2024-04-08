namespace HipicaFacilSQL.Models
{
    public class Cavalo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public DateTime Idade { get; set; }
        public double Peso { get; set; }

        public ICollection<Navigation> Navigations { get; set; }
    }
}
