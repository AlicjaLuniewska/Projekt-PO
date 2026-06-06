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
    /// Wyjątek zgłaszany, gdy nowa aktywność koliduje z istniejącą aktywnością.
    /// </exception>
    public void DodajAktywnosc(Aktywnosc nowaAktywnosc)
    {
        foreach (var aktywnosc in aktywnosci)
        {
            if (nowaAktywnosc.CzyKolidujeZ(aktywnosc))
            {
                throw new HarmonogramException("Ta aktywnosc koliduje z inna aktywnoscia.");
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
}