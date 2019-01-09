using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteChat
{
    public partial class UImFriendList : Form
    {
        private string myID, friendID,friendName;

        public System.Windows.Forms.Label lbName = new System.Windows.Forms.Label();


        public UImFriendList(UIchat x)
        {
            InitializeComponent();
            this.lbName.BackColor = System.Drawing.Color.LightPink;
            this.lbName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbName.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbName.ForeColor = System.Drawing.Color.White;
            this.lbName.Location = new System.Drawing.Point(3, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(264, 47);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "label1";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(this.lbName);
            lbName.Click += (s, e) => {
                x.Show();
            };
        }
        
        public Panel getPanel(string name,string id,string myId)
        {
            lbName.Text = name;
            this.myID = myId;
            this.friendID = id;
            this.friendName = name;
            return this.panel1;
        }
    }
}
