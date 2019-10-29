using System.Drawing;
using System.Text.RegularExpressions;

namespace System.Windows.Forms
{
    internal class ToolStripTextBoxCallsign : ToolStripTextBox
    {
        private Color _backColor;
        private string _prevText;
        private bool _validCallsign = false;
        private static readonly Color ErrorBackColor = System.Drawing.ColorTranslator.FromHtml("#FFE1E1");

        public bool validCallsign { get { return _validCallsign; } }
        static bool CallsignChar(char c)
        {
            return char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) || c == '/';
        }

        static Regex CallsignRegex = new Regex(@"^(:?[A-Z\d]+\/)?\d?[A-Z]+\d+[A-Z]+(:?\/[A-Z\d]+)*$", RegexOptions.Compiled);

        public ToolStripTextBoxCallsign() : base()
        {
            _backColor = BackColor;
            BackColorChanged += TextBoxCallsign_BackColorChanged;
        }

        private void TextBoxCallsign_BackColorChanged(object sender, EventArgs e)
        {
            _backColor = BackColor;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            int selStart = SelectionStart;
            int newSelStart = 0;
            string newText = "";
            Text = Text.ToUpper();
            for (int co = 0; co < Text.Length; co++)
                if (CallsignChar(Text[co]))
                {
                    if (co < selStart)
                        newSelStart++;
                    newText += Text[co];
                }
            Text = newText;

            BackColorChanged -= TextBoxCallsign_BackColorChanged;
            _validCallsign = CallsignRegex.IsMatch(Text);
            BackColor = _validCallsign || Text.Equals(string.Empty) ?
                _backColor : ErrorBackColor;
            BackColorChanged += TextBoxCallsign_BackColorChanged;

            SelectionStart = newSelStart;

            if (!(string.IsNullOrEmpty(_prevText) && string.IsNullOrEmpty(Text)) && _prevText != Text)
            {
                _prevText = Text;
                base.OnTextChanged(e);
            }
        }
    }

}