using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Rpg.Files;

namespace Rpg
{
    public partial class Form1 : Form
    {
        Mob zombie = new Mob("Зомби", rand.Next(10,20), rand.Next(2, 4), rand.Next(10, 20), true);
        Mob skeleton = new Mob("Скелет", rand.Next(10, 15), rand.Next(3, 5), rand.Next(10, 15), false);

        static Random rand = new Random();

        Weapon swordW = new Weapon("Каменный меч", 4, false);
        Weapon swordArac = new Weapon("меч Арака", 6, false);

        Mob player = new Mob("Игрок", 20, 2, 20, true);

        DateTime today = DateTime.Today;

        Item objStone = new Item("Камень", 4, 60);
        Item objWood = new Item("Дерево", 4, 60);
        Item objArac = new Item("руда Арак", 0, 15);
        Item objMonsterMoney = new Item("Монета монстров", 0, 125); 

        Factory factory = new Factory("Станция дерева", false);
        Factory factory2Object = new Factory("Станция камня", false);
        Factory factoryDrill = new Factory("Бур", false);
        Factory factoryMagicBuilding = new Factory("Магазин мобов", false);

        Armor woodenArmor = new Armor("Деревянная броня", 25, false);

        Crafting crafter = new Crafting();


        public Form1()
        {
            InitializeComponent();

            //загрузка переменных 
            LoadValues();

            if(woodenArmor.IsCrafted == true)
            {
                player.SetHealth(woodenArmor.Health);
                player.SetMaxHealth(woodenArmor.Health);
            }

            //сезонные события
            if (today.Month == 3 /*|| today.Month == 4*/)
            {
                MarchBush.Visible = true;
                MarchBush2.Visible = true;
                MarchBush3.Visible = true;
                MarchBush4.Visible = true;
            }
            if(today.Month == 11 || today.Month == 12)
            {
                Snowman.Visible = true;
            };


            this.KeyDown += new KeyEventHandler(PlayerController);
            CounterWoodText.Text = objWood.Counter.ToString();
            counterStoneText.Text = objStone.Counter.ToString();
            CounterWoodText.Text = objArac.Counter.ToString();
        }

        private void PlayerController(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode.ToString()) 
            {
                case "W":
                    hero.Location = new Point(hero.Location.X, hero.Location.Y - 13);
                    break;
                case "S":
                    hero.Location = new Point(hero.Location.X, hero.Location.Y + 13);
                    break;
                case "A":
                    hero.Location = new Point(hero.Location.X - 13, hero.Location.Y);
                    break;
                case "D":
                    hero.Location = new Point(hero.Location.X + 13, hero.Location.Y);
                    break;
                case "E":
                    CraftPanel.Visible = true;
                    break;    
                case "Q":
                    InventoryPanel.Visible = true;
                    break;
            }
        }

        private void stone_Click(object sender, EventArgs e)
        {
            objStone.Put(1, stone, counterStoneText, objStone);
        }
        
