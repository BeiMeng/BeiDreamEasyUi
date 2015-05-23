namespace Util.Offices {
    /// <summary>
    /// 单元格
    /// </summary>
    public class Cell : NullObject{
        /// <summary>
        /// 初始化单元格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="columnSpan">列跨度</param>
        /// <param name="rowSpan">行跨度</param>
        public Cell( object value,int columnSpan = 1,int rowSpan = 1 ) {
            Value = value;
            ColumnSpan = columnSpan;
            RowSpan = rowSpan;
        }

        /// <summary>
        /// 行
        /// </summary>
        public Row Row { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        private int _columnSpan;
        /// <summary>
        /// 列跨度
        /// </summary>
        public int ColumnSpan {
            get { return _columnSpan; }
            set {
                if ( value < 1 )
                    value = 1;
                _columnSpan = value;
            }
        }

        private int _rowSpan;
        /// <summary>
        /// 行跨度
        /// </summary>
        public int RowSpan {
            get { return _rowSpan; }
            set {
                if ( value < 1 )
                    value = 1;
                _rowSpan = value;
            }
        }

        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex {
            get {
                Row.CheckNull( "Row" );
                return Row.RowIndex;
            }
        }

        /// <summary>
        /// 列索引
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 结束行索引
        /// </summary>
        public int EndRowIndex {
            get { return RowIndex + RowSpan - 1; }
        }

        /// <summary>
        /// 结束列索引
        /// </summary>
        public int EndColumnIndex {
            get { return ColumnIndex + ColumnSpan - 1; }
        }

        /// <summary>
        /// 是否需要合并单元格
        /// </summary>
        public bool NeedMerge {
            get { return ColumnSpan > 1 || RowSpan > 1; }
        }

        /// <summary>
        /// 创建空单元格
        /// </summary>
        public static Cell Null{
            get { return new NullCell();}
        }
    }
}
