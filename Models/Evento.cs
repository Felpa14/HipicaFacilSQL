namespace HipicaFacilSQL.Models
{
    public class Evento
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public ICollection<Navigation>? Navigations { get; set; }
    }
}
