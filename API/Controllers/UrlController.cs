using API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers;

[ApiController]
[Route("/")]
public class UrlController : ControllerBase
{
    private IUrlRepo _urlRepo;

    public UrlController(IUrlRepo urlRepo)
    {
        _urlRepo = urlRepo;
    }

    [HttpPost]
    [Route("/add-url/")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateUrl([FromBody] object Body)
    {
        var json = Body.ToString();
        var req = JsonConvert.DeserializeObject<UrlEntity>(json);
        string url = req.Url;

        if (url == null)
        {
            return BadRequest(ModelState);
        }

        // Check if URL is already in the database, and return it if it is.
        try 
        {
            UrlEntity[] existingUrlEntity = await _urlRepo.UrlExists(url);
            if (existingUrlEntity.Length > 0)
            {
                return Ok(existingUrlEntity[0]);
            }
        } catch
        {
            ModelState.AddModelError("Error", $"Something went wrong while looking for the url in our records. Please try again or contact the site administrator for help.");
            return StatusCode(200, ModelState);
        }

        // Create new UrlEntity and return it if it is valid.
        bool isValid = await UrlEntity.UrlIsValid(url);

        if (isValid) 
        {
            try 
            {
                UrlEntity newUrlEntity = await _urlRepo.AddUrl(url);
                return Ok(newUrlEntity);
            } catch 
            {
                ModelState.AddModelError("Error", $"Something went wrong while saving the url. Please try again or contact the site administrator for help.");
                return StatusCode(200, ModelState);
            }
        } else {
            ModelState.AddModelError("Error", $"The url " + $"{url}" + " is invalid or inactive. Please try again using a valid url.");
            return StatusCode(200, ModelState);
        }
    }

    [HttpGet]
    [Route("/{Id:int}")]
    public async Task<IActionResult> ForwardUser([FromRoute]int id)
    {
        try {
            UrlEntity[] urlEntity = await _urlRepo.GetUrlFromId(id);
            if (urlEntity.Length > 0)
            {
                return new RedirectResult(urlEntity[0].Url);
            } else {
                return Redirect("/error");
            }
        } catch {
            return Redirect("/error");
        }
    }
}
