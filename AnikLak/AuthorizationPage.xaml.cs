namespace AnikLak
{
	public partial class AuthorizationPage : ContentPage
	{
		public AuthorizationPage()
		{
			InitializeComponent();
		}

        private void OnLoginClicked(object sender, EventArgs e)
		{
			Application.Current.MainPage = new AppShell();
        }
    }
}