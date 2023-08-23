using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagmentSystem
{
    public partial class frmMedicineTypeEntry : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=medicineDB;Trusted_Connection=True;");
        public frmMedicineTypeEntry()
        {
            InitializeComponent();
        }

        private void frmMedicineTypeEntry_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM type";

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO type(name) VALUES('" + txtMTName.Text + "')", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Inserted Successfully!!";
            ClearAll();
            con.Close();
        }

        private void ClearAll()
        {
            txtMTName.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE type SET name='" + txtMTName.Text + "'", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Updated Successfully!!";
            con.Close();
            ClearAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM type WHERE id=" + txtId.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtMTName.Text = dt.Rows[0][1].ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM type WHERE id=" + txtId.Text + "", con);
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Data Deleted Successfully!!";
            con.Close();
            ClearAll();
        }
    }
}
