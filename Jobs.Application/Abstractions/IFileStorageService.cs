using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Application.Abstractions
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default);

        Task<Stream?> GetAsync(string filePath, CancellationToken cancellationToken = default);

        Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken = default);
}
}
