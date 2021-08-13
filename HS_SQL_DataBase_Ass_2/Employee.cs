using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hs_sql_database_ass_2
{
    class Employee
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }

        public decimal Salary { get; set; }
        public int SuperviserID { get; set; }
        public int BranchID { get; set; }

        public Employee(int EmpID, string FirstName, string LastName, DateTime DateOfBirth, string Sex, decimal Salary, int SuperviserID, int BranchID)
        {
            this.EmpID = EmpID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Sex = Sex;
            this.Salary = Salary;
            this.SuperviserID = SuperviserID;
            this.BranchID = BranchID;


        }
        public Employee FindEmployee(string valueBranch)
        {
            throw new NotImplementedException();
        }


    }
    class Work
    {
        public int EmpID { get; set; }
        public string ClientName { get; set; }

        public decimal TotalSales { get; set; }
    }


}
