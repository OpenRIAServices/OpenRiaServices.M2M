 

 


using EF.M2M;

namespace M2MDemo.Web
{
    public partial class M2MDemoDomainService
	{
		public void InsertPatient_Doctor(Patient_Doctor patient_Doctor )
		{
			Patient patient = patient_Doctor.Patient;
			if(patient == null)
			{
				Patient PatientStubEntity = new Patient { Id  = patient_Doctor.PatientId };
				patient = M2MTools.GetEntityByKey<Patient>(ObjectContext, "Patients", PatientStubEntity);
			}
			Doctor doctor = patient_Doctor.Doctor;
			if(doctor == null)
			{
				Doctor DoctorStubEntity = new Doctor { Id  = patient_Doctor.DoctorId };
				doctor = M2MTools.GetEntityByKey<Doctor>(ObjectContext, "Doctors", DoctorStubEntity);
			}
			patient.DoctorSet.Add(doctor);
		}			
		public void DeletePatient_Doctor(Patient_Doctor patient_Doctor )
		{
			Patient patient = patient_Doctor.Patient;
			if(patient == null)
			{
				Patient PatientStubEntity = new Patient { Id  = patient_Doctor.PatientId };
				patient = M2MTools.GetEntityByKey<Patient>(ObjectContext, "Patients", PatientStubEntity);
			}
			Doctor doctor = patient_Doctor.Doctor;
			if(doctor == null)
			{
				Doctor DoctorStubEntity = new Doctor { Id  = patient_Doctor.DoctorId };
				doctor = M2MTools.GetEntityByKey<Doctor>(ObjectContext, "Doctors", DoctorStubEntity);
			}

			patient.DoctorSet.Remove(doctor);
		}
	 }
 }
