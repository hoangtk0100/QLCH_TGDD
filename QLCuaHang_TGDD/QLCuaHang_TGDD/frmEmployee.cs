using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHang_TGDD.DAL;
using QLCuaHang_TGDD.BUS;
using System.Data.SqlClient;
using System.IO;
/// <summary>
/// Bus_event  sql for 2 tables
/// BUS_store
/// 
/// 
/// </summary>
namespace QLCuaHang_TGDD
{
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
        }
        private void frmEmployee_Load(object sender, EventArgs e)
        {
            Reset();
            cursection = section.Employee;
            btnCustomer.Normalcolor = Color.FromArgb(118, 194, 201);
            pnlCustomer.Enabled = true;
            pnlCustomer.Show();
            LoadData();
        }
        bool Add = false;
        string error, gender, date;
        public static string strFilePath = null;
        public enum section { Employee, Customer, Product, Event, Account, ImportBill,ExportBill, Store, Statistic };
        public section cursection;

        DataTable dt = null;
        DataSet ds = null;

        BUS_Employee bemp = new BUS_Employee();
        BUS_Customer bcus = new BUS_Customer();
        BUS_Account ba = new BUS_Account();
        BUS_Event bev = new BUS_Event();
        BUS_Product bpro = new BUS_Product();
        BUS_Bill bb = new BUS_Bill();
        BUS_Store bstore = new BUS_Store();

        public static string pro_id_b;
        public static string pro_id;
        public float price;
        void Reset()
        {
            pnlCustomer.Enabled = pnlCustomer.Enabled = pnlProduct.Enabled = pnlEvent.Enabled = pnlStore.Enabled=tabPage1.Enabled= tabImport.Enabled=tabExport.Enabled = pnlExportBill.Enabled= pnlImportBill.Enabled= false;
            pnlCustomer.Hide(); pnlCustomer.Hide(); pnlProduct.Hide();
            pnlEvent.Hide(); pnlStore.Hide();tabPage1.Hide();tabImport.Hide();tabExport.Hide(); pnlExportBill.Hide(); pnlImportBill.Hide();
            btnCustomer.BackColor = btnCustomer.BackColor = btnProduct.BackColor = btnEvent.BackColor = btnStore.BackColor = btnStatistic.BackColor =btnImportBill.BackColor = btnExportBill.BackColor = Color.Transparent;
            btnCustomer.Normalcolor = btnCustomer.Normalcolor = btnProduct.Normalcolor = btnEvent.Normalcolor = btnStore.Normalcolor = btnStatistic.Normalcolor =btnImportBill.Normalcolor= btnExportBill.Normalcolor=Color.Transparent;
            Add = false;
            dt = null; ds = null;
            error = null; gender = null;
            date = null;
            strFilePath = null;
            pro_id_b = null;
            pro_id = null;
            price= 0;
        }

        #region Load Data
        //void LoadEmployee()
        //{
        //    txtEmp_ID.ResetText();
        //    txtMidName1.ResetText();
        //    txtName1.ResetText(); ;
        //    txtSurName1.ResetText();
        //    txtPosition1.ResetText();
        //    txtTel1.ResetText(); ;
        //    txtID1.ResetText(); ;
        //    txtAddress1.ResetText();

        //    btnSave1.Enabled = false;
        //    btnReload1.Enabled = true;
        //    btnAdd1.Enabled = true;
        //    btnEdit1.Enabled = true;
        //    //btnDelete1.Enabled = true;
        //    btnImage1.Enabled = false;
        //    menuSearch1.SelectedIndex = 0;
        //    menuSearch1.Text = menuSearch1.SelectedItem.ToString();
        //}
        void LoadCustomer()
        {
            txtCus_ID.ResetText();
            txtMidName2.ResetText();
            txtName2.ResetText();
            txtSurName2.ResetText();
            txtTel2.ResetText();
            txtID2.ResetText();
            txtAddress2.ResetText();

            btnSave2.Enabled = false;
            btnReload2.Enabled = true;
            btnAdd2.Enabled = true;
            btnEdit2.Enabled = true;
            //btnDelete2.Enabled = true;
            btnImage2.Enabled = false;
            menuSearch2.SelectedIndex = 0;
            menuSearch2.Text = menuSearch2.SelectedItem.ToString();
        }
        void LoadProduct()
        {
            txtPro_ID3.ResetText();
            txtName3.ResetText();
            txtDis_ID3.ResetText();
            txtCa_ID3.ResetText();
            txtQuantity3.ResetText();
            txtExp_Price3.ResetText();

            btnSave3.Enabled = false;
            btnReload3.Enabled = true;
            btnAdd3.Enabled = true;
            btnEdit3.Enabled = true;
            //btnDelete3.Enabled = true;
            btnImage3.Enabled = false;
            menuSearch3.SelectedIndex = 0;
            menuSearch3.Text = menuSearch3.SelectedItem.ToString();
        }
        void LoadEvent()
        {
            txtEv_ID4.ResetText();
            txtEv_Name4.ResetText();
            txtPro_ID4.ResetText();
            txtPro_Name4.ResetText();
            dtpStart4.ResetText();
            dtpEnd4.ResetText();

            txtPro_Name4.Enabled = false;
            btnSave4.Enabled = false;
            btnReload4.Enabled = true;
            btnAdd4.Enabled = true;
            btnEdit4.Enabled = true;
            //btnDelete4.Enabled = true;
            btnImage4.Enabled = false;
            menuSearch4.SelectedIndex = 0;
            menuSearch4.Text = menuSearch4.SelectedItem.ToString();
        }
        void LoadStore()
        {
            txtPro_ID6.ResetText();
            txtDis_ID6.ResetText();
            txtDis_Name6.ResetText();
            dtpImp_Date6.ResetText();
            txtImp_ID6.ResetText();
            txtEmp_ID6.ResetText();
            txtPrice6.ResetText();

            btnSave6.Enabled = false;
            btnReload6.Enabled = true;
            btnImport6.Enabled = true;
            btnEdit6.Enabled = true;
            //btnDelete6.Enabled = true;
            pnlHide6.Enabled = false;
            pnlHide6.Hide();
            dgvImp_Bill6.Enabled = false;
            dgvImp_Bill6.Hide();
            btnImage6.Enabled = false;
            menuSearch6.SelectedIndex = 0;
            menuSearch6.Text = menuSearch6.SelectedItem.ToString();
        }

        void LoadtabExport()
        {
            dgvExportBill.DataSource = bb.Get_Export_Bill().Tables[0];
            dgvExportBill.AutoResizeColumns();
            dgvExportBill_CellContentClick(null, null);

            dgvExportDetail.DataSource = bb.Get_Export_Detail(null, null).Tables[0];
            dgvExportDetail.AutoResizeColumns();
            dgvExportDetail_CellContentClick(null, null);


            dgvProductEB.DataSource = dt;
            dgvProductEB.AutoResizeColumns();
            dgvProductEB_CellContentClick(null, null);
        }

        void LoadtabImport()
        {

            //ds = bb.Get_Import_Bill();
            //dt = ds.Tables[0];
            dgvImportBill.DataSource = bb.Get_Import_Bill().Tables[0];
            dgvImportBill.AutoResizeColumns();
            dgvImportBill_CellContentClick(null, null);


            //ds = bb.Get_Import_Detail(null, null);
            //dt = ds.Tables[0];
            dgvImportDetail.DataSource = bb.Get_Import_Detail(null, null).Tables[0];
            dgvImportDetail.AutoResizeColumns();
            dgvImportDetail_CellContentClick(null, null);

            ds = bpro.Get();
            dt = ds.Tables[0];
            dgvProductIB.DataSource = dt;
            dgvProductIB.AutoResizeColumns();
            dgvProductIB_CellContentClick(null, null);

        }
        void LoadBill()
        {
            txtImportIDib.ResetText();
            txtEmpIDib.ResetText();
            txtDisIDib.ResetText();
            dtpDateib.ResetText();
            txtTotalib.ResetText();

            txtExportIDeb.ResetText();
            txtCusIDeb.ResetText();
            txtEmpIDeb.ResetText();
            txtEventIDeb.ResetText();
            dtpDateeb.ResetText();
            txtTotalEB.ResetText();

            btnSaveEB.Enabled = false;
            btnSaveExporD.Enabled = false;
            btnSaveEB.Enabled = false;
            btnSaveExpD.Enabled = false;
            tabBill.Enabled = true;
            btnAddEB.Enabled = true;
            btnAddImpD.Enabled = true;
            btnAddEB.Enabled = true;
            btnAddExpD.Enabled = true;
            
            btnEditEB.Enabled = true;
            btnEditImpD.Enabled = true;
            btnEditEB.Enabled = true;
            btnEditExpD.Enabled = true;

            btnDeleteImpD.Enabled = true;
            btnDeleteExpD.Enabled = true;
        }
        void LoadData()
        {
            try
            {
                dt = new DataTable();
                ds = new DataSet();
                dt.Clear(); ds.Clear();
                switch (cursection)
                {
                    case section.Employee:
                        //LoadEmployee();
                        //ds = bemp.Get();
                        //dt = ds.Tables[0];
                        //dgvEmployee.DataSource = dt;
                        //dgvEmployee.AutoResizeColumns();
                        //dgvEmployee_CellClick(null, null);
                        break;

                    case section.Customer:
                        LoadCustomer();
                        ds = bcus.Get();
                        dt = ds.Tables[0];
                        dgvCustomer.DataSource = dt;
                        dgvCustomer.AutoResizeColumns();
                        dgvCustomer_CellClick(null, null);
                        break;

                    case section.Product:
                        LoadProduct();
                        ds = bpro.Get();
                        dt = ds.Tables[0];
                        dgvProduct.DataSource = dt;
                        dgvProduct.AutoResizeColumns();
                        dgvProduct_CellClick(null, null);
                        break;

                    case section.Event:
                        LoadEvent();
                        ds = bev.Get();
                        dt = ds.Tables[0];
                        dgvEvent.DataSource = dt;
                        dgvEvent.AutoResizeColumns();
                        dgvEvent_CellClick(null, null);
                        break;

                    case section.Store:
                        LoadStore();
                        ds = bstore.Get();
                        dt = ds.Tables[0];
                        dgvStore.DataSource = dt;
                        dgvStore.AutoResizeColumns();
                        dgvStore_CellClick(null, null);
                        break;

                    case section.ImportBill:
                        LoadBill();
                        LoadtabImport();

                        break;

                    case section.ExportBill:
                        LoadBill();
                        ds = bb.Get_Export_Bill();
                        dt = ds.Tables[0];
                        dgvExportBill.DataSource = dt;
                        dgvExportBill.AutoResizeColumns();
                        dgvExportBill_CellContentClick(null, null);

                        dgvExportDetail.DataSource = bb.Get_Export_Detail(null, null).Tables[0];
                        dgvExportDetail.AutoResizeColumns();
                        dgvExportDetail_CellContentClick(null, null);


                        dgvProductEB.DataSource = bpro.Get().Tables[0];
                        dgvProductEB.AutoResizeColumns();
                        dgvProductEB_CellContentClick(null, null);
                        //LoadtabExport();

                        break;
                        #region   COTINUOUS
                        /*
                          case section.Cart:
                              ds = bb.Get();
                              dt = ds.Tables[0];
                              dgvCart.DataSource = dt;
                              dgvCart.AutoResizeColumns();
                              dgvCart_CellContentClick(null, null);
                              break;
                          case section.Account:
                              ds = bev.Get();
                              dt = ds.Tables[0];
                              dgvAccount.DataSource = dt;
                              dgvAccount.AutoResizeColumns();
                              dgvAccount_CellContentClick(null, null);
                              break;
                         */
                        #endregion
                }

            }
            catch (SqlException ex)
            { MessageBox.Show(ex.Message); }
        }
        #endregion


        #region Button Click
        /*
        #region EMPLOYEE PANEL
        private void btnAdd1_Click(object sender, EventArgs e)
        {
            Add = true;
            btnImage1.Enabled = true;
            btnSave1.Enabled = true;

            txtEmp_ID.ResetText();
            txtEmp_ID.Enabled = true;
            txtMidName1.ResetText();
            txtName1.ResetText(); ;
            txtSurName1.ResetText(); ;
            txtPosition1.ResetText(); ;
            txtTel1.ResetText(); ;
            txtID1.ResetText(); ;
            txtAddress1.ResetText();

            btnReload1.Enabled = true;
            btnAdd1.Enabled = false;
            btnEdit1.Enabled = false;
            //btnDelete1.Enabled = false;

            txtEmp_ID.Focus();
        }
        private void btnEdit1_Click(object sender, EventArgs e)
        {
            Add = false;
            btnImage1.Enabled = true;
            btnSave1.Enabled = true;
            btnReload1.Enabled = true;

            btnAdd1.Enabled = false;
            //btnDelete1.Enabled = false;
            btnEdit1.Enabled = false;
            txtEmp_ID.Enabled = false;
            txtName1.Focus();
        }
        //private void btnDelete1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Lay thu tu chon
        //        int id = dgvEmployee.CurrentCell.RowIndex;
        //        //Lay ma nhan vien do
        //        string ma = dgvEmployee.Rows[id].Cells[0].Value.ToString();
        //        //Thong bao
        //        DialogResult = MessageBox.Show("Are you sure to delete this Emloyee's Info?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        //Kiem tra TraLoi
        //        if (DialogResult == DialogResult.Yes)
        //        {
        //            bemp.Delete(ma, ref error);
        //            //Load lai du lieu
        //            LoadData();
        //            //Thong bao
        //            MessageBox.Show("! Finish !");
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        MessageBox.Show("Can not delete this employee's Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (txtEmp_ID.Text != "" && txtName1.Text != "" && txtSurName1.Text != "" &&
                txtTel1.Text != "" && txtAddress1.Text != "" && txtPosition1.Text != "" && txtID1.Text != "")
            {
                if (rdMale1.Checked)
                {
                    rdFemale1.Checked = false;
                    gender = "True";
                }
                else
                {
                    rdMale1.Checked = false;
                    gender = "False";
                }

                string date = dtp1.Value.ToString("yyyy/MM/dd");

                if (Add)
                {
                    try
                    {
                        bool exist = false;
                        for (int i = 0; i < dgvEmployee.RowCount - 1; i++)
                        {
                            if (txtEmp_ID.Text == dgvEmployee.Rows[i].Cells[0].Value.ToString())
                            {
                                exist = true;
                                MessageBox.Show("Existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        if (!exist)
                        {
                            bool k;
                            k = bemp.Add(txtEmp_ID.Text, txtName1.Text, txtMidName1.Text, txtSurName1.Text, gender, txtID1.Text, txtAddress1.Text, txtTel1.Text, date, txtPosition1.Text, strFilePath,txtStatusEmp.Text, ref error);
                            LoadData();
                            if (k == true)
                                MessageBox.Show("! Finish !");
                            //else
                              //  MessageBox.Show("Position ID is wrong");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Can not add data", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // sua doi
                {
                    try
                    {
                        bool k=false;
                        try {
                            k = bemp.Update(txtEmp_ID.Text, txtName1.Text, txtMidName1.Text, txtSurName1.Text, gender, txtID1.Text, txtAddress1.Text, txtTel1.Text, date, txtPosition1.Text, strFilePath, txtStatusEmp.Text, ref error);  
                        }
                        catch
                        {
                            MessageBox.Show("Sql error: "+error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            
                        }
                        LoadData();
                        if (k == true)
                            MessageBox.Show("Finish!");
                        //else
                        //   MessageBox.Show("Position ID is wrong!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    }
                    catch (SqlException)
                    {
                        DialogResult = MessageBox.Show("SQL Error:"+ error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (DialogResult == DialogResult.Retry)
                            LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all information!");
                txtEmp_ID.Focus();
            }
            Add = false;
        }
        private void btnReload1_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnImage1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png;*jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;
                Image a = new Bitmap(strFilePath);
                btnImage1.Image = a;
                btnImage1.Image = Image.FromFile(strFilePath);
            }
        }
        private void btnSearch1_Click(object sender, EventArgs e)
        {
            int type;
            if (menuSearch1.SelectedItem.ToString() != "")
            {
                type = -1;
                string selected = menuSearch1.SelectedItem.ToString();
                for (int i = 0; i < menuSearch1.Items.Count; i++)
                    if (selected == menuSearch1.Items[i].ToString())
                        type = i;
                if (txtSearch1.Text != "")
                {
                    try
                    {
                        switch (type)
                        {
                            case 0:
                                ds = bemp.Search(0, txtSearch1.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEmployee.DataSource = dt;
                                dgvEmployee.AutoResizeColumns();
                                dgvEmployee_CellClick(null, null);
                                break;
                            case 1:
                                ds = bemp.Search(1, txtSearch1.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEmployee.DataSource = dt;
                                dgvEmployee.AutoResizeColumns();
                                dgvEmployee_CellClick(null, null);
                                break;
                            case 2:
                                ds = bemp.Search(2, txtSearch1.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEmployee.DataSource = dt;
                                dgvEmployee.AutoResizeColumns();
                                dgvEmployee_CellClick(null, null);
                                break;
                        }
                        if (dgvEmployee.Rows[0].Cells[0].Value.ToString() != null)
                            MessageBox.Show("! Found !");
                    }
                    catch
                    {
                        LoadEmployee();
                        MessageBox.Show("This Employee is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Please enter info to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Please choose kind of type to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        #endregion
        */
        #region CUSTOMER PANEL
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            Add = true;
            btnImage2.Enabled = true;
            btnSave2.Enabled = true;

            txtCus_ID.ResetText();
            txtCus_ID.Enabled = true;
            txtMidName2.ResetText();
            txtName2.ResetText();
            txtSurName2.ResetText();
            txtTel2.ResetText();
            txtID2.ResetText();
            txtAddress2.ResetText();

            btnReload2.Enabled = true;
            btnAdd2.Enabled = false;
            btnEdit2.Enabled = false;
            //btnDelete2.Enabled = false;

            txtCus_ID.Focus();
        }
        private void btnEdit2_Click(object sender, EventArgs e)
        {
            Add = false;
            btnImage2.Enabled = true;
            btnSave2.Enabled = true;
            btnReload2.Enabled = true;

            btnAdd2.Enabled = false;
            //btnDelete2.Enabled = false;
            btnEdit2.Enabled = false;
            txtCus_ID.Enabled = false;
            txtName2.Focus();
        }
        //private void btnDelete2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Lay thu tu chon
        //        int id = dgvCustomer.CurrentCell.RowIndex;
        //        //Lay ma nhan vien do
        //        string ma = dgvCustomer.Rows[id].Cells[0].Value.ToString();
        //        //Thong bao
        //        DialogResult = MessageBox.Show("Are you sure to delete this Emloyee's Info?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        //Kiem tra TraLoi
        //        if (DialogResult == DialogResult.Yes)
        //        {
        //            bcus.Delete(ma, ref error);
        //            //Load lai du lieu
        //            LoadData();
        //            //Thong bao
        //            MessageBox.Show("! Finish !");
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        MessageBox.Show("Can not delete this employee's Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (txtCus_ID.Text != "" && txtName2.Text != "" && txtSurName2.Text != "" &&
                txtTel2.Text != "" && txtAddress2.Text != "" && txtID2.Text != "")
            {
                if (rdMale2.Checked)
                {
                    rdFemale2.Checked = false;
                    gender = "True";
                }
                else
                {
                    rdMale2.Checked = false;
                    gender = "False";
                }

                string date = dtp2.Value.ToString("yyyy/MM/dd");

                if (Add)
                {
                    try
                    {
                        bool exist = false;
                        for (int i = 0; i < dgvCustomer.RowCount - 1; i++)
                        {
                            if (txtCus_ID.Text == dgvCustomer.Rows[i].Cells[0].Value.ToString())
                            {
                                exist = true;
                                MessageBox.Show("Existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        if (!exist)
                        {
                            bcus.Add(txtCus_ID.Text, txtName2.Text, txtMidName2.Text, txtSurName2.Text, gender, txtID2.Text, txtAddress2.Text, txtTel2.Text, date, strFilePath, ref error);

                            // Load lại dữ liệu trên DataGridView
                            LoadData();
                            // Thông báo
                            MessageBox.Show("! Finish !");
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Can not add data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // sua doi
                {
                    try
                    {
                        bcus.Update(txtCus_ID.Text, txtName2.Text, txtMidName2.Text, txtSurName2.Text, gender, txtID2.Text, txtAddress2.Text, txtTel2.Text, date, strFilePath, ref error);
                        LoadData();
                        // Thông báo
                        MessageBox.Show("Finish!");
                    }
                    catch (SqlException)
                    {
                        DialogResult = MessageBox.Show("Can not update!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (DialogResult == DialogResult.Retry)
                            LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all information!");
                txtCus_ID.Focus();
            }
            Add = false;
        }
        private void btnReload2_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnImage2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png;*jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;
                Image a = new Bitmap(strFilePath);
                //btnImage2.Image = a;
                btnImage2.Image = Image.FromFile(strFilePath);
            }
        }
        private void btnSearch2_Click(object sender, EventArgs e)
        {
            if (menuSearch2.SelectedItem != null)
            {
                int type = -1;
                string selected = menuSearch2.SelectedItem.ToString();
                for (int i = 0; i < menuSearch2.Items.Count; i++)
                    if (selected == menuSearch2.Items[i].ToString())
                        type = i;
                if (txtSearch2.Text != "")
                {
                    try
                    {
                        switch (type)
                        {
                            case 0://Cus_ID
                                ds = bcus.Search(0, txtSearch2.Text, ref error);
                                dt = ds.Tables[0];
                                dgvCustomer.DataSource = dt;
                                dgvCustomer.AutoResizeColumns();
                                dgvCustomer_CellClick(null, null);
                                break;
                            case 1:// Name
                                ds = bcus.Search(1, txtSearch2.Text, ref error);
                                dt = ds.Tables[0];
                                dgvCustomer.DataSource = dt;
                                dgvCustomer.AutoResizeColumns();
                                dgvCustomer_CellClick(null, null);
                                break;
                            case 2://ID
                                ds = bcus.Search(2, txtSearch2.Text, ref error);
                                dt = ds.Tables[0];
                                dgvCustomer.DataSource = dt;
                                dgvCustomer.AutoResizeColumns();
                                dgvCustomer_CellClick(null, null);
                                break;
                        }
                        if (dgvCustomer.Rows[0].Cells[0].Value.ToString() != null)
                            MessageBox.Show("! Found !");
                    }
                    catch
                    {
                        LoadCustomer();
                        MessageBox.Show("This Customer is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("This Customer is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Please choose kind of type to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #region PRODUCT PANEL
        private void btnAdd3_Click(object sender, EventArgs e)
        {
            Add = true;
            txtPro_ID3.Enabled = true;
            btnSave3.Enabled = true;
            btnImage3.Enabled = true;

            txtPro_ID3.ResetText();
            txtName3.ResetText();
            txtDis_ID3.ResetText();
            txtCa_ID3.ResetText();
            txtQuantity3.ResetText();
            txtExp_Price3.ResetText();

            btnReload3.Enabled = true;
            btnAdd3.Enabled = false;
            btnEdit3.Enabled = false;
            //btnDelete3.Enabled = false;

            txtPro_ID3.Focus();
        }
        private void btnEdit3_Click(object sender, EventArgs e)
        {
            Add = false;
            btnImage3.Enabled = true;
            btnSave3.Enabled = true;
            btnReload3.Enabled = true;

            btnAdd3.Enabled = false;
            //btnDelete3.Enabled = false;
            btnEdit3.Enabled = false;
            txtPro_ID3.Enabled = false;
            txtName3.Focus();
        }
        private void btnDelete3_Click(object sender, EventArgs e)
        {
            try
            {
                //Lay thu tu chon
                int id = dgvProduct.CurrentCell.RowIndex;
                //Lay ma nhan vien do
                string ma = dgvProduct.Rows[id].Cells[0].Value.ToString();
                //Thong bao
                DialogResult = MessageBox.Show("Are you sure to delete this Product's Info?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Kiem tra TraLoi
                if (DialogResult == DialogResult.Yes)
                {
                    bpro.Delete(ma, ref error);
                    //Load lai du lieu
                    LoadData();
                    //Thong bao
                    MessageBox.Show("! Finish !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Can not delete this Product's Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave3_Click(object sender, EventArgs e)
        {
            if (txtPro_ID3.Text != "" && txtName3.Text != "" && txtDis_ID3.Text != "" &&
                txtCa_ID3.Text != "" && txtQuantity3.Text != "" && txtExp_Price3.Text != "")
            {
                if (Add)
                {
                    try
                    {
                        bool exist = false;
                        for (int i = 0; i < dgvProduct.RowCount - 1; i++)
                        {
                            if (txtPro_ID3.Text == dgvProduct.Rows[i].Cells[0].Value.ToString())
                            {
                                exist = true;
                                MessageBox.Show("Existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        if (!exist)
                        {
                            bool k;
                            k = bpro.Add(txtPro_ID3.Text, txtName3.Text, txtDis_ID3.Text, txtCa_ID3.Text, txtQuantity3.Text, float.Parse(txtExp_Price3.Text), strFilePath, ref error);
                            LoadData();
                            if (k == true)
                                MessageBox.Show("! Finish !");
                            else
                                MessageBox.Show("Distributor ID or Category ID is wrong");
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Can not add data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // sua doi
                {
                    try
                    {
                        bool k;
                        k = bpro.Update(txtPro_ID3.Text, txtName3.Text, txtDis_ID3.Text, txtCa_ID3.Text, txtQuantity3.Text, float.Parse(txtExp_Price3.Text), strFilePath, ref error);
                        LoadData();
                        if (k == true)
                            MessageBox.Show("! Finish !");
                        else
                            MessageBox.Show("Distributor ID or Category ID is wrong");
                    }
                    catch (SqlException)
                    {
                        DialogResult = MessageBox.Show("Can not update!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (DialogResult == DialogResult.Retry)
                            LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all information!");
                txtPro_ID3.Focus();
            }
            Add = false;
        }
        private void btnReload3_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnImage3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png;*jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;
                Image a = new Bitmap(strFilePath);
                //btnImage3.Image = a;
                btnImage3.Image = Image.FromFile(strFilePath);
            }
        }
        private void btnSearch3_Click(object sender, EventArgs e)
        {
            if (menuSearch3.SelectedItem != null)
            {
                int type = -1;
                string selected = menuSearch3.SelectedItem.ToString();
                for (int i = 0; i < menuSearch3.Items.Count; i++)
                    if (selected == menuSearch3.Items[i].ToString())
                        type = i;
                if (txtSearch3.Text != "")
                {
                    try
                    {
                        switch (type)
                        {
                            case 0://Pro_ID
                                ds = bpro.Search(0, txtSearch3.Text, ref error);
                                dt = ds.Tables[0];
                                dgvProduct.DataSource = dt;
                                dgvProduct.AutoResizeColumns();
                                dgvProduct_CellClick(null, null);
                                break;
                            case 1://Name
                                ds = bpro.Search(1, txtSearch3.Text, ref error);
                                dt = ds.Tables[0];
                                dgvProduct.DataSource = dt;
                                dgvProduct.AutoResizeColumns();
                                dgvProduct_CellClick(null, null);
                                break;
                            case 2://Dis_ID
                                ds = bpro.Search(2, txtSearch3.Text, ref error);
                                dt = ds.Tables[0];
                                dgvProduct.DataSource = dt;
                                dgvProduct.AutoResizeColumns();
                                dgvProduct_CellClick(null, null);
                                break;
                            case 3://Category_ID
                                ds = bpro.Search(3, txtSearch3.Text, ref error);
                                dt = ds.Tables[0];
                                dgvProduct.DataSource = dt;
                                dgvProduct.AutoResizeColumns();
                                dgvProduct_CellClick(null, null);
                                break;
                        }
                        if (dgvProduct.Rows[0].Cells[0].Value.ToString() != null)
                            MessageBox.Show("! Found !");
                    }
                    catch
                    {
                        LoadProduct();
                        MessageBox.Show("This Product is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Please enter info to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Please choose kind of type to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #region EVENT PANEL
        private void btnAdd4_Click(object sender, EventArgs e)
        {
            Add = true;
            btnImage4.Enabled = true;
            btnSave4.Enabled = true;

            txtEv_ID4.ResetText();
            txtEv_Name4.ResetText();
            txtPro_ID4.ResetText();
            txtPro_Name4.ResetText();
            dtpStart4.ResetText();
            dtpEnd4.ResetText();

            btnReload4.Enabled = true;
            btnAdd4.Enabled = false;
            btnEdit4.Enabled = false;
            //btnDelete4.Enabled = false;

            txtEv_ID4.Focus();
        }
        private void btnEdit4_Click(object sender, EventArgs e)
        {
            Add = false;
            btnImage4.Enabled = true;
            btnSave4.Enabled = true;
            btnReload4.Enabled = true;

            btnAdd4.Enabled = false;
            //btnDelete4.Enabled = false;
            btnEdit4.Enabled = false;
            txtEv_ID4.Enabled = false;
            txtEv_Name4.Focus();
        }
        private void btnDelete4_Click(object sender, EventArgs e)
        {
            try
            {
                //Lay thu tu chon
                int id = dgvEvent.CurrentCell.RowIndex;
                //Lay ma nhan vien do
                string ev_id = dgvEvent.Rows[id].Cells[0].Value.ToString();
                string pro_id = dgvEvent.Rows[id].Cells[4].Value.ToString();
                //Thong bao
                DialogResult = MessageBox.Show("Are you sure to delete this Event's Info?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Kiem tra TraLoi
                if (DialogResult == DialogResult.Yes)
                {
                    bev.Delete(ev_id, pro_id, ref error);
                    //Load lai du lieu
                    LoadData();
                    //Thong bao
                    MessageBox.Show("! Finish !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Can not delete this Event's Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave4_Click(object sender, EventArgs e)
        {
            if (txtEv_ID4.Text != "" && txtEv_Name4.Text != "" && txtPro_ID4.Text != "" &&
                dtpStart4.Text != "" && dtpEnd4.Text != "" && txtSaleoff.Text != "")
            {
                if (Add)
                {
                    try
                    {
                        bool exist = false;
                        for (int i = 0; i < dgvEvent.RowCount - 1; i++)
                        {
                            if (txtEv_ID4.Text == dgvEvent.Rows[i].Cells[0].Value.ToString())
                            {
                                exist = true;
                                MessageBox.Show("Existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        if (!exist)
                        {
                            bool k = bev.Add(txtEv_ID4.Text, txtPro_ID4.Text, float.Parse(txtSaleoff.Text), txtEv_Name4.Text, dtpStart4.Text, dtpEnd4.Text, ref error);
                            LoadData();
                            if (k == true)
                                MessageBox.Show("! Finish !");
                            else
                                MessageBox.Show("Product ID is wrong");
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Can not add data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // sua doi
                {
                    try
                    {
                        bool k = bev.Update(txtEv_ID4.Text, txtPro_ID4.Text, float.Parse(txtSaleoff.Text), txtEv_Name4.Text, dtpStart4.Text, dtpEnd4.Text, ref error);
                        LoadData();
                        if (k == true)
                            MessageBox.Show("! Finish !");
                        else
                            MessageBox.Show("Product ID is wrong");
                    }
                    catch (SqlException)
                    {
                        DialogResult = MessageBox.Show("Can not update!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (DialogResult == DialogResult.Retry)
                            LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all information!");
                txtEv_ID4.Focus();
            }
            Add = false;
        }
        private void btnReload4_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnImage4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png;*jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;
                Image a = new Bitmap(strFilePath);
                //btnImage4.Image = a;
                btnImage4.Image = Image.FromFile(strFilePath);
            }
        }
        private void btnSearch4_Click(object sender, EventArgs e)
        {
            if (menuSearch4.SelectedItem != null)
            {
                int type = -1;
                string selected = menuSearch4.SelectedItem.ToString();
                for (int i = 0; i < menuSearch4.Items.Count; i++)
                    if (selected == menuSearch4.Items[i].ToString())
                        type = i;

                if (txtSearch4.Text != "")
                {
                    try
                    {
                        switch (type)
                        {
                            case 0://Ev_ID
                                ds = bev.Search(0, txtSearch4.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEvent.DataSource = dt;
                                dgvEvent.AutoResizeColumns();
                                dgvEvent_CellClick(null, null);
                                break;
                            case 1://Ev_Name
                                ds = bev.Search(1, txtSearch4.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEvent.DataSource = dt;
                                dgvEvent.AutoResizeColumns();
                                dgvEvent_CellClick(null, null);
                                break;
                            case 2://Pro_ID
                                ds = bev.Search(2, txtSearch4.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEvent.DataSource = dt;
                                dgvEvent.AutoResizeColumns();
                                dgvEvent_CellClick(null, null);
                                break;
                            case 3://Pro_Name
                                ds = bev.Search(3, txtSearch4.Text, ref error);
                                dt = ds.Tables[0];
                                dgvEvent.DataSource = dt;
                                dgvEvent.AutoResizeColumns();
                                dgvEvent_CellClick(null, null);
                                break;
                        }
                        if (dgvEvent.Rows[0].Cells[0].Value.ToString() != null)
                            MessageBox.Show("! Found !");
                    }
                    catch
                    {
                        LoadEvent();
                        MessageBox.Show("This Event with your Product ID is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Please enter info to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Please choose kind of type to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion
        //----
        #region STORE PANEL

        private void btnImport6_Click(object sender, EventArgs e)
        {
            Add = true;
            btnImage6.Enabled = true;
            btnSave6.Enabled = true;
            txtPrice6.Enabled = true;
            txtQuantity6.Enabled = true;
            txtImp_ID6.Enabled = true;
            txtEmp_ID6.Enabled = true;
            txtDis_ID6.Enabled = true;
            txtPro_ID6.Enabled = true;
            txtPro_Name6.Enabled = false;
            txtDis_Name6.Enabled = false;
            txtPro_ID6.ResetText();
            txtPro_Name6.ResetText();
            txtDis_ID6.ResetText();
            txtDis_Name6.ResetText();
            dtpImp_Date6.ResetText();
            txtImp_ID6.ResetText();
            txtEmp_ID6.ResetText();
            pnlHide6.Enabled = true;
            pnlHide6.Show();
            btnReload6.Enabled = true;
            btnImport6.Enabled = false;
            btnEdit6.Enabled = false;
            //btnDelete6.Enabled = false;

            txtImp_ID6.Focus();
        }
        private void btnImp_Bill6_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnImp_Bill6.Text == "Import Bill")
                {
                    btnImp_Bill6.Text = "Products";
                    LoadStore();
                    dgvStore.Enabled = false;
                    dgvStore.Hide();
                    dgvImp_Bill6.Enabled = true;
                    dgvImp_Bill6.Show();

                    ds = new DataSet(); dt = new DataTable();
                    ds.Clear(); dt.Clear();
                    this.store_Imp_BillTableAdapter.Fill(DataSet1.Store_Imp_Bill);
                    bstore.Get_Imp_Bill(ref error);
                    dt = ds.Tables[0];
                    dgvImp_Bill6.DataSource = dt;
                    dgvImp_Bill6.AutoResizeColumns();
                    dgvImp_Bill6_CellClick(null, null);

                }
                else
                {
                    btnImp_Bill6.Text = "Import Bill";
                    LoadStore();
                    dgvStore.Enabled = true;
                    dgvStore.Show();
                    dgvImp_Bill6.Enabled = false;
                    dgvImp_Bill6.Hide();
                    ds = new DataSet(); dt = new DataTable();
                    ds.Clear(); dt.Clear();

                    ds = bstore.Get();
                    dt = ds.Tables[0];
                    dgvStore.DataSource = dt;
                    dgvStore.AutoResizeColumns();
                    dgvStore_CellClick(null, null);
                }
            }
            catch { }
        }
        private void btnEdit6_Click(object sender, EventArgs e)
        {
            Add = false;
            btnImage6.Enabled = true;
            btnSave6.Enabled = true;
            btnReload6.Enabled = true;

            btnImport6.Enabled = false;
            //btnDelete6.Enabled = false;
            btnEdit6.Enabled = false;
            txtImp_ID6.Enabled = false;
            txtPrice6.Enabled = true;
            txtPro_Name6.Enabled = true;
            txtDis_Name6.Enabled = true;
            txtPro_ID6.Focus();
        }
        private void btnDelete6_Click(object sender, EventArgs e)
        {
            try
            {
                //Lay thu tu chon
                int id = dgvStore.CurrentCell.RowIndex;
                //Lay ma nhan vien do
                string imp_id = dgvStore.Rows[id].Cells[0].Value.ToString();
                string pro_id = dgvStore.Rows[id].Cells[1].Value.ToString();
                //Thong bao
                DialogResult = MessageBox.Show("Are you sure to delete this Store's Info?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Kiem tra TraLoi
                if (DialogResult == DialogResult.Yes)
                {
                    bstore.Delete(imp_id, pro_id, ref error);
                    //Load lai du lieu
                    LoadData();
                    //Thong bao
                    MessageBox.Show("! Finish !");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Can not delete this Store's Info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave6_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (txtImp_ID6.Text != ""  && txtPro_ID6.Text != "" && txtEmp_ID6.Text!=""&&
                dtpImp_Date6.Text != "" && txtDis_ID6.Text != ""  && txtQuantity6.Text != "" &&
                txtPrice6.Text != "")
            {
                if (Add)
                {
                    try
                    {
                        bool exist = false;

                        for (int i = 0; i < dgvEvent.RowCount - 1; i++)
                        {
                            if (txtImp_ID6.Text == dgvStore.Rows[i].Cells[0].Value.ToString() && txtPro_ID6.Text == dgvStore.Rows[i].Cells[1].Value.ToString())
                            {
                                exist = true;
                                k = 1;
                                MessageBox.Show("Existed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                k = 0;
                        }

                        if (!exist)
                        {
                            //int m;
                           // m=this.store_Imp_BillTableAdapter.Insert(txtImp_ID6.Text, txtPro_ID6.Text, txtPro_Name6.Text, txtDis_ID6.Text, txtDis_Name6.Text, int.Parse(txtQuantity6.Text), float.Parse(txtPrice6.Text), dtpImp_Date6.Value, txtEmp_ID6.Text, strFilePath);
                            bool h = bstore.Import(k, txtImp_ID6.Text, txtPro_ID6.Text, txtPro_Name6.Text, txtDis_ID6.Text, txtDis_Name6.Text, int.Parse(txtQuantity6.Text), float.Parse(txtPrice6.Text), dtpImp_Date6.Text, txtEmp_ID6.Text,strFilePath, ref error);
                            LoadData();
                            if (h == true )
                                MessageBox.Show("! Finish !");
                            else
                                MessageBox.Show("Some ID is wrong");
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Can not add data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // sua doi
                {
                    try
                    {
                        bool h = bstore.Update(k, txtImp_ID6.Text, txtPro_ID6.Text, txtPro_Name6.Text, txtDis_ID6.Text, txtDis_Name6.Text, int.Parse(txtQuantity6.Text), float.Parse(txtPrice6.Text), dtpImp_Date6.Text, txtEmp_ID6.Text, ref error);
                        LoadData();
                        if (h == true)
                            MessageBox.Show("! Finish !");
                        else
                            MessageBox.Show("Some ID is wrong");
                    }
                    catch
                    {
                        DialogResult = MessageBox.Show("Can not update!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (DialogResult == DialogResult.Retry)
                            LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all information!");
                txtPro_ID6.Focus();
            }
            Add = false;
            pnlHide6.Hide();
        }
        private void btnReload6_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnImage6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png;*jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strFilePath = ofd.FileName;
                Image a = new Bitmap(strFilePath);
                //btnImage6.Image = a;
                btnImage6.Image = Image.FromFile(strFilePath);
            }
        }
        private void btnSearch6_Click(object sender, EventArgs e)
        {
            if (menuSearch6.SelectedItem != null)
            {
                int type = -1;
                string selected = menuSearch6.SelectedItem.ToString();
                for (int i = 0; i < menuSearch6.Items.Count; i++)
                    if (selected == menuSearch6.Items[i].ToString())
                        type = i;

                if (txtSearch6.Text != "")
                {
                    try
                    {
                        switch (type)
                        {
                            case 0://Imp_ID
                                ds = bstore.Search(0, txtSearch6.Text, ref error);
                                dt = ds.Tables[0];
                                dgvStore.DataSource = dt;
                                dgvStore.AutoResizeColumns();
                                dgvStore_CellClick(null, null);
                                break;
                            case 1://Pro_Id
                                ds = bstore.Search(1, txtSearch6.Text, ref error);
                                dt = ds.Tables[0];
                                dgvStore.DataSource = dt;
                                dgvStore.AutoResizeColumns();
                                dgvStore_CellClick(null, null);
                                break;
                            case 2://Pro_name
                                ds = bstore.Search(2, txtSearch6.Text, ref error);
                                dt = ds.Tables[0];
                                dgvStore.DataSource = dt;
                                dgvStore.AutoResizeColumns();
                                dgvStore_CellClick(null, null);
                                break;
                            case 3://Dis_ID
                                ds = bstore.Search(3, txtSearch6.Text, ref error);
                                dt = ds.Tables[0];
                                dgvStore.DataSource = dt;
                                dgvStore.AutoResizeColumns();
                                dgvStore_CellClick(null, null);
                                break;
                            case 4://Dis_Name
                                ds = bstore.Search(4, txtSearch6.Text, ref error);
                                dt = ds.Tables[0];
                                dgvStore.DataSource = dt;
                                dgvStore.AutoResizeColumns();
                                dgvStore_CellClick(null, null);
                                break;
                        }
                        if (dgvStore.Rows[0].Cells[0].Value.ToString() != null)
                            MessageBox.Show("! Found !");
                    }
                    catch
                    {
                        LoadStore();
                        MessageBox.Show("This Store with your Product ID is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Please enter info to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Please choose kind of type to search", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #region IMPORT PANEL
        private void btnAddIB_Click(object sender, EventArgs e)
        {
            Add = true;
            btnSaveIB.Enabled = true;

            txtImportIDib.ResetText();
            txtEmpIDib.ResetText();
            txtDisIDib.ResetText();

            btnAddIB.Enabled = false;
            btnEditIB.Enabled = false;
            btnDeleteImpD.Enabled = false;

            txtImportIDib.Focus();
        }

        private void btnEditIB_Click(object sender, EventArgs e)
        {
            Add = false;
            btnSaveIB.Enabled = true;

            btnAddIB.Enabled = false;
            btnEditIB.Enabled = false;
            txtImportIDib.Enabled = false;
            txtImportIDib.Focus();
        }
        private void btnSaveIB_Click(object sender, EventArgs e)
        {
            //xác nhận có muốn thêm ko
            DialogResult tl = MessageBox.Show("Bạn có muốn thêm Hoa Don Nhap không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                //nếu xác nhận thì kiểm tra xem có nhập đủ thông tin ko
                if (txtImportIDib.Text.Trim().Equals("") || txtEmpIDib.Text.Trim().Equals("") || txtDisIDib.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo");   //nếu chưa đủ thì thông báo
                }
                else
                {
                    string date = dtpDateib.Value.ToString("yyyy/MM/dd");

                    if (Add)
                    {
                        try
                        {
                            string error = "";
                            try
                            {
                                //Thêm mới CTHĐ
                                if (bb.Add_Import_Bill(txtImportIDib.Text, txtDisIDib.Text, txtEmpIDib.Text, dtpDateib.Text, 0, ref error))
                                {   //nếu thêm thành công thì thông báo
                                    MessageBox.Show("Thêm Hoa Don thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    //Lỗi từ sqlserver
                                    MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception er)//bắt các lỗi khác
                            {
                                MessageBox.Show("Thêm Hoa don mới vào dữ liệu không thành công.\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Can not add data", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // sua doi
                    {
                        try
                        {
                            if (bb.Update_Import_Bill(txtImportIDib.Text, txtDisIDib.Text, txtEmpIDib.Text, dtpDateib.Text, 0, ref error))
                            {   //nếu thêm thành công thì thông báo
                                MessageBox.Show("Cap Nhat  thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                //Lỗi từ sqlserver
                                MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException)
                        {
                            DialogResult = MessageBox.Show("SQL Error:" + error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (DialogResult == DialogResult.Retry)
                                LoadData();
                        }
                    }
                }
                   
              }
            Add = false;
        }

        private void btnAddImpD_Click(object sender, EventArgs e)
        {
            Add = true;
            btnSaveIB.Enabled = false;

            btnSaveExporD.Enabled = true;
            btnAddImpD.Enabled = false;
            btnEditImpD.Enabled = false;
            btnDeleteImpD.Enabled = false;
        }

        private void btnEditImpD_Click(object sender, EventArgs e)
        {
            Add = false;
            btnSaveIB.Enabled = false;

            btnSaveExporD.Enabled = true;
            btnAddImpD.Enabled = false;
            btnEditImpD.Enabled = false;
            btnDeleteImpD.Enabled = false;
        }

        private void btnSaveImporD_Click(object sender, EventArgs e)
        {
            //xác nhận có muốn thêm ko
            DialogResult tl = MessageBox.Show("Bạn có muốn them chi tiet Hoa Don Nhap không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                //nếu xác nhận thì kiểm tra xem có nhập đủ thông tin ko
                if (txtImportIDib.Text.Trim().Equals("") || txtEmpIDib.Text.Trim().Equals("") || txtDisIDib.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo");   //nếu chưa đủ thì thông báo
                }
                else
                {
                    string date = dtpDateib.Value.ToString("yyyy/MM/dd");

                    if (Add)
                    {
                        try
                        {
                            string error = "";
                            try
                            {
                                //Thêm mới CTHĐ
                                if (bb.Add_Import_Detail(txtImportIDib.Text, pro_id_b, int.Parse(txtAmountImpD.Text),price, ref error))
                                {   //nếu thêm thành công thì thông báo
                                    MessageBox.Show("Thêm chi tiet Hoa Don thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    LoadBill();
                                }
                                else
                                {
                                    //Lỗi từ sqlserver
                                    MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception er)//bắt các lỗi khác
                            {
                                MessageBox.Show("Thêm chi tite Hoa don mới vào dữ liệu không thành công.\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Can not add data", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // sua doi
                    {
                        try
                        {
                            if (bb.Update_Import_Detail(txtImportIDib.Text, pro_id_b, int.Parse(txtAmountImpD.Text), price, ref error))
                            {   //nếu thêm thành công thì thông báo
                                MessageBox.Show("Cap Nhat  thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                LoadBill();
                            }
                            else
                            {
                                //Lỗi từ sqlserver
                                MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException)
                        {
                            DialogResult = MessageBox.Show("SQL Error:" + error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (DialogResult == DialogResult.Retry)
                                LoadData();
                        }
                    }
                }

            }
            Add = false;
        }
        private void btnDeleteImpD_Click(object sender, EventArgs e)
        {
            try
            {

                //Thong bao
                DialogResult = MessageBox.Show("Are you sure to delete this Product from Import Bill?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Kiem tra TraLoi
                DialogResult tl = MessageBox.Show("Are you sure to delete this Product from Import Bill?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tl == DialogResult.OK)
                {// nếu xác nhận muốn xóa
                    string error = "";
                    try
                    {
                        if (bb.Delete_Import_Detail(txtImportIDib.Text, pro_id_b, ref error))
                        {
                            //khi thành công thì thông báo
                            MessageBox.Show("Xóa chi tiết HD thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            //lỗi từ sqlserver
                            MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception er)
                    {
                        //bắt các lỗi khác
                        MessageBox.Show("Xóa chi tiết hoa don không thành công!\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch(Exception er)
            {
                MessageBox.Show("Xóa chi tiết hoa don không thành công!\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               
        private void btnReloadIB_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion




        #region  EXPORT PANEL
        private void btnAddEB_Click(object sender, EventArgs e)
        {
            Add = true;
            btnSaveEB.Enabled = true;

            txtExportIDeb.ResetText();
            txtCusIDeb.ResetText();
            txtEmpIDeb.ResetText();
            txtEventIDeb.ResetText();

            btnAddEB.Enabled = false;
            btnEditEB.Enabled = false;
            btnDeleteExpD.Enabled = false;

            txtExportIDeb.Focus();
        }

        private void btnEditEB_Click(object sender, EventArgs e)
        {
            Add = false;
            btnSaveEB.Enabled = true;

            btnAddEB.Enabled = false;
            btnEditEB.Enabled = false;
            txtExportIDeb.Enabled = false;
            txtExportIDeb.Focus();
        }
        private void btnSaveEB_Click(object sender, EventArgs e)
        {
            //xác nhận có muốn thêm ko
            DialogResult tl = MessageBox.Show("Bạn có muốn thêm Hoa Don Nhap không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                //nếu xác nhận thì kiểm tra xem có nhập đủ thông tin ko
                if (txtExportIDeb.Text.Trim().Equals("") || txtEmpIDib.Text.Trim().Equals("") || txtDisIDib.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo");   //nếu chưa đủ thì thông báo
                }
                else
                {
                    string date = dtpDateeb.Value.ToString("yyyy/MM/dd");

                    if (Add)
                    {
                        try
                        {
                            string error = "";
                            try
                            {
                                //Thêm mới CTHĐ
                                if (bb.Add_Export_Bill(txtExportIDeb.Text, txtCusIDeb.Text, txtEmpIDeb.Text, txtEventIDeb.Text,dtpDateeb.Text, 0, ref error))
                                {   //nếu thêm thành công thì thông báo
                                    MessageBox.Show("Thêm Hoa Don thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    //Lỗi từ sqlserver
                                    MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception er)//bắt các lỗi khác
                            {
                                MessageBox.Show("Thêm Hoa don mới vào dữ liệu không thành công.\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Can not add data", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // sua doi
                    {
                        try
                        {
                            if (bb.Update_Export_Bill(txtExportIDeb.Text, txtCusIDeb.Text, txtEmpIDeb.Text, txtEventIDeb.Text, dtpDateeb.Text, 0, ref error))
                            {   //nếu thêm thành công thì thông báo
                                MessageBox.Show("Cap Nhat  thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            else
                            {
                                //Lỗi từ sqlserver
                                MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException)
                        {
                            DialogResult = MessageBox.Show("SQL Error:" + error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (DialogResult == DialogResult.Retry)
                                LoadData();
                        }
                    }
                }

            }
            Add = false;
        }

        private void btnAddExpD_Click(object sender, EventArgs e)
        {
            Add = true;
            btnSaveEB.Enabled = false;
             
            btnSaveExporD.Enabled = true;
            btnAddExpD.Enabled = false;
            btnEditExpD.Enabled = false;
            btnDeleteExpD.Enabled = false;
        }

        private void btnEditExpD_Click(object sender, EventArgs e)
        {
            Add = false;
            btnSaveEB.Enabled = false;

            btnSaveExporD.Enabled = true;
            btnAddExpD.Enabled = false;
            btnEditExpD.Enabled = false;
            btnDeleteExpD.Enabled = false;
        }

        private void btnSaveExpD_Click(object sender, EventArgs e)
        {
            //xác nhận có muốn thêm ko
            DialogResult tl = MessageBox.Show("Bạn có muốn them chi tiet Hoa Don Nhap không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                //nếu xác nhận thì kiểm tra xem có nhập đủ thông tin ko
                if (txtEmpIDeb.Text.Trim().Equals("") || txtCusIDeb.Text.Trim().Equals("") || txtEmpIDeb.Text.Trim().Equals("") || txtEventIDeb.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!", "Thông báo");   //nếu chưa đủ thì thông báo
                }
                else
                {
                    string date = dtpDateeb.Value.ToString("yyyy/MM/dd");

                    if (Add)
                    {
                        try
                        {
                            string error = "";
                            try
                            {
                                //Thêm mới CTHĐ
                                if (bb.Add_Export_Detail(txtEmpIDeb.Text, pro_id_b, int.Parse(txtAmountExpD.Text), price, ref error))
                                {   //nếu thêm thành công thì thông báo
                                    MessageBox.Show("Thêm chi tiet Hoa Don thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    LoadtabExport();
                                    LoadBill();
                                }
                                else
                                {
                                    //Lỗi từ sqlserver
                                    MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception er)//bắt các lỗi khác
                            {
                                MessageBox.Show("Thêm chi tite Hoa don mới vào dữ liệu không thành công.\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Can not add data", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // sua doi
                    {
                        try
                        {
                            if (bb.Update_Export_Detail(txtEmpIDeb.Text, pro_id_b, int.Parse(txtAmountExpD.Text), price, ref error))
                            {   //nếu thêm thành công thì thông báo
                                MessageBox.Show("Cap Nhat  thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                LoadtabExport();
                                LoadBill();

                            }
                            else
                            {
                                //Lỗi từ sqlserver
                                MessageBox.Show("Lỗi: " + error, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (SqlException)
                        {
                            DialogResult = MessageBox.Show("SQL Error:" + error, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                            if (DialogResult == DialogResult.Retry)
                                LoadData();
                        }
                    }
                }

            }
            Add = false;
        }
        private void btnDeleteExpD_Click(object sender, EventArgs e)
        {
            try
            {

                //Thong bao
                DialogResult = MessageBox.Show("Are you sure to delete this Product from Import Bill?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //Kiem tra TraLoi
                DialogResult tl = MessageBox.Show("Are you sure to delete this Product from Import Bill?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tl == DialogResult.OK)
                {// nếu xác nhận muốn xóa
                    string error = "";
                    try
                    {
                        if (bb.Delete_Export_Detail(txtEmpIDeb.Text, pro_id_b, ref error))
                        {
                            //khi thành công thì thông báo
                            MessageBox.Show("Xóa chi tiết HD thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            LoadtabExport();
                        }
                        else
                        {
                            //lỗi từ sqlserver
                            MessageBox.Show("Lỗi: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception er)
                    {
                        //bắt các lỗi khác
                        MessageBox.Show("Xóa chi tiết hoa don không thành công!\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("Xóa chi tiết hoa don không thành công!\rLỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReloadEB_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadtabExport();
        }
        #endregion

        #region ELSE

        private void tabExport_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            dt = new DataTable();
            LoadtabExport();
        }
        #endregion

        #endregion

        #region Section Click
        //private void btnCustomer_Click(object sender, EventArgs e)
        //{
        //    cursection = section.Employee;
        //    BUS_Employee bus = new BUS_Employee();
        //    Reset();
        //    btnCustomer.Normalcolor= Color.FromArgb(118, 194, 201);
        //    pnlCustomer.Enabled = true; pnlCustomer.Show();
        //    LoadData();
        //}
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            cursection = section.Customer;
            BUS_Customer bus = new BUS_Customer();
            Reset();
            btnCustomer.Normalcolor= Color.FromArgb(118, 194, 201);
            pnlCustomer.Location = pnlCustomer.Location;
            pnlCustomer.Enabled = true; pnlCustomer.Show();
            LoadData();
        }
        private void btnProduct_Click(object sender, EventArgs e)
        {
            cursection = section.Product;
            BUS_Product bus = new BUS_Product();
            Reset();
            btnProduct.Normalcolor = Color.FromArgb(118, 194, 201);
            pnlProduct.Location = pnlCustomer.Location;
            pnlProduct.Enabled = true; pnlProduct.Show();
            LoadData();
        }
        private void btnEvent_Click(object sender, EventArgs e)
        {
            cursection = section.Event;
            BUS_Event bus = new BUS_Event();
            Reset();
            btnEvent.Normalcolor = Color.FromArgb(118, 194, 201);
            pnlEvent.Location = pnlCustomer.Location;
            pnlEvent.Enabled = true; pnlEvent.Show();
            LoadData();
        }
        private void btnImportBill_Click(object sender, EventArgs e)
        {
            cursection = section.ImportBill;
            BUS_Bill bus = new BUS_Bill();
            Reset();
            btnImportBill.Normalcolor = Color.FromArgb(118, 194, 201);
            tabImport.Location = pnlCustomer.Location;
            tabImport.Enabled = true; tabImport.Show();
          
            pnlImportBill.Location = pnlCustomer.Location;
            pnlImportBill.Enabled = true; pnlImportBill.Show();
            LoadData();
        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {
            cursection = section.ExportBill;
            BUS_Bill bus = new BUS_Bill();
            Reset();
            btnExportBill.Normalcolor = Color.FromArgb(118, 194, 201);
            tabExport.Location = pnlCustomer.Location;
            tabExport.Enabled = true; tabExport.Show();

            pnlExportBill.Location = pnlCustomer.Location;
            pnlExportBill.Enabled = true; pnlExportBill.Show();
            LoadData();
        }
        private void btnStore_Click(object sender, EventArgs e)
        {
            cursection = section.Store;
            BUS_Store bus = new BUS_Store();
            Reset();
            btnStore.Normalcolor = Color.FromArgb(118, 194, 201);
            pnlStore.Location = pnlCustomer.Location;
            pnlStore.Enabled = true; pnlStore.Show();
            LoadData();
            //this.store_DataTableAdapter.Fill(this.DataSet1.Store_Data); 
        }
        private void btnStatistic_Click(object sender, EventArgs e)
        {
            cursection = section.Statistic;
            Reset();
         
            tabPage1.Location = pnlCustomer.Location;
            btnStatistic.Normalcolor = Color.FromArgb(118, 194, 201);
            tabPage1.Enabled = true;tabPage1.Show();
            txtSum1.Text = "Sum: "+Salary_EmployeeTableAdapter.Sum1().ToString()+" VND";
            txtCount1.Text ="Employees: "+ Salary_EmployeeTableAdapter.Count1().ToString();
            this.Salary_EmployeeTableAdapter.FillBy(DataSet1.Salary_Employee);
            this.rpvSalary.RefreshReport();

            txtSum2.Text = "Sum: "+ProductTableAdapter.Sum2().ToString() + " VND";
            txtCount2.Text = "Products: " + ProductTableAdapter.Count2().ToString();
            this.ProductTableAdapter.FillBy(DataSet1.Product);
            this.rpvProduct.RefreshReport();
        }
        private void btnFind1_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.Salary_Employee' table. You can move, or remove it, as needed.
            this.Salary_EmployeeTableAdapter.Fill(this.DataSet1.Salary_Employee, txtFind1.Text);
            this.rpvSalary.RefreshReport();
        }
        private void btnFind2_Click(object sender, EventArgs e)
        {
            this.ProductTableAdapter.Fill(this.DataSet1.Product, txtFind2.Text);
            this.rpvProduct.RefreshReport();
        }
        private void btnReload_rpv_Click(object sender, EventArgs e)
        {
            txtSum1.Text = "Sum: " + Salary_EmployeeTableAdapter.Sum1().ToString() + " VND";
            txtCount1.Text = "Employees: " + Salary_EmployeeTableAdapter.Count1().ToString();
            this.Salary_EmployeeTableAdapter.FillBy(DataSet1.Salary_Employee);
            this.rpvSalary.RefreshReport();
        }
        private void btnReload_rpv2_Click(object sender, EventArgs e)
        {
            txtSum2.Text = "Sum: " + ProductTableAdapter.Sum2().ToString() + " VND";
            txtCount2.Text = "Products: " + ProductTableAdapter.Count2().ToString();
            this.ProductTableAdapter.FillBy(DataSet1.Product);
            this.rpvProduct.RefreshReport();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure to exit??", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {
                this.Close();
                Application.Restart();
            }
        }

        #endregion

        #region Cell Click
        /*
        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvEmployee.CurrentCell.RowIndex;

                txtEmp_ID.Text = dgvEmployee.Rows[r].Cells[0].Value.ToString();
                txtName1.Text = dgvEmployee.Rows[r].Cells[1].Value.ToString();
                txtMidName1.Text = dgvEmployee.Rows[r].Cells[2].Value.ToString();
                txtSurName1.Text = dgvEmployee.Rows[r].Cells[3].Value.ToString();
                if (dgvEmployee.Rows[r].Cells[4].Value.ToString() == "True")
                    rdMale1.Checked = true;
                else
                    rdFemale1.Checked = true;
                txtID1.Text = dgvEmployee.Rows[r].Cells[5].Value.ToString();
                txtAddress1.Text = dgvEmployee.Rows[r].Cells[6].Value.ToString();
                txtTel1.Text = dgvEmployee.Rows[r].Cells[7].Value.ToString();
                dtp1.Text = dgvEmployee.Rows[r].Cells[8].Value.ToString();
                txtPosition1.Text = dgvEmployee.Rows[r].Cells[9].Value.ToString();
                strFilePath = dgvEmployee.Rows[r].Cells[10].Value.ToString();
                if (strFilePath == "")
                    btnImage1.Image = null;
                else
                    btnImage1.Image = Image.FromFile(strFilePath);
              
                txtStatusEmp.Text = dgvEmployee.Rows[r].Cells[11].Value.ToString();
            }
            catch
            { }
        }
        */
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvCustomer.CurrentCell.RowIndex;

                txtCus_ID.Text = dgvCustomer.Rows[r].Cells[0].Value.ToString();
                txtName2.Text = dgvCustomer.Rows[r].Cells[1].Value.ToString();
                txtMidName2.Text = dgvCustomer.Rows[r].Cells[2].Value.ToString();
                txtSurName2.Text = dgvCustomer.Rows[r].Cells[3].Value.ToString();
                if (dgvCustomer.Rows[r].Cells[4].Value.ToString() == "True")
                    rdMale2.Checked = true;
                else
                    rdFemale2.Checked = true;
                txtID2.Text = dgvCustomer.Rows[r].Cells[5].Value.ToString();
                txtAddress2.Text = dgvCustomer.Rows[r].Cells[6].Value.ToString();
                txtTel2.Text = dgvCustomer.Rows[r].Cells[7].Value.ToString();
                dtp2.Text = dgvCustomer.Rows[r].Cells[8].Value.ToString();
                strFilePath = dgvCustomer.Rows[r].Cells[9].Value.ToString();
                if (strFilePath == "")
                    btnImage2.Image = null;
                else
                    btnImage2.Image = Image.FromFile(strFilePath);
            }
            catch
            { }
        }
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvProduct.CurrentCell.RowIndex;

                txtPro_ID3.Text = dgvProduct.Rows[r].Cells[0].Value.ToString();
                txtName3.Text = dgvProduct.Rows[r].Cells[1].Value.ToString();
                txtDis_ID3.Text = dgvProduct.Rows[r].Cells[2].Value.ToString();
                txtCa_ID3.Text = dgvProduct.Rows[r].Cells[3].Value.ToString();
                txtQuantity3.Text = dgvProduct.Rows[r].Cells[4].Value.ToString();
                txtExp_Price3.Text = dgvProduct.Rows[r].Cells[5].Value.ToString();
                strFilePath = dgvProduct.Rows[r].Cells[6].Value.ToString();
                if (strFilePath == "")
                    btnImage3.Image = null;
                else
                    btnImage3.Image = Image.FromFile(strFilePath);
            }
            catch
            { }
        }
        private void dgvEvent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvEvent.CurrentCell.RowIndex;

                txtEv_ID4.Text = dgvEvent.Rows[r].Cells[0].Value.ToString();
                txtEv_Name4.Text = dgvEvent.Rows[r].Cells[1].Value.ToString();
                dtpStart4.Text = dgvEvent.Rows[r].Cells[2].Value.ToString();
                dtpEnd4.Text = dgvEvent.Rows[r].Cells[3].Value.ToString();
                txtPro_ID4.Text = dgvEvent.Rows[r].Cells[4].Value.ToString();
                txtPro_Name4.Text = dgvEvent.Rows[r].Cells[5].Value.ToString();
                txtSaleoff.Text = dgvEvent.Rows[r].Cells[6].Value.ToString();
            }
            catch
            { }
        }

        private void pnlStore_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

       

        private void dgvStore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvStore.CurrentCell.RowIndex;

                txtImp_ID6.Text = dgvStore.Rows[r].Cells[0].Value.ToString();
                txtPro_ID6.Text = dgvStore.Rows[r].Cells[1].Value.ToString();
                txtPro_Name6.Text = dgvStore.Rows[r].Cells[2].Value.ToString();
                txtDis_ID6.Text = dgvStore.Rows[r].Cells[3].Value.ToString();
                txtDis_Name6.Text = dgvStore.Rows[r].Cells[4].Value.ToString();
                txtQuantity6.Text = dgvStore.Rows[r].Cells[5].Value.ToString();
                txtPrice6.Text = dgvStore.Rows[r].Cells[6].Value.ToString();
                dtpImp_Date6.Text = dgvStore.Rows[r].Cells[7].Value.ToString();
                txtEmp_ID6.Text= dgvStore.Rows[r].Cells[8].Value.ToString();
                strFilePath = dgvStore.Rows[r].Cells[9].Value.ToString();
                btnImage6.Image = Image.FromFile(strFilePath);
            }
            catch
            { }
        }

        
        private void dgvImp_Bill6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvStore.CurrentCell.RowIndex;

                txtImp_ID6.Text = dgvImp_Bill6.Rows[r].Cells[0].Value.ToString();
                txtPro_ID6.Text = dgvImp_Bill6.Rows[r].Cells[1].Value.ToString();
                txtPro_Name6.Text = dgvImp_Bill6.Rows[r].Cells[2].Value.ToString();
                txtDis_ID6.Text = dgvImp_Bill6.Rows[r].Cells[3].Value.ToString();
                txtDis_Name6.Text = dgvImp_Bill6.Rows[r].Cells[4].Value.ToString();
                txtQuantity6.Text = dgvImp_Bill6.Rows[r].Cells[5].Value.ToString();
                txtPrice6.Text = dgvImp_Bill6.Rows[r].Cells[6].Value.ToString();
                dtpImp_Date6.Text = dgvImp_Bill6.Rows[r].Cells[7].Value.ToString();
                strFilePath = dgvImp_Bill6.Rows[r].Cells[8].Value.ToString();
                btnImage6.Image = Image.FromFile(strFilePath);
            }
            catch
            { }
        }



        public DataSet dss = new DataSet();
        public DataTable dtt = new DataTable();


        private void dgvImportBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvImportBill.CurrentCell.RowIndex;

                txtImportIDib.Text = dgvImportBill.Rows[r].Cells[0].Value.ToString();
                txtDisIDib.Text = dgvImportBill.Rows[r].Cells[1].Value.ToString();
                txtEmpIDib.Text = dgvImportBill.Rows[r].Cells[2].Value.ToString();
                txtTotalib.Text = dgvImportBill.Rows[r].Cells[3].Value.ToString();
                dtpDateib.Text = dgvImportBill.Rows[r].Cells[4].Value.ToString();
                
              
                dgvImportDetail.DataSource = bb.Get_Import_Detail(txtImportIDib.Text, null).Tables[0];
                dgvImportDetail.AutoResizeColumns();
            }
            catch
            { }
        }

        private void dgvImportDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvImportDetail.CurrentCell.RowIndex;

                pro_id_b = dgvImportDetail.Rows[r].Cells[0].Value.ToString();

            }
            catch
            { }
        }

        private void dgvExportBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvExportBill.CurrentCell.RowIndex;

                txtExportIDeb.Text = dgvExportBill.Rows[r].Cells[0].Value.ToString();
                txtEmpIDeb.Text = dgvExportBill.Rows[r].Cells[1].Value.ToString();
                txtCusIDeb.Text = dgvExportBill.Rows[r].Cells[2].Value.ToString();
                txtEventIDeb.Text = dgvExportBill.Rows[r].Cells[3].Value.ToString();
                txtTotalEB.Text = dgvExportBill.Rows[r].Cells[4].Value.ToString();
                dtpDateeb.Text = dgvExportBill.Rows[r].Cells[5].Value.ToString();
                

                dgvExportDetail.DataSource = bb.Get_Export_Detail(txtExportIDeb.Text, null).Tables[0];
                dgvExportDetail.AutoResizeColumns();
                MessageBox.Show("Exp_ID: " + txtEmpIDeb.Text + " -Pro_ID_B: " + pro_id_b + " -Pro_ID: " + pro_id);
            }
            catch
            { }
        }

        private void dgvExportDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvExportDetail.CurrentCell.RowIndex;

                pro_id_b = dgvExportDetail.Rows[r].Cells[0].Value.ToString();

            }
            catch
            { }
        }

        private void dgvProductIB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvProductIB.CurrentCell.RowIndex;

                pro_id = dgvProductIB.Rows[r].Cells[0].Value.ToString();
                price = float.Parse(dgvProductIB.Rows[r].Cells[5].Value.ToString());
                MessageBox.Show(txtImportIDib.Text+"   " + pro_id_b + "   " + pro_id + "   " + price + "   " + txtAmountExpD.Text);
            }
            catch
            { }
        }

        private void dgvProductEB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = dgvProductEB.CurrentCell.RowIndex;

                pro_id = dgvProductEB.Rows[r].Cells[0].Value.ToString();
                price= float.Parse(dgvProductEB.Rows[r].Cells[5].Value.ToString());

            }
            catch
            { }
        }

        #endregion

    }
}
