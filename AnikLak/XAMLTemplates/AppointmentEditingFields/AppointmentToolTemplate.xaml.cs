using AnikLak.ModelsDto.Tools;

namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentToolTemplate : ContentView
{
    private int _count;
    private VerticalStackLayout _parent;

    private List<ToolDto> _tools;

    public AppointmentToolTemplate(string toolName, int count, VerticalStackLayout parent, List<ToolDto> tools)
    {
		InitializeComponent();

        _parent = parent;
        _count = count;
        _toolName.Text = $"• {toolName}";
        _countField.Text = count.ToString();
        _tools = tools;
    }

    private void VerifyToolCount(object? sender, EventArgs e)
    {
        var count = Convert.ToInt32(_countField.Text);

        if (count < 1)
        {
            _countField.Text = 1.ToString();
            _count = 1;
        }
        else
        {
            _count = count;
        }
    }

    private void RemoveTool(object? sender, EventArgs e)
    {
        _parent.Remove(this);
        _tools.Remove(_tools.FirstOrDefault(t => t.Name == _toolName.Text) ?? new());
    }
}