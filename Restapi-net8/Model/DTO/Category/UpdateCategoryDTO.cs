using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Category
{
    public class UpdateCategoryDTO
    {
        [MaxLength(50, ErrorMessage = "must not exceed 50 characters. ")] 
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
