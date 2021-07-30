using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class ValueTask
    {
        static readonly Random s_rnd = new Random();

        //static async Task Main() =>
        //    Console.WriteLine($"You rolled {await GetDiceRollAsync()}");

        static async ValueTask<int> GetDiceRollAsync()
        {
            Console.WriteLine("Shaking dice...");

            int roll1 = await RollAsync();
            int roll2 = await RollAsync();

            return roll1 + roll2;
        }

        static async ValueTask<int> RollAsync()
        {
            await Task.Delay(500);

            int diceRoll = s_rnd.Next(1, 7);
            return diceRoll;
        }
    }
}
