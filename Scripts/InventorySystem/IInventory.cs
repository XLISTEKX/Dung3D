namespace XGeneric.Inventory
{
	public interface IInventory
	{
		public Inventory GetInventory();
		
		public void AddToPickUp(WorldItem worldItem);
		
		public void RemoveToPickUp(WorldItem worldItem);
	}
}