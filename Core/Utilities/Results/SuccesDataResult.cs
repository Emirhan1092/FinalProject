using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccesDataResult<T>:DataResult<T>
    {
        public SuccesDataResult(T data ,string massege):base(data,true, massege)
        {

        }

        public SuccesDataResult(T data):base(data,true)
        { 
        
        }
        public SuccesDataResult(string massege):base(default,true,massege)
        
        {
        

        }

        public SuccesDataResult():base(default,true) 
        { 

        }


    }
}
