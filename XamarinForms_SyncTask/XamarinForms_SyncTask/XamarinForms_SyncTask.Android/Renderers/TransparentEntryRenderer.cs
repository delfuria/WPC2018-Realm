﻿using System.ComponentModel;
using Android.Graphics;
using Android.Text;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms_SyncTask;
using XamarinForms_SyncTask.Android;
using Color = Android.Graphics.Color;


[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace XamarinForms_SyncTask.Android
{
    public class TransparentEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Gravity = GravityFlags.CenterVertical | GravityFlags.Left;
                Control.SetBackgroundColor(Color.Transparent);
                Control.InputType |= InputTypes.TextFlagNoSuggestions;
                Control.SetPadding(25, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);

                UpdateStrikeThrough(e.NewElement as TransparentEntry);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TransparentEntry.IsStrikeThrough))
            {
                UpdateStrikeThrough(sender as TransparentEntry);
            }
        }

        private void UpdateStrikeThrough(TransparentEntry entry)
        {
            if (entry?.IsStrikeThrough == true)
            {
                Control.PaintFlags |= PaintFlags.StrikeThruText;
            }
            else
            {
                Control.PaintFlags &= ~PaintFlags.StrikeThruText;
            }
        }
    }
}