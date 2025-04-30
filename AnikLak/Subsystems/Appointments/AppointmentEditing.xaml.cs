using CommunityToolkit.Maui.Views;
using AnikLak.XAMLTemplates.AppointmentEditingFields;

namespace AnikLak.Subsystems.Appointments;

public partial class AppointmentEditing : ContentPage
{
	public int? AppointmentId;
    public int? ClientId;

	public AppointmentEditing()
	{
		InitializeComponent();
    }

    public AppointmentEditing(int appointmentId)
    {
        AppointmentId = appointmentId;

        InitializeComponent();
    }

    private async void ChoseClient(object? sender, EventArgs e)
    {
        var popup = new ChoseAppointmentClientPopup(_clientField, this);

        await this.ShowPopupAsync(popup);
    }

    private async void ChangeStatus(object? sender, EventArgs e)
    {
        var popup = new ChangeAppointmentStatusPopup(_statusField);

        await this.ShowPopupAsync(popup);
    }

    private async void AddService(object? sender, EventArgs e)
    {
        var popup = new AddServicePopup(_chosedServicesContainer, this);

        await this.ShowPopupAsync(popup);
    }

    private async void AddDiscount(object? sender, EventArgs e)
    {
        var popup = new AddDiscountPopup(_chosedDiscountsContainer, this);

        await this.ShowPopupAsync(popup);
    }

    private async void AddTool(object? sender, EventArgs e)
    {
        var popup = new AddToolPopup(_chosedToolsContainer);

        await this.ShowPopupAsync(popup);
    }

    private async void AddMaterial(object? sender, EventArgs e)
    {
        var popup = new AddMaterialPopup(_chosedMaterialsContainer);

        await this.ShowPopupAsync(popup);
    }

    public void RecalculatePrice()
    {
        decimal basePrice = 0;
        foreach (var child in _chosedServicesContainer)
        {
            if (child is AppointmentServiceTemplate service)
            {
                basePrice += service.Price;
            }
        }
        float discount = 0f;
        foreach (var child in _chosedDiscountsContainer)
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