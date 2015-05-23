namespace Util {
    /// <summary>
    /// 格式化
    /// </summary>
    public static class Format {
        /// <summary>
        /// 移除尾随0
        /// </summary>
        /// <param name="value">值</param>
        public static string RemoveEnd0( decimal value ) {
            string result = value.ToString();
            if ( result.IndexOf( '.' ) < 0 )
                return result;
            if ( result.EndsWith( "0" ) )
                result = result.TrimEnd( '0' );
            if( result.EndsWith( "." ) )
                result = result.TrimEnd( '.' );
            return result;
        }
    }
}
