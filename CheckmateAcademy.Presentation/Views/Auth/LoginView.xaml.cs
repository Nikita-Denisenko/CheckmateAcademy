using System.Windows;
using System.Windows.Controls;
using CheckmateAcademy.Application.DTOs.Requests.Auth;
using CheckmateAcademy.Application.Interfaces.Services;

namespace CheckmateAcademy.Presentation.Views.Auth
{
    public partial class LoginView : UserControl
    {
        private readonly IAuthService _authService;

        public event Action? AuthenticationSucceeded;
        public event Action? RegisterRequested;

        public LoginView(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void LoginButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;

            try
            {
                LoginButton.IsEnabled = false;

                var request = new LoginRequest(
                    EmailTextBox.Text,
                    PasswordBox.Password);

                await _authService.LoginAsync(
                    request,
                    CancellationToken.None);

                AuthenticationSucceeded?.Invoke();
            }
            catch (UnauthorizedAccessException)
            {
                ShowError("Неверный email или пароль.");
            }
            catch (ArgumentException exception)
            {
                ShowError(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                ShowError(exception.Message);
            }
            catch (Exception)
            {
                ShowError("Не удалось выполнить вход. Попробуйте позже.");
            }
            finally
            {
                LoginButton.IsEnabled = true;
            }
        }

        private void RegisterButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            RegisterRequested?.Invoke();
        }

        private void ShowError(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }
    }
}
