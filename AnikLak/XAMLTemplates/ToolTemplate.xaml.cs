using AnikLak.ModelsDto.Tools;
using AnikLak.Subsystems.Tools;

namespace AnikLak.XAMLTemplates;

public partial class ToolTemplate : ContentView
{
    private ToolsList _toolsListPage;

    private ToolDto _toolInfo;

	public ToolTemplate(ToolDto tool, ToolsList toolsListPage)
    {
        InitializeComponent();

        _toolsListPage = toolsListPage;

        _toolInfo = tool;
        _name.Text = _toolInfo.Name;
        _count.Text = $"{_toolInfo.FunctioningCount}/{_toolInfo.Count}";
    }

    private async void EditTool(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolEditing(_toolInfo, _toolsListPage));
    }
}