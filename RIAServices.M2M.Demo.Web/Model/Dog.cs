using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RIAServices.M2M.Demo.Web.Model
{
    public class Dog
    {
        #region Constructors and Destructor

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
            get { return Trainers.ProjectObject1(this, x => x.DogTrainers); }
        }

        public string Name { get; set; }

        public ICollection<Trainer> Trainers { get; set; }

        #endregion
    }
}