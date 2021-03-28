using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.AttributeHelper
{
    /// <summary>
    /// DTO实体属性验证判断
    /// 范围:
    /// 只作用于dto实体的属性
    /// type范围:(小写)
    /// string :空或长度
    /// int : 大于零
    /// long : 大于零
    /// datetime : 时间类型判断
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidateAttribute : Attribute
    {
        private ValidateAttribute()
        {

        }
        //string/int/long等 判空
        public ValidateAttribute(string type, string errorMsg)
        {
            this.PropertyType = type;
            this.ErrorMsg = errorMsg;   
        }
        //string/int/long等长度验证及 判空
        public ValidateAttribute(string type, int minLength, int maxLength, string errorMsg)
        {
            this.PropertyType = type;
            this.ErrorMsg = errorMsg; 
            this.MaxLength = maxLength;
            this.MinLength = minLength;
        }
        /// <summary>
        /// 属性类型  string/int/long
        /// </summary>
        public string PropertyType { get; set; }
        public string ErrorMsg { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
    }
}
