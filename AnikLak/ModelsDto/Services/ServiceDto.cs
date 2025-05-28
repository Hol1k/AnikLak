using System.Text;

namespace AnikLak.ModelsDto.Services
{
    public class ServiceDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }

        public static List<ServiceDto> DeserializeServiceListString(string serviceListString)
        {
            if (string.IsNullOrWhiteSpace(serviceListString))
                return new List<ServiceDto>();

            string[] serviceStrings = serviceListString.Split(';');

            List<ServiceDto> serviceList = new List<ServiceDto>();
            foreach (string serviceString in serviceStrings)
            {
                string[] serviceData = serviceString.Split("&");
                ServiceDto serviceDto = new ServiceDto()
                {
                    Name = serviceData[0],
                    Price = Convert.ToDecimal(serviceData[1])
                };
                serviceList.Add(serviceDto);
            }

            return serviceList;
        }

        public static string SerializeServiceListString(List<ServiceDto> serviceList)
        {
            if (!serviceList.Any()) return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (ServiceDto service in serviceList)
            {
                sb.Append($"{service.Name}&{service.Price.ToString("0.##")};");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
