using System.Windows;
using System.Windows.Controls;


namespace JSON_Editor.Views.UserControls
{
    /// <summary>
    /// Creates an interaction header.
    /// 
    /// This component doesn't use MVVM to keep it simple because its generic.
    /// Also this component doesn't keep information.
    /// 
    /// If needed this can be ported into MVVM to add functionality.
    /// </summary>
    public partial class WindowHeader : UserControl
    {
        public WindowHeader()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this == null)
                return;

            //Close the window this component is located at
            Window.GetWindow(this)?.DragMove();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            if (this == null)
                return;
            
            //Close the window this component is located at
            Window.GetWindow(this)?.Close();
        }

        private void Button_Click_Minimize(object sender, RoutedEventArgs e)
        {
            if (this == null)
                return;

            Window WindowRef = Window.GetWindow(this);
            if (WindowRef == null)
                return;

            //Minimize the window this component is located at
            WindowRef.WindowState = WindowState.Minimized;            
        }

        private void Button_Click_Maximize(object sender, RoutedEventArgs e)
        {
            if (this == null)
                return;

            Window WindowRef = Window.GetWindow(this);
            if (WindowRef == null)
                return;


            //Toggle Between Normal and Maximized
            WindowRef.WindowState = WindowRef.WindowState == WindowState.Maximized ? 
                WindowState.Normal : WindowState.Maximized;
        }

        // CanMaximize property
        #region CanMaximize Property
            public static readonly DependencyProperty CanMaximizeProperty = DependencyProperty.Register
            (
                "CanMaximize",
                typeof(bool),
                typeof(WindowHeader),
                new PropertyMetadata(true, OnCanMaximizeChanged)
            );
            private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var header = (WindowHeader)d;
                bool canMax = (bool)e.NewValue;
                var OriginalWidth = header.MaximizeButton_ColumnDefinition.Width;

                //Toggle Visibility of Maximize Button
                header.MaximizeButton.Visibility = canMax ? Visibility.Visible : Visibility.Collapsed;

                //Set New Locations on the grid for each of the headers buttons
                Grid.SetColumn(header.MinimizeButton, canMax ? 0 : 1);
            }

            public bool CanMaximize
            {
                get => (bool)GetValue(CanMaximizeProperty);
                set => SetValue(CanMaximizeProperty, value);
            }
        #endregion
    }
}
