using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddServicePopup : Popup
{
    private VerticalStackLayout _chosedServicesContainer;
    private AppointmentEditing _appointmentEditingWindow;


    public AddServicePopup(VerticalStackLayout chosedServicesContainer, AppointmentEditing appointmentEditingWindow)
	{
        _chosedServicesContainer = chosedServicesContainer;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyPrice();

        _chosedServicesContainer.Add(new AppointmentServiceTemplate(_serviceName.Text, Convert.ToDecimal(_price.Text), _chosedServicesContainer));
        _appointmentEditingWindow.RecalculatePrice();

        await CloseAsync();
    }

    private void VerifyPrice()
    {
        if (Convert.ToInt32(_price.Text) < 0)
        {
            _price.Text = "0";
        }
    }

    private void VerifyPrice(object? sender, EventArgs e)
    {
        VerifyPrice();
    }
}