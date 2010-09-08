 

// RIAM2MShared.ttinclude has been located and loaded.

 

#pragma warning disable 618

#region Entities

namespace M2M4RiaDemo.Web  
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.Runtime.Serialization;
	using System.ServiceModel.DomainServices.Server;
	using RIAM2M.Web.Services.RIAM2MTools;

	//
	// Association Entity Types
	//
	[Obsolete("This class is only intended for use by the RIA M2M solution")]
	public partial class PatientDoctor
	{

		// 'PatientDoctorToDoctor' associationSet from 'Doctor.Id' to 'PatientDoctor.DoctorId'
		private int _DoctorId;
		
		[DataMember]
		[Key]
		public int DoctorId
		{
			get
			{
				if(Doctor != null)
				{
					if(_DoctorId != Doctor.Id && _DoctorId == 0)
						_DoctorId = Doctor.Id;
				}
				
				return _DoctorId;
			}
			set
			{
				_DoctorId = value;
			}
		}
		
		[Include]
		[Association("PatientDoctorToDoctor", "DoctorId", "Id", IsForeignKey = true)]
		[DataMember]
		public Doctor Doctor { get; set; }
		
		// 'PatientDoctorToPatient' associationSet from 'Patient.Id' to 'PatientDoctor.PatientId'
		private int _PatientId;

		[DataMember]
		[Key]
		public int PatientId
		{
			get
			{
				if(Patient != null)
				{
					if(_PatientId != Patient.Id && _PatientId == 0)
						_PatientId = Patient.Id;
				}

				return _PatientId;
			}
			set
			{
				_PatientId = value;
			}
		}
		
		[Include]
		[Association("PatientDoctorToPatient", "PatientId", "Id", IsForeignKey = true)]
		[DataMember]
		public Patient Patient { get; set; }

	}
		
	//
	// Regular Entity Types
	//
		
	public partial class Patient
	{
		// 'PatientDoctorToDoctor' associationSet from 'Doctor.Id' to 'PatientDoctor.DoctorId'
		private M2MEntityCollection<PatientDoctor, Doctor> _PatientDoctorToDoctor;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("PatientDoctorToPatient", "Id", "PatientId", IsForeignKey = false)]
		public M2MEntityCollection<PatientDoctor, Doctor> PatientDoctorToDoctor
		{
			get
			{
				if(_PatientDoctorToDoctor == null)
				{
					_PatientDoctorToDoctor = new M2MEntityCollection<PatientDoctor, Doctor>
					(
						DoctorSet,
						(r) => new PatientDoctor { Patient = this, Doctor = r },
						pd => pd.Doctor
					);
				}
				
				return _PatientDoctorToDoctor;
			}
		}
		
	}
		
	public partial class Doctor
	{
		// 'PatientDoctorToPatient' associationSet from 'Patient.Id' to 'PatientDoctor.PatientId'
		private M2MEntityCollection<PatientDoctor, Patient> _PatientDoctorToPatient;
		
		[Obsolete("This property is only intended for use by the RIA M2M solution")]
		[DataMember]
		[Include]
		[Association("PatientDoctorToDoctor", "Id", "DoctorId", IsForeignKey = false)]
		public M2MEntityCollection<PatientDoctor, Patient> PatientDoctorToPatient
		{
			get
			{
				if(_PatientDoctorToPatient == null)
				{
					_PatientDoctorToPatient = new M2MEntityCollection<PatientDoctor, Patient>
					(
						PatientSet,
						(r) => new PatientDoctor { Doctor = this, Patient = r },
						pd => pd.Patient
					);
				}
				
				return _PatientDoctorToPatient;
			}
		}
		
	}
}

#endregion

#region M2MEntityCollection

namespace RIAM2M.Web.Services.RIAM2MTools
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;

    public class M2MEntityCollection<JoinType, TEntity> : IEnumerable<JoinType>
        where JoinType : new()
        where TEntity : class
    {
        private ICollection<TEntity> collection;
        private Func<TEntity, JoinType> newJoinType;
        private Func<JoinType, TEntity> getEntity;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">Entity collection that represents a m2m relation</param>
        /// <param name="newJoinType">The function used to create a new joint type entity and set both elements</param>
        public M2MEntityCollection(ICollection<TEntity> collection,Func<TEntity, JoinType> newJoinType, Func<JoinType, TEntity> getEntity)
        {
            this.collection = collection;
            this.newJoinType = newJoinType;
            this.getEntity = getEntity;
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <summary>
        /// Construct an enumerator by creating JoinType objects for each element in the associated m2m collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<JoinType> GetEnumerator()
        {
            return collection.Select(newJoinType).GetEnumerator();
        }

        /// <summary>
        /// Not clear if this method should have an implementation. It is only called for newly created JoinType objects.
        /// However, the corresponding domainservice operation will already take the appropriate action the add a new association obejct.
        /// Is there a need to also add similar functionality here?
        /// </summary>
        /// <param name="entity"></param>
        public void Add(JoinType entity)
        {
            // Empty
        }
    }
}

#endregion

#pragma warning restore 618
		

