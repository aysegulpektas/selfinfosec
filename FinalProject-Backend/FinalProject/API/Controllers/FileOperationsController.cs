using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FileOperationsController : ControllerBase
    {
        [HttpGet("createFile")]
        public IActionResult CreateFile()
        {
            var fileName = "testCreateFile";
            var path = Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar
                                    + "Articles" + Path.DirectorySeparatorChar + fileName);

            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            return Ok(Environment.CurrentDirectory);
        }


        [HttpGet("readFile")]
        public IActionResult GetFile()
        {
            var readedText = " ";
            var fileName = "testCreateFile";
            var path = Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar
                                    + "Articles" + Path.DirectorySeparatorChar + fileName);

            using (var sr = new StreamReader(path))
            {
                readedText = sr.ReadToEnd();
            }

            return Ok(readedText);
        }


        [HttpGet("deleteFile")]
        public IActionResult DeleteFile()
        {
            var fileName = "testCreateFile";
            var path = Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar
                                    + "Articles" + Path.DirectorySeparatorChar + fileName);

            System.IO.File.Delete(path);

            return Ok();
        }

        [HttpGet("copyFile")]
        public IActionResult CopyFile()
        {
            var fileName = "testCreateFile";
            var path = Path.Combine(Environment.CurrentDirectory + Path.DirectorySeparatorChar
                                    + "Articles" + Path.DirectorySeparatorChar + fileName);
            return Ok();

        }

       





    }
}

