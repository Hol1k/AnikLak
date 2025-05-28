using AnikLak.ModelsDto.Appointments;
using AnikLak.ModelsDto.Clients;
using AnikLak.Subsystems.Appointments;
using AnikLak.XAMLTemplates;
using Microsoft.Maui.Layouts;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Clients;

public partial class ClientEditing : ContentPage
{
    private ClientsList _clientsListPage;
    private readonly string _addNewClientlUrl = "http://hol1k.ru:5000/clients/add-new-client";
    private readonly string _updateClientValuesUrl = "http://hol1k.ru:5000/clients/update-client-values";
    private readonly string _getAllAppointmentsUrl = "http://hol1k.ru:5000/appointments/get-appointments-list";

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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowAppointmentsList();
    }

    private async Task ShowAppointmentsList()
    {
        _lastAppointmentsContainer.Children.Clear();

        List<AppointmentDto> appointmentList = new List<AppointmentDto>();
        appointmentList = await GetAppointmentsAsync();

        foreach (AppointmentDto appointment in appointmentList)
        {
            if (appointment.ClientId == _id)
                _lastAppointmentsContainer.Insert(0, new LittleAppointmentTemplate(appointment));
        }
    }

    private async Task<List<AppointmentDto>> GetAppointmentsAsync()
    {
        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(_getAllAppointmentsUrl);

            if (response.IsSuccessStatusCode)
            {
                var appointmentList = await response.Content.ReadFromJsonAsync<List<AppointmentDto>>();
                return appointmentList ?? new List<AppointmentDto>();
            }
            else
            {
                Console.WriteLine($"������ API: {response.StatusCode}");
                return new List<AppointmentDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"���������� ��� �������: {ex.Message}");
            return new List<AppointmentDto>();
        }
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

    private async void AddNewAppointment(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing(new AppointmentDto(), null));
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
            await DisplayAlert("������", "��������� ��� �������", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PostAsJsonAsync(_addNewClientlUrl, clientToAdd);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�����", "������ ������� ��������", "OK");
            }
            else
            {
                await DisplayAlert("������", $"������ �������: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("����", $"������ �������: {ex.Message}", "OK");
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
            await DisplayAlert("������", "��������� ��� �������", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PutAsJsonAsync(_updateClientValuesUrl, clientToAdd);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("�����", "������ � ������� ������� ���������", "OK");
            }
            else
            {
                await DisplayAlert("������", $"������ �������: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("����", $"������ �������: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        await _clientsListPage.ShowClientsList();
    }
}