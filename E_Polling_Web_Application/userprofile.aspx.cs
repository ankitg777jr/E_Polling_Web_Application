using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace E_Polling_Web_Application
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        getUserPersonalDetails();

                    }

                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");
                Response.Redirect("userlogin.aspx");
            }
        }

        // update button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("userlogin.aspx");
            }
            else
            {
                updateUserPassword();

            }
        }



        // user defined function


        void updateUserPassword()
        {            
            try
            {
                string password;
                if (TextBox10.Text.Trim() == "")
                {
                    Response.Write("<script>alert('Invaid Entry');</script>");
                    getUserPersonalDetails();
                }
                else
                {
                    password = TextBox10.Text.Trim();
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE user_master_table set password = @password where user_id = '" + TextBox8.Text.Trim() + "';", con);
                    cmd.Parameters.AddWithValue("@password", password);
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {

                        Response.Write("<script>alert('Your Password Updated Successfully');</script>");
                        getUserPersonalDetails();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invaid entry');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void getUserPersonalDetails()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from user_master_table where user_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Image1.ImageUrl = dt.Rows[0]["user_img_link"].ToString();
                TextBox1.Text = dt.Rows[0]["full_name"].ToString();
                TextBox2.Text = dt.Rows[0]["dob"].ToString();
                TextBox15.Text = (DateTime.Today.Year - Convert.ToDateTime(TextBox2.Text).Year -1).ToString();
                TextBox11.Text = dt.Rows[0]["father_name"].ToString();
                TextBox13.Text = dt.Rows[0]["gender"].ToString();
                TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox4.Text = dt.Rows[0]["email_id"].ToString();
                TextBox14.Text = dt.Rows[0]["state"].ToString();
                TextBox6.Text = dt.Rows[0]["city"].ToString();
                TextBox7.Text = dt.Rows[0]["pincode"].ToString();
                TextBox5.Text = dt.Rows[0]["permanent_address"].ToString();
                TextBox12.Text = dt.Rows[0]["aadhaar_no"].ToString();
                Image2.ImageUrl = dt.Rows[0]["aadhaar_img_link"].ToString();
                TextBox8.Text = dt.Rows[0]["user_id"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                if (dt.Rows[0]["account_status"].ToString().Trim() == "active")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-success");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-warning");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "deactive")
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }
    }
}