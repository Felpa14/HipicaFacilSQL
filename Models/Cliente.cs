namespace HipicaFacilSQL.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set;}
        public string Endereco { get; set;}
        public string Cpf { get; set; }
        public string Telefone { get; set;}

        public ICollection<Navigation> Navigations { get; set; }
    }
}
