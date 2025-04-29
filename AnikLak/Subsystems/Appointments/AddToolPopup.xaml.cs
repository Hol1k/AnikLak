using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddToolPopup : Popup
{
    private VerticalStackLayout _chosedToolsContainer;

    public AddToolPopup(VerticalStackLayout chosedToolsContainer)
    {
        _chosedToolsContainer = chosedToolsContainer;

        InitializeComponent();
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyCount();

        int nonEmptyCount = Convert.ToInt32((_toolCount.Text == null || _toolCount.Text == string.Empty) ? "0" : _toolCount.Text);

        if (nonEmptyCount > 0)
        {
            _chosedToolsContainer.Add(new AppointmentToolTemplate(
                _toolName.Text,
                nonEmptyCount,
                _chosedToolsContainer));
        }

        await CloseAsync();
    }

    private void VerifyCount()
    {
        if (_toolCount.Text == null || _toolCount.Text == string.Empty)
            return;

        var materialCount = (int)Convert.ToDouble(_toolCount.Text.Replace('.', ','));

        if (materialCount <= 0)
            _toolCount.Text = string.Empty;
        else
            _toolCount.Text = materialCount.ToString();
    }

    private void VerifyCount(object? sender, EventArgs e)
    {
        VerifyCount();
    }
}