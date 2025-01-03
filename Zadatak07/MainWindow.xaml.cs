using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Zadatak07
{
    public partial class MainWindow : Window
    {
        private string _accessToken;
        private string _refreshToken;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();     // admin
            var password = PasswordBox.Password.Trim();     // 12345678

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var url = $"http://localhost:5184/api/user/login?username={username}&password={password}";
            try
            {
                var response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var json = JsonDocument.Parse(responseData);

                    _accessToken = json.RootElement.GetProperty("accessToken").GetString();
                    _refreshToken = json.RootElement.GetProperty("refreshToken").GetString();

                    Application.Current.Properties["AccessToken"] = _accessToken;
                    Application.Current.Properties["RefreshToken"] = _refreshToken;

                    LoginStatusTextBlock.Text = "Login successful!";
                    ShowTaskTabs();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    LoginStatusTextBlock.Text = $"Login failed: {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                LoginStatusTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        private void PasswordEnter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void ShowTaskTabs()
        {
            Z01Tab.Visibility = Visibility.Visible;
            Z02Tab.Visibility = Visibility.Visible;
            Z03Tab.Visibility = Visibility.Visible;
            Z04Tab.Visibility = Visibility.Visible;
            Z05Tab.Visibility = Visibility.Visible;
            Z06Tab.Visibility = Visibility.Visible;

            Z01Frame.Content = new Zadatak01();
            Z02Frame.Content = new Zadatak02();
            Z03Frame.Content = new Zadatak03();
            Z04Frame.Content = new Zadatak04();
            Z05Frame.Content = new Zadatak05();
            Z06Frame.Content = new Zadatak06();

            MainTabControl.SelectedIndex = 1;
        }
    }
}
