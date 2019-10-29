using ExileCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPlagueBearer
{
    public class AutoPlagueBearerPlugin : BaseSettingsPlugin<AutoPlagueBearerSetting>
    {
        DateTime _lastCast = DateTime.MinValue;

        public override Job Tick()
        {
            var player = this.GameController.Game.IngameState.Data.LocalPlayer;
            var entities = this.GameController.Entities;
            var buffs = player.Buffs;

            if (!this.Settings.Enable)
            {
                return null;
            }

            // Return null if cool down span
            TimeSpan cooldownSpan = TimeSpan.FromMilliseconds(this.Settings.CoolDown);
            if (_lastCast + cooldownSpan > DateTime.UtcNow)
            {
                return null;
            }

            bool enemiesNearby = false;
            foreach (var entity in entities)
            {
                if (entity.IsAlive && entity.IsHostile)
                {
                    float distance = (player.Pos - entity.Pos).Length();
                    if (distance <= this.Settings.Range)
                    {
                        enemiesNearby = true;
                    }
                }
            }

            bool auraOn = buffs.Any(b => b.Name == "corrosive_shroud_aura");

            if (enemiesNearby)
            {
                // Turn aura on if enemies nearby
                if (!auraOn)
                {
                    Input.KeyDown(this.Settings.HotKey);
                    Input.KeyUp(this.Settings.HotKey);
                    _lastCast = DateTime.UtcNow;
                }
            }
            else
            {
                // Turn off aura if enemies not nearby
                if (auraOn)
                {
                    Input.KeyDown(this.Settings.HotKey);
                    Input.KeyUp(this.Settings.HotKey);
                    _lastCast = DateTime.UtcNow;
                }
            }

            return null;
        }
    }
}
