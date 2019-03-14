using Backend.DTOs;
using System.Collections.Generic;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ITaskInfoService
    {
        TaskDescription GetTaskDescription(int id);
        CustomerDTO GetTaskCustomer(int taskId);
    }
}
