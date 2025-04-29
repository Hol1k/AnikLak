using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates;

public partial class LittleAppointmentTemplate : ContentView
{
	public LittleAppointmentTemplate()
	{
		InitializeComponent();
    }

    private async void EditAppointment(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing());
    }
}