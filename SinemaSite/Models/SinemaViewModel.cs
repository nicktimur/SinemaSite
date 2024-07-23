using Microsoft.AspNetCore.Mvc;

namespace SinemaSite.Models
{
    public class SinemaViewModel
    {
        public Sinema Sinema { get; set; }
        public SalonAddingModel SalonAddingModel { get; set; }
    }
}