        private void DamageSetter()
        {
            if (swordW.IsCrafted == true && swordArac.IsCrafted != true)
            {
                player.SetDamage(swordW.Damage);
            }
            if(swordW.IsCrafted == true && swordArac.IsCrafted == true)
            {
                player.SetDamage(swordArac.Damage);
            }
            DamageState.Text = $"Урон: {player.Damage}";
        }
        private void HideSwordWMaterials()
        {
            Sword1CraftIcon.Visible = false;
            Sword1material2.Visible = false;
            CountSword1m2.Text = "2";

            CraftAracSwordButton.Visible = true;
            AracCraftedPanel.Visible = true;
            AracSwordCraft.Visible = true;
        }
        private void SaveValues()
        {
            Properties.Settings.Default.factory1Value = factory.CanBuilded;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.factory2Value = factory2Object.CanBuilded;
            Properties.Settings.Default.Save(); 
            
            Properties.Settings.Default.factory3Value = factoryDrill.CanBuilded;
            Properties.Settings.Default.Save(); 
            
            Properties.Settings.Default.factory4Value = factoryMagicBuilding.CanBuilded;
            Properties.Settings.Default.Save(); 
            
            Properties.Settings.Default.stone = objStone.Counter;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.wood = objWood.Counter;
            Properties.Settings.Default.Save();   
                
            Properties.Settings.Default.monsterMoney = objMonsterMoney.Counter;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.arac = objArac.Counter;
            Properties.Settings.Default.Save();       
            
            Properties.Settings.Default.sword1 = swordW.IsCrafted;
            Properties.Settings.Default.Save();  
            
            Properties.Settings.Default.sword2 = swordArac.IsCrafted;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.mob1IsBuyed = skeleton.IsBuyed;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.armor1 = woodenArmor.IsCrafted;
            Properties.Settings.Default.Save();

        }
        private void LoadValues()
        {
            factory.SetValue(Properties.Settings.Default.factory1Value);
            factory2Object.SetValue(Properties.Settings.Default.factory2Value);
            factoryDrill.SetValue(Properties.Settings.Default.factory3Value);  
            factoryMagicBuilding.SetValue(Properties.Settings.Default.factory4Value);

            objStone.SetCount(Properties.Settings.Default.stone);
            objWood.SetCount(Properties.Settings.Default.wood);
            objArac.SetCount(Properties.Settings.Default.arac);
            objMonsterMoney.SetCount(Properties.Settings.Default.monsterMoney);

            swordW.SetIsCrafted(Properties.Settings.Default.sword1);
            swordArac.SetIsCrafted(Properties.Settings.Default.sword2);   
            
            woodenArmor.SetIsCrafted(Properties.Settings.Default.armor1);  
            
            skeleton.SetIsBuyed(Properties.Settings.Default.mob1IsBuyed);
        }

        private void stone2_Click(object sender, EventArgs e)
        {
            objStone.Put(1, stone2, counterStoneText, objStone);
        }

        private void CloseCraftPanel_Click(object sender, EventArgs e)
        {
            CraftPanel.Visible = false;
        }

        private void wood_Click(object sender, EventArgs e)
        {
            objWood.Put(1, wood, CounterWoodText, objWood);
        }

        private void wood2_Click(object sender, EventArgs e)
        {
            objWood.Put(1, wood2, CounterWoodText, objWood);
        }

        private void CraftSwordButton_Click_1(object sender, EventArgs e)
        {
            Recipe swordRecipe = new Recipe(5, 2, swordW.Name);
            crafter.Craft(swordRecipe, objStone, objWood, swordW);
        }

        private void factory1_Click(object sender, EventArgs e)
        {
            Factory1Info.Visible = true;
        }

        private void wood3_Click(object sender, EventArgs e)
        {
            objWood.Put(1, wood3, CounterWoodText, objWood);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            CraftFactory1.Visible = false;
        }

