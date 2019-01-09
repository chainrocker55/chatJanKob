using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiteChat
{
    public partial class UImain : Form
    {

        public static User user;
        private string userID="123456";
        public UImain(String userID)
        {
            InitializeComponent();
            user = UserService.getUser(userID);
            this.userID = userID;
            name.Text = user.Username;
        }
      

        private void formLoad(object sender, EventArgs e)
        {
            showListFriend();

        }
 
        public  void showListFriend()
        {
            name.Text = user.Username;
            tvFriendFlow.Controls.Clear();

            var friList = MainService.getAllFriend(userID);
            while (friList.Count > 0)
            {
                
                UIchat chat = new UIchat(userID, friList[0].FriID, friList[0].FriName);
                UImFriendList uif = new UImFriendList(chat);
                Panel x = uif.getPanel(friList[0].FriName, friList[0].FriID,userID);
                
                
                x.Click += (s, e) => {
                    chat.Show();
                };
                tvFriendFlow.Controls.Add(x);

 
                friList.RemoveAt(0);
            }
        }

      

        private void editUser(object sender, EventArgs e)
        {
            EditUser edit = new EditUser(user);
            edit.ShowDialog();
            if (edit.DialogResult == DialogResult.Cancel)
            {
                this.name.Text = edit.getName();
            }
        }

        private void closeAccount(object sender, EventArgs e)
        {
            CloseAccount c = new CloseAccount(userID);
            c.Show();
        }

        private void addFriend(object sender, EventArgs e)
        {
            var friList = MainService.getAllFriend(userID);

            if (FriendID.Text.Equals(userID))
            {
                MessageBox.Show("คุณไม่สามารถเพิ่มเพื่อนตัวเองได้!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!MainService.searchFromList(FriendID.Text, friList))
            {
                MessageBox.Show("เป็นเพื่อนกันอยู่แล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!MainService.checkIfFriendExist(FriendID.Text))
            {
                MessageBox.Show("ไม่พบไอดีนี้!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MainService.insertFriend(userID, FriendID.Text);
            showListFriend();
        }

        private void exit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

       
    }
    
}
