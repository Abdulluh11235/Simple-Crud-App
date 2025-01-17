﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Crud_App
{


        public partial class baseForm : Form
        {
            public baseForm()
            {
               // InitializeComponent();
            }

            private void baseForm_Load(object sender, EventArgs e)
            {

            }
            protected override void WndProc(ref Message m) //low level cool stuff directly dealing with the OS
            {

                const int WM_NCCALCSIZE = 0x0083;// Standar Title Bar - Snap Window
                const int WM_SYSCOMMAND = 0x0112;
                const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
                const int SC_RESTORE = 0x09;
                const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
                const int resizeAreaSize = 10;

                #region Form Resize
                // Resize/ WM_NCHITTEST values
                const int HTCLIENT = 1; //Represents the client area of the window
                const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
                const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
                const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
                const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
                const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
                const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
                const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
                const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right

                ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>

                if (m.Msg == WM_NCHITTEST)
                { //  WM_NCHITTEST  sent by the operating system when the mouse interacts with a window
                    base.WndProc(ref m);
                    if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                    {
                        if ((int)m.Result == HTCLIENT) //area
                        {
                            Point screenPoint = new Point(m.LParam.ToInt32());
                            Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          

                            if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                            {
                                if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                    m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                                else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                    m.Result = (IntPtr)HTTOP; //Resize vertically up
                                else //Resize diagonally to the right
                                    m.Result = (IntPtr)HTTOPRIGHT;
                            }
                            else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                            {
                                if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                    m.Result = (IntPtr)HTLEFT;
                                else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                    m.Result = (IntPtr)HTRIGHT;
                            }
                            else
                            {
                                if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                    m.Result = (IntPtr)HTBOTTOMLEFT;
                                else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                    m.Result = (IntPtr)HTBOTTOM;
                                else //Resize diagonally to the right
                                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                            }
                        }
                    }
                    return;
                }
                #endregion

                //Remove border and keep snap window
                if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
                {
                    return;
                }

                //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
                if (m.Msg == WM_SYSCOMMAND)
                {
                    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                    /// Quote:
                    /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                    /// are used internally by the system.To obtain the correct result when testing 
                    /// the value of wParam, an application must combine the value 0xFFF0 with the 
                    /// wParam value by using the bitwise AND operator.
                    int wParam = (m.WParam.ToInt32() & 0xFFF0);

                    Size formSize = new Size();
                    if (wParam == SC_MINIMIZE)  //Before
                        formSize = this.ClientSize;
                    if (wParam == SC_RESTORE)// Restored form(Before)
                        this.Size = formSize;
                }
                base.WndProc(ref m);

            }
            [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")] /*try finding a sol to duplication in this prog since 
                  each from used in this app will have this code  */
            protected extern static void ReleaseCapture();
            [DllImport("user32.DLL", EntryPoint = "SendMessage")]
            protected extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
            protected void Draggable()
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
    }
