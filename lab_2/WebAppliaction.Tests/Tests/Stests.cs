using Xunit;
using System.Collections.Generic;
using WebApplication.Classes_For_IN;
using System.Linq;

namespace WebApplication.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Constructor_ShouldInitializeCorrectly()
        {
            var player = new Player(10, 20, 100, "Hero");

            Assert.Equal("Hero", player.Nickname);
            var stats = player.Get_Player_Stats();
            Assert.Equal(100, stats[0]);
            Assert.Equal(20, stats[1]);
            Assert.Equal(10, stats[2]);
        }

        [Fact]
        public void Take_Damage_ShouldReduceArmorFirst()
        {
            var player = new Player(50, 10, 100, "Tank");

            player.Take_Damage(30);

            var stats = player.Get_Player_Stats();
            Assert.Equal(100, stats[0]);
            Assert.Equal(20, stats[2]);
        }

        [Fact]
        public void Take_Damage_ShouldReduceHealthWhenArmorIsExceeded()
        {
            var player = new Player(10, 10, 100, "Mage");

            player.Take_Damage(30);

            var stats = player.Get_Player_Stats();
            Assert.Equal(80, stats[0]);
            Assert.Equal(0, stats[2]);
        }

        [Fact]
        public void Attack_ShouldReduceOpponentHealth()
        {
            var attacker = new Player(0, 25, 100, "Attacker");
            var victim = new Player(0, 0, 100, "Victim");

            attacker.Attack(victim);

            Assert.Equal(75, victim.Get_Player_Stats()[0]);
        }

        [Theory]
        [InlineData(150, 0)]
        [InlineData(110, 0)]
        public void Take_Damage_ShouldSetStatsToZeroIfLethal(int damage, int expectedHealth)
        {
            var player = new Player(10, 10, 100, "Mortal");

            bool alive = player.Take_Damage(damage);

            Assert.False(alive);
            Assert.Equal(expectedHealth, player.Get_Player_Stats()[0]);
            Assert.Equal(0, player.Get_Player_Stats()[2]);
        }

        [Fact]
        public void Unequip_Weapon_ShouldRestoreBaseDamage()
        {
            var player = new Player(0, 10, 100, "Warrior");
            var sword = new Weapon { Damage = 15 }; 
            player.Take_Item(sword, 1);
            
            player.Equip_Weapon(sword);
            Assert.Equal(25, player.Get_Player_Stats()[1]);

            bool result = player.Unequip_Weapon();

            Assert.True(result);
            Assert.Equal(10, player.Get_Player_Stats()[1]);
        }

        [Fact]
        public void Combine_Items_ShouldFailIfDifferentTypes()
        {
            var player = new Player(0, 0, 100, "Crafter");
            var potion = new Poiton { Typee = 1, CanCombine = true };
            var armor = new Armor { CanCombine = true };

            bool result = player.Combine_Item(potion, armor);

            Assert.False(result);
        }

        [Fact]
        public void Clear_Inventory_Testable_ShouldReturnFalseIfNothingToUnequip()
        {
            var player = new Player(0, 0, 100, "Empty");

            var result = player.Clear_Inventory_Testable();

            Assert.False(result);
        }
    }
}
