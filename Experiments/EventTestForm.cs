using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experiments
{
    public partial class EventTestForm : Form
    {
        public EventTestForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void visibleButton_Click(object sender, EventArgs e)
        {
            this.checkBox.Visible = !this.checkBox.Visible;
            this.visibleButton.Text = this.checkBox.Visible ? "Hide" : "Show";
            this.eventLabel.Text = "Visible changed.";

            ShowStatus();
        }

        private void enableButton_Click(object sender, EventArgs e)
        {
            this.checkBox.Enabled = !this.checkBox.Enabled;
            this.enableButton.Text = this.checkBox.Enabled ? "Disable" : "Enable";
            this.eventLabel.Text = "Enabled changed.";

            ShowStatus();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            this.checkBox.Checked = !this.checkBox.Checked;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.checkButton.Text = this.checkBox.Checked ? "Uncheck" : "Check";
            this.eventLabel.Text = "Checked changed.";

            ShowStatus();
        }

        private void ShowStatus()
        {
            this.visibleLabel.Text = string.Format("Visible = {0}", this.checkBox.Visible);// ? "" : "");
            this.enabledLabel.Text = string.Format("Enabled = {0}", this.checkBox.Enabled);// ? "" : "");
            this.checkedLabel.Text = string.Format("Checked = {0}", this.checkBox.Checked);// ? "" : "");
        }
    }
}
