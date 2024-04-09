using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Schema;
using Rpg.Classes;
using Rpg.Files;

namespace Rpg
{
    public partial class Form1 : Form
    {
        private object currObject = null;

        MechanicObject lever = new MechanicObject("Автособиратель", 0, 0, false);
        MechanicObject battler = new MechanicObject("Манекен боя", 0, 0, false);

        Block block1 = new Block("Каменный блок", 0, 0);
        Block block2 = new Block("Каменный блок", 0, 0);
        Block block3 = new Block("Каменный блок", 0, 0);
        Block block4 = new Block("Каменный блок", 0, 0);
        Block block5 = new Block("Каменный блок", 0, 0);

        Mob zombie = new Mob("Зомби", rand.Next(10,20), rand.Next(2, 4), rand.Next(10, 20), true);
        Mob skeleton = new Mob("Скелет", rand.Next(10, 15), rand.Next(3, 5), rand.Next(10, 15), false);      
        Mob springMob = new Mob("Веслайм", rand.Next(17, 23), rand.Next(4, 7), rand.Next(17, 23), false);      
        Mob radiantZombie = new Mob("Радиоактивный зомби", rand.Next(15, 25), rand.Next(4, 5), rand.Next(15, 25), false);

        Culture carrot = new Culture("Морковь");

        static Random rand = new Random();

        Weapon swordW = new Weapon("Каменный меч", 4, false);
        Weapon swordArac = new Weapon("меч Арака", 6, false);
        Weapon swordRadiant = new Weapon("Радиационный меч", 9, false);

        Mob player = new Mob("Игрок", 20, 2, 20, true);

        DateTime today = DateTime.Today;

        Item objStone = new Item("Камень", 4, 60);
        Item objWood = new Item("Дерево", 4, 60);
        Item objArac = new Item("руда Арак", 0, 15);
        Item objMonsterMoney = new Item("Монета монстров", 0, 125);      
        Item objSpringItem = new Item("Весенний камень", 0, 100); 

        Factory factory = new Factory("Станция дерева", false);
        Factory factory2Object = new Factory("Станция камня", false);
        Factory factoryDrill = new Factory("Бур", false);
        Factory factoryMagicBuilding = new Factory("Магазин мобов", false);
        Factory factorySpring = new Factory("Весенняя фабрика", false);

        Armor woodenArmor = new Armor("Деревянная броня", 25, false);

        Crafting crafter = new Crafting();


        public Form1()
        {
            InitializeComponent();

            //загрузка переменных 
            LoadValues();
            //загрузка позиций блоков
            SetBlocksCoordinates();

            if (woodenArmor.IsCrafted == true)
            {
                player.SetHealth(woodenArmor.Health);
                player.SetMaxHealth(woodenArmor.Health);
            }

            if(objWood.Counter > 7)
            {
                wood.Visible = false;
                wood2.Visible = false;
                wood3.Visible = false;
            }
            if(objStone.Counter > 10)
            {
                stone.Visible = false;
                stone2.Visible = false;
            }

            //сезонные события
            if (today.Month == 3 || today.Month == 4)
            {
                MarchBush.Visible = true;
                MarchBush2.Visible = true;
                MarchBush3.Visible = true;
                MarchBush4.Visible = true;

                SpringEventInfo.Visible = true;
                SpringItemIcon.Visible = true;
                CounterSpringItem.Visible = true;

                if(factorySpring.CanBuilded == true)
                {
                    SpringFactoryObject.Visible = true;
                }
                else
                {
                    BrokenSpringFactory.Visible = true;
                }
            }
            if(today.Month == 11 || today.Month == 12)
            {
                Snowman.Visible = true;
            }

            this.KeyDown += new KeyEventHandler(PlayerController);
            this.MouseMove += new MouseEventHandler(mouseEvent);
        }

