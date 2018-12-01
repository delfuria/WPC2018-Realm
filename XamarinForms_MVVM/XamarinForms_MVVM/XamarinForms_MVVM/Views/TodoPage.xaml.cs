using System;
using System.Collections.Generic;
using XamarinForms_MVVM.ViewModels;
using Xamarin.Forms;

namespace XamarinForms_MVVM.Views
{
    public partial class TodoPage : ContentPage
    {
        public TodoPage()
        {
            InitializeComponent();
            this.BindingContext = new TodoPageViewModel();
        }
    }
}
