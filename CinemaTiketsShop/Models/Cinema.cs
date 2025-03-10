﻿using CinemaTiketsShop.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CinemaTiketsShop.Models
{
    public class Cinema : IEntityBase
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cinema Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Descrption")]
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Logo Picture")]
        public string LogoUrl { get; set; } = string.Empty;

        [AllowNull]
        public string? PublicId { get; set; }

        //Relationships
        public List<Movie>? Movies { get; set; }
        public List<CinemaHall>? Halls { get; set; }
    }
}
