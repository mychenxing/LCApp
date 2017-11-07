using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LCApp {
    public class Person {
        public Person() { }
        private string id;//序号
        private string name;//名字
        private string level;//级别
        private string srcImg;//缩略图存放路径
        private string srcInfo;//简介图存放路径
        private string srcPhoto;//生活照存放路径
        public string FullImgName; //缩略图文件名
        public string FullInfoName;//简介图文件名
        public List<string> FullPhotosName = new List<string>();//生活照文件名数组

        /// <summary>
        /// 清除生活照数组的元素
        /// </summary>
        public void DeleteFullPhotoNames() {
            foreach (var item in FullPhotosName)
            {
                File.Delete(@srcPhoto + item);
            }

            FullPhotosName.Clear();
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level
        {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string SrcImg
        {
            get { return srcImg; }
            set { srcImg = value; }
        }

        /// <summary>
        /// 简介图路径
        /// </summary>
        public string SrcInfo
        {
            get { return srcInfo; }
            set { srcInfo = value; }
        }

        /// <summary>
        /// 生活照路径
        /// </summary>
        public string SrcPhoto
        {
            get { return srcPhoto; }
            set { srcPhoto = value; }
        }
    }
}
