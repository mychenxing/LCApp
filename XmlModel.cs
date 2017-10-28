using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCApp {
    public class XmlModel {
        public XmlModel() { }

        /// <summary>
        /// 所对应的70缩略图文件夹路径
        /// </summary>
        private string imgSrc70;
        public string ImgSrc70
        {
            get { return imgSrc70; }
            set { imgSrc70 = value; }
        }
        /// <summary>
        /// 所对应的80缩略图文件夹路径
        /// </summary>
        private string imgSrc80;
        public string ImgSrc80
        {
            get { return imgSrc80; }
            set { imgSrc80 = value; }
        }
        /// <summary>
        /// 所对应的90缩略图文件夹路径
        /// </summary>
        private string imgSrc90;
        public string ImgSrc90
        {
            get { return imgSrc90; }
            set { imgSrc90 = value; }
        }

        /// <summary>
        /// 所对应的70简介大图文件夹路径
        /// </summary>
        private string imgInfoSrc70;
        public string ImgInfoSrc70
        {
            get { return imgInfoSrc70; }
            set { imgInfoSrc70 = value; }
        }
        /// <summary>
        /// 所对应的80简介大图文件夹路径
        /// </summary>
        private string imgInfoSrc80;
        public string ImgInfoSrc80
        {
            get { return imgInfoSrc80; }
            set { imgInfoSrc80 = value; }
        }
        /// <summary>
        /// 所对应的90简介大图文件夹路径
        /// </summary>
        private string imgInfoSrc90;
        public string ImgInfoSrc90
        {
            get { return imgInfoSrc90; }
            set { imgInfoSrc90 = value; }
        }

        /// <summary>
        /// 所对应的70生活照大图文件夹路径
        /// </summary>
        private string imgPhotoSrc70;
        public string ImgPhotoSrc70
        {
            get { return imgPhotoSrc70; }
            set { imgPhotoSrc70 = value; }
        }
        /// <summary>
        /// 所对应的80生活照大图文件夹路径
        /// </summary>
        private string imgPhotoSrc80;
        public string ImgPhotoSrc80
        {
            get { return imgPhotoSrc80; }
            set { imgPhotoSrc80 = value; }
        }
        /// <summary>
        /// 所对应的90生活照大图文件夹路径
        /// </summary>
        private string imgPhotoSrc90;
        public string ImgPhotoSrc90
        {
            get { return imgPhotoSrc90; }
            set { imgPhotoSrc90 = value; }
        }
    }
}
