﻿using Xamarin.Forms;

namespace XamarinForms_SyncTask
{
    public abstract class PageBase : ContentPage
    {
        public abstract ViewModelBase ViewModel { get; }

        protected PageBase()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.Initialize();
        }
    }
}