using Backend.DTOs;
using System.Collections.Generic;
using FreelanceLand.Models;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITasksService
    {
        IEnumerable<TaskDTO> GetToDoEntities();
        IEnumerable<TaskDTO> GetHistoryTaskByUser(int id);
        IEnumerable<TaskDTO> GetActiveTaskByUser(int id);

        void DeleteTask(int id);

    }
}
