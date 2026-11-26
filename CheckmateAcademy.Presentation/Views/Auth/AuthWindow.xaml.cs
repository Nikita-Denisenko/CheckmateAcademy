
using CheckmateAcademy.Application.Interfaces.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace CheckmateAcademy.Presentation.Views.Auth
{
    public partial class AuthWindow : Window
    {
        private readonly IAuthService _authService;
        private bool _isSwitching;

        public AuthWindow(IAuthService authService)
        {
            InitializeComponent();

            _authService = authService;

            ShowLoginView(false);
        }

        private void AuthWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFloatingPiecesScale();

            AnimateIn(HeaderPanel, 0, 0, -18);
            AnimateOpacity(DecorationPanel, 100, 900);
            AnimateIn(IntroPanel, 160, -35, 12);
            AnimateIn(LoginHost, 320, 35, 8, true);
            AnimateOpacity(FloatingPiecesPanel, 250, 900);
        }

        private void AuthWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateFloatingPiecesScale();
        }

        private void UpdateFloatingPiecesScale()
        {
            if (FloatingPiecesScale == null)
            {
                return;
            }

            var scale = Math.Clamp(ActualWidth / 1440d, 1d, 1.3d);

            FloatingPiecesScale.ScaleX = scale;
            FloatingPiecesScale.ScaleY = scale;
        }

        private void ShowLoginView(bool animate = true)
        {
            var loginView = new LoginView(_authService);

            loginView.AuthenticationSucceeded += OnAuthenticationSucceeded;
            loginView.RegisterRequested += ShowRegisterView;

            SwitchView(loginView, animate);
        }

        private void ShowRegisterView()
        {
            var registerView = new RegisterView(_authService);

            registerView.AuthenticationSucceeded += OnAuthenticationSucceeded;
            registerView.LoginRequested += () => ShowLoginView();

            SwitchView(registerView);
        }

        private void SwitchView(UserControl nextView, bool animate = true)
        {
            if (_isSwitching)
            {
                return;
            }

            _isSwitching = true;

            if (!animate || LoginHost.Children.Count == 0)
            {
                LoginHost.Children.Clear();
                LoginHost.Children.Add(nextView);

                nextView.Opacity = 1;
                _isSwitching = false;

                return;
            }

            var currentView = LoginHost.Children[0] as FrameworkElement;

            if (currentView == null)
            {
                LoginHost.Children.Clear();
                LoginHost.Children.Add(nextView);
                _isSwitching = false;

                return;
            }

            var fadeOut = new DoubleAnimation(
                0,
                TimeSpan.FromMilliseconds(180))
            {
                EasingFunction = new CubicEase
                {
                    EasingMode = EasingMode.EaseIn
                }
            };

            fadeOut.Completed += (_, _) =>
            {
                LoginHost.Children.Clear();

                nextView.Opacity = 0;
                LoginHost.Children.Add(nextView);

                var fadeIn = new DoubleAnimation(
                    0,
                    1,
                    TimeSpan.FromMilliseconds(260))
                {
                    EasingFunction = new CubicEase
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                };

                fadeIn.Completed += (_, _) =>
                {
                    _isSwitching = false;
                };

                nextView.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };

            currentView.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void AnimateIn(
            FrameworkElement element,
            int delay,
            double offsetX,
            double offsetY,
            bool scale = false)
        {
            var translate = new TranslateTransform(offsetX, offsetY);
            var transformGroup = new TransformGroup();

            transformGroup.Children.Add(translate);

            ScaleTransform scaleTransform = null;

            if (scale)
            {
                scaleTransform = new ScaleTransform(0.97, 0.97);
                transformGroup.Children.Add(scaleTransform);
            }

            element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = transformGroup;
            element.Opacity = 0;

            var easing = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };

            var duration = TimeSpan.FromMilliseconds(700);
            var beginTime = TimeSpan.FromMilliseconds(delay);

            element.BeginAnimation(
                UIElement.OpacityProperty,
                new DoubleAnimation(0, 1, duration)
                {
                    BeginTime = beginTime,
                    EasingFunction = easing
                });

            translate.BeginAnimation(
                TranslateTransform.XProperty,
                new DoubleAnimation(offsetX, 0, duration)
                {
                    BeginTime = beginTime,
                    EasingFunction = easing
                });

            translate.BeginAnimation(
                TranslateTransform.YProperty,
                new DoubleAnimation(offsetY, 0, duration)
                {
                    BeginTime = beginTime,
                    EasingFunction = easing
                });

            if (scaleTransform != null)
            {
                scaleTransform.BeginAnimation(
                    ScaleTransform.ScaleXProperty,
                    new DoubleAnimation(0.97, 1, duration)
                    {
                        BeginTime = beginTime,
                        EasingFunction = easing
                    });

                scaleTransform.BeginAnimation(
                    ScaleTransform.ScaleYProperty,
                    new DoubleAnimation(0.97, 1, duration)
                    {
                        BeginTime = beginTime,
                        EasingFunction = easing
                    });
            }
        }

        private void AnimateOpacity(
            FrameworkElement element,
            int delay,
            int duration)
        {
            element.Opacity = 0;

            element.BeginAnimation(
                UIElement.OpacityProperty,
                new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(duration))
                {
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    EasingFunction = new CubicEase
                    {
                        EasingMode = EasingMode.EaseOut
                    }
                });
        }

        private void FeatureCard_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border card)
            {
                AnimateFeatureCard(card, true);
            }
        }

        private void FeatureCard_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border card)
            {
                AnimateFeatureCard(card, false);
            }
        }

        private void FloatingBubble_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border bubble)
            {
                AnimateFloatingBubble(bubble, true);
            }
        }

        private void FloatingBubble_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border bubble)
            {
                AnimateFloatingBubble(bubble, false);
            }
        }

        private static void AnimateFeatureCard(Border card, bool isHovered)
        {
            var duration = TimeSpan.FromMilliseconds(650);
            var easing = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };

            AnimateBrush(
                card,
                Border.BackgroundProperty,
                isHovered
                    ? Color.FromRgb(23, 40, 68)
                    : Color.FromRgb(17, 30, 51),
                duration,
                easing);

            AnimateBrush(
                card,
                Border.BorderBrushProperty,
                isHovered
                    ? Color.FromRgb(65, 117, 181)
                    : Color.FromRgb(38, 59, 91),
                duration,
                easing);

            AnimateShadow(
                card,
                isHovered ? 19 : 14,
                isHovered ? 0.11 : 0.035,
                duration,
                easing);
        }

        private static void AnimateFloatingBubble(Border bubble, bool isHovered)
        {
            var duration = TimeSpan.FromMilliseconds(550);
            var easing = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };

            AnimateBrush(
                bubble,
                Border.BackgroundProperty,
                isHovered
                    ? Color.FromRgb(27, 51, 82)
                    : Color.FromRgb(20, 36, 59),
                duration,
                easing);

            AnimateBrush(
                bubble,
                Border.BorderBrushProperty,
                isHovered
                    ? Color.FromRgb(96, 165, 250)
                    : Color.FromRgb(48, 75, 112),
                duration,
                easing);

            AnimateShadow(
                bubble,
                isHovered ? 29 : 13,
                isHovered ? 0.32 : 0.08,
                duration,
                easing);

            if (bubble.Child is TextBlock piece)
            {
                AnimateBrush(
                    piece,
                    TextBlock.ForegroundProperty,
                    isHovered
                        ? Color.FromRgb(219, 234, 254)
                        : Color.FromRgb(140, 191, 255),
                    duration,
                    easing);
            }
        }

        private static void AnimateShadow(
            Border element,
            double blurRadius,
            double opacity,
            TimeSpan duration,
            IEasingFunction easing)
        {
            var shadow = element.Effect is DropShadowEffect currentShadow
                ? currentShadow.CloneCurrentValue()
                : new DropShadowEffect
                {
                    Color = Color.FromRgb(59, 130, 246),
                    ShadowDepth = 0
                };

            element.Effect = shadow;

            shadow.BeginAnimation(
                DropShadowEffect.BlurRadiusProperty,
                new DoubleAnimation(shadow.BlurRadius, blurRadius, duration)
                {
                    EasingFunction = easing
                });

            shadow.BeginAnimation(
                DropShadowEffect.OpacityProperty,
                new DoubleAnimation(shadow.Opacity, opacity, duration)
                {
                    EasingFunction = easing
                });
        }

        private static void AnimateBrush(
            DependencyObject element,
            DependencyProperty property,
            Color targetColor,
            TimeSpan duration,
            IEasingFunction easing)
        {
            var currentBrush = element.GetValue(property) as SolidColorBrush;

            var brush = currentBrush != null
                ? currentBrush.CloneCurrentValue()
                : new SolidColorBrush(targetColor);

            element.SetValue(property, brush);

            brush.BeginAnimation(
                SolidColorBrush.ColorProperty,
                new ColorAnimation(targetColor, duration)
                {
                    EasingFunction = easing
                });
        }

        private void WindowRoot_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is not DependencyObject source)
            {
                return;
            }

            if (FindAncestor<Control>(source) is TextBoxBase
                or PasswordBox)
            {
                return;
            }

            Keyboard.ClearFocus();
        }

        private static T FindAncestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T result)
                {
                    return result;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        private void OnAuthenticationSucceeded()
        {
            DialogResult = true;
        }
    }
}
