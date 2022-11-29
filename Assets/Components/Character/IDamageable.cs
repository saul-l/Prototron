public delegate void DeathNotify();

public interface IDamageable
{
    void ApplyDamage(int damageAmount);
    event DeathNotify EventDead;
}

