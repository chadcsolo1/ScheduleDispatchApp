using Jobs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
    public interface ILinksResponse
    {
        public List<LinkDto> Links { get; set; }
    }
}
