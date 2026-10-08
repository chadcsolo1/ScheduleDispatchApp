using SharedKernel.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.Events
{
    public sealed class CreateUserEvent : DomainEvent
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
        public string IdentityId
        {
            get;
        }
        public CreateUserEvent(Guid userId, string email, string name, string identityId)
        {
            UserId = userId;
            Email = email;
            Name = name;
            IdentityId = identityId;
        }
    }
}
