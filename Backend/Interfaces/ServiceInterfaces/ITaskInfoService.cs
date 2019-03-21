using Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITaskInfoService
    {
        Task<TaskDescription> GetTaskDescription(int id);
        Task<CustomerDTO> GetTaskCustomer(int taskId);
    }
}
