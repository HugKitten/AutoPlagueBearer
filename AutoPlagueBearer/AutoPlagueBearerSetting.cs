using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using System.Windows.Forms;

namespace AutoPlagueBearer
{
    public class AutoPlagueBearerSetting : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        public RangeNode<int> MinimumMana { get; set; } = new RangeNode<int>(6, 0, 100);
        public RangeNode<int> Range { get; set; } = new RangeNode<int>(500, 0, 10000);
        public RangeNode<int> CoolDown { get; set; } = new RangeNode<int>(511, 0, 25000);
        public HotkeyNode HotKey { get; set; } = new HotkeyNode(Keys.None);
        public HotkeyNode ToggleSkillSetNode { get; set; } = new HotkeyNode(Keys.ControlKey);
    }
}
