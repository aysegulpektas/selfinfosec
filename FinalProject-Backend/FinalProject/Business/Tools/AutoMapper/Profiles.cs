using AutoMapper;
using DTOs;
using Entities.Concrete.DTOs.Responses;
using Entities.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tools.AutoMapper
{
  public class Profiles : Profile
  {
    public Profiles()
    {
      CreateMap<User, UserResponseModel>().ReverseMap();

      CreateMap<Question, AddQuestionDto>().ReverseMap();

      CreateMap<Article, ArticleDataModel>().ReverseMap();
      CreateMap<SequencedImage, SequencedImageUploadDto>().ReverseMap();

            
        }
    }
}
