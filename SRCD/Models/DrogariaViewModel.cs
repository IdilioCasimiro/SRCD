using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SRCD.Models
{
    public class DrogariaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        [Display(Name = "Telefone 1")]
        public string Telefone1 { get; set; }

        [Display(Name = "Telefone 2")]
        public string Telefone2 { get; set; }
        public string Morada { get; set; }
        //public List<string> Horarios { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N3}")]
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [Display(Name = "Imagem 1")]
        public HttpPostedFileBase Imagem1 { get; set; }

        [Display(Name = "Imagem 2")]
        public HttpPostedFileBase Imagem2 { get; set; }
    }
}
