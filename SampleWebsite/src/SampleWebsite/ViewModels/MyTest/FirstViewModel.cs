using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebsite.ViewModels.MyTest
{
    public class FirstViewModel
    {
        public const int MinimumField1Length = 2;
        public const int MaximumField1Length = 128;

        [Required]
        [StringLength(MaximumField1Length, MinimumLength = MinimumField1Length,
            ErrorMessage = "Field 1 must be between 2 and 128 characters")]
        [RegularExpression("^[a-zA-Z0-9-:.+%_#*?!(),=@;$']+$",
            ErrorMessage = "Field 1 should only contain letters, numbers and special characters")]
        public string Field1 { get; set; }

        [Required]
        public string Field2 { get; set; }
    }
}
