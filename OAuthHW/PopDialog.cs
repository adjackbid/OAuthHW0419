using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OAuthHW
{
    public partial class PopDialog : Form
    {
        private readonly IWebHost host;

        public PopDialog(IWebHost host)
        {
            InitializeComponent();
            this.host = host;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                host.StopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                host.Dispose();
                this.Close();
            }
        }
    }
}
