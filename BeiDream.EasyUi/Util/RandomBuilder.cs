using System;
using System.Text;

namespace Util {
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public class RandomBuilder {
        /// <summary>
        /// 初始化随机数生成器
        /// </summary>
        public RandomBuilder() {
            _random = new Random();
        }

        /// <summary>
        /// 随机数操作
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateString( int maxLength ) {
            return Generate( maxLength, Const.Letters + Const.Numbers );
        }

        /// <summary>
        /// 生成随机常用汉字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateChinese( int maxLength ) {
            return Generate( maxLength, Const.SimplifiedChinese );
        }

        /// <summary>
        /// 生成随机字母，不出现汉字和数字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateLetters( int maxLength ) {
            return Generate( maxLength, Const.Letters );
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        private string Generate( int maxLength,string text ) {
            var length = GetLength( maxLength );
            var result = new StringBuilder();
            for ( int i = 0; i < length; i++ )
                result.Append( GetRandomChar( text ) );
            return result.ToString();
        }

        /// <summary>
        /// 获取随机长度
        /// </summary>
        private int GetLength( int maxLength ) {
            return _random.GetInt( 1, maxLength );
        }

        /// <summary>
        /// 获取随机字符
        /// </summary>
        private string GetRandomChar( string text ) {
            var index = _random.GetInt( 1, text.Length );
            return text[index].ToString();
        }

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        public bool GenerateBool() {
            var random = _random.GetInt( 1, 3 );
            if ( random == 1 )
                return false;
            return true;
        }

        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="beginYear">起始年份</param>
        /// <param name="endYear">结束年份</param>
        public DateTime GenerateDate(int beginYear = 2000,int endYear = 2030) {
            var year = _random.GetInt( beginYear, endYear );
            var month = _random.GetInt( 1, 13 );
            var day = _random.GetInt( 1, 29 );
            var hour = _random.GetInt( 1, 24 );
            var minute = _random.GetInt( 1, 60 );
            var second = _random.GetInt( 1, 60 );
            return new DateTime( year, month, day, hour, minute, second );
        }

        /// <summary>
        /// 生成随机整数
        /// </summary>
        /// <param name="maxValue">整数最大值</param>
        public int GenerateInt( int maxValue ) {
            return _random.GetInt( 0, maxValue + 1 );
        }

        /// <summary>
        /// 生成随机枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        public T GenerateEnum<T>() {
            var list = Enum.GetItems<T>();
            int index = _random.GetInt( 0, list.Count );
            return Enum.GetInstance<T>( list[index].Value );
        }
    }
}
