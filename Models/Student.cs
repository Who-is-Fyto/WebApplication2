using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    //A PCO (Plain Old C# Object) - no MVC-Specific code needed here.
    //THis is the "M" in MVC. It is just data, no behaviors
    public class Student
    {
        //{get;set;} is a C# "auto-property"- shorthand for a private field plus a public setter and getter
        
        public int Id{get;set;}
        //[Required] means this field cannot be blank. The ErrorMessage is what gets displayed to the user if they leave it empty
        [Required(ErrorMessage = "Please Enter The Student's Name")]
        //[StringLength] caps the max length and can enforce a minimum too
        [StringLength(100,MinimumLength = 2)]
        public string Name{get;set;} = string.Empty;
        [Required]
        [StringLength(50)]
        
        [DataType(DataType.Date)]
        public DateTime EnrollementDate{get;set;}
        public int CourseId {get;set;}
        [ForeignKey("CourseId")]
        public Course? Course{get;set;}
    }
}