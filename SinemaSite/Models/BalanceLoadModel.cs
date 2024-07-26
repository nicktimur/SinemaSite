using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SinemaSite.Models
{
    public class BalanceLoadModel
    {

        [Required]
        public int YuklenecekMiktar { get; set; }

    }

}
