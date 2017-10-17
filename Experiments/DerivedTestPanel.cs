using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Experiments
{
    public partial class DerivedTestPanel : BaseTestPanel
    {
        public DerivedTestPanel()
        {
            InitializeComponent();
        }

        public DerivedTestPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
