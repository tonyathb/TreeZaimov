using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TREES_Zaimov_11a
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connstr = "server=localhost;" +//localhost;" +//192.168.1.237;" +//192.168.137.1//"server=10.42.41.215;" +   87.97.181.216" +
                "port=3306;" +
                "user=REMOTE;" +
                "password=Aa123456@;" +
                "database=trees";

        private void LoadComboBox(string stringQuery, System.Windows.Forms.ComboBox control)
        {
            //1. CONNECT to DB tree
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            //2. query to DB
            MySqlCommand query = new MySqlCommand(stringQuery, connect);
            //3. get the data from DB
            MySqlDataReader readerCombo = query.ExecuteReader();

            //4. if have many data from SELECT query
            //READ a selected records
            List<ComboBoxItem> items = new List<ComboBoxItem>();
            while (readerCombo.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = readerCombo[1].ToString();
                item.Value = (int)readerCombo[0];

                items.Add(item);
            }

            control.DataSource = items;
            control.DisplayMember = "Text";
            control.ValueMember = "Value";

            readerCombo.Close();
            connect.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            MessageBox.Show("Connection NOW opened");
            connect.Close();
            //text query
            string str = "SELECT * FROM class";
            LoadComboBox(str, cmbClass);
            str = "SELECT * FROM family";
            LoadComboBox(str, cmbFam);
            str = "SELECT * FROM otdel";
            LoadComboBox(str, cmbOtdel);
            str = "SELECT * FROM razred";
            LoadComboBox(str, cmbRazred);
            str = "SELECT * FROM rod";
            LoadComboBox(str, cmbRod);
            str = "SELECT * FROM type";
            LoadComboBox(str, cmbVid);
            pictureBox1.Image = System.Drawing.Image.FromFile(pictureBox1.ImageLocation);
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(pictureBox1.ImageLocation);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO `trees`.`tree` " +
                "(`name`, `photo`, " +
                "`otdel_id`, `class_id`,`razred_id`, `family_id`, `rod_id`, `type_id`, " +
                "`discription`)" +
                " VALUES (@name, @img, @otdel, @class,@razred,@family,@rod,@vid,@info);";
             // " VALUES ('Gigant', 0x6A6A6A, '1', '2', '1', '1', '1', ',kmkkl');";

            //1. CONNECT to DB tree
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();//connection string
            //2. query to DB
            MySqlCommand query = new MySqlCommand(sql, connect);           
           

            query = new MySqlCommand(sql, connect);
            query.Parameters.AddWithValue("@name", txtName.Text);
            query.Parameters.AddWithValue("@otdel", cmbOtdel.SelectedValue);
            query.Parameters.AddWithValue("@class", cmbClass.SelectedValue);
            query.Parameters.AddWithValue("@razred", cmbRazred.SelectedValue);
            query.Parameters.AddWithValue("@family", cmbFam.SelectedValue);
            query.Parameters.AddWithValue("@rod", cmbRod.SelectedValue);
            query.Parameters.AddWithValue("@vid", cmbVid.SelectedValue);
            query.Parameters.AddWithValue("@info", txtDescription.Text);
            query.Parameters.AddWithValue("@img", txtImageURL.Text);
                                                                                    // pictureBox1.ImageLocation);

            query.ExecuteNonQuery();


            connect.Close();
        }

        private void txtImageURL_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
