using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagmentSystem
{
    public partial class Medicine_Entry : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=medicineDB;Trusted_Connection=True;");
        public Medicine_Entry()
        {
            InitializeComponent();
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

        private void Medicine_Entry_Load(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(txtPicture.Text);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO medicines(medicineName,price,generic,Picture,typeId) VALUES(@n,@p,@g,@pi,@t);";
            cmd.Parameters.AddWithValue("@n", txtMedicineName.Text);
            cmd.Parameters.AddWithValue("@p", txtPrice.Text);
            cmd.Parameters.AddWithValue("@g", txtGeneric.Text);
            cmd.Parameters.Add(new SqlParameter("@pi", SqlDbType.VarBinary) { Value = ms.ToArray() });
            cmd.Parameters.AddWithValue("@t", cmbType.SelectedValue);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Inserted successfully!!";
            ClearAll();
            con.Close();
        }

        private void ClearAll()
        {
            txtMedicineName.Text = "";
            txtGeneric.Text = "";
            txtPicture.Text = "";
            txtPrice.Text = "";
        }
    }
}
