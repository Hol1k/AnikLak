namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentMaterialTemplate : ContentView
{
    private int _count;
    private VerticalStackLayout _parent;

    public AppointmentMaterialTemplate(string materialName, int count, VerticalStackLayout parent)
    {
        InitializeComponent();

        _parent = parent;
        _count = count;
        _materialName.Text = $"• {materialName}";
        _countField.Text = count.ToString();
    }

    private void VerifyMaterialCount(object? sender, EventArgs e)
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

    private void RemoveMaterial(object? sender, EventArgs e)
    {
        _parent.Remove(this);
    }
}