using Jobs.Application.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Infrastructure.Services.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {

        private readonly string _rootPath;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(
            IHostEnvironment env,
            ILogger<LocalFileStorageService> logger)
        {
            _logger = logger;

            // Store files under: <content-root>/Storage/Files
            _rootPath = Path.Combine(env.ContentRootPath, "Storage", "Files");

            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }
        }

        /// <summary>
        /// Deletes a file from the local storage. If the file does not exist, no action is taken.
        /// </summary>
        /// <param name="filePath">The relative path of the file to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_rootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation("Deleted local file: {FilePath}", fullPath);
            }

            return Task.CompletedTask;
        }


        /// <summary>
        /// Checks if a file exists in the local storage.
        /// </summary>
        /// <param name="filePath">The relative path of the file to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the file exists; otherwise, false.</returns>
        public Task<bool> ExistsAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_rootPath, filePath);
            return Task.FromResult(File.Exists(fullPath));
        }


        /// <summary>
        /// Retrieves a file and saves it to a MemoryStream. Returns null if the file does not exist.
        /// </summary>
        /// <param name="filePath">The relative path of the file to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A MemoryStream containing the file's contents, or null if the file does not exist.</returns>
        public async Task<Stream?> GetAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_rootPath, filePath);

            if (!File.Exists(fullPath))
            {
                return null;
            }

            var memory = new MemoryStream();
            await using var fileStream = File.OpenRead(fullPath);
            await fileStream.CopyToAsync(memory, cancellationToken);

            memory.Position = 0;
            return memory;
        }

        /// <summary>
        /// Saves a file to the local storage. The file is saved with a unique name to avoid conflicts.
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SaveAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
        {
            var safeFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(_rootPath, safeFileName);

            await using var output = File.Create(filePath);
            await fileStream.CopyToAsync(output, cancellationToken);

            _logger.LogInformation("Saved file locally: {FilePath}", filePath);

            return safeFileName; // return relative path
        }
    }
}
