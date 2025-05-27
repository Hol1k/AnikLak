using AnikLak.ModelsDto.Clients;
using AnikLak.XAMLTemplates;
using Microsoft.Maui.Layouts;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Clients;

public partial class ClientEditing : ContentPage
{
    private ClientsList _clientsListPage;
    private readonly string _addNewClientlUrl = "http://hol1k.ru:5000/clients/add-new-client";
    private readonly string _updateClientValuesUrl = "http://hol1k.ru:5000/clients/update-client-values";

    private int? _id;

	public ClientEditing(ClientDto clientInfo, ClientsList clientsListPage)
	{
		InitializeComponent();

        _id = clientInfo.Id;
        _clientsListPage = clientsListPage;

        _name.Text = clientInfo.Name;
        _number.Text = clientInfo.PhoneNumber;
        _note.Text = clientInfo.Note;
        _appointmentCount.Text = clientInfo.AppointmentsCount.ToString();

        double noteHeight = 0.0;
        noteHeight = _note.ComputeDesiredSize(_note.Width, double.PositiveInfinity).Height;
        ((Border)_note.Parent).HeightRequest = noteHeight;

        _lastAppointmentsContainer.Add(new LittleAppointmentTemplate());
        _lastAppointmentsContainer.Add(new LittleAppointmentTemplate());
    }

    private void TextChanged(object sender, TextChangedEventArgs e)
    {
        double noteHeight = 0.0;
        if (sender is Editor editor)
        {
            noteHeight = editor.ComputeDesiredSize(editor.Width, double.PositiveInfinity).Height;
            ((Border)editor.Parent).HeightRequest = noteHeight;
        }
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        if (_id == null)
            await AddNewClient();
        else
            await EditClient();
    }

    private async Task AddNewClient()
    {
        var clientToAdd = new AddClientDto()
        {
            Name = _name.Text.Trim(),
            PhoneNumber = _number.Text.Trim(),
            Note = _note.Text.Trim()
        };

        if (string.IsNullOrEmpty(clientToAdd.Name))
        {
            await DisplayAlert("Ошибка", "Заполните имя клиента", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PostAsJsonAsync(_addNewClientlUrl, clientToAdd);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Клиент успешно добавлен", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", $"Ошибка сервера: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        await _clientsListPage.ShowClientsList();
    }

    private async Task EditClient()
    {
        var clientToAdd = new UpdateClientDto()
        {
            Id = _id ?? 0,
            Name = _name.Text.Trim(),
            PhoneNumber = _number.Text.Trim(),
            Note = _note.Text.Trim()
        };

        if (string.IsNullOrEmpty(clientToAdd.Name))
        {
            await DisplayAlert("Ошибка", "Заполните имя клиента", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PutAsJsonAsync(_updateClientValuesUrl, clientToAdd);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Данные о клиенте успшено обновлены", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", $"Ошибка сервера: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        await _clientsListPage.ShowClientsList();
    }
}