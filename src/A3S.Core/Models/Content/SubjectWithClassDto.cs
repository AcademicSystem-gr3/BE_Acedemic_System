using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class SubjectWithClassDto
    {
        public Guid ClassId { get; set; }
        public Subject Subject { get; set; }
        public string TeacherName { get; set; }
        public string teacherEmail {  get; set; }
        public string teacherAvatar { get; set;}
        public string imgTheme { get; set; }
    }
}
