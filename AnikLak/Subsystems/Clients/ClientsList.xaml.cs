using AnikLak.ModelsDto.Clients;
using AnikLak.XAMLTemplates;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AnikLak.Subsystems.Clients;

public partial class ClientsList : ContentPage
{
    private readonly string _getAllClientsUrl = "http://hol1k.ru:5000/clients/get-clients-list";

    public ClientsList()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowClientsList();
    }

    public async Task ShowClientsList()
    {
        _stackLayout.Children.Clear();

        List<ClientDto> clientList = new List<ClientDto>();
        clientList = await GetClientsAsync();

        foreach (ClientDto client in clientList)
        {
            _stackLayout.Insert(0, new ClientTemplate(client, this));
        }
    }

    private async Task<List<ClientDto>> GetClientsAsync()
    {
        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(_getAllClientsUrl);

            if (response.IsSuccessStatusCode)
            {
                var clientList = await response.Content.ReadFromJsonAsync<List<ClientDto>>();
                return clientList ?? new List<ClientDto>();
            }
            else
            {
                Console.WriteLine($"Ошибка API: {response.StatusCode}");
                return new List<ClientDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при запросе: {ex.Message}");
            return new List<ClientDto>();
        }
    }

    private async void AddNewClientClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClientEditing(new ClientDto(), this));
    }
}