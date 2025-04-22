namespace AnikLak.XAMLTemplates;

public partial class AppointmentToolTemplate : ContentView
{
    private int _count;
    private VerticalStackLayout _parent;

    public AppointmentToolTemplate(string toolName, int count, VerticalStackLayout parent)
    {
		InitializeComponent();

        _parent = parent;
        _count = count;
        _toolName.Text = $"• {toolName}";
        _countField.Text = count.ToString();
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
    }
}