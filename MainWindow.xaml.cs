using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Crisant_FBManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int generatedOTP = 0;
        Regex regex = new Regex("[^0-9]+");
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            txtMobileNo.PreviewTextInput += TxtMobileNo_PreviewTextInput;
            txtOTP.PreviewTextInput += TxtOTP_PreviewTextInput;
            brdComment.Height = gdMain.ActualHeight - 390;
            
        }


        private void TxtOTP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text) || txtMobileNo.Text.Length == 4;
        }

        private void TxtMobileNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {           
            e.Handled = regex.IsMatch(e.Text) || txtMobileNo.Text.Length == 10;
        }

        private void txtMobileNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnGenerateOTP.IsEnabled = (txtMobileNo.Text.Length == 10);
        }

        private void txtOTP_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnValidateOTP.IsEnabled = (txtOTP.Text.Length == 4);
        }

        private void btnGenerateOTP_Click(object sender, RoutedEventArgs e)
        {
            generatedOTP = GenerateRandom();
            if(SendRandomToSMSGateway(generatedOTP))
            {
                //Row 1 Changes
                txtMobileNo.IsEnabled = false;
                btnGenerateOTP.IsEnabled = false;
                //Row 2 Changes
                txtblkOTP.Visibility = txtOTP.Visibility = Visibility.Visible;                
                btnValidateOTP.Visibility= btnResetValidation.Visibility = Visibility.Visible; 
                
            }
            else
                btnResetValidation_Click(null, null);
        }

        private void btnValidateOTP_Click(object sender, RoutedEventArgs e)
        {
            if(txtOTP.Text == generatedOTP.ToString())
            {
                gdUserInput.Visibility = Visibility.Collapsed;
                gdFeedBackMain.Visibility = Visibility.Visible;
                
            }
            else
            {
                MessageBox.Show("Please Enter Valid OTP !!", "OTP Generator", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnResetValidation_Click(object sender, RoutedEventArgs e)
        {
            txtblkOTP.Visibility = btnValidateOTP.Visibility = btnResetValidation.Visibility=txtOTP.Visibility = Visibility.Hidden;
            txtMobileNo.Clear();
            btnValidateOTP.IsEnabled = btnGenerateOTP.IsEnabled = false;
            txtMobileNo.IsEnabled  = true;
                
        }

        private int GenerateRandom()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        private bool SendRandomToSMSGateway(int code)
        {
            bool success = true;
            //TODO: implement when we get URL
            //string url = "";
            //try
            //{

            //    using (WebClient client = new WebClient())
            //    {
            //        string response = client.DownloadString(url);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Sending SMS Failed. Please try again!", "OTP Generation", MessageBoxButton.OK, MessageBoxImage.Error);
            //    success = false;
            //}
            return success;
        }

        private void btnSaveFB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gdUserInput.Visibility = Visibility.Visible;
            gdFeedBackMain.Visibility = Visibility.Hidden;
            btnResetValidation_Click(null, null);
        }
    }
}
