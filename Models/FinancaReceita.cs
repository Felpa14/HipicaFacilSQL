namespace HipicaFacilSQL.Models
{
    public class FinancaReceita
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Valor{ get; set; }
        public string Descricao { get; set; }
        public string? Tipo { get; set; }
        public int Quantidade { get; set; }

        public ICollection<Navigation>? Navigations { get; set; }
    }
}
