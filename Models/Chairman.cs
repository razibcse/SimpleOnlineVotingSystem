using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Chairman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string ProfilePicPath { get; set; }
        [Required]
        public string SymbolPath { get; set; }
        public int TotalVote { get; set; }
    }
}
