namespace AnikLak.ModelsDto.Appointments
{
    public class AddAppointmentDto
    {
        public int? ClientId { get; set; }
        public string Date { get; set; } = "";
        public string Time { get; set; } = "";
        public string Status { get; set; } = "";
        public string ServiceListString { get; set; } = "";
        public string DiscountListString { get; set; } = "";
        public string ToolListString { get; set; } = "";
        public string MaterialListString { get; set; } = "";
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
    }
}