using System;
using System.IO;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Util.Webs {
    /// <summary>
    /// HtmlHelper扩展
    /// </summary>
    public static class Extentions {
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static IMvcService X( this HtmlHelper helper ) {
            return new MvcService();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="expression">属性表达式</param>
        public static object Value<TEntity,TProperty>( this HtmlHelper<TEntity> helper,Expression<Func<TEntity, TProperty>> expression ) {
            if ( expression == null )
                return string.Empty;
            var metadata = ModelMetadata.FromLambdaExpression( expression, helper.ViewData );
            return metadata.Model;
        }

        /// <summary>
        /// 获取Html写入器
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="helper">HtmlHelper</param>
        public static TextWriter Writer<TEntity>( this HtmlHelper<TEntity> helper ) {
            return helper.ViewContext.Writer;
        }
    }
}
