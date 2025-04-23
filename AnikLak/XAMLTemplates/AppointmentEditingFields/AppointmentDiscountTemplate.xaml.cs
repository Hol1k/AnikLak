namespace AnikLak.XAMLTemplates.AppointmentEditingFields;

public partial class AppointmentDiscountTemplate : ContentView
{
    private float _discountValueInPercent;
    private VerticalStackLayout _parent;

    public float DiscountValue { get { return _discountValueInPercent / 100; } }

    public AppointmentDiscountTemplate(string discountName, float discountValueInPercent, VerticalStackLayout parent)
	{
		InitializeComponent();

        _parent = parent;
        _discountValueInPercent = discountValueInPercent;
        _discountName.Text = $"• {discountName} (-{discountValueInPercent}%)";
    }

    private void RemoveDiscount(object? sender, EventArgs e)
    {
        _parent.Remove(this);
    }
}