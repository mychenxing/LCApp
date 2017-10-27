using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCApp {
    public class XmlModel {
        public XmlModel() { }

        /// <summary>
        /// 所对应的缩略图文件夹路径
        /// </summary>
        private string imgSrc;
        public string ImgSrc
        {
            get { return imgSrc; }
            set { imgSrc = value; }
        }

        /// <summary>
        /// 所对应的简介大图文件夹路径
        /// </summary>
        private string imgInfoSrc;
        public string ImgInfoSrc
        {
            get { return imgInfoSrc; }
            set { imgInfoSrc = value; }
        }

        /// <summary>
        /// 所对应的生活照大图文件夹路径
        /// </summary>
        private string imgPhotoSrc;
        public string ImgPhotoSrc
        {
            get { return imgInfoSrc; }
            set { imgInfoSrc = value; }
        }
    }
}
