using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ColorantChangeForm.DB;

namespace ColorantChangeForm.UpLoad
{
    public partial class UpLoadColorant : Form
    {
        public string Filename = string.Empty;

        LoadForm load=new LoadForm();
        Task task=new Task();

        public UpLoadColorant()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnShowBrandList();
        }

        private void OnRegisterEvents()
        {
            tmClose.Click += TmClose_Click;
            btnOpenExcel.Click += BtnOpenExcel_Click;
            btnImport.Click += BtnImport_Click;
        }


        //public class Info
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //}

        /// <summary>
        /// 显示品牌下拉列表
        /// </summary>
        private void OnShowBrandList()
        {
            var dt = new DataTable();
#region
            //var value = string.Empty;
           // var infolList=new List<Info>();

            //for (int i = 0; i < 7; i++)
            //{
            //    switch (i)
            //    {
            //        case 0:
            //            value = "EC";
            //            break;
            //        case 1:
            //            value = "GD";
            //            break;
            //        case 2:
            //            value = "KYD";
            //            break;
            //        case 3:
            //            value = "MAX";
            //            break;
            //        case 4:
            //            value = "PC";
            //            break;
            //        case 5:
            //            value = "PR";
            //            break;
            //        case 6:
            //            value = "SW";
            //            break;
            //    }

            //    var info=new Info() {Id = i,Name = value };
            //    infolList.Add(info);
            //}
#endregion
            //创建表头
            for (var i = 0; i <2; i++)
            {
                var dc = new DataColumn();
                switch (i)
                {
                    case 0:
                        dc.ColumnName = "Id";
                        break;
                    case 1:
                        dc.ColumnName = "Name";
                        break;
                }
                dt.Columns.Add(dc);
            }

            //创建行内容
            for (var j = 1; j <= 6; j++)
            {
                var dr = dt.NewRow();
 
                switch (j)
                {
                    case 1:
                        dr[0] = "0";
                        dr[1] = "EC";
                        break;
                    case 2:
                        dr[0] = "1";
                        dr[1] = "GD";
                        break;
                    case 3:
                        dr[0] = "2";
                        dr[1] = "KYD";
                        break;
                    case 4:
                        dr[0] = "3";
                        dr[1] = "MAX";
                        break;
                    case 5:
                        dr[0] = "4";
                        dr[1] = "PC";
                        break;
                    case 6:
                        dr[0] = "5";
                        dr[1] = "SW";
                        break;
                        //case 6:
                        //    dr[0] = "5";
                        //    dr[1] = "PR";
                        //    break;
                }
                dt.Rows.Add(dr);
            }

            comBrand.DataSource = dt;
            comBrand.DisplayMember = "Name"; //设置显示值
            comBrand.ValueMember = "Id";    //设置默认值内码
        }

        /// <summary>
        /// 打开EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAddress = openFileDialog.FileName;
                Filename = Path.GetFileNameWithoutExtension(fileAddress);  //获取地址中文件名

                //将所需的值赋到Task类内
                task.TaskId = 1;
                task.FileAddress = fileAddress;
                task.Tablename = "ColorantContrast";

                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (task.ExDataTable.Rows.Count == 0)throw new Exception("不能成功导入,请检查导入模板是否有误.");
                MessageBox.Show("导入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gvdtl.DataSource = task.ExDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.RowCount == 0) throw (new Exception("没有记录不能进行导入"));

                var dv =(DataRowView)comBrand.Items[comBrand.SelectedIndex];
                var name = Convert.ToString(dv["Name"]);
                var id = Convert.ToInt32(dv["Id"]);

                if (name != Filename)
                {
                    ClearDt((DataTable)gvdtl.DataSource);
                    throw (new Exception("所选的品牌要与导入的色母品牌一致"));
                }
                else
                {
                    //将所需的值赋到Task类内
                    task.TaskId = 2;
                    task.Tablename = "ColorantContrast";
                    task.ImporTable = (DataTable) gvdtl.DataSource;
                    task.ConnectionType = 0;
                    task.Brandid = id;

                    //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                    new Thread(Start).Start();
                    load.StartPosition = FormStartPosition.CenterScreen;
                    load.ShowDialog();

                    var result = task.ImportResult;

                    switch (result)
                    {
                        case "0":
                            MessageBox.Show("导入成功!","成功",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            break;
                        default:
                            throw (new Exception(result));
                    }
                }
                //清空原来DataGridView内的内容(无论成功与否都会执行)
                ClearDt((DataTable)gvdtl.DataSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                load.Close();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 清空DataTable
        /// </summary>
        private void ClearDt(DataTable dt)
        {
            try
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                gvdtl.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void Start()
        {
            task.StartTask();

            //当完成后将Form2子窗体关闭
            this.Invoke((ThreadStart)(() => {
                load.Close();
            }));
        }
    }
}
