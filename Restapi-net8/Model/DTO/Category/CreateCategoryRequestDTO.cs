using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Restapi_net8.Model.DTO.Category
{
    public class CreateCategoryRequestDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string name { get; set; }

        [MaxLength(100, ErrorMessage = "Category Alias name can't be longer than 100 characters")]
        public string? categoryAliasName { get; set; }

        [MaxLength(100, ErrorMessage = "Category Description can't be longer than 100 characters")]
        public string? description { get; set; }
        [Required(ErrorMessage = "Url image is Required")]
        public string imageUrl { get; set; }


    }
}
