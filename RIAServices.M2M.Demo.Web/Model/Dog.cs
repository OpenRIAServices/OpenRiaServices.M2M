namespace RIAServices.M2M.Demo.Web.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Dog
    {
        #region Constructors and Destructors

        public Dog()
        {
            Trainers = new Collection<Trainer>();
        }

        #endregion

        #region Public Properties

        public bool ChasesCars { get; set; }

        public int DogId { get; set; }

        public ICollection<DogTrainer> DogTrainers
        {
            get
            {
                return Trainers.ToLinkTable<Dog, Trainer, DogTrainer>(this);
            }
        }

        public string Name { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }

        #endregion
    }
}