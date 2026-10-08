using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Events
{
    internal class UpdateUserEvent : DomainEvent
    {
        public Guid UserId
        {
            get;
        }
        public string Email
        {
            get;
        }
        public string Name
        {
            get;
        }
        public UpdateUserEvent(Guid userId, string email, string name)
        {
            UserId = userId;
            Email = email;
            Name = name;
        }
    {
    }
}
