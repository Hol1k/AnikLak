using AnikLak.XAMLTemplates;

namespace AnikLak.Subsystems.Tools;

public partial class ToolsList : ContentPage
{
	public ToolsList()
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