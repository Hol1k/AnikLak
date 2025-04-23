using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Materials;

public partial class MaterialsList : ContentPage
{
	public MaterialsList()
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