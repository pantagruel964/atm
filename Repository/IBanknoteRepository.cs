using ATM.Model;

namespace ATM.Repository;

public interface IBanknoteRepository
{
    public List<Banknote>? GetBanknotes();
}
