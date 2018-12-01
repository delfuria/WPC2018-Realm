using System.ComponentModel;
using CoreGraphics;
using Foundation;
using XamarinForms_SyncTask.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinForms_SyncTask;

[assembly: ExportRenderer(typeof(TransparentEntry), typeof(TransparentEntryRenderer))]
namespace XamarinForms_SyncTask.iOS
{
    public class TransparentEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.BackgroundColor = UIColor.Clear;
                Control.TintColor = UIColor.White;
                Control.LeftView = new UIView(new CGRect(0, 0, 15, 10));
                Control.LeftViewMode = UITextFieldViewMode.Always;
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
            var text = new NSMutableAttributedString(Control.AttributedText);
            text.SetAttributes(new UIStringAttributes
            {
                StrikethroughStyle = entry?.IsStrikeThrough == true ? NSUnderlineStyle.Single : NSUnderlineStyle.None
            }, new NSRange(0, text.Length));
            Control.AttributedText = text;
        }
    }
}