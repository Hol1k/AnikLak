using AnikLak.ModelsDto.Clients;
using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class ChoseAppointmentClientPopup : Popup
{
    private Button _clientField;
    private AppointmentEditing _appointmentEditingWindow;

    public ChoseAppointmentClientPopup(Button clientField, AppointmentEditing appointmentEditingWindow)
    {
        _clientField = clientField;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();
    }

    public async void AddNewClient(ClientDto clientInfo)
    {
        _clientsListContainer.Add(new ChoseClientTemplate(clientInfo, _clientField, this, _appointmentEditingWindow));
    }
}