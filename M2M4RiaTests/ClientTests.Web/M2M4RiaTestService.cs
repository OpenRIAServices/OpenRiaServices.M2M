
namespace ClientTests.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the DemoModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class M2M4RiaTestService : LinqToEntitiesDomainService<M2M4RiaTestModelContainer>
    {
        /// <summary>
        /// Generate the test database.
        /// </summary>
        [Invoke]
        public void CreateDataBase()
        {
            if(this.ObjectContext.DatabaseExists() == false)
            {
                this.ObjectContext.CreateDatabase();
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Animals' query.
        public IQueryable<Animal> GetAnimals()
        {
            return this.ObjectContext.Animals;
        }

        public void InsertAnimal(Animal animal)
        {
            if ((animal.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(animal, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Animals.AddObject(animal);
            }
        }

        public void UpdateAnimal(Animal currentAnimal)
        {
            this.ObjectContext.Animals.AttachAsModified(currentAnimal, this.ChangeSet.GetOriginal(currentAnimal));
        }

        public void DeleteAnimal(Animal animal)
        {
            if ((animal.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Animals.Attach(animal);
            }
            this.ObjectContext.Animals.DeleteObject(animal);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ChewedShoes' query.
        public IQueryable<ChewedShoe> GetChewedShoes()
        {
            return this.ObjectContext.ChewedShoes;
        }

        public void InsertChewedShoe(ChewedShoe chewedShoe)
        {
            if ((chewedShoe.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(chewedShoe, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ChewedShoes.AddObject(chewedShoe);
            }
        }

        public void UpdateChewedShoe(ChewedShoe currentChewedShoe)
        {
            this.ObjectContext.ChewedShoes.AttachAsModified(currentChewedShoe, this.ChangeSet.GetOriginal(currentChewedShoe));
        }

        public void DeleteChewedShoe(ChewedShoe chewedShoe)
        {
            if ((chewedShoe.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ChewedShoes.Attach(chewedShoe);
            }
            this.ObjectContext.ChewedShoes.DeleteObject(chewedShoe);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FireHydrants' query.
        public IQueryable<FireHydrant> GetFireHydrants()
        {
            return this.ObjectContext.FireHydrants;
        }

        public void InsertFireHydrant(FireHydrant fireHydrant)
        {
            if ((fireHydrant.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fireHydrant, EntityState.Added);
            }
            else
            {
                this.ObjectContext.FireHydrants.AddObject(fireHydrant);
            }
        }

        public void UpdateFireHydrant(FireHydrant currentFireHydrant)
        {
            this.ObjectContext.FireHydrants.AttachAsModified(currentFireHydrant, this.ChangeSet.GetOriginal(currentFireHydrant));
        }

        public void DeleteFireHydrant(FireHydrant fireHydrant)
        {
            if ((fireHydrant.EntityState == EntityState.Detached))
            {
                this.ObjectContext.FireHydrants.Attach(fireHydrant);
            }
            this.ObjectContext.FireHydrants.DeleteObject(fireHydrant);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Foods' query.
        public IQueryable<Food> GetFoods()
        {
            return this.ObjectContext.Foods;
        }

        public void InsertFood(Food food)
        {
            if ((food.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(food, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Foods.AddObject(food);
            }
        }

        public void UpdateFood(Food currentFood)
        {
            this.ObjectContext.Foods.AttachAsModified(currentFood, this.ChangeSet.GetOriginal(currentFood));
        }

        public void DeleteFood(Food food)
        {
            if ((food.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Foods.Attach(food);
            }
            this.ObjectContext.Foods.DeleteObject(food);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Owners' query.
        public IQueryable<Owner> GetOwners()
        {
            return this.ObjectContext.Owners;
        }

        public void InsertOwner(Owner owner)
        {
            if ((owner.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(owner, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Owners.AddObject(owner);
            }
        }

        public void UpdateOwner(Owner currentOwner)
        {
            this.ObjectContext.Owners.AttachAsModified(currentOwner, this.ChangeSet.GetOriginal(currentOwner));
        }

        public void DeleteOwner(Owner owner)
        {
            if ((owner.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Owners.Attach(owner);
            }
            this.ObjectContext.Owners.DeleteObject(owner);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Trainers' query.
        public IQueryable<Trainer> GetTrainers()
        {
            return this.ObjectContext.Trainers;
        }

        public void InsertTrainer(Trainer trainer)
        {
            if ((trainer.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(trainer, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Trainers.AddObject(trainer);
            }
        }

        public void UpdateTrainer(Trainer currentTrainer)
        {
            this.ObjectContext.Trainers.AttachAsModified(currentTrainer, this.ChangeSet.GetOriginal(currentTrainer));
        }

        public void DeleteTrainer(Trainer trainer)
        {
            if ((trainer.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Trainers.Attach(trainer);
            }
            this.ObjectContext.Trainers.DeleteObject(trainer);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Vets' query.
        public IQueryable<Vet> GetVets()
        {
            return this.ObjectContext.Vets;
        }

        public void InsertVet(Vet vet)
        {
            if ((vet.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(vet, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Vets.AddObject(vet);
            }
        }

        public void UpdateVet(Vet currentVet)
        {
            this.ObjectContext.Vets.AttachAsModified(currentVet, this.ChangeSet.GetOriginal(currentVet));
        }

        public void DeleteVet(Vet vet)
        {
            if ((vet.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Vets.Attach(vet);
            }
            this.ObjectContext.Vets.DeleteObject(vet);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Vets' query.
        public IQueryable<MarkedTerritory> GetMarkedTerritories()
        {
            return this.ObjectContext.MarkedTerritories;
        }

        public void InsertMarkedTerritory( MarkedTerritory markedTerritory )
        {
            if( (markedTerritory.EntityState != EntityState.Detached) )
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState( markedTerritory, EntityState.Added );
            }
            else
            {
                this.ObjectContext.MarkedTerritories.AddObject( markedTerritory );
            }
        }

        public void UpdateMarkedTerritory( MarkedTerritory currentMarkedTerritory )
        {
            this.ObjectContext.MarkedTerritories.AttachAsModified( currentMarkedTerritory, this.ChangeSet.GetOriginal( currentMarkedTerritory ) );
        }

        public void DeleteMarkedTerritory( MarkedTerritory markedTerritory )
        {
            if( (markedTerritory.EntityState == EntityState.Detached) )
            {
                this.ObjectContext.MarkedTerritories.Attach( markedTerritory );
            }
            this.ObjectContext.MarkedTerritories.DeleteObject( markedTerritory );
        }
    }
}


