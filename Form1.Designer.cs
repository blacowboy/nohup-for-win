namespace nohup
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnSelect = new Button();
            txtFilePath = new TextBox();
            btnStart = new Button();
            listViewProcesses = new ListView();
            btnMinimize = new Button();
            btnTerminate = new Button();
            notifyIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(632, 12);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "选择程序";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(12, 12);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(614, 23);
            txtFilePath.TabIndex = 1;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(713, 12);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "启动程序";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // listViewProcesses
            // 
            listViewProcesses.Location = new Point(12, 41);
            listViewProcesses.Name = "listViewProcesses";
            listViewProcesses.Size = new Size(695, 397);
            listViewProcesses.TabIndex = 3;
            listViewProcesses.UseCompatibleStateImageBehavior = false;
            // 
            // btnMinimize
            // 
            btnMinimize.Location = new Point(713, 386);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(75, 23);
            btnMinimize.TabIndex = 4;
            btnMinimize.Text = "最小化";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnTerminate
            // 
            btnTerminate.Location = new Point(713, 415);
            btnTerminate.Name = "btnTerminate";
            btnTerminate.Size = new Size(75, 23);
            btnTerminate.TabIndex = 5;
            btnTerminate.Text = "终止";
            btnTerminate.UseVisualStyleBackColor = true;
            btnTerminate.Click += btnTerminate_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "notifyIcon1";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnTerminate);
            Controls.Add(btnMinimize);
            Controls.Add(listViewProcesses);
            Controls.Add(btnStart);
            Controls.Add(txtFilePath);
            Controls.Add(btnSelect);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelect;
        private TextBox txtFilePath;
        private Button btnStart;
        private ListView listViewProcesses;
        private Button btnMinimize;
        private Button btnTerminate;
        private NotifyIcon notifyIcon;
    }
}
