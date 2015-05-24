using System.Text;
using Util;

namespace BeiDream.Common {
    /// <summary>
    /// 状态描述
    /// </summary>
    public abstract class StateDescription {
        /// <summary>
        /// 描述
        /// </summary>
        private StringBuilder _description;

        /// <summary>
        /// 输出对象状态
        /// </summary>
        public override string ToString() {
            _description = new StringBuilder();
            AddDescriptions();
            return _description.ToString().TrimEnd().TrimEnd( ',' );
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected virtual void AddDescriptions() {
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected void AddDescription( string description ) {
            if ( description.IsEmpty() )
                return;
            _description.Append( description );
        }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected void AddDescription<T>( string name, T value ) {
            if ( value.ToStr().IsEmpty() )
                return;
            _description.AppendFormat( "{0}:{1},", name, value );
        }
    }
}
