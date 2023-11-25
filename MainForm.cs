using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR_Business_App
{
    [Serializable]
    public partial class MainForm : Form
    {
        // class level references
        private const string FILENAME = "Employee.dat";

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // open newform
            InputForm frmInput = new InputForm();

            using (frmInput)
            {
                DialogResult result = frmInput.ShowDialog();
                if (result == DialogResult.Cancel)
                    return;

                string fName = frmInput.FirstNameTextBox.Text;
                string lName = frmInput.LastNameTextBox.Text;
                string ssn = frmInput.SSNTextBox.Text;
                string date = frmInput.HireDateTextBox.Text;
                DateTime hireDate = DateTime.Parse(date);
                string healthIns = frmInput.HealthInsuranceTextBox.Text;
                int lifeIns = int.Parse(frmInput.LifeInsuranceTextBox.Text);
                int vacation = int.Parse(frmInput.VacationDaysTextBox.Text);

                Benefits ben = new Benefits(healthIns, lifeIns, vacation);
                Employee emp = null; // empty ref

                if (frmInput.SalaryRadioButton.Checked)
                {
                    double salary = double.Parse(frmInput.SalaryTextBox.Text);
                    emp = new Salary(fName, lName, ssn, hireDate, ben, salary);
                }
                else if (frmInput.HourlyRadioButton.Checked)
                {
                    double hourlyRate = double.Parse(frmInput.HourlyRateTextBox.Text);
                    double hoursWorked = double.Parse(frmInput.HoursWorkedTextBox.Text);
                    emp = new Hourly(fName, lName, ssn, hireDate, ben, hourlyRate, hoursWorked);
                }
                else
                {
                    MessageBox.Show("Error. Please select an employee type.");
                    return;
                }

                EmployeesListBox.Items.Add(emp);

                WriteEmpsToFile();

            }
        }

        private void WriteEmpsToFile()
        {
            // convert listbox to generic list

            List<Employee> empList = new List<Employee>();
            foreach (Employee emp in EmployeesListBox.Items)
            {
                empList.Add(emp);
            }

            FileStream fs = new FileStream(FILENAME, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fs, empList);

            fs.Close();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            int itemNumber = EmployeesListBox.SelectedIndex;

            if (itemNumber > -1)
            {
                EmployeesListBox.Items.RemoveAt(itemNumber);
                WriteEmpsToFile();
            }
            else
            {
                MessageBox.Show("Please select employee to remove.");
            }
        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {

            // check to see if file exists
            if(File.Exists(FILENAME) && new FileInfo(FILENAME).Length > 0)
            {
                // pipe fromfile and translator
                FileStream fs = new FileStream(FILENAME, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                // read list from file
                List<Employee> list = (List<Employee>)formatter.Deserialize(fs);

                fs.Close();

                EmployeesListBox.Items.Clear();

                foreach (Employee emp in list)
                    EmployeesListBox.Items.Add(emp);
            }
        
        }

        private void PrintPaychecksButton_Click(object sender, EventArgs e)
        {
            foreach (Employee emp in EmployeesListBox.Items)
            {
                string line1 = "Pay To: " + emp.FirstName + " " + emp.LastName;
                string line2 = "Amount Of: " + emp.CalculatePay().ToString("C2");

                string output = "Paycheck:\n\n" + line1 + "\n" + line2;

                MessageBox.Show(output);
            }
        }

        private void EmployeesListBox_DoubleClick(object sender, EventArgs e)
        {
            Employee emp = EmployeesListBox.SelectedItem as Employee;

            InputForm frmUpdate = new InputForm();
            frmUpdate.FirstNameTextBox.Text = emp.FirstName;
            frmUpdate.LastNameTextBox.Text = emp.LastName;
            frmUpdate.SSNTextBox.Text = emp.SSN;
            frmUpdate.HireDateTextBox.Text = emp.HireDate.ToShortDateString();
            frmUpdate.HealthInsuranceTextBox.Text = emp.BenefitsEmp.HealthInsurance;
            frmUpdate.LifeInsuranceTextBox.Text = emp.BenefitsEmp.LifeInsurance.ToString();
            frmUpdate.VacationDaysTextBox.Text = emp.BenefitsEmp.Vacation.ToString();

            // hourly or salary
            if (emp is Salary)
            {
                frmUpdate.HourlyRateLabel.Visible = false;
                frmUpdate.HourlyRateTextBox.Visible = false;
                frmUpdate.HoursWorkedLabel.Visible = false;
                frmUpdate.HoursWorkedTextBox.Visible = false;
                frmUpdate.SalaryLabel.Visible = true;
                frmUpdate.SalaryTextBox.Visible = true;

                frmUpdate.SalaryRadioButton.Checked = true;

                Salary sal = (Salary)emp;

                // show salary
                frmUpdate.SalaryTextBox.Text = sal.AnnualSalary.ToString("F2");
            }
            else if(emp is Hourly)
            {
                frmUpdate.HourlyRateLabel.Visible = true;
                frmUpdate.HourlyRateTextBox.Visible = true;
                frmUpdate.HoursWorkedLabel.Visible = true;
                frmUpdate.HoursWorkedTextBox.Visible = true;
                frmUpdate.SalaryLabel.Visible = false;
                frmUpdate.SalaryTextBox.Visible = false;

                frmUpdate.HourlyRadioButton.Checked = true;

                Hourly hrly = (Hourly)emp;

                // show salary
                frmUpdate.HourlyRateTextBox.Text = hrly.HourlyRate.ToString("F2");
                // frmUpdate.HoursWorkedTextBox.Text = hrly.HoursWorked.ToString("F2");
            }
            else
            {
                MessageBox.Show("Error. Invalid employee type found.");
                return;
            }

            DialogResult result = frmUpdate.ShowDialog();

            //cancel = stop

            if ( result == DialogResult.Cancel )
                return;

            int position = EmployeesListBox.SelectedIndex;
            EmployeesListBox.Items.RemoveAt(position);

            // create new emp post update
            Employee newEmp = null;


            string fName = frmUpdate.FirstNameTextBox.Text;
            string lName = frmUpdate.LastNameTextBox.Text;
            string ssn = frmUpdate.SSNTextBox.Text;
            DateTime hireDate = DateTime.Parse(frmUpdate.HireDateTextBox.Text);
            string healthInsurance = frmUpdate.HealthInsuranceTextBox.Text;
            int lifeInsurance= int.Parse(frmUpdate.LifeInsuranceTextBox.Text);
            int vacation = int.Parse(frmUpdate.VacationDaysTextBox.Text);



            Benefits ben = new Benefits(healthInsurance, lifeInsurance, vacation);

            if (frmUpdate.SalaryRadioButton.Checked)
            {
                double salary = double.Parse(frmUpdate.SalaryTextBox.Text);
                newEmp = new Salary(fName, lName, ssn, hireDate, ben, salary);
            }
            else if (frmUpdate.HourlyRadioButton.Checked)
            {
                double hourlyRate = double.Parse(frmUpdate.HourlyRateTextBox.Text);
                double hoursWorked = double.Parse(frmUpdate.HoursWorkedTextBox.Text);
                newEmp = new Hourly(fName, lName, ssn, hireDate, ben, hourlyRate, hoursWorked);
            }
            else
            {
                MessageBox.Show("Error. Invalid employee type.");
                return;
            }

            // add updated employee
            EmployeesListBox.Items.Add(newEmp);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
