using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 实体子表格列
    /// </summary>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntitySubGridColumn<TProperty> : SubGridColumn {
        /// <summary>
        /// 初始化实体子表格列
        /// </summary>
        /// <param name="expression">属性表达式</param>
        public EntitySubGridColumn( Expression<Func<TProperty>> expression ) {
            InitField( expression );
            InitTitle( expression );
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        private void InitField( Expression<Func<TProperty>> expression ) {
            var name = Lambda.GetName( expression );
            Field( Str.GetLastProperty( name ) );
        }

        /// <summary>
        /// 初始化标题
        /// </summary>
        private void InitTitle( Expression<Func<TProperty>> expression ) {
            var title = Lambda.GetAttribute<TProperty, DisplayAttribute>( expression );
            Text( title.Name );
        }
    }
}
