using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
  public class DataResult<T> : Result, IDataResult<T>
  {
    public T Data { get; }
    public DataResult(bool success,T data) : base(success)
    {
      this.Data = data;
    }
    public DataResult(bool success,T data,string message) : base(success,message)
    {
      this.Data = data;
    }
    public DataResult(bool success) : base(success)
    {
    }
    public DataResult(bool success,string message) : base(success,message)
    {

    }
  }
}
