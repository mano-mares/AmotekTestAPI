using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmotekAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length <= 0)
            {
                return BadRequest("No image file uploaded.");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine("path/to/your/image/folder", uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Optionally, you can save the uniqueFileName or other metadata in a database for future reference

            return Ok("Image uploaded successfully.");
        }
        [HttpGet("{imageName}")]
        public IActionResult DownloadImage(string imageName)
        {
            var filePath = Path.Combine("path/to/your/image/folder", imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var fileType = "application/octet-stream"; // Set the appropriate content type based on your image format

            return File(fileStream, fileType);
        }
    }
}
