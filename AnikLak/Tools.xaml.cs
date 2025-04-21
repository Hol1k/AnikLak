using AnikLak.XAMLTemplates;

namespace AnikLak;

public partial class Tools : ContentPage
{
	public Tools()
	{
		InitializeComponent();

		stackLayout.Add(new ToolTemplate());
        stackLayout.Add(new ToolTemplate());
        stackLayout.Add(new ToolTemplate());
        stackLayout.Add(new ToolTemplate());
        stackLayout.Add(new ToolTemplate());
    }

    private async void AddNewToolClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolEditing());
    }
}