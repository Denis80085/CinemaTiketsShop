﻿using System.ComponentModel.DataAnnotations;

namespace CinemaTiketsShop.ViewModels.ActorVMs
{
    public class CreateActorViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(270, MinimumLength = 2, ErrorMessage = "Full name must contain at least 2 and maximum 70 chars")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Biography")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Biography must contain at least 2 and maximum 500 chars")]
        public string? Bio { get; set; }

        public IFormFile? Foto { get; set; }

        [Url]
        public string? FotoUrl { get; set; }
    }
}
