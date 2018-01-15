using System.Collections.Generic;
using System.IO;

namespace LCApp {
    public class Person {
        private string _id;                  //序号——》输出到表格中 隐藏
        private string _name;            //名字——》输出到表格中 隐藏
        private string _level;              //级别——》输出到表格中 隐藏
        private string _srcImg;          //缩略图存放路径——》输出到表格中 隐藏
        private string _srcInfo;         //简介图存放路径——》输出到表格中 隐藏
        private string _srcPhoto;      //生活照存放路径——》输出到表格中 隐藏
        private string _imgName;     //缩略图文件名——》输出到表格中 隐藏
        private string _infoName;    //简介图文件名——》输出到表格中 隐藏
        private string _srcImgPath;  //缩略图文件 绝对路径——》输出到表格中 隐藏
        private string _srcInfoPath;  //简介图文件 绝对路径——》输出到表格中 隐藏
        private  List<string> _fullPhotosName = new List<string>();//生活照文件 绝对路径 数组——》输出到表格中 隐藏



        /// <summary>
        /// 清除生活照数组的元素
        /// </summary>
        public void DeleteFullPhotoNames() {
            foreach (var item in FullPhotosName)
            {
                File.Delete(_srcPhoto + item);
            }

            FullPhotosName.Clear();
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string SrcImg
        {
            get { return _srcImg; }
            set { _srcImg = value; }
        }

        /// <summary>
        /// 简介图路径
        /// </summary>
        public string SrcInfo
        {
            get { return _srcInfo; }
            set { _srcInfo = value; }
        }

        /// <summary>
        /// 生活照路径
        /// </summary>
        public string SrcPhoto
        {
            get { return _srcPhoto; }
            set { _srcPhoto = value; }
        }

        /// <summary>
        /// 缩略图文件名
        /// </summary>
        public string ImgName
        {
            get { return _imgName; }
            set { _imgName = value; }
        }

        /// <summary>
        /// 简介图文件名
        /// </summary>
        public string InfoName
        {
            get { return _infoName; }
            set { _infoName = value; }
        }
        /// <summary>
        /// 缩略图文件 绝对路径
        /// </summary>
        public string SrcImgPath
        {
            get { return _srcImgPath; }
            set { _srcImgPath = value; }
        }

        /// <summary>
        /// 简介图文件 绝对路径
        /// </summary>
        public string SrcInfoPath
        {
            get { return _srcInfoPath; }
            set { _srcInfoPath = value; }
        }

        /// <summary>
        /// 生活照 数组 文件绝对路径
        /// </summary>
        public List<string> FullPhotosName
        {
            get { return _fullPhotosName; }
            set { _fullPhotosName = value; }
        }
    }
}
