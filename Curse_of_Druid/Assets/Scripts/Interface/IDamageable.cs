public interface IDamageable
{
    void GetDamage(int amount, DAMAGE_TYPE dmgType);
    void Dead();
}

public enum DAMAGE_TYPE
{
    Melee,
    Projectile,
    Falling,
    Flame,
    Step, 
    Machete,
    Pickaxe
}