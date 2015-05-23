namespace Util {
    /// <summary>
    /// 尺寸
    /// </summary>
    public struct Size {
        /// <summary>
        /// 初始化尺寸
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public Size( int width, int height ) {
            _width = width;
            _height = height;
        }

        private readonly int _width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get { return _width; } }

        private readonly int _height;
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get { return _height; } }
    }
}
