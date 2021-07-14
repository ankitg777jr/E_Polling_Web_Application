using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Polling_Web_Application
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // Go button
        protected void Button1_Click(object sender, EventArgs e)
        {
            getUserByID();
        }
        // Active button
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateUserStatusByID("active");
        }
        // pending button
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateUserStatusByID("pending");
        }
        // deactive button
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateUserStatusByID("deactive");
        }
        // delete button
        protected void Button2_Click(object sender, EventArgs e)
        {
            deleteMemberByID();
        }





        // user defined function

        bool checkIfUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from user_master_table where user_id='" + TextBox1.Text.Trim() + "';", con);
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

        void deleteMemberByID()
        {
            if (checkIfUserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE from user_master_table WHERE user_id='" + TextBox1.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('User Deleted Successfully');window.location='adminusermanagement.aspx';", true);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid User ID');</script>");
            }
        }

        void getUserByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from user_master_table where user_id='" + TextBox1.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Image1.ImageUrl = dr.GetValue(0).ToString();
                        TextBox2.Text = dr.GetValue(1).ToString();
                        TextBox7.Text = dr.GetValue(15).ToString();
                        TextBox5.Text = (DateTime.Today.Year - Convert.ToDateTime(dr.GetValue(2)).Year - 1).ToString();
                        TextBox12.Text = dr.GetValue(3).ToString();
                        TextBox13.Text = dr.GetValue(4).ToString();
                        TextBox8.Text = dr.GetValue(2).ToString();
                        TextBox3.Text = dr.GetValue(5).ToString();
                        TextBox4.Text = dr.GetValue(6).ToString();
                        TextBox9.Text = dr.GetValue(7).ToString();
                        TextBox10.Text = dr.GetValue(8).ToString();
                        TextBox11.Text = dr.GetValue(9).ToString();
                        TextBox6.Text = dr.GetValue(10).ToString();
                        TextBox14.Text = dr.GetValue(11).ToString();
                        Image2.ImageUrl = dr.GetValue(12).ToString();

                    }

                }
                else
                {
                    Response.Write("<script>alert('Invalid credentials');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateUserStatusByID(string status)
        {
            if (checkIfUserExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    }
                    SqlCommand cmd = new SqlCommand("UPDATE user_master_table SET account_status='" + status + "' WHERE user_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("select * from user_master_table where user_id='" + TextBox1.Text.Trim() + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TextBox7.Text = dr.GetValue(15).ToString();
                        }

                    }
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('User Status Updated');</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    if (Convert.ToString(e.Row.Cells[6].Text) == "active")
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.PaleGreen;
                    }
                    if (Convert.ToString(e.Row.Cells[6].Text) == "pending")
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                    }
                    if (Convert.ToString(e.Row.Cells[6].Text) == "deactive")
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}