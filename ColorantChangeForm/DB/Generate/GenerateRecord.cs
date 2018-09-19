using System;
using System.Data;

namespace ColorantChangeForm.DB.Generate
{
    public class GenerateRecord
    {


        Conn conn =new Conn();

        /// <summary>
        /// 分别从两个数据库内获取记录用于计算新的色母量后生成DataTable
        /// </summary>
        /// <param name="colorant"></param>
        /// <param name="akzoColorant"></param>
        /// <returns></returns>
        public DataTable GetRecordToDataTable(string colorant,string akzoColorant)
        {
            var dt=new DataTable();

            try
            {
                
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }

            return dt;
        }


    }
}
