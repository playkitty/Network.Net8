using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOBAgentClient
{
    public partial class Form1 : Form
    {
        TextBox changeNameTextBox = new TextBox();
        ListViewItem selectedItem = null;
        public Form1()
        {
            InitializeComponent();
            this.serviceListView.Columns.Add("ServiceName");
            this.serviceListView.Columns.Add("Status");
            this.serviceListView.Columns[0].Width = 200;
            this.serviceListView.Columns[1].Width = 100;
            changeNameTextBox.Multiline = true;
            changeNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.changeNameTextBox_KeyDown);
            this.changeNameTextBox.Hide();
        }

        private void listView1_SelectIndexChanged(object sender, EventArgs e)
        {
            //if (this.listView1.SelectedItems.Count == 0)
            //{
            //    return;
            //}

            this.changeNameTextBox_KeyDown(this.changeNameTextBox, new KeyEventArgs(Keys.Escape));
        }

        private void serviceListView_ClickServiceStart(object sender, EventArgs e)
        {
            if (this.serviceListView.SelectedItems.Count <= 0)
                return;

            MainController.Inst.ReqServiceStart(this.serviceListView.SelectedItems[0].Text);
        }

        private void serviceListView_ClickServiceStop(object sender, EventArgs e)
        {
            if (this.serviceListView.SelectedItems.Count <= 0)
                return;

            MainController.Inst.ReqServiceStop(this.serviceListView.SelectedItems[0].Text);
        }

        private void PathOpenClicked(object sender, EventArgs e)
        {
            //GetCurrent
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Download");
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Download");
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;

            var selItem = this.listView1.SelectedItems[0];
            if (selItem == null)
                return;

            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    this.MouseLButton(sender, e, selItem);
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    this.MouseRButton(sender, e, selItem);
                    break;
                default:
                    break;
            }
        }

        private void MouseLButton(object sender, MouseEventArgs e, ListViewItem selItem)
        {
            if (this.changeNameTextBox.Visible == false)
            {
                this.selectedItem = null;
                return;
            }
            //KeyEventArgs
            //this.changeNameTextBox_KeyDown(this.changeNameTextBox, new KeyEventArgs(Keys.Enter));
            this.changeNameTextBox_KeyDown(this.changeNameTextBox, new KeyEventArgs(Keys.Escape));
        }

        private void MouseRButton(object sender, MouseEventArgs e, ListViewItem selItem)
        {
            // TODO ContextMenu(은)는 더 이상 지원되지 않습니다. 대신 ContextMenuStrip을(를) 사용하세요. 자세한 내용은 https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls(을)를 참조하세요.
            var contextMenu = new ContextMenuStrip();

            // TODO MenuItem(은)는 더 이상 지원되지 않습니다. 대신 ToolStripMenuItem을(를) 사용하세요. 자세한 내용은 https://docs.microsoft.com/en-us/dotnet/core/compatibility/winforms#removed-controls(을)를 참조하세요.
            var menuItem = new ToolStripMenuItem();
            menuItem.Text = "내려받기";
            menuItem.Click += (senders, es) =>
            {
                if (selItem.ImageIndex == 0)
                {
                    MessageBox.Show("폴더 다운로드는 지원하지 않습니다.");
                }
                else
                {
                    MainController.Inst.ReqFileReceiveBegin(selItem.Text);
                }
            };

            contextMenu.Items.Add(menuItem);
            contextMenu.Show(this.listView1, e.Location);
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 1)
                return;

            var selItem = this.listView1.SelectedItems[0];
            if (selItem.ImageIndex == 0)
            {
                this.listView1.Items.Clear();
                var path = MainController.Inst.Directory;
                if (selItem.Text == "..")
                {
                    int index = path.LastIndexOf("\\");
                    path = path.Substring(0, index);
                    index = path.LastIndexOf("\\");
                    path = path.Substring(0, index);
                    path += "\\";
                }
                else
                {
                    path += selItem.Text + "\\";
                }

                MainController.Inst.Directory = path;
                MainController.Inst.NetworkController.SendReqFolderInfo(path);
            }
        }

        private void changeNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (this.selectedItem != null)
                        {
                            //error
                            return;
                        }

                        this.selectedItem = this.listView1.SelectedItems[0];
                        MainController.Inst.ReqFileNameChange(this.selectedItem.Text, changeNameTextBox.Text);
                        changeNameTextBox.Hide();
                    }
                    break;
                case Keys.Escape:
                    {
                        this.selectedItem = null;
                        changeNameTextBox.Hide();
                        this.listView1.Focus();
                    }
                    break;
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F2:
                    {
                        var selectedItem = this.listView1.SelectedItems[0];
                        changeNameTextBox.Text = selectedItem.Text;
                        changeNameTextBox.Parent = listView1;

                        var heightMargin = this.listView1.Margin.Top + this.imageList1.ImageSize.Height;// + this.listView1.Margin.Bottom;
                        changeNameTextBox.SetBounds(selectedItem.Bounds.X, selectedItem.Bounds.Y + heightMargin, selectedItem.Bounds.Width, selectedItem.Bounds.Height / 2);
                        changeNameTextBox.Show();
                        changeNameTextBox.Focus();
                    }
                    break;
                case Keys.Delete:
                    {
                        if (this.selectedItem != null)
                        {
                            //error
                            return;
                        }

                        this.selectedItem = this.listView1.SelectedItems[0];
                        // Really?
                        String text = this.selectedItem.Text + " 을 지우시겠습니까?";
                        if (MessageBox.Show(text, "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            MainController.Inst.ReqFileDelete(this.selectedItem.Text);
                        }
                    }
                    break;
            }
        }

        public void ClearFolder()
        {
            this.BeginInvoke(new Action(() =>
            {
                this.listView1.Items.Clear();

                ListViewItem dirItem = new ListViewItem("..");
                dirItem.ImageIndex = 0;
                this.listView1.Items.Add(dirItem);
            }));
        }

        public void DrawFolder(List<string> folders, List<string> files)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (folders != null)
                {
                    foreach (var dir in folders)
                    {
                        ListViewItem dirItem = new ListViewItem(dir);
                        dirItem.ImageIndex = 0;
                        dirItem.SubItems.Add("lengh");
                        dirItem.SubItems.Add("2023-03-10");
                        this.listView1.Items.Add(dirItem);
                    }
                }

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        ListViewItem fileItem = new ListViewItem(file);
                        int iconIndex = 5;

                        if (file.ToLower().CompareTo(".exe") == 0)
                        {
                            iconIndex = 1;
                            if (file.ToLower().CompareTo("nateonbiz.exe") == 0)
                            {
                                iconIndex = 4;
                            }
                        }
                        else if (file.ToLower().CompareTo(".txt") == 0)
                        {
                            iconIndex = 2;
                        }
                        else if (file.ToLower().CompareTo(".zip") == 0)
                        {
                            iconIndex = 3;
                        }
                        else if (file.ToLower().CompareTo(".dll") == 0)
                        {
                            iconIndex = 6;
                        }
                        else if (file.ToLower().CompareTo(".bat") == 0)
                        {
                            iconIndex = 7;
                        }

                        fileItem.ImageIndex = iconIndex;
                        fileItem.SubItems.Add("lengh");
                        fileItem.SubItems.Add("2023-03-10");
                        this.listView1.Items.Add(fileItem);
                    }
                }
            }));
        }

        private void DragFileEnter(object sender, DragEventArgs e)
        {
            //var files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void DragFileDrop(object sender, DragEventArgs e)
        {
            if (MainController.Inst.Filenames.Count > 0)
            {
                MessageBox.Show("파일이 전송중입니다.");
                return;
            }

            var filenames = new List<string>();
            var foldernames = new List<string>();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                foreach (var file in files)
                {
                    var index = file.LastIndexOf("\\");
                    var basePath = file.Remove(index);
                    FileSeperate(basePath, file, foldernames, filenames, index);
                }

                // 
                MainController.Inst.Filenames = filenames;
                MainController.Inst.SendReqDuplicateCheck(foldernames, filenames);
            }
        }

        private void FileSeperate(string basePath, string filename, List<string> folders, List<string> files, int curDirLength)
        {
            var attr = File.GetAttributes(filename);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var subDir = filename.Substring(curDirLength);
                var dirInfo = new DirectoryInfo(filename);
                var fileList = dirInfo.GetFiles().Select(k => k.Name).ToList();
                var dirList = dirInfo.GetDirectories().Select(k => k.Name).ToList();

                folders.Add(subDir.Substring(1));
                folders.AddRange(dirList.Select(e => subDir.Substring(1) + "\\" + e));
                files.AddRange(fileList.Select(e => subDir.Substring(1) + "\\" + e));

                var dic = files.ToDictionary(e => e, k => basePath);
                MainController.Inst.FilePaths = MainController.Inst.FilePaths.Union(dic).ToDictionary(e => e.Key, k => k.Value);

                SubFolderSeperate(basePath, subDir, dirList, folders, files);
            }
            else
            {
                var subDir = filename.Substring(curDirLength);
                var name = subDir.Substring(1);
                files.Add(name);
                MainController.Inst.FilePaths.Add(name, basePath);
            }
        }

        private void SubFolderSeperate(string basePath, string parentfolerName, List<string> dirlist, List<string> folders, List<string> files)
        {
            foreach (var dir in dirlist)
            {
                var subDir = parentfolerName + "\\" + dir;
                var dirInfo = new DirectoryInfo(basePath + subDir);
                var fileList = dirInfo.GetFiles().Select(k => k.Name).ToList();
                var dirs = dirInfo.GetDirectories().Select(k => k.Name).ToList();

                folders.AddRange(dirs.Select(e => subDir.Substring(1) + "\\" + e));
                files.AddRange(fileList.Select(e => subDir.Substring(1) + "\\" + e));

                var dic = files.ToDictionary(e => e, k => basePath);
                MainController.Inst.FilePaths = MainController.Inst.FilePaths.Union(dic).ToDictionary(e => e.Key, k => k.Value);

                SubFolderSeperate(basePath, subDir, dirs, folders, files);
            }
        }

        public void SetProgressVar(int value, String text)
        {
            if (value < 0)
            {
                return;
            }
            this.BeginInvoke(new Action(() =>
            {
                this.progressBar.Value = value;
                this.progressText.Text = text;
            }));
        }

        public void ChangeName(string filename)
        {
            if (this.selectedItem == null)
                return;
            
            this.BeginInvoke(new Action(() =>
            {
                this.selectedItem.Text = filename;
                this.selectedItem = null;
            }));
        }

        public void DeleteName(string filename)
        {
            if (this.selectedItem == null)
                return;

            this.BeginInvoke(new Action(() =>
            {
                if (this.selectedItem.Text == filename)
                {
                    this.listView1.Items.Remove(this.selectedItem);
                    this.selectedItem = null;
                }
            }));
        }

        public void DrawServiceList(List<ResServiceList.ServiceInfo> list)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.serviceListView.Items.Clear();
                foreach (var service in list)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = service.name;
                    item.SubItems.Add(service.status.ToString());
                    this.serviceListView.Items.Add(item);
                }

                //this.serviceListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }));
            
        }

        private void ChatKeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
                return;

            SendChat(sender, e);

            //var tt = e.KeyChar;

            //int a = 0;
        }

        private void SendChat(object sender, EventArgs e)
        {
            String text = this.InputText.Text;

            this.TextContent.AppendText("Q: " + text + "\r\n");
            MainController.Inst.ReqChatBot(text);

            this.InputText.Text = "";

            // test
            //this.TextContent.TextAlign = HorizontalAlignment.Left;
            //AddTextLine("문자열 응답\r\n");
            //this.TextContent.TextAlign = HorizontalAlignment.Right;
            //AddTextLine("문자열 전달\r\n");
            //this.TextContent.TextAlign()
        }

        public void AddTextLine(String text)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.TextContent.AppendText("A: " + text + "\r\n");
            }));
        }
    }
}