using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using Microsoft.Xaml.Behaviors;

namespace WeAnim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Timers for animations
        private DispatcherTimer? _particleTimer;
        private DispatcherTimer? _waveTimer;
        private DispatcherTimer? _neonTimer;
        
        // Random for generating random values
        private Random _random = new Random();
        
        // Current active page
        private UIElement? _currentPage;

        public MainWindow()
        {
            try
            {
                // Debug message at startup
                MessageBox.Show("Starting WeAnim application...", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);
                
                InitializeComponent();
                
                // Safe initialization
                this.Loaded += MainWindow_Loaded;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing main window: {ex.Message}\n\nDetails: {ex.StackTrace}", 
                    "Initialization Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set current page with null checks
                if (HomePage != null)
                {
                    _currentPage = HomePage;
                    HomePage.Visibility = Visibility.Visible;
                    
                    // Ensure other pages are hidden
                    if (AnimationsPage != null) AnimationsPage.Visibility = Visibility.Collapsed;
                    if (EffectsPage != null) EffectsPage.Visibility = Visibility.Collapsed;
                    if (AboutPage != null) AboutPage.Visibility = Visibility.Collapsed;
                }
                
                // Initialize timers
                InitializeTimers();
                
                // Add mouse events to animation canvas
                if (AnimationCanvas != null)
                {
                    AnimationCanvas.MouseDown += AnimationCanvas_MouseDown;
                    AnimationCanvas.MouseMove += AnimationCanvas_MouseMove;
                }
                
                // Les particules ont été supprimées, donc on n'a plus besoin de démarrer leur animation
                // Ancien code commenté pour référence:
                // if (Particle1 != null && Particle2 != null && Particle3 != null && 
                //     Particle4 != null && Particle5 != null && AnimationCanvas != null)
                // {
                //     EnsureRenderTransforms();
                //     StartParticleAnimation();
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading window: {ex.Message}\n\nDetails: {ex.StackTrace}", 
                    "Loading Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        #region Navigation

        // Method to handle navigation to Home page
        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            try
            {
                if (HomePage != null && _currentPage != HomePage)
                {
                    // Hide current page
                    if (_currentPage != null)
                        _currentPage.Visibility = Visibility.Collapsed;
                    
                    // Show home page
                    HomePage.Visibility = Visibility.Visible;
                    _currentPage = HomePage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error navigating to Home: {ex.Message}");
            }
        }

        // Method to handle navigation to Animations page
        private void NavigateToAnimations(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AnimationsPage != null && _currentPage != AnimationsPage)
                {
                    // Hide current page
                    if (_currentPage != null)
                        _currentPage.Visibility = Visibility.Collapsed;
                    
                    // Show animations page
                    AnimationsPage.Visibility = Visibility.Visible;
                    _currentPage = AnimationsPage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error navigating to Animations: {ex.Message}");
            }
        }

        // Method to handle navigation to Effects page
        private void NavigateToEffects(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EffectsPage != null && _currentPage != EffectsPage)
                {
                    // Hide current page
                    if (_currentPage != null)
                        _currentPage.Visibility = Visibility.Collapsed;
                    
                    // Show effects page
                    EffectsPage.Visibility = Visibility.Visible;
                    _currentPage = EffectsPage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error navigating to Effects: {ex.Message}");
            }
        }

        // Method to handle navigation to About page
        private void NavigateToAbout(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AboutPage != null && _currentPage != AboutPage)
                {
                    // Hide current page
                    if (_currentPage != null)
                        _currentPage.Visibility = Visibility.Collapsed;
                    
                    // Show about page
                    AboutPage.Visibility = Visibility.Visible;
                    _currentPage = AboutPage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error navigating to About: {ex.Message}");
            }
        }

        #endregion

        #region Window Controls

        // Method to handle window dragging
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error dragging window: {ex.Message}");
            }
        }

        // Method to minimize window
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error minimizing window: {ex.Message}");
            }
        }

        // Method to maximize/restore window
        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error maximizing window: {ex.Message}");
            }
        }

        // Method to close window
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing window: {ex.Message}");
            }
        }

        #endregion

        #region Animation Triggers
        
        private void TriggerFadeAnimation(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            if (sender is Button button)
            {
                // Create fade out animation
                DoubleAnimation fadeOut = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                
                // Create fade in animation
                DoubleAnimation fadeIn = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                
                // Set up the completed event for fade out
                fadeOut.Completed += (s, args) =>
                {
                    // Change the button content
                    button.Content = button.Content.ToString() == "Click me" ? "Fade Effect!" : "Click me";
                    
                    // Start fade in animation
                    button.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                };
                
                // Start fade out animation
                button.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            }
        }

        private void TriggerSlideAnimation(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            if (sender is Button button)
            {
                // Create slide out animation
                ThicknessAnimation slideOut = new ThicknessAnimation
                {
                    From = new Thickness(0),
                    To = new Thickness(300, 0, -300, 0),
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                
                // Create slide in animation
                ThicknessAnimation slideIn = new ThicknessAnimation
                {
                    From = new Thickness(-300, 0, 300, 0),
                    To = new Thickness(0),
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                
                // Set up the completed event for slide out
                slideOut.Completed += (s, args) =>
                {
                    // Change the button content
                    button.Content = button.Content.ToString() == "Click me" ? "Slide Effect!" : "Click me";
                    
                    // Start slide in animation
                    button.BeginAnimation(Button.MarginProperty, slideIn);
                };
                
                // Start slide out animation
                button.BeginAnimation(Button.MarginProperty, slideOut);
            }
        }
        
        #endregion

        #region Particle Animation
        
        private void InitializeTimers()
        {
            try
            {
                // Initialize particle timer
                _particleTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(50)
                };
                _particleTimer.Tick += ParticleTimer_Tick;
                
                // Note: Wave and Neon animations now use Storyboards instead of timers
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing timers: {ex.Message}", "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void StartParticleAnimation()
        {
            // Cette méthode est maintenant vide car les particules ont été supprimées
            try
            {
                // Les particules ont été supprimées du XAML, donc cette méthode ne fait plus rien
                // _particleTimer?.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du démarrage de l'animation des particules : {ex.Message}");
            }
        }

        private void AnimateParticle(UIElement element, double speedX, double speedY)
        {
            // Cette méthode est maintenant vide car les particules ont été supprimées
            // Le code a été retiré car les particules n'existent plus dans le XAML
        }
        
        private void ParticleTimer_Tick(object? sender, EventArgs e)
        {
            // Cette méthode est conservée mais vidée car les particules ont été supprimées
            // Les références aux particules ont été retirées du XAML
            try
            {
                // Ancien code commenté pour référence:
                // if (Particle1 != null) UpdateParticlePosition(Particle1);
                // if (Particle2 != null) UpdateParticlePosition(Particle2);
                // if (Particle3 != null) UpdateParticlePosition(Particle3);
                // if (Particle4 != null) UpdateParticlePosition(Particle4);
                // if (Particle5 != null) UpdateParticlePosition(Particle5);
            }
            catch (Exception ex)
            {
                _particleTimer?.Stop();
                Console.WriteLine($"Erreur lors de l'animation des particules : {ex.Message}");
            }
        }
        
        private void UpdateParticlePosition(UIElement particle)
        {
            // Get the particle's properties
            if (particle is FrameworkElement element && element.Tag is ParticleProperties props && AnimationCanvas != null)
            {
                // Get the current position
                double left = Canvas.GetLeft(particle);
                double top = Canvas.GetTop(particle);
                
                // Calculate new position
                double newLeft = left + props.SpeedX;
                double newTop = top + props.SpeedY;
                
                // Check if the particle hit the boundaries
                double width = 0;
                if (particle is Ellipse ellipse)
                {
                    width = ellipse.Width;
                }
                
                if (newLeft < 0 || newLeft > AnimationCanvas.ActualWidth - width)
                {
                    props.SpeedX = -props.SpeedX;
                    newLeft = left + props.SpeedX;
                }
                
                if (newTop < 0 || newTop > AnimationCanvas.ActualHeight - width)
                {
                    props.SpeedY = -props.SpeedY;
                    newTop = top + props.SpeedY;
                }
                
                // Update the position
                Canvas.SetLeft(particle, newLeft);
                Canvas.SetTop(particle, newTop);
            }
        }
        
        private void AnimationCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (AnimationCanvas == null) return;
            
            // Get the click position
            Point position = e.GetPosition(AnimationCanvas);
            
            // Create a new particle
            Ellipse newParticle = new Ellipse
            {
                Width = _random.Next(10, 40),
                Height = _random.Next(10, 40),
                Fill = GetRandomBrush(),
                Opacity = 0.8
            };
            
            // Set the position
            Canvas.SetLeft(newParticle, position.X - newParticle.Width / 2);
            Canvas.SetTop(newParticle, position.Y - newParticle.Height / 2);
            
            // Add the particle to the canvas
            AnimationCanvas.Children.Add(newParticle);
            
            // Animate the particle
            AnimateParticle(newParticle, _random.Next(-5, 5), _random.Next(-5, 5));
        }
        
        private void AnimationCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (AnimationCanvas == null) return;
            
            // Only create particles on mouse down
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Get the position
                Point position = e.GetPosition(AnimationCanvas);
                
                // Create a new particle
                Ellipse newParticle = new Ellipse
                {
                    Width = _random.Next(5, 20),
                    Height = _random.Next(5, 20),
                    Fill = GetRandomBrush(),
                    Opacity = 0.6
                };
                
                // Set the position
                Canvas.SetLeft(newParticle, position.X - newParticle.Width / 2);
                Canvas.SetTop(newParticle, position.Y - newParticle.Height / 2);
                
                // Add the particle to the canvas
                AnimationCanvas.Children.Add(newParticle);
                
                // Animate the particle
                AnimateParticle(newParticle, _random.Next(-3, 3), _random.Next(-3, 3));
                
                // Fade out and remove the particle after a delay
                DispatcherTimer removeTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(2)
                };
                
                removeTimer.Tick += (s, args) =>
                {
                    // Create fade out animation
                    DoubleAnimation fadeOut = new DoubleAnimation
                    {
                        From = newParticle.Opacity,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(1)
                    };
                    
                    // Set up the completed event
                    fadeOut.Completed += (sender, eventArgs) =>
                    {
                        // Remove the particle
                        AnimationCanvas.Children.Remove(newParticle);
                    };
                    
                    // Start the animation
                    newParticle.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                    
                    // Stop the timer
                    removeTimer.Stop();
                };
                
                // Start the timer
                removeTimer.Start();
            }
        }
        
        // Méthode pour s'assurer que toutes les particules ont une RenderTransform
        private void EnsureRenderTransforms()
        {
            // Cette méthode est maintenant vide car les particules ont été supprimées
            try
            {
                // Les particules ont été supprimées du XAML, donc cette méthode ne fait plus rien
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'initialisation des transformations : {ex.Message}");
            }
        }
        
        // Crée un TransformGroup avec les transformations nécessaires
        private TransformGroup CreateDefaultTransformGroup()
        {
            TransformGroup group = new TransformGroup();
            group.Children.Add(new ScaleTransform(1, 1));
            group.Children.Add(new RotateTransform(0));
            group.Children.Add(new TranslateTransform(0, 0));
            return group;
        }
        
        #endregion

        #region Visual Effects
        
        private void ShowParticleEffect(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EffectsContainer != null)
                {
                    // Clear previous effects
                    EffectsContainer.Children.Clear();

                    // Create particle effect
                    Canvas particleCanvas = new Canvas();
                    particleCanvas.Background = Brushes.Transparent;
                    EffectsContainer.Children.Add(particleCanvas);

                    // Add random particles
                    for (int i = 0; i < 50; i++)
                    {
                        Ellipse particle = new Ellipse();
                        particle.Width = _random.Next(5, 15);
                        particle.Height = particle.Width;
                        
                        // Random color
                        Color color = Color.FromRgb(
                            (byte)_random.Next(100, 255),
                            (byte)_random.Next(100, 255),
                            (byte)_random.Next(100, 255));
                        
                        particle.Fill = new SolidColorBrush(color);
                        
                        // Random position
                        Canvas.SetLeft(particle, _random.Next(0, (int)EffectsContainer.ActualWidth));
                        Canvas.SetTop(particle, _random.Next(0, (int)EffectsContainer.ActualHeight));
                        
                        // Add to canvas
                        particleCanvas.Children.Add(particle);
                        
                        // Animate
                        DoubleAnimation moveX = new DoubleAnimation
                        {
                            From = Canvas.GetLeft(particle),
                            To = _random.Next(0, (int)EffectsContainer.ActualWidth),
                            Duration = TimeSpan.FromSeconds(_random.Next(3, 8)),
                            AutoReverse = true,
                            RepeatBehavior = RepeatBehavior.Forever
                        };
                        
                        DoubleAnimation moveY = new DoubleAnimation
                        {
                            From = Canvas.GetTop(particle),
                            To = _random.Next(0, (int)EffectsContainer.ActualHeight),
                            Duration = TimeSpan.FromSeconds(_random.Next(3, 8)),
                            AutoReverse = true,
                            RepeatBehavior = RepeatBehavior.Forever
                        };
                        
                        moveX.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };
                        moveY.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };
                        
                        Storyboard sb = new Storyboard();
                        Storyboard.SetTarget(moveX, particle);
                        Storyboard.SetTarget(moveY, particle);
                        
                        Storyboard.SetTargetProperty(moveX, new PropertyPath("(Canvas.Left)"));
                        Storyboard.SetTargetProperty(moveY, new PropertyPath("(Canvas.Top)"));
                        
                        sb.Children.Add(moveX);
                        sb.Children.Add(moveY);
                        sb.Begin();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing particle effect: {ex.Message}", "Effect Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowWaveEffect(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EffectsContainer != null)
                {
                    // Clear previous effects
                    EffectsContainer.Children.Clear();

                    // Create wave container
                    Canvas waveCanvas = new Canvas();
                    waveCanvas.Background = Brushes.Transparent;
                    EffectsContainer.Children.Add(waveCanvas);

                    // Create wave lines
                    for (int i = 0; i < 10; i++)
                    {
                        Polyline waveLine = new Polyline();
                        waveLine.Stroke = new SolidColorBrush(Color.FromArgb(
                            (byte)(150 - i * 10),
                            (byte)_random.Next(0, 100),
                            (byte)_random.Next(100, 255),
                            (byte)_random.Next(200, 255)));
                        waveLine.StrokeThickness = 2;
                        
                        // Create wave points
                        PointCollection points = new PointCollection();
                        double height = EffectsContainer.ActualHeight;
                        double width = EffectsContainer.ActualWidth;
                        
                        for (int x = 0; x < width; x += 5)
                        {
                            double y = height / 2 + Math.Sin(x * 0.03 + i) * 50;
                            points.Add(new Point(x, y));
                        }
                        
                        waveLine.Points = points;
                        
                        // Add to canvas with offset
                        Canvas.SetTop(waveLine, i * 15);
                        waveCanvas.Children.Add(waveLine);
                        
                        // Animate wave
                        DoubleAnimation waveAnim = new DoubleAnimation
                        {
                            From = 0,
                            To = 20,
                            Duration = TimeSpan.FromSeconds(2 + i * 0.5),
                            AutoReverse = true,
                            RepeatBehavior = RepeatBehavior.Forever
                        };
                        
                        TranslateTransform transform = new TranslateTransform();
                        waveLine.RenderTransform = transform;
                        
                        transform.BeginAnimation(TranslateTransform.YProperty, waveAnim);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing wave effect: {ex.Message}", "Effect Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowNeonEffect(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EffectsContainer != null)
                {
                    // Clear previous effects
                    EffectsContainer.Children.Clear();
                    
                    // Create neon container
                    Canvas neonCanvas = new Canvas();
                    neonCanvas.Background = new SolidColorBrush(Colors.Black);
                    EffectsContainer.Children.Add(neonCanvas);
                    
                    // Create neon text
                    TextBlock neonText = new TextBlock
                    {
                        Text = "NEON EFFECT",
                        FontSize = 48,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    
                    // Add glow effect
                    neonText.Effect = new DropShadowEffect
                    {
                        Color = Colors.Cyan,
                        Direction = 0,
                        ShadowDepth = 0,
                        BlurRadius = 20,
                        Opacity = 1
                    };
                    
                    // Set foreground with gradient
                    LinearGradientBrush gradientBrush = new LinearGradientBrush();
                    gradientBrush.StartPoint = new Point(0, 0);
                    gradientBrush.EndPoint = new Point(0, 1);
                    gradientBrush.GradientStops.Add(new GradientStop(Colors.Cyan, 0.0));
                    gradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.5));
                    gradientBrush.GradientStops.Add(new GradientStop(Colors.Purple, 1.0));
                    
                    neonText.Foreground = gradientBrush;
                    
                    // Center in canvas
                    Canvas.SetLeft(neonText, (EffectsContainer.ActualWidth - neonText.ActualWidth) / 2);
                    Canvas.SetTop(neonText, (EffectsContainer.ActualHeight - neonText.ActualHeight) / 2);
                    neonCanvas.Children.Add(neonText);
                    
                    // Add animation to glow effect
                    DoubleAnimation glowAnimation = new DoubleAnimation
                    {
                        From = 10,
                        To = 30,
                        Duration = TimeSpan.FromSeconds(1.5),
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever
                    };
                    
                    glowAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };
                    
                    Storyboard sb = new Storyboard();
                    Storyboard.SetTarget(glowAnimation, neonText);
                    Storyboard.SetTargetProperty(glowAnimation, new PropertyPath("Effect.BlurRadius"));
                    
                    sb.Children.Add(glowAnimation);
                    sb.Begin();
                    
                    // Add random neon lines
                    for (int i = 0; i < 10; i++)
                    {
                        Line neonLine = new Line();
                        neonLine.X1 = _random.Next(0, (int)EffectsContainer.ActualWidth);
                        neonLine.Y1 = _random.Next(0, (int)EffectsContainer.ActualHeight);
                        neonLine.X2 = _random.Next(0, (int)EffectsContainer.ActualWidth);
                        neonLine.Y2 = _random.Next(0, (int)EffectsContainer.ActualHeight);
                        
                        // Random color
                        Color lineColor = Color.FromRgb(
                            (byte)_random.Next(150, 255),
                            (byte)_random.Next(150, 255),
                            (byte)_random.Next(150, 255));
                        
                        neonLine.Stroke = new SolidColorBrush(lineColor);
                        neonLine.StrokeThickness = _random.Next(1, 3);
                        
                        // Add glow effect
                        neonLine.Effect = new DropShadowEffect
                        {
                            Color = lineColor,
                            Direction = 0,
                            ShadowDepth = 0,
                            BlurRadius = 10,
                            Opacity = 0.7
                        };
                        
                        neonCanvas.Children.Add(neonLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing neon effect: {ex.Message}", "Effect Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Demo

        private void StartDemo(object sender, RoutedEventArgs e)
        {
            try
            {
                // Navigate to animations page
                NavigateToAnimations(sender, e);
                
                // Show demo message
                MessageBox.Show("Welcome to the WeAnim demo!\n\nThis demonstration will showcase various animations and visual effects available in this application.", 
                    "WeAnim Demo", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting demo: {ex.Message}");
            }
        }

        #endregion

        #region Animation Button Event Handlers
        
        private void FadeButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Masquer la page Animations
                if (AnimationsPage != null)
                {
                    AnimationsPage.Visibility = Visibility.Collapsed;
                }
                
                // Create a demo element to animate
                Border demoElement = new Border
                {
                    Width = 200,
                    Height = 200,
                    Background = new SolidColorBrush(Color.FromRgb(0, 184, 169)), // Teal color
                    CornerRadius = new CornerRadius(8),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                TextBlock text = new TextBlock
                {
                    Text = "Fade Animation",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                Grid container = new Grid();
                container.Name = "AnimationContainer"; // Donner un nom au container pour le retrouver facilement
                container.Width = AnimationCanvas.ActualWidth;
                container.Height = AnimationCanvas.ActualHeight;
                container.Children.Add(demoElement);
                container.Children.Add(text);
                
                // Add a return button
                Button returnButton = new Button
                {
                    Content = "Back",
                    Style = (Style)FindResource("ModernButton"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 0, 20),
                    Width = 100,
                    Height = 35
                };
                returnButton.Click += ReturnButton_Click;
                container.Children.Add(returnButton);
                
                // Clear previous demo
                if (AnimationCanvas != null)
                {
                    // Remove any previous animations
                    var previousAnimations = AnimationCanvas.Children.OfType<Grid>()
                        .Where(g => g.Name == "AnimationContainer")
                        .ToList();
                    
                    foreach (var animation in previousAnimations)
                    {
                        AnimationCanvas.Children.Remove(animation);
                    }
                    
                    // Add the new animation
                    AnimationCanvas.Children.Add(container);
                    
                    // Create and start the animation
                    DoubleAnimation fadeAnimation = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(1.5),
                        AutoReverse = true,
                        RepeatBehavior = new RepeatBehavior(3)
                    };
                    
                    fadeAnimation.Completed += (s, args) =>
                    {
                        // Do not automatically remove the demo element when animation completes
                        // Let the user click the return button instead
                    };
                    
                    demoElement.BeginAnimation(UIElement.OpacityProperty, fadeAnimation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in fade animation: {ex.Message}", "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Trouver le bouton qui a été cliqué
                Button button = sender as Button;
                if (button == null) return;
                
                // Trouver le container parent du bouton
                Grid container = FindParent<Grid>(button);
                if (container == null) return;
                
                // Supprimer le container de l'AnimationCanvas
                if (AnimationCanvas != null)
                {
                    AnimationCanvas.Children.Remove(container);
                }
                
                // Afficher la page Animations
                if (AnimationsPage != null)
                {
                    AnimationsPage.Visibility = Visibility.Visible;
                    _currentPage = AnimationsPage;
                }
                
                // S'assurer que tous les boutons sont activés
                if (FadeButton != null) FadeButton.IsEnabled = true;
                if (SlideButton != null) SlideButton.IsEnabled = true;
                if (ScaleButton != null) ScaleButton.IsEnabled = true;
                if (RotateButton != null) RotateButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning to animations: {ex.Message}", "Navigation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void SlideButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Masquer la page Animations
                if (AnimationsPage != null)
                {
                    AnimationsPage.Visibility = Visibility.Collapsed;
                }
                
                // Create a demo element to animate
                Border demoElement = new Border
                {
                    Width = 200,
                    Height = 200,
                    Background = new SolidColorBrush(Color.FromRgb(255, 191, 0)), // Amber color
                    CornerRadius = new CornerRadius(8),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                TextBlock text = new TextBlock
                {
                    Text = "Slide Animation",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                Grid container = new Grid();
                container.Name = "AnimationContainer"; // Donner un nom au container pour le retrouver facilement
                container.Width = AnimationCanvas.ActualWidth;
                container.Height = AnimationCanvas.ActualHeight;
                
                // Créer un sous-container pour l'animation qui sera animé
                Grid animatedContainer = new Grid();
                animatedContainer.Children.Add(demoElement);
                animatedContainer.Children.Add(text);
                animatedContainer.HorizontalAlignment = HorizontalAlignment.Center;
                animatedContainer.VerticalAlignment = VerticalAlignment.Center;
                
                // Ajouter le sous-container au container principal
                container.Children.Add(animatedContainer);
                
                // Add a return button (fixed position, not affected by animation)
                Button returnButton = new Button
                {
                    Content = "Back",
                    Style = (Style)FindResource("ModernButton"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 0, 20),
                    Width = 100,
                    Height = 35
                };
                returnButton.Click += ReturnButton_Click;
                container.Children.Add(returnButton);
                
                // Clear previous demo
                if (AnimationCanvas != null)
                {
                    // Remove any previous animations
                    var previousAnimations = AnimationCanvas.Children.OfType<Grid>()
                        .Where(g => g.Name == "AnimationContainer")
                        .ToList();
                    
                    foreach (var animation in previousAnimations)
                    {
                        AnimationCanvas.Children.Remove(animation);
                    }
                    
                    // Add the new animation
                    AnimationCanvas.Children.Add(container);
                    
                    // Set initial transform
                    TranslateTransform transform = new TranslateTransform(-300, 0);
                    animatedContainer.RenderTransform = transform;
                    
                    // Create and start the animation
                    DoubleAnimation slideAnimation = new DoubleAnimation
                    {
                        From = -300,
                        To = 0,
                        Duration = TimeSpan.FromSeconds(1),
                        AutoReverse = true,
                        RepeatBehavior = new RepeatBehavior(2)
                    };
                    
                    slideAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut };
                    
                    slideAnimation.Completed += (s, args) =>
                    {
                        // Do not automatically remove the demo element when animation completes
                        // Let the user click the return button instead
                    };
                    
                    transform.BeginAnimation(TranslateTransform.XProperty, slideAnimation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in slide animation: {ex.Message}", "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void ScaleButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Masquer la page Animations
                if (AnimationsPage != null)
                {
                    AnimationsPage.Visibility = Visibility.Collapsed;
                }
                
                // Create a demo element to animate
                Border demoElement = new Border
                {
                    Width = 200,
                    Height = 200,
                    Background = new SolidColorBrush(Color.FromRgb(103, 58, 183)), // Deep Purple color
                    CornerRadius = new CornerRadius(8),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                TextBlock text = new TextBlock
                {
                    Text = "Scale Animation",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                Grid container = new Grid();
                container.Name = "AnimationContainer"; // Donner un nom au container pour le retrouver facilement
                container.Width = AnimationCanvas.ActualWidth;
                container.Height = AnimationCanvas.ActualHeight;
                
                // Créer un sous-container pour l'animation qui sera animé
                Grid animatedContainer = new Grid();
                animatedContainer.Children.Add(demoElement);
                animatedContainer.Children.Add(text);
                animatedContainer.HorizontalAlignment = HorizontalAlignment.Center;
                animatedContainer.VerticalAlignment = VerticalAlignment.Center;
                
                // Ajouter le sous-container au container principal
                container.Children.Add(animatedContainer);
                
                // Add a return button (fixed position, not affected by animation)
                Button returnButton = new Button
                {
                    Content = "Back",
                    Style = (Style)FindResource("ModernButton"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 0, 20),
                    Width = 100,
                    Height = 35
                };
                returnButton.Click += ReturnButton_Click;
                container.Children.Add(returnButton);
                
                // Clear previous demo
                if (AnimationCanvas != null)
                {
                    // Remove any previous animations
                    var previousAnimations = AnimationCanvas.Children.OfType<Grid>()
                        .Where(g => g.Name == "AnimationContainer")
                        .ToList();
                    
                    foreach (var animation in previousAnimations)
                    {
                        AnimationCanvas.Children.Remove(animation);
                    }
                    
                    // Add the new animation
                    AnimationCanvas.Children.Add(container);
                    
                    // Set initial transform
                    ScaleTransform transform = new ScaleTransform(0.5, 0.5);
                    animatedContainer.RenderTransform = transform;
                    
                    // Create and start the animation
                    DoubleAnimation scaleAnimation = new DoubleAnimation
                    {
                        From = 0.5,
                        To = 1.2,
                        Duration = TimeSpan.FromSeconds(1),
                        AutoReverse = true,
                        RepeatBehavior = new RepeatBehavior(2)
                    };
                    
                    scaleAnimation.EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 3, Springiness = 5 };
                    
                    scaleAnimation.Completed += (s, args) =>
                    {
                        // Do not automatically remove the demo element when animation completes
                        // Let the user click the return button instead
                    };
                    
                    transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                    transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in scale animation: {ex.Message}", "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void RotateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Masquer la page Animations
                if (AnimationsPage != null)
                {
                    AnimationsPage.Visibility = Visibility.Collapsed;
                }
                
                // Create a demo element to animate
                Border demoElement = new Border
                {
                    Width = 200,
                    Height = 200,
                    Background = new SolidColorBrush(Color.FromRgb(0, 184, 169)), // Teal color
                    CornerRadius = new CornerRadius(8),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                TextBlock text = new TextBlock
                {
                    Text = "Rotate Animation",
                    Foreground = Brushes.White,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                
                Grid container = new Grid();
                container.Name = "AnimationContainer"; // Donner un nom au container pour le retrouver facilement
                container.Width = AnimationCanvas.ActualWidth;
                container.Height = AnimationCanvas.ActualHeight;
                
                // Créer un sous-container pour l'animation qui sera animé
                Grid animatedContainer = new Grid();
                animatedContainer.Children.Add(demoElement);
                animatedContainer.Children.Add(text);
                animatedContainer.HorizontalAlignment = HorizontalAlignment.Center;
                animatedContainer.VerticalAlignment = VerticalAlignment.Center;
                
                // Ajouter le sous-container au container principal
                container.Children.Add(animatedContainer);
                
                // Add a return button (fixed position, not affected by animation)
                Button returnButton = new Button
                {
                    Content = "Back",
                    Style = (Style)FindResource("ModernButton"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 0, 20),
                    Width = 100,
                    Height = 35
                };
                returnButton.Click += ReturnButton_Click;
                container.Children.Add(returnButton);
                
                // Clear previous demo
                if (AnimationCanvas != null)
                {
                    // Remove any previous animations
                    var previousAnimations = AnimationCanvas.Children.OfType<Grid>()
                        .Where(g => g.Name == "AnimationContainer")
                        .ToList();
                    
                    foreach (var animation in previousAnimations)
                    {
                        AnimationCanvas.Children.Remove(animation);
                    }
                    
                    // Add the new animation
                    AnimationCanvas.Children.Add(container);
                    
                    // Set initial transform
                    RotateTransform transform = new RotateTransform(0);
                    animatedContainer.RenderTransform = transform;
                    
                    // Create and start the animation
                    DoubleAnimation rotateAnimation = new DoubleAnimation
                    {
                        From = 0,
                        To = 360,
                        Duration = TimeSpan.FromSeconds(2),
                        RepeatBehavior = new RepeatBehavior(2)
                    };
                    
                    rotateAnimation.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut };
                    
                    rotateAnimation.Completed += (s, args) =>
                    {
                        // Do not automatically remove the demo element when animation completes
                        // Let the user click the return button instead
                    };
                    
                    transform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in rotate animation: {ex.Message}", "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        #endregion

        #region Helper Classes and Methods
        
        private class ParticleProperties
        {
            public double SpeedX { get; set; }
            public double SpeedY { get; set; }
        }
        
        private class WaveProperties
        {
            public double Phase { get; set; }
            public double Frequency { get; set; }
            public double Speed { get; set; }
        }
        
        private Brush GetRandomBrush()
        {
            // Create a random brush
            return new SolidColorBrush(Color.FromRgb(
                (byte)_random.Next(100, 255),
                (byte)_random.Next(100, 255),
                (byte)_random.Next(100, 255)));
        }
        
        private Color GetRandomNeonColor()
        {
            // Array of neon colors
            Color[] neonColors = new Color[]
            {
                Colors.Cyan,
                Colors.Magenta,
                Colors.Lime,
                Colors.Yellow,
                Colors.OrangeRed,
                Colors.DeepPink,
                Colors.Aqua
            };
            
            // Return a random color
            return neonColors[_random.Next(neonColors.Length)];
        }
        
        #endregion

        // Méthode utilitaire pour trouver un parent d'un type spécifique
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            
            if (parentObject == null) return null;
            
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
