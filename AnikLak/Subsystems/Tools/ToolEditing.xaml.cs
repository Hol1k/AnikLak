namespace AnikLak.Subsystems.Tools;

public partial class ToolEditing : ContentPage
{
	public ToolEditing()
	{
		InitializeComponent();
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}