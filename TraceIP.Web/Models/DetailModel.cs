using System.ComponentModel.DataAnnotations;

namespace TraceIP.Web.Models
{
    public class DetailModel
    {
        [Display(Name = "Distancia más cercana")]
        public string DistanceMin { get; set; }

        [Display(Name = "Distancia más lejana")]
        public string DistanceMax { get; set; }

        [Display(Name = "Distancia promedio General")]
        public string DistanceAverage { get; set; }
    }
}
