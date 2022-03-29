using ATM.Model;

namespace ATM.Service;

public class BanknoteService
{
    public List<Banknote> GetSortedByDescBanknotes(List<Banknote> banknotes)
    {
        return banknotes.OrderByDescending(banknote => banknote.NominalValue).ToList();
    }
}
