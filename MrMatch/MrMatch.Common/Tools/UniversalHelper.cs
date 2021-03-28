using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Tools
{
    /// <summary>
    /// 猫奇通用帮助类
    /// </summary>
    public static class UniversalHelper
    {
        private static string units = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        /// <summary>
        /// 获取新注册用户的昵称
        /// </summary>
        /// <returns></returns>
        public static string GetNickName()
        {
            Random rd = new Random();
            var list = units.Split(',').ToList();
            var res = "用户";
            for (int i = 0; i < 6; i++)
            {
                var num = rd.Next(0, 63);
                res += list[num];
            }
            return res;
        }
    }
}
