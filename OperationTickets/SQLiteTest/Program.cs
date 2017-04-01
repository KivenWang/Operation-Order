using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SQLiteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection conn = null;
            string strSQLiteDB = Environment.CurrentDirectory;
            strSQLiteDB = strSQLiteDB.Substring(0, strSQLiteDB.LastIndexOf("\\"));
            strSQLiteDB = strSQLiteDB.Substring(0, strSQLiteDB.LastIndexOf("\\"));// 这里获取到了Bin目录  

            try
            {
                string dbPath = @"Data Source=D:\Kiven\code\OperationTickets\OperationTickets\bin\Debug\Database\OperationTickets.db";
                conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置    
                conn.Open();                        //打开数据库，若文件不存在会自动创建    

                //string sql = "CREATE TABLE IF NOT EXISTS Tickets(No varchar(10), Name varchar(20), Task varchar(50), OperationDate varchar(20), User varchar(20), StartTime varchar(20), EndTime varchar(20), CreateTime varchar(20), CreateComment varchar(255));";//建表语句    
                //SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                //cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                //string sql = "CREATE TABLE IF NOT EXISTS OperationSteps(StepNo integer, Action varchar(255), Comment varchar(255), Image varchar(50));";//建表语句
                string sql = @"INSERT INTO Tickets (
                        [No],
                        Name,
                        Task,
                        OperationDate,
                        User,
                        StartTime,
                        EndTime,
                        CreateTime,
                        CreateComment
                    )
                    VALUES (
                        '0004',
                        'Name4',
                        'Task4',
                        'OperationDate4',
                        'User4',
                        'StartTime4',
                        'EndTime4',
                        'CreateTime4',
                        'CreateComment4'
                    )";
                SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表  

                //SQLiteCommand cmdInsert = new SQLiteCommand(conn);
                //cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                //cmdInsert.ExecuteNonQuery();
                //cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                //cmdInsert.ExecuteNonQuery();
                //cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                //cmdInsert.ExecuteNonQuery();

                conn.Close();
                Console.WriteLine("success!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("failure!");
                Console.ReadKey();
            }
        }
        public void SQLite_Test()
        {
            SQLiteConnection conn = null;
            string strSQLiteDB = Environment.CurrentDirectory;
            strSQLiteDB = strSQLiteDB.Substring(0, strSQLiteDB.LastIndexOf("\\"));
            strSQLiteDB = strSQLiteDB.Substring(0, strSQLiteDB.LastIndexOf("\\"));// 这里获取到了Bin目录  

            try
            {
                string dbPath = "Data Source=" + strSQLiteDB + "\\test.db";
                conn = new SQLiteConnection(dbPath);//创建数据库实例，指定文件位置    
                conn.Open();                        //打开数据库，若文件不存在会自动创建    

                string sql = "CREATE TABLE IF NOT EXISTS phone(ID integer, brand varchar(20), Memery varchar(50));";//建表语句    
                SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, conn);
                cmdCreateTable.ExecuteNonQuery();//如果表不存在，创建数据表    

                SQLiteCommand cmdInsert = new SQLiteCommand(conn);
                cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                cmdInsert.ExecuteNonQuery();
                cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                cmdInsert.ExecuteNonQuery();
                cmdInsert.CommandText = "INSERT INTO phone(brand, Memery) VALUES('samsung', '三星')";//插入几条数据    
                cmdInsert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
            }
        } 
    }
}
