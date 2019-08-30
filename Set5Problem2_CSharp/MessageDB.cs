using System.Collections.Generic;

namespace Set5Problem2_CSharp
{
    public sealed class MessageDB
    {
        //Private variable to hold a list of String Messages
        private List<string> messagesToKingdoms = new List<string>();
        private string error;
        
        //Private Constructor, so that outside access is restricted and data can be initialized
        private MessageDB()
        {
            error = "There was an error in Processing your Request..";

            messagesToKingdoms.Add("Summer is coming");
            messagesToKingdoms.Add("a1d22n333a4444p");
            messagesToKingdoms.Add("oaaawaala");
            messagesToKingdoms.Add("zmzmzmzaztzozh");
            messagesToKingdoms.Add("go risk it all");
            messagesToKingdoms.Add("Let's swing the sword together");
            messagesToKingdoms.Add("Die or play the tame of thrones");
            messagesToKingdoms.Add("Ahoy! Fight for me with men and money");
            messagesToKingdoms.Add("Drag on Martin!");
            messagesToKingdoms.Add("When you play the tame of thrones you win or die");
            messagesToKingdoms.Add("What could we say to the Lord of Death? Game On?");
            messagesToKingdoms.Add("Turn us away or we will burn you first");
            messagesToKingdoms.Add("Death is so terribly final while life is full of possibilities");
            messagesToKingdoms.Add("You win or you die");
            messagesToKingdoms.Add("His watch is ended");
            messagesToKingdoms.Add("Sphinx of black quartz judge my dozen vows");
            messagesToKingdoms.Add("Fear cuts deeper than swords My Lord");
            messagesToKingdoms.Add("Different raods sometimes lead to the same castle");
            messagesToKingdoms.Add("A DRAGON IS NOT A SLAVE");
            messagesToKingdoms.Add("Do not waste paper");
            messagesToKingdoms.Add("Go ring all the bells");
            messagesToKingdoms.Add("Crazy Fredrick bought many very exquisite pearl emerald and diamond jewels");
            messagesToKingdoms.Add("the quick brown fox jumps over a lazy dog multiple times");
            messagesToKingdoms.Add("We promptly judged antique ivory buckles for the next prize");
            messagesToKingdoms.Add("Walar Morghulis: All men must die");
        }

        //Static method to access Singleton Instance of this class
        public static MessageDB Instance { get; } = new MessageDB();

        //Public Methods
        public string GetMessageFromDB(int index)
        {
            //We need to check if the passed-in index is not less than 0 and not more than the current size of the Messages List, and then return the message stored at that index
            if (index >= 0 && index <= messagesToKingdoms.Count)
                return messagesToKingdoms[index];
            else
                return error;

        }

        public int GetMessageDBListCount() => messagesToKingdoms.Count;
    }
}
