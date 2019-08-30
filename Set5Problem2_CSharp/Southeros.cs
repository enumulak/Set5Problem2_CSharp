using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Set5Problem2_CSharp
{
    public sealed class Southeros
    {
        //Private Variables        
        private List<Kingdom> allKingdoms;
        private Kingdom currentRuler;
        private List<Kingdom> conquerors;
        private bool involveTheHighPriest;
        private int numberOfConquerors;
        private int maxConquerors;
        private int minConquerors;
                        
        //Private Constructor, so outsiders do not have access. We initialize Current Ruler as None
        private Southeros()
        {
            //Initialize a List of All Kingdoms
            allKingdoms = new List<Kingdom>
            {
                new Kingdom("Space", "Gorilla"),
                new Kingdom("Land", "Panda"),
                new Kingdom("Water", "Octopus"),
                new Kingdom("Ice", "Mammoth"),
                new Kingdom("Air", "Owl"),
                new Kingdom("Fire", "Dragon")
            };

            conquerors = new List<Kingdom>();
            involveTheHighPriest = false;
        }

        //Static method to provide access to the Singleton Instance of this Class
        public static Southeros Instance { get; } = new Southeros();

        #region Public Methods

        public void BeginBallotForRuler()
        {
            //We first display the current state of Southeros
            Console.WriteLine("Who is the Ruler of Southeros?");                        
            Console.WriteLine("None");
            Console.WriteLine("Current Allies of Ruler: None");

            Console.WriteLine('\n');

            //We first check how many Kingdoms are competing for Rulership - this sets a value for numberOfConquerors
            GetNumberOfConquerors();

            //Send Kingdom and Conqueror information to the High Priest
            if(involveTheHighPriest)
            { HighPriest.Instance.RegisterConquerorsAndKingdoms(conquerors, allKingdoms); }


            /******  ALL LOGIC IS NOW HANDLED BY THE HIGH PRIEST CLASS *****************************/

            //Now get the Output
            GetOutput();            
        }

        public string GetCurrentRulerName() => currentRuler.GetKingdomName();
        
        public void SetCurrentRuler(Kingdom ruler)
        {
            if (ruler != null)
                currentRuler = ruler;
        }

        #endregion

        #region Private Methods

        //Method that asks user as to how many kingdoms are compting for rulership (certain validations also need to be done here..)
        private void GetNumberOfConquerors()
        {
            minConquerors = 2;
            maxConquerors = 4;

            Console.WriteLine("How many Kingdoms are competing for rulership of Southeros? (minimum {0}, maximum {1})", minConquerors, maxConquerors);
            var input = Console.ReadLine();

            if (int.TryParse(input, out numberOfConquerors))
                Console.WriteLine("Input is valid. Proceeding.. ");
            else
                Console.WriteLine("Invalid Input. Exiting Program..");

            Console.WriteLine('\n');

            //We can proceed only if Number of COnquerors are equal to or more than the Minimum Conquerors
            if(numberOfConquerors < minConquerors)
            {
                Console.WriteLine("You need a minimum of {0} conquerors to proceed. The Ballot for Rulership is suspended...", minConquerors);
            }
            else
            {
                //Setting Number of Conquerors to Max Conquerors, in case user enters a bigger value
                if (numberOfConquerors > maxConquerors)
                {
                    Console.WriteLine("Resetting the Maximum Number of Conquerors.. You can have only {0} ", maxConquerors);
                    Console.WriteLine('\n');
                    numberOfConquerors = maxConquerors;
                }

                //Start obtaining Conqueror information...
                GetNamesOfConquerors();
            }
        }

        //Method that gets the Conquerors' names from user and stores them in a List
        private void GetNamesOfConquerors()
        {
            for (var i = 0; i < numberOfConquerors; i++)
            {
                Console.WriteLine("Enter name of Conqueror {0}: ", i + 1);
                var name = Console.ReadLine();

                //Now we need to loop through All Kingdoms List to check for the entered name and store that Kingdom in the Conquerors List..
                //A Kingdom will be added to the Conqueror List Only if User-Entered Value matches with an Existing Kingdom Name 
                //So, even if User says that there are 3 conquerors, then it is possible that the Conqueror List may contain only 1 Valid Kingdom, if User enters only 1 valid Name..)
                for (var k = 0; k < allKingdoms.Count; k++)
                {
                    if (allKingdoms[k].GetKingdomName().ToLower() == name.ToLower())
                    {
                        conquerors.Add(allKingdoms[k]);
                    }
                }
            }
            
            //Now we check if we have the same number of Conquerors as specified by the User. If not, then User might have entered invalid Kingdom names, and the ballot is suspended
            if(conquerors.Count < numberOfConquerors)
            {
                Console.WriteLine("Hmm.. Looks like you entered Names that did not match any existing Kingdoms. Ballot for rulership is suspended..");
            }
            else
            {
                //The Conquerors now set their Competing Status to True
                for(var i = 0; i < conquerors.Count; i++)
                {
                    conquerors[i].SetRulershipCompetingStatus(true);
                }

                //Now that there are valid number of Conquerors for rulership of Southeros, the High Priest is involved
                involveTheHighPriest = true;
            }
        }

        private void GetOutput()
        {
            Console.WriteLine('\n');

            Console.WriteLine("Who is the Ruler of Southeros?");

            if (currentRuler == null)
            {
                Console.WriteLine("None");
                Console.WriteLine("Allies of Ruler: None");
            }
            else
            {
                Console.WriteLine("Ruler of Southeros: {0}", currentRuler.GetKingdomName());
                Console.WriteLine('\n');

                for (var i = 0; i < currentRuler.GetNumberOfAllies(); i++)
                {
                    Console.WriteLine("Ally {0} of Ruler: {1}", i + 1, currentRuler.GetAllyName(i));
                }
            }

            Console.WriteLine('\n');
        }

        #endregion        
        
    }
}
