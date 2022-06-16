using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLD
{
    public partial class _Default : Page
    {
        public static List<String> listTableName = new List<string>();
        public static List<String> listColumnName = new List<string>();
        public static List<String> listColumnNameTemp1 = new List<string>();
        public static List<String> listTableNameTemp1 = new List<string>();
        public static List<String> listColumnNameTemp2 = new List<string>();
        public static List<String> listTableNameTemp2 = new List<string>();
        string mess = "SELECT ";
        //string query = "";
 
        TextBox fixSelect = new TextBox();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getTableName();
            }
                
        }

        protected void checkBoxListIndexTable(object sender, EventArgs e)
        {
            CheckBoxListColumn.Items.Clear();
            listTableName.Clear();
            listColumnNameTemp2.Clear();
            listTableNameTemp2.Clear();
            LabelMess.Text = "";
            OrderChecked.Text = "";
            foreach (ListItem item in CheckBoxListTable.Items)
            {
                if (item.Selected)
                {
                    listTableName.Add(item.Text);
                }
            }
            for(int i=0; i < listTableName.Count; i++)
            {
                getColumnName(listTableName[i].ToString());
            }
            
            foreach (ListItem item in CheckBoxListColumn.Items)
            {
                listColumnNameTemp2.Add(item.Text.ToString());
                listTableNameTemp2.Add(item.Value.ToString());
            }
             
        }
        protected void CheckBoxListIndexColumn(object sender, EventArgs e)
        {
            listColumnNameTemp1.Clear();
            listTableNameTemp1.Clear();
            LabelMess.Text = "";
            //List<String> distinct = new List<string>();
            foreach (ListItem item in CheckBoxListColumn.Items)
            {
                if (item.Selected)
                {
                    listColumnNameTemp1.Add(item.Text.ToString());
                    listTableNameTemp1.Add(item.Value.ToString());

                }
                //listColumnNameTemp1 = distinct.Distinct().ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("Tên cột", Type.GetType("System.String"));
                dt.Columns.Add("Tên bảng", Type.GetType("System.String"));
                string[] arrTemp1 = listColumnNameTemp1.ToArray();
                string[] arrTemp2 = listTableNameTemp1.ToArray();
                for (int i = 0; i < arrTemp1.GetLength(0); i++)
                {
                    dt.Rows.Add();
                    dt.Rows[i]["Tên cột"] = arrTemp1[i];
                    dt.Rows[i]["Tên bảng"] = arrTemp2[i];
                }
                

                GridView1.DataSource = dt;
                GridView1.DataBind();   
            }
        }
        
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {

            LabelMess.Text = "";
            string tableName = string.Join(", ", listTableName);
            String columnName = "";
            String dk = "";
            string group_by = "";
            List<String> listColumnOfGroupBy = new List<string>();
            bool usedGrouBy = true;
            int count = 0;
            String where = "";
            int w = 0;

            for (int i = 0; i < listColumnNameTemp2.Count - 1; i++)
            {

                for (int j = i + 1; j < listColumnNameTemp2.Count; j++)
                {
                    if (listColumnNameTemp2[j] == listColumnNameTemp2[i])
                    {
                        w++;
                        count++;
                        if (w > 1)
                        {
                            where += " AND " + listTableNameTemp2[i].ToString() + "." + listColumnNameTemp2[i] + " = " + listTableNameTemp2[j].ToString() + "." + listColumnNameTemp2[j];
                        }
                        else
                        {
                            where += listTableNameTemp2[i].ToString() + "." + listColumnNameTemp2[i] + " = " + listTableNameTemp2[j].ToString() + "." + listColumnNameTemp2[j];
                        }

                    }
                }

            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox strBang = new TextBox();
                TextBox strCot = new TextBox();
                TextBox dieuKien = (TextBox)GridView1.Rows[i].Cells[2].FindControl("TextBoxDieuKien");
                CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBoxOrder");
                cb.Checked = false;
                if (dieuKien.Text.ToString() != "")
                {
                    count++;
                    strBang.Text = GridView1.Rows[i].Cells[5].Text;
                    strCot.Text = GridView1.Rows[i].Cells[4].Text;
                    if (count == 1)
                    {
                        dk += strBang.Text.ToString() + "." + strCot.Text.ToString() + dieuKien.Text.ToString();
                    }
                    else
                    {
                        dk += " AND " + strBang.Text.ToString() + "." + strCot.Text.ToString() + dieuKien.Text.ToString(); ;
                    }
                    
                }

                strBang.Text = GridView1.Rows[i].Cells[5].Text;
                strCot.Text = GridView1.Rows[i].Cells[4].Text;
                
                DropDownList func= (DropDownList)GridView1.Rows[i].Cells[1].FindControl("DropDownListFunction");
                string selectedFunc = func.SelectedItem.Value;
                if(selectedFunc != "")
                {
                    columnName = selectedFunc + "(" + strBang.Text.ToString() + "." + strCot.Text.ToString() + ")";
                    usedGrouBy = false;
                }
                else
                {
                    columnName = strBang.Text.ToString() + "." + strCot.Text.ToString();
                    listColumnOfGroupBy.Add(columnName.ToString());
                }
                TextBox rename = (TextBox)GridView1.Rows[i].Cells[3].FindControl("TextBoxRename");
                rename.Text = rename.Text.ToString().Trim();
                if (rename.Text.ToString() != "")
                {
                    columnName+= " AS " + "' " + rename.Text.ToString()+" ' ";
                }

                if (i < GridView1.Rows.Count - 1)
                {
                    columnName += ", ";
                }
                mess += columnName;

            }
            if (usedGrouBy == false && listColumnOfGroupBy.Count!=0)
            {
                group_by = " GROUP BY "+ string.Join(", ",listColumnOfGroupBy);
            }

            
            where = where.Trim();
            if (!where.Equals("") || count!=0)
            {
                where = " WHERE " + where;
            }
            OrderChecked.Text = mess.Substring(6);
            //mess += selectText.Text;
            mess += " \nFROM " + tableName.ToString() + "\n"+ where + dk + group_by;
            //LabelMess.Text = string.Join(",", listColumnNameTemp2);

            LabelMess.Text = mess;

        }

        protected void CreateReport(object sender, EventArgs e)
        {
            Session["test"]= ((TextBox)Panel1.FindControl("fixSelect")).Text;
            Session["title"] = ((TextBox)Panel2.FindControl("titleTexbox")).Text;
            Session["Producer"] = ((TextBox)Panel2.FindControl("Producer")).Text;
            Response.Redirect("Report.aspx");
            Server.Execute("Report.aspx");
        }

        
        private void getTableName()
        {
            using(SqlConnection qld=new SqlConnection())
            {
                qld.ConnectionString = ConfigurationManager.ConnectionStrings["QuanlydiemConnectionString"].ConnectionString;
                using(SqlCommand cmd=new SqlCommand())
                {
                    string query = "SELECT ROW_NUMBER() OVER (ORDER BY TABLE_NAME) AS VALUE, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = 'Quanlydiem' AND TABLE_NAME NOT LIKE 'sys%' AND TABLE_NAME NOT LIKE 'MS%' ";
                    cmd.CommandText = query;
                    cmd.Connection = qld;
                    qld.Open();
                    using(SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["TABLE_NAME"].ToString();
                            item.Value = sdr["VALUE"].ToString();
                            CheckBoxListTable.Items.Add(item);
                            CheckBoxListTable.AutoPostBack = true;
                        }
                    }
                    qld.Close();
                }
            }
        }

        private void getColumnName(string tableName)
        {
            using (SqlConnection qld = new SqlConnection())
            {
                qld.ConnectionString = ConfigurationManager.ConnectionStrings["QuanlydiemConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME NOT LIKE 'rowguid%' ";
                    cmd.CommandText = query;
                    cmd.Connection = qld;
                    qld.Open();
                    int i = 0;
                    using(SqlDataReader sdr=cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["COLUMN_NAME"].ToString();
                            item.Value = tableName.ToString();
                            CheckBoxListColumn.Items.Add(item);
                            CheckBoxListColumn.RepeatColumns = 5;
                            CheckBoxListColumn.AutoPostBack = true;
                            DataTable dt = new DataTable(tableName);
                            i++;
                        }
                    }
                    qld.Close();
                }
            }
        }


       

        
    }
}