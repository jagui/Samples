using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GetPageCompletedEventArgs = AsyncWebClient.AsyncWebService.GetPageCompletedEventArgs;

namespace AsyncWebClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (sender, args) => Url.Text = "juanagui.com";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri("http://" + Url.Text, UriKind.Absolute);
                var ws = new AsyncWebService.AsyncWebService();
                ws.GetPageCompleted += WsOnGetPageCompleted;
                ws.GetPageAsync(uri.ToString());
            }
            catch (Exception ex)
            {
                error.Text = ex.ToString();
            }

        }

        private void WsOnGetPageCompleted(object sender, GetPageCompletedEventArgs getPageCompletedEventArgs)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(getPageCompletedEventArgs.Result));
            page.NavigateToStream(stream);
        }
    }
}
