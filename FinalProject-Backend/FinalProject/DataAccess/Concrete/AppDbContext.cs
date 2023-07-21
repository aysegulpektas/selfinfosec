using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataAccess.Concrete
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:beufinalproject.database.windows.net,1433;Initial Catalog=beufinal;Persist Security Info=False;User ID=final_user;Password=beun!12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Answer>(x =>
            {
                x.Property(x => x.AnswerId).IsRequired();
                x.Property(x => x.AnswerText).IsRequired(true);
                x.Property(x => x.IsTrue).HasDefaultValue(false);
                x.Property(x => x.QuestionId).IsRequired();
                x.HasOne<Question>(s => s.Question).WithOne().HasForeignKey<Answer>(f => f.QuestionId);
                x.HasKey(p => p.AnswerId);
            });
            modelBuilder.Entity<Answer>().HasOne(e => e.Question).WithMany();

            modelBuilder.Entity<Article>(x =>
            {
                x.Property(x => x.AddedDate).IsRequired(true);
                x.Property(x => x.ArticleId).IsRequired();
                x.Property(x => x.ArticleDescription).IsRequired(false);
                x.Property(x => x.ArticleFilePath).IsRequired(false);
                x.Property(x => x.ContentType).IsRequired(false);
                x.Property(x => x.AddedUser).IsRequired(true);
                x.Property(x => x.SubcategoryId).IsRequired();
                x.Property(x => x.LanguageCode).IsRequired();
                x.HasOne<User>(s => s.User).WithOne().HasForeignKey<Article>(f => f.AddedUser);
                x.HasOne<Subcategory>(s => s.Subcategory).WithOne().HasForeignKey<Article>(f => f.SubcategoryId);
                x.HasKey(x => x.ArticleId);
            });
            modelBuilder.Entity<Article>().HasOne(e => e.User).WithMany();
            modelBuilder.Entity<Article>().HasOne(e => e.Subcategory).WithMany();

            modelBuilder.Entity<Question>(x =>
             {
                 x.Property(x => x.QuestionId).IsRequired(true);
                 x.Property(x => x.AddedDate).IsRequired(true);
                 x.Property(x => x.AddedUser).IsRequired(true);
                 x.Property(x => x.QuestionGroupId).IsRequired(true);
                 x.Property(x => x.QuestionText).IsRequired(true);
                 x.Property(x => x.QuestionType).HasDefaultValue(0);
                 x.HasOne<User>(s => s.User).WithOne().HasForeignKey<Question>(f => f.AddedUser);
                 x.HasOne<QuestionGroup>(s => s.QuestionGroup).WithOne().HasForeignKey<Question>(f => f.QuestionGroupId).OnDelete(DeleteBehavior.Cascade);
                 x.HasKey(x => x.QuestionId);
             });
            modelBuilder.Entity<Question>().HasOne(e => e.User).WithMany();
            modelBuilder.Entity<Question>().HasOne(e => e.QuestionGroup).WithMany();

            modelBuilder.Entity<SequencedImage>(x =>
            {
                x.Property(x => x.SequencedImageId).IsRequired();
                x.Property(x => x.ImagePath).IsRequired(false);
                x.Property(x => x.ArticleId).IsRequired(true);
                x.Property(x => x.Description).IsRequired(false);
                x.Property(x => x.Sequence).IsRequired(true);
                x.Property(x => x.ShowType).HasDefaultValue(0);
                x.HasOne<Article>(s => s.Article).WithOne().HasForeignKey<SequencedImage>(f => f.ArticleId).OnDelete(DeleteBehavior.Cascade);
                x.HasKey(x => x.SequencedImageId);
            });
            modelBuilder.Entity<SequencedImage>().HasOne(e => e.Article).WithMany();

            modelBuilder.Entity<QuestionGroup>(x =>
            {
                x.Property(x => x.QuestionGroupId).IsRequired();
                x.Property(x => x.ArticleId).IsRequired(true);
                x.Property(x => x.GroupTitle).IsRequired(true);
                x.Property(x => x.UseForScore).HasDefaultValue(false);
                x.HasOne<Article>(s => s.Article).WithOne().HasForeignKey<QuestionGroup>(f => f.ArticleId).OnDelete(DeleteBehavior.Cascade);
                x.HasKey(x => x.QuestionGroupId);
            });
            modelBuilder.Entity<QuestionGroup>().HasOne(e => e.Article).WithMany();

            modelBuilder.Entity<ResetCode>(x =>
            {
                x.Property(x => x.ResetCodeId).IsRequired();
                x.Property(x => x.UserId).IsRequired();
                x.Property(x => x.PasswordResetCode).IsRequired();
                x.Property(x => x.Expiration).IsRequired();
                x.Property(x => x.IsUsed).HasDefaultValue(false);
                x.HasOne<User>(s => s.User).WithOne().HasForeignKey<ResetCode>(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
                x.HasKey(x => x.ResetCodeId);
            });
            modelBuilder.Entity<ResetCode>().HasOne(e => e.User).WithMany();

            modelBuilder.Entity<Role>(x =>
            {
                x.Property(x => x.RoleId).IsRequired();
                x.Property(x => x.RoleName).IsRequired();
                x.HasKey(x => x.RoleId);
            });



            modelBuilder.Entity<UserAnswers>(x =>
            {
                x.Property(x => x.AnswersId).IsRequired(true);
                x.Property(x => x.QuestionId).IsRequired(true);
                x.Property(x => x.UserId).IsRequired(true);
                x.HasOne<User>(s => s.User).WithOne().HasForeignKey<UserAnswers>(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
                x.HasOne<Question>(s => s.Question).WithOne().HasForeignKey<UserAnswers>(f => f.QuestionId).OnDelete(DeleteBehavior.Cascade);
                x.HasOne<Answer>(s => s.Answer).WithOne().HasForeignKey<UserAnswers>(f => f.AnswersId).OnDelete(DeleteBehavior.Cascade);
                x.HasKey(x => x.UserAnswersId);
            });
            modelBuilder.Entity<UserAnswers>().HasOne(e => e.User).WithMany();
            modelBuilder.Entity<UserAnswers>().HasOne(e => e.Question).WithMany();
            modelBuilder.Entity<UserAnswers>().HasOne(e => e.Answer).WithMany();
            modelBuilder.Entity<UserQuiz>(x =>
            {
                x.Property(x => x.UserQuizId).IsRequired();

                x.Property(a => a.ArticleId).IsRequired(true);
                x.Property(a => a.QuestionGroupId).IsRequired(true);
                x.Property(a => a.UserId).IsRequired(true);
                x.Property(x => x.IsCompleted).HasDefaultValue(false);
                x.Property(x => x.StartDate).IsRequired(false);
                x.Property(x => x.FinishDate).IsRequired(false);
                x.HasOne<Article>(s => s.Article).WithOne().HasForeignKey<UserQuiz>(f => f.ArticleId);
                x.HasOne<QuestionGroup>(s => s.QuestionGroup).WithOne().HasForeignKey<UserQuiz>(f => f.QuestionGroupId);
                x.HasOne<User>(s => s.User).WithOne().HasForeignKey<UserQuiz>(f => f.UserId);
                x.HasKey(x => x.UserQuizId);
            });
            modelBuilder.Entity<UserQuiz>().HasOne(e => e.Article).WithMany();
            modelBuilder.Entity<UserQuiz>().HasOne(e => e.QuestionGroup).WithMany();
            modelBuilder.Entity<UserQuiz>().HasOne(e => e.User).WithMany();
            modelBuilder.Entity<Subcategory>(x =>
            {
                x.Property(x => x.SubcategoryId).IsRequired(true);
                x.Property(x => x.SubcategoryName).IsRequired(true);
                x.Property(a => a.CategoryId).IsRequired();
                x.HasOne<Category>(s => s.Category).WithOne().HasForeignKey<Subcategory>(f => f.CategoryId).OnDelete(DeleteBehavior.Cascade);
                x.HasKey(x => x.SubcategoryId);
            });
            modelBuilder.Entity<Subcategory>().HasOne(e => e.Category).WithMany();

            modelBuilder.Entity<User>(x =>
            {
                x.Property(x => x.UserId).IsRequired();
                x.Property(x => x.UserName).IsRequired();
                x.Property(x => x.FirstName).IsRequired();
                x.Property(x => x.Lastname).IsRequired();
                x.Property(x => x.Email).IsRequired();

                x.Property(a => a.Roleld).IsRequired(true);
                x.HasOne<Role>(s => s.Role).WithOne().HasForeignKey<User>(f => f.Roleld);
                x.HasKey(x => x.UserId);
            });
            modelBuilder.Entity<User>().HasOne(e => e.Role).WithMany();


            modelBuilder.Entity<Category>(x =>
            {
                x.Property(x => x.CategoryId).IsRequired();
                x.Property(x => x.CategoryName).IsRequired();
                x.Property(x => x.CategoryDescription).IsRequired(false);
                x.HasKey(x => x.CategoryId);
            });


        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<QuestionGroup> QuestionGroups { get; set; }
        public DbSet<SequencedImage> SequencedImages { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        public DbSet<ResetCode> ResetCodes { get; set; }

    }
}

