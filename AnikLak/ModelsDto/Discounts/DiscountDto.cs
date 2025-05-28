using System.Text;

namespace AnikLak.ModelsDto.Discounts
{
    public class DiscountDto
    {
        public string Name { get; set; } = "";
        public double Value { get; set; }

        public static List<DiscountDto> DeserializeDiscountListString(string discountListString)
        {
            if (string.IsNullOrWhiteSpace(discountListString))
                return new List<DiscountDto>();

            string[] discountStrings = discountListString.Split(';');

            List<DiscountDto> discountList = new List<DiscountDto>();
            foreach (string discountString in discountStrings)
            {
                string[] discountData = discountString.Split("&");
                DiscountDto discountDto = new DiscountDto()
                {
                    Name = discountData[0],
                    Value = Convert.ToDouble(discountData[1])
                };
                discountList.Add(discountDto);
            }

            return discountList;
        }

        public static string SerializeDiscountListString(List<DiscountDto> discountList)
        {
            if (!discountList.Any()) return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (DiscountDto discount in discountList)
            {
                sb.Append($"{discount.Name}&{discount.Value.ToString("0.##")};");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
