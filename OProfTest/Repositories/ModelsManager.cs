using System.Collections.Generic;
using System.Data.Entity;
using OProfTest.MVVM.Model;

namespace OProfTest.Repositories
{
    public partial class ModelsManager : DbContext
    {
        public ModelsManager() : base("name=ModelsManager") { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestImage> TestImages { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>()
                .HasRequired(q => q.Test)
                .WithMany()
                .HasForeignKey(q => q.TestID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
                .HasRequired(r => r.User)
                .WithMany(u => (ICollection<Result>)u.Results)
                .HasForeignKey(r => r.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
                .HasRequired(r => r.Test)
                .WithMany()
                .HasForeignKey(r => r.TestID)
                .WillCascadeOnDelete(false);
        }
    }
}
