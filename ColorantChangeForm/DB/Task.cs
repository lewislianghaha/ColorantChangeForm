using System.Data;
using System.Threading;
using ColorantChangeForm.DB.Generate;
using ColorantChangeForm.DB.Search;
using ColorantChangeForm.DB.UpLoad;

namespace ColorantChangeForm.DB
{
    public class Task
    {
        Excel _excel = new Excel();
        SearchDtl search=new SearchDtl();
        GenerateRecord gen=new GenerateRecord();

        private string _fileAddress;
        private string _tablename;
        private int _taskid;
        private DataTable _excelTable;
        private DataTable _imporTable;
        private string _importResult="0";
        private int _brandid;
        private int _connectiontype;
        private int _searchTypeId;
        private string _colorant;
        private string _akzoColorant;
        private decimal _value;

        /// <summary>
        /// 中转ID
        /// </summary>
        public int TaskId { set { _taskid = value; } }

        /// <summary>
        /// //接收文件地址信息
        /// </summary>
        public string FileAddress { set { _fileAddress = value; } }

        /// <summary>
        /// 接收指定的表名(Excel上传时使用)
        /// </summary>
        public string Tablename { set { _tablename = value; } }

        /// <summary>
        /// 接收GridView内的DataTable
        /// </summary>
        public DataTable ImporTable {set { _imporTable = value; }}

        /// <summary>
        /// 接收品牌ID
        /// </summary>
        public int Brandid { set { _brandid = value; }}

        /// <summary>
        /// 接收连接帐套信息(0:akzo 1:三华国内 2:三华海外)
        /// </summary>
        public int ConnectionType {set { _connectiontype = value; }}

        /// <summary>
        /// 接收查询类型ID(0:色母对照表查询;1:三华色母查询)
        /// </summary>
        public int SearchTypeId {set { _searchTypeId = value; }}

        /// <summary>
        /// 接收三华色母(运算功能使用)
        /// </summary>
        public string Colorant { set { _colorant = value; } }

        /// <summary>
        /// 接收AKZO色母(运算功能使用)
        /// </summary>
        public string AkzoColorant { set { _akzoColorant = value; } }

        /// <summary>
        /// 接收浓度系数(运算功能使用)
        /// </summary>
        public decimal Value { set { _value = value; } }



        /// <summary>
        ///返回DataTable至主窗体
        /// </summary>
        public DataTable ExDataTable => _excelTable;

        /// <summary>
        /// 返回导入(更新)数据库结果(作用:导入及更新数据库后使用)
        /// </summary>
        public string ImportResult => _importResult;

        


        /// <summary>
        /// 监控功能执行情况
        /// </summary>
        public void StartTask()
        {
            Thread.Sleep(1000);

            switch (_taskid)
            {
                //打开EXCEL功能
                case 1:
                    OpenExcel(_fileAddress, _tablename);
                    break;
                //Excel导入数据库功能
                case 2:
                    ImportDataTable(_tablename,_imporTable,_connectiontype,_brandid);
                    break;
                //查询色母记录(包括三华色母及色母对照表)
                case 3:
                    SearchColorantRecord(_connectiontype, _searchTypeId, _brandid);
                    break;
                //计算色母量
                case 4:
                    GetRecordToDataTable(_colorant,_akzoColorant,_value);
                    break;
                //更新(包括录入端;国内查询端及海外查询端)
                case 5:

                    break;
            }
        }

        /// <summary>
        /// 打开EXCEL
        /// </summary>
        /// <param name="fileAddress"></param>
        /// <param name="tableName"></param>
        private void OpenExcel(string fileAddress, string tableName)
        {
             _excelTable = _excel.OpenExcel(fileAddress, tableName);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dt"></param>
        /// <param name="connectiontype"></param>
        /// <param name="brandid"></param>
        private void ImportDataTable(string tableName,DataTable dt,int connectiontype,int brandid)
        {
             _importResult=_excel.ImportExcelToDataBase(tableName, dt, connectiontype, brandid);
        }

        /// <summary>
        /// 查询色母记录(包括色母对照表及三华色母表)
        /// </summary>
        /// <param name="connectiontype"></param>
        /// <param name="searchTypeId"></param>
        /// <param name="brandId"></param>
        private void SearchColorantRecord(int connectiontype, int searchTypeId, int brandId)
        {
            _excelTable = search.SearchColorantRecord(connectiontype,searchTypeId,brandId);
        }

        /// <summary>
        /// 计算色母量
        /// </summary>
        /// <param name="colorant"></param>
        /// <param name="akzoColorant"></param>
        /// <param name="value"></param>
        private void GetRecordToDataTable(string colorant, string akzoColorant, decimal value)
        {
            _excelTable= gen.GetRecordToDataTable(colorant, akzoColorant, value);
        }


    }
}
