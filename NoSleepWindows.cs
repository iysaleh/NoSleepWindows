using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace NoSleepWindows
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NoSleepApp());
        }
    }

    public partial class NoSleepApp : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip contextMenu;
        private System.Windows.Forms.Timer preventSleepTimer;

        // Windows API imports
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(uint esFlags);

        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;

        public NoSleepApp()
        {
            // Hide the form by default
            this.Text = "NoSleepWindows";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(-10000, -10000);
            this.Size = new Size(1, 1);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;

            // Create tray icon
            trayIcon = new NotifyIcon();
            trayIcon.Icon = CreateIcon();
            trayIcon.Visible = true;
            trayIcon.Text = "NoSleepWindows - Running";
            trayIcon.MouseClick += TrayIcon_MouseClick;

            // Create context menu
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Exit", null, (s, e) => ExitApplication());
            trayIcon.ContextMenuStrip = contextMenu;

            // Create timer to prevent sleep
            preventSleepTimer = new System.Windows.Forms.Timer();
            preventSleepTimer.Interval = 60000; // Every 60 seconds
            preventSleepTimer.Tick += PreventSleepTimer_Tick;
            preventSleepTimer.Start();

            // Initial call
            PreventSleep();

            this.FormClosing += NoSleepApp_FormClosing;
        }

        private Icon CreateIcon()
        {
            // Create a simple green circle icon
            Bitmap bitmap = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            g.FillEllipse(Brushes.Green, 4, 4, 24, 24);
            g.DrawEllipse(new Pen(Color.DarkGreen, 2), 4, 4, 24, 24);
            g.Dispose();

            return Icon.FromHandle(bitmap.GetHicon());
        }

        private void PreventSleep()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
        }

        private void PreventSleepTimer_Tick(object sender, EventArgs e)
        {
            PreventSleep();
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Left-click shows a status message
                MessageBox.Show("NoSleepWindows is running.\nYour computer will not sleep.\n\nRight-click for options.", 
                    "NoSleepWindows", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NoSleepApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitApplication();
        }

        private void ExitApplication()
        {
            // Allow system to sleep again
            SetThreadExecutionState(ES_CONTINUOUS);

            preventSleepTimer.Stop();
            preventSleepTimer.Dispose();
            trayIcon.Visible = false;
            trayIcon.Dispose();

            Application.Exit();
        }
    }
}
