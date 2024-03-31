using System;
using System.Windows.Forms;

namespace Rpg.Files
{
    class Mob
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsBuyed { get; private set; }
        public Mob(string name, int health, int damage, int maxHealth, bool isBuyed)
        {
            Name = name;
            Health = health;
            Damage = damage;
            MaxHealth = maxHealth;
            IsBuyed = isBuyed;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
            }
        }
        public void SetDamage(int objectDamage)
        {
            Damage = objectDamage;
        }
        public void SetHealth(int health)
        {
            Health = health;
        }
        public void SetIsBuyed(bool value)
        {
            IsBuyed = value;
        }
        public void SetMaxHealth(int value)
        {
            MaxHealth = value;
        }
    }
}
