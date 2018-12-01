using System;

namespace XamarinForms_SyncTask
{
    public partial class ListsPage : PageBase
    {
        private readonly Lazy<ListsViewModel> _typedViewModel = new Lazy<ListsViewModel>();

        public override ViewModelBase ViewModel => _typedViewModel.Value;

        public ListsPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}