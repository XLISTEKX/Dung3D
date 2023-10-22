using Godot;

namespace XGeneric.Inventory
{
	public partial class Item : Resource
	{
		[Export] public string name { get; set;}
		[Export] public int id{ get; set;}
		[Export] public string iconPath{ get; set;}
		
		public Item(string name, int id, string iconPath)
		{
			this.name = name;
			this.id = id;
			this.iconPath = iconPath;
		}
		
	}
}