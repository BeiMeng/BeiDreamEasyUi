using System.ComponentModel;

namespace Util.Files {
    /// <summary>
    /// 文件容量单位
    /// </summary>
    public enum FileUnit {
        /// <summary>
        /// 字节
        /// </summary>
        [Description( "B" )]
        Byte = 1,
        /// <summary>
        /// K字节
        /// </summary>
        [Description( "KB" )]
        K = 2,
        /// <summary>
        /// M字节
        /// </summary>
        [Description( "MB" )]
        M = 3,
        /// <summary>
        /// G字节
        /// </summary>
        [Description( "GB" )]
        G = 4
    }

    /// <summary>
    /// 文件容量单位枚举扩展
    /// </summary>
    public static class FileUnitExtensions {
        /// <summary>
        /// 获取描述
        /// </summary>
        public static string Description( this FileUnit? unit ) {
            return unit == null ? string.Empty : unit.Value.Description();
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public static int? Value( this FileUnit? unit ) {
            if ( unit == null )
                return null;
            return unit.Value.Value();
        }
    }
}
