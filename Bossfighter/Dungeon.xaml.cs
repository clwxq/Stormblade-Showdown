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
        public static BitmapImage bitmap = new();
        public static Stats stats = new();
        public static Random random = new();
        LoseScreen losescreen = new();
        WinScreen winscreen = new();

        public Dungeon()
        {
            InitializeComponent();
            Floor_checker();
            Round();
        }
        private void sword_atck_Click (object sender, RoutedEventArgs e)
        {
            stats.action = 1;
        }

        private void e_skill_Click(object sender, RoutedEventArgs e)
        {
            stats.action = 2;
        }


        private void parry_Click(object sender, RoutedEventArgs e)
        {
            stats.action = 3;
        }

        private void next_rnd_Click (object sender, RoutedEventArgs e)
        {
            Round();
        }


        public void Round()
        {
            hp_player.Maximum = stats.player_hp_hold;
            hp_enemy.Maximum = stats.enemy_hp_hold;
            hp_player.Value = stats.player_hp_num;
            hp_enemy.Value = stats.enemy_hp_num;
            if (stats.player_hp_num <= 0)
            {
                this.Close();
                losescreen.Show();
            }
            else if (stats.enemy_hp_num <= 0)
            {
                stats.floor++;
                Floor_checker();
            }
            else
            {
                switch (stats.action)
                {
                    case 1:
                        {
                            if (stats.enemy_hp_num >= 790)
                            {
                                stats.enemy_action = random.Next(0, 2);
                                if (stats.enemy_action == 0)
                                {
                                    Sword_attack();
                                    Enemy_attack();
                                }
                                else if (stats.enemy_action == 1)
                                {
                                    Enemy_parry();
                                }
                            }
                            else
                            {
                                stats.enemy_action = random.Next(0, 3);
                                if (stats.enemy_action == 0)
                                {
                                    Sword_attack();
                                    Enemy_attack();
                                }
                                else if (stats.enemy_action == 1)
                                {
                                    Sword_attack();
                                    Enemy_heal();
                                }
                                else if (stats.enemy_action == 2)
                                {
                                    Enemy_parry();
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            Heal();
                            if (stats.enemy_hp_num >= 790)
                            {
                                Enemy_attack();
                            }
                            else
                            {
                                stats.enemy_action = random.Next(0, 2);
                                if (stats.enemy_action == 0)
                                {
                                    Enemy_attack();
                                }
                                else if (stats.enemy_action == 1)
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
        }
        public void Floor_checker()
        {
            if (stats.floor == 1)
            {
                stats.enemy_dmg = 4;
                stats.enemy_hp_num = 2000;
                Player_buffer();
                floor_round.Content = "I.";
                enemy.Content = "Sucrose";
                string img_path = "/Images/sucrose.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
            }
            else if (stats.floor == 2)
            {
                stats.enemy_dmg = 6;
                stats.enemy_hp_num = 4000;
                Player_buffer();
                floor_round.Content = "II.";
                enemy.Content = "Ganyu";
                string img_path = "/Images/ganyu.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
            }
            else if (stats.floor == 3)
            {
                stats.enemy_dmg = 8;
                stats.enemy_hp_num = 6000;
                Player_buffer();
                floor_round.Content = "III.";
                enemy.Content = "Hydro Slime";
                string img_path = "/Images/slime.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
            }
            else if (stats.floor == 4)
            {
                stats.enemy_dmg = 10;
                stats.enemy_hp_num = 8000;
                Player_buffer();
                floor_round.Content = "IV.";
                enemy.Content = "Osial";
                string img_path = "/Images/osial.png";
                BitmapImage bitmap = new BitmapImage(new Uri(img_path, UriKind.Relative));
                enemy_img.Source = bitmap;
            }
            else
            {
                this.Close();
                winscreen.Show();
            }
        }

        public void Sword_attack()
        {
            //crit attack
            if (stats.crit_chance() == 1)
            {
                stats.player_dmg *= stats.crit_rate;
                stats.enemy_hp_num -= stats.player_dmg;
                hp_enemy.Value = stats.enemy_hp_num;
                chat.Text = $"You dealt ({stats.player_dmg}dmg)";
                stats.player_dmg = stats.player_dmg_hold;
            }
            //unlucky basic attack
            else if(stats.crit_chance() == 0)
            {
                stats.enemy_hp_num -= stats.player_dmg;
                hp_enemy.Value = stats.enemy_hp_num;
                chat.Text = $"You dealt ({stats.player_dmg}dmg)";
            }
        }

        public void Parry()
        {
            //unlucky neni parry
            if(stats.player_parry() == 0)
            {
                Enemy_attack();
                chat.Text = $"You failed the parry and took ({stats.enemy_dmg}dmg)";
            }
            //je perry bez dmg
            else if (stats.player_parry() == 1)
            {
                //no action
                chat.Text = $"You succesfully paried without bonus dmg";
            }
            //je perry + bonus hit
            else if (stats.player_parry() == 2)
            {
                chat.Text = $"You succesfully paried and dealt ({stats.player_dmg}dmg)";
                Sword_attack();
            }
        }

        public void Heal()
        {
            //player heal
            stats.player_hp_num += stats.player_heal();
            hp_player.Value = stats.player_hp_num;
            chat.Text = $"You healed for ({stats.player_heal()}hp)";
        }

        public void Enemy_attack ()
        {
            //crit attack
            if (stats.crit_chance() == 1)
            {
                stats.enemy_dmg *= stats.crit_rate;
                stats.player_hp_num -= stats.enemy_dmg;
                hp_player.Value = stats.player_hp_num;
                chat.Text = $"Enemy dealt ({stats.enemy_dmg})";
                stats.enemy_dmg = stats.enemy_dmg_hold;
            }
            //unlucky basic attack
            else if (stats.crit_chance() == 0)
            {
                stats.player_hp_num -= stats.enemy_dmg;
                hp_player.Value = stats.player_hp_num;
                chat.Text = $"Enemy dealt ({stats.enemy_dmg}dmg)";
            }
        }

        public void Enemy_parry ()
        {
            //unlucky neni parry
            if (stats.player_parry() == 0)
            {
                chat.Text = $"Enemy failed the parry and took ({stats.player_dmg}dmg)";
                Sword_attack();
            }
            //je perry bez dmg
            else if (stats.player_parry() == 1)
            {
                //no action
                chat.Text = $"Enemy succesfully paried without bonus dmg";
            }
            //je perry + bonus hit
            else if (stats.player_parry() == 2)
            {
                chat.Text = $"Enemy succesfully paried and dealt ({stats.enemy_dmg}dmg)";
                Enemy_attack();
            }
        }

        public void Enemy_heal ()
        {
            //boss heal
            stats.enemy_hp_num += stats.enemy_heal();
            hp_enemy.Value = stats.enemy_hp_num;
            chat.Text = $"Enemy healed for ({stats.enemy_heal()}hp)";
        }

        public void Player_buffer() //+enemy dmg hold
        {
            stats.player_hp_num += stats.player_increase_hp();
            stats.player_dmg += stats.player_increase_dmg();
            stats.player_hp_hold = stats.player_hp_num;
            stats.player_dmg_hold = stats.player_dmg;
            stats.enemy_dmg_hold = stats.enemy_dmg;
            stats.enemy_hp_hold = stats.enemy_hp_num;
        }
    }
}
