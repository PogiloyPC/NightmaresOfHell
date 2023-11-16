
using BulletInterface;

namespace BulletInterface
{
    public interface IDamageDillerEnemy
    {
        public float GetDamageBullet();

        public TypeDamage GetTypeDamage();
    }

    public interface ILoaderBullet
    {
        public float GetLaunchDamage();

        public TypeDamage GetLaunchTypeDamage();
    }

    public enum TypeDamage
    {
        magicDamage,
        physicalDamage
    }
}