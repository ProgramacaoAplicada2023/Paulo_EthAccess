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

                CreateFile(AppConfigs.filePath, users);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível inserir o usuário.\n\nCódigo do erro:\n" + ex.Message);
                return false;
            }
        }

        //Cria um arquivo temporario e reescreve o original
        private void CreateFile(string filePath, List<User> userList)
        {
            File.Create("temp" + AppConfigs.filePath).Close();
            File.WriteAllText("temp" + AppConfigs.filePath, JsonConvert.SerializeObject(userList));
            if (File.Exists(AppConfigs.filePath))
            {
                File.Delete(AppConfigs.filePath);
            }
            File.Copy("temp" + AppConfigs.filePath, AppConfigs.filePath);
            File.Delete("temp" + AppConfigs.filePath);
        }

        //Faz o update caso alguma informação na lista de usuários mude
        public bool UpdateUser(User updateUser, string fileContent = "")
        {
            if (fileContent == "")
            {
                fileContent = AppConfigs.filePath;
            }

            List<User> users = new List<User>();
            users.RemoveAll(x => x.login == updateUser.login);
            users.Add(updateUser);

            try
            {
                CreateFile(AppConfigs.filePath, users);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível fazer o update do usuário.\n\nCódigo do erro:\n" + ex.Message);
                return false;
            }
        }
    }
}
