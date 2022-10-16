using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BDOtimers
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();

            this.HScroll = false;
            this.VScroll = false;

            List<string> files =
            (   from a in Directory.GetFiles(
                ".",
                "*.htm",
                SearchOption.TopDirectoryOnly)
                select Path.GetFullPath    (a)
            ).ToList();

          //webBrowser1.Navigate(files[0]);

            richTextBox1.AllowDrop =  true;
            richTextBox1.LoadFile("1.rtf");
        }

        private void webBrowser1_DocumentCompleted
            (object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /// ...
        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {  //this.Close();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {   this.Close();
        }
    }
}
