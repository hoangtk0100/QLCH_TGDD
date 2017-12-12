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

namespace QLCuaHang_TGDD
{
    public partial class frmSign : Form
    {
        bool f = true;
        public frmSign()
        {
            InitializeComponent();
            bunifuElipse1.ApplyElipse(this,10);
            Separator1.Show();   
        }

        BUS_Account db = new BUS_Account();
        string error;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnSignIn1_Click(object sender, EventArgs e)
        {
            f = true;
            btnChangePW1.IdleFillColor = Color.Transparent;
            btnSignIn1.IdleFillColor = Color.FromArgb(16, 160, 136);
            Separator1.Location= new Point(63, 64);
            slideA.Show();
            slideB.Hide();
        }
        private void btnChangePW1_Click(object sender, EventArgs e)
        {
            f = false;
            btnSignIn1.IdleFillColor=Color.Transparent;
            btnChangePW1.IdleFillColor = Color.FromArgb(16, 160, 136);
            Separator1.Location = new Point(179, 64);

            slideB.Location = slideA.Location;
            slideB.Show();
            slideA.Hide();
        }
        private void frmSign_MouseDown(object sender, MouseEventArgs e)
        {
            //dragform.Drag(this);
        }
        private void frmSign_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void frmSign_MouseUp(object sender, MouseEventArgs e)
        {
            //dragform.CreateObjRef();
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if(f)
            {
                if((txtUsername.Text!="" && txtPass.Text!= "") &&
                    (txtUsername.Text != "Enter User Name" && txtPass.Text!= "Enter Your Password"))
                {
                    int x=db.SignIn(txtUsername.Text, txtPass.Text, ref error);
                    //MessageBox.Show(x.ToString(), "Welcome to Software", MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (x == 0)
                    {
                        lbIncorrected.Text= "Account or Password is Not Correct";
                        txtUsername.ResetText();
                        txtPass.ResetText();
                        txtUsername.Focus();
                    } 
                    else
                    {
                        MessageBox.Show("Congratulation", "Welcome to Software", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Hide();
                        if(x==1)
                        {
                            Form frm = new frmMain();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        else
                        {
                            Form frm = new frmEmployee();
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                        
                    }
                }
                else
                    MessageBox.Show("Please enter all info", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }
        private void btnChangePW_Click(object sender, EventArgs e)
        {
            if(!f)
            {
                if (txtUsername2.Text != "" && txtOldPW.Text != "" && txtReNewPW.Text != ""&&  txtNewPW.Text != "")
                {
                    int k = db.SignIn(txtUsername2.Text, txtOldPW.Text,ref error );
                    if (k !=0)
                    {
                        if (txtNewPW.Text == txtReNewPW.Text)
                        {
                            try
                            {
                                db.ChangePassword(txtUsername2.Text, txtOldPW.Text, txtNewPW.Text, ref error);
                                MessageBox.Show("Complete");
                            }
                            catch
                            {
                                MessageBox.Show("SQL error: "+ error);
                            }
                        }
                        else
                            MessageBox.Show("Your new password and re-enter new password are not the same");
                    }
                    else
                        MessageBox.Show("Username or Old password is wrong!");
                }
                else
                    MessageBox.Show("Please enter all information", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }
        private void txtPass_Click(object sender, EventArgs e)
        {
            txtPass.ResetText();
        }
        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.ResetText();
        }
    }
}
