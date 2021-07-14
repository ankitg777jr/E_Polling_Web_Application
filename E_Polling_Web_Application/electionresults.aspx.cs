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
    public partial class WebForm15 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                SqlConnection con;
                SqlCommand cmd;
                SqlDataAdapter da;
                DataTable dt;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    con = new SqlConnection(strcon);

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    
                    cmd = new SqlCommand("SELECT * from e001_master_table WHERE candidate_id='" + e.Row.Cells[0].Text + "';", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    e.Row.Cells[2].Text = dt.Rows.Count.ToString().Trim();
                    
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}