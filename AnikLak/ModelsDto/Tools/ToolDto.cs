using System.Text;

namespace AnikLak.ModelsDto.Tools
{
    public class ToolDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; }
        public int FunctioningCount { get; set; }

        public static List<ToolDto> DeserializeToolListString(string toolListString)
        {
            if (string.IsNullOrWhiteSpace(toolListString))
                return new List<ToolDto>();

            string[] toolStrings = toolListString.Split(';');

            List<ToolDto> toolList = new List<ToolDto>();
            foreach (string toolString in toolStrings)
            {
                string[] toolData = toolString.Split("&");
                ToolDto toolDto = new ToolDto()
                {
                    Id = string.IsNullOrEmpty(toolData[0]) ? null : Convert.ToInt32(toolData[0]),
                    Name = toolData[1],
                    Count = Convert.ToInt32(toolData[2])
                };
                toolList.Add(toolDto);
            }

            return toolList;
        }

        public static string SerializeToolListString(List<ToolDto> toolList)
        {
            if (!toolList.Any()) return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (ToolDto tool in toolList)
            {
                sb.Append($"{tool.Id}&{tool.Name}&{tool.Count};");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
