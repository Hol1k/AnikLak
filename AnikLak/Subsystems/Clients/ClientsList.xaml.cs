using AnikLak.XAMLTemplates;

namespace AnikLak.Subsystems.Clients;

public partial class ClientsList : ContentPage
{
	public ClientsList()
	{
		InitializeComponent();

		stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
    }

    private async void AddNewClientClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientEditing());
    }
}