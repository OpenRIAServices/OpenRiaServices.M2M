using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace RIAServices.M2M.Demo.Web.Model
{
    public class DogTrainerModel : System.Data.Entity.DbContext
    {
        #region Public Properties

        public DbSet<Dog> DogSet { get; set; }

        public DbSet<Trainer> TrainerSet { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>().Property(x => x.DogId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Trainer>().Property(x => x.TrainerId).HasDatabaseGeneratedOption(
                DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Dog>().Ignore(x => x.DogTrainers);
            modelBuilder.Entity<Trainer>().Ignore(x => x.DogTrainers);

            modelBuilder.Entity<Dog>().HasMany(x => x.Trainers).WithMany(x => x.Dogs);
        }

        #endregion
    }
}