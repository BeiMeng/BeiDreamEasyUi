using System.Collections.Generic;
using System.Linq;

namespace Util {
    /// <summary>
    /// 随机数操作
    /// </summary>
    public class Random {
        /// <summary>
        /// 随机数
        /// </summary>
        private readonly System.Random _random;

        /// <summary>
        /// 初始化随机数
        /// </summary>
        public Random() {
            _random = new System.Random();
        }

        /// <summary>
        /// 获取指定范围的随机整数，该范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public int GetInt( int minNum, int maxNum ) {
            return _random.Next( minNum, maxNum );
        }

        /// <summary>
        /// 获取随机排序的集合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        public static List<T> GetSortList<T>( IEnumerable<T> array ) {
            if ( array == null )
                return null;
            var list = array.ToList();
            var random = new Random();
            for ( int i = 0; i < list.Count; i++ ) {
                int position1 = random.GetInt( 0, list.Count );
                int positio2 = random.GetInt( 0, list.Count );
                T temp = list[position1];
                list[position1] = list[positio2];
                list[positio2] = temp;
            }
            return list;
        }
    }
}
