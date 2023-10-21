using Godot;

namespace XGeneric.Inventory
{
	public partial class Item
	{
		public string name { get; set;}
		public int id{ get; set;}
		public string iconPath{ get; set;}
		
		public Item(string name, int id, string iconPath)
		{
			this.name = name;
			this.id = id;
			this.iconPath = iconPath;
		}
		
	}
}