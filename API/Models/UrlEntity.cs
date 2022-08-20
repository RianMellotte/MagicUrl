namespace API.Models
{
    public class UrlEntity : IUrlEntity
    {
        public UrlEntity(string url)
        {
           this.Url = url;
        }

        public int Id { get; set; } 
        public string Url { get; set; }

        public static async Task<bool> UrlIsValid(string Url)
        {
            HttpClient client = new HttpClient();
            try 
            {
                var res = await client.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead);
                return res.IsSuccessStatusCode;
            } 
            catch
            {
                return false;
            }
        }
    }
}