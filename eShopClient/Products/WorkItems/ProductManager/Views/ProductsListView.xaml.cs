using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Modules.Products.WorkItems.ProductManager.Views
{
    /// <summary>
    /// Interaction logic for ProductsListView.xaml
    /// </summary>
    public partial class ProductsListView : UserControl
    {
        public ProductsListView()
        {
            InitializeComponent();
#if DESIGN
            DataContext = new ViewModels.ProductsListViewModel();
#endif
        }
    }
}
