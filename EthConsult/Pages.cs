using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EthConsult
{
    public static class Pages
    {
        #region Colors
        public static SolidColorBrush userInputColor = Brushes.Black;
        public static SolidColorBrush idleColor = Brushes.LightGray;
        #endregion

        #region Pages
        public static MainWindow mainWindow { get; set; }
        public static LoginPage loginPage = new LoginPage();
        public static SignInPage signInPage = new SignInPage();
        public static MainPage mainPage = new MainPage();
        #endregion
    }
}
