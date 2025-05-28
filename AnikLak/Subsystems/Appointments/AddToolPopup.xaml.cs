using AnikLak.ModelsDto.Tools;
using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddToolPopup : Popup
{
    private VerticalStackLayout _chosedToolsContainer;

    private List<ToolDto> _tools;

    public AddToolPopup(VerticalStackLayout chosedToolsContainer, List<ToolDto> tools)
    {
        _chosedToolsContainer = chosedToolsContainer;

        InitializeComponent();

        _tools = tools;
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
                _chosedToolsContainer,
                _tools));

            _tools.Add(new ToolDto()
            {
                Name = _toolName.Text,
                Count = nonEmptyCount
            });
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