using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace hs_sql_database_ass_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        // Define Connection Details
        private string dbName = "hs_sql_database_ass_2";
        private string dbUser = "hs_traffic_cop";
        private string dbPassword = "hs_traffic_cop";
        private int dbPort = 3306;
        private string dbServer = "localhost";


        // Connection String and MySQL Connection
        private string dbConnectionString = "";
        private MySqlConnection conn;

       
        List <Employee> employees = new List <Employee>();
      

        public MainWindow()
        {
            InitializeComponent();
            dbConnectionString = $"server={dbServer}; user={dbUser}; database={dbName}; port={dbPort};password={dbPassword}";
            conn = new MySqlConnection(dbConnectionString);
            
            EmployeeListBox.ItemsSource = employees;
            PerformQuery("SELECT* FROM Employee");

            AddValueToSearchEmployeeinbranchComboBox();
            AddValueToSearchBySalaryComboBox();
            
        }

        private void PerformQuery(string sqlToRun, params object[] list)
        {
            try
            {
                conn.Open();
                sqlToRun = "SELECT Employee.FirstName,Employee.LastName,Work.ClientName,Work.TotalSales FROM Employee LEFT JOIN Work ON Employee.EmpID = Work.EmpID";
                MySqlCommand cmd = new MySqlCommand(sqlToRun,conn);
                
                MySqlDataReader rdr = cmd.ExecuteReader();

                
                while (rdr.Read())
                {
                    int EmpID = int.Parse(rdr[0].ToString());
                    string FirstName = rdr[1].ToString();
                    string LastName = rdr[2].ToString();
                    DateTime DateOfBirth = DateTime.Parse(rdr[3].ToString());
                    string Sex = rdr[4].ToString();
                    int Salary = int.Parse(rdr[5].ToString());
                    int SuperviserID = int.Parse(rdr[6].ToString());
                    int BranchID = int.Parse(rdr[7].ToString());
                    employees.Add(new Employee(EmpID, FirstName, LastName, DateOfBirth, Sex, Salary, SuperviserID, BranchID));


                   
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

           
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            int EmpID = int.Parse(txtIDBox.Text);
            string FirstName = txtFirstNameBox.Text;
            string LastName = txtLastNameBox.Text;
            DateTime DateOfBirth = DateTime.Parse(txtDOBText.Text);
            string Sex = txtSalaryBox.Text;
            int Salary = int.Parse(txtSalaryBox.Text);
            int SuperviserID = int.Parse(txtSuperviserIDBox.Text);
            int BranchID = int.Parse(txtBranchIDBox.Text);

            Employee newEmployee = new Employee(EmpID, FirstName, LastName, DateOfBirth, Sex, Salary, SuperviserID, BranchID);


            employees.Add(newEmployee);

            //AddEmployeeData addEmployeeData = new AddEmployeeData();
            //addEmployeeData.ShowDialog();
            PerformQuery("SELECT * FROM Employee");
        }

        private void txtSearchByNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string sqlQuery = "SELECT * FROM Employee WHERE FirstName and LastName LIKE @SearchText";
            string FindThisText = txtSearchByNameBox.Text.ToUpper();
            PerformQuery(sqlQuery, "@SearchText", $"%{FindThisText}");
        }

        public void AddValueToSearchEmployeeinbranchComboBox()
        {

            SearchEmployeeInBranchComboBox.Items.Add(1);
            SearchEmployeeInBranchComboBox.Items.Add(2);
            SearchEmployeeInBranchComboBox.Items.Add(3);
        }

        public void AddValueToSearchBySalaryComboBox()
        {

            SearchBySalaryComboBox.Items.Add("less than 70000");
            SearchBySalaryComboBox.Items.Add("more than 70000");
        }

        


        private void SearchEmployeeInBranchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SalaryValue = SearchBySalaryComboBox.Text.ToString();
            string valueBranch = SearchEmployeeInBranchComboBox.Text.ToString();

            EmployeeListBox.Items.Clear();

            if (SalaryValue != "")
            { 
                string[] splitSalaryValue = SalaryValue.Split(' ');
                int min = int.Parse(splitSalaryValue[2]);

                foreach (Employee employee in employees)
                {
                    if (employee != null)
                    {
                        if (employee.Salary == min);
                        {
                            EmployeeListBox.Items.Add(employee);
                            Console.WriteLine($" employee {employee}");
                        }
                    }
                }
               
            }
            if (valueBranch != "")
            {
                string[] splitBranchIDValue = valueBranch.Split();
                int max = int.Parse(splitBranchIDValue[0]);

                foreach (Employee employee in employees)
                {
                    if (employee != null)
                    {
                        if (employee.BranchID == max) ;
                        {
                            EmployeeListBox.Items.Add(employee);
                            Console.WriteLine($" employee {employee}");
                        }
                    }
                }
            }

        }
    }

  
    }
