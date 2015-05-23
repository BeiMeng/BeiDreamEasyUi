using log4net.Layout;

namespace Util.Logs.Log4.Layouts {
    /// <summary>
    /// 自定义log4net布局组件
    /// </summary>
    public class CustomLayout : PatternLayout {
        /// <summary>
        /// 初始化
        /// </summary>
        public CustomLayout() {
            AddConverter( "property", typeof( CustomPatternLayoutConverter ) );
        }
    }
}
