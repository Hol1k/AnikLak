using AnikLak.Subsystems.Tools;

namespace AnikLak.XAMLTemplates;

public partial class ToolTemplate : ContentView
{
	public ToolTemplate()
	{
		InitializeComponent();
    }

    private async void EditTool(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolEditing());
    }
}