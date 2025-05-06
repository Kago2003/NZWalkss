using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Constraints;
using NZWalkssAPI.Models.Domain;
using NZWalkssAPI.Models.DTO;
using NZWalkssAPI.Repositories;

namespace NZWalkssAPI.Controllers
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

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid) 
            {

                //convert DTO to domain model 

                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                //Use repository to upload image\\
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request) 
        {
            var allowedExtention = new string[]
            {
                ".jpg", 
                ".jped", 
                ".png",
                ".jpeg"
            };
            if (allowedExtention.Contains(Path.GetExtension(request.File.FileName)) == false) 
            {
                ModelState.AddModelError("file", "Unsupported file extention");
            }

            if (request.File.Length > 10485760) //that number is = to 10 mb so if the file is larger than 10 mb, we're going to throw the error specified below
            {
                ModelState.AddModelError("file", "File size is more than 10MB. Please upload a smaller file size");
            }
        }
    }
}
