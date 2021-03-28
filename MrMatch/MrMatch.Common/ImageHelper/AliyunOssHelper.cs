using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.ImageHelper
{
    public class AliyunOssHelper
    {
        private string accessKeyId = string.Empty;
        private string accessKeySecret = string.Empty;
        private string endpoint = string.Empty;
        private string bucketName = string.Empty;

        public AliyunOssHelper(string _accessKeyId, string _accessKeySecret, string _endpoint, string _bucketName)
        {
            this.accessKeyId = _accessKeyId;
            this.accessKeySecret = _accessKeySecret;
            this.endpoint = _endpoint;
            this.bucketName = _bucketName;
        }

        public UploadResult PushImg(byte[] filebyte, string fileName)
        {
            var oup = new UploadResult();
            try
            {
                var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
                MemoryStream stream = new MemoryStream(filebyte, 0, filebyte.Length);
                oup.Success = client.PutObject(bucketName, fileName, stream).HttpStatusCode == System.Net.HttpStatusCode.OK;
                oup.Messgae = "ok";
                return oup;
            }
            catch (Exception ex)
            {
                oup.Success = false;
                oup.Messgae = ex.Message;
                return oup;
            }
        }
    }

    public class UploadResult
    {
        public bool Success { get; set; }
        public string Messgae { get; set; }
    }
}
