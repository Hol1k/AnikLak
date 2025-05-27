using AnikLak.ModelsDto.Tools;
using System.Net.Http.Json;

namespace AnikLak.Subsystems.Tools;

public partial class ToolEditing : ContentPage
{
    private ToolsList _toolsListPage;
    private readonly string _addNewToolUrl = "http://hol1k.ru:5000/tools/add-new-tool";
    private readonly string _updateToolValuesUrl = "http://hol1k.ru:5000/tools/update-tool-values";

    private int? _id;

	public ToolEditing(ToolDto tool, ToolsList toolsListPage)
	{
		InitializeComponent();

        _toolsListPage = toolsListPage;

        _id = tool.Id;
        _nameField.Text = tool.Name;
        _countField.Text = tool.Count.ToString();
        _functioningCountField.Text = tool.FunctioningCount.ToString();
        _functioningMaxCountField.Text = '/' + tool.Count.ToString();
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        if (_id == null)
            await AddNewTool();
        else
            await EditTool();
    }

    private async Task AddNewTool()
    {
        var toolToAdd = new AddToolDto()
        {
            Name = _nameField.Text.Trim(),
            Count = Convert.ToInt32(_countField.Text),
            FunctioningCount = Convert.ToInt32(_functioningCountField.Text)
        };

        if (toolToAdd.Count <= 0 || toolToAdd.FunctioningCount < 0)
        {
            await DisplayAlert("Ошибка", "Нет корректных данных для отправки", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PostAsJsonAsync(_addNewToolUrl, toolToAdd);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Инструмент успешно добавлен", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", $"Ошибка сервера: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        await _toolsListPage.ShowToolsList();
    }

    private async Task EditTool()
    {
        var toolToUpdate = new ToolDto()
        {
            Id = _id,
            Name = _nameField.Text.Trim(),
            Count = Convert.ToInt32(_countField.Text),
            FunctioningCount = Convert.ToInt32(_functioningCountField.Text)
        };

        if (Convert.ToInt32(_countField.Text) <= 0 || Convert.ToInt32(_functioningCountField.Text) < 0 ||
            Convert.ToInt32(_functioningCountField.Text) > Convert.ToInt32(_countField.Text))
        {
            await DisplayAlert("Ошибка", "Нет корректных данных для отправки", "OK");
            return;
        }

        var client = new HttpClient();

        try
        {
            var response = await client.PutAsJsonAsync(_updateToolValuesUrl, toolToUpdate);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Успех", "Данные об инструменте успшено обновлены", "OK");
            }
            else
            {
                await DisplayAlert("Ошибка", $"Ошибка сервера: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Сеть", $"Ошибка запроса: {ex.Message}", "OK");
        }

        await Navigation.PopAsync();

        await _toolsListPage.ShowToolsList();
    }
}