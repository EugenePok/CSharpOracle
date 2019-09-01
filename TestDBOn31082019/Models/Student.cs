using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Oracle.ManagedDataAccess.Client;

namespace TestDBOn31082019.Models
{
    public class Student
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public byte Course { get; set; }

        public const string Prop_ID = "ID";
        public const string Prop_Name = "Name";
        public const string Prop_Course = "Course";
        public const string TableNameInOracle = "Student";
        public const string ConnectionString = "User Id=system; Password=Database65710; Data Source=" +
                                               "(DESCRIPTION =" +
                                               "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                                               "(CONNECT_DATA = " +
                                               "(SERVER = DEDICATED)" +
                                               "(SERVICE_NAME = orcl)" +
                                               ")" +
                                               "); ";

        public static List<Student> GetStudent(string sql = "select * from " + TableNameInOracle)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                List<Student> students = new List<Student>();
                OracleCommand cmd = new OracleCommand(sql, connection);
                connection.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student newStudent = new Student()
                    {
                        ID = Convert.ToInt32(dr[Prop_ID]),
                        Name = dr[Prop_Name].ToString(),
                        Course = Convert.ToByte(dr[Prop_Course])
                    };
                    students.Add(newStudent);
                }
                dr.Dispose();
                cmd.Dispose();
                return students;
            }
        }

        public static List<Student> GetStudentWhere(string Constraint)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                List<Student> students = new List<Student>();
                string sql = "select * from " + TableNameInOracle + " where " + Constraint;
                OracleCommand cmd = new OracleCommand(sql, connection);
                connection.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student newStudent = new Student()
                    {
                        ID = Convert.ToInt32(dr[Prop_ID]),
                        Name = dr[Prop_Name].ToString(),
                        Course = Convert.ToByte(dr[Prop_Course])
                    };
                    students.Add(newStudent);
                }
                dr.Dispose();
                cmd.Dispose();
                return students;
            }
        }

        public static List<Student> GetStudent(List<string> PropertyToBeSelect, string Constraint = "")
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                List<Student> students = new List<Student>();
                string sql = "select ";
                foreach (string propertyName in PropertyToBeSelect)
                {
                    sql += propertyName + ", ";
                }
                sql = sql.Substring(0, sql.Length - 2);
                sql += " from " + TableNameInOracle + " where " + Constraint;
                OracleCommand cmd = new OracleCommand(sql, connection);
                connection.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student newStudent = new Student()
                    {
                        ID = PropertyToBeSelect.Contains(Prop_ID) ? Convert.ToInt32(dr[Prop_ID]) : (int) 0,
                        Name = PropertyToBeSelect.Contains(Prop_Name) ? dr[Prop_Name].ToString() : string.Empty,
                        Course = PropertyToBeSelect.Contains(Prop_Course) ? Convert.ToByte(dr[Prop_Course]) : (byte) 0
                    };
                    students.Add(newStudent);
                }
                dr.Dispose();
                cmd.Dispose();
                return students;
            }
        }

        public static Student InsertStudent(Student student)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                string sql = "insert into " + TableNameInOracle + " values(:" 
                             + Prop_ID + ",:" 
                             + Prop_Name + ",:" 
                             + Prop_Course +")";
                OracleCommand cmd = new OracleCommand(sql, connection);
                OracleParameter[] parameters = 
                {
                    new OracleParameter(Prop_ID,student.ID),
                    new OracleParameter(Prop_Name,student.Name),
                    new OracleParameter(Prop_Course,student.Course)
                };
                connection.Open();
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return student;
            }
        }

        public static Student UpdateStudent(Student student)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                string sql = "UPDATE " + TableNameInOracle + " SET "
                             + Prop_Name + "=:" + Prop_Name + ", "
                             + Prop_Course + "=:" + Prop_Course
                             + " WHERE " + Prop_ID + "=:" + Prop_ID;
                OracleCommand cmd = new OracleCommand(sql, connection);
                OracleParameter[] parameters =
                {
                    new OracleParameter(Prop_ID,student.ID),
                    new OracleParameter(Prop_Name,student.Name),
                    new OracleParameter(Prop_Course,student.Course)
                };
                connection.Open();
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return student;
            }
        }

        public static bool DeleteStudent(int id)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                string sql = "DELETE FROM " + TableNameInOracle + " WHERE "
                             + Prop_ID + "=:" + Prop_ID;
                OracleCommand cmd = new OracleCommand(sql, connection);
                OracleParameter[] parameters =
                {
                    new OracleParameter(Prop_ID,id),
                };
                connection.Open();
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
        }

        public static void Run()
        {
            using (OracleConnection objConn = new OracleConnection(ConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "Oracle_PkrName.Stored_Proc_Name";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("Emp_id", OracleDbType.Int32).Value = 3; // Input id
                objCmd.Parameters.Add("Emp_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    OracleDataAdapter da = new OracleDataAdapter(objCmd);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                }
                objConn.Close();
            }
        }
    }
}