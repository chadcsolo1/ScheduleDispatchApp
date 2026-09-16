using Jobs.Domain.Models;
using ScheduleDispatch.API.Models.Responses;

namespace ScheduleDispatch.API.Services
{
    public sealed class LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {

        public LinkDto Create(
            string endpointNmae,
            string rel,
            string method,
            object? values = null,
            string? controller = null)
        {
            string? href = linkGenerator.GetPathByAction(
                httpContextAccessor.HttpContext!,
                endpointNmae,
                controller,
                values);

            return new LinkDto
            {
                Href = href ?? throw new InvalidOperationException("Invalid endpoint name provided."),
                Rel = rel,
                Method = method
            };
        }
        
    }
}
