using A3S.Core.Repositories;
using A3S.Core.SeedWorks;
using A3S.Data.Repositories;


namespace A3S.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly A3SContext _context;

        public UnitOfWork(A3SContext context)
        {
            _context = context;
            UserLogin = new UserLoginRepository(context);
            Role = new RoleRepository(context);
            UserRole = new UserRoleRepository(context);
            User = new UserRepository(context); 
            Folder = new FolderRepository(context); 
            FileContent = new FileRepository(context);
            Class = new ClassRepository(context);
            Subject = new SubjectRepository(context);
            Block = new BlockRepository(context);
            Homework = new HomeworkRepository(context);
            Blog = new BlogRepository(context);
            CommentBlog = new CommentBlogRepository(context);
            ClassBlog = new ClassBlogRepository(context);
            HomeworkSubmission = new HomeworkSubmissionRepository(context);
            Lesson = new LessonRepository(context);
            Quiz = new QuizRepository(context);
            QuizQuestion = new QuizQuestionRepository(context);
            Question = new QuestionRepository(context); 
            Answer = new AnswerRepository(context);
            QuizRecord = new QuizRecordRepository(context);
            ClassSubject = new ClassSubjectRepository(context);
            ClassMember = new ClassMemberRepository(context);
        }

        public IUserLogin UserLogin { get; private set; }

        public IRole Role { get; private set; }

        public IIdentityUserRole UserRole { get; private set; }

        public IUser User { get; private set; }

        public IFolder Folder { get; private set; }

        public IFile FileContent { get; private set; }

        public IClass Class { get; private set; }

        public ISubject Subject { get; private set; }

        public IBlock Block { get; private set; }

        public IHomework Homework { get; private set; }

        public IBlog Blog { get; private set; }

        public ICommentBlog CommentBlog { get; private set; }

        public IClassBlog ClassBlog { get; private set; }

        public IHomeworkSubmission HomeworkSubmission { get; private set; }

        public ILesson Lesson { get; private set; }

        public IQuiz Quiz { get; private set; } 

        public IQuizQuestion QuizQuestion { get; private set; }

        public IQuestion Question { get; private set; }

        public IAnswer Answer { get; private set; }

        public IQuizRecord QuizRecord { get; private set; } 

        public IClassSubject ClassSubject { get; private set; }

        public IClassMember ClassMember { get; private set; }   

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
