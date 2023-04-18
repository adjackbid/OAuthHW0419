using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OAuthHW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuthHW.Common
{
    //裡面放的都是假DB用的簡單方法
    public static partial class CommFuns
    {
        /// <summary>
        /// 取得所有DB資料(全部USER)
        /// </summary>
        /// <returns></returns>
        public static FakeDB GetUsersFromDB()
        {
            string sDB = "";
            using (StreamReader sr = new StreamReader(".\\FakeDB\\FakeDatabase.json"))
            {
                sDB = sr.ReadToEnd();
            }
            //Trans Into class
            return JsonConvert.DeserializeObject<FakeDB>(sDB);
        }

        /// <summary>
        /// 取得單一USER BY SUB
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfoFromDB(string sub)
        {
            string sDB = "";
            using(StreamReader sr = new StreamReader(".\\FakeDB\\FakeDatabase.json"))
            {
                sDB = sr.ReadToEnd();
            }
            //Trans Into class
            return JsonConvert.DeserializeObject<FakeDB>(sDB)?.users.FirstOrDefault(x=>x.sub == sub);
        }

        /// <summary>
        /// 新增或刪除使用(方便用)
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateOrInsertUserInfo(UserInfo user)
        {
            string sDB = "";
            using (StreamReader sr = new StreamReader(".\\FakeDB\\FakeDatabase.json"))
            {
                sDB = sr.ReadToEnd();
            }

            var Users = JsonConvert.DeserializeObject<FakeDB>(sDB);
            var listUser = Users.users.ToList();

            if (Users.users.FirstOrDefault(x => x.sub == user.sub) == null)
            {
                //insert
                listUser.Add(user);
            }
            else
            {
                //update
                var delete_user = listUser.FirstOrDefault(x => x.sub == user.sub);
                if (delete_user != null)
                {
                    listUser.Remove(delete_user);
                    listUser.Add(user);
                }
            }

            Users.users = listUser.ToArray();
            string sJson = JsonConvert.SerializeObject(Users);
            using (StreamWriter sw = new StreamWriter(".\\FakeDB\\FakeDatabase.json"))
            {
                sw.Write(sJson);
            }
        }
    }
}
