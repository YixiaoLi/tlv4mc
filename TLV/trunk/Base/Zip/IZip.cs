using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface IZip
    {
        void Compress(string zipFilePath, string targetDirectoryPath);
        void Extract(string zipFilePath, string targetDirectoryPath);
    }
}
