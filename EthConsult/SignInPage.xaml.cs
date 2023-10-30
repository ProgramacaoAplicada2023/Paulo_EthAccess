using EthConsult.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EthConsult
{
    /// <summary>
    /// Lógica interna para SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        SolidColorBrush userInputColor;
        SolidColorBrush idleColor;
        string lastFocus = "";

        public SignInPage()
        {
            InitializeComponent();
            userInputColor = Pages.userInputColor;
            idleColor = Pages.idleColor;
        }

        private void InsertInfo(object sender, RoutedEventArgs e)
        {
            TextBox textBox;
            PasswordBox _passwordBox;
            try
            {
                textBox = (TextBox)sender;
            }
            catch
            {
                textBox = passwordText;
                _passwordBox = passwordBox;
            }

            try
            {
                lastFocus = textBox.Name;
                if (!(textBox.Text == "Username") && !(textBox.Text == "Password") && !(textBox.Text == "Seu Nome") && !(textBox.Text == "Wallet"))
                {
                    return;
                }
                if (textBox.Name == "passwordText")
                {
                    textBox.Visibility = Visibility.Hidden;
                    passwordBox.Visibility = Visibility.Visible;
                    passwordBox.Focus();
                    return;
                }
                textBox.Text = "";
                textBox.Foreground = userInputColor;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetText(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lastFocus == "login")
                {
                    TextBox textBox = (TextBox)sender;
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        return;
                    }
                    textBox.Foreground = idleColor;
                    login.Text = "Username";
                    return;
                }
                if (lastFocus == "nome")
                {
                    TextBox textBox = (TextBox)sender;
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        return;
                    }
                    textBox.Foreground = idleColor;
                    nome.Text = "Seu Nome";
                    return;
                }
                if (lastFocus == "wallet")
                {
                    TextBox textBox = (TextBox)sender;
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        return;
                    }
                    textBox.Foreground = idleColor;
                    nome.Text = "Wallet";
                    return;
                }
                if (lastFocus == "passwordText")
                {
                    PasswordBox passwordBox = (PasswordBox)sender;
                    TextBox textBox = passwordText;
                    if (!string.IsNullOrWhiteSpace(passwordBox.Password))
                    {
                        return;
                    }
                    textBox.Visibility = Visibility.Visible;
                    passwordBox.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {

        }
    }
}
