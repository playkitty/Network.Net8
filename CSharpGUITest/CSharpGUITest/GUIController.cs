using CSharpGUITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOBAgentClient
{
    public class GUIController
    {
        public Form1 form = new Form1();

        public GUIController()
        {
        }

        public void SetProgressBar(string filename, int basebytes, int sendCount, int fileSize, int fileCount, int maxfileCount)
        {
            var curProgressRate = 0;
            String text = String.Empty;
            if (fileSize == 0)
            {
                text = filename + " 파일 전송 중 " + curProgressRate + "%" + " (" + (fileCount + 1) + "/" + maxfileCount + ")";
                this.form.SetProgressVar(0, text);
                return;
            }

            curProgressRate = (int)((sendCount * basebytes) / (fileSize * 0.01));
            if (curProgressRate > 100)
                curProgressRate = 100;

            text = filename + " 파일 전송 중 " + curProgressRate + "%" + " (" + (fileCount + 1) + "/" + maxfileCount + ")";
            Console.WriteLine("ProgressBar Percent ({0}/100)", curProgressRate);
            this.form.SetProgressVar(curProgressRate, text);   
        }
    }
}
