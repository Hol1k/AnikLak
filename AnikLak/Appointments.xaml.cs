using AnikLak.XAMLTemplates;

namespace AnikLak;

public partial class Appointments : ContentPage
{
	public Appointments()
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