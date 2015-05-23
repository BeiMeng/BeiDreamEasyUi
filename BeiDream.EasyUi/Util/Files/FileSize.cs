namespace Util.Files {
    /// <summary>
    /// 文件大小
    /// </summary>
    public struct FileSize {
        /// <summary>
        /// 初始化文件大小
        /// </summary>
        /// <param name="size">文件字节大小</param>
        /// <param name="unit">文件字节大小</param>
        public FileSize( long size,FileUnit unit = FileUnit.Byte ) {
            _size = GetSize( size, unit );
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        private static long GetSize( long size, FileUnit unit ) {
            switch ( unit ) {
                case FileUnit.K:
                    return size * 1024;
                case FileUnit.M:
                    return size * 1024 * 1024;
                case FileUnit.G:
                    return size * 1024 * 1024 * 1024;
                default :
                    return size;
            }
        }

        private readonly long _size;
        /// <summary>
        /// 文件字节长度
        /// </summary>
        public long Size { get { return _size; } }

        /// <summary>
        /// 获取文件大小，单位：G
        /// </summary>
        public double GetSizeByG() {
            return Conv.ToDouble( _size/1024.0/1024.0/1024.0, 2 );
        }

        /// <summary>
        /// 获取文件大小，单位：M
        /// </summary>
        public double GetSizeByM() {
            return Conv.ToDouble( _size/1024.0/1024.0, 2 );
        }

        /// <summary>
        /// 获取文件大小，单位：K
        /// </summary>
        public double GetSizeByK() {
            return Conv.ToDouble( _size / 1024.0, 2 );
        }

        /// <summary>
        /// 获取文件大小，单位：字节
        /// </summary>
        public int GetSize() {
            return (int)Size;
        }

        /// <summary>
        /// 输出描述
        /// </summary>
        public override string ToString() {
            if ( _size >= 1024 * 1024 * 1024 )
                return string.Format( "{0} {1}", GetSizeByG(), FileUnit.G.Description() );
            if ( _size >= 1024 * 1024 )
                return string.Format( "{0} {1}", GetSizeByM(), FileUnit.M.Description() );
            if ( _size >= 1024 )
                return string.Format( "{0} {1}", GetSizeByK(), FileUnit.K.Description() );
            return string.Format( "{0} {1}", _size, FileUnit.Byte.Description() );
        }
    }
}
