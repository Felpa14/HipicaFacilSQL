﻿using System.Drawing;

namespace HipicaFacilSQL.Models
{
    public class Cavalo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public string Idade { get; set; }
        public string Peso { get; set; }
        //public string ?Imagem { get; set; }

        public ICollection<Navigation> ?Navigations { get; set; }
    }
}
