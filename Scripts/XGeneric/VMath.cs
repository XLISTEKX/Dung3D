using Godot;

namespace XGeneric.Utilities
{
	public static class MathV
	{
		public static float GetAngleToVector(Vector3 destinaton, Vector3 objectGlobalPosition)
		{
			Vector3 look = destinaton - objectGlobalPosition;
			look.Y = 0;
		
			return look.SignedAngleTo(new Vector3(0,0,1), new Vector3(0,-1,0));
		}
	}
}