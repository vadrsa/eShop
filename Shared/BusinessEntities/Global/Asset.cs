using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessEntities.Global
{
    public class Asset
    {
        public string RelativePath { get; set; }
        public string FileName
        {
            get
            {
                return Path.GetFileName(RelativePath);
            }
        }
        public byte[] Contents;
    }
}
