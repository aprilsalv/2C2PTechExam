using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{
    public interface IRepository<T> where T : class
    {
        string UploadFile(IFormFile entity);

    }
}
