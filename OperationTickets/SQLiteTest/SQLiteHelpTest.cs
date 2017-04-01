using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace SQLiteTest
{
    class SQLiteHelpTest
    {
        public void  Execute()
        {
            //CreateTable(); 
            //InsertData(); 
            ShowData();
            Console.ReadLine();
        }
        public static void CreateTable()
        {
            string dbPath = "D:\\Demo.db3";
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteDBHelper.CreateDB("D:\\Demo.db3");
            }
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            string sql = "CREATE TABLE Test3(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name char(3),TypeName varchar(50),addDate datetime,UpdateTime Date,Time time,Comments blob)";
            db.ExecuteNonQuery(sql, null);
        }
        public static void InsertData()
        {
            string sql = "INSERT INTO Test3(Name,TypeName,addDate,UpdateTime,Time,Comments)values(@Name,@TypeName,@addDate,@UpdateTime,@Time,@Comments)";
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                for (int i = 0; i < 100; i++)
                {
                    SQLiteParameter[] parameters = new SQLiteParameter[]{ 
                                                 new SQLiteParameter("@Name",c+i.ToString()), 
                                         new SQLiteParameter("@TypeName",c.ToString()), 
                                         new SQLiteParameter("@addDate",DateTime.Now), 
                                         new SQLiteParameter("@UpdateTime",DateTime.Now.Date), 
                                         new SQLiteParameter("@Time",DateTime.Now.ToShortTimeString()), 
                                         new SQLiteParameter("@Comments","Just a Test"+i) 
                                         };
                    db.ExecuteNonQuery(sql, parameters);
                }
            }
        }
        public static void ShowData()
        {
            //查询从50条起的20条记录 
            string sql = "select * from test3 order by id desc limit 50 offset 20";
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID:{0},TypeName{1}", reader.GetInt64(0), reader.GetString(1));
                }
            }
        }


        //在实际情况中，采用通用类大批量插入数据会有些慢，这是因为在System.Data.SQLite中的操作如果没有指定操作，则会被当做一个事物，如果需要一次性写入大量记录，则建议显式创建一个事物，在这个事务中完成所有的操作比较好，这样的话比每次操作创建一个事物的效率要提升很多。
        //最终利用VS2008提供的功能，可以看到里面的数据如下： 
        //需要说明的是在System.Data.SQLite中数据类型的规定不适很严格，从创建Test3表的SQL语句来看，表中addDate、UpdateTime、Time分别是DateTime、Date、Time类型字段，但实际上我们插入的时候没有按照这个规定，最终显示的结果也是尽量遵循数据库字段的定义。
        //总结
        //System.Data.SQLite确实是一个非常小巧精悍的数据库，作为对SQLite的封装（SQLite可以在Android等类型的手机上利用Java访问），它依然是体较小，同比性能高、内存消耗小、无需安装仅需一个dll就可以运行的优点（如果在Mobile手机上则需要两个文件），唯一的一个缺点是没有比较的GUI（图形用户界面），不过正因为如此它才得以体积小。
        //在实际开发中没有图形用户界面可能有些不便，我们可以使用VS来查看和操作数据，我自己也做了一个小东东，便于管理和维护数据，界面如下： 
        //如果你要开发数据量在10万条以下的应用，我建议你尝试使用一下System.Data.SQLite，它或许是一个不错的选择。
    }
}
