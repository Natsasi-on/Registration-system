using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Models.DataAccess
{
    [ModelMetadataType(typeof(AcademicRecordMetadata))]
    public partial class AcademicRecord
    {
        [NotMapped]
        public StudentRecordContext DataContext { private get; set; }
    
       
        public string CourseDisplayText
        {
            get
            {
                if (CourseCodeNavigation == null && DataContext == null) return "";

                if (CourseCodeNavigation == null)
                    CourseCodeNavigation = DataContext.Courses.Find(this.CourseCode);

                return CourseCodeNavigation.DisplayText;
            }
        }

        public string StudentDisplayText
        {
            get
            {
                if (Student == null && DataContext == null) return "";
                if (Student == null)
                    Student = DataContext.Students.Find(this.StudentId);

                return Student.DisplayText;
            }
        }
    }
}
