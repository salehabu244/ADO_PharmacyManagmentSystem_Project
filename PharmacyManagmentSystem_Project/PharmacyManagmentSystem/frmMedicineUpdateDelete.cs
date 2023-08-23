using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PharmacyManagmentSystem
{
    public partial class frmMedicineUpdateDelete : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=medicineDB;Trusted_Connection=True;");
        public frmMedicineUpdateDelete()
        {
            InitializeComponent();
        }

        private void frmMedicineUpdateDelete_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT id,name FROM type";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            cmbType.DataSource = ds.Tables[0];
            cmbType.DisplayMember = "name";
            cmbType.ValueMember = "id";
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT medicineId,medicineName,price,generic,picture,typeId,expiredDate,stock FROM medicines WHERE medicineId=@i";
            cmd.Parameters.AddWithValue("@i", txtSearch.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtMName.Text = dt.Rows[0][1].ToString();
                txtPrice.Text = dt.Rows[0][2].ToString();
                txtGeneric.Text = dt.Rows[0][3].ToString();
                cmbType.SelectedValue = dt.Rows[0][5].ToString();
                dateTimePicker1.Text = dt.Rows[0][6].ToString();
                //chkGender.Text = dt.Rows[0][7].ToString();
                string genderStatus = dt.Rows[0][7].ToString();
                if (genderStatus == "True")
                {
                    ckhStock.Checked = true;
                }
                else
                {
                    ckhStock.Checked = false;
                }
                MemoryStream ms = new MemoryStream((byte[])dt.Rows[0][4]);
                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;

            }
            con.Close();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                this.pictureBox1.Image = img;
                txtPicture.Text = openFileDialog1.FileName;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtPicture.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);


            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE medicines SET medicineName=@n,price=@pr,generic=@g,picture=@p,typeId=@t,expiredDate=@ed,stock=@s WHERE medicineId=@i";
            cmd.Parameters.AddWithValue("@i", txtSearch.Text);
            cmd.Parameters.AddWithValue("@n", txtMName.Text);
            cmd.Parameters.AddWithValue("@pr", txtPrice.Text);
            cmd.Parameters.AddWithValue("@g", txtGeneric.Text);
            cmd.Parameters.AddWithValue("@ed", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@s", ckhStock.Checked.ToString());

            cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@t", cmbType.SelectedValue);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Updated successfully!!!";
            txtSearch.Text = "";
            txtMName.Text = "";
            txtPrice.Text = "";
            txtGeneric.Text = "";
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete from medicines WHERE medicineId=@i";
            cmd.Parameters.AddWithValue("@i", txtSearch.Text);

            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted successfully!!!";
            txtSearch.Text = "";
            txtMName.Text = "";
            txtPrice.Text = "";
            txtGeneric.Text = "";
            con.Close();
        }
    }
}
