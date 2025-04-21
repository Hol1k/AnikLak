using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;

namespace AnikLak;

public partial class AddMaterialsPopup : Popup
{
	public AddMaterialsPopup()
	{
		InitializeComponent();

		addingMaterialsListContainer.Add(new MaterialToAddTemplate());
        addingMaterialsListContainer.Add(new MaterialToAddTemplate());
        addingMaterialsListContainer.Add(new MaterialToAddTemplate());
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        await CloseAsync();
    }
}