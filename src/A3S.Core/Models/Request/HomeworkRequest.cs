using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Request
{
    public class HomeworkRequest
    {
        public  Guid ClassId { get; set; }
        public  Guid CreateBy { get; set; }
        public string fileName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
