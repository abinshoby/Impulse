﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Iway.Repository.Helper
{
    public class FileContent : MultipartFormDataContent
    {
        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            Add(new StreamContent(filestream), apiParamName, filename);
        }
    }
}
