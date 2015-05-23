using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Util.Webs.EasyUi.Commons;

namespace Util.Webs.EasyUi.Grids {
    /// <summary>
    /// 实体表格列
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class EntityDataGridColumn<TEntity, TProperty> : DataGridColumn {
        /// <summary>
        /// 初始化实体文本框
        /// </summary>
        /// <param name="expression">属性表达式</param>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="isEdit">是否允许编辑</param>
        public EntityDataGridColumn( Expression<Func<TEntity, TProperty>> expression, HtmlHelper<TEntity> helper, bool isEdit ) 
            : base( isEdit ){
            if ( helper != null )
                _metadata = ModelMetadata.FromLambdaExpression( expression, helper.ViewData );
            _expression = expression;
            _memberInfo = Lambda.GetMember( _expression );
            Init();
            ExpressionResolver<IDataGridColumn, TEntity, TProperty>.Resolve( this, expression );
        }

        /// <summary>
        /// 值
        /// </summary>
        private readonly ModelMetadata _metadata;
        /// <summary>
        /// 属性表达式
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> _expression;
        /// <summary>
        /// 成员
        /// </summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init() {
            InitField();
            InitText();
            InitType();
        }

        /// <summary>
        /// 初始化字段属性
        /// </summary>
        private void InitField() {
            Field( Lambda.GetName( _expression ) );
        }

        /// <summary>
        /// 初始化文本
        /// </summary>
        private void InitText() {
            if ( _metadata == null )
                return;
            Text( _metadata.DisplayName );
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        private void InitType() {
            if ( InitDate() )
                return;
            if ( InitBool() )
                return;
            if ( InitEnum() )
                return;
        }

        /// <summary>
        /// 初始化日期类型
        /// </summary>
        private bool InitDate() {
            if ( !Reflection.IsDate( _memberInfo ) )
                return false;
            FormatDate();
            return true;
        }

        /// <summary>
        /// 初始化布尔类型
        /// </summary>
        private bool InitBool() {
            if ( !Reflection.IsBool( _memberInfo ) )
                return false;
            FormatBool();
            if ( IsEdit )
                CheckBox();
            return true;
        }

        /// <summary>
        /// 初始化枚举类型
        /// </summary>
        private bool InitEnum() {
            if ( !Reflection.IsEnum( _memberInfo ) )
                return false;
            if ( !IsEdit )
                return false;
            Combox<TProperty>().PanelHeight().Editable( false );
            return true;
        }
    }
}
