using Backend.DTOs;
using System.Collections.Generic;

namespace Backend.Interfaces.ServiceInterfaces
{
    public interface ICommentsService
    {
        IEnumerable<CommentDTO> GetComments(int taskId);
        CommentDTO AddComment(CommentDTO comment);
    }
}
