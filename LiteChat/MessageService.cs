using System.Collections.Generic;
namespace LiteChat
{
    class MessageService
    {
        MessageDAO messageDAO = new MessageDAO();

        public void serveMessageIncoming(User sender, User reciever)
        {
            messageDAO.getMessage(sender, reciever);
        }

        public void fetchAllMessage(User sender, User reciever)
        {
            messageDAO.pullAllMessage(sender, reciever);
        }
        public void InsertMessage(Message DocMsg)
        {
            messageDAO.createMessage(DocMsg);
        }
        public Queue<Message> getMessageQueue()
        {
            return messageDAO.getMessageQueue();
        }
    }
}
