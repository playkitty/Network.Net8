using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOBAgentClient
{
    public partial class OverwriteConfirm : Form
    {
        private int command = 0;
        private bool allSkip = false;
        public OverwriteConfirm(string name, int count)
        {
            var index = name.LastIndexOf("\\");
            var filename = name.Substring(index + 1);
            var remainCount = (count - 1);

            InitializeComponent(remainCount);
            
            this.FileCollisionExplain.Text = filename + " 외 " + remainCount + "개의 파일을 덮어 쓰시겠습니까?";
            if (remainCount == 0)
            {
                this.FileCollisionExplain.Text = filename + " 파일을 덮어 쓰시겠습니까?";
            }
        }

        public int Command { get => this.command; }
        public bool AllSkip { get => this.allSkip; }

        private void Overwrite_Click(object sender, EventArgs e)
        {
            this.command = 1;
            if (this.AllApply.Checked)
                this.command = 0;

            Close();
        }

        private void Skip_Click(object sender, EventArgs e)
        {
            this.command = 2;
            if (this.AllApply.Checked)
                this.allSkip = true;
            
            
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.command = -1;
            Close();
        }
    }
}
