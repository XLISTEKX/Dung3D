using System;
using Godot;

namespace XGeneric.Statics
{
	public static class XStatic
	{
		public static bool GetScriptInNode<T>(Node3D node, out T script)
		{
			if(node is T t)
			{
				script = t;
				return true;
			}
			
			foreach(Node nodeS in node.GetChildren())
			{
				if(nodeS is T temp)
				{
					script = temp;
					return true;
				}
					
			}
			script = default;
			return false;
		}
    }
}