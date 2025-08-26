using JSON_Editor.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;


namespace JSON_Editor.Views
{
    public partial class JSON_EditorWindow : Window
    {
        //Each Index represents a column width. (all the rows adapt to the column width)


        public JSON_EditorWindow(string FilePath)
        {            
            JSON_EditorViewModel VM = new JSON_EditorViewModel(FilePath);
            DataContext = VM;

            //Bind view-model "OnTryClosingWindow" to "Closing" window event
            Closing += (Sender, CancelArgs) => VM.OnTryClosingWindow(Sender, CancelArgs);

            InitializeComponent();

            Loaded += (s, e) => {SizeChanged += EditorWindow_SizeChanged; };
        }
        public ObservableCollection<GridLength> ColumnWidthsVal { get; private set; } =
            new ObservableCollection<GridLength>
            {
                new GridLength(250),
                new GridLength(250),
                new GridLength(250),
                new GridLength(250)
            };

        private void EditorWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsInitialized)
                return;

            if (e.WidthChanged && ColumnWidthsVal != null && ColumnWidthsVal.Count > 0)
            {
                double delta = e.NewSize.Width - e.PreviousSize.Width;
                double increment = delta / (ColumnWidthsVal.Count + 1);

                for (int i = 0; i < ColumnWidthsVal.Count; ++i)
                {
                    // Only handle Pixel GridLengths for now
                    if (ColumnWidthsVal[i].IsAbsolute)
                    {
                        double newWidth = ColumnWidthsVal[i].Value + increment;
                        if (newWidth < 100) 
                            newWidth = 100; // prevent < 100 Sizes

                        ColumnWidthsVal[i] = new GridLength(newWidth);
                    }
                }
            }
        }
    }
}
