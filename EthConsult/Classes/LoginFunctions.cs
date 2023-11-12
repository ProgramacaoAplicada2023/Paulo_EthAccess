using ControlzEx.Standard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace EthConsult.Classes
{
    class LoginFunctions
    {
        //Verifica se já tem um usuário com o mesmo login
        public bool VerifyUser(string login, string fileContent)
        {
            if (fileContent == "")
            {
                return true;
            }
            List<User> users = JsonConvert.DeserializeObject<List<User>>(fileContent);
            if (users.Select(x => x.login).Contains(login))
            {
                return false;
            }
            return true;
        }

        //Insere o usuário no arquivo de usuários
        public bool InsertUser(string fileContent, User signUpUser)
        {
            List<User> users = new List<User>();
            if (fileContent != "")
            {
                users = JsonConvert.DeserializeObject<List<User>>(fileContent);
            }

            try
            {
                users.Add(signUpUser);

                //Cria um arquivo temporario e reescreve o original
                File.Create("temp" + Eth.filePath).Close();
                File.WriteAllText("temp" + Eth.filePath, JsonConvert.SerializeObject(users));
                if (File.Exists(Eth.filePath))
                {
                    File.Delete(Eth.filePath);
                }
                File.Copy("temp" + Eth.filePath, Eth.filePath);
                File.Delete("temp" + Eth.filePath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível inserir o usuário.\n\nCódigo do erro:\n" + ex.Message);
                return false;
            }
        }
    }
}
