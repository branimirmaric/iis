using System.Windows;
using System.Windows.Controls;

namespace Zadatak07
{
    /// <summary>
    /// Interaction logic for Zadatak06.xaml
    /// </summary>
    public partial class Zadatak06 : Page
    {
        public Zadatak06()
        {
            InitializeComponent();
            LoadToken();
        }

        private void LoadToken()
        {
            if (Application.Current.Properties.Contains("AccessToken") && Application.Current.Properties.Contains("RefreshToken"))
            {
                var accessToken = Application.Current.Properties["AccessToken"] as string;
                var refreshToken = Application.Current.Properties["RefreshToken"] as string;

                tbAccessToken.Text = $"Access Token: {accessToken}";
                tbRefreshToken.Text = $"Refresh Token: {refreshToken}";
            }
            else
            {
                tbAccessToken.Text = "No Access Token found.";
                tbRefreshToken.Text = "No Refresh Token found.";
            }
        }
    }
}
