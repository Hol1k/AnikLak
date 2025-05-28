using AnikLak.ModelsDto.Appointments;
using AnikLak.ModelsDto.Discounts;
using AnikLak.ModelsDto.Services;
using AnikLak.Subsystems.Appointments;
using System.Text;

namespace AnikLak.XAMLTemplates;

public partial class AppointmentTemplate : ContentView
{
    private AppointmentsList _appointmentsListPage;
    private AppointmentDto _appointmentInfo;

	public AppointmentTemplate(AppointmentDto appointment, AppointmentsList appointmentsListPage)
	{
		InitializeComponent();

        _appointmentsListPage = appointmentsListPage;
        _appointmentInfo = appointment;

        _date.Text = new DateTime(DateOnly.Parse(_appointmentInfo.Date), TimeOnly.Parse(_appointmentInfo.Time)).ToString();
        _name.Text = _appointmentInfo.ClientName;

        List<ServiceDto> services = ServiceDto.DeserializeServiceListString(_appointmentInfo.ServiceListString);
        StringBuilder servicesSB = new StringBuilder();
        if (services.Any())
        {
            foreach (ServiceDto service in services)
            {
                servicesSB.Append("• " + service.Name + "&#10;");
            }
            servicesSB.Remove(servicesSB.Length - 5, 5);
        }
        _services.Text = servicesSB.ToString();

        List<DiscountDto> discounts = DiscountDto.DeserializeDiscountListString(_appointmentInfo.DiscountListString);
        StringBuilder discountsSB = new StringBuilder();
        discountsSB.Append($"({_appointmentInfo.BasePrice}");
        foreach (DiscountDto discount in discounts)
        {
            discountsSB.Append($"-{discount.Value.ToString("0.##")}%");
        }
        discountsSB.Append(")");
        _priceCalculate.Text = discountsSB.ToString();

        _price.Text = _appointmentInfo.FinalPrice.ToString("0.##");
    }

    private async void EditAppointment(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing(_appointmentInfo, _appointmentsListPage));
    }
}