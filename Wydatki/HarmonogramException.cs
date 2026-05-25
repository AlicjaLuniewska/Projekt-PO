namespace PlanerWyjazdu.Wyjatki;

public class HarmonogramException : Exception
{
    public HarmonogramException(string wiadomosc) : base(wiadomosc)
    {
    }
}