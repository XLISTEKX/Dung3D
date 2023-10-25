using Godot;
using XGeneric.Inventory;

public partial class PlayerControler : Node3D, HealthSystem
{
	[Export] public float staminaMax = 10;
	UIGameplay uIGameplay;
	Inventory playerInventory;

	#region HealthSystem
	[Export] int maxHealth = 1;
	int health = 10;
	
	public int Health
	{
		get
		{
			return health;
		}
	}
	
	#endregion


	public float stamina = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		uIGameplay = GetNode<UIGameplay>("../../UIGameplay");
		
		uIGameplay.InitUI(this);
		
		// playerInventory = new(10);
		
		// playerInventory.AddItem(InventorySystem.GetItemFromJson(0));
		
		 health = maxHealth;
	}

	public void UpdateUI()
	{
		uIGameplay.UpdateStamina(stamina);
	}

	public void UpdateStamina(float valueToAdd)
	{
		var newValue = valueToAdd + stamina;

		if(newValue <= staminaMax)
		{
			if(newValue <= 0)
			{
				stamina = 0;
			}
			else
			{
				stamina = newValue;
			}
		}
		else
		{
			stamina = staminaMax;
		}
		
		UpdateUI();
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		
		if(health <= 0)
		{
			Dead();
		}
	}
	public void Dead()
	{
		GD.Print(InventorySystem.GetAllItems()[0].ID);
	}
}
