using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR_Business_App
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Hide();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void FirstNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void FirstNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void HealthInsuranceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void HourlyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HourlyRadioButton.Checked)
            {
                SalaryLabel.Visible = false;
                SalaryTextBox.Visible = false;

                HourlyRateLabel.Visible = true;
                HourlyRateTextBox.Visible = true;
                HoursWorkedLabel.Visible = true;
                HoursWorkedTextBox.Visible = true;
            }
        }

        private void SalaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SalaryRadioButton.Checked)
            {
                SalaryLabel.Visible = true;
                SalaryTextBox.Visible = true;

                HourlyRateLabel.Visible = false;
                HourlyRateTextBox.Visible = false;
                HoursWorkedLabel.Visible = false;
                HoursWorkedTextBox.Visible = false;
            }
        }
    }
}
