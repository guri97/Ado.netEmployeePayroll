using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetEmployeeProblem
{//all the bussiness logic are write here
    public class EmployeeRepository
    {
        //this is the connection string to connect with the database
        public static string ConncetionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=EmployeePayRollProblem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection Connection = null;
        //it represent connection to sql server database and
        //it cannot be inherited buz it is a sealed class
        public void GetAllEmployees()
        {
            try
            {//here using is bolck and it is a keyword used to
             //dispose the data after the execution completed
                using (Connection = new SqlConnection(ConncetionString))
                {
                    EmployeePayRoll employee = new EmployeePayRoll();
                    String Query = "select * from EmployeeTable";
                    //sqlcommand class provide multiple commad methods
                    SqlCommand command = new SqlCommand(Query, Connection);
                    Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    //sqlreader provides a way to readind a stream from sql server it is also not inherited
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {//here if teh data is null we are replacing with the defult values
                         //or it will read the dta from tha specified row
                            employee.EmployeeID = Convert.ToInt32(reader["EmployeeID"] == DBNull.Value ? default : reader["EmployeeID"]);
                            employee.Name = reader["Name"] == DBNull.Value ? default : reader["Name"].ToString();
                            employee.Gender = reader["Gender"] == DBNull.Value ? default : reader["Gender"].ToString();
                            employee.Department = reader["Department"] == DBNull.Value ? default : reader["Department"].ToString();
                            employee.Address = reader["Address"] == DBNull.Value ? default : reader["Address"].ToString();
                            employee.StartDate = reader["StartDate"] == DBNull.Value ? default : reader["StartDate"].ToString();
                            employee.Phone = Convert.ToInt32(reader["Phone"] == DBNull.Value ? default : reader["Phone"]);
                            employee.BasicPay = Convert.ToInt32(reader["BasicPay"] == DBNull.Value ? default : reader["BasicPay"]);
                            employee.TaxablePay = Convert.ToInt32(reader["TaxablePay"] == DBNull.Value ? default : reader["TaxablePay"]);
                            employee.NetPay = Convert.ToInt32(reader["NetPay"] == DBNull.Value ? default : reader["NetPay"]);
                            employee.IncomTax = Convert.ToInt32(reader["IncomTax"] == DBNull.Value ? default : reader["IncomTax"]);
                            employee.Deductions = Convert.ToInt32(reader["Deductions"] == DBNull.Value ? default : reader["Deductions"]);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", employee.Name,
                                employee.EmployeeID, employee.Department,
                                employee.Address, employee.Phone, employee.Gender, employee.BasicPay,
                                employee.Gender, employee.StartDate,
                                employee.TaxablePay, employee.NetPay, employee.IncomTax, employee.Deductions);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}