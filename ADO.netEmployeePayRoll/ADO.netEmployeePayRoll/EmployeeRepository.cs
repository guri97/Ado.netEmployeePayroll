using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//here i am using EmployeePayRollproblem DB
namespace ADO.NetEmployeeProblem
{//all the bussiness logic are write here
    public class EmployeeRepository
    {
        //this is the connection string to connect with the database
        public static string ConncetionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=EmployeePayRollProblem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection Connection = null;
        //it represent connection to sql server database and
        //it cannot be inherited buz it is a sealed class

        //uc1 and uc2 installing and rettiving data from the database table 
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

        //UC2_Adding some data by connecting with the database
        public void AddEmployee(EmployeePayRoll model)
        {
            try
            {
                Connection = new SqlConnection(ConncetionString);
                SqlCommand command = new SqlCommand("dbo.spAddEmployee", Connection);
                command.CommandType = CommandType.StoredProcedure;

                //command.Parameters.AddWithValue("@EmployeeName", model.EmployeeID);
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Department", model.Department);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Phone", model.Phone);
                command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                command.Parameters.AddWithValue("@StartDate", model.StartDate);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                command.Parameters.AddWithValue("@NetPay", model.NetPay);
                command.Parameters.AddWithValue("@IncomeTax", model.IncomTax);
                command.Parameters.AddWithValue("@Deductions", model.Deductions);



                this.Connection.Open();
                var result = command.ExecuteNonQuery();
                this.Connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("employee inserted suceesfully into table");
                }
                else
                {
                    Console.WriteLine("Not interested");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();

            }
        }
        //uc3 and UC4  -Updateing the data with the salary using name
        public void UpdateEmployee(EmployeePayRoll model)
        {
            try
            {

                Connection = new SqlConnection(ConncetionString);
                SqlCommand command = new SqlCommand("dbo.spUpdateEmployee", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                command.Parameters.AddWithValue("@Name", model.Name);
                this.Connection.Open();
                var result = command.ExecuteNonQuery();
                this.Connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("employee updates suceesfully into table");
                }
                else
                {
                    Console.WriteLine("Not interested");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();

            }

        }
        public void DeleteEmployee(EmployeePayRoll model)
        {
            try
            {

                Connection = new SqlConnection(ConncetionString);
                SqlCommand command = new SqlCommand("dbo.spDeleteEmployee", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", model.Name);
                this.Connection.Open();
                var result = command.ExecuteNonQuery();
                this.Connection.Close();
                if (result != 0)
                {
                    Console.WriteLine("employee deleted suceesfully into table");
                }
                else
                {
                    Console.WriteLine("Not interested");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.Close();

            }

        }
        //this method is uesd to insert the values for the two tables buz here this will get failed
        //buz here we  are trying to use TSQL. 
        public void InsertIntoTwoTables(EmployeePayRoll model)
        {
            try
            {
                Connection = new SqlConnection(ConncetionString);
                SqlCommand command = new SqlCommand("spInsertIntoTwoTables", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Direction = ParameterDirection.Output;
                this.Connection.Open();
                var result = command.ExecuteScalar();
                string EmployeeID = command.Parameters["@EmployeeID"].Value.ToString();
                int newid = Convert.ToInt32(EmployeeID);

                string query = $"insert into Salary (EmployeeID,OTSaraly) values({newid},{model.BasicPay})";
                SqlCommand Comd = new SqlCommand(query, Connection);
                int res = command.ExecuteNonQuery();
                if (res != 0)
                {
                    Console.WriteLine("employee inserted suceesfully into table");
                }
                else
                {
                    Console.WriteLine("Not interested");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally

            {
                Connection.Close();
            }

        }
        public void InsertIntoTwoTablesusingTSQL(EmployeePayRoll model)
        {
            SqlTransaction sqlTransaction = null;
            try
            {
                Connection = new SqlConnection(ConncetionString);
                this.Connection.Open();
                sqlTransaction = Connection.BeginTransaction();

                SqlCommand command = new SqlCommand("spInsertIntoTwoTables", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = sqlTransaction;
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@Address", model.Address);
                //we are giving EmpIP than Employee id buz to know rollback is working or not
                //command.Parameters.Add("@EmpID", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Direction = ParameterDirection.Output;
                var result = command.ExecuteScalar();

                int newid = Convert.ToInt32(command.Parameters["@EmployeeID"].Value);

                string query = $"insert into Salary (EmployeeID,OTSaraly) values({newid},{model.BasicPay})";
                SqlCommand Comd = new SqlCommand(query, Connection);
                int res = command.ExecuteNonQuery();
                if (res != 0)
                {
                    Console.WriteLine("employee inserted suceesfully into table");
                }
                else
                {
                    Console.WriteLine("Not interested");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
            }
        }
        //UC_5 Ability to retrive all the employees who have joined the particular range from the date payroll service
        public void RetrivetheEmployeeAccordingToDateRange(EmployeePayRoll model)
        {
            try
            {
                using (Connection = new SqlConnection(ConncetionString))
                {

                    EmployeePayRoll employee = new EmployeePayRoll();
                    String Query = "SELECT * FROM  EmployeeTable where StartDate between CAST('2017-04-01' as date) and GETDATE();";
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
        public int getAggrigateSumSalary(EmployeePayRoll model)
        {
            try
            {
                int sum = 0;

                using (Connection = new SqlConnection(ConncetionString))
                {
                    string query = @"select sum(BasicPay) from  EmployeeTable GROUP BY Gender;";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            sum = (int)(sqlDataReader.GetDouble(0));
                            //Console.WriteLine(cmd);


                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    Connection.Close();
                    Console.WriteLine("The total sum of the basic salary" + sum);
                    return sum;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int getAggrigateAVGSalary(EmployeePayRoll model)
        {
            try
            {
                int avg = 0;

                using (Connection = new SqlConnection(ConncetionString))
                {
                    string query = @"select AVG(BasicPay) from  EmployeeTable GROUP BY Gender;";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            avg = (int)(sqlDataReader.GetDouble(0));



                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    Connection.Close();
                    Console.WriteLine("The total AVG of the basic salary" + avg);
                    return avg;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int getAggrigateMAXSalary(EmployeePayRoll model)
        {
            try
            {
                int max = 0;

                using (Connection = new SqlConnection(ConncetionString))
                {
                    string query = @"select MAX(BasicPay) from  EmployeeTable GROUP BY Gender;";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            max = (int)(sqlDataReader.GetDouble(0));



                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    Connection.Close();
                    Console.WriteLine("The total MAX of the basic salary" + max);
                    return max;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int getAggrigateMINSalary(EmployeePayRoll model)
        {
            try
            {
                int min = 0;

                using (Connection = new SqlConnection(ConncetionString))
                {
                    string query = @"select MIN(BasicPay) from  EmployeeTable GROUP BY Gender;";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            min = (int)(sqlDataReader.GetDouble(0));



                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    Connection.Close();
                    Console.WriteLine("The total MIN of the basic salary" + min);
                    return min;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int getAggrigateCOUNTSalary(EmployeePayRoll model)
        {
            try
            {
                int count = 0;

                using (Connection = new SqlConnection(ConncetionString))
                {
                    string query = @"select COUNT(EmployeeID) from  EmployeeTable GROUP BY Gender;";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            count = sqlDataReader.GetInt32(0);



                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    Connection.Close();
                    Console.WriteLine("The total count of the basic salary" + count);
                    return count;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}