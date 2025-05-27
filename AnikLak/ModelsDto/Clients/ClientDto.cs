using AnikLak.ModelsDto.Appointments;

namespace AnikLak.ModelsDto.Clients
{
    public class ClientDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Note { get; set; } = "";
        public int AppointmentsCount { get; set; }
        public List<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
    }
}
