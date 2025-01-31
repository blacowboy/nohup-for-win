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
            listViewProcesses.View = View.Details; // ���� ListView Ϊ��ϸ��ͼ
            listViewProcesses.Columns.Add("��������", 200); // ��һ�У���������
            listViewProcesses.Columns.Add("PID", 100); // �ڶ��У�PID
            listViewProcesses.Columns.Add("·��", 400); // �����У�·��

            // ���� NotifyIcon ��˫���¼�
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
                MessageBox.Show("��ѡ��һ����Ч�ĳ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = false;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.CreateNoWindow = true; // ����ʾ����
                process.Start();

                string processName = Path.GetFileNameWithoutExtension(filePath);
                ListViewItem item = new ListViewItem(new[] { processName, process.Id.ToString(), filePath });
                listViewProcesses.Items.Add(item);
                //MessageBox.Show($"������������{filePath} (PID: {process.Id})", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�޷���������{filePath}\n������Ϣ��{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide(); // ���ش���
            notifyIcon.Visible = true; // ��ʾϵͳ����ͼ��
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            notifyIcon.Visible = false; // ȷ���ڹر�ʱ����ϵͳ����ͼ��
        }





        private void btnTerminate_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��һ���������еĳ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ListViewItem selectedItem = listViewProcesses.SelectedItems[0];
            int processId;

            // ���Խ��� PID �е�ֵ
            if (int.TryParse(selectedItem.SubItems[1].Text, out processId))
            {
                Process process = Process.GetProcessById(processId);
                if (process != null)
                {
                    process.Kill();
                    process.WaitForExit();
                    listViewProcesses.Items.Remove(selectedItem);
                    MessageBox.Show($"��������ֹ��PID {processId}", "�ɹ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"�޷��ҵ����̣�PID {processId}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"��Ч�� PID ��ʽ��{selectedItem.SubItems[1].Text}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MINIMIZE = 6;

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show(); // ��ʾ����
            this.WindowState = FormWindowState.Normal; // �ָ�����״̬
            notifyIcon.Visible = false; // ����ϵͳ����ͼ��
        }
    }
}
