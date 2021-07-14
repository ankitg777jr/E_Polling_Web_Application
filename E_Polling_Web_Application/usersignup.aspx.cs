using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E_Polling_Web_Application
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        // sign up button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkUserExists())
            {

                Response.Write("<script>alert('User Already Exist with this User ID or Aadhaar Number');</script>");
            }
            else
            {
                signUpNewUser();
            }
        }

        // user defined method
        bool checkUserExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from user_master_table where user_id = '" + TextBox8.Text.Trim() + "' OR aadhaar_no = '" + TextBox10.Text.Trim() + "'; ", con);
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
        void signUpNewUser()
        {

            try
            {
                if (DateTime.Today.Year - Convert.ToDateTime(TextBox2.Text).Year < 19)
                {
                    Response.Write("<script>alert('Check the Date of Birth, Age is less then 18');</script>");
                }
                else
                {
                    string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(Server.MapPath("user_imgs/" + filename1));
                    string filepath1 = "~/user_imgs/" + filename1;

                    string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                    FileUpload2.SaveAs(Server.MapPath("aadhaar_card_imgs/" + filename2));
                    string filepath2 = "~/aadhaar_card_imgs/" + filename2;

                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("INSERT INTO user_master_table(user_img_link,full_name,dob,father_name,gender,contact_no,email_id,state,city,pincode,permanent_address,aadhaar_no,aadhaar_img_link,user_id,password,account_status) values(@user_img_link,@full_name,@dob,@father_name,@gender,@contact_no,@email_id,@state,@city,@pincode,@permanent_address,@aadhaar_no,@aadhaar_img_link,@user_id,@password,@account_status)", con);
                    cmd.Parameters.AddWithValue("@user_img_link", filepath1);
                    cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@father_name", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@gender", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email_id", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@permanent_address", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@aadhaar_no", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@aadhaar_img_link", filepath2);
                    cmd.Parameters.AddWithValue("@user_id", TextBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@account_status", "pending");
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}