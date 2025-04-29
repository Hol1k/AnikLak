using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentServiceTemplate : ContentView
{
	private decimal _price;
    private VerticalStackLayout _parent;
    AppointmentEditing _appointmentEditingWindow;

    public decimal Price { get { return _price; } }

	public AppointmentServiceTemplate(string serviceName, decimal price, VerticalStackLayout parent, AppointmentEditing appointmentEditingWindow)
	{
		InitializeComponent();

        _parent = parent;
        _appointmentEditingWindow = appointmentEditingWindow;
        
        _price = price;
        _serviceName.Text = $"• {serviceName} ({(price <= 0 ? "Бесплатно" : price)})";
    }

    private void RemoveSrvice(object? sender, EventArgs e)
    {
        _parent.Remove(this);

        _appointmentEditingWindow.RecalculatePrice();
    }
}