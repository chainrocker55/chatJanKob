using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LiteChat
{
    public partial class UIchat : Form
    {
        MessageService msgService = new MessageService();

        private string myID, friendID;
        public UIchat()
        {
            InitializeComponent();
        }
        public UIchat(string myID, string friendID, string friendName)
        {
            InitializeComponent();
            this.myID = myID;
            this.friendID = friendID;
            this.name.Text = "Litechat : "+friendName;
        }
        private void backgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            
            msgService.serveMessageIncoming(new User(friendID), new User(myID));
            if (this.backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            if (this.displayMessageWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void sendButtonClick(object sender, EventArgs e)
        {

            if (InternetConnection.checkConnectionInternet() == true)
            {
                Message msg = new Message(textMsg.Text, myID, friendID);
                msgService.InsertMessage(msg);
                addMsgBox(msg, "my");
                textMsg.Clear();
            }
            else
            {
                showMessage();
            }

        }

        private void displayMessageWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {

                    if (msgService.getMessageQueue().Count > 0)
                    {
                        Message msg = msgService.getMessageQueue().Dequeue();
                        //Console.WriteLine(msg.MessageText);
                        if (!msg.Sender.Equals(myID))
                        {
                            addMsgBox(msg, "");
                        }
                        else
                        {
                            addMsgBox(msg, "my");
                        }
                    }
                    if (this.displayMessageWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }


                }
            }catch(Exception ee)
            {

            }
        }
        private void showMessage()
        {
            MessageBox.Show("No network connection");
        }

        private void chatLoad(object sender, EventArgs e)
        {
            msgService.fetchAllMessage(new User(friendID), new User(myID));
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }

            if (!displayMessageWorker.IsBusy)
            {
                displayMessageWorker.RunWorkerAsync();
            }

            
        }



        private void exit(object sender, EventArgs e)
        {
            /*
            this.Dispose();
            this.Close();
            */
            this.Hide();
        }

        private void addMsgBox(Message msg, String side)
        {
            try
            {
              
                    flow.BeginInvoke(new Action(() =>
                {
                    TextShow textShow = new TextShow();
                    Panel msgbox = textShow.getPanel(msg.MessageText, side);
                    flow.Controls.Add(msgbox);
                    textShow.Dispose();
                }));
                }
            
            catch (Exception e)
            {

            }

        }
    }
}
