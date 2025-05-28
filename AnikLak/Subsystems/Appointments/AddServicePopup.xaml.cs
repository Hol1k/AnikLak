using AnikLak.ModelsDto.Services;
using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddServicePopup : Popup
{
    private VerticalStackLayout _chosedServicesContainer;
    private AppointmentEditing _appointmentEditingWindow;

    private List<ServiceDto> _services;


    public AddServicePopup(VerticalStackLayout chosedServicesContainer, AppointmentEditing appointmentEditingWindow, List<ServiceDto> services)
	{
        _chosedServicesContainer = chosedServicesContainer;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();

        _services = services;
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyPrice();

        decimal nonEmptyPrice = Convert.ToDecimal((_price.Text == null || _price.Text == string.Empty) ? "0" : _price.Text);

        _chosedServicesContainer.Add(new AppointmentServiceTemplate(
            _serviceName.Text,
            nonEmptyPrice,
            _chosedServicesContainer,
            _appointmentEditingWindow,
            _services));

        _services.Add(new ServiceDto()
        {
            Name = _serviceName.Text,
            Price = nonEmptyPrice
        });

        _appointmentEditingWindow.RecalculatePrice();

        await CloseAsync();
    }

    private void VerifyPrice()
    {
        if (_price.Text == null || _price.Text == string.Empty)
            return;

        _price.Text = _price.Text.Replace('.', ',');

        if (Convert.ToDouble(_price.Text) <= 0)
            _price.Text = string.Empty;
    }

    private void VerifyPrice(object? sender, EventArgs e)
    {
        VerifyPrice();
    }
}