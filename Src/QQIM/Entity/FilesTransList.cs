using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientView
{
    class FilesTransList
    {
        public String Savepath { get; set; }
        public String Remotepath { get; set; }
        public List<string> FileNamesList { get; set; }
        public Boolean FilesGetFinished { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FilesSize { get; set; }
        /// <summary>
        /// file path
        /// </summary>
        public string FileOrPath { get; set; } 
    }
}
