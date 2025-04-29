using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class ChangeAppointmentStatusPopup : Popup
{
	Button _statusField;

	public ChangeAppointmentStatusPopup(Button statusField)
	{
		_statusField = statusField;

		InitializeComponent();
	}

    private async void ChangeStatus(object? sender, EventArgs e)
    {
		_statusField.Text = ((Button)sender).Text;

        await CloseAsync();
    }
}