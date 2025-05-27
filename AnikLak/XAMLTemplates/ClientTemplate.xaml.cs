using AnikLak.ModelsDto.Clients;
using AnikLak.Subsystems.Clients;

namespace AnikLak.XAMLTemplates;

public partial class ClientTemplate : ContentView
{
    private ClientsList _clientsListPage;

    private ClientDto _clientInfo;

    public ClientTemplate(ClientDto client, ClientsList clientsListPage)
	{
		InitializeComponent();

        _clientInfo = client;
        _clientsListPage = clientsListPage;

        _name.Text = _clientInfo.Name;
        _phoneNumber.Text = _clientInfo.PhoneNumber;
        _note.Text = _clientInfo.Note;
        _visitCount.Text = _clientInfo.AppointmentsCount.ToString();
    }

    private async void EditClient(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientEditing(_clientInfo, _clientsListPage));
    }
}