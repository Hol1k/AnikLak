using AnikLak.ModelsDto;
using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Materials;

public partial class MaterialsList : ContentPage
{
	public MaterialsList()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMaterialsList();
    }

    public async Task UpdateMaterialsList()
    {
        await LoadMaterialsList();
    }

    private async Task LoadMaterialsList()
    {
        stackLayout.Children.Clear();

        List<MaterialDto> materials = new List<MaterialDto>();
        materials = await GetMaterialsAsync();

        foreach (MaterialDto material in materials)
        {
            stackLayout.Add(new MaterialTemplate(material));
        }
    }

    private async void AddMaterialsClicked(object? sender, EventArgs e)
    {
        var popup = new AddMaterialsPopup(this);

        await this.ShowPopupAsync(popup);
    }

    private async Task<List<MaterialDto>> GetMaterialsAsync()
    {
        var client = new HttpClient();
        var url = "http://hol1k.ru:5000/materials/get-materials-list";

        try
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var materials = await response.Content.ReadFromJsonAsync<List<MaterialDto>>();
                return materials ?? new List<MaterialDto>();
            }
            else
            {
                Console.WriteLine($"Ошибка API: {response.StatusCode}");
                return new List<MaterialDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение при запросе: {ex.Message}");
            return new List<MaterialDto>();
        }
    }

}