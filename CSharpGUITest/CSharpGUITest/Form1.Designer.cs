using System;
using System.Configuration;

namespace NOBAgentClient
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabFolderView = new System.Windows.Forms.TabPage();
            this.PathOepn = new System.Windows.Forms.Button();
            this.progressText = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tabServiceControll = new System.Windows.Forms.TabPage();
            this.ServiceStop = new System.Windows.Forms.Button();
            this.ServiceStart = new System.Windows.Forms.Button();
            this.serviceListView = new System.Windows.Forms.ListView();
            this.tabChatBot = new System.Windows.Forms.TabPage();
            this.TextContent = new System.Windows.Forms.TextBox();
            this.InputText = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.tabFolderView.SuspendLayout();
            this.tabServiceControll.SuspendLayout();
            this.tabChatBot.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(797, 349);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectIndexChanged);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragFileDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragFileEnter);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ImgFolder.png");
            this.imageList1.Images.SetKeyName(1, "ImgExe.png");
            this.imageList1.Images.SetKeyName(2, "ImgText.png");
            this.imageList1.Images.SetKeyName(3, "ImgZip.png");
            this.imageList1.Images.SetKeyName(4, "ImgNOBExe.png");
            this.imageList1.Images.SetKeyName(5, "ImgDefault.png");
            this.imageList1.Images.SetKeyName(6, "ImgDll.png");
            this.imageList1.Images.SetKeyName(7, "ImgBat.png");
            // 
            // TabControl
            // 
            //FolderFeature
            var folderFeature = ConfigurationManager.AppSettings.Get("FolderFeature");
            if (folderFeature.ToLower() == "true")
                this.TabControl.Controls.Add(this.tabFolderView);

            var serviceFeature = ConfigurationManager.AppSettings.Get("ServiceFeature");
            if (serviceFeature.ToLower() == "true")
                this.TabControl.Controls.Add(this.tabServiceControll);

            this.TabControl.Controls.Add(this.tabChatBot);
            this.TabControl.Location = new System.Drawing.Point(4, 5);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(805, 426);
            this.TabControl.TabIndex = 1;
            // 
            // tabFolderView
            // 
            this.tabFolderView.Controls.Add(this.PathOepn);
            this.tabFolderView.Controls.Add(this.progressText);
            this.tabFolderView.Controls.Add(this.progressBar);
            this.tabFolderView.Controls.Add(this.listView1);
            this.tabFolderView.Location = new System.Drawing.Point(4, 22);
            this.tabFolderView.Name = "tabFolderView";
            this.tabFolderView.Padding = new System.Windows.Forms.Padding(3);
            this.tabFolderView.Size = new System.Drawing.Size(797, 400);
            this.tabFolderView.TabIndex = 0;
            this.tabFolderView.Text = "FolderView";
            this.tabFolderView.UseVisualStyleBackColor = true;
            // 
            // PathOepn
            // 
            this.PathOepn.Location = new System.Drawing.Point(752, 351);
            this.PathOepn.Name = "PathOepn";
            this.PathOepn.Size = new System.Drawing.Size(42, 28);
            this.PathOepn.TabIndex = 3;
            this.PathOepn.Text = "Path";
            this.PathOepn.UseVisualStyleBackColor = true;
            this.PathOepn.Click += new System.EventHandler(this.PathOpenClicked);
            
            // 
            // progressText
            // 
            this.progressText.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressText.Location = new System.Drawing.Point(7, 351);
            this.progressText.Name = "progressText";
            this.progressText.ReadOnly = true;
            this.progressText.Size = new System.Drawing.Size(242, 21);
            this.progressText.TabIndex = 2;
            this.progressText.TabStop = false;
            this.progressText.Text = "파일 전송 준비";
            this.progressText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 373);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(714, 21);
            this.progressBar.TabIndex = 1;
            // 
            // tabServiceControll
            // 
            this.tabServiceControll.Controls.Add(this.ServiceStop);
            this.tabServiceControll.Controls.Add(this.ServiceStart);
            this.tabServiceControll.Controls.Add(this.serviceListView);
            this.tabServiceControll.Location = new System.Drawing.Point(4, 22);
            this.tabServiceControll.Name = "tabServiceControll";
            this.tabServiceControll.Padding = new System.Windows.Forms.Padding(3);
            this.tabServiceControll.Size = new System.Drawing.Size(797, 400);
            this.tabServiceControll.TabIndex = 1;
            this.tabServiceControll.Text = "ServiceControll";
            this.tabServiceControll.UseVisualStyleBackColor = true;
            // 
            // ServiceStop
            // 
            this.ServiceStop.Location = new System.Drawing.Point(529, 62);
            this.ServiceStop.Name = "ServiceStop";
            this.ServiceStop.Size = new System.Drawing.Size(66, 32);
            this.ServiceStop.TabIndex = 2;
            this.ServiceStop.Text = "Stop";
            this.ServiceStop.UseVisualStyleBackColor = true;
            this.ServiceStop.Click += new System.EventHandler(this.serviceListView_ClickServiceStop);
            // 
            // ServiceStart
            // 
            this.ServiceStart.Location = new System.Drawing.Point(529, 23);
            this.ServiceStart.Name = "ServiceStart";
            this.ServiceStart.Size = new System.Drawing.Size(66, 32);
            this.ServiceStart.TabIndex = 1;
            this.ServiceStart.Text = "Start";
            this.ServiceStart.UseVisualStyleBackColor = true;
            this.ServiceStart.Click += new System.EventHandler(this.serviceListView_ClickServiceStart);
            // 
            // serviceListView
            // 
            this.serviceListView.HideSelection = false;
            this.serviceListView.Location = new System.Drawing.Point(6, 4);
            this.serviceListView.MultiSelect = false;
            this.serviceListView.Name = "serviceListView";
            this.serviceListView.Size = new System.Drawing.Size(492, 392);
            this.serviceListView.TabIndex = 0;
            this.serviceListView.UseCompatibleStateImageBehavior = false;
            this.serviceListView.View = System.Windows.Forms.View.Details;
            // 
            // tabChatBot
            // 
            this.tabChatBot.Controls.Add(this.BtnSend);
            this.tabChatBot.Controls.Add(this.TextContent);
            this.tabChatBot.Controls.Add(this.InputText);
            this.tabChatBot.Location = new System.Drawing.Point(4, 22);
            this.tabChatBot.Name = "tabChatBot";
            this.tabChatBot.Padding = new System.Windows.Forms.Padding(3);
            this.tabChatBot.Size = new System.Drawing.Size(797, 400);
            this.tabChatBot.TabIndex = 2;
            this.tabChatBot.Text = "ChatBot";
            this.tabChatBot.UseVisualStyleBackColor = true;
            // 
            // TextContent
            // 
            this.TextContent.Location = new System.Drawing.Point(6, 7);
            this.TextContent.Multiline = true;
            this.TextContent.Name = "TextContent";
            this.TextContent.ReadOnly = true;
            this.TextContent.Size = new System.Drawing.Size(405, 357);
            this.TextContent.TabIndex = 1;
            // 
            // InputText
            // 
            this.InputText.Location = new System.Drawing.Point(3, 373);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(409, 21);
            this.InputText.TabIndex = 0;
            this.InputText.KeyPress += this.ChatKeyDown;
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(432, 372);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(77, 21);
            this.BtnSend.TabIndex = 2;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.SendChat);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 443);
            this.Controls.Add(this.TabControl);
            this.Name = "Form1";
            this.Text = "NOBAgentClient";
            this.TabControl.ResumeLayout(false);
            this.tabFolderView.ResumeLayout(false);
            this.tabFolderView.PerformLayout();
            this.tabServiceControll.ResumeLayout(false);
            this.tabChatBot.ResumeLayout(false);
            this.tabChatBot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabFolderView;
        private System.Windows.Forms.TabPage tabServiceControll;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox progressText;
        private System.Windows.Forms.ListView serviceListView;
        private System.Windows.Forms.Button ServiceStop;
        private System.Windows.Forms.Button ServiceStart;
        private System.Windows.Forms.Button PathOepn;
        private System.Windows.Forms.TabPage tabChatBot;
        private System.Windows.Forms.TextBox TextContent;
        private System.Windows.Forms.TextBox InputText;
        private System.Windows.Forms.Button BtnSend;

        public System.Windows.Forms.ProgressBar ProgressBar => this.progressBar;
    }
}

