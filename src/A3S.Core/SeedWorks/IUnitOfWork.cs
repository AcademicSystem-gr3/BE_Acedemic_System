using A3S.Core.Repositories;

namespace A3S.Core.SeedWorks
{
    public interface IUnitOfWork
    {
        IUserLogin UserLogin { get; }
        IRole Role { get; }
        IIdentityUserRole UserRole { get; }
        IUser User { get; }
        IFolder Folder { get; }
        IFile FileContent { get; }
        IBlock Block { get; }
        IClass Class { get; }
        ISubject Subject { get; }
        IBlog Blog { get; }
        ICommentBlog CommentBlog { get; }
        IClassBlog ClassBlog { get; }
        IHomework Homework { get; }
        IHomeworkSubmission HomeworkSubmission { get; }
        ILesson Lesson { get; }
        IQuiz Quiz { get; }
        IQuizQuestion QuizQuestion { get; }
        IQuestion Question { get; }
        IAnswer Answer { get; }
        IClassSubject ClassSubject { get; }
        IClassMember ClassMember { get; }
        Task<int> CompleteAsync();
    }
}