        private void CraftFactory1Button_Click(object sender, EventArgs e)
        {
            factory.Build(objWood, objStone, 3, 3, CraftFactory1);
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            HeroHealthText.Text = player.Health.ToString();
            HeroHealthText.Location = new Point(hero.Location.X + 5, hero.Location.Y - 40);

            ZombieHealthText.Text = zombie.Health.ToString();
            SkeletonHealthText.Text = skeleton.Health.ToString();

            HealthState.Text = $"Здоровье: {player.Health}";

            //отображение кнопок атаки мобов
            if(hero.Bounds.IntersectsWith(ZombieFightZone.Bounds))
            {
                ZombieAttack.Visible = true;
            }
            else
            {
                ZombieAttack.Visible = false;
            }
            if (hero.Bounds.IntersectsWith(SkeletonFightZone.Bounds))
            {
                SkeletonAttack.Visible = true;
            }
            else
            {
                SkeletonAttack.Visible = false;
            }

            //броня
            if (woodenArmor.IsCrafted == true)
            {
                hero.Image = Properties.Resources.WoodArmor;
                WoodArmorInventory.Visible = true;
            }

            //отображение покупаемых мобов
            if(skeleton.IsBuyed == true)
            {
                SkeletonObject.Visible = true;
                SkeletonHealthText.Visible = true;
            }
            else
            {
                SkeletonObject.Visible = false;
                SkeletonHealthText.Visible = false;
            }

            //ограничение предметов
            if (objStone.Counter >= objStone.Stack)
            {
                objStone.SetCount(objStone.Stack);
            }       
            if (objWood.Counter >= objWood.Stack)
            {
                objWood.SetCount(objWood.Stack);
            }
            if (objArac.Counter >= objArac.Stack)
            {
                objArac.SetCount(objArac.Stack);
            }
            if(objMonsterMoney.Counter >= objMonsterMoney.Stack)
            {
                objMonsterMoney.SetCount(objMonsterMoney.Stack);
            }

            //отображение иконок в инвентаре
            if (swordW.IsCrafted == true)
            {
                Sword1InvenIcon.Visible = true;
                HideSwordWMaterials();
            }    
            if(swordArac.IsCrafted == true)
            {
                AracSwordInvenIcon.Visible = true;
            }

            //Set урона
            DamageSetter();

            CounterWoodText.Text = objWood.Counter.ToString();
            counterStoneText.Text = objStone.Counter.ToString();
            CounterAracText.Text = objArac.Counter.ToString();
            CounterMonMoneyText.Text = objMonsterMoney.Counter.ToString();

            //фабрики boolean
            if (factory.CanBuilded == true)
            {
                factory1.Visible = true;
                DestroyedFactory1.Visible = false;
            }
            else
            {
                factory1.Visible = false;
                DestroyedFactory1.Visible = true;
            }        
            
            if (factory2Object.CanBuilded == true)
            {
                factory2.Visible = true;
                DestroyedFactory2.Visible = false;
            }
            else
            {
                factory2.Visible = false;
                DestroyedFactory2.Visible = true;
            }

            if (factoryDrill.CanBuilded == true)
            {
                FactoryDrillS.Visible = true;
            }
            else
            {
                FactoryDrillS.Visible = false;
            }

            //фабрики таймеры
            if (factory.CanBuilded == true)
            {
                Factory1Timer.Enabled = true;
            }
            else
            {
                Factory1Timer.Enabled = false;
            }   
            
            if (factory2Object.CanBuilded == true)
            {
                Factory2Timer.Enabled = true;
            }
            else
            {
                Factory2Timer.Enabled = false;
            }       

            if (factoryDrill.CanBuilded == true)
            {
                FactoryDrillTimer.Enabled = true;
            }
            else
            {
                FactoryDrillTimer.Enabled = false;
            }
        }

        private void Battle(Mob player, Mob mob, Item item)
        {
            player.TakeDamage(mob.Damage);
            mob.TakeDamage(player.Damage);

            if (player.Health <= 0 && mob.Health <= 0)
            {
                MessageBox.Show("Ничья");
                RecoveryHealth(player, mob);
            }
            else if (player.Health <= 0)
            {
                MessageBox.Show("Вас убили");
                RecoveryHealth(player, mob);
            }
            else if (mob.Health <= 0)
            {
                MessageBox.Show($"Вы убили {mob.Name}");
                RecoveryHealth(player, mob);
                item.AddCount(1);
            }
        }
        private void RecoveryHealth(Mob player, Mob mob)
        {
            player.SetHealth(player.MaxHealth);
            mob.SetHealth(mob.MaxHealth);
        }


        private void DestroyedFactory1_Click(object sender, EventArgs e)
        {
            CraftFactory1.Visible = true;
        }

        private void Factory1Timer_Tick(object sender, EventArgs e)
        {
            objWood.AddCount(1);
        }

        private void CloseInfoFactory1_Click(object sender, EventArgs e)
        {
            Factory1Info.Visible = false;
        }

        private void DestroyedFactory2_Click(object sender, EventArgs e)
        {
            CraftFactory2.Visible = true;
        }

        private void CloseFactory2Craft_Click(object sender, EventArgs e)
        {
            CraftFactory2.Visible = false;
        }

        private void factory2_Click(object sender, EventArgs e)
        {
            Factory2Info.Visible = true;
        }

        private void CloseFactory2Info_Click(object sender, EventArgs e)
        {
            Factory2Info.Visible = false;
        }

        private void CraftFactory2O_Click(object sender, EventArgs e)
        {
            factory2Object.Build(objWood, objStone, 3, 5, CraftFactory2);
        }

