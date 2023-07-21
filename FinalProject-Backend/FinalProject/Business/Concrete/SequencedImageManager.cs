using System;
using System.IO;
using System.Text;
using Abstract;
using AutoMapper;
using Core.Utilities.Results;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Models;

namespace Concrete
{
  public class SequencedImageManager : ISequencedImageService
  {
    
    private ISequencedImageDao _sequencedImageDao;
    private IMapper _mapper;
    private IWebHostEnvironment _environment;
    string mainPath;
    public SequencedImageManager(ISequencedImageDao sequencedImageDao, IMapper mapper,IWebHostEnvironment environment)
    {
      _environment = environment;
      _sequencedImageDao = sequencedImageDao;
      _mapper = mapper;
      mainPath = _environment.WebRootPath + Path.DirectorySeparatorChar + "sequencedImages" + Path.DirectorySeparatorChar;
    }
    [Authorize(Roles = "ADMIN")]
    public IResult Add(SequencedImageUploadDto sequencedImageUploadDto)
    {
      var otherImages = _sequencedImageDao.GetAll(x => x.ArticleId == sequencedImageUploadDto.ArticleId && x.Sequence == sequencedImageUploadDto.Sequence);
      if(otherImages.Count > 0)
      {
        return new ErrorResult("Bu sıra numarasında bir resim zaten kayıtlı");
      }
      string fileNameGuid = Guid.NewGuid().ToString("N");
      var file = sequencedImageUploadDto.ImageFile;
      System.IO.Directory.CreateDirectory(mainPath);
      var fileExtension = System.IO.Path.GetExtension(sequencedImageUploadDto.ImageFile.FileName);
      var uploadPath = Path.Combine(mainPath, fileNameGuid + fileExtension);
      byte[] fileBytes;
      using (MemoryStream ms = new MemoryStream())
      {
        file.CopyTo(ms);
        fileBytes = ms.ToArray();
      }
      if (IsImage(fileBytes))
      {
        using (FileStream fs = System.IO.File.Create(uploadPath))
        {
          file.CopyTo(fs);
          fs.Flush();
        }
      }
      else
      {
        return new ErrorResult("Lütfen dosyayı kontrol edin");
      }
      sequencedImageUploadDto.ImagePath = fileNameGuid + fileExtension;
      var sequencedImageObj = _mapper.Map<SequencedImage>(sequencedImageUploadDto);
      _sequencedImageDao.Add(sequencedImageObj);
      return new SuccessResult();
    }
    public IDataResult<List<SequencedImage>> GetImagesByArticleId(int articleId)
    {
      var sequencedImages = _sequencedImageDao.GetAll(x => x.ArticleId == articleId);
      return new SuccessDataResult<List<SequencedImage>>(sequencedImages);
    }
    private static bool IsImage(byte[] fileBytes)
    {
      if (fileBytes.Length < 2)
      {
        return false;
      }

      var headers = new List<byte[]>
        {
            new byte[] { 0x42, 0x4D }, // BMP
            new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }, // GIF
            new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }, // GIF
            new byte[] { 0x89, 0x50, 0x4e, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, // PNG
            new byte[] { 0x49, 0x49, 0x2A, 0x00 }, // TIFF
            new byte[] { 0x4D, 0x4D, 0x00, 0x2A }, // TIFF
            new byte[] { 0xFF, 0xD8, 0xFF }, // JPEG
            new byte[] { 0xFF, 0xD9 }, // JPEG
        };

      return headers.Any(x => x.SequenceEqual(fileBytes.Take(x.Length)));
    }
  }
}