using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experiments
{
    public partial class BaseTestPanel : Panel
    {
        public BaseTestPanel()
        {
            InitializeComponent();
        }

        public BaseTestPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}