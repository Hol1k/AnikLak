namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentServiceTemplate : ContentView
{
	private decimal _price;
    private VerticalStackLayout _parent;

    public decimal Price { get { return _price; } }

	public AppointmentServiceTemplate(string serviceName, decimal price, VerticalStackLayout parent)
	{
		InitializeComponent();

        _parent = parent;
		_price = price;
        _serviceName.Text = $"• {serviceName} ({price})";
    }

    private void RemoveSrvice(object? sender, EventArgs e)
    {
        _parent.Remove(this);
    }
}