using ScheduleDispatch.UI.Services.Jobs.Request;
using ScheduleDispatch.UI.Services.Jobs.Response;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace ScheduleDispatch.UI.Services.Jobs
{
    public sealed class JobsService
    {
        private readonly HttpClient _http;

        public JobsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<CreateJobResponse?> CreateJobAsync(CreateJobRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/jobs", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CreateJobResponse>();
        }

        public async Task<JobResponse?> GetJobByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<JobResponse>($"api/jobs/{id}");
        }

        public async Task<IEnumerable<JobResponse>> GetJobsAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<JobResponse>>("api/jobs");
            return result ?? Enumerable.Empty<JobResponse>();
        }

        public async Task<JobResponse?> UpdateJobAsync(Guid id, UpdateJobRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/jobs/{id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobResponse>();
        }

        public async Task DeleteJobAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/jobs/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
