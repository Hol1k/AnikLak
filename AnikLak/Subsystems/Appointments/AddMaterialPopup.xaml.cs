using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddMaterialPopup : Popup
{
    private VerticalStackLayout _chosedMaterialsContainer;

    public AddMaterialPopup(VerticalStackLayout chosedMaterialsContainer)
    {
        _chosedMaterialsContainer = chosedMaterialsContainer;

        InitializeComponent();
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyCount();

        if (_materialCount.Text == "0")
        {
            _chosedMaterialsContainer.Add(new AppointmentToolTemplate(
            _materialName.Text,
            Convert.ToInt32(_materialCount.Text),
            _chosedMaterialsContainer));
        }

        await CloseAsync();
    }

    private void VerifyCount()
    {
        if (_materialCount.Text == "" || Convert.ToInt32(_materialCount.Text) < 0)
        {
            _materialCount.Text = "0";
        }
    }

    private void VerifyCount(object? sender, EventArgs e)
    {
        VerifyCount();
    }
}