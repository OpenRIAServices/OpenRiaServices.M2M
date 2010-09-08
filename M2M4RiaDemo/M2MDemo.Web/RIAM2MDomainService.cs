 

// RIAM2MShared.ttinclude has been located and loaded.

 

#pragma warning disable 618

#region Domain Service

namespace M2MDemo.Web
{
	using System;
	using System.Linq;
	using System.Data;
	using System.Data.Objects;
	using M2MDemo.Web;
	
	public partial class M2MDemoDomainService
	{
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public IQueryable<PatientDoctor> GetPatientDoctorQuery()
		{
			throw new System.NotImplementedException();
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void InsertPatientDoctor(PatientDoctor linkEntity)
		{
			// ** Process Patient end **
			Patient end1Entity;
			
			if (linkEntity.Patient != null)
			{
				// If a reference of Patient is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Patient;
			}
            else
            {
                // If there is no reference to Patient in the linkEntity, then build Patient from the PatientId that has been passed in the linkEntity.
				//
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
                end1Entity = new Patient() { Id = linkEntity.PatientId };
            }
			
			ObjectStateEntry end1StateEntry;
			
			// Check to see if Patient is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Entity, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Patient is already being tracked by the object context, then use the instance of Patient that is being tracked instead of the current Patient
                end1Entity = end1StateEntry.Entity as Patient;
            }
            else
            {
                // If Patient is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Patients", end1Entity);
            }
				
				
			// ** Process Doctor **
            Doctor end2Entity;

            if (linkEntity.Doctor != null)
            {
                // If a reference of Doctor is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Doctor;
            }
            else
            {
                // If there is no reference to Doctor in the linkEntity, then build Doctor from the DoctorId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Doctor() { Id = linkEntity.DoctorId };
            }
		
			ObjectStateEntry end2StateEntry;

            // Check to see if Doctor is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Entity, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Doctor is already being tracked by the object context, then use the instance of Doctor that is being tracked instead of the current Doctor
                end2Entity = end2StateEntry.Entity as Doctor;
            }
            else
            {
                // If Doctor is not being tracked by the object context, then attach it.
                ObjectContext.AttachTo("Doctors", end2Entity);
            }
	
            // ** Add Relationship **
			end1Entity.DoctorSet.Add(end2Entity);
		}
		
		[Obsolete("This method is only intended for use by the RIA M2M solution")]
		public void DeletePatientDoctor(PatientDoctor linkEntity)
		{
			// ** Process Patient end **
			Patient end1Entity;
			
			if (linkEntity.Patient != null)
			{
				// If a reference of Patient is in the linkEntity that has been passed to this method, then get it.
				end1Entity = linkEntity.Patient;
			}
            else
            {
                // If there is no reference to Patient in the linkEntity, then build Patient from the PatientId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end1Entity = new Patient() { Id = linkEntity.PatientId };
            }
			
			ObjectStateEntry end1StateEntry;
			
            EntityKey end1Key = ObjectContext.CreateEntityKey("Patients", end1Entity);
			
			// Check to see if Patient is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end1Key, out end1StateEntry);

			if (end1StateEntry != null && end1StateEntry.State != EntityState.Detached)
            {
                // If Patient is already being tracked by the object context, then use the instance of Patient that is being tracked instead of the current Patient
                end1Entity = end1StateEntry.Entity as Patient;
            }
				
			// ** Process Doctor **
            Doctor end2Entity;

            if (linkEntity.Doctor != null)
            {
                // If a reference of Doctor is in the linkEntity that has been passed to this method, then get it.
                end2Entity = linkEntity.Doctor;
            }
            else
            {
                // If there is no reference to Doctor in the linkEntity, then build Doctor from the DoctorId that has been passed in the linkEntity.
                //
				// Note: In the situation where the entity that we are dealing with is abstract, then use one of it's 
				// derived entities to act as the concrete type for the end.  A derived entity is used in this situation 
				// because you can't instantiate an abstract entity.  The derived entity that you use is not important,
				// since all derived entities will posses the same many to many relationship from the base entity.
				end2Entity = new Doctor() { Id = linkEntity.DoctorId };
            }
		
			ObjectStateEntry end2StateEntry;
            
			EntityKey end2Key = ObjectContext.CreateEntityKey("Doctors", end2Entity);

            // Check to see if Doctor is already being tracked by they current entity framework object context.
            ObjectContext.ObjectStateManager.TryGetObjectStateEntry(end2Key, out end2StateEntry);

            if (end2StateEntry != null && end2StateEntry.State != EntityState.Detached)
            {
                // If Doctor is already being tracked by the object context, then use the instance of Doctor that is being tracked instead of the current Doctor
                end2Entity = end2StateEntry.Entity as Doctor;
            }
	
			// ** Attach Patient to the Object Context if it wasnt already attached **
            if (end1StateEntry == null || end1StateEntry.State == EntityState.Detached)
            {
                // Build many to many relationship between Patient and Doctor so it can be removed after being attached.
                end1Entity.DoctorSet.Add(end2Entity);

                // Attach Patient (Doctor will be attached indrectly through this method)
                ObjectContext.AttachTo("Patients", end1Entity);
            }

            // ** Remove Relationship **
            end1Entity.DoctorSet.Remove(end2Entity);
		
		}
		

	}
}

#endregion

#pragma warning restore 618

