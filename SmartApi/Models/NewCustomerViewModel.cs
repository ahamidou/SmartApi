using System.ComponentModel.DataAnnotations;
using SmartApi.Infrastructure;

namespace SmartApi.Models
{
    public class NewCustomerViewModel
    {
        [Required(ErrorMessage = "Please provide an Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a Name")]
        [MinLength(3, ErrorMessage = "Username must be at lease 3 characters")]
        [JsonRedact]
        public string Name { get; set; }

        [Range(18, 65, ErrorMessage = "Must be between 18 and 65")]
        [JsonRedact]
        public int Age { get; set; }
    }
}