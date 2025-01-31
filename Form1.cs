using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;



namespace nohup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listViewProcesses.View = View.Details; // 设置 ListView 为详细视图
            listViewProcesses.Columns.Add("程序名称", 200); // 第一列：程序名称
            listViewProcesses.Columns.Add("PID", 100); // 第二列：PID
            listViewProcesses.Columns.Add("路径", 400); // 第三列：路径

            // 设置 NotifyIcon 的双击事件
            //notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executable Files (*.exe;*.bat)|*.exe;*.bat|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择一个有效的程序！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.CreateNoWindow = true; // 不显示窗口
                process.Start();

                string processName = Path.GetFileNameWithoutExtension(filePath);
                ListViewItem item = new ListViewItem(new[] { processName, process.Id.ToString(), filePath });
                listViewProcesses.Items.Add(item);
                //MessageBox.Show($"程序已启动：{filePath} (PID: {process.Id})", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动程序：{filePath}\n错误信息：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide(); // 隐藏窗口
            notifyIcon.Visible = true; // 显示系统托盘图标
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            notifyIcon.Visible = false; // 确保在关闭时隐藏系统托盘图标
        }





        private void btnTerminate_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一个正在运行的程序！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ListViewItem selectedItem = listViewProcesses.SelectedItems[0];
            int processId;

            // 尝试解析 PID 列的值
            if (int.TryParse(selectedItem.SubItems[1].Text, out processId))
            {
                Process process = Process.GetProcessById(processId);
                if (process != null)
                {
                    process.Kill();
                    process.WaitForExit();
                    listViewProcesses.Items.Remove(selectedItem);
                    MessageBox.Show($"程序已终止：PID {processId}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"无法找到进程：PID {processId}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"无效的 PID 格式：{selectedItem.SubItems[1].Text}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MINIMIZE = 6;

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); // 显示窗口
            this.WindowState = FormWindowState.Normal; // 恢复窗口状态
            notifyIcon.Visible = false; // 隐藏系统托盘图标
        }
    }
}
