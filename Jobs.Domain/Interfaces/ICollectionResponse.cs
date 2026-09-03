using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Interfaces
{
        public interface ICollectionResponse<T>
    {
        List<T> Items { get; init; }
    }
}
