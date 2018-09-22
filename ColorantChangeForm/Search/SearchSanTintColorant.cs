using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using ColorantChangeForm.DB;
using ColorantChangeForm.DB.Search;

namespace ColorantChangeForm.Search
{
    public partial class SearchSanTintColorant : Form
    {
        LoadForm load = new LoadForm();
        Task task = new Task();

        private string _colorantCode;
        private string _akzoColorant;

        /// <summary>
        /// 返回三华色母编号
        /// </summary>
        public string ColorantCode => _colorantCode;

        /// <summary>
        /// 返回对应的Akzo色母编号
        /// </summary>
        public string AkzoColorant => _akzoColorant;

        public SearchSanTintColorant()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnShowBrandList();
        }

        private void OnRegisterEvents()
        {
            tmClose.Click += TmClose_Click;
            btnSearch.Click += BtnSearch_Click;
            tmGetColorant.Click += TmGetColorant_Click;
        }

        /// <summary>
        /// 读取品牌列表
        /// </summary>
        private void OnShowBrandList()
        {
            try
            {
                #region
                //cmbBrand.DataSource = searchDtl.SearchBrandList(1);
                //cmbBrand.DisplayMember = "BrandName"; //设置显示值
                //cmbBrand.ValueMember = "BrandId";     //设置默认值内码
                #endregion

                var dt = new DataTable();

                //创建表头
                for (var i = 0; i < 2; i++)
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
                for (var j = 0; j < 6; j++)
                {
                    var dr = dt.NewRow();

                    switch (j)
                    {
                        case 0:
                            dr[0] = "0";
                            dr[1] = "EC";
                            break;
                        case 1:
                            dr[0] = "1";
                            dr[1] = "GD";
                            break;
                        case 2:
                            dr[0] = "2";
                            dr[1] = "KYD";
                            break;
                        case 3:
                            dr[0] = "3";
                            dr[1] = "MAX";
                            break;
                        case 4:
                            dr[0] = "4";
                            dr[1] = "PC";
                            break;
                        case 5:
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

                cmbBrand.DataSource = dt;
                cmbBrand.DisplayMember = "Name"; //设置显示值
                cmbBrand.ValueMember = "Id";    //设置默认值内码
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var dv = (DataRowView)cmbBrand.Items[cmbBrand.SelectedIndex];
                var brandId = Convert.ToInt32(dv["Id"]);

                //将所需的值赋到Task类内
                task.TaskId = 3;
                task.ConnectionType = 0;
                task.SearchTypeId = 1;
                task.Brandid = brandId;

                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (task.ExDataTable.Rows.Count == 0) throw new Exception("查询时出现异常,请联系管理员");
                gvdtl.DataSource = task.ExDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 获取从GridView所选中的色母编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmGetColorant_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.CurrentRow == null)throw new Exception("请选择指定的色母编号");
                    var rowIndex = gvdtl.CurrentRow.Index;
                _colorantCode = gvdtl.Rows[rowIndex].Cells[0].Value.ToString();
                _akzoColorant = gvdtl.Rows[rowIndex].Cells[1].Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
