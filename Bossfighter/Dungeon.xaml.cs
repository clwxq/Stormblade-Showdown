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

        public double player_hp_num = 100;
        public double player_dmg = 11;
        public double player_heal = 10;
        public double player_parry { get; set; }

        public double enemy_hp_num = 1000;
        public double enemy_dmg = 8;
        public double enemy_heal { get; set; }
        public double enemy_parry { get; set; }

        public double crit_chance { get; set; }
        public double crit_rate = 0.5;
        public int floor = 1;

        public Dungeon()
        {
            InitializeComponent();
            Round();
        }

        private void e_skill_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sword_atck_Click(object sender, RoutedEventArgs e)
        {
            Sword_attck();
        }

        private void parry_Click(object sender, RoutedEventArgs e)
        {
            Parry();
        }


        public void Round()
        {
            hp_player.Maximum = player_hp_num;
            hp_enemy.Maximum = enemy_hp_num;
            floor_round.Content = Convert.ToString(floor);
        }
        public void Sword_attck()
        {
            //crit attack
            RNG();
            if (crit_chance == 1)
            {
                player_dmg *= crit_rate;
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
                player_dmg = 11;
                MessageBox.Show("Crit attack");
            }
            //unlucky basic attack
            else if(crit_chance == 0)
            {
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
                MessageBox.Show("Basic attack");
            }
        }
        public void Parry()
        {
            RNG();
            //unlucky neni parry
            if(player_parry == 0)
            {
                if(crit_chance == 1)
                {
                    enemy_dmg *= crit_rate;
                    player_hp_num -= enemy_dmg;
                    hp_player.Value = player_hp_num;
                    enemy_dmg = 8;
                    MessageBox.Show("Nono parry + crit attack");
                }
                else if(player_parry == 0) 
                {
                    player_hp_num -= enemy_dmg;
                    hp_player.Value = player_hp_num;
                    MessageBox.Show("Nono parry + basic attack");
                }
            }
            //je perry bez dmg
            else if (player_parry == 1)
            {
                MessageBox.Show("Parry");
            }
            //je perry + bonus hit
            else if (player_parry == 2)
            {
                if (crit_chance == 1)
                {
                    player_dmg *= crit_rate;
                    enemy_hp_num -= player_dmg;
                    hp_enemy.Value = enemy_hp_num;
                    player_dmg = 11;
                    MessageBox.Show("Parry + crit attack");
                }
                //unlucky basic attack
                else if (crit_chance == 0)
                {
                    enemy_hp_num -= player_dmg;
                    hp_enemy.Value = enemy_hp_num;
                    MessageBox.Show("Parry + basic attack");
                }
            }
        }


        public void RNG()
        {
            player_parry = random.Next(0, 2);
            enemy_heal = random.Next(0, 1);
            enemy_parry = random.Next(0, 2);
            crit_chance = random.Next(0, 1);
        }
    }
}
