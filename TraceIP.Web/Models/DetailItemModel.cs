using System.ComponentModel.DataAnnotations;

namespace TraceIP.Web.Models
{
    public class DetailItemModel
    {
        [Display(Name = "Ciudad")]
        public string Country { get; set; }

        [Display(Name = "Distancia promedio")]
        public string DistanceKms { get; set; }

        [Display(Name = "Invocaciones")]
        public int Invocation { get; set; }
    }
}
