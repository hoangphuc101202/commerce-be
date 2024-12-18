﻿using System.ComponentModel.DataAnnotations;

namespace Restapi_net8.Model.DTO.Category
{
    public class UpdateCategoryDTO
    {
        [MaxLength(50, ErrorMessage = "must not exceed 50 characters. ")] 
        public string? Name { get; set; }
        [RegularExpression(@"^[\p{L}\p{N}\s]*$", ErrorMessage = "category alias name must be alphanumeric. ")]
        public string? categoryAliasName { get; set; }
        public string? imageUrl { get; set; }
        [MaxLength(100, ErrorMessage = "must not exceed 100 characters. ")]
        public string? description { get; set; }
    }
}
