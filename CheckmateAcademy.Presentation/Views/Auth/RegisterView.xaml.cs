
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CheckmateAcademy.Application.DTOs.Requests.Auth;
using CheckmateAcademy.Application.Interfaces.Services;

namespace CheckmateAcademy.Presentation.Views.Auth
{
    public partial class RegisterView : UserControl
    {
        private const string BirthDatePlaceholder = "ДД.ММ.ГГГГ";

        private readonly IAuthService _authService;
        private bool _isUpdatingBirthDate;

        public event Action? AuthenticationSucceeded;
        public event Action? LoginRequested;

        public RegisterView(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void RegisterButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                ShowError("Введите имя.");
                return;
            }

            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
            {
                ShowError("Введите фамилию.");
                return;
            }

            if (!TryGetBirthDate(out var birthDate))
            {
                ShowError("Введите корректную дату рождения в формате ДД.ММ.ГГГГ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                ShowError("Введите электронную почту.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                ShowError("Введите пароль.");
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ShowError("Пароли не совпадают.");
                return;
            }

            try
            {
                RegisterButton.IsEnabled = false;

                var request = new RegisterRequest(
                    NameTextBox.Text.Trim(),
                    SurnameTextBox.Text.Trim(),
                    birthDate,
                    EmailTextBox.Text.Trim(),
                    PasswordBox.Password);

                await _authService.RegisterAsync(
                    request,
                    CancellationToken.None);

                AuthenticationSucceeded?.Invoke();
            }
            catch (ArgumentException exception)
            {
                ShowError(exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                ShowError(exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                ShowError(exception.Message);
            }
            catch (Exception)
            {
                ShowError("Не удалось создать аккаунт. Попробуйте позже.");
            }
            finally
            {
                RegisterButton.IsEnabled = true;
            }
        }

        private bool TryGetBirthDate(out DateTime birthDate)
        {
            birthDate = default;

            var value = BirthDateTextBox.Text.Trim();

            return DateTime.TryParseExact(
                       value,
                       "dd.MM.yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out birthDate)
                   && birthDate.Date <= DateTime.Today;
        }

        private void BirthDateTextBox_GotFocus(
            object sender,
            RoutedEventArgs e)
        {
            if (BirthDateTextBox.Text == BirthDatePlaceholder)
            {
                BirthDateTextBox.Clear();
                BirthDateTextBox.Foreground =
                    new SolidColorBrush(Color.FromRgb(248, 250, 252));
            }
        }

        private void BirthDateTextBox_LostFocus(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BirthDateTextBox.Text))
            {
                BirthDateTextBox.Text = BirthDatePlaceholder;
                BirthDateTextBox.Foreground =
                    new SolidColorBrush(Color.FromRgb(113, 128, 150));
            }
        }

        private void BirthDateTextBox_PreviewTextInput(
            object sender,
            TextCompositionEventArgs e)
        {
            if (e.Text.Any(character => !char.IsDigit(character)))
            {
                e.Handled = true;
                return;
            }

            e.Handled = true;

            InsertBirthDateDigits(e.Text);
        }

        private void BirthDateTextBox_PreviewKeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                e.Handled = true;
                DeleteBirthDateDigits();
                return;
            }

            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                DeleteForwardBirthDateDigits();
                return;
            }

            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void BirthDateTextBox_Pasting(
            object sender,
            DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(DataFormats.Text))
            {
                e.CancelCommand();
                return;
            }

            var pastedText = e.DataObject.GetData(DataFormats.Text) as string;

            if (string.IsNullOrWhiteSpace(pastedText))
            {
                e.CancelCommand();
                return;
            }

            var digits = new string(
                pastedText.Where(char.IsDigit).ToArray());

            if (digits.Length != 8)
            {
                e.CancelCommand();
                return;
            }

            e.CancelCommand();

            BirthDateTextBox.Clear();
            BirthDateTextBox.Foreground =
                new SolidColorBrush(Color.FromRgb(248, 250, 252));

