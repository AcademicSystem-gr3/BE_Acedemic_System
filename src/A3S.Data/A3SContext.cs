using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A3S.Data
{
    public class A3SContext : IdentityDbContext<User, Role, Guid>
    {

        public DbSet<Notification> SNotifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizRecord> QuizRecords { get; set; }
        public DbSet<CommentBlog> CommentBlogs { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ClassBlog> ClassBlogs { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<ClassMember> ClassMembers { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<ClassFolder> ClassFolders { get; set; }
        public DbSet<FileContent> Files { get; set; }
        public DbSet<Block> Blocks { get; set; }

        public A3SContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure indexes
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = item.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    item.SetTableName(tableName.Substring(6));
                }
            }      
            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.Title, n.Message });

            modelBuilder.Entity<Subject>()
                .HasIndex(s => new { s.SubjectCode, s.SubjectName });

            modelBuilder.Entity<Folder>()
                .HasIndex(f => f.FolderName);

            modelBuilder.Entity<FileContent>()
                .HasIndex(f => f.FileName);

            modelBuilder.Entity<FileContent>()
                .HasIndex(f => f.FolderId);

            // Configure relationships
            modelBuilder.Entity<ClassBlog>()
                .HasKey(cb => new { cb.ClassId, cb.BlogId });

            modelBuilder.Entity<ClassSubject>()
                .HasKey(cs => new { cs.ClassId, cs.SubjectId });

            modelBuilder.Entity<ClassMember>()
                .HasKey(cm => new { cm.ClassId, cm.UserId });

            modelBuilder.Entity<QuizQuestion>()
                .HasKey(qq => new { qq.QuizId, qq.QuestionId });

            modelBuilder.Entity<ClassFolder>()
                .HasKey(cf => new { cf.ClassId, cf.FolderId });


            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.RecevieUserId);

            modelBuilder.Entity<Class>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Class)
                .HasForeignKey(h => h.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Class>()
                .HasMany(c=>c.Lessons)
                .WithOne(h => h.Class)
                .HasForeignKey(h => h.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Subject>()
                .HasOne(subject => subject.Creator)
                .WithMany()
                .HasForeignKey(subject => subject.CreatorBy)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Block>()
                .HasMany(b=>b.Classes)
                .WithOne(c=>c.Block)
                .HasForeignKey(c => c.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship for Teacher
            modelBuilder.Entity<Subject>()
                .HasOne(subject => subject.Teacher)
                .WithMany(user => user.TaughtSubjects)
                .HasForeignKey(subject => subject.TeacherID)
                .OnDelete(DeleteBehavior.Restrict); // Optional: specify delete behavior
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.QuizRecords)
                .WithOne(qr => qr.Quiz)
                .HasForeignKey(qr => qr.QuizId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l=>l.Quizzes)
                .WithOne(q=>q.Lesson)
                .HasForeignKey(q => q.LessonId) 
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<Homework>()
                .HasMany(h => h.HomeworkSubmissions)
                .WithOne(hs => hs.Homework)
                .HasForeignKey(hs => hs.HomeworkId);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.Files)
                .WithOne(file => file.Folder)
                .HasForeignKey(file => file.FolderId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<Blog>()
                .HasMany(b=>b.CommentBlogs)
                .WithOne(cb=>cb.Blog)
                .HasForeignKey(cb=>cb.BlogID)
                .OnDelete(DeleteBehavior.Cascade);
            // Configure many-to-many relationships
            modelBuilder.Entity<ClassBlog>()
                .HasOne(cb => cb.Class)
                .WithMany(c => c.ClassBlogs)
                .HasForeignKey(cb => cb.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassBlog>()
                .HasOne(cb => cb.Blog)
                .WithMany(b => b.ClassBlogs)
                .HasForeignKey(cb => cb.BlogId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.ClassId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassMember>()
                .HasOne(cm => cm.Class)
                .WithMany(c => c.ClassMembers)
                .HasForeignKey(cm => cm.ClassId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassMember>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.ClassMembers)
                .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Quiz)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuizId);

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Question)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuestionId);

            modelBuilder.Entity<ClassFolder>()
                .HasOne(cf => cf.Class)
                .WithMany(c => c.ClassFolders)
                .HasForeignKey(cf => cf.ClassId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FolderShare>()
                .HasKey(fs => new { fs.UserId, fs.FolderId });

            modelBuilder.Entity<FolderShare>()
                .HasOne(fs => fs.User)
                .WithMany(u => u.FolderShare)
                .HasForeignKey(fs => fs.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // Prevent cascading delete

            modelBuilder.Entity<FolderShare>()
                .HasOne(fs => fs.Folder)
                .WithMany(f => f.FolderShare)
                .HasForeignKey(fs => fs.FolderId)
                .OnDelete(DeleteBehavior.Cascade);  // Allow cascading delete on Folder if needed


            modelBuilder.Entity<ClassFolder>()
                .HasOne(cf => cf.Folder)
                .WithMany(f => f.ClassFolders)
                .HasForeignKey(cf => cf.FolderId)
            .OnDelete(DeleteBehavior.NoAction);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Lấy ra tất cả các thực thể mới được thêm vào
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            // Duyệt qua từng thực thể mới
            foreach (var entityEntry in entities)
            {
                // Lấy thuộc tính "DateCreated" của thực thể
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty("CreatedAt");

                // Nếu thực thể mới và có thuộc tính "DateCreated"
                if (entityEntry.State == EntityState.Added && dateCreatedProp != null)
                {
                    // Cập nhật giá trị của thuộc tính "DateCreated" bằng thời điểm hiện tại
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
                var dateModifiedProp = entityEntry.Entity.GetType().GetProperty("UpdatedAt");
                if (entityEntry.State == EntityState.Modified && dateModifiedProp != null)
                {
                    // Cập nhật giá trị của thuộc tính "DateCreated" bằng thời điểm hiện tại
                    dateModifiedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
