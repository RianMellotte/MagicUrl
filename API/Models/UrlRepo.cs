using System.Data;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class UrlRepo : IUrlRepo
    {
        private UrlContext _urlContext;

        public UrlRepo(UrlContext urlContext)
        {
            _urlContext = urlContext;
        }

        public async Task<UrlEntity> AddUrl(string url)
        {
            var urlEntityToBeCreated = new UrlEntity(url);
            var createdUrl = await _urlContext.UrlRepo.AddAsync(urlEntityToBeCreated);
            _urlContext.SaveChanges();
            return createdUrl.Entity;
        }

        public async Task<UrlEntity[]> GetUrlFromId(int id)
        {
            UrlEntity[] redirectUrl = await _urlContext.UrlRepo.Where(x => Equals(x.Id, id)).ToArrayAsync();
            return redirectUrl;
        }

        public async Task<UrlEntity[]> UrlExists(string url)
        {
            UrlEntity[] existingUrl = await _urlContext.UrlRepo.Where(x => Equals(x.Url, url)).ToArrayAsync();
            return existingUrl;
        }

        public void DeleteAll()
        {
            IEnumerable<UrlEntity> AllUrls = _urlContext.UrlRepo.ToList();
            _urlContext.UrlRepo.RemoveRange(AllUrls);    
            _urlContext.SaveChanges();
        }
    }
}