            InsertBirthDateDigits(digits);
        }

        private void InsertBirthDateDigits(string digits)
        {
            if (_isUpdatingBirthDate)
                return;

            var currentDigits = GetBirthDateDigits();

            if (BirthDateTextBox.Text == BirthDatePlaceholder)
                currentDigits = string.Empty;

            var caret = GetDigitPosition(BirthDateTextBox.CaretIndex);

            var selectionStart = GetDigitPosition(
                BirthDateTextBox.SelectionStart);

            var selectionEnd = selectionStart +
                               CountDigits(BirthDateTextBox.SelectedText);

            if (BirthDateTextBox.SelectionLength > 0)
            {
                currentDigits = currentDigits.Remove(
                    selectionStart,
                    Math.Min(selectionEnd - selectionStart,
                        currentDigits.Length - selectionStart));

                caret = selectionStart;
            }

            var available = 8 - currentDigits.Length;

            if (available <= 0)
                return;

            digits = digits[..Math.Min(digits.Length, available)];

            currentDigits = currentDigits.Insert(caret, digits);

            SetBirthDateDigits(currentDigits, caret + digits.Length);
        }

        private void DeleteBirthDateDigits()
        {
            var currentDigits = GetBirthDateDigits();

            if (BirthDateTextBox.Text == BirthDatePlaceholder)
                return;

            var selectionStart = GetDigitPosition(
                BirthDateTextBox.SelectionStart);

            var selectionLength = CountDigits(
                BirthDateTextBox.SelectedText);

            var caret = selectionStart;

            if (selectionLength > 0)
            {
                currentDigits = currentDigits.Remove(
                    selectionStart,
                    Math.Min(selectionLength,
                        currentDigits.Length - selectionStart));
            }
            else if (caret > 0)
            {
                currentDigits = currentDigits.Remove(caret - 1, 1);
                caret--;
            }

            SetBirthDateDigits(currentDigits, caret);
        }

        private void DeleteForwardBirthDateDigits()
        {
            var currentDigits = GetBirthDateDigits();

            if (BirthDateTextBox.Text == BirthDatePlaceholder)
                return;

            var caret = GetDigitPosition(BirthDateTextBox.CaretIndex);
            var selectionStart = GetDigitPosition(
                BirthDateTextBox.SelectionStart);

            var selectionLength = CountDigits(
                BirthDateTextBox.SelectedText);

            if (selectionLength > 0)
            {
                currentDigits = currentDigits.Remove(
                    selectionStart,
                    Math.Min(selectionLength,
                        currentDigits.Length - selectionStart));

                caret = selectionStart;
            }
            else if (caret < currentDigits.Length)
            {
                currentDigits = currentDigits.Remove(caret, 1);
            }

            SetBirthDateDigits(currentDigits, caret);
        }

        private string GetBirthDateDigits()
        {
            if (BirthDateTextBox.Text == BirthDatePlaceholder)
                return string.Empty;

            return new string(
                BirthDateTextBox.Text.Where(char.IsDigit).ToArray());
        }

        private void SetBirthDateDigits(string digits, int caretPosition)
        {
            _isUpdatingBirthDate = true;

            var formatted = FormatBirthDate(digits);

            BirthDateTextBox.Text = formatted;
            BirthDateTextBox.Foreground =
                new SolidColorBrush(Color.FromRgb(248, 250, 252));

            BirthDateTextBox.CaretIndex =
                GetFormattedPosition(formatted, caretPosition);

            _isUpdatingBirthDate = false;
        }

        private static string FormatBirthDate(string digits)
        {
            if (digits.Length <= 2)
                return digits;

            if (digits.Length <= 4)
                return $"{digits[..2]}.{digits[2..]}";

            return $"{digits[..2]}.{digits.Substring(2, 2)}.{digits[4..]}";
        }

        private static int GetDigitPosition(int formattedPosition)
        {
            var position = 0;

            for (var i = 0; i < Math.Min(formattedPosition, 10); i++)
            {
                if (i != 2 && i != 5)
                    position++;
            }

            return Math.Min(position, 8);
        }

        private static int GetFormattedPosition(
            string formatted,
            int digitPosition)
        {
            if (digitPosition <= 0)
                return 0;

            var digits = 0;

            for (var i = 0; i < formatted.Length; i++)
            {
                if (formatted[i] == '.')
                    continue;

                digits++;

                if (digits == digitPosition)
                    return i + 1;
            }

            return formatted.Length;
        }

        private static int CountDigits(string? value)
        {
            return value?.Count(char.IsDigit) ?? 0;
        }

        private void LoginButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            LoginRequested?.Invoke();
        }

        private void ShowError(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Visibility = Visibility.Visible;
        }
    }
}
