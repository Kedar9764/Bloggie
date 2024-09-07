using Bloggie.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var ImageUrl = await imageRepository.UploadAsync(file);

            if (ImageUrl == null) {

                return Problem("Something Went Wrong!", null, (int)HttpStatusCode.InternalServerError);

            }

            return new JsonResult(new { link  = ImageUrl });
        }
    }
}
