using System;
using System.Collections.Generic;
using System.Linq;

namespace Set5Problem2_CSharp
{
    public sealed class HighPriest
    {
        //Private Variables
        private List<string> messages;
        private List<Kingdom> theConquerors;
        private List<Kingdom> theKingdoms;
        private int numberOfMessages;
        private List<int> allyCountForConqueror;

        //Private Constructor, so outside users do not have access and Data can be initialized for this Singleton Class
        private HighPriest()
        {
            messages = new List<string>();
            allyCountForConqueror = new List<int>();

            //For Now, let's hard-code the number of random messages that are required
            numberOfMessages = 6;
        }

        //Static Method to access Singleton Instance of this class
        public static HighPriest Instance { get; } = new HighPriest();

        //Public Methods
        public void RegisterConquerorsAndKingdoms(List<Kingdom> c, List<Kingdom> k)
        {
            theConquerors = c;
            theKingdoms = k;

            //We do another check to ensure that the above lists are not empty, and then start the Ballot process for Rulership
            if(theConquerors.Count != 0 && theKingdoms.Count != 0)
            { StartBallotForRulership(); }
        }

        //Private Methods
        private void StartBallotForRulership()
        {
            //Clear the Current Ally Count List for all Conquerors
            allyCountForConqueror.Clear();

            //First we loop through the Conquerors List
            for(var i = 0; i < theConquerors.Count; i++)
            {
                //Store name of the current conqueror in a temporary place
                var name = theConquerors[i].GetKingdomName().ToLower();

                //Generate the random message list
                GenerateRandomMessageList();

                //Now we loop through the Kingdoms List
                for(var k = 0; k < theKingdoms.Count; k++)
                {
                    //Conqueror sends messages to all Kingdoms except itself
                    if(name != theKingdoms[k].GetKingdomName().ToLower())
                    {
                        theConquerors[i].SendMessage(messages[k], theKingdoms[k]);
                        //Console.WriteLine("Message {0} sent to {1} Kingdom, by the {2} Kingdom", messages[k], theKingdoms[k].GetKingdomName(), theConquerors[i].GetKingdomName());
                    }
                }

                //Clear the Current Message List, so that we have a new one for the next conqueror
                messages.Clear();

                //Store the number of Allies for current conqueror in the private list and display it also
                allyCountForConqueror.Add(theConquerors[i].GetNumberOfAllies());
                Console.WriteLine('\n');
                Console.WriteLine("Number of Allies for {0}: {1}", theConquerors[i].GetKingdomName(), allyCountForConqueror[i].ToString());
                Console.WriteLine('\n');
            }
            
            //Once all Conquerors have sent messages, Decide who the Ruler is and set rulership of Southeros
            DecideRulership();
        }

        private void GenerateRandomMessageList()
        {
            //Get the Current size of the Message DB
            var currentMessageDBSize = MessageDB.Instance.GetMessageDBListCount();

            //Create an Object of type Random
            Random random = new Random();

            //Loop through the number of messages required, generate a random index between 1 - MessageDBCount, retrieve message from DB at that index and store the message in the Private list
            for(var i = 0; i < numberOfMessages; i++)
            {
                var rIndex = random.Next(1, currentMessageDBSize);
                messages.Add(MessageDB.Instance.GetMessageFromDB(rIndex));
            }
        }

        private void DecideRulership()
        {
            //Get the largest value from the Ally Count List
            var maxValue = allyCountForConqueror.Max();

            //Get the index for the above largest value
            var maxIndex = allyCountForConqueror.IndexOf(maxValue);

            //Temporary flag to check if there is a tie for rulership
            var isThereATie = false;

            //First, we check if all values in the Ally Count List are equal or not (a straight-forward tie)
            if(allyCountForConqueror.Distinct().Count() == 1)
            {
                isThereATie = true;
            }
            else
            {
                //If not, we loop through the list and see if there is any value which is Equal to the Largest Value in the list (in which case it will be a tie)
                for (var i = 0; i < allyCountForConqueror.Count; i++)
                {
                    if (allyCountForConqueror[i] == maxValue && i != maxIndex)
                    {
                        isThereATie = true;
                    }
                }
            }
            //If we have a tie, we re-initiate the Ballot, otherwise we assign Conqueror at the maxIndex as ruler of Southeros
            if(isThereATie)
            {
                Console.WriteLine("There is a Tie ! Re-initiating Ballot for Ruler..");
                Console.WriteLine('\n');

                StartBallotForRulership();
            }
            else
            {
                Southeros.Instance.SetCurrentRuler(theConquerors[maxIndex]);
            }                          
        }
    }
}
