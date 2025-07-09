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
    }
}
