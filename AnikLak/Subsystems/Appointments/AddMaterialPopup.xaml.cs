using AnikLak.ModelsDto.Materials;
using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddMaterialPopup : Popup
{
    private VerticalStackLayout _chosedMaterialsContainer;

    private List<MaterialDto> _materials;

    public AddMaterialPopup(VerticalStackLayout chosedMaterialsContainer, List<MaterialDto> materials)
    {
        _chosedMaterialsContainer = chosedMaterialsContainer;

        InitializeComponent();

        _materials = materials;
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyCount();

        int nonEmptyCount= Convert.ToInt32((_materialCount.Text == null || _materialCount.Text == string.Empty) ? "0" : _materialCount.Text);

        if (nonEmptyCount > 0)
        {
            _chosedMaterialsContainer.Add(new AppointmentMaterialTemplate(
                _materialName.Text,
                nonEmptyCount,
                _chosedMaterialsContainer,
                _materials));

            _materials.Add(new MaterialDto()
            {
                Name = _materialName.Text,
                Count = nonEmptyCount
            });
        }

        await CloseAsync();
    }

    private void VerifyCount()
    {
        if (_materialCount.Text == null || _materialCount.Text == string.Empty)
            return;

        var materialCount = (int)Convert.ToDouble(_materialCount.Text.Replace('.', ','));

        if (materialCount <= 0)
            _materialCount.Text = string.Empty;
        else
            _materialCount.Text = materialCount.ToString();
    }

    private void VerifyCount(object? sender, EventArgs e)
    {
        VerifyCount();
    }
}