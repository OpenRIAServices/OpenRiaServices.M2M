 

 


using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ServiceModel.DomainServices.Server;
using M2M.EF;

namespace M2MDemo.Web  {
public partial class Patient_Doctor {
		int patientId;
		[DataMember]
		[Key]
		public int PatientId
		{
			get
			{
				if (Patient != null)
				{
					if (patientId != Patient.Id && patientId == 0)
						patientId = Patient.Id;
				}
				return patientId;
			}
			set
			{
				patientId = value;
			}
		}
		[Include]
		[Association("Patient_Doctor_Patient", "PatientId", "Id", IsForeignKey = true)]
		[DataMember]
		public Patient Patient { get; set; }
		int doctorId;
		[DataMember]
		[Key]
		public int DoctorId
		{
			get
			{
				if (Doctor != null)
				{
					if (doctorId != Doctor.Id && doctorId == 0)
						doctorId = Doctor.Id;
				}
				return doctorId;
			}
			set
			{
				doctorId = value;
			}
		}
		[Include]
		[Association("Patient_Doctor_Doctor", "DoctorId", "Id", IsForeignKey = true)]
		[DataMember]
		public Doctor Doctor { get; set; }
	}
    public partial class Patient
    {
        private  EntityCollection<Patient_Doctor,Doctor> _Patient_Doctor_DoctorSet;
        [DataMember]
        [Include]
        [Association("Patient_Doctor_Patient", "Id", "PatientId")]
        public EntityCollection<Patient_Doctor,Doctor>Patient_Doctor_DoctorSet { 
			get{
				if(_Patient_Doctor_DoctorSet == null)
				{            
					_Patient_Doctor_DoctorSet = new EntityCollection<Patient_Doctor,Doctor>(
						DoctorSet,
						(doctor) => new Patient_Doctor { 
							Patient = this, 
							Doctor = doctor }, pd=>pd.Doctor);
				}
				return _Patient_Doctor_DoctorSet;
			}
		}
    }
    public partial class Doctor
    {
        private  EntityCollection<Patient_Doctor,Patient> _Patient_Doctor_PatientSet;
        [DataMember]
        [Include]
        [Association("Patient_Doctor_Doctor", "Id", "DoctorId")]
        public EntityCollection<Patient_Doctor,Patient>Patient_Doctor_PatientSet { 
			get{
				if(_Patient_Doctor_PatientSet == null)
				{            
					_Patient_Doctor_PatientSet = new EntityCollection<Patient_Doctor,Patient>(
						PatientSet,
						(patient) => new Patient_Doctor { 
							Doctor = this, 
							Patient = patient }, pd=>pd.Patient);
				}
				return _Patient_Doctor_PatientSet;
			}
		}
    }
 }
