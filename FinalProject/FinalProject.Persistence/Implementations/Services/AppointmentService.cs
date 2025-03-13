using AutoMapper;
using FinalProject.Application.Abstractions.Repositories;
using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Appointment;
using FinalProject.Domain.Entities;
using FinalProject.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace FinalProject.Persistence.Implementations.Services
{
    internal class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IEmailService _emailService;
        private const string EmailSubject = "Your Appointment Has Been Confirmed ✅";


        public AppointmentService(IAppointmentRepository appointmentRepository
            , IMapper mapper
            , IDoctorRepository doctorRepository
            , IPatientRepository patientRepository,
            IEmailService emailService)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _emailService = emailService;
        }

        public async Task<GetAppointmentDto> GetByIdAsync(int id)
        {
            Appointment? appointment = await _appointmentRepository.GetbyIdAsync(id, "Doctor", "Patient");

            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            return _mapper.Map<GetAppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentItemDto>> GetAllAsync(int page, int take)
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository
                             .GetAll(null, null, false, false, skip: (page - 1) * take, take: take, "Doctor", "Patient")
                   .ToListAsync(); ;


            return _mapper.Map<IEnumerable<AppointmentItemDto>>(appointments);
        }



        public async Task CreateAsync(CreateAppointmentDto appointmentDto)
        {
            if (!await _doctorRepository.AnyAsync(d => d.Id == appointmentDto.DoctorId))
                throw new Exception("The specified doctor does not exist.");

            Patient patient = await _patientRepository.SearchPatientIdentityAsync(appointmentDto.PatientCode);
            if (patient is null)
                throw new Exception("The specified patient does not exist.");

            DateTime roundedDate = appointmentDto.AppointmentDate.RoundToNearest10Minutes();

            if (roundedDate.TimeOfDay < TimeSpan.FromHours(9) || roundedDate.TimeOfDay > TimeSpan.FromHours(18))
                throw new Exception("Appointments can only be scheduled between 09:00 and 18:00.");

            if (roundedDate > DateTime.UtcNow.AddDays(180))
                throw new Exception("Appointments can only be scheduled up to 6 months in advance.");

            if (await _appointmentRepository.GetAppointmentByDateAndDoctorAsync(roundedDate, appointmentDto.DoctorId) is not null)
                throw new Exception("There is already an appointment at this time.");

            if (await _appointmentRepository.HasPatientAppointmentForDateAsync(appointmentDto.PatientCode, roundedDate.Date))
                throw new Exception($"The patient already has an appointment on {roundedDate:dd/MM/yyyy}. Only one appointment per day is allowed.");

            Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
            appointment.PatientId = patient.Id;
            appointment.AppointmentDate = roundedDate;
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.ModifiedAt = DateTime.UtcNow;
            appointment.AppointmentNumber = Guid.NewGuid().ToString().Substring(0,8).ToUpper();

            Doctor doctor = await _doctorRepository.GetbyIdAsync(appointment.DoctorId);

            string emailMessage = GenerateEmailMessage(patient, doctor, appointment);
            await _emailService.SendEmailAsync(patient.Email,EmailSubject, emailMessage);

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        private string GenerateEmailMessage(Patient patient, Doctor doctor, Appointment appointment)
        {
            return $@"
        <p>Dear {patient.Name} {patient.Surname},</p>
        <p>We are pleased to inform you that your appointment has been successfully scheduled. Below are the details:</p>
        <ul>
            <li><strong>Date:</strong> {appointment.AppointmentDate:g}</li>
            <li><strong>Doctor:</strong> Dr. {doctor.Name} {doctor.Surname}</li>
        </ul>
        <p>Your appointment number is: <strong>{appointment.AppointmentNumber}</strong>.</p>
        <p>We look forward to seeing you!</p>
        <p><strong>MedTex Clinic</strong></p>";
        }



        public async Task UpdateAsync(int id, UpdateAppointmentDto appointmentDto)
        {
            Appointment appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            if (appointmentDto.AppointmentDate.TimeOfDay < TimeSpan.FromHours(9) ||
       appointmentDto.AppointmentDate.TimeOfDay > TimeSpan.FromHours(18))
            {
                throw new Exception("Appointments can only scheduled between 9:00 and 18:00");
            }


            _mapper.Map(appointmentDto, appointment);
            appointment.ModifiedAt = DateTime.Now;
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();



        }

        public async Task DeleteAsync(int id)
        {
            Appointment appointment = await _appointmentRepository.GetbyIdAsync(id);
            if (appointment is null)
                throw new NotFoundException("Appointment not found");

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task CancelAppointmentAsync(string appointmentNumber)
        {
            Appointment? appointment = await _appointmentRepository.GetAppointmentByNumber(appointmentNumber);
            if (appointment is null)
                throw new NotFoundException("Appointment does not exist");
            appointment.IsCanceled = true;
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAppointmentDto>> GetDoctorAppointmentsAsync(string doctorCode,DateOnly date)
        {
            Doctor doctor = await _doctorRepository.SearchByIdentityNumberAsync(doctorCode,date);

            if (doctor is null)
                throw new Exception("Doctor not found");

            return _mapper.Map<IEnumerable<GetAppointmentDto>>(doctor.Appointments);

        }
    }
}
