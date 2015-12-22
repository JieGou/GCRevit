using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCRevit.Utils {

    public partial class GCLoggerForm : Form {

        public GCLoggerForm(string message) {
            InitializeComponent();
            this.richTextBox.Text = message;
        }
    }
}
