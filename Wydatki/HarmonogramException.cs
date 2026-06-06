namespace ProjektPO.Wyjatki;

/// <summary>
/// Wyjątek zgłaszany w przypadku błędów związanych z harmonogramem wyjazdu.
/// </summary>
public class HarmonogramException : Exception
{
    /// <summary>
    /// Tworzy nowy wyjątek harmonogramu.
    /// </summary>
    /// <param name="wiadomosc">Komunikat błędu.</param>
    public HarmonogramException(string wiadomosc) : base(wiadomosc)
    {
    }
}