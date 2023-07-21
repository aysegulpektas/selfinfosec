using System;
using Microsoft.AspNetCore.Http;

namespace DTOs
{
    public class SequencedImageUploadDto
    {
        public int ArticleId { get; set; }
        public int Sequence { get; set; } //Resmin sırası
        public IFormFile ImageFile {get;set;}
        public string? ImagePath { get; set; }
        public string Description { get; set; }
        public int ShowType {get; set;} //Tıklayınca kayan resim, Tek tek gösterim vb
    }
}