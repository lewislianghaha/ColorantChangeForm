using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ColorantChangeForm.DB.Generate
{
    public class GenerateRecord
    {
        #region 根据三华色母获取对应的内部色号记录

        private string _searInnerCode = @"
                                             SELECT DISTINCT d.ColorCode
                                             FROM dbo.Colorants A
                                             INNER JOIN dbo.ColorFormulaColorants b ON a.ColorantId=b.ColorantsId
                                             INNER JOIN dbo.ColorFormula c ON b.ColorFormulaId=c.ColorFormulaId
                                             INNER JOIN dbo.InnerColor d ON c.InnerColorId=d.InnerColorId
                                             WHERE A.ColorantCode='{0}'--'PC-1516' 'EC-B10'
                                             AND d.RelationId=1
                                             AND not EXISTS (
                                                                SELECT NULL
                                                                FROM dbo.DerivateColor X
                                                                WHERE D.InnerColorId=X.InnerColorId
                                                             )
                                            ORDER BY d.ColorCode
                                          ";

        #endregion

        #region 获取色母对照表以及对应的色母量记录(注:前提AKZO号必须在Color表内使用) 

        private string _searchAkzoCode = @"
                                            SELECT A.ColorCode,F.InnerCode,D.WeightPercentage
                                            FROM dbo.Color A
                                            INNER JOIN dbo.Color_Formula B ON A.ColorId=B.ColorId
                                            INNER JOIN dbo.Formula C ON B.FormulaId=C.FormulaId
                                            INNER JOIN dbo.ColorFormula_Ingredient D ON B.ColorFormulaId=D.ColorFormulaId
                                            INNER JOIN dbo.Ingredient E ON D.IngredientId=E.IngredientId
                                            INNER JOIN dbo.ColorCodeContrast F ON A.ColorCode=F.AkzoCode

                                            WHERE E.StandardDescription='{0}'
                                            AND NOT EXISTS (
                                                                SELECT NULL
                                                                FROM dbo.ColorVariant X
                                                                WHERE A.ColorId=X.ColorId
                                                            )
                                            ORDER BY C.VersionDate DESC
                                          ";
        #endregion

        Conn conn =new Conn();

        /// <summary>
        /// 分别从两个数据库内获取记录用于计算新的色母量后生成DataTable
        /// </summary>
        /// <param name="colorant">三华色母</param>
        /// <param name="akzoColorant">Akzo色母</param>
        /// <param name="value">浓度系数</param>
        /// <returns></returns>
        public DataTable GetRecordToDataTable(string colorant,string akzoColorant,decimal value)
        {
            var dt=new DataTable();
            var resultDt=new DataTable();

            try
            {
                //创建一个DataTable里面包括(Akzo色号,Akzo色母,Akzo色母量,三华色号,三华色母,三华色母量) 6列
                dt =CreateDt();

                //分别获取AKZO以及三华色号记录
                var sanTintDt = GetInnerCode(colorant);
                var akzoDt = GetAkzoCode(akzoColorant);

                //判断色号是否存在,并将存在的记录存放在同一个DT内(重)
                resultDt = CompareDt(sanTintDt,akzoDt,dt,value,colorant,akzoColorant);
            }
            catch (Exception ex)
            {
                resultDt.Rows.Clear();
                resultDt.Columns.Clear();
                throw new Exception(ex.Message);
            }

            return resultDt;
        }

        /// <summary>
        /// 创建指定的表格表头信息(为最后的结果DT作准备)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDt()
        {
            var dt=new DataTable();

            try
            {
                //创建表头
                for (var i = 0; i < 6; i++)
                {
                    var dc=new DataColumn();

                    switch (i)
                    {
                        case 0:
                            dc.ColumnName = "Akzo色号";
                            dc.DataType = Type.GetType("System.String");
                            break;
                        case 1:
                            dc.ColumnName = "Akzo色母";
                            dc.DataType = Type.GetType("System.String");
                            break;
                        case 2:
                            dc.ColumnName = "Akzo色母量"; 
                            dc.DataType = Type.GetType("System.Decimal");
                            break;
                        case 3:
                            dc.ColumnName = "三华色号";
                            dc.DataType = Type.GetType("System.String");
                            break;
                        case 4:
                            dc.ColumnName = "三华色母";
                            dc.DataType = Type.GetType("System.String");
                            break;
                        case 5:
                            dc.ColumnName = "三华色母量";
                            dc.DataType = Type.GetType("System.Decimal");
                            break;
                    }
                    dt.Columns.Add(dc);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 比较-作用:用于判断三华内部色号与AKZO色号对应,若存在的将相关记录插入至新的DT内(重)
        /// </summary>
        /// <param name="sanTintdt">三华DaTable</param>
        /// <param name="akzodt">AkzoDataTable</param>
        /// <param name="newdt">用于最终合拼的DataTable</param>
        /// <param name="num">浓度系数</param>
        /// <param name="colorant">三华色母</param>
        /// <param name="akzoColorant">Akzo色母</param>
        /// <returns></returns>
        private DataTable CompareDt(DataTable sanTintdt,DataTable akzodt,DataTable newdt,decimal num, string colorant, string akzoColorant)
        {
            try
            {
                foreach (DataRow santintrows in sanTintdt.Rows)
                {
                    var row = akzodt.Select("InnerCode='" + santintrows[0] + "'");

                    if (row.Length < 1)
                    {
                        continue;
                    }
                    else
                    {
                        var akzoCode = row[0][0];
                        var innerCode = row[0][1];
                        var akzoNum = Convert.ToDecimal(row[0][2]);

                        var newrow = newdt.NewRow();
                        newrow["Akzo色号"] = akzoCode;
                        newrow["Akzo色母"] = akzoColorant;
                        newrow["Akzo色母量"] = decimal.Round(akzoNum,5);
                        newrow["三华色号"] = innerCode;
                        newrow["三华色母"] = colorant;
                        newrow["三华色母量"] = decimal.Round(akzoNum*num,5);
                        newdt.Rows.Add(newrow);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return newdt;
        }

        /// <summary>
        /// 根据指定的三华色母获取对应的三华色号记录
        /// </summary>
        /// <param name="colorant">三华色母</param>
        /// <returns></returns>
        private DataTable GetInnerCode(string colorant)
        {
            var ds=new DataSet();

            try
            {
                using (var sql=new SqlConnection(conn.GetConnection(1)))
                {
                    var sqlDataAdapter=new SqlDataAdapter(string.Format(_searInnerCode,colorant),sql);
                    sqlDataAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取色号对照表以及AKZO色母量信息
        /// (注:1)AKZO号必须在Color表内存在 2)要确保所定指定AKZO色母在此范围内并找到AKZO色母量) 
        /// </summary>
        /// <param name="akzoColorant">Akzo色母</param>
        /// <returns></returns>
        private DataTable GetAkzoCode(string akzoColorant)
        {
            var ds=new DataSet();

            try
            {
                using (var sql=new SqlConnection(conn.GetConnection(0)))
                {
                    SqlDataAdapter sqlDataAdapter=new SqlDataAdapter(string.Format(_searchAkzoCode,akzoColorant),sql);
                    sqlDataAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }
    }
}
