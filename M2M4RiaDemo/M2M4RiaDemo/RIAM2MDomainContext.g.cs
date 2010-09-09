

 

// RIAM2MShared.ttinclude has been located and loaded.


// Instruct compiler not to warn about usage of obsolete members, because using them is intended.
#pragma warning disable 618

#region Domain Context

namespace M2M4RiaDemo.Web.Service
{
	using M2M4RiaDemo.Web.Model;
	
    public partial class DemoContext
    {
        partial void OnCreated()
        {
            // Install a handler on this entity container that installs a remove action on each added entity
			// to correctly remove an m2m relation
			EntityContainer.GetEntitySet<Animal>().EntityAdded +=
				(sender, args) =>
				{
					args.Entity.AnimalVetToVetRemoved = (p) => EntityContainer.GetEntitySet<AnimalVet>().Remove(p);
				};
			// Installs a handler on this entity container that removes a remove action on each removed entity.
			EntityContainer.GetEntitySet<Animal>().EntityRemoved +=
				(sender, args) => args.Entity.AnimalVetToVetRemoved = null;
            // Install a handler on this entity container that installs a remove action on each added entity
			// to correctly remove an m2m relation
			EntityContainer.GetEntitySet<Vet>().EntityAdded +=
				(sender, args) =>
				{
					args.Entity.AnimalVetToAnimalRemoved = (p) => EntityContainer.GetEntitySet<AnimalVet>().Remove(p);
				};
			// Installs a handler on this entity container that removes a remove action on each removed entity.
			EntityContainer.GetEntitySet<Vet>().EntityRemoved +=
				(sender, args) => args.Entity.AnimalVetToAnimalRemoved = null;
            // Install a handler on this entity container that installs a remove action on each added entity
			// to correctly remove an m2m relation
			EntityContainer.GetEntitySet<Trainer>().EntityAdded +=
				(sender, args) =>
				{
					args.Entity.DogTrainerToDogRemoved = (p) => EntityContainer.GetEntitySet<DogTrainer>().Remove(p);
				};
			// Installs a handler on this entity container that removes a remove action on each removed entity.
			EntityContainer.GetEntitySet<Trainer>().EntityRemoved +=
				(sender, args) => args.Entity.DogTrainerToDogRemoved = null;
			EntityContainer.GetEntitySet<Animal>().EntityAdded +=
				(sender, args) =>
				{
					if(args.Entity is Dog)
					{
						var entity = args.Entity as Dog;
						
						entity.DogFireHydrantToFireHydrantRemoved = (p) => EntityContainer.GetEntitySet<DogFireHydrant>().Remove(p);
					}
				};
				
			EntityContainer.GetEntitySet<Animal>().EntityAdded +=
				(sender, args) =>
				{
					if(args.Entity is Dog)
					{
						var entity = args.Entity as Dog;
						
						entity.DogTrainerToTrainerRemoved = (p) => EntityContainer.GetEntitySet<DogTrainer>().Remove(p);
					}
				};
				
		}
	}
}

#endregion

namespace M2M4RiaDemo.Web.Model
{
	#region Entities
	
	using M2M4RiaDemo.Web.Service;
	using System;
	using M2M4Ria;
	
	public partial class Animal
	{
	
		//
		// Code relating to the managing of the 'AnimalVet' association from 'Animal' to 'Vet'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'AnimalVetToVet' association records from the 'Animal' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<AnimalVet> AnimalVetToVetRemoved;
		
		private EntityCollection<AnimalVet, Vet> _Vets;
		
		public EntityCollection<AnimalVet, Vet> Vets
		{
			get
			{
				if(_Vets == null)
				{
					_Vets = new EntityCollection<AnimalVet, Vet>(this.AnimalVetToVet, r => r.Vet, (r, t2) => r.Vet = t2, r => r.Animal = this, RemoveAnimalVetToVet);
				}
				
				return _Vets;
			}
		}
		
		private void RemoveAnimalVetToVet(AnimalVet r)
		{
			if(AnimalVetToVetRemoved == null)
			{
				this.AnimalVetToVet.Remove(r);
			}
			else
			{
				AnimalVetToVetRemoved(r);
			}
		}
	}
	
	public partial class Vet
	{
	
		//
		// Code relating to the managing of the 'AnimalVet' association from 'Vet' to 'Animal'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'AnimalVetToAnimal' association records from the 'Vet' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<AnimalVet> AnimalVetToAnimalRemoved;
		
