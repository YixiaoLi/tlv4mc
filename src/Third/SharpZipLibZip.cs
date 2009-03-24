using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using ICSharpCode.SharpZipLib.Zip;

namespace NU.OJL.MPRTOS.TLV.Third
{
    public class SharpZipLibZip : IZip
    {

        public void Compress(string zipFilePath, string targetDirectoryPath)
        {
            new FastZip().CreateZip(zipFilePath, targetDirectoryPath, false, "", "");
        }

        public void Extract(string zipFilePath, string targetDirectoryPath)
        {
            new FastZip().ExtractZip(zipFilePath, targetDirectoryPath, "");
        }

    }
}
