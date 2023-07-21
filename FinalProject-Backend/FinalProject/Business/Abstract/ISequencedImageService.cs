using System;
using Core.Utilities.Results;
using DTOs;
using Entities.Models;
using Models;

namespace Abstract
{
  public interface ISequencedImageService
  {
    IResult Add(SequencedImageUploadDto sequencedImageUploadDto);
    IDataResult<List<SequencedImage>> GetImagesByArticleId(int articleId);
  }
}