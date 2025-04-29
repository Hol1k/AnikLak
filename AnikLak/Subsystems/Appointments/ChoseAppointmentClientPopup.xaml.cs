using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class ChoseAppointmentClientPopup : Popup
{
	public ChoseAppointmentClientPopup()
	{
		InitializeComponent();

        _clientsListContainer.Add(new ClientTemplate());
        _clientsListContainer.Add(new ClientTemplate());
    }
}