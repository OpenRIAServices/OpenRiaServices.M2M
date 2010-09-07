 

 


namespace M2MDemo.Web
{
    public partial class M2MDemoDomainContext
    {
        /// <summary>
        /// This is needed to enable deletion of associations on the domain context
        /// </summary>
        public static M2MDemoDomainContext Current { get; private set; }

        partial void OnCreated()
        {
            Current = this;
        }
    }
}
namespace M2MDemo.Web {
using M2MDemo.Web;
using M2M.RIA;

	public static class Extensions
	{
	
        /// <summary>
        /// Returns an instance of M2MDomainContext. This is needed to enable deletion of associations on the domain context.
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
		public static M2MDemoDomainContext DomainContext (this Patient_Doctor  _)
		{
			return M2MDemoDomainContext.Current;
		}
	
}
	public partial class Patient
	{
		EntityCollection<Patient_Doctor, Doctor> _DoctorSet;
		public EntityCollection<Patient_Doctor, Doctor> DoctorSet
		{
			get
			{
				if (_DoctorSet == null)
				{
					_DoctorSet = new EntityCollection<Patient_Doctor, Doctor>(this.Patient_Doctor_DoctorSet, r => r.Doctor, (r, t2) => r.Doctor = t2, r => r.Patient = this, removeAction);
				}
				return _DoctorSet;
			}
		}
		/// <summary>
		/// Remove an PatientDoctor association.
		/// If the association is connected to a domain context, remove it from the domain context (this is because 
		/// of a limitation in RIA service), otherwise remove it from the PatientDoctors collection of the entity itself
		/// </summary>
		/// <param name="pd"></param>
		private void removeAction(Patient_Doctor r)
		{
			if (r.DomainContext() == null)
			{
				this.Patient_Doctor_DoctorSet.Remove(r);
			}
			else
			{
				// remove an entity from domainContext.EntityContainter.EntitySet<relation>
                r.DomainContext().EntityContainer.GetEntitySet<Patient_Doctor>().Remove(r);
			}
		}
	}
	public partial class Doctor
	{
		EntityCollection<Patient_Doctor, Patient> _PatientSet;
		public EntityCollection<Patient_Doctor, Patient> PatientSet
		{
			get
			{
				if (_PatientSet == null)
				{
					_PatientSet = new EntityCollection<Patient_Doctor, Patient>(this.Patient_Doctor_PatientSet, r => r.Patient, (r, t2) => r.Patient = t2, r => r.Doctor = this, removeAction);
				}
				return _PatientSet;
			}
		}
		/// <summary>
		/// Remove an PatientDoctor association.
		/// If the association is connected to a domain context, remove it from the domain context (this is because 
		/// of a limitation in RIA service), otherwise remove it from the PatientDoctors collection of the entity itself
		/// </summary>
		/// <param name="pd"></param>
		private void removeAction(Patient_Doctor r)
		{
			if (r.DomainContext() == null)
			{
				this.Patient_Doctor_PatientSet.Remove(r);
			}
			else
			{
				// remove an entity from domainContext.EntityContainter.EntitySet<relation>
                r.DomainContext().EntityContainer.GetEntitySet<Patient_Doctor>().Remove(r);
			}
		}
	}
}

