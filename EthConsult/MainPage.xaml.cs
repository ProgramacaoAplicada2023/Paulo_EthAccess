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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EthConsult.Classes;
using Nethereum.Web3;
using Newtonsoft.Json;

namespace EthConsult
{
    public class KeyBalance
    {
        public string Key { get; set; }
        public decimal Balance { get; set; }
    }

    public partial class MainPage : Page
    {
        Web3 web3 = new Web3(AppConfigs.infuraLink);
        LoginFunctions loginFunctions = new LoginFunctions();

        List<KeyBalance> balances = new List<KeyBalance>();

        public MainPage()
        {
            InitializeComponent();
        }

        //Funcao que preenche o DataGrid com os saldos das wallets
        public async void ShowBalances(User showUser)
        {
            balances.Clear();

            foreach (var key in showUser.wallets)
            {
                decimal tempBalance = 0;
                try
                {
                    var connection = await web3.Eth.GetBalance.SendRequestAsync(key);
                    tempBalance = Web3.Convert.FromWei(connection.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível pegar o valor da carteira " + key + "\n\nExceção:\n" + ex.Message);
                }
                balances.Add(new KeyBalance { Key = key, Balance = tempBalance });
            }

            dataGrid.ItemsSource = balances;
        }

        private void Transfer(object sender, RoutedEventArgs e)
        {
            Transfer transferWindow = new Transfer();
            transferWindow.ShowDialog();
        }

        private void AddWallet(object sender, RoutedEventArgs e)
        {
            // Display a MessageBox with an input prompt
            string userInput = Microsoft.VisualBasic.Interaction.InputBox("Digite o valor da nova wallet:", "Adicionar Wallet", "");

            //Adiciona a wallet no usuario
            AppConfigs.loggedUser.wallets.Add(userInput);

            //Faz update no arquivo dos usuarios
            loginFunctions.UpdateUser(AppConfigs.loggedUser);

            //Atualiza os valores das carteiras do usuario
            ShowBalances(AppConfigs.loggedUser);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            AppConfigs.loggedUser = null;

            //Muda para a página de login
            Pages.mainWindow.SwitchPage(Pages.loginPage);
        }
    }
}
