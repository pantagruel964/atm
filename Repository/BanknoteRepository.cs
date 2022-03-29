using System.Text.Json;
using ATM.Model;

namespace ATM.Repository;

public class BanknoteRepository : IBanknoteRepository
{
    private const string BanknotesResource = "Stub/data.json";

    public List<Banknote>? GetBanknotes()
    {
        string data;
        Console.WriteLine(Directory.GetCurrentDirectory());
        string path = Path.Combine(Directory.GetCurrentDirectory(), BanknotesResource);

        try
        {
            data = File.ReadAllText(path);
        }
        catch (Exception e)
        {
            throw new FileLoadException(e.Message);
        }

        return JsonSerializer.Deserialize<List<Banknote>>(data);
    }
}
