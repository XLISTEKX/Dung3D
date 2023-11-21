using Godot;
namespace XGeneric.Utilities
{
	public static class XCamera
	{
		public static Vector3 ScreenToWorldPoint(Vector2 position, Camera3D camera)
		{
			var SpaceState = camera.GetWorld3D().DirectSpaceState;
			
			var rayOrigin = camera.ProjectRayOrigin(position);
			var rayEnd = rayOrigin + camera.ProjectRayNormal(position) * 1000;
			var querry = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
			
			var rayArray = SpaceState.IntersectRay(querry);
			
			if(rayArray.Count <= 0)
				return Vector3.Zero;
			
			return (Vector3) rayArray["position"];
		}
		
		public static T GetScriptInNode<T>(Node3D node)
		{
			if(node is T t)
				return t;
			
			foreach(Node nodeS in node.GetChildren())
			{
				if(nodeS is T temp)
					return temp;
			}
			
			return default;
		}
	}
}