using System.Text;

namespace AnikLak.ModelsDto.Materials
{
    public class MaterialDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; }

        public static List<MaterialDto> DeserializeMaterialListString(string materialListString)
        {
            if (string.IsNullOrWhiteSpace(materialListString))
                return new List<MaterialDto>();

            string[] materialStrings = materialListString.Split(';');

            List<MaterialDto> materialList = new List<MaterialDto>();
            foreach (string materialString in materialStrings)
            {
                string[] materialData = materialString.Split("&");
                MaterialDto materialDto = new MaterialDto()
                {
                    Id = string.IsNullOrEmpty(materialData[0]) ? null : Convert.ToInt32(materialData[0]),
                    Name = materialData[1],
                    Count = Convert.ToInt32(materialData[2])
                };
                materialList.Add(materialDto);
            }

            return materialList;
        }

        public static string SerializeMaterialListString(List<MaterialDto> materialList)
        {
            if (!materialList.Any()) return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (MaterialDto material in materialList)
            {
                sb.Append($"{material.Id}&{material.Name}&{material.Count};");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
