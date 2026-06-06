using ProjektPO.Interfejsy;

namespace ProjektPO.Modele;

/// <summary>
/// Klasa reprezentująca nocleg w harmonogramie wyjazdu.
/// </summary>
public class Nocleg : Aktywnosc
{
    /// <summary>
    /// Nazwa aktywności.
    /// </summary>
    public string Nazwa { get; set; }

    /// <summary>
    /// Data i godzina rozpoczęcia noclegu.
    /// </summary>
    public DateTime CzasRozpoczecia { get; set; }

    /// <summary>
    /// Data i godzina zakończenia noclegu.
    /// </summary>
    public DateTime CzasZakonczenia { get; set; }

    /// <summary>
    /// Adres noclegu.
    /// </summary>
    public string Adres { get; set; }

    /// <summary>
    /// Cena za jedną noc.
    /// </summary>
    public double CenaZaNoc { get; set; }

    /// <summary>
    /// Liczba nocy.
    /// </summary>
    public int LiczbaNocy { get; set; }

    /// <summary>
    /// Tworzy nowy nocleg.
    /// </summary>
    public Nocleg(
        string nazwa,
        DateTime start,
        DateTime koniec,
        string adres,
        double cenaZaNoc,
        int liczbaNocy)
    {
        Nazwa = nazwa;
        CzasRozpoczecia = start;
        CzasZakonczenia = koniec;
        Adres = adres;
        CenaZaNoc = cenaZaNoc;
        LiczbaNocy = liczbaNocy;
    }

    /// <summary>
    /// Oblicza koszt noclegu.
    /// </summary>
    /// <returns>Cena za noc pomnożona przez liczbę nocy.</returns>
    public double ObliczKoszt()
    {
        return CenaZaNoc * LiczbaNocy;
    }

    /// <summary>
    /// Zwraca opis noclegu.
    /// </summary>
    /// <returns>Opis noclegu jako tekst.</returns>
    public string PobierzOpis()
    {
        return $"Nocleg: {Nazwa}, adres: {Adres}, koszt: {ObliczKoszt()} zl";
    }

    /// <summary>
    /// Sprawdza, czy nocleg koliduje czasowo z inną aktywnością.
    /// </summary>
    /// <param name="inna">Inna aktywność do porównania.</param>
    /// <returns>True, jeżeli aktywności nachodzą na siebie czasowo.</returns>
    public bool CzyKolidujeZ(Aktywnosc inna)
    {
        return CzasRozpoczecia < inna.CzasZakonczenia &&
               CzasZakonczenia > inna.CzasRozpoczecia;
    }
}