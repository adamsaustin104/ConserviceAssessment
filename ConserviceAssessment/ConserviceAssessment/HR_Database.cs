using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConserviceAssessment
{
    public struct Employee
    {
        public string Name;
        public string Address;
        public string Email;
        public string PhoneNumber;
        public string Position;
        public string Department;
        public DateTime StartDate;
        public DateTime EndDate;
        public string EmployementStatus;
        public string WorkShift;
        public string Manager;
        public string PhotoUrl;
        public string FavColor;
    }

    public class HR_Database
    {
        public static List<Employee> Employees = new List<Employee>();
        public static List<string> EmployeeNames = new List<string>();
        public string Name { get; set; }

        public static bool HR_GetEmployees()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=AUSTIN-CMP; Database=HR; User Id=austin.adams; Password=password123;";

            try
            {
                conn.Open();
                SqlCommand com =
                    new SqlCommand("SELECT * FROM Employees Order By EmployeeName asc", conn);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return false;
                    }
                    else
                    {
                        EmployeeNames.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            conn.Close();
            return true;
        }

        public static bool HR_GetEmployeeByName(string employeeName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=AUSTIN-CMP; Database=HR; User Id=austin.adams; Password=password123;";

            try
            {
                conn.Open();
                SqlCommand com =
                    new SqlCommand("SELECT * FROM Employees WHERE EmployeeName like '%" + employeeName + "%'", conn);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return false;
                    }
                    else
                    {
                        EmployeeNames.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            conn.Close();
            return true;
        }

        public static Employee HR_GetEmployeeInfo(string employeeName)
        {
            Employee employee = new Employee();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=AUSTIN-CMP; Database=HR; User Id=austin.adams; Password=password123;";

            try
            {
                conn.Open();
                SqlCommand com =
                    new SqlCommand("SELECT * FROM Employees WHERE EmployeeName = '" + employeeName + "'", conn);

                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return employee;
                    }
                    else
                    {
                        employee.Name = reader[0].ToString();
                        employee.Address = reader[1].ToString();
                        employee.Email = reader[2].ToString();

                        employee.PhoneNumber = string.Format("({0}) {1}-{2}",
                            reader[3].ToString().Substring(0, 3),
                            reader[3].ToString().Substring(3, 3),
                            reader[3].ToString().Substring(6));

                        employee.Position = reader[4].ToString();
                        employee.Department = reader[5].ToString();
                        employee.EmployementStatus = reader[8].ToString();
                        employee.WorkShift = reader[9].ToString();
                        employee.Manager = reader[10].ToString();
                        employee.PhotoUrl = reader[11].ToString();
                        employee.FavColor = reader[12].ToString();
                        if (reader[6] != null)
                            employee.StartDate = Convert.ToDateTime(reader[6]);
                        if (reader[7] != null)
                            employee.EndDate = Convert.ToDateTime(reader[7]);
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.Close();
            return employee;
        }

    }
}
