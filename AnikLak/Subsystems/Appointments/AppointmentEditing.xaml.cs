using AnikLak.XAMLTemplates.AppointmentEditingFields;

namespace AnikLak.Subsystems.Appointments;

public partial class AppointmentEditing : ContentPage
{
	public int? appointmentId;

	public AppointmentEditing()
	{
		InitializeComponent();
    }

    public AppointmentEditing(int appointmentId)
    {
        this.appointmentId = appointmentId;

        InitializeComponent();
    }

    private void AddService(object? sender, EventArgs e)
    {
        chosedServicesContainer.Add(new AppointmentServiceTemplate("Услуга1", 800, chosedServicesContainer));
        RecalculatePrice();
    }

    private void AddDiscount(object? sender, EventArgs e)
    {
        chosedDiscountsContainer.Add(new AppointmentDiscountTemplate("Первое посещение", 10f, chosedDiscountsContainer));
        RecalculatePrice();
    }

    private void AddTool(object? sender, EventArgs e)
    {
        chosedToolsContainer.Add(new AppointmentToolTemplate("Фреза алмазная пламя", 1, chosedToolsContainer));
    }

    private void AddMaterial(object? sender, EventArgs e)
    {
        chosedMaterialContainer.Add(new AppointmentMaterialTemplate("Апельсиновая палочка", 2, chosedMaterialContainer));
    }

    private void RecalculatePrice()
    {
        decimal basePrice = 0;
        foreach (var child in chosedServicesContainer)
        {
            if (child is AppointmentServiceTemplate service)
            {
                basePrice += service.Price;
            }
        }
        float discount = 0f;
        foreach (var child in chosedDiscountsContainer)
        {
            if (child is AppointmentDiscountTemplate discountTemplate)
            {
                discount += discountTemplate.DiscountValue;
            }
        }

        _priceField.Text = (basePrice * (decimal)(1 - discount)).ToString("0.##");
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}