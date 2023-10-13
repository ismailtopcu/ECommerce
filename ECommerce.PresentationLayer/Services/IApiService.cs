namespace ECommerce.PresentationLayer.Services
{
    public interface IApiService
    {
        Task<List<T>> GetTableData<T>(string apiUrl);
        Task<T> GetData<T>(string apiUrl);
        Task<bool> AddData(string apiUrl, object T);
        Task<bool> GetNoContent(string apiUrl);
    }
}
