using System;
using System.Collections.Generic;
using System.Linq;

namespace Util {
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class Conv {

        #region 数值转换

        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt( object data ) {
            if( data == null )
                return 0;
            int result;
            var success = int.TryParse( data.ToString(), out result );
            if ( success )
                return result;
            try {
                return Convert.ToInt32( ToDouble( data,0 ) );
            }
            catch( Exception ) {
                return 0;
            }
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull( object data ) {
            if( data == null )
                return null;
            int result;
            bool isValid = int.TryParse( data.ToString(), out result );
            if( isValid )
                return result;
            return null;
        }

        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble( object data ) {
            if( data == null )
                return 0;
            double result;
            return double.TryParse( data.ToString(), out result ) ? result : 0;
        }

        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble( object data, int digits ) {
            return Math.Round( ToDouble( data ), digits );
        }

        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull( object data ) {
            if( data == null )
                return null;
            double result;
            bool isValid = double.TryParse( data.ToString(), out result );
            if( isValid )
                return result;
            return null;
        }

        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal( object data ) {
            if( data == null )
                return 0;
            decimal result;
            return decimal.TryParse( data.ToString(), out result ) ? result : 0;
        }

        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal( object data, int digits ) {
            return Math.Round( ToDecimal( data ), digits );
        }

        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull( object data ) {
            if( data == null )
                return null;
            decimal result;
            bool isValid = decimal.TryParse( data.ToString(), out result );
            if( isValid )
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull( object data, int digits ) {
            var result = ToDecimalOrNull( data );
            if( result == null )
                return null;
            return Math.Round( result.Value, digits );
        }

        #endregion

        #region Guid转换

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid ToGuid( object data ) {
            if( data == null )
                return Guid.Empty;
            Guid result;
            return Guid.TryParse( data.ToString(), out result ) ? result : Guid.Empty;
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="data">数据</param>
        public static Guid? ToGuidOrNull( object data ) {
            if( data == null )
                return null;
            Guid result;
            bool isValid = Guid.TryParse( data.ToString(), out result );
            if( isValid )
                return result;
            return null;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="list">guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<Guid> ToGuidList( string list ) {
            return ToList<Guid>( list );
        }

        #endregion

        #region 日期转换

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate( object data ) {
            if ( data == null )
                return DateTime.MinValue;
            DateTime result;
            return DateTime.TryParse( data.ToString(), out result ) ? result : DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull( object data ) {
            if ( data == null )
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse( data.ToString(), out result );
            if ( isValid )
                return result;
            return null;
        }

        #endregion

        #region 布尔转换

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool( object data ) {
            if ( data == null )
                return false;
            bool? value = GetBool( data );
            if ( value != null )
                return value.Value;
            bool result;
            return bool.TryParse( data.ToString(), out result ) && result;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool( object data ) {
            switch ( data.ToString().Trim().ToLower() ) {
                case "0":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "否":
                    return false;
                case "yes":
                    return true;
                case "no":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull( object data ) {
            if ( data == null )
                return null;
            bool? value = GetBool( data );
            if ( value != null )
                return value.Value;
            bool result;
            bool isValid = bool.TryParse( data.ToString(), out result );
            if ( isValid )
                return result;
            return null;
        }

        #endregion

        #region 字符串转换

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString( object data ) {
            return data == null ? string.Empty : data.ToString().Trim();
        }

        #endregion

        #region 通用转换

        /// <summary>
        /// 转换为目标元素
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="data">数据</param>
        public static T To<T>( object data ) {
            if ( data == null )
                return default( T );
            if ( data is string && string.IsNullOrWhiteSpace( data.ToString() ) )
                return default( T );
            Type type = Sys.GetType<T>();
            try {
                if ( type.Name.ToLower() == "guid" )
                    return (T)(object)new Guid( data.ToString() );
                if ( data is IConvertible )
                    return (T)Convert.ChangeType( data, type );
                return (T)data;
            }
            catch {
                return default( T );
            }
        }

        /// <summary>
        /// 转换为目标元素集合
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="list">元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>( string list ) {
            var result = new List<T>();
            if ( string.IsNullOrWhiteSpace( list ) )
                return result;
            var array = list.Split( ',' );
            result.AddRange( from each in array where !string.IsNullOrWhiteSpace( each ) select To<T>( each ) );
            return result;
        }

        #endregion
    }
}
