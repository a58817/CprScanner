namespace CprScannerRestApi.Controllers
{
    using CprScanner;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CprScannerController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult IsAlive()
        {
            return this.Ok("Er i live");
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ScannerResultat[]))]
        public async Task<IActionResult> SkanFilerAsync(List<IFormFile> formFiles)
        {
            var resultater = new List<ScannerResultat>();

            foreach (var formFile in formFiles)
            {
                var filePath = Path.Combine(new string[] { Path.GetTempPath(), $"{DateTime.Now:yyyyMMddHHmm}-{formFile.FileName}" });

                if (formFile.Length > 0)
                {
                    using (var createStream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(createStream);
                    }

                    using (var readStream = System.IO.File.OpenRead(filePath))
                    {
                        ICprScanner cprSkanner = CprScannerFactory.Create(new CprScannerConfig(), null);
                        var fundneCprNumre = new List<string>();
                        cprSkanner.FindCpr(readStream, formFile.FileName, (cprNummer) => fundneCprNumre.Add(cprNummer));
                        resultater.Add(new ScannerResultat(formFile.FileName, fundneCprNumre));
                    }
                }
            }

            return this.Ok(resultater.ToArray());
        }
    }
}
