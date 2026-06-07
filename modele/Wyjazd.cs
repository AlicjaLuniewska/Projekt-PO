using ProjektPO.Interfejsy;
using ProjektPO.Wyjatki;

namespace ProjektPO.Modele;

/// <summary>
/// Klasa reprezentująca cały wyjazd wraz z harmonogramem aktywności.
/// </summary>
public class Wyjazd
{
    /// <summary>
    /// Cel podróży.
    /// </summary>
    public string CelPodrozy { get; set; }

    /// <summary>
    /// Data rozpoczęcia wyjazdu.
    /// </summary>
    public DateTime DataRozpoczecia { get; set; }

    /// <summary>
    /// Data zakończenia wyjazdu.
    /// </summary>
    public DateTime DataZakonczenia { get; set; }

    /// <summary>
    /// Lista aktywności zaplanowanych w ramach wyjazdu.
    /// </summary>
    private List<Aktywnosc> aktywnosci = new();

    /// <summary>
    /// Tworzy nowy wyjazd.
    /// </summary>
    public Wyjazd(string celPodrozy, DateTime dataRozpoczecia, DateTime dataZakonczenia)
    {
        CelPodrozy = celPodrozy;
        DataRozpoczecia = dataRozpoczecia;
        DataZakonczenia = dataZakonczenia;
    }

    /// <summary>
    /// Dodaje nową aktywność do harmonogramu.
    /// </summary>
    /// <param name="nowaAktywnosc">Aktywność do dodania.</param>
    /// <exception cref="HarmonogramException">
    /// Wyjątek zgłaszany, gdy nowa aktywność koliduje z istniejącą aktywnością lub wykracza poza ramy wyjazdu.
    /// </exception>
    public void DodajAktywnosc(Aktywnosc nowaAktywnosc)
    {
        // Walidacja: sprawdzenie ram czasowych całego wyjazdu
        if (nowaAktywnosc.CzasRozpoczecia < DataRozpoczecia || nowaAktywnosc.CzasZakonczenia > DataZakonczenia)
        {
            throw new HarmonogramException($"Nie można dodać '{nowaAktywnosc.Nazwa}'. Aktywność musi zawierać się w terminie wyjazdu ({DataRozpoczecia:d} - {DataZakonczenia:d}).");
        }

        // Walidacja kolizji czasowych
        foreach (var aktywnosc in aktywnosci)
        {
            if (nowaAktywnosc.CzyKolidujeZ(aktywnosc))
            {
                throw new HarmonogramException($"Ta aktywność ({nowaAktywnosc.Nazwa}) koliduje czasowo z: {aktywnosc.Nazwa}.");
            }
        }

        aktywnosci.Add(nowaAktywnosc);
    }

    /// <summary>
    /// Oblicza całkowity koszt wyjazdu.
    /// </summary>
    /// <returns>Suma kosztów wszystkich aktywności.</returns>
    public double ObliczKoszt()
    {
        double suma = 0;

        foreach (var aktywnosc in aktywnosci)
        {
            suma += aktywnosc.ObliczKoszt();
        }

        return suma;
    }

    /// <summary>
    /// Zwraca harmonogram aktywności posortowany według czasu rozpoczęcia.
    /// </summary>
    /// <returns>Lista aktywności w kolejności chronologicznej.</returns>
    public List<Aktywnosc> PobierzHarmonogram()
    {
        return aktywnosci
            .OrderBy(a => a.CzasRozpoczecia)
            .ToList();
    }

    /// <summary>
    /// Wyszukuje aktywności po nazwie (ignoruje wielkość liter).
    /// </summary>
    public List<Aktywnosc> WyszukajAktywnosci(string fraza)
    {
        return aktywnosci
            .Where(a => a.Nazwa.Contains(fraza, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Usuwa aktywność z harmonogramu na podstawie dokładnej nazwy.
    /// </summary>
    public bool UsunAktywnosc(string nazwa)
    {
        var doUsuniecia = aktywnosci.FirstOrDefault(a => a.Nazwa.Equals(nazwa, StringComparison.OrdinalIgnoreCase));
        if (doUsuniecia != null)
        {
            aktywnosci.Remove(doUsuniecia);
            return true;
        }
        return false;
    }
}