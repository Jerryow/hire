using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.ImageHelper
{
    public class ImageInfo
    {
        /// <summary>
        /// 原图片地址
        /// </summary>
        public string SourcePicPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OffSetX { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OffSetY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CutHeight { get; set; }
    }
}
