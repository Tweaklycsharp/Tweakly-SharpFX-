using System;
using System.Windows;
using System.Windows.Threading;

namespace WeAnim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                // Add handlers for unhandled exceptions
                this.DispatcherUnhandledException += App_DispatcherUnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                
                // Log application startup for debugging
                Console.WriteLine("WeAnim application initializing...");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during application initialization: {ex.Message}", 
                    "Initialization Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                
                // Material Design settings are configured in App.xaml
                Console.WriteLine("WeAnim application starting...");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during application startup: {ex.Message}", 
                    "Startup Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                // Ignore specific animation errors that don't affect functionality
                if (e.Exception.ToString().Contains("System.Windows.Media.Animation.DoubleAnimation") &&
                    (e.Exception.ToString().Contains("No target was specified") || 
                     e.Exception.ToString().Contains("Aucune cible n'a été spécifiée")))
                {
                    // Simply mark the exception as handled without displaying a message
                    e.Handled = true;
                    Console.WriteLine("Non-critical animation error suppressed");
                    return;
                }
                
                // For other errors, display a message
                MessageBox.Show($"An unhandled error occurred: {e.Exception.Message}\n\nDetails: {e.Exception.StackTrace}",
                    "Application Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                
                // Mark the exception as handled to prevent application closure
                e.Handled = true;
            }
            catch (Exception ex)
            {
                // Failsafe for errors in the exception handler itself
                MessageBox.Show($"Error in exception handler: {ex.Message}", 
                    "Critical Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"A fatal error occurred: {ex.Message}\n\nDetails: {ex.StackTrace}",
                        "Fatal Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("An unknown fatal error occurred.",
                        "Fatal Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch
            {
                // Last resort error handling
                MessageBox.Show("A critical system error occurred that could not be processed.",
                    "System Failure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
