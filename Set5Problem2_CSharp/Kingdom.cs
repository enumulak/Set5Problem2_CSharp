using System;
using System.Collections.Generic;

namespace Set5Problem2_CSharp
{
    public class Kingdom
    {
        //Private Variables
        private readonly string Name;
        private readonly string Emblem;
        private string incomingMessage = "";
        private bool isAlly;
        private string isAllyOf = "";
        private int numberOfAllies = 0;
        private readonly List<Kingdom> allies = new List<Kingdom>();
        private bool isCompetingForRulership;

        
        //Constructor with Arguments - No other Constructor for Kingdom is allowed. Instance of Kingdom has to be created through this Constructor
        public Kingdom(string name, string emblem)
        {
            Name = name;
            Emblem = emblem;
        }

        #region PRIVATE METHODS

        //Method that recieves the Incoming Message and sends it off for Processing
        private void ReceiveMessage(Kingdom sender, string msg, Kingdom receiver)
        {
            if (msg != "")
            {
                incomingMessage = msg;
                MessageProcessor.Instance.StartMessageProcessing(sender, msg, receiver);
            }
        }

        //Method that gets a Kingdom Object and sets it as an Ally of the Current Kingdom (object)
        private void SetAlly(Kingdom ally)
        {
            if (ally != null)
            {
                numberOfAllies++;
                allies.Add(ally);
            }
        }

        #endregion

        #region PUBLIC METHODS

        //Method used to send Message between two Kingdoms
        public void SendMessage(string msg, Kingdom receiver)
        {
            if (msg != "")
                receiver.ReceiveMessage(this, msg, receiver);
        }

        //Method that is used to process allegiance for the Kingdom that Receives a Message. We can do all sorts of checks here before the Kingdom registers itself as an Ally of another Kingdom (Message Sender)
        //The 'ruler' object is the Kingdom that sent the message
        public void ProcessAllegiance(bool flag, Kingdom ruler)
        {
            //The Flag is sent in by the Message Processor, confirming that the Kingdom's emblem was found in the Incoming Message
            if (flag)
            {
                //The Kingdom can only be an Ally if it is not already an Ally of another kingdom AND is not competing for rulership of Southeros
                if (!isAlly && !isCompetingForRulership)
                {
                    isAlly = true;
                    isAllyOf = ruler.GetKingdomName();
                    //If everything is satisfactory, the Kingdom which is processing the allegiance registers itself as an Ally of the 'ruler' kingdom
                    ruler.SetAlly(this);
                }
            }
        }

        //Method used by Current Kingdom to Display its Allies
        public void DisplayAllies()
        {
            if (allies.Count > 0)
            {
                for (var i = 0; i < allies.Count; i++)
                {
                    Console.WriteLine("Ally {0}: {1}", i + 1, allies[i].GetKingdomName());
                }
            }
            else
            {
                Console.WriteLine("{0} has no Allies...", GetKingdomName());
            }
        }

        public void SetRulershipCompetingStatus(bool status)
        {
            isCompetingForRulership = status;
        }

        #endregion

        #region DATA READERS

        public string GetKingdomName() => Name;
        public string GetEmblem() => Emblem;
        public string GetAllyName(int index) => allies[index].GetKingdomName();
        public string GetIncomingMessage() => incomingMessage;
        public int GetNumberOfAllies() => numberOfAllies;
        public bool IsAlly() => isAlly;
        public string IsAllyOf() => isAllyOf;

        #endregion

    }
}
