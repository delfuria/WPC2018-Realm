using XamarinForms_SyncTask.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(TaskListViewRenderer))]
namespace XamarinForms_SyncTask.iOS
{
    public class TaskListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.AllowsSelection = false;
            }
        }
    }
}