using System.Collections.Generic;
using Godot;
using XGeneric.Inventory;
using XGeneric.Statistic;

public partial class PlayerControler : Node3D, HealthSystem, IInventory
{
	[Export] public float staminaMax = 10;
	UIGameplay uIGameplay;
	Inventory playerInventory;
	public StatsSystem stats;

	List<WorldItem> itemsToPickUp;
	
	#region HealthSystem
	[Export] public int maxHealth = 1;
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
		uIGameplay = GetNode<UIGameplay>("/root/TestSite/UI/Gameplay");
		health = maxHealth;
		uIGameplay.InitUI(this);
		
		playerInventory = new(30);
		playerInventory.AddItem(InventorySystem.GetItemByID(0));
		playerInventory.AddItem(InventorySystem.GetItemByID(1));
		
		UpdateUI();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if(@event is InputEventKey key && key.KeyLabel == Key.E && key.Pressed)
		{
			if(itemsToPickUp == null)
				return;
			
			playerInventory.AddItem(itemsToPickUp[0].item);
			Node3D item = itemsToPickUp[0];
			item.QueueFree();
		}
	}
	public void UpdateUI()
	{
		uIGameplay.UpdateHealth(health);
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
		
		uIGameplay.UpdateStamina(stamina);
	}
	
	#region Health Interface
	public void TakeDamage(int damage)
	{
		health -= damage;
		UpdateUI();
		if(health <= 0)
		{
			health = 0;
			Dead();
		}
	}
	public void Dead()
	{
		
	}
	#endregion

	#region Inventory Interface
	public Inventory GetInventory()
	{
		return playerInventory;
	}

	public void AddToPickUp(WorldItem worldItem)
	{
		if(itemsToPickUp == null)
		{
			itemsToPickUp = new()
			{
				worldItem
			};
		}
		else
		{
			itemsToPickUp.Add(worldItem);
		}
		GD.Print(itemsToPickUp.Count);
	}

	public void RemoveToPickUp(WorldItem worldItem)
	{
		itemsToPickUp.Remove(worldItem);
		
		if(itemsToPickUp.Count == 0)
			itemsToPickUp = null;
	}
	
	#endregion
}
