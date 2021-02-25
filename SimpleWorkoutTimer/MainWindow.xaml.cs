using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Timers;
using System.Media;

// TODO 
// - adjustable beep/soundfile
// - notification bubble or window or dialog to click
// - run exe on time event (if want to start external apps or websites), dont run exe if previous not closed?
// - if screensaver on, dont play
// - if in meeting, silent notifications

namespace SimpleWorkoutTimer
{
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            // build notifyicon (using windows.forms)
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Images/favicon.ico")).Stream);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseClick);
        }



        // maximize window
        void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // clear old timer
            if (aTimer != null)
            {
                aTimer.Elapsed -= OnTimedEvent;
                aTimer.Dispose();
            }

            // start timer, and minimize
            SetTimer();
        }


        private void SetTimer()
        {
            var parsedTime = 0;
            if (int.TryParse(txtTimer.Text, out parsedTime))
            {
                aTimer = new Timer(parsedTime * 60000); // minutes to ms
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;

                Console.WriteLine("SetTimer");
            }
            else
            {

            }


        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("beep");
            // set timer again?

            // beep, or other info popups to click
            SystemSounds.Beep.Play();

        }
    }
}
