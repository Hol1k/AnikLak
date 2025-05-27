namespace AnikLak.ModelsDto.Clients
{
    public class UpdateClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Note { get; set; } = "";
    }
}
