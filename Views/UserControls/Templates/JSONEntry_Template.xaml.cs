using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace JSON_Editor.Views.UserControls.Templates
{

    /*
     * This class is a template, therefore it doesn't need a View Model
     * because the bindings are used in the instantiating XAML file's ViewModel
     * 
     * For better understanding:
     * 
     *  - The ViewModel Creates the binds each instance uses.
     * 
     *  |-------------------------------|
     *  |          ViewModel            |
     *  |              ↓                |           |-----------------------------------|
     *  |  |-----------------------|    |   --->    |   Instance Of JSONEntry_Template  |            
     *  |  |  View -> Code Behind  |    |           |-----------------------------------|
     *  |  |-----------------------|    |
     *  |-------------------------------|
     */
    public partial class JSONEntry_Template : System.Windows.Controls.UserControl
    {
        public JSONEntry_Template()
        {
            InitializeComponent();
        }


        // DP holds GridLength, not double
        public static readonly DependencyProperty ColumnWidthsProperty =
            DependencyProperty.Register(
                nameof(ColumnWidths),
                typeof(ObservableCollection<GridLength>),
                typeof(JSONEntry_Template),
                // do NOT put a new collection here; it's shared across instances
                new PropertyMetadata(null));

        public ObservableCollection<GridLength> ColumnWidths
        {
            get => (ObservableCollection<GridLength>)GetValue(ColumnWidthsProperty);
            set => SetValue(ColumnWidthsProperty, value);
        }
    }
}
