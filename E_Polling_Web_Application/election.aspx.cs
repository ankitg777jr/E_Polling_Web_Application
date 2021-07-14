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
    public partial class WebForm14 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * from e001_master_table WHERE user_id='" + Session["username"] + "';", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('User already voted for this election');window.location='homepage.aspx';", true);
            }
            
            con.Close();
        }
        
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strcon);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO e001_master_table(candidate_id,user_id) values(@candidate_id,@user_id)", con);

            cmd.Parameters.AddWithValue("@candidate_id", GridView1.SelectedRow.Cells[0].Text.Trim().ToString());
            cmd.Parameters.AddWithValue("@user_id", Session["username"].ToString().Trim());
            
            cmd.ExecuteNonQuery();
            con.Close();

            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Your vote is counted');window.location='homepage.aspx';", true);
        }

    }
}