		private EntityCollection<AnimalVet, Animal> _Animals;
		
		public EntityCollection<AnimalVet, Animal> Animals
		{
			get
			{
				if(_Animals == null)
				{
					_Animals = new EntityCollection<AnimalVet, Animal>(this.AnimalVetToAnimal, r => r.Animal, (r, t2) => r.Animal = t2, r => r.Vet = this, RemoveAnimalVetToAnimal);
				}
				
				return _Animals;
			}
		}
		
		private void RemoveAnimalVetToAnimal(AnimalVet r)
		{
			if(AnimalVetToAnimalRemoved == null)
			{
				this.AnimalVetToAnimal.Remove(r);
			}
			else
			{
				AnimalVetToAnimalRemoved(r);
			}
		}
	}
	
	public partial class FireHydrant
	{
	
	}
	
	public partial class Trainer
	{
	
		//
		// Code relating to the managing of the 'DogTrainer' association from 'Trainer' to 'Dog'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'DogTrainerToDog' association records from the 'Trainer' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<DogTrainer> DogTrainerToDogRemoved;
		
		private EntityCollection<DogTrainer, Dog> _Dogs;
		
		public EntityCollection<DogTrainer, Dog> Dogs
		{
			get
			{
				if(_Dogs == null)
				{
					_Dogs = new EntityCollection<DogTrainer, Dog>(this.DogTrainerToDog, r => r.Dog, (r, t2) => r.Dog = t2, r => r.Trainer = this, RemoveDogTrainerToDog);
				}
				
				return _Dogs;
			}
		}
		
		private void RemoveDogTrainerToDog(DogTrainer r)
		{
			if(DogTrainerToDogRemoved == null)
			{
				this.DogTrainerToDog.Remove(r);
			}
			else
			{
				DogTrainerToDogRemoved(r);
			}
		}
	}
	
	public partial class Food
	{
	
	}
	
	public partial class Dog
	{
	
		//
		// Code relating to the managing of the 'DogFireHydrant' association from 'Dog' to 'FireHydrant'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'DogFireHydrantToFireHydrant' association records from the 'Dog' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<DogFireHydrant> DogFireHydrantToFireHydrantRemoved;
		
		private EntityCollection<DogFireHydrant, FireHydrant> _FireHydrants;
		
		public EntityCollection<DogFireHydrant, FireHydrant> FireHydrants
		{
			get
			{
				if(_FireHydrants == null)
				{
					_FireHydrants = new EntityCollection<DogFireHydrant, FireHydrant>(this.DogFireHydrantToFireHydrant, r => r.FireHydrant, (r, t2) => r.FireHydrant = t2, r => r.Dog = this, RemoveDogFireHydrantToFireHydrant);
				}
				
				return _FireHydrants;
			}
		}
		
		private void RemoveDogFireHydrantToFireHydrant(DogFireHydrant r)
		{
			if(DogFireHydrantToFireHydrantRemoved == null)
			{
				this.DogFireHydrantToFireHydrant.Remove(r);
			}
			else
			{
				DogFireHydrantToFireHydrantRemoved(r);
			}
		}
		//
		// Code relating to the managing of the 'DogTrainer' association from 'Dog' to 'Trainer'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'DogTrainerToTrainer' association records from the 'Dog' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<DogTrainer> DogTrainerToTrainerRemoved;
		
		private EntityCollection<DogTrainer, Trainer> _Trainers;
		
		public EntityCollection<DogTrainer, Trainer> Trainers
		{
			get
			{
				if(_Trainers == null)
				{
					_Trainers = new EntityCollection<DogTrainer, Trainer>(this.DogTrainerToTrainer, r => r.Trainer, (r, t2) => r.Trainer = t2, r => r.Dog = this, RemoveDogTrainerToTrainer);
				}
				
				return _Trainers;
			}
		}
		
		private void RemoveDogTrainerToTrainer(DogTrainer r)
		{
			if(DogTrainerToTrainerRemoved == null)
			{
				this.DogTrainerToTrainer.Remove(r);
			}
			else
			{
				DogTrainerToTrainerRemoved(r);
			}
		}
	}
	
	#endregion

	#region EntityCollection

