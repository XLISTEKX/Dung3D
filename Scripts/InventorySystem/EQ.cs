using XGeneric.Weapons;

namespace XGeneric.Inventory
{
	public class EQ
	{
		public InvItem[] EQSlots;
		public ItemType[] eqSlotsTypes;
		
		public EQ(InvItem[] eqSlots, ItemType[] itemTypes)
		{
			EQSlots = eqSlots;
			eqSlotsTypes = itemTypes;
		}
	}
}