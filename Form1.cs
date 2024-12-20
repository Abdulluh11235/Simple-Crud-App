using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_L;

namespace Simple_Crud_App
{
    public partial class FrmMain : baseForm
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Padding = new Padding(2);
            this.MinimumSize = new Size(700, 600);

            this.Size = new Size(700, 700);

            ConfigureDataGridView();



        }

        private void ConfigureDataGridView()
        {
            dgvLibrary.ContextMenuStrip = contextMenuStrip1;
            // Fill the container
            dgvLibrary.Dock = DockStyle.Fill;

            // Allow column widths to adjust
            dgvLibrary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Allow row heights to adjust
            dgvLibrary.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Enable word wrapping for cell content
            dgvLibrary.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Refresh row sizes after configuration
            dgvLibrary.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private void AdjustSides()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0); break;
               default: //Restored form (After)
                    if (this.Padding.Top != 2)
                        this.Padding = new Padding(2);
                    break;
            }
        }
 

        private void AdjustPnls()
        {
            pnlSideMenu.Width = (int)(0.19 * ClientSize.Width);
            pnl_BlockMain.Height= (int)(0.14 * ClientSize.Height);

        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            AdjustSides();
            AdjustPnls(); 
            
        }
        private void adjustButtons()
        {
            foreach(Control control in pnlSideMenu.Controls) {
                control.Height = Convert.ToInt32(pnlSideMenu.Height * .18);
            }
        }

        private void pnl_BlockMain_MouseDown(object sender, MouseEventArgs e)
        {
            Draggable();
        }

        private void pnl_BlockSideMenu_MouseDown(object sender, MouseEventArgs e)
        {
            Draggable();
        }

        private void btnMangeMembers_Click(object sender, EventArgs e)
        {
            dgvLibrary.DataSource=clsMember.ListAllMembers();
            dgvLibrary.Tag = "Mem";
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            dgvLibrary.DataSource = clsBook.ListAllBooks();
            dgvLibrary.Tag = "book";

        }

        private void btnLoans_Click(object sender, EventArgs e)
        {
            dgvLibrary.DataSource = clsLoan.ListAllLoans();
            dgvLibrary.Tag = "loan";

        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            dgvLibrary.DataSource = clsEmployee.ListAllEmployees();
            dgvLibrary.Tag = "emp";
        }

        private void pnl_BlockMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlSideMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
