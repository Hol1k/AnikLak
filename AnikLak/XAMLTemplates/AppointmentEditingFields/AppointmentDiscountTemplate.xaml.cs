using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentDiscountTemplate : ContentView
{
    private float _discountValueInPercent;
    private VerticalStackLayout _parent;
    AppointmentEditing _appointmentEditingWindow;

    public float DiscountValue { get { return _discountValueInPercent / 100; } }

    public AppointmentDiscountTemplate(string discountName, float discountValueInPercent, VerticalStackLayout parent, AppointmentEditing appointmentEditingWindow)
	{
		InitializeComponent();

        _parent = parent;
        _appointmentEditingWindow = appointmentEditingWindow;

        _discountValueInPercent = discountValueInPercent;
        _discountName.Text = $"• {discountName} (-{discountValueInPercent}%)";
    }

    private void RemoveDiscount(object? sender, EventArgs e)
    {
        _parent.Remove(this);

        _appointmentEditingWindow.RecalculatePrice();
    }
}