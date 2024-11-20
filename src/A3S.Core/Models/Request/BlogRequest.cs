using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Request
{
    public class BlogRequest
    {
        public string Content { get; set; }
        public Guid classId { get; set; }
    }
}
