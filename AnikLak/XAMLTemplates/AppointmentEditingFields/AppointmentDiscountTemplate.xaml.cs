using AnikLak.ModelsDto.Discounts;
using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentDiscountTemplate : ContentView
{
    private float _discountValueInPercent;
    private VerticalStackLayout _parent;
    AppointmentEditing _appointmentEditingWindow;

    private List<DiscountDto> _discounts;

    public float DiscountValue { get { return _discountValueInPercent / 100; } }

    public AppointmentDiscountTemplate(string discountName, float discountValueInPercent, VerticalStackLayout parent, AppointmentEditing appointmentEditingWindow, List<DiscountDto> discounts)
	{
		InitializeComponent();

        _parent = parent;
        _appointmentEditingWindow = appointmentEditingWindow;
        _discounts = discounts;

        _discountValueInPercent = discountValueInPercent;
        _discountName.Text = $"• {discountName} (-{discountValueInPercent}%)";
    }

    private void RemoveDiscount(object? sender, EventArgs e)
    {
        _parent.Remove(this);
        _discounts.Remove(_discounts.FirstOrDefault(d => d.Name == _discountName.Text) ?? new());

        _appointmentEditingWindow.RecalculatePrice();
    }
}