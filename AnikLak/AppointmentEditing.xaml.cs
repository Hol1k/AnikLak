namespace AnikLak;

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

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}