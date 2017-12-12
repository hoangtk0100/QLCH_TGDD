using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLCuaHang_TGDD.BUS;
using System.Data.SqlClient;

namespace QLCuaHang_TGDD
{
    public partial class frmConnect : Form
    {
        BUS_Connect BUS_Connect = new BUS_Connect();
        BUS_Account dbTK;
        public frmConnect()
        {
            InitializeComponent();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string error = "";
            BUS_Connect BUS_Connect = new BUS_Connect();
            if (cbServer.Text == "" || txtUser.Text == "" || txtPass.Text == "")    //nếu nhập thiếu thông tin
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin!!");
            }
            else
            {
                try
                {
                    //kiểm tra có đăng nhập vào server được không?
                    bool k = BUS_Connect.SignInPartner(cbServer.Text, txtUser.Text, txtPass.Text, ref error);

                    if (k)
                    {
                        try
                        {
                            MessageBox.Show("Successfully Connect to Server " , "", MessageBoxButtons.OK, MessageBoxIcon.None);
                            this.Hide();
                            frmSign sign = new frmSign();
                            sign.ShowDialog();
                            sign.Show();
                        }
                        catch (Exception er)//bắt lỗi
                        {
                            MessageBox.Show("Không đăng nhập được! Lỗi: " + er.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.ToString());
                    DialogResult = MessageBox.Show("Can not connect to this server. Please check your input", "@ Error @", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (DialogResult == DialogResult.Retry)
                    {
                        txtUser.ResetText();
                        txtPass.ResetText();
                        txtUser.Focus();
                    }
                    if (DialogResult == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }

                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
