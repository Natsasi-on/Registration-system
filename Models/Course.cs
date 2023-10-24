using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models.DataAccess
{
    public partial class Course
    {
        [NotMapped]
        public string DisplayText { get; set; }
    }
}
