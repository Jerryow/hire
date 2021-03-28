using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.MessageHelper
{
    public class AliCloudMessage
    {
        public static AliCloudMessageResponse Send(AliCloudMessageConfig form)
        {
            AliCloudMessageResponse rtn = new AliCloudMessageResponse();
            try
            {
                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", form.LoginName, form.VerifyKey);
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest();
                request.Method = MethodType.POST;
                request.Domain = form.Domain;
                request.Version = "2017-05-25";
                request.Action = "SendSms";
                // request.Protocol = ProtocolType.HTTP;
                request.AddQueryParameters("PhoneNumbers", form.PhoneNumber);
                request.AddQueryParameters("SignName", form.SignName);
                request.AddQueryParameters("TemplateCode", form.TempCode);
                request.AddQueryParameters("TemplateParam", "{'code':'" + form.VerifyCode + "'}");
                CommonResponse response = client.GetCommonResponse(request);
                rtn = Newtonsoft.Json.JsonConvert.DeserializeObject<AliCloudMessageResponse>(response.Data);
            }
            catch (Exception ex)
            {
                rtn.Code = "failed";
                rtn.Message = ex.Message;
            }
            return rtn;
        }
    }
}
