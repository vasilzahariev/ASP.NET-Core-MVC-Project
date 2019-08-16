using System;
using System.ComponentModel.DataAnnotations;

namespace UltimateMovies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int PictureId { get; set; }

        public virtual Image Picture { get; set; }
    }
}
