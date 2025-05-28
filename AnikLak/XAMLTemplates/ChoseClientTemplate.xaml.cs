using AnikLak.ModelsDto.Clients;
using AnikLak.Subsystems.Appointments;

namespace AnikLak.XAMLTemplates;

public partial class ChoseClientTemplate : ContentView
{
    private int _clientId;

    private Button _clientField;
    private ChoseAppointmentClientPopup _parentPopup;
    private AppointmentEditing _appointmentEditingWindow;

    public ChoseClientTemplate(ClientDto clientInfo, Button clientField, ChoseAppointmentClientPopup parentPopup, AppointmentEditing appointmentEditingWindow)
    {
        _clientId = clientInfo.Id ?? 0;
        _clientField = clientField;
        _parentPopup = parentPopup;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();

        _name.Text = clientInfo.Name;
        _phoneNumber.Text = clientInfo.PhoneNumber;
    }

    private async void ChoseClient(object? sender, EventArgs e)
    {
        _appointmentEditingWindow.ClientId = _clientId;
        _clientField.Text = _name.Text;

        await _parentPopup.CloseAsync();
    }
}