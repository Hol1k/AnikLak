using CommunityToolkit.Maui.Views;
using AnikLak.XAMLTemplates.AppointmentEditingFields;
using AnikLak.ModelsDto.Appointments;
using AnikLak.ModelsDto.Services;
using AnikLak.ModelsDto.Discounts;
using AnikLak.ModelsDto.Tools;
using AnikLak.ModelsDto.Materials;
using AnikLak.ModelsDto.Clients;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System;

namespace AnikLak.Subsystems.Appointments;

public partial class AppointmentEditing : ContentPage
{
    private readonly string _getAllClientsUrl = "http://hol1k.ru:5000/clients/get-clients-list";
    private readonly string _addNewAppointmentlUrl = "http://hol1k.ru:5000/appointments/add-new-appointment";
    private readonly string _updateClientValuesUrl = "http://hol1k.ru:5000/appointments/update-appointment-values";

    private AppointmentsList? _appointmentListPage;

	private int? _appointmentId;
    public int? ClientId;

    private List<ServiceDto> _services = new();
    private List<DiscountDto> _discounts = new();
    private List<ToolDto> _tools = new();
    private List<MaterialDto> _materials = new();

    public AppointmentEditing(AppointmentDto appointmentInfo, AppointmentsList? appointmentsListPage)
    {
        InitializeComponent();

        _appointmentListPage = appointmentsListPage;
        _appointmentId = appointmentInfo.Id;
        ClientId = appointmentInfo.ClientId;

        _clientField.Text = appointmentInfo.ClientName;

        if (string.IsNullOrEmpty(appointmentInfo.Date) || string.IsNullOrEmpty(appointmentInfo.Time))
            _date.Date = DateTime.Now;
        else
            _date.Date = new DateTime(DateOnly.Parse(appointmentInfo.Date), TimeOnly.Parse(appointmentInfo.Time));

        _statusField.Text = appointmentInfo.Status;

        _services = ServiceDto.DeserializeServiceListString(appointmentInfo.ServiceListString);
        foreach (ServiceDto service in _services)
        {
            _chosedServicesContainer.Add(new AppointmentServiceTemplate(
                service.Name,
                service.Price,
                _chosedServicesContainer,
                this,
                _services));
        }

        _discounts = DiscountDto.DeserializeDiscountListString(appointmentInfo.DiscountListString);
        foreach (DiscountDto discount in _discounts)
        {
            _chosedDiscountsContainer.Add(new AppointmentDiscountTemplate(
                discount.Name,
                (float)discount.Value,
                _chosedDiscountsContainer,
                this,
                _discounts));
        }

        _tools = ToolDto.DeserializeToolListString(appointmentInfo.ToolListString);
        foreach (ToolDto tool in _tools)
        {
            _chosedToolsContainer.Add(new AppointmentToolTemplate(
                tool.Name,
                1,
                _chosedToolsContainer,
                _tools));
        }

        _materials = MaterialDto.DeserializeMaterialListString(appointmentInfo.MaterialListString);
        foreach (MaterialDto material in _materials)
        {
            _chosedMaterialsContainer.Add(new AppointmentMaterialTemplate(
                material.Name,
                Random.Shared.Next() % 2 + 1,
                _chosedMaterialsContainer,
                _materials));
        }

        _priceField.Text= appointmentInfo.FinalPrice.ToString("0.##");
    }

    private async void ChoseClient(object? sender, EventArgs e)
    {
        var popup = new ChoseAppointmentClientPopup(_clientField, this);

        await ShowClientsList(popup);

        await this.ShowPopupAsync(popup);
    }

