using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using ColorantChangeForm.DB;
using ColorantChangeForm.DB.UpLoad;

namespace ColorantChangeForm.UpLoad
{
    public partial class UpLoadColorcode : Form
    {
        Task task=new Task();
        LoadForm load=new LoadForm();

        public UpLoadColorcode()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmOpen.Click += TmOpen_Click;
            tmImport.Click += TmImport_Click;
            tmClose.Click += TmClose_Click;
        }

        /// <summary>
        /// 打开EXCEL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmOpen_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "Xlsx文件|*.xlsx" };
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var fileAddress = openFileDialog.FileName;
                //Filename = Path.GetFileNameWithoutExtension(fileAddress);  //获取地址中文件名

                //将所需的值赋到Task类内
                task.TaskId = 1;
                task.FileAddress = fileAddress;
                task.Tablename = "ColorCodeContrast";

                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (task.ExDataTable.Rows.Count == 0) throw new Exception("不能成功导入,请检查导入模板是否有误.");
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
        private void TmImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw (new Exception("没有记录不能进行导入"));

                //将所需的值赋到Task类内
                task.TaskId = 2;
                task.Tablename = "ColorCodeContrast";
                task.ImporTable = (DataTable)gvdtl.DataSource;
                task.ConnectionType = 0;
                task.Brandid = 0;

                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                var result = task.ImportResult;

                switch (result)
                {
                    case "0":
                        MessageBox.Show("导入成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        throw (new Exception(result));
                }
                //清空原来DataGridView内的内容(无论成功与否都会执行)
                ClearDt((DataTable)gvdtl.DataSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                load.Close();
            }
        }

        private void TmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///  清空DataTable
        /// </summary>
        /// <param name="dt"></param>
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
