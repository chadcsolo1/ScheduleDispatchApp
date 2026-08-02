using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Entities
{
    public class Organization
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Organization() { } // EF

        public Organization(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string name) => Name = name;
        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
