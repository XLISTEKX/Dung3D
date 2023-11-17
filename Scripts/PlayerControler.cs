using System.Collections.Generic;
using Godot;
using XGeneric.Inventory;
using XGeneric.Statistic;
using XGeneric.System;

public partial class PlayerControler : Node3D, HealthSystem, IInventory, IInteractMain, IStats
{
	[Export] public float staminaMax = 10;
	UIGameplay uIGameplay;
	Inventory playerInventory;
	[Export] StatsSystem stats;

	List<IInteract> interactItems;
	
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

	public override void _Ready()
	{
		uIGameplay = GetNode<UIGameplay>("/root/TestSite/UI/Gameplay");
		health = maxHealth;
		uIGameplay.InitUI(this);
		
		InvItem[] invs = new InvItem[3];
		ItemType[] types = new ItemType[]
		{
			ItemType.Weapon,
			ItemType.Weapon,
			ItemType.Weapon
		};
		
		playerInventory = new(30, new EQ(invs, types));
		playerInventory.AddItem(InventorySystem.GetItemByID(0));
		playerInventory.AddItem(InventorySystem.GetItemByID(1));
		
		UpdateUI();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if(Input.IsActionJustPressed("Use"))
		{
			if(interactItems == null)
				return;
			
			interactItems[0].Interact(this);
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
	#endregion
	
	#region Interact Interface
	public void AddInteract(IInteract interactObject)
	{
		if(interactItems == null)
		{
			interactItems = new()
			{
				interactObject
			};
		}
		else
		{
			interactItems.Add(interactObject);
		}
	}

	public void RemoveInteract(IInteract interactObject)
	{
		interactItems.Remove(interactObject);
		
		if(interactItems.Count == 0)
			interactItems = null;
	}
	#endregion
	
	public StatsSystem GetStats()
    {
        return stats;
    }
}
