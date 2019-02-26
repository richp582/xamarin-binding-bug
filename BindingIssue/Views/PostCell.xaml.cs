using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BindingIssue.ViewModels;
using Xamarin.Forms;

namespace BindingIssue.Views
{
    public partial class PostCell : ViewCell
    {

        private bool _isLoaded;
        private CancellationTokenSource _cancellationTokenSource;

        public PostCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _isLoaded = false;
        }

        private async Task DelayedOnAppearing(CancellationToken token)
        {
            try
            {
                await Task.Delay(333);
                if (token.IsCancellationRequested)
                {
                    return;
                }
                if (!(this.BindingContext is PostCellViewModel vm))
                {
                    return;
                }
                await vm.OnAppearing();
                lblAuthor.Text = vm.Author;
                _isLoaded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!(this.BindingContext is PostCellViewModel vm) || _isLoaded)
            {
                return;
            }

            if (!_isLoaded)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await Task.Factory.StartNew(async () => await DelayedOnAppearing(_cancellationTokenSource.Token));
            }
            _isLoaded = true;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();


            if (_cancellationTokenSource != null && _cancellationTokenSource.Token.CanBeCanceled)
                _cancellationTokenSource.Cancel();
            if (!(this.BindingContext is PostCellViewModel vm))
            {
                return;
            }

            //await vm.OnDisappearing();
        }
    }
}
