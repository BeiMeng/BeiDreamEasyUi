using System;
using System.Security;
using System.Text;
using Util;
using Util.Files;

namespace BeiDream.Services.Systems.Configs {
    /// <summary>
    /// 租户上传路径策略
    /// </summary>
    public class TenantUploadPathStrategy : IUploadPathStrategy {
        /// <summary>
        /// 获取上传路径,形式：/基目录/租户目录/文件分类目录/yyyy-MM-dd/用户Id/文件名-HHmmss.扩展名
        /// </summary>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <param name="fileCategory">文件分类目录</param>
        /// <param name="baseCategory">基目录</param>
        public string GetPath( string fileName, string fileCategory = "", string baseCategory = "" ) {
            return GetDirectoryPath( baseCategory, fileCategory ) + FileInfo.GetSafeName( fileName );
        }

        /// <summary>
        /// 获取上传目录路径,目录形式：/基分类目录/租户目录/文件分类目录/yyyy-MM-dd/用户名/
        /// </summary>
        private string GetDirectoryPath( string baseCategory, string fileCategory ) {
            var result = new StringBuilder();
            result.AppendFormat( "/{0}/{1}/{2}", GetBaseCatetory(baseCategory), GetTenantCategory(), GetFileCategory( fileCategory ) );
            result.AppendFormat( "/{0}/{1}/", DateTime.Now.ToDateString(), GetUserId() );
            return result.ToString();
        }

        /// <summary>
        /// 获取基目录
        /// </summary>
        private string GetBaseCatetory( string baseCategory ) {
            if ( baseCategory.IsEmpty() )
                return "UploadFiles";
            return baseCategory;
        }

        /// <summary>
        /// 获取租户目录
        /// </summary>
        private string GetTenantCategory() {
                return "Default";
        }

        /// <summary>
        /// 获取文件分类目录
        /// </summary>
        private string GetFileCategory( string fileCategory ) {
            if ( string.IsNullOrWhiteSpace( fileCategory ) )
                return "Default";
            return fileCategory;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        private string GetUserId() {
                return "Default";
        }
    }
}
