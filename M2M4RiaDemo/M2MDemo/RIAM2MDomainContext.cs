 

// RIAM2MShared.ttinclude has been located and loaded.


#pragma warning disable 618

#region Domain Context

namespace M2MDemo.Web
{
	using M2MDemo.Web;
	
    public partial class M2MDemoDomainContext
    {
        partial void OnCreated()
        {
			EntityContainer.GetEntitySet<Patient>().EntityAdded +=
				(sender, args) =>
				{
					args.Entity.PatientDoctorToDoctorRemoved = (p) => EntityContainer.GetEntitySet<PatientDoctor>().Remove(p);
				};
				
			EntityContainer.GetEntitySet<Doctor>().EntityAdded +=
				(sender, args) =>
				{
					args.Entity.PatientDoctorToPatientRemoved = (p) => EntityContainer.GetEntitySet<PatientDoctor>().Remove(p);
				};
				
		}
	}
}

#endregion

#region Entities


namespace M2MDemo.Web
{
	using M2MDemo.Web;
	using RIAM2M.Web.Services.RIAM2MTools;
	using System;
	
	public partial class Patient
	{
	
		//
		// Code relating to the managing of the 'PatientDoctor' association from 'Patient' to 'Doctor'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'PatientDoctorToDoctor' association records from the 'Patient' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<PatientDoctor> PatientDoctorToDoctorRemoved;
		
		private M2MEntityCollection<PatientDoctor, Doctor> _DoctorSet;
		
		public M2MEntityCollection<PatientDoctor, Doctor> DoctorSet
		{
			get
			{
				if(_DoctorSet == null)
				{
					_DoctorSet = new M2MEntityCollection<PatientDoctor, Doctor>(this.PatientDoctorToDoctor, r => r.Doctor, (r, t2) => r.Doctor = t2, r => r.Patient = this, RemovePatientDoctorToDoctor);
				}
				
				return _DoctorSet;
			}
		}
		
		private void RemovePatientDoctorToDoctor(PatientDoctor r)
		{
			if(PatientDoctorToDoctorRemoved == null)
			{
				this.PatientDoctorToDoctor.Remove(r);
			}
			else
			{
				PatientDoctorToDoctorRemoved(r);
			}
		}
	}
	
	public partial class Doctor
	{
	
		//
		// Code relating to the managing of the 'PatientDoctor' association from 'Doctor' to 'Patient'
		//
		
		/// <summary>
		/// Provides a point for the Domain Context to hook into to manage the removal of 'PatientDoctorToPatient' association records from the 'Doctor' entity. Do not register to this action in your code.
		/// </summary>
		[Obsolete("This action is only intended for use by the RIA M2M solution.")]
		public Action<PatientDoctor> PatientDoctorToPatientRemoved;
		
		private M2MEntityCollection<PatientDoctor, Patient> _PatientSet;
		
		public M2MEntityCollection<PatientDoctor, Patient> PatientSet
		{
			get
			{
				if(_PatientSet == null)
				{
					_PatientSet = new M2MEntityCollection<PatientDoctor, Patient>(this.PatientDoctorToPatient, r => r.Patient, (r, t2) => r.Patient = t2, r => r.Doctor = this, RemovePatientDoctorToPatient);
				}
				
				return _PatientSet;
			}
		}
		
		private void RemovePatientDoctorToPatient(PatientDoctor r)
		{
			if(PatientDoctorToPatientRemoved == null)
			{
				this.PatientDoctorToPatient.Remove(r);
			}
			else
			{
				PatientDoctorToPatientRemoved(r);
			}
		}
	}
	
}


#endregion

#region M2MEntityCollection

namespace RIAM2M.Web.Services.RIAM2MTools
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
    public class M2MEntityCollection<JoinType, TEntity> : IEnumerable<TEntity>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
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
        public M2MEntityCollection(EntityCollection<JoinType> entityList, Func<JoinType, TEntity> getEntity,
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

#pragma warning restore 618


