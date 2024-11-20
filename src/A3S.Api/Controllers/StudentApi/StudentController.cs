using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using A3S.Core.Models.Content;
using A3S.Core.Models.Request;
using A3S.Core.Repositories;
using A3S.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A3S.Api.Controllers.StudentApi
{
    [Route("api/student")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("join-class")]
        [Authorize]
        public async Task<ActionResult> JoinClass(string classCode)
        {
            if (classCode == null)
            {
                return BadRequest();
            }
            Class classFound = _unitOfWork.Class.Find(c=>c.ClassCode == classCode).FirstOrDefault();
            if(classFound == null)
            {
                return BadRequest(new
                {
                    message="Mã lớp sai"
                });
            }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            ClassMember classMember = new ClassMember
            {
                ClassId = classFound.ClassId,
                UserId = user.Id,
            };
            _unitOfWork.ClassMember.Add(classMember);
            await _unitOfWork.CompleteAsync();
            return Ok(classMember);
        }

        [HttpGet]
        [Route("get-subjects")]
        [Authorize]
        public async Task<ActionResult> GetAllSubjectInClass(string className)
        {
            if (className == null)
            {
                return BadRequest();
            }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var subjects = await _unitOfWork.Class.GetSubjectsForUserClasses(user.Id,className);
            return Ok(subjects); 
        }

        [HttpGet]
        [Route("get-classes")]

        public async Task<ActionResult> GetAllClassStudent()
        {

            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            List<Class> classes = await _unitOfWork.Class.GetAllClassStudent(user.Id);
            return Ok(_mapper.Map<List<Class>, List<ClassStudentDto>>(classes));
        }
        [HttpPost]
        [Route("blog")]
        public async Task<ActionResult> CreateBlog([FromBody] BlogRequest blogRequest)
        {
            if (blogRequest == null)
            {
                return BadRequest();
            }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            Blog blog = await _unitOfWork.Blog.CreateBlog(user.Id, blogRequest.Content);
            _unitOfWork.Blog.Add(blog);
            ClassBlog classBlog = await _unitOfWork.ClassBlog.CreateClassBlog(blog.BlogId, blogRequest.classId);
            _unitOfWork.ClassBlog.Add(classBlog);
            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Blog, BlogDto>(blog));
        }

        [HttpGet]
        [Route("class")]
        public async Task<ActionResult> GetClass(string classId)
        {
            if (string.IsNullOrEmpty(classId))
            {
                return BadRequest();
            }
            var classes = _unitOfWork.Class.Find(c=>c.ClassId==Guid.Parse(classId)).FirstOrDefault();
            return Ok(_mapper.Map<Class, ClassDto>(classes));
        }
        [HttpGet]
        [Route("all-blog")]
        public async Task<ActionResult> getBlogs(string classId)
        {
            if (classId == null)
            {
                return BadRequest();
            }
            List<BlogWithUserDto> blogWithUserDtos = await _unitOfWork.Blog.GetAllBlogInClassSubject(Guid.Parse(classId));
            return Ok(blogWithUserDtos);
        }
        [HttpGet]
        [Route("homework-submission")]
        public async Task<ActionResult> GetHomeworkSubmission(string homeworkId)
        {
            if (homeworkId==null) {return BadRequest(); }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            HomeworkSubmission homeworkSubmissions = _unitOfWork.HomeworkSubmission
                .Find(h=>h.UserId==Guid.Parse(userId) && h.HomeworkId == Guid.Parse(homeworkId)).FirstOrDefault();
            return Ok(_mapper.Map<HomeworkSubmission, HomeworkSubmissionDto>(homeworkSubmissions));
        }
        [HttpPost]
        [Route("homework-submission")]
        public async Task<ActionResult> CreateHomeworkSubmission(HomeworkSubmissionRequest homeworkSubmissionRequest)
        {
            if(homeworkSubmissionRequest==null)
            {
                return BadRequest();
            }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            HomeworkSubmission homeworkSubmission = new HomeworkSubmission
            {
                SubmissionId = Guid.NewGuid(),
                HomeworkId = homeworkSubmissionRequest.HomeworkId,
                UserId = user.Id,
                SubmittedAt = DateTime.Now,
                status = homeworkSubmissionRequest.status,
            };
            return Ok(_mapper.Map<HomeworkSubmission, HomeworkSubmissionDto>(homeworkSubmission));
        }
    }
}
