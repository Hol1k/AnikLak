using AnikLak.ModelsDto.Discounts;
using AnikLak.XAMLTemplates.AppointmentEditingFields;
using CommunityToolkit.Maui.Views;

namespace AnikLak.Subsystems.Appointments;

public partial class AddDiscountPopup : Popup
{
    private VerticalStackLayout _chosedDiscountsContainer;
    private AppointmentEditing _appointmentEditingWindow;

    private List<DiscountDto> _discounts;

    public AddDiscountPopup(VerticalStackLayout chosedDiscountsContainer, AppointmentEditing appointmentEditingWindow, List<DiscountDto> discounts)
    {
        _chosedDiscountsContainer = chosedDiscountsContainer;
        _appointmentEditingWindow = appointmentEditingWindow;

        InitializeComponent();

        _discounts = discounts;
    }

    private async void ApplyAdding(object? sender, EventArgs e)
    {
        VerifyValue();

        float nonEmptyDiscount= (float)Convert.ToDecimal((_discountValue.Text == null || _discountValue.Text == string.Empty) ? "0" : _discountValue.Text);

        if (nonEmptyDiscount > 0)
        {
            _chosedDiscountsContainer.Add(new AppointmentDiscountTemplate(
                _discountName.Text,
                nonEmptyDiscount,
                _chosedDiscountsContainer,
                _appointmentEditingWindow,
                _discounts));

            _discounts.Add(new DiscountDto()
            {
                Name = _discountName.Text,
                Value = nonEmptyDiscount
            });

            _appointmentEditingWindow.RecalculatePrice();
        }

        await CloseAsync();
    }

    private void VerifyValue()
    {
        if (_discountValue.Text == null || _discountValue.Text == string.Empty)
            return;

        _discountValue.Text = _discountValue.Text.Replace('.', ',');

        if (Convert.ToDouble(_discountValue.Text) <= 0)
            _discountValue.Text = string.Empty;
    }

    private void VerifyValue(object? sender, EventArgs e)
    {
        VerifyValue();
    }
}