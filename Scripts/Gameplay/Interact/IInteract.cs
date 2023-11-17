using Godot;

namespace XGeneric.System
{
	
	
	/// <summary>
	/// Interact interface. Use to interact with objects
	/// </summary>
	public interface IInteract
	{
		/// <summary>
		/// Method to <c>interact</c> with objects
		/// </summary>
		/// <param name="interactObject">As parameter give <c>Node3D</c>, that is calling the method</param>
		public void Interact(Node3D interactObject);
	}
}