using Json.Net;

namespace Util.Webs.EasyUi.Configs {
    /// <summary>
    /// 子表格配置项
    /// </summary>
    public class SubGridOption {
        /// <summary>
        /// 初始化子表格配置项
        /// </summary>
        public SubGridOption() {
            Options = new DataGridOption();
        }

        /// <summary>
        /// 选项
        /// </summary>
        [Json( PropertyName = "options", NullValueHandling = NullValueHandling.Ignore )]
        public DataGridOption Options { get; set; }

        /// <summary>
        /// 子表格
        /// </summary>
        [Json( PropertyName = "subgrid", NullValueHandling = NullValueHandling.Ignore )]
        protected SubGridOption SubGrid { get;  set; }

        /// <summary>
        /// 添加子表格配置项
        /// </summary>
        /// <param name="option">子表格配置项</param>
        public void SetSubGrid( SubGridOption option ) {
            SubGrid = option;
        }

        /// <summary>
        /// 输出Json结果
        /// </summary>
        public override string ToString() {
            return Json.ToJson( this,true );
        }
    }
}
