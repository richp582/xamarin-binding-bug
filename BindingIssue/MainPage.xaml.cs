using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BindingIssue.ViewModels;
using Xamarin.Forms;

namespace BindingIssue
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<PostCellViewModel> Entries { get; set; }

        public MainPage()
        {
            this.Entries = new ObservableCollection<PostCellViewModel>();
            var svc = new Services.RandomMeService();

            for (int i = 0; i < 150; i++)
            {
                var vm = new PostCellViewModel(svc);
                this.Entries.Add(vm);
            }

            InitializeComponent();
            listView.ItemsSource = Entries;
        }
    }
}
