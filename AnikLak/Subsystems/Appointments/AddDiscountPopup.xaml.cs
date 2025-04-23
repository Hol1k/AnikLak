using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddDiscountPopup : Popup
{
    private VerticalStackLayout _chosedDiscountsContainer;
    private AppointmentEditing _appointmentEditingWindow;


    public AddDiscountPopup(VerticalStackLayout chosedDiscountsContainer, AppointmentEditing appointmentEditingWindow)
    {
        _chosedDiscountsContainer = chosedDiscountsContainer;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyValue();

        if (_discountValue.Text != "0")
        {
            _chosedDiscountsContainer.Add(new AppointmentDiscountTemplate(
            _discountName.Text,
            (float)Convert.ToDouble(_discountValue.Text),
            _chosedDiscountsContainer));

            _appointmentEditingWindow.RecalculatePrice();
        }

        await CloseAsync();
    }

    private void VerifyValue()
    {
        if (_discountValue.Text == "" || Convert.ToInt32(_discountValue.Text) < 0)
        {
            _discountValue.Text = "0";
        }
    }

    private void VerifyValue(object? sender, EventArgs e)
    {
        VerifyValue();
    }
}