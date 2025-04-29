using AnikLak.Subsystems.Clients;

namespace AnikLak.XAMLTemplates;

public partial class ClientTemplate : ContentView
{
	public ClientTemplate()
	{
		InitializeComponent();
    }

    private async void EditClient(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientEditing());
    }
}