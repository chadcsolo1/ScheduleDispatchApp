using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Models
{
    public sealed class LinkDto
    {
        public required string Href
        {
            get;
            init;
        }

        public required string Rel
        {
            get;
            init;
        }

        public required string Method
        {
            get;
            init;
        }
    }
}
