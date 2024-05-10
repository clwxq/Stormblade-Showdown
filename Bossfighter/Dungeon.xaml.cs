using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bossfighter
{
    /// <summary>
    /// Interakční logika pro Dungeon.xaml
    /// </summary>
    public partial class Dungeon : Window
    {
        public static Random random = new();
        public static BitmapImage bitmap = new();

        public double player_hp_num = 200;
        public double player_dmg = 11;
        public double player_heal()
        { 
            return random.Next(8, 20); 
        }
        public double player_parry ()
        {
            return random.Next(0, 3);
        }

        public double enemy_hp_num { get; set; }

        public double enemy_dmg { get; set; }
        public double enemy_heal ()
        {
            return random.Next(4, 10);
        }
        public double enemy_parry ()
        {
            return random.Next(0, 3);
        }

        public double crit_chance ()
        {
            return random.Next(0, 2);
        }
        public double crit_rate = 0.5;
        public int floor = 1;
        public int action = 0;
        public int enemy_action = 0;
        public string img_path = string.Empty;

        public Dungeon()
        {
            InitializeComponent();
            hp_player.Maximum = player_hp_num;
            hp_enemy.Maximum = enemy_hp_num;
            hp_player.Value = player_hp_num;
            hp_enemy.Value = enemy_hp_num;
            Round();
        }
        private void sword_atck_Click (object sender, RoutedEventArgs e)
        {
            action = 1;
        }

        private void e_skill_Click(object sender, RoutedEventArgs e)
        {
            action = 2;
        }


        private void parry_Click(object sender, RoutedEventArgs e)
        {
            action = 3;
        }

        private void next_rnd_Click (object sender, RoutedEventArgs e)
        {
            Round();
        }


        public void Round()
        {
            if(hp_enemy.Value <= 0)
            {
                floor++;
            }
            if (floor == 1)
            {
                floor_round.Content = "I.";
                enemy.Content = "Sucrose";
                string img_path = "/Images/sucrose.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
                enemy_dmg = 4;
                enemy_hp_num = 200;
            }
            else if (floor == 2)
            {
                floor_round.Content = "II.";
                enemy.Content = "Ganyu";
                string img_path = "/Images/ganyu.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
                enemy_dmg = 6;
                enemy_hp_num = 400;
            }
            else if (floor == 3)
            {
                floor_round.Content = "III.";
                enemy.Content = "Hydro Slime";
                string img_path = "/Images/slime.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
                enemy_dmg = 8;
                enemy_hp_num = 600;
            }
            else if (floor == 4)
            {
                floor_round.Content = "IV.";
                enemy.Content = "Osial";
                string img_path = "/Images/osial.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
                enemy_dmg = 10;
                enemy_hp_num = 800;
            }
            else
            {
                //win window
            }
            switch (action)
            {
                case 1:
                    {
                        if (enemy_hp_num >= 790)
                        {
                            enemy_action = random.Next(0, 2);
                            if (enemy_action == 0)
                            {
                                Sword_attack();
                                Enemy_attack();
                            }
                            else if (enemy_action == 1)
                            {
                                Enemy_parry();
                            }
                        }
                        else
                        {
                            enemy_action = random.Next(0, 3);
                            if (enemy_action == 0)
                            {
                                Sword_attack();
                                Enemy_attack();
                            }
                            else if (enemy_action == 1)
                            {
                                Sword_attack();
                                Enemy_heal();
                            }
                            else if (enemy_action == 2)
                            {
                                Enemy_parry();
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        Heal();
                        if (enemy_hp_num >= 790)
                        {
                            Enemy_attack();
                        }
                        else
                        {
                            enemy_action = random.Next(0, 2);
                            if (enemy_action == 0)
                            {
                                Enemy_attack();
                            }
                            else if (enemy_action == 1)
                            {
                                Enemy_heal();
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        Parry();
                        break;
                    }
            }
        }

        public void Sword_attack()
        {
            //crit attack
            if (crit_chance() == 1)
            {
                player_dmg *= crit_rate;
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
                player_dmg = 11;
            }
            //unlucky basic attack
            else if(crit_chance() == 0)
            {
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
            }
        }

        public void Parry()
        {
            //unlucky neni parry
            if(player_parry() == 0)
            {
                Enemy_attack();
            }
            //je perry bez dmg
            else if (player_parry() == 1)
            {
                //no action
            }
            //je perry + bonus hit
            else if (player_parry() == 2)
            {
                Sword_attack();
            }
        }

        public void Heal()
        {
            //player heal
            hp_player.Value += player_heal();
        }

        public void Enemy_attack ()
        {
            //crit attack
            if (crit_chance() == 1)
            {
                enemy_dmg *= crit_rate;
                player_hp_num -= enemy_dmg;
                hp_player.Value = player_hp_num;
                enemy_dmg = 8;
            }
            //unlucky basic attack
            else if (crit_chance() == 0)
            {
                player_hp_num -= enemy_dmg;
                hp_player.Value = player_hp_num;
            }
        }

        public void Enemy_parry ()
        {
            //unlucky neni parry
            if (player_parry() == 0)
            {
                Sword_attack();
            }
            //je perry bez dmg
            else if (player_parry() == 1)
            {
                //no action
            }
            //je perry + bonus hit
            else if (player_parry() == 2)
            {
                Enemy_attack();
            }
        }

        public void Enemy_heal ()
        {
            //boss heal
            hp_enemy.Value += enemy_heal();
        }
    }
}
