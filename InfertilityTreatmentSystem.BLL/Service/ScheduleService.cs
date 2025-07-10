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

        public async Task<Schedule> GetScheduleByIdAsync(Guid id)
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

        // Update Schedule by ScheduleId
        public async Task UpdateScheduleByIdAsync(Guid scheduleId, Schedule updatedSchedule)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                throw new Exception("Schedule not found.");
            }

            // Update schedule properties
            schedule.CustomerId = updatedSchedule.CustomerId;
            schedule.DoctorId = updatedSchedule.DoctorId;
            schedule.SerivceName = updatedSchedule.SerivceName;
            schedule.ScheduleDate = updatedSchedule.ScheduleDate;
            schedule.Note = updatedSchedule.Note;

            _unitOfWork.ScheduleRepository.PrepareUpdate(schedule);
            await _unitOfWork.ScheduleRepository.SaveAsync();
        }

        // Delete Schedule by ScheduleId
        public async Task DeleteScheduleByIdAsync(Guid scheduleId)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                throw new Exception("Schedule not found.");
            }

            _unitOfWork.ScheduleRepository.PrepareRemove(schedule);
            await _unitOfWork.ScheduleRepository.SaveAsync();
        }
    }
}
