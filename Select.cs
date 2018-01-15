using System.Collections.Generic;

namespace LCApp {
    class Select
    {
        private int _id;                                      //序号
        private string _name;                           //姓名
        private string _genre;                           //市场类别
        private string _rank;                             //级别
        private string _imgPath;                       //缩略图路径
        private string _infoPath;                       //简介图路径
        private List<string> _photoPaths;        //生活照路径列表

        /// <summary>
        /// 序号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 市场类别
        /// </summary>
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        /// <summary>
        /// 级别
        /// </summary>
        public string Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        /// <summary>
        /// 缩略图绝对路径
        /// </summary>
        public string ImgPath
        {
            get { return _imgPath; }
            set { _imgPath = value; }
        }

        /// <summary>
        /// 简介图绝对路径
        /// </summary>
        public string InfoPath
        {
            get { return _infoPath; }
            set { _infoPath = value; }
        }

        /// <summary>
        /// 生活照绝对路径列表
        /// </summary>
        public List<string> PhotoPaths
        {
            get { return _photoPaths; }
            set { _photoPaths = value; }
        }
    }
}
