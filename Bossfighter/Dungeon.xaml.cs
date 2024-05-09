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
            round();
        }

        private void e_skill_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sword_atck_Click(object sender, RoutedEventArgs e)
        {
            sword_attck();
        }

        private void parry_Click(object sender, RoutedEventArgs e)
        {

        }


        public void round()
        {
            hp_player.Maximum = player_hp_num;
            hp_enemy.Maximum = enemy_hp_num;
            floor_round.Content = Convert.ToString(floor);
        }
        public void sword_attck()
        {
            crit_chance = random.Next(0, 1);
            if (crit_chance == 1)
            {
                player_dmg *= crit_rate;
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
                player_dmg = 11;
            }
            else if(crit_chance == 0)
            {
                enemy_hp_num -= player_dmg;
                hp_enemy.Value = enemy_hp_num;
            }
        }
        public void parry()
        {
            player_parry = random.Next(0, 2);
            if(player_parry == 0)
            {

            }
            else if (player_parry == 1)
            {

            }
        }
    }
}
