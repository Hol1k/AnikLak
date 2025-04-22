using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;

namespace AnikLak;

public partial class Materials : ContentPage
{
	public Materials()
	{
		InitializeComponent();

		stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
    }

    private async void AddMaterialsClicked(object? sender, EventArgs e)
    {
        var popup = new AddMaterialsPopup();

        await this.ShowPopupAsync(popup);
    }
}