using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace Dissconnected_arche
{
    public partial class Form1 : Form
    {
       
            SqlConnection con;
            SqlDataAdapter da;
            SqlCommandBuilder scb;
            DataSet ds;

        public Form1()
            {
                InitializeComponent();
                string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
                con = new SqlConnection(constr);
            }

            private void Form1_Load(object sender, EventArgs e)
            {

            }
            public DataSet GetAllempl()
            {
                da = new SqlDataAdapter("select * from empl", con);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                scb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "empl");
                return ds;
            }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllempl();
                DataRow row = ds.Tables["empl"].NewRow();
                row["name"] = txtName.Text;
                row["salary"] = txtSalary.Text;
                ds.Tables["empl"].Rows.Add(row);
                int result = da.Update(ds.Tables["empl"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

