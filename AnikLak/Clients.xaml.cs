using AnikLak.XAMLTemplates;

namespace AnikLak;

public partial class Clients : ContentPage
{
	public Clients()
	{
		InitializeComponent();

		stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
        stackLayout.Add(new ClientTemplate());
    }
}