using System;
using Json.Net;

namespace Util {
    /// <summary>
    /// Json特性
    /// </summary>
    [AttributeUsage( AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false )]
    public class JsonAttribute : JsonPropertyAttribute{
        /// <summary>
        /// 初始化Json特性
        /// </summary>
        public JsonAttribute() 
            : this( true ){
        }
        /// <summary>
        /// 初始化Json特性
        /// </summary>
        /// <param name="isAddQuoteForString">是否为字符串添加引号</param>
        public JsonAttribute( bool isAddQuoteForString ) {
            IsAddQuoteForString = isAddQuoteForString;
        }
    }
}
