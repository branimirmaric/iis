using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Zadatak02.Services;

namespace Zadatak02.Controllers
{
    [Route("api/cartoon")]
    [ApiController]
    public class CartoonController : ControllerBase
    {
        private readonly Validations _validation = new();

        [HttpPost("xmlupload")]
        public async Task<IActionResult> UploadXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var tempPath = Path.GetTempFileName();

            try
            {
                using (var stream = new FileStream(tempPath, FileMode.Create))
                    await file.CopyToAsync(stream);

                var result = _validation.ValidateXMLUsingRNG(tempPath);

                return string.IsNullOrEmpty(result)
                    ? Ok("XML file is valid and successfully processed.")
                    : BadRequest($"XML validation failed: {result}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            finally
            {
                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);
            }
        }
    }
}
