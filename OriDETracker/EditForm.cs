using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OriDE.Memory;

namespace OriDETracker
{
	public partial class EditForm : Form
	{
		protected Tracker parent;
		public EditForm(Tracker p)
		{
			this.parent = p;
			InitializeComponent();
		}

		private bool rando = true;

		public void ChangeMapstone(bool new_val)
		{
			numeric_mapstone.Enabled = new_val;
			button_down.Enabled = new_val;
			button_up.Enabled = new_val;
		}
		public void ChangeShards(bool new_val)
		{
			cb_shard_wv1.Enabled = new_val;
			cb_shard_wv2.Enabled = new_val;

			cb_shard_gs1.Enabled = new_val;
			cb_shard_gs2.Enabled = new_val;

			cb_shard_ss1.Enabled = new_val;
			cb_shard_ss2.Enabled = new_val;

            cb_shards.Checked = new_val;
		}
		public void ChangeWarmth(bool new_val)
		{
			cb_event_warmth.Enabled = new_val;
		}
		public void Reset()
		{
			Clear();
            cb_shards.Checked = false;

			cb_shard_wv1.Enabled = false;
			cb_shard_wv2.Enabled = false;

			cb_shard_gs1.Enabled = false;
			cb_shard_gs2.Enabled = false;

			cb_shard_ss1.Enabled = false;
			cb_shard_ss2.Enabled = false;

			cb_event_warmth.Enabled = false;

		}
		public void Clear()
		{
			cb_skill_sein.Checked = false;
			cb_skill_wj.Checked = false;
			cb_skill_cflame.Checked = false;
			cb_skill_djump.Checked = false;
			cb_skill_bash.Checked = false;
			cb_skill_stomp.Checked = false;
			cb_skill_glide.Checked = false;
			cb_skill_climb.Checked = false;
			cb_skill_cjump.Checked = false;
			cb_skill_grenade.Checked = false;
			cb_skill_dash.Checked = false;

			cb_tree_sein.Checked = false;
			cb_tree_wj.Checked = false;
			cb_tree_cflame.Checked = false;
			cb_tree_djump.Checked = false;
			cb_tree_bash.Checked = false;
			cb_tree_stomp.Checked = false;
			cb_tree_glide.Checked = false;
			cb_tree_climb.Checked = false;
			cb_tree_cjump.Checked = false;
			cb_tree_grenade.Checked = false;
			cb_tree_dash.Checked = false;

			cb_event_cleanwater.Checked = false;
			cb_event_wind.Checked = false;
			cb_event_warmth.Checked = false;
			cb_event_watervein.Checked = false;
			cb_event_gumonseal.Checked = false;
			cb_event_sunstone.Checked = false;

			cb_shard_wv1.Checked = false;
			cb_shard_wv2.Checked = false;

			cb_shard_gs1.Checked = false;
			cb_shard_gs2.Checked = false;

			cb_shard_ss1.Checked = false;
			cb_shard_ss2.Checked = false;

			numeric_mapstone.Value = 0;
		}

