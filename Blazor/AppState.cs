namespace Blazor
{
    public class AppState
    {
        public bool IsLogin { get; private set; }

        public event Action OnChange;

        public void SetLogin(bool value)
        {
            IsLogin = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
