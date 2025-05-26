using AnikLak.ModelsDto;

namespace AnikLak.XAMLTemplates;

public partial class MaterialTemplate : ContentView
{
	public MaterialTemplate(MaterialDto material)
	{
		InitializeComponent();

		_name.Text = material.Name;
		_count.Text = material.Count.ToString();
	}
}