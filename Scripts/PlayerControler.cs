using Godot;
using XGeneric.Inventory;

public partial class PlayerControler : Node3D, HealthSystem
{
	[Export] public float staminaMax = 10;
	UIGameplay uIGameplay;
	public Inventory playerInventory;

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
		
		
		UpdateUI();
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
}
