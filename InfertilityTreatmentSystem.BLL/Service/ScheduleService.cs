using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class ScheduleService
    {
        private readonly UnitOfWork _unitOfWork;

        public ScheduleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _unitOfWork.ScheduleRepository.GetAllAsync();
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            return await _unitOfWork.ScheduleRepository.GetByIdAsync(id);
        }

        public async Task CreateScheduleAsync(Schedule schedule)
        {
            _unitOfWork.ScheduleRepository.PrepareCreate(schedule);
            await _unitOfWork.ScheduleRepository.SaveAsync();
        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            _unitOfWork.ScheduleRepository.PrepareUpdate(schedule);
            await _unitOfWork.ScheduleRepository.SaveAsync();
        }

        public async Task DeleteScheduleAsync(Schedule schedule)
        {
            _unitOfWork.ScheduleRepository.PrepareRemove(schedule);
            await _unitOfWork.ScheduleRepository.SaveAsync();
        }
    }
}
