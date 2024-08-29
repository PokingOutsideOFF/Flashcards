using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Services
{
    public class UserInput
    {
        public string GetText()
        {
            string text = Console.ReadLine();
            return text;
        }

        public int GetInt()
        {
            int id;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    return id;
                }
                Console.Write("Enter integer: ");
            }
        }
    }
}
