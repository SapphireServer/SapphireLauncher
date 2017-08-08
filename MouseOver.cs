using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SapphireBootWPF
{
    /// <summary>
    /// An attached property provider for setting the content of a control
    /// when the mouse is over top.
    /// </summary>
    public class MouseOver
    {
        /// <summary>
        /// Specifies the ImageSource when the mouse is over the control.
        /// </summary>
        public static readonly DependencyProperty HoverImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "HoverImageSource", typeof(ImageSource), typeof(MouseOver),
                new PropertyMetadata(HoverImageSourcePropertyChanged));

        public static ImageSource GetHoverImageSource(Image target)
        {
            return (ImageSource)target.GetValue(HoverImageSourceProperty);
        }

        public static void SetHoverImageSource(Image target, ImageSource value)
        {
            target.SetValue(HoverImageSourceProperty, value);
        }

        private static void HoverImageSourcePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Image target = d as Image;
            // Remove existing handlers first.
            RemoveHandlers(target);
            // Register handlers with the target.
            System.Diagnostics.Debug.WriteLine(
                "Registering event mouse handlers for " + target.Name,
                "ImageSourcePropertyChanged");
            target.MouseEnter += target_MouseEnter;
            target.MouseLeave += target_MouseLeave;
            target.Unloaded += target_Unloaded;
            System.Diagnostics.Debug.WriteLine(
                "Registered event mouse handlers for " + target.Name,
                "ImageSourcePropertyChanged");
        }

        private static void RemoveHandlers(Image target)
        {
            System.Diagnostics.Debug.WriteLine(
                "Removing event mouse handlers for " + target.Name,
                "RemoveHandlers");
            target.MouseEnter -= target_MouseEnter;
            target.MouseLeave -= target_MouseLeave;
            target.Unloaded -= target_Unloaded;
            System.Diagnostics.Debug.WriteLine(
                "Removed event mouse handlers for " + target.Name,
                "RemoveHandlers");
        }

        static void target_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Restore the original ImageSource.
            Image target = sender as Image;
            target.Source = (ImageSource)target.Tag;
            System.Diagnostics.Debug.WriteLine(
                "Restored image source with original image...",
                "target_MouseLeave");
        }

        static void target_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Save the original ImageSource first.
            Image target = sender as Image;
            target.Tag = target.Source;
            target.Source = GetHoverImageSource(target);
            System.Diagnostics.Debug.WriteLine(
                "Updated image source with MouseOver image...",
                "target_MouseEnter");
        }

        static void target_Unloaded(object sender, RoutedEventArgs e)
        {
            RemoveHandlers(sender as Image);
        }
    }
}
