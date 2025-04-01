using AnikLak.XAMLTemplates;

namespace AnikLak;

public partial class Materials : ContentPage
{
	public Materials()
	{
		InitializeComponent();

		stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
        stackLayout.Add(new MaterialTemplate());
    }
}