namespace API.Models
{
    public interface IUrlRepo
    {
        public Task<UrlEntity[]> UrlExists(string Url);
        public Task<UrlEntity> AddUrl(string Url);
        public Task<UrlEntity[]> GetUrlFromId(int Id);
    }
}