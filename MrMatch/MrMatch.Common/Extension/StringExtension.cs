using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串截取转int.
        /// 首尾不包含截取的字符:
        /// 如  '1,2,3'  返回 [1,2,3]
        /// 如  '1,2,3,' 返回 [1,2,3],而不是返回[1,2,3,0]
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="splitSymbol">截取的符号</param>
        /// <returns></returns>
        public static List<int> SplitToIntList(this string str, char splitSymbol)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("数据源不能为空");
            }
            if (!char.IsSeparator(splitSymbol))
            {
                throw new NullReferenceException("截取符号必须是char类型");
            }
            var res = new List<int>();
            var splits = str.Split(splitSymbol).ToList();
            for (int i = 0; i < splits.Count; i++)
            {
                if (splits[i].Trim().Length > 0)
                {
                    var transfer = Convert.ToInt32(splits[i]);
                    res.Add(transfer);
                }
            }
            return res;
        }

        /// <summary>
        /// 字符串截取转long,首尾不包含截取的字符
        /// 首尾不包含截取的字符:
        /// 如  '1,2,3'  返回 [1,2,3]
        /// 如  '1,2,3,' 返回 [1,2,3],而不是返回[1,2,3,0]
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="splitSymbol">截取的符号</param>
        /// <returns></returns>
        public static List<long> SplitToLongList(this string str, char splitSymbol)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("数据源不能为空");
            }

            var res = new List<long>();
            var splits = str.Split(splitSymbol).ToList();
            for (int i = 0; i < splits.Count; i++)
            {
                if (splits[i].Trim().Length > 0)
                {
                    var transfer = Convert.ToInt64(splits[i]);
                    res.Add(transfer);
                }
            }
            return res;
        }

        /// <summary>
        /// 此方法专门为求职意向的职能和意向职能作用  
        /// ********其他地方慎用********
        /// 所需结构：
        /// '1-2-3' / '1-2-3,1-2-4'
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="splitSymbol">截取的符号</param>
        /// <returns></returns>
        public static List<long> SplitFunctionsToLongList(this string str, char splitSymbol)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("数据源不能为空");
            }

            var res = new List<long>();
            var splits = str.Split(splitSymbol).ToList();
            for (int i = 0; i < splits.Count; i++)
            {
                if (splits[i].Trim().Length > 0)
                {
                    var count = splits[i].Split('-').ToList().Count;
                    var transfer = Convert.ToInt64(splits[i].Split('-')[count - 1]);
                    res.Add(transfer);
                }
            }
            return res;
        }

        /// <summary>
        /// 判断截取之后是否有重复项
        /// 有重复项返回  true
        /// 没有则返回    false
        /// 判断不包含空项
        /// 如  '1,2,3'  则正常
        /// 如  '1,2,3,' 则会替换成 '1,2,3'
        /// 如  ',1,2,3' 则会替换成 '1,2,3'
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="splitSymbol">截取的符号</param>
        /// <returns></returns>
        public static bool IsSplitRepet(this string str, char splitSymbol)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("数据源不能为空");
            }

            var splits = str.Split(splitSymbol).ToList();
            var newList = new List<string>();
            for (int i = 0; i < splits.Count; i++)
            {
                if (splits[i].Trim().Length > 0)
                {
                    newList.Add(splits[i]);
                }
            }
           
            if (newList.Distinct().Count() == newList.Count())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 截取最后一位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SubStringLast(this string str)
        {
            str = str.Substring(0, str.Length - 1);
            return str;
        }
    }
}
