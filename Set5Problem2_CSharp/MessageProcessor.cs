using System.Linq;

namespace Set5Problem2_CSharp
{
    public sealed class MessageProcessor
    {
        //Private Variables - Two Kingdom objects that represent the message sender and receiver
        private Kingdom Sender;
        private Kingdom Receiver;

        //Static method to provide access to the Singleton Instance of this Class
        public static MessageProcessor Instance { get; } = new MessageProcessor();

        private EmblemFinder emblemFinder = new EmblemFinder();


        public void StartMessageProcessing(Kingdom sender, string msg, Kingdom receiver)
        {
            Sender = sender;
            Receiver = receiver;

            var found = emblemFinder.ProcessMessageForKingdom(msg, receiver);

            if(found)
            {
                //Transmit confirmation to the Receiver that its Emblem was found in the Incoming Message, and let it decide if it wants to be an Ally or not
                receiver.ProcessAllegiance(found, Sender);
            }
        }
    }

    public class EmblemFinder
    {
        public bool ProcessMessageForKingdom(string msg, Kingdom receiver)
        {
            var found = receiver.GetEmblem().ToCharArray().Select(c => char.ToUpper(c)).Distinct().All(c => msg.ToUpper().Contains(c));

            return found;
        }
    }
}
