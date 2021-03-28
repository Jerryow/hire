using MrMatch.Common.AttributeHelper;
using MrMatch.Common.ModelHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.ReflectionHelper
{
    public static class EntityProperties
    {
        /// <summary>
        /// 配合EntityDto特性验证
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ValidateModel EntityValidate<TEntity>(TEntity entity)
        {
            var rtn = new ValidateModel();
            rtn.BoolResult = true;
            rtn.Message = "";
            if (entity == null)
            {
                rtn.BoolResult = false;
                rtn.Message = "实体不能为null";
                return rtn;
            }

            Type type = entity.GetType();
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var validateAttr = properties[i].GetCustomAttributes<ValidateAttribute>(true).FirstOrDefault();
                if (validateAttr != null)
                {
                    //验证String为空
                    if (validateAttr.PropertyType != null
                        && validateAttr.PropertyType.ToLower() == "string")
                    {
                        if (validateAttr.MinLength > 0 || validateAttr.MaxLength > 0)
                        {
                            //验证长度
                            if (properties[i].GetValue(entity).ToString().Length < validateAttr.MinLength
                           || properties[i].GetValue(entity).ToString().Length > validateAttr.MaxLength)
                            {
                                rtn.BoolResult = false;
                                rtn.Message = validateAttr.ErrorMsg;
                                return rtn;
                            }
                            continue;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(properties[i].GetValue(entity).ToString()))
                            {
                                rtn.BoolResult = false;
                                rtn.Message = validateAttr.ErrorMsg;
                                return rtn;
                            }
                            continue;
                        }
                    }

                    //验证int大于零
                    if (validateAttr.PropertyType != null
                        && validateAttr.PropertyType.ToLower() == "int")
                    {
                        if (Convert.ToInt32(properties[i].GetValue(entity)) <= 0)
                        {
                            rtn.BoolResult = false;
                            rtn.Message = validateAttr.ErrorMsg;
                            return rtn;
                        }
                        continue;
                    }

                    //验证long大于零
                    if (validateAttr.PropertyType != null
                        && validateAttr.PropertyType.ToLower() == "long")
                    {
                        if (Convert.ToInt64(properties[i].GetValue(entity)) <= 0)
                        {
                            rtn.BoolResult = false;
                            rtn.Message = validateAttr.ErrorMsg;
                            return rtn;
                        }
                        continue;
                    }
                }
                
            }

            return rtn;
        }
    }
}
