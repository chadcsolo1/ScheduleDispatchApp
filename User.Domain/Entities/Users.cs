using SharedKernel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Events;

namespace User.Domain.Entities
{
    public class Users : AggregateRoot
    {
        public Guid Id
        {
            get;
            private set;
        }
        public string Email
        {
            get;
            private set;
        } = string.Empty;

        public string Name
        {
            get;
            private set;
        } = string.Empty;

        public DateTime CreatedAtUtc
        {
            get;
            private set;
        }
        public DateTime? UpdatedAtUtc
        {
            get;
            private set;
        }
        public string IdentityId
        {
            get;
            private set;
        } = string.Empty;

        private User() { } // EF Core

        public Users(string email, string name, string identityId)
        {
            Id = Guid.NewGuid();
            Email = email;
            Name = name;
            IdentityId = identityId;
            CreatedAtUtc = DateTime.UtcNow;

            AddDomainEvent(new CreateUserEvent(Id, Email, Name, IdentityId));
        }

        public void UpdateUser(string email, string name)
        {
            Email = email;
            Name = name;
            UpdatedAtUtc = DateTime.UtcNow;
            AddDomainEvent(new UpdateUserEvent(Id, Email, Name));
        }
    }
}
