using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace E_Polling_Web_Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // go button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            getElectionByID();
        }


        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            updateElectionByID();
        }
        // delete button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteElectionByID();
        }
        // add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfElectionExists())
            {
                Response.Write("<script>alert('Election Already Exists, try some other Election ID or Name');</script>");
            }
            else
            {
                addNewElection();
            }
        }



        // user defined functions

        void deleteElectionByID()
        {
            if (checkIfElectionExists())
            {
                try
                {
                    string table_name = TextBox1.Text.Trim().ToString() + "_master_table";

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from election_master_table WHERE election_id='" + TextBox1.Text.Trim() + "';", con);
                    cmd.ExecuteNonQuery();

                    string query = "IF OBJECT_ID('dbo." + table_name + "', 'U') IS NOT NULL ";
                    query += "BEGIN ";
                    query += "DROP TABLE dbo." + table_name + " ";
                    query += "END";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Election Deleted Successfully');</script>");
                    clearForm();
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Election ID');</script>");
            }
        }

        void updateElectionByID()
        {

            if (checkIfElectionExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE election_master_table set election_name=@election_name, election_link=@election_link, results_link=@results_link where election_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@election_name", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@election_link", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@results_link", TextBox4.Text.Trim());

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Election Updated Successfully');</script>");
                    clearForm();
                    GridView1.DataBind();
                   
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Election ID');</script>");
            }
        }


        void getElectionByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from election_master_table WHERE election_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["Election_name"].ToString();
                    TextBox3.Text = dt.Rows[0]["Election_table"].ToString();
                    TextBox6.Text = dt.Rows[0]["Election_link"].ToString();
                    TextBox4.Text = dt.Rows[0]["results_link"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Election ID');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        

        bool checkIfElectionExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from election_master_table where election_id='" + TextBox1.Text.Trim() + "' OR election_name='" + TextBox2.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void addNewElection()
        {
            try
            {
                string table_name = TextBox1.Text.Trim().ToString() + "_master_table";

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO election_master_table(election_id,election_name,election_table,election_link,results_link) values(@election_id,@election_name,@election_table,@election_link,@results_link)", con);

                cmd.Parameters.AddWithValue("@election_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@election_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@election_table", table_name);
                cmd.Parameters.AddWithValue("@election_link", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@results_link", TextBox4.Text.Trim());
                cmd.ExecuteNonQuery();

                string query = "IF OBJECT_ID('dbo." + table_name + "', 'U') IS NULL ";
                query += "BEGIN ";
                query += "CREATE TABLE [dbo].[" + table_name + "](";
                query += "[candidate_id] NVARCHAR(50) NOT NULL,";
                query += "[user_id] NVARCHAR(50) NOT NULL,";
                query += ")";
                query += " END";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                
                Response.Write("<script>alert('Election added successfully.');</script>");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox6.Text = "";
        }
    }
}