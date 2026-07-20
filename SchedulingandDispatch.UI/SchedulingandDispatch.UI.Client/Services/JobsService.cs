using System.Net;
using System.Net.Http.Json;

namespace SchedulingandDispatch.UI.Client.Services
{
    public sealed class JobsService
    {
                    private readonly HttpClient _http;

            public JobsService(HttpClient http)
            {
                _http = http;
            }

            // ------------------------------------------------------------
            // CREATE JOB
            // ------------------------------------------------------------
            public async Task<CreateJobResponse?> CreateJobAsync(CreateJobRequest request)
            {
                var response = await _http.PostAsJsonAsync("api/jobs", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreateJobResponse>();
            }

            // ------------------------------------------------------------
            // GET JOB BY ID
            // ------------------------------------------------------------
            public async Task<JobResponse?> GetJobByIdAsync(Guid id)
            {
                return await _http.GetFromJsonAsync<JobResponse>($"api/jobs/{id}");
            }

            // ------------------------------------------------------------
            // GET ALL JOBS
            // ------------------------------------------------------------
            public async Task<IEnumerable<JobResponse>> GetJobsAsync()
            {
                var result = await _http.GetFromJsonAsync<IEnumerable<JobResponse>>("api/jobs");
                return result ?? Enumerable.Empty<JobResponse>();
            }

            // ------------------------------------------------------------
            // UPDATE JOB
            // ------------------------------------------------------------
            public async Task<JobResponse?> UpdateJobAsync(Guid id, UpdateJobRequest request)
            {
                var response = await _http.PutAsJsonAsync($"api/jobs/{id}", request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<JobResponse>();
            }

            // ------------------------------------------------------------
            // DELETE JOB
            // ------------------------------------------------------------
            public async Task DeleteJobAsync(Guid id)
            {
                var response = await _http.DeleteAsync($"api/jobs/{id}");
                response.EnsureSuccessStatusCode();
            }
    }
}
