using AnikLak.XAMLTemplates;
using Microsoft.Maui.Layouts;

namespace AnikLak.Subsystems.Clients;

public partial class ClientEditing : ContentPage
{
    int clientId;

	public ClientEditing()
	{
		InitializeComponent();

        lastAppointmentsContainer.Add(new LittleAppointmentTemplate());
        lastAppointmentsContainer.Add(new LittleAppointmentTemplate());
    }

    public ClientEditing(int clientId)
    {
        this.clientId = clientId;

        double height = 0.0;
        height = note.ComputeDesiredSize(note.Width, double.PositiveInfinity).Height;
        ((Border)note.Parent).HeightRequest = height;

        InitializeComponent();
    }

    private void TextChanged(object sender, TextChangedEventArgs e)
    {
        double height = 0.0;
        if (sender is Editor editor)
        {
            height = editor.ComputeDesiredSize(editor.Width, double.PositiveInfinity).Height;
            ((Border)editor.Parent).HeightRequest = height;
        }
    }

    private async void ApplyEditing(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}