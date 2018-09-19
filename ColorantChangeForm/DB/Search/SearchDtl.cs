using System;
using System.Data;
using System.Data.SqlClient;

namespace ColorantChangeForm.DB.Search
{
    public class SearchDtl
    {
        #region 查询色母对照表信息

        private string _searchColorantMatch = @"
                                                    SELECT A.AkzoColorant,A.Colorant,A.Number,A.CredateDate
                                                    FROM dbo.ColorantContrast A
                                                    WHERE A.TypeId={0}
                                               ";

        #endregion

        #region 查询与Akzo色母关联的三华色母记录

        private string _searchColorantIncludAkzo = @"
                                                    SELECT A.Colorant,A.AkzoColorant
                                                    FROM dbo.ColorantContrast A
                                                    WHERE A.TypeId={0}
                                               ";

        #endregion

        #region 查询三华色母记录表信息(暂不需要 2018-09-18)

        private string _searchColorants = @"
                                                  SELECT a.ColorantCode,a.ColorantName,a.ColorantDensity
                                                  FROM dbo.Colorants a
                                                  WHERE a.BrandId={0}
                                           ";

        #endregion

        #region 查询三华品牌列表(暂不需要 2018-09-18)

        private string _searchBrandList = @"
                                                SELECT A.BrandId,A.BrandName
                                                FROM dbo.Brand A
                                                where A.BrandId not in(7,9)
                                           ";

        #endregion

        Conn conn=new Conn();

        /// <summary>
        /// 查找三华品牌列表
        /// </summary>
        /// <param name="connectiontype"></param>
        /// <returns></returns>
        public DataTable SearchBrandList(int connectiontype)
        {
            var ds=new DataSet();

            try
            {
                using (var sql=new SqlConnection(conn.GetConnection(connectiontype)))
                {
                     var sqladAdapter=new SqlDataAdapter(_searchBrandList,sql);
                     sqladAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ds.Tables[0].Rows.Clear();
                ds.Tables[0].Columns.Clear();
                throw new Exception(ex.Message);
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 查询色母明细记录(包括色母对照表及三华色母表使用)
        /// </summary>
        /// <param name="connectiontype">连接类型</param>
        /// <param name="searchTypeId">查询类型,分0:色母对照表查询;1:三华色母查询</param>
        /// <param name="brandId">品牌ID</param>
        /// <returns></returns>
        public DataTable SearchColorantRecord(int connectiontype,int searchTypeId,int brandId)
        {
            var ds=new DataSet();
            var resultdt=new DataTable();
            var script=string.Empty;

            try
            {
                using (var sql=new SqlConnection(conn.GetConnection(connectiontype)))
                {
                    switch (searchTypeId)
                    {
                        case 0:
                            script = _searchColorantMatch;
                            break;
                        case 1:
                            script = _searchColorantIncludAkzo;  // _searchColorants;
                            break;
                    }

                    var sqladAdapter = new SqlDataAdapter(string.Format(script, brandId),sql);
                    sqladAdapter.Fill(ds);
                    
                    resultdt = RedoColoumnName(ds.Tables[0],searchTypeId);
                }
            }
            catch (Exception ex)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return resultdt;
        }

        /// <summary>
        /// 整理从数据库里获取的DataTable内的列名
        /// </summary>
        /// <param name="dtTable"></param>
        /// <param name="searchTypeId"></param>
        /// <returns></returns>
        private DataTable RedoColoumnName(DataTable dtTable,int searchTypeId)
        {
            switch (searchTypeId)
            {
                case 0:
                    for (var i = 0; i < dtTable.Columns.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                dtTable.Columns[i].ColumnName = "Akzo色母";
                                break;
                            case 1:
                                dtTable.Columns[i].ColumnName = "三华色母";
                                break;
                            case 2:
                                dtTable.Columns[i].ColumnName = "浓度系数";
                                break;
                            case 3:
                                dtTable.Columns[i].ColumnName = "创建日期";
                                break;
                        }
                    }
                break;
                case 1:
                    for (var i = 0; i < dtTable.Columns.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                dtTable.Columns[i].ColumnName = "三华色母编号";
                                break;
                            case 1:
                                dtTable.Columns[i].ColumnName = "Akzo色母编号";
                                break;
                        }
                    }
                    #region
                    //for (var i = 0; i < dtTable.Columns.Count; i++)
                    //{
                    //    switch (i)
                    //    {
                    //        case 0:
                    //            dtTable.Columns[i].ColumnName = "色母编码";
                    //            break;
                    //        case 1:
                    //            dtTable.Columns[i].ColumnName = "色母名称";
                    //            break;
                    //        case 2:
                    //            dtTable.Columns[i].ColumnName = "密度";
                    //            break;
                    //    }
                    //}
                    #endregion
                    break;
            }
            return dtTable;
        }
    }
}
