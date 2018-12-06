using DevExpress.Data.TreeList;
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

namespace Modules.Products.WorkItems.Categories.Views
{
    /// <summary>
    /// Interaction logic for BrandsListView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
        }

        private void TreeListView_NodeChanged(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeChangedEventArgs e)
        {
            if (e.ChangeType == NodeChangeType.Add)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    if (e.Node.ParentNode != null)
                        e.Node.ParentNode.IsExpanded = true;
                }));
            }
        }
    }
}
