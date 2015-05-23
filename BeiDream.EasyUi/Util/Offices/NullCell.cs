namespace Util.Offices {
    /// <summary>
    /// 空单元格
    /// </summary>
    public class NullCell : Cell {
        /// <summary>
        /// 初始化空单元格
        /// </summary>
        public NullCell() 
            : base( "",1,1 ){
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        public override bool IsNull() {
            return true;
        }
    }
}
