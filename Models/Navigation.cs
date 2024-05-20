namespace HipicaFacilSQL.Models
{
    public class Navigation
    {
        public int NavigationID { get; set; }
        public int ClienteID { get; set; }
        public int CavaloID { get; set; }
        public int ProdutoID { get; set; }
        public int FinancaID { get; set; }

        public Produto Produto { get; set; }
        public Cavalo Cavalo { get; set; }
        public Financa Financa { get; set; }
        public Cliente Cliente { get; set; }
    }
}
