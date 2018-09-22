using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorantChangeForm.DB
{
    public class Conn
    {
        /// <summary>
        /// 与相关的数据库进行连接
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetConnection(int type)
        {
            var ip = string.Empty;
            var dataBase = string.Empty;
            var uid = string.Empty;
            var pwd = string.Empty;

            switch (type)
            {
                case 0:  //AKZO
                    ip = "192.168.1.250";
                    dataBase = "DB20171026091452_Russia";
                    uid = "sa";
                    pwd = "8990489he";
                    break;
                case 1: //三华录入端(国内) 作用:查询&&更新
                    ip = "113.108.95.73";
                    dataBase = "AutoPaint_Integrated";
                    uid = "sa";
                    pwd = "Yatu8888";
                    break;
                case 2: //三华查询端(国内) 作用:更新
                    ip = "113.108.95.73";
                    dataBase = "SantintStandard0630";
                    uid = "sa";
                    pwd = "Yatu8888";
                    break;
                case 3: //海外  作用:更新
                    ip = "47.254.17.161";
                    dataBase = "SantintStandard0630";
                    uid = "sa";
                    pwd = "Yatu8888";
                    break;
            }

            var pubs = ConfigurationManager.ConnectionStrings["Connstring"];  //读取App.Config配置文件中的Connstring节点
            var consplit = pubs.ConnectionString.Split(';');

            var strcon = string.Format(consplit[0],ip) + ";" + string.Format(consplit[1], dataBase) + ";" + 
                         consplit[2] + ";" + string.Format(consplit[3],uid)+ ";" +
                         string.Format(consplit[4],pwd) + ";" + consplit[5] + ";" + consplit[6] + ";" + consplit[7];

            //var conn = new SqlConnection(strcon);
            //return conn;
            return strcon;
        }
    }
}
