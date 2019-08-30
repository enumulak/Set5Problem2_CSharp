using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Southeros.Test
{
    [TestClass]
    public class SoutherosTest
    {
        //This Test Method is used mainly for checking Messages that will definitely contain Symbol of the Input Kingdom - so this method is to Test the 'True' condition for the Message Processor Logic
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var found = true; //This is our expected Value - The Test is expected to return a True as the input Message contains the Symbol of the input Kingdom

            var emblemFinder = new Set5Problem2_CSharp.EmblemFinder();

            //We can specify the message and required Kingdom here
            var message = "oaaawaala";
            var kingdom = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            //Act
            var foundForKingdom = emblemFinder.ProcessMessageForKingdom(message, kingdom);

            //Assert
            Assert.AreEqual(found, foundForKingdom);
        }

        //This method is used mainly for checking Messages that Will NOT contain symbol of the given Kingdom - so we check the 'False' condition for the Message Processor Logic
        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            var found = false; //This is our Expected Value - the Test is supposed to return a False as the Message will not contain Symbol of the input Kingdom

            var emblemFinder = new Set5Problem2_CSharp.EmblemFinder();

            var message = "zmzmzmzaztzozh";
            var kingdom = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            //Act
            var foundForKingdom = emblemFinder.ProcessMessageForKingdom(message, kingdom);

            //Assert
            Assert.AreEqual(found, foundForKingdom);
        }

        /****************************************************** BOTH TEST METHODS ABOVE CHECK IF CENTRAL LOGIC WORKS CORRECTLY IN THE MESSAGE PROCESSOR CLASS *****************************************/

        //This Method tests the following logic - A Kingdom becomes an Ally when it recieves a Message that contains its Symbol !!
        //This Test is expected to return a True - Message sent by KingdomOne contains the Emblem of KingdomTwo, and so KingdomTwo becomes an Ally of KingdomOne
        [TestMethod]
        public void KingdomBecomesAlly()
        {
            //Arrange
            var isAlly = true; //This is our Expected Value - The Test is epected to return a True

            var kingdomOne = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var kingdomTwo = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            var message = "oaaawaala";

            //Act
            kingdomOne.SendMessage(message, kingdomTwo);
            var allegiance = kingdomTwo.IsAlly();

            //Assert
            Assert.AreEqual(isAlly, allegiance);
        }


        //This Method tests the following logic - When a Kingdom is competing for Rulership, it does not become an Ally even if it gets a message that contains its Symbol
        //This method is expected to return a False - Although the message sent by KingdomOne contains the Emblem for KingdomTwo, KingdomTwo does not become an ally because it is competing for Rulership
        [TestMethod]
        public void KingdomRejectsAllegiance()
        {
            //Arrange
            var isAlly = false; //This is our Expected value - The test is expected to return False

            var kingdomOne = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var kingdomTwo = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            var message = "oaaawaala";

            //Kingdom2 is competing for rulership, so it will not ackowledge any messages that contain its symbol
            kingdomTwo.SetRulershipCompetingStatus(true);

            //Act
            kingdomOne.SendMessage(message, kingdomTwo);
            var allegiance = kingdomTwo.IsAlly();

            //Assert
            Assert.AreEqual(isAlly, allegiance);
        }

        //This method tests the following Logic - A kingdom displays the Number of Allies it has
        [TestMethod]
        public void KingdomDisplaysAllyCount()
        {
            //Arrange
            var allyCount = 3; //This is our expected value - We are sending messages to three kingdoms and all three will become Allies

            var kSpace = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");

            var kAir = new Set5Problem2_CSharp.Kingdom("Air", "Owl");
            var kLand = new Set5Problem2_CSharp.Kingdom("Land", "Panda");
            var kIce = new Set5Problem2_CSharp.Kingdom("Ice", "Mammoth");

            var m1 = "oaaawaala";
            var m2 = "a1d22n333a4444p";
            var m3 = "zmzmzmzaztzozh";

            //Act
            kSpace.SendMessage(m1, kAir);
            kSpace.SendMessage(m2, kLand);
            kSpace.SendMessage(m3, kIce);

            var alliesOfSpace = kSpace.GetNumberOfAllies();

            //Assert
            Assert.AreEqual(allyCount, alliesOfSpace);

        }

        //This method tests the following logic - A Kingdom stores and returns the Names of its Allies
        [TestMethod]
        public void KingdomDisplaysAllyNames()
        {
            //Arrange
            var ally = "Air"; //This is our expected value - we are simulating a situation where the Air Kingdom becomes an Ally of a given Kingdom

            var space = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var air = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            var message = "oaaawaala";

            //Act
            space.SendMessage(message, air);

            //Since we are testing only for one Ally - we take value from the first index of the Ally List. This can be extended to retrieve multiple values from the Ally List if required
            var allyName = space.GetAllyName(0);

            //Asert
            Assert.AreEqual(ally.ToLower(), allyName.ToLower());
        }

        //This method tests the following logic - A Kingdom stores and displays the Kingdom Name of which it is an Ally
        [TestMethod]
        public void WhoIsAllyOfKingdom()
        {
            //Arrange
            var allyOf = "Space"; //This is our expected value - we are simulating a scenario where a given kingdom has become an Ally of Space

            var space = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var air = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            var message = "oaaawaala";

            //Act
            space.SendMessage(message, air);
            //Air has become an Ally of Space, so this method is expected to return "Space"
            var ally = air.IsAllyOf();

            //Assert
            Assert.AreEqual(allyOf, ally);

        }

        //This method tests the following logic - A Kingdom Stores a message that is sent to it by another Kingdom. This also tests the 'SendMessage' method of the Kingdom Class
        [TestMethod]
        public void RetrieveIncomingMessage()
        {
            //Arrange
            var incomingMessage = "Oaaawaala";

            var space = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var air = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            var message = "oaaawaala";

            //Act
            space.SendMessage(message, air);
            var msg = air.GetIncomingMessage();

            //Assert
            Assert.AreEqual(incomingMessage.ToLower(), msg.ToLower());

        }

        //This method tests the 'ProcessAllegiance' method of the Kingdom Class
        //ProcessAllegiance() is used by Message Processor and basically, when the method is called for a 'Receiver', and a 'True' with 'Sender' is passed in - the Receiver becomes an Ally of 'Sender'
        [TestMethod]
        public void ProcessAllegiance()
        {
            //Arrange
            var isAllyOf = "Space"; //This is our expected value - we simulate a situation where a given Kingdom becomes an Ally of Space Kingdom

            var space = new Set5Problem2_CSharp.Kingdom("Space", "Gorilla");
            var air = new Set5Problem2_CSharp.Kingdom("Air", "Owl");

            //Act
            air.ProcessAllegiance(true, space);
            var airIsAllyOf = air.IsAllyOf();

            //Assert
            Assert.AreEqual(isAllyOf, airIsAllyOf);
        }
    }
}
