using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.ImageHelper
{
    public class VerifyImg
    {
        //静态方法全局只有一个,而且它的方法和属性是用类调用的,不是用对象调用的;
        //其他的用对象调用的方法需要实例化一个对象
        private static VerifyImg instance = null;
        private static readonly object lockRoot = new object();
        private static List<string> ImagePathList = new List<string>();

        //构造私有化,不让外面去new了
        private VerifyImg()
        {

        }


        //静态的属性访问器
        public static VerifyImg Instance
        {
            get
            {
                //判断如果为null,就new一个
                if (instance == null)
                {
                    lock (lockRoot)
                    {
                        if (instance == null)
                        {
                            instance = new VerifyImg();
                        }
                    }
                }
                return instance;
            }
        }



        /// <summary>
        /// 初始化图片列表
        /// </summary>
        /// <param name="dirpic">暂时放空不需要</param>
        public void InitPicList(string dirpic)
        {
            //DirectoryInfo dir = new DirectoryInfo(dirpic);
            //ImagePathList = new List<string>();
            //var files = dir.GetFiles().ToList();
            //files.ForEach(c =>
            //{
            //    ImagePathList.Add(c.Name);
            //});
            ImagePathList = System.Configuration.ConfigurationManager.AppSettings["VerifyUrls"].Split(',').ToList();
        }

        /// <summary>
        /// 获取随机图片
        /// </summary>
        /// <param name="picInitDir">暂时放空不需要</param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public ImageOup GetRandImagePath(string picInitDir, string phoneNumber)
        {
            var offsetX = new Random().Next(60, 190);
            var img = new ImageSign();
            img.PhoneNumber = phoneNumber;
            img.CreatedTime = DateTime.Now;
            img.OffsetX = offsetX;
            var signStr = Newtonsoft.Json.JsonConvert.SerializeObject(img);
            var sign = Encrypt.Encryption.EncryptString(signStr);
            InitPicList(picInitDir);
            int randNum = new Random().Next(0, ImagePathList.Count);
            var imgHost = System.Configuration.ConfigurationManager.AppSettings["CaptchaImageHost"];
            var oup = new ImageOup();
            oup.Sign = sign;
            oup.OffsetX = offsetX;
            oup.InitImage = imgHost + ImagePathList[randNum];

            oup.VOffset = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["voffset"]);
            return oup;
        }


        /// <summary>
        /// 验证图片块移动的最终位置坐标
        /// </summary>
        /// <param name="offsetX"></param>
        /// <returns></returns>
        public bool ValidateCaptcha(int offsetX, string sign)
        {

            var rtn = true;
            var voffset = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["voffset"]);
            var voffsetTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["voffsetTime"]);
            var imgSign = Encrypt.Encryption.DecryptString(sign);
            var imgInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageSign>(imgSign);

            var startTime = imgInfo.CreatedTime;
            var endTime = DateTime.Now;
            var res = endTime.Subtract(startTime).TotalMinutes;
            if (res > voffsetTime)
            {
                rtn = false;
            }

            var vSet = Math.Abs(offsetX - imgInfo.OffsetX);

            if (vSet > voffset)
            {
                rtn = false;
            }


            return rtn;
        }
    }
}