		public void UpdateSkill(String sk, bool b)
		{
			switch (sk)
			{
			case "Sein":
				cb_skill_sein.Checked = b;
				break;
			case "WallJump":
				cb_skill_wj.Checked = b;
				break;
			case "ChargeFlame":
				cb_skill_cflame.Checked = b;
				break;
			case "DoubleJump":
				cb_skill_djump.Checked = b;
				break;
			case "Bash":
				cb_skill_bash.Checked = b;
				break;
			case "Stomp":
				cb_skill_stomp.Checked = b;
				break;
			case "Glide":
				cb_skill_glide.Checked = b;
				break;
			case "Climb":
				cb_skill_climb.Checked = b;
				break;
			case "ChargeJump":
				cb_skill_cjump.Checked = b;
				break;
			case "Grenade":
				cb_skill_grenade.Checked = b;
				break;
			case "Dash":
				cb_skill_dash.Checked = b;
				break;
			default:
				break;
			}
		}
		public void UpdateTree(String sk, bool b)
		{
			switch (sk)
			{
            case "Sein":
                cb_tree_sein.Checked = b;
                break;
            case "WallJump":
                cb_tree_wj.Checked = b;
                break;
            case "ChargeFlame":
                cb_tree_cflame.Checked = b;
                break;
            case "DoubleJump":
                cb_tree_djump.Checked = b;
                break;
            case "Bash":
                cb_tree_bash.Checked = b;
                break;
            case "Stomp":
                cb_tree_stomp.Checked = b;
                break;
            case "Glide":
                cb_tree_glide.Checked = b;
                break;
            case "Climb":
                cb_tree_climb.Checked = b;
                break;
            case "ChargeJump":
                cb_tree_cjump.Checked = b;
                break;
            case "Grenade":
                cb_tree_grenade.Checked = b;
                break;
            case "Dash":
                cb_tree_dash.Checked = b;
                break;
            default:
                break;
            }
        }
		public void UpdateEvent(String ev, bool b)
		{
			switch (ev)
			{
			case "Water Vein":
				cb_event_watervein.Checked = b;
				break;
			case "Gumon Seal":
				cb_event_gumonseal.Checked = b;
				break;
			case "Sunstone":
				cb_event_sunstone.Checked = b;
				break;
			case "Warmth Returned":
				if (rando)
				{
					cb_event_cleanwater.Checked = b;
				}
				else
				{
					cb_event_warmth.Checked = b;
				}
				break;
			case "Wind Restored":
				cb_event_wind.Checked = b;
				break;
			case "Clean Water":
				cb_event_cleanwater.Checked = b;
				break;
			}
		}
		public void UpdateShard(String ev, bool b)
		{
			switch (ev)
			{
			case "Water Vein 1":
				cb_shard_wv1.Checked = b;
				break;
			case "Water Vein 2":
				cb_shard_wv2.Checked = b;
				break;
			case "Gumon Seal 1":
				cb_shard_gs1.Checked = b;
				break;
			case "Gumon Seal 2":
				cb_shard_gs2.Checked = b;
				break;
			case "Sunstone 1":
				cb_shard_ss1.Checked = b;
				break;
			case "Sunstone 2":
				cb_shard_ss2.Checked = b;
				break;

			}
		}
		public void UpdateMapstones(int ms)
		{
			this.numeric_mapstone.Value = ms;
		}

		protected void SendSkillChange(String sk, bool b)
		{
			parent.haveSkill[sk] = b;
			parent.Refresh();
		}
		protected void SendTreeChange(String sk, bool b)
		{
			parent.haveTree[sk] = b;
			parent.Refresh();
		}
		protected void SendEventChange(String ev, bool b)
		{
            parent.haveEvent[ev] = b;
            parent.Refresh();
		}
		protected void SendShardChange(String ev, bool b)
		{
			parent.haveShards[ev] = b;
			parent.Refresh();

		}
		protected void SendMapstoneChange(int ms)
		{
			parent.MapstoneCount = ms;
			parent.Refresh();
		}


		#region Skills
		private void cb_skill_sein_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Sein", cb_skill_sein.Checked);
		}

