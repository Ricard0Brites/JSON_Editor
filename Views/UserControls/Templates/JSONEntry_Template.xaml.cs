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
    public partial class JSONEntry_Template : UserControl
    {
        public JSONEntry_Template()
        {
            InitializeComponent();
        }
    }
}
