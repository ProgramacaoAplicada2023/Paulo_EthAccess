using EthConsult.Classes;
using Newtonsoft.Json;
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

namespace EthConsult
{
    /// <summary>
    /// Interação lógica para LoginPage.xam
    /// </summary>
    public partial class LoginPage : Page
    {
        SolidColorBrush userInputColor;
        SolidColorBrush idleColor;
        string lastFocus = "";
        LoginFunctions loginFunctions = new LoginFunctions();

        public LoginPage()
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
                if (!(textBox.Text == "Username") && !(textBox.Text == "Password"))
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
                    textBox.Text = "Username";
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

        private void Login(object sender, RoutedEventArgs e)
        {
            //Confere se o usuário existe
            string content = File.ReadAllText(AppConfigs.filePath);

            if (loginFunctions.VerifyUser(login.Text, content))
            {
                MessageBox.Show("Usuário não existente!");
                return;
            }

            //Verifica se a senha está correta
            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            User tryingUser = users.Find(x => x.login == login.Text);

            if (tryingUser.password != passwordBox.Password)
            {
                MessageBox.Show("Senha incorreta!");
                return;
            }
            AppConfigs.loggedUser = tryingUser;

            //Muda para a página principal
            Pages.mainWindow.SwitchPage(Pages.mainPage);
            Pages.mainPage.ShowBalances(AppConfigs.loggedUser);
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            Pages.mainWindow.SwitchPage(Pages.signInPage);
        }
    }
}
