using System.ComponentModel.DataAnnotations;

namespace TraceIP.Web.Models
{
    public class IPRequestModel
    {
        [Display(Name = "Ingrese IP a consultar")]
        [Required(ErrorMessage = "Requerido")]
        [RegularExpression(@"\b(?:(?:2[0-5]{2}|1?\d{1,2})\.){3}(?:2[0-5]{2}|1?\d{1,2})\b", ErrorMessage = "Dirección IP no válida")]
        public string Search { get; set; }

        [Display(Name = "IP")]
        [MinLength(0)]
        public string Ip { get; set; }

        [Display(Name = "Fecha actual")]
        [MinLength(0)]
        public string Current_date { get; set; }

        [Display(Name = "País")]
        [MinLength(0)]
        public string Country { get; set; }

        [Display(Name = "Ciudad")]
        [MinLength(0)]
        public string City { get; set; }

        [Display(Name = "Código ISO")]
        [MinLength(0)]
        public string Iso_code { get; set; }

        [Display(Name = "Idiomas")]
        [MinLength(0)]
        public string Language { get; set; }

        [Display(Name = "Moneda")]
        public string Currency { get; set; }

        [Display(Name = "Hora")]
        public string Country_date { get; set; }

        [Display(Name = "Distancia")]
        public string Distance { get; set; }
    }
}
