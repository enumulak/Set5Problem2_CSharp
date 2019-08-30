using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set5Problem2_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            //All proceedings begin in the Southeros Universe
            Southeros.Instance.BeginBallotForRuler();
            
            Console.ReadKey();
        }
    }
}
