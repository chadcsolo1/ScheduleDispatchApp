namespace ScheduleDispatch.API.DTOs
{
    public interface ICollectionResponse<T>
    {
        List<T> Items { get; init; }
    }

}