        private void Factory2Timer_Tick(object sender, EventArgs e)
        {
            objStone.AddCount(1);
        }

        private void AracOre1_Click(object sender, EventArgs e)
        {
            if(factoryDrill.CanBuilded == true)
            {
                DrillInfo.Visible = true;
            }
            else
            {
                CraftDrill.Visible = true;
            }
        }

        private void CloseCraftDrillPanel_Click(object sender, EventArgs e)
        {
            CraftDrill.Visible = false;
        }

        private void CraftDrill1_Click(object sender, EventArgs e)
        {
            factoryDrill.Build(objWood, objStone, 0, 3, CraftDrill);
        }

        private void FactoryDrillTimer_Tick(object sender, EventArgs e)
        {
            objArac.AddCount(1);
        }

        private void CloseInventory_Click(object sender, EventArgs e)
        {
            InventoryPanel.Visible = false;
        }

        private void Sword1InvenIcon_Click(object sender, EventArgs e)
        {
            Sword1Info.Visible = true;
        }

        private void CloseSword1Info_Click(object sender, EventArgs e)
        {
            Sword1Info.Visible = false;
        }

        private void AracSwordInvenIcon_Click(object sender, EventArgs e)
        {
            AracSwordInfo.Visible = true;
        }

        private void CloseAracSwordInfo_Click(object sender, EventArgs e)
        {
            AracSwordInfo.Visible = false;
        }

        private void CraftAracSwordButton_Click(object sender, EventArgs e)
        {
            Recipe aracSwordRecipe = new Recipe(5, 2, swordArac.Name);
            if (!swordArac.IsCrafted)
            {
                crafter.Craft(aracSwordRecipe, objStone, objArac, swordArac);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void SaveData_Click(object sender, EventArgs e)
        {
            SaveValues();
            MessageBox.Show("Данные сохранены");
        }

        private void FactoryDrillS_Click(object sender, EventArgs e)
        {
            if (factoryDrill.CanBuilded == true)
            {
                DrillInfo.Visible = true;
            }
            else
            {
                CraftDrill.Visible = true;
            }
        }

        private void CloseDrillInfo_Click(object sender, EventArgs e)
        {
            DrillInfo.Visible = false;
        }

        private void CloseMagicBuildingRec_Click(object sender, EventArgs e)
        {
            MagicBuildingRecovery.Visible = false;
        }

        private void MagicBuilding_Click(object sender, EventArgs e)
        {
            if (factoryMagicBuilding.CanBuilded == true)
            {
                MagicBuildShop.Visible = true;
            }
            else
            {
                MagicBuildingRecovery.Visible = true;
            }
        }

        private void MagicBuildingCraft_Click(object sender, EventArgs e)
        {
            factoryMagicBuilding.Build(objStone, objArac, 40, 10, MagicBuildingRecovery);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CloseMagicBuildShop_Click(object sender, EventArgs e)
        {
            MagicBuildShop.Visible = false;
        }

        private void BuySkeleton_Click(object sender, EventArgs e)
        {
            Recipe skeletonCraft = new Recipe(35, 15, skeleton.Name);
            if(!skeleton.IsBuyed)
            {
                crafter.CraftMob(skeletonCraft, objWood, objMonsterMoney, skeleton);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void CraftWoodenArmor_Click(object sender, EventArgs e)
        {
            Recipe woodenArmorRecipe = new Recipe(12, 4, woodenArmor.Name);
            if(!woodenArmor.IsCrafted)
            {
                crafter.CraftArmor(woodenArmorRecipe, objWood, objStone, woodenArmor);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void WoodArmorInventory_Click(object sender, EventArgs e)
        {
            WoodArmorInfo.Visible = true;
        }

        private void CloseWoodArmorInfo_Click(object sender, EventArgs e)
        {
            WoodArmorInfo.Visible = false;
        }

        private void ZombieAttack_Click(object sender, EventArgs e)
        {
            Battle(player, zombie, objMonsterMoney);
        }

        private void SkeletonAttack_Click(object sender, EventArgs e)
        {
            Battle(player, skeleton, objMonsterMoney);
        }

        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            SaveValues();
        }
    }
}
