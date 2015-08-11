using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nmp_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KeyboardListener KListener = new KeyboardListener();
        public MainWindow()
        {
            InitializeComponent();
            // catch closing event
            this.Closing += Window_Closing;
            // default location - doesn't work atm for some reason...
            String place = "http://music.kaiserapps.com";
            if (!File.Exists("config.txt"))
            {
                // erroring on config, but in the future warnconfig() can be swapped in
                errconfig();
                System.IO.File.WriteAllText("config.txt", "http://music.kaiserapps.com");
            }
            else
            {
                // read the place from the file
                place = File.ReadAllLines("config.txt")[0];
            }
            // minor tweaking for the browser element
            browser.DocumentTitleChanged += titleChanged;
            browser.IsWebBrowserContextMenuEnabled = false;
            browser.AllowWebBrowserDrop = false;
            browser.ScriptErrorsSuppressed = true;
            try
            {
                browser.Navigate(place);
            }
            catch (Exception e)
            {
                // erroring on config, but in the future warnconfig() can be swapped in
                errconfig();
                browser.Navigate("http://music.kaiserapps.com");
            }
            // Register key listener
            KListener.KeyDown += new RawKeyEventHandler(KListener_KeyDown);
        }

        // keylogger
        private void KListener_KeyDown(object sender, RawKeyEventArgs args)
        {
            // we're assuming the glue is already in place
            if (args.Key == Key.MediaNextTrack)
            {
                browser.Document.InvokeScript("nextsong");
            }
            else if (args.Key == Key.MediaPlayPause)
            {
                browser.Document.InvokeScript("playpause");
            }
            else if (args.Key == Key.MediaPreviousTrack)
            {
                browser.Document.InvokeScript("prevsong");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // clean up references to key hooks
            KListener.Dispose();
        }
        private void titleChanged(object sender, EventArgs e)
        {
            // change window title
            this.Title = (sender as System.Windows.Forms.WebBrowser).DocumentTitle;
        }
        private void warnconfig()
        {
            MessageBox.Show("'config.txt' does not exist or contains an invalid URL.\nThe default one (http://music.kaiserapps.com/) will be used.\nTo change this, please edit the file 'config.txt' and make the first line a valid URL.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void errconfig()
        {
            MessageBox.Show("'config.txt' does not exist or contains an invalid URL.\nTo change this, please edit the file 'config.txt' and make the first line a valid URL.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(3);
        }
    }

}