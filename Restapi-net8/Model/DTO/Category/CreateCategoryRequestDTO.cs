using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Category
{
    public class CreateCategoryRequestDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UrlHandle is Required")]
        [MaxLength(100, ErrorMessage = "UrlHandle can't be longer than 100 characters")]
        public string UrlHandle { get; set; }

    }
}
