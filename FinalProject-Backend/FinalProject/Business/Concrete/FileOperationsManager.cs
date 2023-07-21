using System;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;

namespace Business.Concrete
{
	public class FileOperationsManager : IFileOperationsService
	{
		

        public IResult CreateFile(string path, string fileName)
        {
            var fullFilePath = Path.Combine(path + Path.DirectorySeparatorChar + fileName);
            System.IO.File.Create(fullFilePath);
            return new SuccessResult();
        }


        public IResult CreateFile(string path, string fileName, string content)
        {
            var fullFilePath = Path.Combine(path + Path.DirectorySeparatorChar + fileName);
            using (FileStream fs = System.IO.File.Create(fullFilePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
 
                fs.Write(info, 0, info.Length);
            }
            return new SuccessResult();

        }

        public IResult DeleteFile(string path, string fileName)
        {
            var fullFilePath = Path.Combine(path + Path.DirectorySeparatorChar + fileName);
            System.IO.File.Delete(fullFilePath);
            return new SuccessResult();
        }


        public IDataResult<string> ReadFile(string path, string fileName)
        {
            var readedText = "";
            var fullFilePath = Path.Combine(path + Path.DirectorySeparatorChar + fileName);
            using (var sr = new StreamReader(fullFilePath))
            {
                readedText = sr.ReadToEnd();
            }
            return new SuccessDataResult<string>("Dosya Okundu", readedText);
        }


        public IResult WriteFile(string path, string fileName, string content)
        {
            var fullFilePath = Path.Combine(path + Path.DirectorySeparatorChar + fileName);
            using (FileStream fs = System.IO.File.Create(fullFilePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
            return new SuccessResult();
        }
    }
}



