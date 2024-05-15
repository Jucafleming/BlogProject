using BlogProject.API.Models.Domain;
using BlogProject.API.Models.DTO;
using BlogProject.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string Title)
        {
            ValidateFileUpload(file);
            if(ModelState.IsValid)
            {
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = Title,
                    DateCreated = DateTime.Now,
                };

                await imageRepository.Upload(file, blogImage);


                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileName = blogImage.FileName,
                    FileExtension = blogImage.FileExtension,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    Url = blogImage.Url
                };


                return Ok(response);
            }
            return BadRequest(ModelState);         



        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtentions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "unsuported file format");
            }
            if(file.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size cannot be more than 10Mb");
            }

        }

    }


    
}
