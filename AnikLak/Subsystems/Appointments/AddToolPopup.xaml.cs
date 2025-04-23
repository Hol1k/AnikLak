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

        if (_toolCount.Text == "0")
        {
            _chosedToolsContainer.Add(new AppointmentToolTemplate(
            _toolName.Text,
            Convert.ToInt32(_toolCount.Text),
            _chosedToolsContainer));
        }

        await CloseAsync();
    }

    private void VerifyCount()
    {
        if (_toolCount.Text == "" || Convert.ToInt32(_toolCount.Text) < 0)
        {
            _toolCount.Text = "0";
        }
    }

    private void VerifyCount(object? sender, EventArgs e)
    {
        VerifyCount();
    }
}