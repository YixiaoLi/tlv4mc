
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class FilePathUndefinedException : Exception
    {
        public FilePathUndefinedException()
        {
        }
        public FilePathUndefinedException(string message)
            : base(message)
        {
        }
        public FilePathUndefinedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
