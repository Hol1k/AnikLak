using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates;

public partial class AppointmentTemplate : ContentView
{
	public AppointmentTemplate()
	{
		InitializeComponent();
    }

    private async void EditAppointment(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing());
    }
}