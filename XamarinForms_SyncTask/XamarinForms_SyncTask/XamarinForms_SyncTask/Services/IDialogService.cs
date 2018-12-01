namespace XamarinForms_SyncTask
{
    public interface IDialogService
    {
        void ShowProgress(string message);

        void HideProgress();

        void Alert(string title, string message);
    }
}
