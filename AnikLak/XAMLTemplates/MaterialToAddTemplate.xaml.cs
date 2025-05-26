namespace AnikLak.XAMLTemplates;

public partial class MaterialToAddTemplate : ContentView
{
	public string Name => _name.Text;
	public int Count => Convert.ToInt32(_count.Text);

	public MaterialToAddTemplate()
	{
		InitializeComponent();
	}
}