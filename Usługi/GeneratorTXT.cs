using ProjektPO.Interfejsy;
using ProjektPO.Modele;

namespace ProjektPO.Uslugi;

/// <summary>
/// Klasa generująca dokumenty tekstowe TXT dla wyjazdu.
/// </summary>
public class GeneratorTXT : IGeneratorDokumentow
{
    /// <summary>
    /// Generuje harmonogram wyjazdu do pliku TXT.
    /// </summary>
    public void GenerujHarmonogramTXT(Wyjazd wyjazd, string sciezkaPliku)
    {
        using StreamWriter writer = new StreamWriter(sciezkaPliku);

        writer.WriteLine("HARMONOGRAM WYJAZDU");
        writer.WriteLine("-------------------");
        writer.WriteLine($"Cel podrozy: {wyjazd.CelPodrozy}");
        writer.WriteLine($"Termin: {wyjazd.DataRozpoczecia:d} - {wyjazd.DataZakonczenia:d}");
        writer.WriteLine();

        foreach (var aktywnosc in wyjazd.PobierzHarmonogram())
        {
            writer.WriteLine(aktywnosc.PobierzOpis());
        }
    }

    /// <summary>
    /// Generuje podsumowanie kosztów wyjazdu do pliku TXT.
    /// </summary>
    public void GenerujKosztyTXT(Wyjazd wyjazd, string sciezkaPliku)
    {
        using StreamWriter writer = new StreamWriter(sciezkaPliku);

        writer.WriteLine("PODSUMOWANIE KOSZTOW");
        writer.WriteLine("--------------------");
        writer.WriteLine($"Cel podrozy: {wyjazd.CelPodrozy}");
        writer.WriteLine($"Calkowity koszt: {wyjazd.ObliczKoszt()} zl");
    }
}