		private void cb_skill_wj_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("WallJump", cb_skill_wj.Checked);
		}

		private void cb_skill_cflame_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("ChargeFlame", cb_skill_cflame.Checked);

		}

		private void cb_skill_djump_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("DoubleJump", cb_skill_djump.Checked);
		}

		private void cb_skill_bash_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Bash", cb_skill_bash.Checked);

		}

		private void cb_skill_stomp_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Stomp", cb_skill_stomp.Checked);
		}

		private void cb_skill_glide_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Glide", cb_skill_glide.Checked);

		}

		private void cb_skill_climb_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Climb", cb_skill_climb.Checked);

		}

		private void cb_skill_cjump_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("ChargeJump", cb_skill_cjump.Checked);

		}

		private void cb_skill_grenade_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Grenade", cb_skill_grenade.Checked);

		}

		private void cb_skill_dash_MouseClick(object sender, EventArgs e)
		{
			SendSkillChange("Dash", cb_skill_dash.Checked);

		}
		#endregion

		#region Trees
		private void cb_tree_sein_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Sein", cb_tree_sein.Checked);
		}

		private void cb_tree_wj_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("WallJump", cb_tree_wj.Checked);
		}

		private void cb_tree_cflame_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("ChargeFlame", cb_tree_cflame.Checked);

		}

		private void cb_tree_djump_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("DoubleJump", cb_tree_djump.Checked);
		}

		private void cb_tree_bash_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Bash", cb_tree_bash.Checked);

		}

		private void cb_tree_stomp_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Stomp", cb_tree_stomp.Checked);
		}

		private void cb_tree_glide_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Glide", cb_tree_glide.Checked);

		}

		private void cb_tree_climb_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Climb", cb_tree_climb.Checked);

		}

		private void cb_tree_cjump_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("ChargeJump", cb_tree_cjump.Checked);

		}

		private void cb_tree_grenade_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Grenade", cb_tree_grenade.Checked);

		}

		private void cb_tree_dash_MouseClick(object sender, EventArgs e)
		{
			SendTreeChange("Dash", cb_tree_dash.Checked);

		}
		#endregion

		#region Events
		private void cb_event_watervein_MouseClick(object sender, EventArgs e)
		{
            SendEventChange("Water Vein", cb_event_watervein.Checked);
		}

		private void cb_event_gumonseal_MouseClick(object sender, EventArgs e)
		{
			SendEventChange("Gumon Seal", cb_event_gumonseal.Checked);
		}

		private void cb_event_sunstone_MouseClick(object sender, EventArgs e)
		{
			SendEventChange("Sunstone", cb_event_sunstone.Checked);

		}

		private void cb_event_cleanwater_MouseClick(object sender, EventArgs e)
		{
			SendEventChange("Clean Water", cb_event_cleanwater.Checked);
		}

		private void cb_event_wind_MouseClick(object sender, EventArgs e)
		{
			SendEventChange("Wind Restored", cb_event_wind.Checked);
		}

		private void cb_event_warmth_MouseClick(object sender, EventArgs e)
		{
			SendEventChange("Warmth Returned", cb_event_warmth.Checked);
		}
		#endregion

		#region Shards
		private void cb_shard_wv1_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Water Vein 1", cb_shard_wv1.Checked);

		}

		private void cb_shard_wv2_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Water Vein 2", cb_shard_wv2.Checked);

		}

		private void cb_shard_gs1_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Gumon Seal 1", cb_shard_gs1.Checked);

		}

		private void cb_shard_gs2_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Gumon Seal 2", cb_shard_gs2.Checked);

		}

		private void cb_shard_ss1_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Sunstone 1", cb_shard_ss1.Checked);

		}

		private void cb_shard_ss2_MouseClick(object sender, EventArgs e)
		{
			SendShardChange("Sunstone 2", cb_shard_ss2.Checked);

		}
		#endregion

		#region Other
		private void button_down_Click(object sender, EventArgs e)
		{
			if (numeric_mapstone.Value > 0)
			{
				numeric_mapstone.Value -= 1;
				SendMapstoneChange((int) numeric_mapstone.Value);
			}
		}
		private void button_up_Click(object sender, EventArgs e)
		{
			if (numeric_mapstone.Value < 9)
			{
				numeric_mapstone.Value += 1;
				SendMapstoneChange((int) numeric_mapstone.Value);
			}
		}
		private void numeric_mapstone_ValueChanged(object sender, EventArgs e)
		{
			SendMapstoneChange((int) numeric_mapstone.Value);
		}

		private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!(e.CloseReason == CloseReason.ApplicationExitCall || e.CloseReason == CloseReason.FormOwnerClosing))
			{
				this.Visible = false;
				e.Cancel = true;
			}
		}

		#endregion

		private void cb_shards_MouseClick(object sender, EventArgs e)
		{
			parent.DisplayShards = cb_shards.Checked;
			parent.ChangeShards();
            parent.Refresh();
		}

        private void button_title_text_Click(object sender, EventArgs e)
        {
            parent.Text = title_text_box.Text;
        }

        private void button_transparent_Click(object sender, EventArgs e)
        {
            parent.BackColor = Color.Black;
            parent.TransparencyKey = Color.Black;
            parent.Opacity = 70;
        }
    }
}
