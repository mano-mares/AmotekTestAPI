using AmotekAPI.Interfaces;
using AmotekAPI.Services;
using Ipfs.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmotekAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private IpfsService ipfsService;
        public ImageController(IpfsService ipfsService) {
            ipfsService = ipfsService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
                return BadRequest("Image file is required.");

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var imageHash = await ipfsService.UploadImageAsync(memoryStream);

                return Ok(new { ImageHash = imageHash });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DownloadImage(string imageHash)
        {
            var imageStream = await ipfsService.DownloadImageAsync(imageHash);
            if (imageStream == null)
                return NotFound();

            return File(imageStream, "image/jpeg");
        }
    }
}
