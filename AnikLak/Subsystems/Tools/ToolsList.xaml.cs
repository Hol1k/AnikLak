using AnikLak.ModelsDto.Tools;
using AnikLak.XAMLTemplates;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Tools;

public partial class ToolsList : ContentPage
{
    private readonly string _getAllToolsUrl = "http://hol1k.ru:5000/tools/get-tools-list";

	public ToolsList()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ShowToolsList();
    }

    public async Task ShowToolsList()
    {
        _stackLayout.Children.Clear();

        List<ToolDto> toolList = new List<ToolDto>();
        toolList = await GetToolsAsync();

        foreach (ToolDto tool in toolList)
        {
            _stackLayout.Add(new ToolTemplate(tool, this));
        }
    }

    private async Task<List<ToolDto>> GetToolsAsync()
    {
        var client = new HttpClient();

        try
        {
            var response = await client.GetAsync(_getAllToolsUrl);
            if (response.IsSuccessStatusCode)
            {
                var tools = await response.Content.ReadFromJsonAsync<List<ToolDto>>();
                return tools ?? new();
            }
            else
            {
                Console.WriteLine($"Ошибка API: {response.StatusCode}");
                return new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исклюение при запрсое: {ex.Message}");
            return new();
        }
    }

    private async void AddNewToolClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolEditing(new ToolDto(), this));
    }
}