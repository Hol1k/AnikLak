using AnikLak.XAMLTemplates;

namespace AnikLak.Subsystems.Appointments;

public partial class AppointmentsList : ContentPage
{
	public AppointmentsList()
	{
		InitializeComponent();

		stackLayout.Add(new AppointmentTemplate());
        stackLayout.Add(new AppointmentTemplate());
        stackLayout.Add(new AppointmentTemplate());
        stackLayout.Add(new AppointmentTemplate());
        stackLayout.Add(new AppointmentTemplate());
    }

    private async void AddNewAppointmentClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing());
    }
}