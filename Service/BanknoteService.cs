using ATM.Model;

namespace ATM.Service;

public class BanknoteService
{
    public List<Banknote> GetSortedDescByNominal(List<Banknote> banknotes)
    {
        return banknotes.OrderByDescending(banknote => banknote.NominalValue).ToList();
    }

    public int GetTotalByNominal(Banknote banknote)
    {
        return banknote.Quantity * banknote.NominalValue;
    }
}
