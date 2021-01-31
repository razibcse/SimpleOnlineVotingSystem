using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class CreateChairmanCadidate
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public IFormFile ProfilePicPath { get; set; }
        [Required]
        public IFormFile SymbolPath { get; set; }
    }
}
