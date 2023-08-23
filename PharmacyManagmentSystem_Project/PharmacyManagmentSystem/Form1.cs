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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=medicineDB;Trusted_Connection=True;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT id,name FROM type";
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            colMedicineType.DataSource = ds.Tables[0];
            colMedicineType.DisplayMember = "name";
            colMedicineType.ValueMember = "id";
            con.Close();
        }

        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction ts = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = ts;
                cmd.CommandText = "INSERT INTO type(name,price) VALUES(@name,@price) SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@name", txtMTName.Text);
                cmd.Parameters.AddWithValue("@price", txtSerialNo.Text);
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["colMedicineType"].Value != null && dataGridView1.Rows[i].Cells["colPrice"].Value != null)
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = con;
                        cmd2.Transaction = ts;
                        cmd2.CommandText = "INSERT INTO result(medicineTypeId,typeId,price) VALUES(@medicineTypeId,@typeId,@price)";
                        cmd2.Parameters.AddWithValue("@medicineTypeId", id);
                        cmd2.Parameters.AddWithValue("@typeId", dataGridView1.Rows[i].Cells["colMedicineType"].Value);
                        cmd2.Parameters.AddWithValue("@price", dataGridView1.Rows[i].Cells["colPrice"].Value);
                        cmd2.ExecuteNonQuery();
                    }
                }
                ts.Commit();
                MessageBox.Show("Data Inserted successfull!!");
            }
            catch (Exception ex)
            {

                ts.Rollback();
                MessageBox.Show(ex.Message + "\nData not saved!!");
            }
            con.Close();
        }
    }
}
