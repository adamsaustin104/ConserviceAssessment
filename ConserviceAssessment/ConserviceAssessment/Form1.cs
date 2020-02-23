using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConserviceAssessment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curName = lbEmployees.SelectedItem.ToString();
            Employee selectedEmployee = HR_Database.HR_GetEmployeeInfo(curName);
            UpdateEmployee(selectedEmployee);
        }

        private void UpdateEmployee(Employee employee)
        {
            try
            {
                pbProfilePic.Load(employee.PhotoUrl);
            }
            catch
            {
                pbProfilePic.ImageLocation = "D:\\WebProjects\\ConserviceAssessment\\default_photo.png";
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HR_Database.HR_GetEmployees();
            foreach(string name in HR_Database.EmployeeNames)
            {
                lbEmployees.Items.Add(name);
            }
            
        }
    }
}
