using AnikLak.ModelsDto;
using AnikLak.XAMLTemplates;
using CommunityToolkit.Maui.Views;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Materials;

public partial class AddMaterialsPopup : Popup
{
    private MaterialsList _materialsListPage;

    private List<MaterialToAddTemplate> _addingMaterials;

    public AddMaterialsPopup(MaterialsList materialsListPage)
	{
		InitializeComponent();

        _addingMaterials = new List<MaterialToAddTemplate>();
        _materialsListPage = materialsListPage;
    }

    private void AddNewMaterialRaw(object? sender, EventArgs e)
    {
        MaterialToAddTemplate newMaterial = new MaterialToAddTemplate();
        _addingMaterials.Add(newMaterial);
        addingMaterialsListContainer.Add(new MaterialToAddTemplate());
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        var materialsToSend = new List<AddMaterialDto>();

        foreach (var template in _addingMaterials)
        {
            string name = template.Name.Trim();
            int count = template.Count;

            if (!string.IsNullOrWhiteSpace(name) && count > 0)
            {
                materialsToSend.Add(new AddMaterialDto
                {
                    Name = name,
                    Count = count
                });
            }
        }

        if (materialsToSend.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет корректных данных для отправки", "OK");
            return;
        }

        var client = new HttpClient();
        var url = "http://hol1k.ru:5000/materials/add-material";

        try
        {
            var response = await client.PutAsJsonAsync(url, materialsToSend);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Успех", "Материалы успешно добавлены", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Ошибка сервера: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await CloseAsync();

        await _materialsListPage.UpdateMaterialsList();
    }
}