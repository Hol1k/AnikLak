using AnikLak.ModelsDto.Appointments;
using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates;

public partial class LittleAppointmentTemplate : ContentView
{
    private AppointmentDto _appointmentInfo;

    public LittleAppointmentTemplate(AppointmentDto appointmentInfo)
	{
		InitializeComponent();

        _appointmentInfo = appointmentInfo;

        _status.Text = _appointmentInfo.Status;
        _dateTime.Text = new DateTime(DateOnly.Parse(_appointmentInfo.Date), TimeOnly.Parse(_appointmentInfo.Time)).ToString();
        _price.Text = $"{_appointmentInfo.FinalPrice} ₽";
    }

    private async void EditAppointment(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing(_appointmentInfo, null));
    }
}