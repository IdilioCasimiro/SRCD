using System;
using System.ComponentModel.DataAnnotations;

namespace SRCD.Models
{
    public class MailMessageModel
    {
        public int ClinicId { get; set; }
        [Display(Name = "Nome *")]
        public string Nome { get; set; }
        
        [Display(Name = "Email *")]
        public string Email { get; set; }
        
        [Display(Name = "Mensagem *")]
        public string Mensagem { get; set; }

        [Display(Name = "Nº de animais *")]
        public int NAnimais { get; set; }

        [Display(Name = "Data *")]
        public DateTime Data { get; set; }
        
        public string Destinatario { get; set; }

        public string StatusMessage { get; set; }
    }
}
