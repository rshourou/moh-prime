using System;

namespace Prime.Models
{
    public interface IUserBoundModel
    {
        Guid UserId { get; set; }

        string Username { get; set; }
    }
}