        private void mouseEvent(object sender, MouseEventArgs e)
        {
            if (currObject != null)
            {
                currObject.GetType().GetProperty("Location").SetValue(currObject, new Point(Cursor.Position.X - 10, Cursor.Position.Y - 60));
            }
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

        public void SetBlocksCoordinates()
        {
            Block1Object.Location = new Point(block1.X, block1.Y);
            Block2Object.Location = new Point(block2.X, block2.Y);
            Block3Object.Location = new Point(block3.X, block3.Y);
            AutoPuter.Location = new Point(lever.X, lever.Y);
            AutoBattler.Location = new Point(battler.X, battler.Y);
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
            if(swordRadiant.IsCrafted)
            {
                player.SetDamage(swordRadiant.Damage);
            }
            DamageState.Text = $"Урон: {player.Damage}";
        }
        private void HideSword2Materials()
        {
            AracSwordCraft.Visible = false;
            Sword1Material1.Visible = false;
            label1.Visible = false;
            CraftAracSwordButton.Visible = false;

            CraftRadiantSwordButton.Visible = true;
            AracCraftedPanel.Visible = true;
            CraftRadiantSwordIcon.Visible = true;
            Sword2Material1.Visible = true;
            Sword2Material2.Visible = true;
            Sword2text1.Visible = true;
            Sword2text2.Visible = true;

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
            
            Properties.Settings.Default.factorySpringValue = factorySpring.CanBuilded;
            Properties.Settings.Default.Save(); 
            
            Properties.Settings.Default.stone = objStone.Counter;
            Properties.Settings.Default.Save();    
            
            Properties.Settings.Default.springItemSave = objSpringItem.Counter;
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
            
            Properties.Settings.Default.mob2IsBuyed = radiantZombie.IsBuyed;
            Properties.Settings.Default.Save(); 
            
            Properties.Settings.Default.springMobSave = springMob.IsBuyed;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.armor1 = woodenArmor.IsCrafted;
            Properties.Settings.Default.Save();     
            
            Properties.Settings.Default.sword3 = swordRadiant.IsCrafted;
            Properties.Settings.Default.Save();

            //блоки
            Properties.Settings.Default.block1X = block1.X;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block1Y = block1.Y;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block1IsCrafted = block1.IsCrafted;
            Properties.Settings.Default.Save();    
            
            Properties.Settings.Default.block2X = block2.X;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block2Y = block2.Y;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block2IsCrafted = block2.IsCrafted;
            Properties.Settings.Default.Save();      
            
            Properties.Settings.Default.block3X = block3.X;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block3Y = block3.Y;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.block3IsCrafted = block3.IsCrafted;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.AutoPuterX = lever.X;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.AutoPuterY = lever.Y;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.AutoPuterIsCrafted = lever.IsCrafted;
            Properties.Settings.Default.Save();    
            
            Properties.Settings.Default.BattlerX = battler.X;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.BattlerY = battler.Y;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.BattlerIsCrafted = battler.IsCrafted;
            Properties.Settings.Default.Save();

        }
        private void LoadValues()
        {
            factory.SetValue(Properties.Settings.Default.factory1Value);
            factory2Object.SetValue(Properties.Settings.Default.factory2Value);
            factoryDrill.SetValue(Properties.Settings.Default.factory3Value);  
            factoryMagicBuilding.SetValue(Properties.Settings.Default.factory4Value);     
            factorySpring.SetValue(Properties.Settings.Default.factorySpringValue);

            objStone.SetCount(Properties.Settings.Default.stone);
            objWood.SetCount(Properties.Settings.Default.wood);
            objArac.SetCount(Properties.Settings.Default.arac);
            objMonsterMoney.SetCount(Properties.Settings.Default.monsterMoney); 
            objSpringItem.SetCount(Properties.Settings.Default.springItemSave);

            swordW.SetIsCrafted(Properties.Settings.Default.sword1);
            swordArac.SetIsCrafted(Properties.Settings.Default.sword2);        
            swordRadiant.SetIsCrafted(Properties.Settings.Default.sword3);   
            
            woodenArmor.SetIsCrafted(Properties.Settings.Default.armor1);  
            
            skeleton.SetIsBuyed(Properties.Settings.Default.mob1IsBuyed);     
            radiantZombie.SetIsBuyed(Properties.Settings.Default.mob2IsBuyed);  
            springMob.SetIsBuyed(Properties.Settings.Default.springMobSave);

            //блоки
            block1.SetCoordinates(Properties.Settings.Default.block1X, Properties.Settings.Default.block1Y);
            block1.SetIsCrafted(Properties.Settings.Default.block1IsCrafted);

            block2.SetCoordinates(Properties.Settings.Default.block2X, Properties.Settings.Default.block2Y);
            block2.SetIsCrafted(Properties.Settings.Default.block2IsCrafted);      
            
            block3.SetCoordinates(Properties.Settings.Default.block3X, Properties.Settings.Default.block3Y);
            block3.SetIsCrafted(Properties.Settings.Default.block3IsCrafted);

            lever.SetCoordinates(Properties.Settings.Default.AutoPuterX, Properties.Settings.Default.AutoPuterY);
            lever.SetIsCrafted(Properties.Settings.Default.AutoPuterIsCrafted);   
            
            battler.SetCoordinates(Properties.Settings.Default.BattlerX, Properties.Settings.Default.BattlerY);
            battler.SetIsCrafted(Properties.Settings.Default.BattlerIsCrafted);
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
            //работа механизмов
            if(lever.IsActive && AutoPuter.Bounds.IntersectsWith(dirtZone.Bounds))
            {
                if(carrot2.Visible)
                {
                    carrot.Put(carrot2, carrot, carrotTimer, objWood);
                }
            }
           if(lever.IsActive) { }
           else
           {
               ActivatedAutoPuter.Location = new Point(AutoPuter.Location.X, AutoPuter.Location.Y);
               ActivateAutoPuter.Location = new Point(AutoPuter.Location.X - 10, AutoPuter.Location.Y - 30);
           }


            if (battler.IsActive) { }
            else
            {
                ActivatedAutoBattler.Location = new Point(AutoBattler.Location.X, AutoBattler.Location.Y);
                ActivateAutoBattler.Location = new Point(AutoBattler.Location.X - 10, AutoBattler.Location.Y - 30);
            }
            

            //отображение блоков
            if (block1.IsCrafted)
            {
                Block1Object.Visible = true;
                BuyBlock.Visible = false;
            }
            if(block2.IsCrafted)
            {
                Block2Object.Visible = true;
                BuyBlock2.Visible = false;
                BuyBlock3.Visible = true;

            }
            if(block3.IsCrafted)
            {
                Block3Object.Visible = true;
                BuyBlock3.Visible = false;
                BlocksBuyedText.Visible = true;
            }

            if (lever.IsCrafted && !lever.IsActive)
            {
                AutoPuter.Visible = true;
                ActivateAutoPuter.Visible = true;
            }      
            if (battler.IsCrafted && !battler.IsActive)
            {
                AutoBattler.Visible = true;
                ActivateAutoBattler.Visible = true;
            }


            HeroHealthText.Text = player.Health.ToString();
            HeroHealthText.Location = new Point(hero.Location.X + 5, hero.Location.Y - 40);

            ZombieHealthText.Text = zombie.Health.ToString();
            SkeletonHealthText.Text = skeleton.Health.ToString();
            RadiantZombieHealthText.Text = radiantZombie.Health.ToString();
            SpringMobHealthText.Text = springMob.Health.ToString();

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
            
            if(hero.Bounds.IntersectsWith(SpringMobFightZone.Bounds))
            {
                SpringMobAttack.Visible = true;
            }
            else
            {
                SpringMobAttack.Visible = false;
            }
            
            if(hero.Bounds.IntersectsWith(SkeletonFightZone.Bounds))
            {
                SkeletonAttack.Visible = true;
            }
            else
            {
                SkeletonAttack.Visible = false;
            }

            if (hero.Bounds.IntersectsWith(RadiantZombieFightZone.Bounds))
            {
                RadiantZombieAttack.Visible = true;
            }
            else
            {
                RadiantZombieAttack.Visible = false;
            }
            //отображение семян
            if(carrotTimer.Enabled == false && hero.Bounds.IntersectsWith(dirtZone.Bounds))
            {
                CarrotSeeds.Visible = true;
            }
            else
            {
                CarrotSeeds.Visible = false;
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
                SkeletonAttack.Visible = false;
            }     
            
            if(springMob.IsBuyed == true)
            {
                SpringMobObject.Visible = true;
                SpringMobHealthText.Visible = true;
            }
            else
            {
                SpringMobObject.Visible = false;
                SpringMobHealthText.Visible = false;
                SpringMobAttack.Visible = false;
            }       
            
            if(radiantZombie.IsBuyed)
            {
                RadantZombieObject.Visible = true;
                RadiantZombieHealthText.Visible = true;
            }
            else
            {
                RadantZombieObject.Visible = false;
                RadiantZombieHealthText.Visible = false;
                RadiantZombieAttack.Visible = false;
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
            if(objSpringItem.Counter >= objSpringItem.Stack)
            {
                objSpringItem.SetCount(objSpringItem.Stack);
            }

            //отображение иконок в инвентаре
            if (swordW.IsCrafted)
            {
                Sword1InvenIcon.Visible = true;
                HideSwordWMaterials();
            }    
            if(swordArac.IsCrafted)
            {
                HideSword2Materials();
                AracSwordInvenIcon.Visible = true;
            }
            if(swordRadiant.IsCrafted)
            {
                RadiantSwordInventIcon.Visible = true;
            }

            //Set урона
            DamageSetter();

            CounterWoodText.Text = objWood.Counter.ToString();
            counterStoneText.Text = objStone.Counter.ToString();
            CounterAracText.Text = objArac.Counter.ToString();
            CounterMonMoneyText.Text = objMonsterMoney.Counter.ToString();     
            CounterSpringItem.Text = objSpringItem.Counter.ToString();

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
            if (factorySpring.CanBuilded == true)
            {
                SpringFactoryObject.Visible = true;
            }
            else
            {
                SpringFactoryObject.Visible = false;
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
            
            if (factorySpring.CanBuilded == true)
            {
                if(today.Month == 3 || today.Month == 4)
                {
                    FactorySpringTimer.Enabled = true;
                }
                else
                {
                    FactorySpringTimer.Enabled = false;
                }
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
        private void battlerFight(Mob mob, Item item)
        {
            mob.TakeDamage(player.Damage);

            if (mob.Health <= 0)
            {
                Recovery1Health(mob);
                item.AddCount(1);
            }
        }
        private void RecoveryHealth(Mob player, Mob mob)
        {
            player.SetHealth(player.MaxHealth);
            mob.SetHealth(mob.MaxHealth);
        }
        private void Recovery1Health(Mob mob)
        {
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
            factoryMagicBuilding.Build(objStone, objArac, 20, 5, MagicBuildingRecovery);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;

            Rectangle bounds = screens[0].Bounds;

            if (this.Width > bounds.Width)
            {
                this.Width = bounds.Width - 50;
            }

            if (this.Height > bounds.Height)
            {
                this.Height = bounds.Height - 50; 
            }

            if (this.Left + this.Width > bounds.Right)
            {
                this.Left = bounds.Right - this.Width; 
            }

            if (this.Top + this.Height > bounds.Bottom)
            {
                this.Top = bounds.Bottom - this.Height; 
            }
        }

        private void CloseMagicBuildShop_Click(object sender, EventArgs e)
        {
            MagicBuildShop.Visible = false;
        }

        private void BuySkeleton_Click(object sender, EventArgs e)
        {
            Recipe skeletonCraft = new Recipe(10, 10, skeleton.Name);
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

        private void BuyRadiantZombie_Click(object sender, EventArgs e)
        {
            Recipe radiantZombieCraft = new Recipe(20, 20, radiantZombie.Name);
            if (!radiantZombie.IsBuyed)
            {
                crafter.CraftMob(radiantZombieCraft, objStone, objMonsterMoney, radiantZombie);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void RadiantZombieAttack_Click(object sender, EventArgs e)
        {
            Battle(player, radiantZombie, objMonsterMoney);
        }

        private void CraftRadiantSwordButton_Click(object sender, EventArgs e)
        {
            Recipe swordRecipe = new Recipe(20, 7, swordRadiant.Name);
            if(!swordRadiant.IsCrafted)
            {
                crafter.Craft(swordRecipe, objMonsterMoney, objArac, swordRadiant);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку!");
            }
            
        }

        private void RadiantSwordInventIcon_Click(object sender, EventArgs e)
        {
            RadiantSwordInfo.Visible = true;
        }

        private void CloseRadiantSwordInfo_Click(object sender, EventArgs e)
        {
            RadiantSwordInfo.Visible = false;
        }

        private void BrokenSpringFactory_Click(object sender, EventArgs e)
        {
            CraftSpringFactory.Visible = true;
        }

        private void CloseSpringFactoryinfo_Click(object sender, EventArgs e)
        {
            SpringFactoryInfo.Visible = false;
        }

        private void CloseCraftSpringFactory_Click(object sender, EventArgs e)
        {
            CraftSpringFactory.Visible = false;
        }

        private void SpringFactoryObject_Click(object sender, EventArgs e)
        {
            SpringFactoryInfo.Visible = true;
        }

        private void CraftSpringFactoryButton_Click(object sender, EventArgs e)
        {
            factorySpring.Build(objStone, objWood, 15, 5, CraftSpringFactory);
        }

        private void FactorySpringTimer_Tick(object sender, EventArgs e)
        {
            objSpringItem.AddCount(1);
        }

        private void SpringEventInfo_Click(object sender, EventArgs e)
        {
            SpringInfoEventPanel.Visible = true;
        }

        private void CloseEventSpringInfo_Click(object sender, EventArgs e)
        {
            SpringInfoEventPanel.Visible = false;
        }

        private void EventShopButton_Click(object sender, EventArgs e)
        {
            SpringInfoEventPanel.Visible = false;
            EventShopPanel.Visible = true;
        }

        private void CloseEventShop_Click(object sender, EventArgs e)
        {
            EventShopPanel.Visible = false;
        }

        private void BuySpringMob_Click(object sender, EventArgs e)
        {
            Recipe springMobCraft = new Recipe(15, 10, springMob.Name);
            if (!springMob.IsBuyed)
            {
                crafter.CraftMob(springMobCraft, objSpringItem, objMonsterMoney, springMob);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void SpringMobAttack_Click(object sender, EventArgs e)
        {
            Battle(player, springMob, objMonsterMoney);
        }

        private void CarrotSeeds_Click(object sender, EventArgs e)
        {
            carrotTimer.Enabled = true;
        }

        private void carrotTimer_Tick(object sender, EventArgs e)
        {
            carrot.UpdateTime();
            CarrotSeeds.Visible = false;
            carrot1.Visible = true;
            if (carrot.TimerValue >= 100) 
            {
                carrot1.Visible = false;
                carrot2.Visible = true;
            }
        }

        private void carrot2_Click(object sender, EventArgs e)
        {
            carrot.Put(carrot2, carrot, carrotTimer, objWood);
        }

        private void BuyBlock_Click(object sender, EventArgs e)
        {
            Recipe blockRecipe = new Recipe(3, 0, block1.Name);
            crafter.CraftBlock(blockRecipe, objStone, objWood, block1, Block1Object);
        }

        private void Block1Object_Click(object sender, EventArgs e)
        {
            currObject = sender;
        }

        private void SetLocationBlock1(object sender, EventArgs e)
        {
            block1.SetCoordinates(Block1Object.Location.X, Block1Object.Location.Y);
            currObject = null;
        }

        private void SetLocationBlock2(object sender, EventArgs e)
        {
            block2.SetCoordinates(Block2Object.Location.X, Block2Object.Location.Y);
            currObject = null;
        }

        private void Block2Object_Click(object sender, EventArgs e)
        {
            currObject = sender;
        }

        private void BuyBlock2_Click(object sender, EventArgs e)
        {
            Recipe blockRecipe = new Recipe(3, 0, block1.Name);
            crafter.CraftBlock(blockRecipe, objStone, objWood, block2, Block2Object);
        }

        private void BuyBlock3_Click(object sender, EventArgs e)
        {
            Recipe blockRecipe = new Recipe(3, 0, block3.Name);
            crafter.CraftBlock(blockRecipe, objStone, objWood, block3, Block3Object);
        }

        private void Block3Object_Click(object sender, EventArgs e)
        {
            currObject = sender;
        }

        private void SetLocationBlock3(object sender, EventArgs e)
        {
            block3.SetCoordinates(Block3Object.Location.X, Block3Object.Location.Y);
            currObject = null;
        }

        private void TestMechanism_Click(object sender, EventArgs e)
        {
            currObject = sender;
        }

        private void SetMechanismCoordinates(object sender, EventArgs e)
        {
            lever.SetCoordinates(AutoPuter.Location.X, AutoPuter.Location.Y);
            currObject = null;
        }

        private void ActivateMechanism(object sender, EventArgs e)
        {
            if(lever.IsActive)
            {
                lever.SetIsActive(false);
                AutoPuter.Visible = true;
                ActivatedAutoPuter.Visible = false;
                ActivateAutoPuter.Text = "Включить";
            }
            else
            {
                ActivatedAutoPuter.Visible = true;
                AutoPuter.Visible = false;
                lever.SetIsActive(true);
                ActivateAutoPuter.Text = "Отключить";
            }
        }

        private void BuyAutoPuter_Click(object sender, EventArgs e)
        {
            Recipe autoPuterRecipe = new Recipe(15, 8, lever.Name);
            if (!lever.IsCrafted)
            {
                crafter.CraftMechanism(autoPuterRecipe, objStone, objArac, lever, AutoPuter);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }

        private void BlockShop_Click(object sender, EventArgs e)
        {
            BlockShopPanel.Visible = true;
        }

        private void CloseBlockShop_Click(object sender, EventArgs e)
        {
            BlockShopPanel.Visible = false;
        }

        private void ActivateAutoBattler_Click(object sender, EventArgs e)
        {
            if (battler.IsActive)
            {
                battler.SetIsActive(false);
                ActivateAutoBattler.Text = "Включить";
                AutoBattler.Visible = true;
                ActivatedAutoBattler.Visible = false;
                AutoBattlerTimer.Enabled = false;
            }
            else
            {
                battler.SetIsActive(true);
                ActivateAutoBattler.Text = "Отключить";
                AutoBattler.Visible = false;
                ActivatedAutoBattler.Visible = true;
                AutoBattlerTimer.Enabled = true;
            }
        }

        private void AutoBattler_Click(object sender, EventArgs e)
        {
            currObject = sender;
        }

        private void AutoBattlerSetCoordinates(object sender, EventArgs e)
        {
            currObject = null;
            battler.SetCoordinates(AutoBattler.Location.X, AutoBattler.Location.Y);
        }

        private void AutoBattlerTimer_Tick(object sender, EventArgs e)
        {
            if(AutoBattler.Bounds.IntersectsWith(SpringMobFightZone.Bounds))
            {
                battlerFight(springMob, objMonsterMoney);
            }
            if (AutoBattler.Bounds.IntersectsWith(RadiantZombieFightZone.Bounds))
            {
                battlerFight(radiantZombie, objMonsterMoney);
            }
            if (AutoBattler.Bounds.IntersectsWith(SkeletonFightZone.Bounds))
            {
                battlerFight(skeleton, objMonsterMoney);
            }
            if (AutoBattler.Bounds.IntersectsWith(ZombieFightZone.Bounds))
            {
                battlerFight(zombie, objMonsterMoney);
            }
        }

        private void BuyAutoBattler_Click(object sender, EventArgs e)
        {
            Recipe autoBattlerRecipe = new Recipe(25, 10, battler.Name);
            if (!battler.IsCrafted)
            {
                crafter.CraftMechanism(autoBattlerRecipe, objStone, objArac, battler, AutoBattler);
            }
            else
            {
                MessageBox.Show("Вы уже совершали покупку");
            }
        }
    }
}
