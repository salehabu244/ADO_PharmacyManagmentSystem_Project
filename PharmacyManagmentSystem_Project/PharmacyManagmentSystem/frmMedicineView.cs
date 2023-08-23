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
    public partial class frmMedicineView : Form
    {
        SqlConnection con = new SqlConnection("Server=.;Database=medicineDB;Trusted_Connection=True;");
        public frmMedicineView()
        {
            InitializeComponent();
        }

        private void frmMedicineView_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT m.medicineId,m.medicineName,m.price,m.generic,m.picture,t.name FROM medicines m INNER JOIN type t ON t.id=m.medicineId", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
