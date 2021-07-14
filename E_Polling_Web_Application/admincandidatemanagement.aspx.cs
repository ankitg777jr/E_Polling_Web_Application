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
    public partial class WebForm9 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfCandidateExists())
            {
                Response.Write("<script>alert('Candidate with this ID already Exist, try some other ID');</script>");
            }
            else
            {
                addNewCandidate();
            }
        }
        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfCandidateExists())
            {
                updateCandidate();

            }
            else
            {
                Response.Write("<script>alert('Candidate does not exist');</script>");
            }
        }
        // delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfCandidateExists())
            {
                deleteCandidate();

            }
            else
            {
                Response.Write("<script>alert('Candidate does not exist');</script>");
            }
        }
        // GO button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            getCandidateByID();
        }



        // user defined function
        void getCandidateByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from candidate_master_table where candidate_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    Image1.ImageUrl = dt.Rows[0][0].ToString();
                    TextBox2.Text = dt.Rows[0][2].ToString();
                    Image2.ImageUrl = dt.Rows[0][3].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Candidate ID');</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }


        void deleteCandidate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from candidate_master_table WHERE candidate_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Candidate Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateCandidate()
        {
            try
            {
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("candidate_imgs/" + filename1));
                string filepath1 = "~/candidate_imgs/" + filename1;

                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                FileUpload2.SaveAs(Server.MapPath("election_symbol_imgs/" + filename2));
                string filepath2 = "~/election_symbol_imgs/" + filename2;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE candidate_master_table SET candidate_img_link=@candidate_img_link, candidate_name=@candidate_name, symbol_img_link=@symbol_img_link WHERE candidate_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@candidate_img_link", filepath1);
                cmd.Parameters.AddWithValue("@candidate_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@symbol_img_link", filepath2);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('candidate Updated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void addNewCandidate()
        {
            try
            {
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("candidate_imgs/" + filename1));
                string filepath1 = "~/candidate_imgs/" + filename1;

                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                FileUpload2.SaveAs(Server.MapPath("election_symbol_imgs/" + filename2));
                string filepath2 = "~/election_symbol_imgs/" + filename2;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO candidate_master_table(candidate_img_link,candidate_id,candidate_name,symbol_img_link) values(@candidate_img_link,@candidate_id,@candidate_name,@symbol_img_link)", con);

                cmd.Parameters.AddWithValue("@candidate_img_link", filepath1);
                cmd.Parameters.AddWithValue("@candidate_id", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@candidate_name", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@symbol_img_link", filepath2);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Candidate added Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIfCandidateExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from candidate_master_table where candidate_id='" + TextBox1.Text.Trim() + "';", con);
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

       
        void clearForm()
        {
            Image1.ImageUrl = "~/imgs/candidate.jpg";
            TextBox1.Text = "";
            TextBox2.Text = "";
            Image2.ImageUrl = "~/imgs/election_symbol.png";
        }

    }
}