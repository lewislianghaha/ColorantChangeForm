using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ColorantChangeForm.DB.UpLoad
{
    public class Excel
    {
        /// <summary>
        /// 找开EXCEL并将结果集以DATASET型式返回
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable OpenExcel(string fileAddress,string tableName)
        {
            var importExcelDt = new DataTable();
            var dt = new DataTable();

            try
            {
                //使用NPOI技术进行导入EXCEL至DATATABLE
                importExcelDt = OpenExcelToDataTable(fileAddress,tableName);
                //将从EXCEL过来的记录集为空的行清除
                dt = RemoveEmptyRows(importExcelDt);
            }
            catch (Exception ex)
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                throw new Exception(ex.Message);
            }
            return dt;
        }

        ///  <summary>
        /// 读取EXCEL内容到DATATABLE内
        ///  </summary>
        ///  <param name="fileAddress"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable OpenExcelToDataTable(string fileAddress,string tableName)
        {
            IWorkbook wk;
            var dt = new DataTable();
            
            using (var fsRead = File.OpenRead(fileAddress))
            {
                wk = new XSSFWorkbook(fsRead);
                //获取第一个sheet
                var sheet = wk.GetSheetAt(0);
                //获取第一行
                var hearRow = sheet.GetRow(0);
                //创建列标题

                if (tableName== "ColorantContrast")
                {
                    for (int i = hearRow.FirstCellNum; i < hearRow.Cells.Count; i++)
                    {
                        var dataColumn = new DataColumn();

                        switch (i)
                        {
                            case 0:
                                dataColumn.ColumnName = "Akzo色母";
                                dataColumn.DataType = Type.GetType("System.String");
                                break;
                            case 1:
                                dataColumn.ColumnName = "三华色母";
                                dataColumn.DataType = Type.GetType("System.String");
                                break;
                            case 2:
                                dataColumn.ColumnName = "浓度系数";
                                dataColumn.DataType = Type.GetType("System.Decimal");
                                break;
                            case 3:
                                dataColumn.ColumnName = "创建日期";
                                dataColumn.DataType = Type.GetType("System.DateTime");
                                break;
                            case 4:
                                dataColumn.ColumnName = "品牌";
                                dataColumn.DataType = Type.GetType("System.Int32");
                                break;
                        }
                        dt.Columns.Add(dataColumn);
                    }
                }
                else
                {
                    for (int i = hearRow.FirstCellNum; i < hearRow.Cells.Count; i++)
                    {
                        var dataColumn = new DataColumn();

                        switch (i)
                        {
                            case 0:
                                dataColumn.ColumnName = "Akzo色号";
                                dataColumn.DataType = Type.GetType("System.String");
                                break;
                            case 1:
                                dataColumn.ColumnName = "三华色号";
                                dataColumn.DataType = Type.GetType("System.String");
                                break;
                        }
                        dt.Columns.Add(dataColumn);
                    }
                }

                //创建完标题后,开始从第二行起读取对应列的值
                for (var r = 1; r <= sheet.LastRowNum; r++)
                {
                    var result = false;
                    var dr = dt.NewRow();
                    //获取当前行
                    var row = sheet.GetRow(r);
                    //读取每列
                    for (var j = 0; j < row.Cells.Count; j++)
                    {
                        //循环获取行中的单元格
                        var cell = row.GetCell(j);
                        //循环获取行中的单元格的值
                        //dr[j] = j == 4 || j == 5 ? cell.DateCellValue.ToString() : cell.ToString();
                        dr[j] = GetCellValue(cell);
                        //全为空就不取
                        if (dr[j].ToString() != "")
                        {
                            result = true;
                        }
                    }
                    if (result == true)
                    {
                        //把每行增加到DataTable
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// //检查单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank: //空数据类型 这里类型注意一下，不同版本NPOI大小写可能不一样,有的版本是Blank（首字母大写)
                    return string.Empty;
                case CellType.Boolean: //bool类型
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric: //数字类型
                    if (DateUtil.IsCellDateFormatted(cell))//日期类型
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else //其它数字
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown: //无法识别类型
                default: //默认类型                    
                    return cell.ToString();//
                case CellType.String: //string 类型
                    return cell.StringCellValue;
                case CellType.Formula: //带公式类型
                    try
                    {
                        var e = new XSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 将从EXCEL导入的DATATABLE的空白行清空
        /// </summary>
        /// <param name="dt"></param>
        protected DataTable RemoveEmptyRows(DataTable dt)
        {
            var removeList = new List<DataRow>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var isNull = true;
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    //将不为空的行标记为False
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        isNull = false;
                    }
                }
                //将整行都为空白的记录
                if (isNull)
                {
                    removeList.Add(dt.Rows[i]);
                }
            }

            //将整理出来的所有空白行通过循环进行删除
            for (var i = 0; i < removeList.Count; i++)
            {
                dt.Rows.Remove(removeList[i]);
            }
            return dt;
        }

        /// <summary>
        /// 导入EXCEL至数据库(使用SqlBulkCopy类进行批量插入)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="connectiontype"></param>
        /// <param name="brandId"></param>
        public string ImportExcelToDataBase(string tableName,DataTable dt,int connectiontype,int brandId)
        {
            var result = "0";
            var conn = new Conn();

            try
            {
                var sqlcon=conn.GetConnection(connectiontype);

                if(tableName== "ColorantContrast")
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        dt.Rows[i].BeginEdit();
                        dt.Rows[i][3] = DateTime.Now.Date;
                        dt.Rows[i][4] = brandId;
                        dt.Rows[i].EndEdit();
                    }
                }

                // sqlcon.Open(); 若返回一个SqlConnection的话,必须要显式打开 
                //注:1)要插入的DataTable内的字段数据类型必须要与数据库内的一致 2)SqlBulkCopy类只提供将数据写入到数据库内
                using (var sqlBulkCopy=new SqlBulkCopy(sqlcon))
                {
                    sqlBulkCopy.BatchSize = 500;                    //表示以500行为一个批次进行插入
                    sqlBulkCopy.DestinationTableName = tableName;  //数据库中对应的表名
                    sqlBulkCopy.NotifyAfter = dt.Rows.Count;      //赋值DataTable的行数
                    sqlBulkCopy.WriteToServer(dt);               //数据导入数据库
                    sqlBulkCopy.Close();                        //关闭连接 
                }
               // sqlcon.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
