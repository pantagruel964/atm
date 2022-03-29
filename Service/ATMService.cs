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

        return Convert.ToBoolean(sum <= GetTotal(banknotes));
    }

    public List<Banknote> Trrrrrrrrr(int sum)
    {
        var banknotes = _banknoteRepository.GetBanknotes();

        if (!HasBanknotes(banknotes))
        {
            Console.WriteLine("ATM has not enough money.");
            Environment.Exit((int) ExitCode.FailedToResolveBanknotesData);
        }

        var sortedBanknotes = _banknoteService.GetSortedByDescBanknotes(banknotes);

        List<Banknote> collected = new List<Banknote>();
        int requiredSum = sum;
        int collectedSum = 0;

        foreach (var banknote in sortedBanknotes)
        {
            if (banknote.Quantity < 1)
            {
                continue;
            }

            var requiredQuantity = requiredSum / banknote.NominalValue;
            var cb = new Banknote();
            cb.NominalValue = banknote.NominalValue;
            cb.Quantity = requiredQuantity <= banknote.Quantity ? requiredQuantity : banknote.Quantity;

            collected.Add(cb);
            collectedSum = collected.Sum(banknote1 => banknote1.NominalValue * banknote1.Quantity);

            if (IsCollectRequiredAmount(sum, collectedSum))
            {
                break;
            }

            requiredSum = sum - collectedSum;
        }

        if (!IsCollectRequiredAmount(sum, collectedSum))
        {
            Console.WriteLine("ATM has not necessary banknotes.");
            Environment.Exit((int) ExitCode.HasNotNecessaryBanknotes);
        }

        return collected;
    }

    private int GetTotal(List<Banknote> banknotes)
    {
        return banknotes.Sum(banknote => banknote.NominalValue * banknote.Quantity);
    }

    private bool HasBanknotes(List<Banknote>? banknotes)
    {
        return null != banknotes && GetTotal(banknotes) > 0;
    }

    private bool IsCollectRequiredAmount(int required, int collected)
    {
        return required == collected;
    }
}