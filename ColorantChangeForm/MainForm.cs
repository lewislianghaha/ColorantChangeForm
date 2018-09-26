using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ColorantChangeForm.DB;
using ColorantChangeForm.Search;
using ColorantChangeForm.UpLoad;

namespace ColorantChangeForm
{
    public partial class MainForm : Form
    {
        LoadForm load=new LoadForm();
        Task task=new Task();

        public MainForm()
        {
            InitializeComponent();
            OnRegisterEvents();
            OnCanShow();
        }

        private void OnRegisterEvents()
        {
            tmUpdateKey.Click += TmUpdateKey_Click;
            tmChina.Click += TmChina_Click;
            tmOutside.Click += TmOutside_Click;
            tmCSearch.Click += TmCSearch_Click;
            tmClose.Click += TmClose_Click;
            tmSearch.Click += Tmsearch_Click;
            btnGenerate.Click += BtnGenerate_Click;
            tmUpLoadColorant.Click += TmUpLoadColorant_Click;
            tmUploadColorCode.Click += TmUploadColorCode_Click;
        }

        /// <summary>
        /// //判断是否显示指定按钮
        /// </summary>
        private void OnCanShow()
        {
            var loadHostIp = GetIp();
            if (loadHostIp == "172.16.4.60") return;
            tmUpLoad.Visible = false;
        }

        /// <summary>
        /// //获取本地IP
        /// </summary>
        /// <returns></returns>
        protected string GetIp() 
        {
            var ipHost = Dns.Resolve(Dns.GetHostName());
            var ipAddr = ipHost.AddressList[0];
            return ipAddr.ToString();
        }

        /// <summary>
        /// 查询-三华色母明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmsearch_Click(object sender, EventArgs e)
        {
            if (txtValue.Text != "") txtValue.Text = "";

            var sanTint = new SearchSanTintColorant();
            sanTint.StartPosition=FormStartPosition.CenterScreen;
            sanTint.ShowDialog();
            txtColorant.Text = sanTint.ColorantCode;
            txtAkzoColorant.Text = sanTint.AkzoColorant;
        }

        /// <summary>
        /// 查询-色母对照表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmCSearch_Click(object sender, EventArgs e)
        {
            var colorantMatch = new SearchColorantMatch();
            colorantMatch.StartPosition = FormStartPosition.CenterScreen;
            colorantMatch.ShowDialog();
        }

        /// <summary>
        /// 计算色母量(重)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtColorant.Text=="" || txtAkzoColorant.Text== "") throw new Exception("请使用三华色母明细查询功能");
                if ( txtValue.Text=="0") throw new Exception("请输入浓度系数大于0的值");

                //将所需的值赋到Task类内
                task.TaskId = 4;
                task.Colorant = txtColorant.Text;
                task.AkzoColorant = txtAkzoColorant.Text;
                task.Value = Convert.ToDecimal(txtValue.Text);

                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                if (task.ExDataTable.Rows.Count == 0)
                    throw new Exception(string.Format($"没有查询记录,请检查此" +
                                                        $"'{0}'色母编码是否有对应的Akzo色母", txtColorant.Text));
                gvdtl.DataSource = task.ExDataTable;
                label4.Text = "查询的记录数为:" + gvdtl.Rows.Count + "行";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtValue.Text = "";
            }
        }

        /// <summary>
        /// //更新至录入端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmUpdateKey_Click(object sender, EventArgs e)
        {
            try
            {
                if(gvdtl.Rows.Count==0)throw new Exception("没有记录,不能进行更新");

                //将所需的值赋到Task类内
                task.TaskId = 5;


                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// //同步至查询端(国内)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmChina_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有记录,不能进行更新");

                //将所需的值赋到Task类内
                task.TaskId = 5;


                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// //同步至查询端(海外)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmOutside_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvdtl.Rows.Count == 0) throw new Exception("没有记录,不能进行更新");

                //将所需的值赋到Task类内
                task.TaskId = 5;


                //使用子线程工作(作用:通过调用子线程进行控制LoadForm窗体的关闭情况)
                new Thread(Start).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导入-色母对照表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmUpLoadColorant_Click(object sender, EventArgs e)
        {
            var upLoadColorant = new UpLoadColorant();
            upLoadColorant.StartPosition = FormStartPosition.CenterScreen;
            upLoadColorant.ShowDialog();
        }

        /// <summary>
        /// 导入-色号对照表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmUploadColorCode_Click(object sender, EventArgs e)
        {
            var upLoadColorCode=new UpLoadColorcode();
            upLoadColorCode.StartPosition = FormStartPosition.CenterScreen;
            upLoadColorCode.ShowDialog();
        }

        /// <summary>
        /// //关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
