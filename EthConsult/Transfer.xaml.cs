using EthConsult.Classes;
using Nethereum.Model;
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
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;
using System.Diagnostics;
using Nethereum.RPC.Eth.DTOs;

namespace EthConsult
{
    /// <summary>
    /// Lógica interna para Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        private void FillComboBox()
        {
            cbxConta.ItemsSource = AppConfigs.loggedUser.wallets;
        }

        public Transfer()
        {
            InitializeComponent();
            FillComboBox();
        }

        //O objetivo dessa função é pegar o valor da carteira selecionada no combobox e preencher o textbox com o valor
        private async void TransferEth(string walletFrom, string walletTo, string value)
        {
            try
            {
                //Instanciando a conta selecionada
                var account = new Nethereum.Web3.Accounts.Account(walletFrom);
                var web3 = new Web3(account);

                //Realizando a transação
                var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(walletTo, Convert.ToDecimal(value));

                //Mensagem de sucesso
                if (transaction.Succeeded())
                {
                    MessageBox.Show("Transação realizada com sucesso!");
                    this.Close();
                    return;
                }

                //Mensagem de erro
                if (transaction.Failed())
                {
                    MessageBox.Show("Erro na hora da transferência.\nHash:" + transaction.BlockHash);
                    this.Close();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível fazer a transferência. Confira os dados e tente novamente.\n\nExceção lançada:\n" + ex.Message);
            }
        }

        private async void TransferEth(object sender, RoutedEventArgs e)
        {
            try
            {
                TransferEth(cbxConta.SelectedItem.ToString(), txtConta.Text, txtValor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Confira os valores e tente novamente");
            }
        }
    }
}
