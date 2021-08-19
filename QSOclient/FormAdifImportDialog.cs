using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tnxlog
{
    public partial class FormAdifImportDialog : Form
    {
        private List<LabelTextBox> qthFieldAdifContols = new List<LabelTextBox>();
        private LabelTextBox ltbComment;
        public string getQthFieldAdif(int field)
        {
            return qthFieldAdifContols[field].editText;
        }

        public void setQthFieldAdif(int field, string value)
        {
            qthFieldAdifContols[field].editText = value;
        }

        public string getCommentFieldAdif()
        {
            return ltbComment.editText;
        }

        public void setCommentFieldAdif(string value)
        {
            ltbComment.editText = value;
        }


        public void setQthFieldAdifLabel(int field, string value)
        {
            qthFieldAdifContols[field].labelText = TnxlogConfig.QthFieldTitle(field, value);
        }

        public string fileName
        {
            get {
                return textBoxFileName.Text;
            }
            set
            {
                textBoxFileName.Text = value;
            }
        }
        public FormAdifImportDialog()
        {
            InitializeComponent();
            for (int field = 0; field < TnxlogConfig.QthFieldCount; field++)
            {
                LabelTextBox ltb = new LabelTextBox();
                qthFieldAdifContols.Add(ltb);
                groupBoxAdifFields.Controls.Add(ltb);
                ltb.Location = new Point(1, 14 + field * (ltb.Height + 2));
            }
            ltbComment = new LabelTextBox();
            qthFieldAdifContols.Add(ltbComment);
            groupBoxAdifFields.Controls.Add(ltbComment);
            ltbComment.Location = new Point(1, 14 + TnxlogConfig.QthFieldCount * (ltbComment.Height + 2));
            ltbComment.labelText = "Comment";

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = textBoxFileName.Text;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxFileName.Text = openFileDialog.FileName;
        }
    }
}
