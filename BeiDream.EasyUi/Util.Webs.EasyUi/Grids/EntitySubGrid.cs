using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 实体子表格
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntitySubGrid<TEntity,TProperty> : SubDataGrid{
        /// <summary>
        /// 初始化实体子表格
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="funcCreateColumns">创建列集合方法</param>
        public EntitySubGrid( Expression<Func<TEntity, IEnumerable<TProperty>>> expression, Func<TProperty, ISubGridColumn[]> funcCreateColumns ) {
            InitField( expression );
            InitColumns( funcCreateColumns );
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        private void InitField( Expression<Func<TEntity, IEnumerable<TProperty>>> expression ) {
            var name = Lambda.GetName( expression );
            Property( Str.GetLastProperty( name ) );
        }

        /// <summary>
        /// 初始化列集合
        /// </summary>
        private void InitColumns( Func<TProperty, ISubGridColumn[]> funcCreateColumns ) {
            var columns = funcCreateColumns( default( TProperty ) );
            Columns( columns );
        }
    }
}
