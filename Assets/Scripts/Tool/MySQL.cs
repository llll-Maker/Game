using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

public class MySQL
{
    public static MySqlConnection mysqlCon;
    public static void OpenSql()
    {
        try
        {
            string sqlStr = "server=localhost;database=vrdb_3;userid=root;password=123456;Charset=utf8;";
            mysqlCon = new MySqlConnection(sqlStr);
            mysqlCon.Open();
            Debug.Log("数据库连接成功");
        }
        catch(System.Exception e) 
        {
            Debug.Log("数据库连接失败："+e.ToString());
        }
        
    }

    public MySQL()
    {
        OpenSql();
    }

    public static DataSet QuerySet(string sql)
    {
        if (mysqlCon.State == ConnectionState.Open)//查看数据库是否为打开状态
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sql, mysqlCon);
                mySqlDataAdapter.Fill(ds);
            }
            catch (System.Exception e)
            {
                Debug.Log("SQL Error:" + e.ToString());
            }
            return ds;
        }
        return null;
    }

    public static MySqlDataReader QuertSet2(string sql)
    {
        if (mysqlCon.State == ConnectionState.Open)
        {
            MySqlCommand com = new MySqlCommand(sql, mysqlCon);
            MySqlDataReader read = com.ExecuteReader();
            return read;
        }
        return null;
    }

    public void Close()
    {
        //关闭数据库连接
        if (mysqlCon.State == ConnectionState.Open)
        {
            mysqlCon.Close();
            mysqlCon.Dispose();
            mysqlCon = null;
        }
    }

    public MySqlDataReader Select(string tableName)
    {
        string str = "Select * from " + tableName + ";";
        Debug.Log(str);
        return QuertSet2(str);
    }

    public DataSet InsertUser(string name, string password, int sex)
    {
        string str = "Insert into 20213908_衷茜芝_user(name,password,sex) values ('" + name + "','" + password + "'," + sex + ");";
        Debug.Log(str);
        return QuerySet(str);
    }

    public DataSet InsertGamedate(string name)
    {
        string str = "Insert into 20213908_衷茜芝_gamedate(name) values ('" + name + "');";
        Debug.Log(str);
        return QuerySet(str);
    }

    public DataSet Delete(string tableName, string col, string colValue)
    {
        string str = "Delete from " + tableName + " where " + col + "='" + colValue + "';";
        Debug.Log(str);
        return QuerySet(str);
    }

    public DataSet UpdateDate(string tableName, string[] cols, string[] colValues, string OpenCol, string OpenColValue)
    {
        string str = "Update " + tableName + " set ";// + col + "='" + colValue + "'where " + col1 + "='" + colValue1 + "';";
        str += cols[0] + "='" + colValues[0] + "'";
        if (cols.Length > 1)
        {
            for (int i = 1; i < cols.Length; i++)
            {
                str += "," + cols[i] + "='" + colValues[i] + "'";
            }

        }
        str += " where " + OpenCol + "='" + OpenColValue + "';";
        Debug.Log(str);
        return QuerySet(str);
    }
}
