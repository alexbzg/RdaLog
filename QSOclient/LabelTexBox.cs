using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxlog
{
    public partial class LabelTexBox : UserControl
    {
        public string labelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public string editText { 
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
        public LabelTexBox()
        {
            InitializeComponent();
        }

        
    }
}
