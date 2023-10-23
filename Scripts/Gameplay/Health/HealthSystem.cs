
public interface HealthSystem
{
	int Health {get;}
	
	
	public void TakeDamage(int damage);
	
	public void RegenHealth()
	{
		return;
	}

	public void Dead()
	{
		return;
	}
}