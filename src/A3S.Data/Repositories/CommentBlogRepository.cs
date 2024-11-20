using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    internal class CommentBlogRepository : RepositoryBase<CommentBlog, Guid>, ICommentBlog
    {
        public CommentBlogRepository(A3SContext context) : base(context)
        {
        }
    }
}
