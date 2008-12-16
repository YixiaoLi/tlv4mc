﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class ResourceFileValidationException : Exception
    {
        public ResourceFileValidationException()
        {
        }
        public ResourceFileValidationException(string message)
            : base(message)
        {
        }
        public ResourceFileValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}