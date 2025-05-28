using AnikLak.ModelsDto.Appointments;
using AnikLak.XAMLTemplates;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Appointments;

public partial class AppointmentsList : ContentPage
{
    private readonly string _getAllAppointmentsUrl = "http://hol1k.ru:5000/appointments/get-appointments-list";

    public AppointmentsList()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowAppointmentsList();
    }

    public async Task ShowAppointmentsList()
    {
        _stackLayout.Children.Clear();

        List<AppointmentDto> appointmentList = new List<AppointmentDto>();
        appointmentList = await GetAppointmentsAsync();

        foreach (AppointmentDto appointment in appointmentList)
        {
            _stackLayout.Insert(0, new AppointmentTemplate(appointment, this));
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
                Console.WriteLine($"Ошибка API: {response.StatusCode}");
                return new List<AppointmentDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при запросе: {ex.Message}");
            return new List<AppointmentDto>();
        }
    }

    private async void AddNewAppointmentClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new AppointmentEditing(new AppointmentDto(), this));
    }
}