	namespace M2M4Ria
	{
		using System;
		using System.Collections;
		using System.Collections.Generic;
		using System.Collections.Specialized;
		using System.ComponentModel;
		using System.Linq;
		using System.ServiceModel.DomainServices.Client;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="JoinType"></typeparam>
		/// <typeparam name="TEntity"></typeparam>
		public class EntityCollection<JoinType, TEntity> : IEnumerable<TEntity>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
			where JoinType : Entity, new()
			where TEntity : Entity
		{
			EntityCollection<JoinType> entityList;
			Func<JoinType, TEntity> getEntity;
			Action<JoinType, TEntity> setEntity;
			Action<JoinType> setParent;
			Action<JoinType> removeAction;
			/// <summary>
			/// 
			/// </summary>
			/// <param name="entityList">The collection of associations to which this collection is connected</param>
			/// <param name="getEntity">The function used to get the entity object out of a join type entity</param>
			/// <param name="setEntity">The function used to set the entity object in a join type entity</param>
			public EntityCollection(EntityCollection<JoinType> entityList, Func<JoinType, TEntity> getEntity,
				Action<JoinType, TEntity> setEntity, Action<JoinType> setParent, Action<JoinType>removeAction)
			{
				this.entityList = entityList;
				this.getEntity = getEntity;
				this.setEntity = setEntity;
				this.setParent = setParent;
				this.removeAction = removeAction;

				entityList.EntityAdded += (a, b) =>
				{
					JoinType jt = b.Entity as JoinType;
					if (EntityAdded != null)
						EntityAdded(this, new EntityCollectionChangedEventArgs<TEntity>(getEntity(jt)));
				};
				entityList.EntityRemoved += (a, b) =>
				{
					JoinType jt = b.Entity as JoinType;
					if (EntityRemoved != null)
						EntityRemoved(this, new EntityCollectionChangedEventArgs<TEntity>(getEntity(jt)));
				};
				((INotifyCollectionChanged)entityList).CollectionChanged += (sender, e) =>
				{
					if (CollectionChanged != null)
						CollectionChanged(this, MakeNotifyCollectionChangedEventArgs(e));
				};
				((INotifyPropertyChanged)entityList).PropertyChanged += (sender, e) =>
				{
					if (PropertyChanged != null)
						PropertyChanged(this, e);
				};
			}

			/// <summary>
			/// Replaces JoinType elements in NotifyCollectionChangedEventArgs by elements of type TEntity
			/// </summary>
			/// <param name="e"></param>
			/// <returns></returns>
			private NotifyCollectionChangedEventArgs MakeNotifyCollectionChangedEventArgs(NotifyCollectionChangedEventArgs e)
			{
				if (e.NewItems != null)
				{
					TEntity entity = getEntity((JoinType)e.NewItems[0]);
					e.NewItems[0] =  entity == null ? entityToAdd : entity;
				}
				if (e.OldItems != null)
				{
					TEntity entity = getEntity((JoinType)e.OldItems[0]);
					e.OldItems[0] = entity;
				} 
				return e;
			}

			public IEnumerator<TEntity> GetEnumerator()
			{
				var x = (from pd in entityList select getEntity(pd));
				return x.ToList().GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			public int Count
			{
				get
				{
					return entityList.Count;
				}
			}

			TEntity entityToAdd = null;
			public void Add(TEntity entity)
			{
				entityToAdd = entity;
				JoinType joinTypeToAdd = new JoinType();
				setParent(joinTypeToAdd);
				setEntity(joinTypeToAdd, entity);
				entityToAdd = null;
			}
			/// <summary>
			/// Use remove on the entityset on the domain context, rather than this functioln
			/// There seems to be a limitation of RIA which requires that associations should be deleted on the domain context
			/// </summary>
			/// <param name="entity"></param>
			public void Remove(TEntity entity)
			{
				JoinType joinTypeToRemove = entityList.SingleOrDefault(jt => getEntity(jt) == entity);
				if (joinTypeToRemove != null)
					//                entityList.Remove(joinTypeToRemove);
					removeAction(joinTypeToRemove);
			}

			public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityAdded;
			public event EventHandler<EntityCollectionChangedEventArgs<TEntity>> EntityRemoved;
			public event NotifyCollectionChangedEventHandler CollectionChanged;
			public event PropertyChangedEventHandler PropertyChanged;
		}
	}

	#endregion

}

// Restore compiler warnings when using obsolete methods
#pragma warning restore 618



