
using BulletInterface;

namespace BulletInterface
{
    public interface IDamageDillerEnemy
    {
        public float GetDamageBullet();

        public TypeDamageBullet GetTypeDamage();
    }

    public interface ILoaderBullet
    {
        public float GetLaunchDamage();

        public TypeDamageBullet GetLaunchTypeDamage();
    }

    public enum TypeDamageBullet
    {
        magicDamage,
        physicalDamage
    }
}