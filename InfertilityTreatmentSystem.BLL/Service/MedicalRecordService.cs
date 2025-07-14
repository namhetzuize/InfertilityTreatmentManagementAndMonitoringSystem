using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class MedicalRecordService
    {
        private readonly UnitOfWork _unitOfWork;

        public MedicalRecordService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            return await _unitOfWork.MedicalRecordRepository.GetAllAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(Guid id)
        {
            return await _unitOfWork.MedicalRecordRepository.GetByIdAsync(id);
        }

        public async Task CreateMedicalRecordAsync(MedicalRecord record)
        {
            _unitOfWork.MedicalRecordRepository.PrepareCreate(record);
            await _unitOfWork.MedicalRecordRepository.SaveAsync();
        }

        public async Task UpdateMedicalRecordAsync(MedicalRecord record)
        {
            _unitOfWork.MedicalRecordRepository.PrepareUpdate(record);
            await _unitOfWork.MedicalRecordRepository.SaveAsync();
        }

        public async Task DeleteMedicalRecordAsync(MedicalRecord record)
        {
            _unitOfWork.MedicalRecordRepository.PrepareRemove(record);
            await _unitOfWork.MedicalRecordRepository.SaveAsync();
        }

        // Update MedicalRecord by RecordId
        public async Task UpdateMedicalRecordByIdAsync(Guid recordId, MedicalRecord updatedRecord)
        {
            var record = await _unitOfWork.MedicalRecordRepository.GetByIdAsync(recordId);
            if (record == null)
            {
                throw new Exception("Medical Record not found.");
            }

            // Update record properties
            record.Prescription = updatedRecord.Prescription;
            record.TestResults = updatedRecord.TestResults;
            record.Note = updatedRecord.Note;

            _unitOfWork.MedicalRecordRepository.PrepareUpdate(record);
            await _unitOfWork.MedicalRecordRepository.SaveAsync();
        }

        // Delete MedicalRecord by RecordId
        public async Task DeleteMedicalRecordByIdAsync(Guid recordId)
        {
            var record = await _unitOfWork.MedicalRecordRepository.GetByIdAsync(recordId);
            if (record == null)
            {
                throw new Exception("Medical Record not found.");
            }

            _unitOfWork.MedicalRecordRepository.PrepareRemove(record);
            await _unitOfWork.MedicalRecordRepository.SaveAsync();
        }
        public async Task<List<MedicalRecord>> GetMedicalRecordsByCustomerAndDoctorAsync(Guid customerId, Guid doctorId)
        {
            var allMedicalRecords = await _unitOfWork.MedicalRecordRepository.GetAllAsync();

            return allMedicalRecords
                .Where(r => r.CustomerId == customerId && r.DoctorId == doctorId)
                .ToList();
        }
    }
}
