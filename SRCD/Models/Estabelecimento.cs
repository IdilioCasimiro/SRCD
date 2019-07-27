using System;
using System.Collections.Generic;
using System.Text;

namespace SRCD.Models
{
    public class Estabelecimento
    {

        public Estabelecimento()
        {

        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Morada { get; set; }
        //public List<string> Horarios { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public byte[] Imagem1 { get; set; }
        public byte[] Imagem2 { get; set; }
        public string Imagem1ContentType { get; set; }
        public string Imagem2ContentType { get; set; }
    }
}
