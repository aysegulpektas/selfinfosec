using System;
using Abstract;
using Core.Database.EntityFramework;
using DataAccess.Concrete;
using Models;

namespace Concrete
{
    public class EFSequencedImageDao : EFCrudOperations<SequencedImage,AppDbContext>, ISequencedImageDao
    {
        public EFSequencedImageDao()
        {
            
        }
    }
}