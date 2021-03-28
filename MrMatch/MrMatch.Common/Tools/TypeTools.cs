using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrMatch.Common.Tools
{
    public class TypeTools
    {
        #region 数据验证
        /// <summary>
        /// 返回一个 Boolean 值，指示表达式的计算结果是否为数字。
        /// </summary>
        /// <param name="Expression">Object 表达式。</param>
        /// <returns>返回一个 Boolean 值，指示表达式的计算结果是否为数字。</returns>
        public static bool IsNumeric(object Expression)
        {
            if (Expression == null)
            {
                return false;
            }
            double num;
            return double.TryParse(Expression.ToString(), out num);
        }

        /*****************************************************************************/

        /// <summary>
        /// 返回一个 Boolean 值，指示表达式的计算结果是否为Int。
        /// </summary>
        /// <param name="Expression">Object 表达式。</param>
        /// <returns>返回一个 Boolean 值，指示表达式的计算结果是否为Int。</returns>
        public static bool IsInt(object Expression)
        {
            if (Expression == null)
            {
                return false;
            }
            int num;
            return int.TryParse(Expression.ToString(), out num);
        }

        /*****************************************************************************/


        /// <summary>
        /// 返回一个 Boolean 值，指示表达式是否为引用类型。
        /// </summary>
        /// <param name="Expression">Object 表达式</param>
        /// <returns>返回一个 Boolean 值，指示表达式是否为引用类型。</returns>
        public static bool IsReference(object Expression)
        {
            if (Expression == null)
            {
                return false;
            }
            return !(Expression is ValueType);
        }

        /*****************************************************************************/

        /// <summary>
        /// 返回一个 Boolean 值，指示表达式是否表示一个有效的 Date 值。
        /// </summary>
        /// <param name="Expression">Object 表达式。</param>
        /// <returns>返回一个 Boolean 值，指示表达式是否表示一个有效的 Date 值。</returns>
        public static bool IsDate(object Expression)
        {
            if (Expression != null)
            {
                if (Expression is DateTime)
                {
                    return true;
                }
                DateTime time;
                return DateTime.TryParse(Expression.ToString(), out time);
            }
            return false;
        }

        /*****************************************************************************/

        public static bool IsMail(object Expression)
        {
            if (Expression != null)
            {
                Regex RegEmail = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
                return RegEmail.Match(Expression.ToString()).Success;
            }
            return false;

        }

        public static bool IsMobine(object Expression)
        {
            if (Expression != null)
            {
                var len = Expression.ToString().Trim().Length;
                if (len < 11 || len > 15)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /*****************************************************************************/

        public static bool IsHasCHZN(object Expression)
        {
            if (Expression != null)
            {
                Regex RegCHZN = new Regex("[一-龥]");
                return RegCHZN.Match(Expression.ToString()).Success;
            }
            return false;

        }


        public static string DateValidate(DateTime start, string end)
        {
            if (string.IsNullOrEmpty(end))
            {
                return "结束时间不能为空";
            }

            var now = DateTime.Now;
            if (start >= now)
            {
                return "开始时间不能大于或等于当前时间";
            }

            if (end != "至今")
            {
                var endTime = now;
                var rtn = DateTime.TryParse(end, out endTime);
                if (!rtn)
                {
                    return "请输入正确的结束时间";
                }

                if (start >= endTime)
                {
                    return "开始时间不能大于或等于结束时间";
                }

                if (endTime > now)
                {
                    return "结束时间不能大于当前时间";
                }
            }
            return "成功";
        }
        #endregion

        #region 字符串处理
        /// <summary>
        /// 返回Hmtl解码后的文本
        /// </summary>
        /// <param name="str">string 表达式</param>
        /// <returns>返回Hmtl解码后的文本</returns>
        public static string HtmlDecode(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }


        /// <summary>
        /// 返回文本编码后的Hmtl
        /// </summary>
        /// <param name="str">string 表达式</param>
        /// <returns>返回文本编码后的Hmtl</returns>
        public static string HtmlEncode(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        #endregion

        #region 数据类型转换
        public static int ToInt(object Expression)
        {
            if (Expression == null) return 0;
            int _tmp;
            if (int.TryParse(Expression.ToString(), out _tmp))
            {
                return _tmp;
            }
            else
            {
                return 0;
            }
        }

        public static Byte ToByte(object Expression)
        {
            if (Expression == null) return 0;
            Byte _tmp;
            if (Byte.TryParse(Expression.ToString(), out _tmp))
            {
                return _tmp;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>  
        /// 把对象序列化为字节数组  
        /// </summary>  
        public static byte[] ObjectToBytes(object obj)
        {
            if (obj == null)
                return null;
            //内存实例
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);//序列化对象，写入ms流中  
            ms.Position = 0;
            //byte[] bytes = new byte[ms.Length];//这个有错误
            byte[] bytes = ms.GetBuffer();
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        /// <summary>
        /// 把字节数组  反序列化为对象
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        public static string ToStr(object Expression)
        {
            if (Expression == null) return "";
            if (string.IsNullOrEmpty(Expression.ToString()))
            {
                return "";
            }
            return Expression.ToString();
        }

        public static long ToLong(object Expression)
        {
            if (Expression == null) return 0;
            long _tmp;
            if (long.TryParse(Expression.ToString(), out _tmp))
            {
                return _tmp;
            }
            else
            {
                return 0;
            }
        }

        public static decimal ToDecimal(object Expression)
        {
            if (Expression == null) return 0;
            decimal _tmp;
            if (decimal.TryParse(Expression.ToString(), out _tmp))
            {
                return _tmp;
            }
            else
            {
                return 0;
            }
        }

        public static DateTime ToDateTime(object Expression)
        {
            if (Expression == null) return DateTime.Now;
            DateTime _tmp;
            if (DateTime.TryParse(Expression.ToString(), out _tmp))
            {
                return _tmp;
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static string[] ToStringArray(object Expression, string Delimiter)
        {
            return ToStr(Expression).Split(new string[] { Delimiter }, StringSplitOptions.None);
        }

        public static int[] ToIntArray(object Expression, string Delimiter)
        {
            var aryStr = ToStringArray(Expression, Delimiter);
            var resut = aryStr.Where(m => IsNumeric(m)).Select(m => ToInt(m)).ToArray();
            return resut;
        }

        public static long[] ToLongArray(object Expression, string Delimiter)
        {
            var aryStr = ToStringArray(Expression, Delimiter);
            var resut = aryStr.Where(m => IsNumeric(m)).Select(m => ToLong(m)).ToArray();
            return resut;
        }

        public static decimal[] ToDecimalArray(object Expression, string Delimiter)
        {
            var aryStr = ToStringArray(Expression, Delimiter);
            var resut = aryStr.Where(m => IsNumeric(m)).Select(m => ToDecimal(m)).ToArray();
            return resut;
        }

        #endregion

        #region Unicode转换

        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string StringToUnicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string UnicodeToString(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
        }

        #endregion
    }
}
