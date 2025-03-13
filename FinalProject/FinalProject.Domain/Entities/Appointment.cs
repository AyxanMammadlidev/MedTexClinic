using FinalProject.Domain.Entites;

namespace FinalProject.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNumber { get; set; }
        public bool IsCanceled { get; set; }

        //Relational
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
