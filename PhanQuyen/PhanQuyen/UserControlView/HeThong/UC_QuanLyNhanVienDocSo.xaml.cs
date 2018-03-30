using Model;
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
using ViewModel;

namespace PhanQuyen.UserControlView.HeThong
{
    /// <summary>
    /// Interaction logic for UC_QuanLyNhanVienDocSo.xaml
    /// </summary>
    public partial class UC_QuanLyNhanVienDocSo : System.Windows.Controls.UserControl
    {
        SolidColorBrush red = new SolidColorBrush(Colors.Red);
        SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);
        SolidColorBrush white = new SolidColorBrush(Colors.White);
        SolidColorBrush green = new SolidColorBrush(Colors.Green);
        public UC_QuanLyNhanVienDocSo()
        {
            InitializeComponent();
        }

        private void btnViewInfo_Click(object sender, RoutedEventArgs e)
        {
            dtgridMain.ItemsSource = HandlingDataDBViewModel.Instance.GetNhanVienDocSo(cbbYear.Text.ToString(), cbbMonth.Text.ToString(), cbbDate.Text.ToString(), cbbGroup.Text.ToString());

            foreach (QuanLyNVDocSo item in dtgridMain.Items)
            {
                DataGridRow row = getRow(item);
                DataGridCell cell = getCell(dtgridMain, row, 5);
                row.Background = white;
                if (item.DongBo == 0)
                {

                    cell.Background = red;
                }
                else if (item.DongBo < item.SoKH)
                {
                    cell.Background = yellow;
                }
                else
                {
                    cell.Background = green;
                }
            }
        }

        private DataGridRow getRow(int index)
        {
            if (index < 0)
                return null;
            DataGridRow row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                dtgridMain.UpdateLayout();
                dtgridMain.ScrollIntoView(dtgridMain.Items[index]);
                row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;

        }

        private DataGridRow getRow(QuanLyNVDocSo item)
        {
            if (item == null)
                return null;
            DataGridRow row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromItem(item);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                dtgridMain.UpdateLayout();
                dtgridMain.ScrollIntoView(item);
                row = (DataGridRow)dtgridMain.ItemContainerGenerator.ContainerFromItem(item);
            }
            return row;

        }
        static DataGridCell getCell(DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                System.Windows.Controls.Primitives.DataGridCellsPresenter presenter = GetVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
