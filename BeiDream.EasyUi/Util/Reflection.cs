using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Util {
    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection {

        #region GetAssembly(获取程序集)

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly( string assemblyName ) {
            return Assembly.Load( assemblyName );
        }

        #endregion

        #region GetDescription(获取描述)

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>( string memberName ) {
            return GetDescription( Sys.GetType<T>(), memberName );
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription( Type type, string memberName ) {
            if ( type == null )
                return string.Empty;
            if ( string.IsNullOrWhiteSpace( memberName ) )
                return string.Empty;
            return GetDescription( type, type.GetField( memberName ) );
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="field">成员</param>
        public static string GetDescription( Type type, FieldInfo field ) {
            if ( type == null )
                return string.Empty;
            if ( field == null )
                return string.Empty;
            var attribute = field.GetCustomAttributes( typeof( DescriptionAttribute ), true ).FirstOrDefault() as DescriptionAttribute;
            if ( attribute == null )
                return field.Name;
            return attribute.Description;
        }

        #endregion

        #region CreateInstance(动态创建实例)

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="className">类名，包括命名空间,如果类型不处于当前执行程序集中，需要包含程序集名，范例：Test.Core.Test2,Test.Core</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>( string className, params object[] parameters ) {
            Type type = Type.GetType( className ) ?? Assembly.GetCallingAssembly().GetType( className );
            return CreateInstance<T>( type, parameters );
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>( Type type, params object[] parameters ) {
            return Conv.To<T>( Activator.CreateInstance( type, parameters ) );
        }

        #endregion

        #region IsBool(是否布尔类型)

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool( MemberInfo member ) {
            if ( member == null )
                return false;
            switch ( member.MemberType ) {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";
                case MemberTypes.Property:
                    return IsBool( (PropertyInfo)member );
            }
            return false;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool( PropertyInfo property ) {
            if ( property.PropertyType == typeof( bool ) )
                return true;
            if ( property.PropertyType == typeof( bool? ) )
                return true;
            return false;
        }

        #endregion

        #region IsEnum(是否枚举类型)

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum( MemberInfo member ) {
            if ( member == null )
                return false;
            switch ( member.MemberType ) {
                case MemberTypes.TypeInfo:
                    return ( (TypeInfo)member ).IsEnum;
                case MemberTypes.Property:
                    return IsEnum( (PropertyInfo)member );
            }
            return false;
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum( PropertyInfo property ) {
            if ( property.PropertyType.IsEnum )
                return true;
            var value = Nullable.GetUnderlyingType( property.PropertyType );
            if ( value == null )
                return false;
            return value.IsEnum;
        }

        #endregion

        #region IsDate(是否日期类型)

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate( MemberInfo member ) {
            if ( member == null )
                return false;
            switch ( member.MemberType ) {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";
                case MemberTypes.Property:
                    return IsDate( (PropertyInfo)member );
            }
            return false;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate( PropertyInfo property ) {
            if ( property.PropertyType == typeof( DateTime ) )
                return true;
            if ( property.PropertyType == typeof( DateTime? ) )
                return true;
            return false;
        }

        #endregion

        #region IsInt(是否整型)

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt( MemberInfo member ) {
            if ( member == null )
                return false;
            switch ( member.MemberType ) {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int32" || member.ToString() == "System.Int16" || member.ToString() == "System.Int64";
                case MemberTypes.Property:
                    return IsInt( (PropertyInfo)member );
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsInt( PropertyInfo property ) {
            if ( property.PropertyType == typeof( int ) )
                return true;
            if ( property.PropertyType == typeof( int? ) )
                return true;
            if ( property.PropertyType == typeof( short ) )
                return true;
            if ( property.PropertyType == typeof( short? ) )
                return true;
            if ( property.PropertyType == typeof( long ) )
                return true;
            if ( property.PropertyType == typeof( long? ) )
                return true;
            return false;
        }

        #endregion

        #region IsNumber(是否数值类型)

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber( MemberInfo member ) {
            if ( member == null )
                return false;
            switch ( member.MemberType ) {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Double" || member.ToString() == "System.Decimal" || member.ToString() == "System.Single";
                case MemberTypes.Property:
                    return IsNumber( (PropertyInfo)member );
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsNumber( PropertyInfo property ) {
            if ( property.PropertyType == typeof( double ) )
                return true;
            if ( property.PropertyType == typeof( double? ) )
                return true;
            if ( property.PropertyType == typeof( decimal ) )
                return true;
            if ( property.PropertyType == typeof( decimal? ) )
                return true;
            if ( property.PropertyType == typeof( float ) )
                return true;
            if ( property.PropertyType == typeof( float? ) )
                return true;
            return false;
        }

        #endregion

        #region GetByInterface(获取实现了接口的所有具体类型)

        /// <summary>
        /// 获取实现了接口的所有具体类型
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="assembly">在该程序集中查找</param>
        public static List<T> GetByInterface<T>( Assembly assembly ) {
            var typeInterface = typeof( T );
            return assembly.GetTypes()
                .Where( t => typeInterface.IsAssignableFrom( t ) && t != typeInterface && t.IsAbstract == false )
                .Select( t => CreateInstance<T>( t ) ).ToList();
        }

        #endregion

        #region GetAssemblies(从目录中获取所有程序集)

        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<Assembly> GetAssemblies( string directoryPath ) {
            var filePaths = File.GetAllFiles( directoryPath ).Where( t => t.EndsWith( ".exe" ) || t.EndsWith( ".dll" ) );
            return filePaths.Select( Assembly.LoadFile ).ToList();
        }

        #endregion
    }
}
