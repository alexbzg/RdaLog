using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RdaLog
{
    public partial class FormSettings : Form
    {
        internal List<Tuple<TextBox, TextBox>> HotKeyBindings
        {
            get
            {
                return new List<Tuple<TextBox, TextBox>>()
                {
                    Tuple.Create(textBoxHotKeyF1Title, textBoxHotKeyF1Bindings),
                    Tuple.Create(textBoxHotKeyF2Title, textBoxHotKeyF2Bindings),
                    Tuple.Create(textBoxHotKeyF3Title, textBoxHotKeyF3Bindings),
                    Tuple.Create(textBoxHotKeyF4Title, textBoxHotKeyF4Bindings),
                    Tuple.Create(textBoxHotKeyF5Title, textBoxHotKeyF5Bindings),
                    Tuple.Create(textBoxHotKeyF6Title, textBoxHotKeyF6Bindings),
                    Tuple.Create(textBoxHotKeyF7Title, textBoxHotKeyF7Bindings),
                    Tuple.Create(textBoxHotKeyF8Title, textBoxHotKeyF8Bindings),
                    Tuple.Create(textBoxHotKeyF9Title, textBoxHotKeyF9Bindings)
                };
            }
        }

        internal Dictionary<string, CheckBox> mainFormPanelCheckboxes;
        public FormSettings()
        {
            InitializeComponent();
            mainFormPanelCheckboxes = new Dictionary<string, CheckBox>()
            {
                {"statusFields", checkBoxViewFields },
                {"statFilter", checkBoxViewStatFilter },
                {"cwMacros", checkBoxViewCwMacro },
                {"callsignId", checkBoxViewCallsignId }
            };
        }

    }
}
