﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace PhanQuyen
{
    /// <summary>
    /// Interaction logic for XemGhiChuWindow.xaml
    /// </summary>
    public partial class XemGhiChuWindow : Window
    {
        private String _danhBa;
        public XemGhiChuWindow(String danhBa)
        {
            _danhBa = danhBa;
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            dtgridMain.ItemsSource = GetDataDBViewModel.Instance.getNote(_danhBa);
        }
        public XemGhiChuWindow()
        {
            InitializeComponent();
            
        }
        public void GetNote(String danhBa)
        {
            dtgridMain.ItemsSource = GetDataDBViewModel.Instance.getNote(danhBa);
        }
    }
}
