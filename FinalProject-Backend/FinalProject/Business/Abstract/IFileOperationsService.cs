using System;
using Core.Utilities.Results;
using Entities.Models;

namespace Business.Abstract
{
	public interface IFileOperationsService
	{
		public IResult CreateFile(string path, string fileName);
        public IResult CreateFile(string path, string fileName, string content);
        public IResult DeleteFile(string path, string fileName);
        public IResult WriteFile(string path, string fileName, string content);
        public IDataResult<string> ReadFile(string path, string fileName);

    }
}

