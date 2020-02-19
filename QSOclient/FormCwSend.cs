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
    public partial class FormCwSend : Form
    {
        internal EventHandler<CharEnteredEventArgs> charEntered;
        public FormCwSend()
        {
            InitializeComponent();
        }

        private void TextBoxSend_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSend.TextLength > 0)
            {
                textBoxSend.TextChanged -= TextBoxSend_TextChanged;
                int selStart = textBoxSend.SelectionStart;
                textBoxSend.Text = textBoxSend.Text.ToUpper();
                charEntered?.Invoke(this, new CharEnteredEventArgs { Char = textBoxSend.Text[textBoxSend.TextLength - 1] });
                textBoxSend.TextChanged += TextBoxSend_TextChanged;
                textBoxSend.SelectionStart = selStart;
            }
        }

        private void FormCwSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
    internal class CharEnteredEventArgs : EventArgs
    {
        internal char Char;
    }

}
