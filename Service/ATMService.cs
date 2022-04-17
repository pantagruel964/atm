using ATM.Enum;
using ATM.Model;
using ATM.Repository;

namespace ATM.Service;

public class AtmService
{
    private readonly IBanknoteRepository _banknoteRepository;
    private readonly BanknoteService _banknoteService;

    public AtmService()
    {
        _banknoteRepository = new BanknoteRepository();
        _banknoteService = new BanknoteService();
    }

    public bool HasEnoughMoney(int sum)
    {
        var banknotes = _banknoteRepository.GetBanknotes();

        if (!HasBanknotes(banknotes))
        {
            Console.WriteLine("ATM has not enough money.");
            Environment.Exit((int) ExitCode.FailedToResolveBanknotesData);
        }

        return Convert.ToBoolean(sum <= GetTotalMoneyInAtm(banknotes));
    }

    public void Trrrrrrrrr(int sum)
    {
        var banknotes = _banknoteRepository.GetBanknotes();

        if (!HasBanknotes(banknotes))
        {
            Console.WriteLine("ATM has not enough money.");
            Environment.Exit((int) ExitCode.FailedToResolveBanknotesData);
        }

        var sortedBanknotes = _banknoteService.GetSortedDescByNominal(banknotes);

        List<Banknote> collected = new List<Banknote>();

        GetMoney(sum, sortedBanknotes, collected);
    }

    void ShowResult(List<Banknote> banknotes)
    {

        Console.WriteLine("Operation successful.");

        foreach (var banknote in banknotes)
        {
            if (banknote.Quantity > 0)
            {
                Console.WriteLine($"Nominal: {banknote.NominalValue} Quantity: {banknote.Quantity}");
            }
        }

        Environment.Exit((int) ExitCode.Successful);
    }

    void GetMoney(int requiredSum, List<Banknote> banknotes, List<Banknote> collected)
    {
        foreach (Banknote banknote in banknotes)
        {
            if (banknote.NominalValue <= requiredSum)
            {
                if (_canFillWholeAmount(requiredSum, banknote)) {
                    var a = new Banknote();
                    a.NominalValue = banknote.NominalValue;
                    a.Quantity = requiredSum / banknote.NominalValue;
                    collected.Add(a);

                    requiredSum -= a.NominalValue * a.Quantity;

                    continue;
                }

                List<Banknote> copyBanknotes = new List<Banknote>(banknotes);

                var quantity = _calculateQuantity(requiredSum, copyBanknotes, banknote);

                var b = new Banknote();
                b.NominalValue = banknote.NominalValue;
                b.Quantity = quantity;
                collected.Add(b);

                requiredSum -= b.NominalValue * b.Quantity;
            }
        }
        
        if (requiredSum != 0)
        {
            Console.WriteLine("There are no required banknotes.");
            Environment.Exit((int)ExitCode.HasNotNecessaryBanknotes);
        }

        ShowResult(collected);
    }
    
    private bool _canFillWholeAmount(int requiredSum, Banknote banknote)
    {
        var totalByNominal = _banknoteService.GetTotalByNominal(banknote);

        return totalByNominal >= requiredSum && requiredSum % banknote.NominalValue == 0;
    }
    
    private bool _canDispenseByAnother(int amount, List<Banknote> banknotes)
    {
        foreach (var banknote in banknotes) {
            if (amount % banknote.NominalValue == 0) {
                return true;
            }
        }

        return false;
    }
    
    private int _calculateQuantity(int requiredSum, List<Banknote> banknotes, Banknote banknote, int decrement = 0, int decrementCount = 1)
    {
        var qty = (int)((requiredSum - decrement * decrementCount) / banknote.NominalValue);

        if(requiredSum < 0 || qty < 0)
        {
            Console.WriteLine("There are no required banknotes.");
            Environment.Exit((int)ExitCode.HasNotNecessaryBanknotes);
        }
        
        if (qty > banknote.Quantity) {
            qty = _calculateQuantity(requiredSum - (requiredSum - banknote.Quantity * banknote.NominalValue), banknotes, banknote);
        }

        if (banknotes.Count == 0) {
            Console.WriteLine("There are no required banknotes.");
            Environment.Exit((int) ExitCode.HasNotNecessaryBanknotes);
        }

        banknotes = banknotes.Where(b => b.NominalValue < banknote.NominalValue).ToList();

        if (_canDispenseByAnother(requiredSum - qty * banknote.NominalValue, banknotes)) {
            return qty;
        }

        ++decrementCount;

        return _calculateQuantity(requiredSum, banknotes, banknote, banknote.NominalValue, decrementCount);
    }

    private bool HasBanknotes(List<Banknote>? banknotes)
    {
        return null != banknotes && GetTotalMoneyInAtm(banknotes) > 0;
    }
    
    private int GetTotalMoneyInAtm(List<Banknote> banknotes)
    {
        return banknotes.Sum(banknote => banknote.NominalValue * banknote.Quantity);
    }
}