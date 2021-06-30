using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApiCLOB.Models
{
    public class Document
    {
        [Key] public int Id { get; set; }
        [Required] [StringLength(255)] public string Title { get; set; }
        [Required] [StringLength(255)] public string Author { get; set; }
        [Required] public string Description { get; set; }
    }
}
