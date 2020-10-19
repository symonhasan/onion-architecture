using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OA.Web.Models
{
    public class TouristPlaceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        [Required , Range(1,5)]
        public int Rating { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
