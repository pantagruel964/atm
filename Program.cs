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

            if (!atmService.HasEnoughMoney(sum))
            {
                Console.WriteLine("ATM has not enough money.");
                Environment.Exit((int) ExitCode.NotEnoughMoney);
            }

            var banknotes = atmService.Trrrrrrrrr(sum);

            Console.WriteLine("Operation successful.");
            
            foreach (var banknote in banknotes)
            {
                Console.WriteLine($"Nominal: {banknote.NominalValue} Quantity: {banknote.Quantity}");
            }

            Environment.Exit((int) ExitCode.Successful);
        }
    }
}