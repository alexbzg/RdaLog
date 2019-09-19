using StorableForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RdaLog
{
    public partial class FormMain : StorableForm.StorableForm
    {
        public FormMain() : base()
        {
            InitializeComponent();
        }


    }

    [DataContract]
    public class FormMainConfig: StorableFormConfig
    {

    }
}
