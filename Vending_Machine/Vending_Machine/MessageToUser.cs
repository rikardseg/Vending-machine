using System;

namespace Vending_Machine
{
    public class MessageToUser
    {
        public void Write(string message)
        {
            Console.WriteLine("\n"+message);
            Console.Write("Press any key to continue!");
            Console.ReadKey();            
        }
    }
}