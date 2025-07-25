using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Repository;
using InfertilityTreatmentSystem.DAL.Paging;  // Add the using directive for PagingResponse
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.DAL
{
    public class UnitOfWork
    {
        private readonly InfertilityTreatmentDBContext _context;

        private Userrepository _userRepository;
        private AppointmentRepository _appointmentRepository;
        private BlogRepository _blogRepository;
        private FeedbackRepository _feedbackRepository;
        private MedicalRecordRepository _medicalRecordRepository;
        private PatientRequestRepository _patientRequestRepository;
        private ScheduleRepository _scheduleRepository;
        private TreatmentServiceRepository _treatmentServiceRepository;

        public UnitOfWork(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public Userrepository UserRepository
        {
            get { return _userRepository ??= new Userrepository(_context); }
        }

        public AppointmentRepository AppointmentRepository
        {
            get { return _appointmentRepository ??= new AppointmentRepository(_context); }
        }

        public BlogRepository BlogRepository
        {
            get { return _blogRepository ??= new BlogRepository(_context); }
        }

        public FeedbackRepository FeedbackRepository
        {
            get { return _feedbackRepository ??= new FeedbackRepository(_context); }
        }

        public MedicalRecordRepository MedicalRecordRepository
        {
            get { return _medicalRecordRepository ??= new MedicalRecordRepository(_context); }
        }

        public PatientRequestRepository PatientRequestRepository
        {
            get { return _patientRequestRepository ??= new PatientRequestRepository(_context); }
        }

        public ScheduleRepository ScheduleRepository
        {
            get { return _scheduleRepository ??= new ScheduleRepository(_context); }
        }

        public TreatmentServiceRepository TreatmentServiceRepository
        {
            get { return _treatmentServiceRepository ??= new TreatmentServiceRepository(_context); }
        }

    }
}
