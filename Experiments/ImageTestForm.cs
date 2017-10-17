using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experiments
{
    /// <summary>
    /// 各種動作確認用フォーム
    /// </summary>
    public partial class ImageTestForm : Form
    {
        private int[] units;
        private int unitID;

        private int unit { get { return this.units[this.unitID]; } }

        public ImageTestForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            this.units = new int[] { 1,  5, 10 };
            this.unitID = this.units.Length / 2;

            ShowSize();
            ShowUnit();
        }

        private void ShowSize()
        {
            this.picSizeLabel.Text = string.Format("({0}, {1})", this.testPictureBox.Width, this.testPictureBox.Height);
            this.panelSizeLabel.Text = string.Format("({0}, {1})", this.testPanel.Width, this.testPanel.Height);
            this.panelCSizeLabel.Text = string.Format("({0}, {1})", this.testPanel.ClientSize.Width, this.testPanel.ClientSize.Height);
            this.dispRectSizeLabel.Text = string.Format("({0}, {1})", this.testPanel.DisplayRectangle.Width, this.testPanel.DisplayRectangle.Height);
        }

        private void ShowUnit()
        {
            this.unitLabel.Text = string.Format("Unit: {0}", this.unit);
        }

        /// <summary>
        /// Helpボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_Click(object sender, EventArgs e)
        {
            string helpPath = this.helpPathTextBox.Text;
            if (!File.Exists(helpPath))
            {
                return;
            }

            Help.ShowHelp(this, helpPath);
        }

        private void widthMinusButton_Click(object sender, EventArgs e)
        {
            this.testPictureBox.Width = Math.Max(this.testPictureBox.Width - this.unit, 0);
            ShowSize();
        }

        private void widthPlusButton_Click(object sender, EventArgs e)
        {
            this.testPictureBox.Width = Math.Min(this.testPictureBox.Width + this.unit, this.testPanel.Width + 30);
            ShowSize();
        }

        private void heightMinusButton_Click(object sender, EventArgs e)
        {
            this.testPictureBox.Height = Math.Max(this.testPictureBox.Height - this.unit, 0);
            ShowSize();
        }

        private void heightPlusButton_Click(object sender, EventArgs e)
        {
            this.testPictureBox.Height = Math.Min(this.testPictureBox.Height + this.unit, this.testPanel.Height + 30);
            ShowSize();
        }

        private void changeUnitButton_Click(object sender, EventArgs e)
        {
            this.unitID = (this.unitID + 1) % this.units.Length;
            ShowUnit();
        }

        private void TestForm_MouseMove(object sender, MouseEventArgs e)
        {
            mousePostionLabel.Text = string.Format("X: {0}, Y: {1}", e.X, e.Y);
        }
    }
}
