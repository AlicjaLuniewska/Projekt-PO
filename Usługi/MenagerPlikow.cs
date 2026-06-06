using ProjektPO.Modele;

namespace ProjektPO.Uslugi;

/// <summary>
/// Klasa odpowiedzialna za proste operacje zapisu danych do plików.
/// </summary>
public class MenagerPlikow
{
    /// <summary>
    /// Zapisuje podstawowe informacje o wyjeździe do pliku tekstowego.
    /// </summary>
    /// <param name="wyjazd">Wyjazd do zapisania.</param>
    /// <param name="sciezkaPliku">Ścieżka do pliku.</param>
    public void ZapiszWyjazd(Wyjazd wyjazd, string sciezkaPliku)
    {
        using StreamWriter writer = new StreamWriter(sciezkaPliku);

        writer.WriteLine(wyjazd.CelPodrozy);
        writer.WriteLine(wyjazd.DataRozpoczecia);
        writer.WriteLine(wyjazd.DataZakonczenia);
        writer.WriteLine(wyjazd.ObliczKoszt());
    }
}