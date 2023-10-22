
using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace XGeneric.Inventory
{
	public static class InventorySystem
	{
		
		public static void ReadFromJson(string path)
		{
			
			
			// if(File.Exists(path))
			// {
				
			// 	var data = Json.ParseString(File.ReadAllText(path));
			// 	return data;
			// }
			// else
			// {
			// 	return new Variant();
			// }
			using var file = FileAccess.Open("res://Scripts/Items/Items.json", FileAccess.ModeFlags.Read);
   			 string content = file.GetAsText();


			Item deptObj = JsonSerializer.Deserialize<Item>(content);
				
		}
	}
}