    public async Task ShowClientsList(ChoseAppointmentClientPopup popup)
    {
        List<ClientDto> clientList = new List<ClientDto>();
        clientList = await GetClientsAsync();

        foreach (ClientDto client in clientList)
        {
            popup.AddNewClient(client);
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

    private async void ChangeStatus(object? sender, EventArgs e)
    {
        var popup = new ChangeAppointmentStatusPopup(_statusField);

        await this.ShowPopupAsync(popup);
    }

    private async void AddService(object? sender, EventArgs e)
    {
        var popup = new AddServicePopup(_chosedServicesContainer, this, _services);

        await this.ShowPopupAsync(popup);
    }

    private async void AddDiscount(object? sender, EventArgs e)
    {
        var popup = new AddDiscountPopup(_chosedDiscountsContainer, this, _discounts);

        await this.ShowPopupAsync(popup);
    }

    private async void AddTool(object? sender, EventArgs e)
    {
        var popup = new AddToolPopup(_chosedToolsContainer, _tools);

        await this.ShowPopupAsync(popup);
    }

    private async void AddMaterial(object? sender, EventArgs e)
    {
        var popup = new AddMaterialPopup(_chosedMaterialsContainer, _materials);

        await this.ShowPopupAsync(popup);
    }

    public void RecalculatePrice()
    {
        decimal basePrice = 0;
        foreach (var child in _chosedServicesContainer)
        {
            if (child is AppointmentServiceTemplate service)
            {
                basePrice += service.Price;
            }
        }
        float discount = 0f;
        foreach (var child in _chosedDiscountsContainer)
        {
            if (child is AppointmentDiscountTemplate discountTemplate)
            {
                discount += discountTemplate.DiscountValue;
            }
        }

        _priceField.Text = (basePrice * (decimal)(1 - discount)).ToString("0.##");
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        if (_appointmentId == null)
            await AddNewAppointment();
        else
            await EditAppointment();
    }

    private async Task AddNewAppointment()
    {
        decimal basePrice = 0;
        foreach (var child in _chosedServicesContainer)
        {
            if (child is AppointmentServiceTemplate service)
            {
                basePrice += service.Price;
            }
        }

        var appointmentToAdd = new AddAppointmentDto()
        {
            Id = 0,
            ClientId = ClientId,
            Date = new DateOnly(_date.Date.Year, _date.Date.Month, _date.Date.Day).ToString(),
            Time = new TimeOnly(_date.Date.Hour, _date.Date.Minute, _date.Date.Second).ToString(),
            Status = _statusField.Text,
            ServiceListString = ServiceDto.SerializeServiceListString(_services),
            DiscountListString = DiscountDto.SerializeDiscountListString(_discounts),
            ToolListString = ToolDto.SerializeToolListString(_tools),
            MaterialListString = MaterialDto.SerializeMaterialListString(_materials),
            BasePrice = basePrice,
            FinalPrice = Convert.ToDecimal(_priceField.Text)
        };

        if (string.IsNullOrEmpty(_clientField.Text))
        {
            await DisplayAlert("Ошибка", "Выберите клиента", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var responseAppointment = await client.PostAsJsonAsync(_addNewAppointmentlUrl, appointmentToAdd);

            if (responseAppointment.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Запись успешно добавлена", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", $"Ошибка сервера: {responseAppointment.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        if (_appointmentListPage != null)
            await _appointmentListPage.ShowAppointmentsList();
    }

    private async Task EditAppointment()
    {
        decimal basePrice = 0;
        foreach (var child in _chosedServicesContainer)
        {
            if (child is AppointmentServiceTemplate service)
            {
                basePrice += service.Price;
            }
        }

        var appointmentToUpdate = new UpdateAppointmentDto()
        {
            Id = _appointmentId,
            ClientId = ClientId,
            Date = new DateOnly(_date.Date.Year, _date.Date.Month, _date.Date.Day).ToString(),
            Time = new TimeOnly(_date.Date.Hour, _date.Date.Minute, _date.Date.Second).ToString(),
            Status = _statusField.Text,
            ServiceListString = ServiceDto.SerializeServiceListString(_services),
            DiscountListString = DiscountDto.SerializeDiscountListString(_discounts),
            ToolListString = ToolDto.SerializeToolListString(_tools),
            MaterialListString = MaterialDto.SerializeMaterialListString(_materials),
            BasePrice = basePrice,
            FinalPrice = Convert.ToDecimal(_priceField.Text)
        };

        if (string.IsNullOrEmpty(_clientField.Text))
        {
            await DisplayAlert("Ошибка", "Выберите клиента", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PutAsJsonAsync(_updateClientValuesUrl, appointmentToUpdate);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Данные о записи успшено обновлены", "OK");
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

        if (_appointmentListPage != null)
            await _appointmentListPage.ShowAppointmentsList();
    }
}