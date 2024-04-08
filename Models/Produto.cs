namespace HipicaFacilSQL.Models
{
    public class Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime Validade { get; set; }
        public string Descricao { get; set; }

        public ICollection<Navigation> Navigations { get; set; }
    }
}
