using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A3S.Api.Controllers.FileApi
{
    [Route("api/file")]
    [ApiController]
    public class UploadFileController : ControllerBase 
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string directory)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", directory);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = MyExtensions.AppendTimeStamp(file.FileName);
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { fileName=fileName });
        }
            
    }
    public static class MyExtensions
    {
        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
    }



}

