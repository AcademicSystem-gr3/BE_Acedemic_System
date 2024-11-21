using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace A3S.Api.Controllers.FileApi
{
    [Route("api/file/download")]
    [ApiController]
    public class DownloadFileController : ControllerBase
    {
        [HttpGet("{fileName}")]
        public IActionResult DownloadFile(string fileName, string directory)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", directory);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound($"File {fileName} không tồn tại");
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                var contentType = GetContentType(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi tải file: {ex.Message}");
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            switch (extension)
            {
                case ".pdf": return "application/pdf";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".png": return "image/png";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".txt": return "text/plain";
                default: return "application/octet-stream";
            }
        }
    }
}
