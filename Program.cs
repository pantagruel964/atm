using ATM.Enum;
using ATM.Service;

namespace ATM
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Input money amount:");

            int sum = Convert.ToInt32(Console.ReadLine());

            if (sum < 1)
            {
                Console.WriteLine("Incorrect input. Money amount must be a positive number.");
                Environment.Exit((int) ExitCode.IncorrectInput);
            }

            var atmService = new AtmService();

            // if (!atmService.HasEnoughMoney(sum))
            // {
            //     Console.WriteLine("ATM has not enough money.");
            //     Environment.Exit((int) ExitCode.NotEnoughMoney);
            // }

            atmService.Trrrrrrrrr(sum);
            // atmService.GetChange([5, 10, 50], 80);

            // var atm2 = new AtmService2();
            // atm2.Run();
        }
    }
}