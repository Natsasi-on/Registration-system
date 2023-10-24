using System.ComponentModel.DataAnnotations;

namespace Lab6.Models.DataAccess
{
    public class AcademicRecordMetadata
    {
        [Required(ErrorMessage = "Must enter a grade.")]
        [Range(0, 100, ErrorMessage = "Must between 0 and 100")]
        [RegularExpression(@"[0-9]{2,}", ErrorMessage = "The value is not valid for Grade")]
        public int? Grade { get; set; }
    }
}
