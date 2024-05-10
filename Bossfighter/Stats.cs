using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bossfighter
{
    public class Stats
    {
        public static Random random = new();
        public double player_hp_num = 200;
        public double player_hp_hold {  get; set; }
        public double player_dmg_hold { get; set; }
        public double player_dmg = 11;
        public double player_heal ()
        {
            return random.Next(8, 20);
        }
        public double player_parry ()
        {
            return random.Next(0, 3);
        }
        public double player_increase_hp ()
        {
            return random.Next(1, 20);
        }
        public double player_increase_dmg ()
        {
            return random.Next(1, 5);
        }

        public double enemy_hp_num = 2000;

        public double enemy_dmg = 4;
        public double enemy_dmg_hold { get; set; }
        public double enemy_hp_hold { get; set; }
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
    }
}
