using CommunityToolkit.Maui.Views;

namespace AnikLak
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OpenNotifications(object? sender, EventArgs e)
        {
            var popup = new Notifications();

            await this.ShowPopupAsync(popup);
        }
    }
}
