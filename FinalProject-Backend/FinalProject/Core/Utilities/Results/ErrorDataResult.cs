using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
  public class ErrorDataResult<T> : DataResult<T>
  {
        private List<string> errors;
        private string v;

    public ErrorDataResult() : base(false)
    {

    }
    public ErrorDataResult(string message) : base(false, message)
    {

    }
    public ErrorDataResult(T data) : base(false, data)
    {

    }
    public ErrorDataResult(string message, T data) : base(false, data, message)
    {

    }

    

    }
}
