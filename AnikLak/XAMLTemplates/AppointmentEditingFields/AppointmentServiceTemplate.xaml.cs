using AnikLak.ModelsDto.Services;
using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentServiceTemplate : ContentView
{
	private decimal _price;
    private VerticalStackLayout _parent;
    private AppointmentEditing _appointmentEditingWindow;

    private List<ServiceDto> _services;

    public decimal Price { get { return _price; } }

	public AppointmentServiceTemplate(string serviceName, decimal price, VerticalStackLayout parent, AppointmentEditing appointmentEditingWindow, List<ServiceDto> services)
	{
		InitializeComponent();

        _parent = parent;
        _appointmentEditingWindow = appointmentEditingWindow;
        _services = services;
        
        _price = price;
        _serviceName.Text = $"• {serviceName} ({(price <= 0 ? "Бесплатно" : price)})";
    }

    private void RemoveSrvice(object? sender, EventArgs e)
    {
        _parent.Remove(this);
        _services.Remove(_services.FirstOrDefault(s => s.Name == _serviceName.Text) ?? new());

        _appointmentEditingWindow.RecalculatePrice();
    }
}