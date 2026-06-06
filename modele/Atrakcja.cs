using ProjektPO.Interfejsy;

namespace ProjektPO.Modele;

/// <summary>
/// Klasa reprezentująca atrakcję turystyczną.
/// </summary>
public class Atrakcja : Aktywnosc
{
    /// <summary>
    /// Nazwa aktywności.
    /// </summary>
    public string Nazwa { get; set; }

    /// <summary>
    /// Data i godzina rozpoczęcia atrakcji.
    /// </summary>
    public DateTime CzasRozpoczecia { get; set; }

    /// <summary>
    /// Data i godzina zakończenia atrakcji.
    /// </summary>
    public DateTime CzasZakonczenia { get; set; }

    /// <summary>
    /// Miejsce odbywania się atrakcji.
    /// </summary>
    public string Miejsce { get; set; }

    /// <summary>
    /// Cena biletu dla jednej osoby.
    /// </summary>
    public double CenaBiletu { get; set; }

    /// <summary>
    /// Liczba osób biorących udział w atrakcji.
    /// </summary>
    public int LiczbaOsob { get; set; }

    /// <summary>
    /// Tworzy nową atrakcję.
    /// </summary>
    public Atrakcja(
        string nazwa,
        DateTime start,
        DateTime koniec,
        string miejsce,
        double cenaBiletu,
        int liczbaOsob)
    {
        Nazwa = nazwa;
        CzasRozpoczecia = start;
        CzasZakonczenia = koniec;
        Miejsce = miejsce;
        CenaBiletu = cenaBiletu;
        LiczbaOsob = liczbaOsob;
    }

    /// <summary>
    /// Oblicza koszt atrakcji.
    /// </summary>
    /// <returns>Cena biletu pomnożona przez liczbę osób.</returns>
    public double ObliczKoszt()
    {
        return CenaBiletu * LiczbaOsob;
    }

    /// <summary>
    /// Zwraca opis atrakcji.
    /// </summary>
    /// <returns>Opis atrakcji jako tekst.</returns>
    public string PobierzOpis()
    {
        return $"Atrakcja: {Nazwa}, miejsce: {Miejsce}, {CzasRozpoczecia:g} - {CzasZakonczenia:g}, koszt: {ObliczKoszt()} zl";
    }

    /// <summary>
    /// Sprawdza, czy atrakcja koliduje czasowo z inną aktywnością.
    /// </summary>
    /// <param name="inna">Inna aktywność do porównania.</param>
    /// <returns>True, jeżeli aktywności nachodzą na siebie czasowo.</returns>
    public bool CzyKolidujeZ(Aktywnosc inna)
    {
        return CzasRozpoczecia < inna.CzasZakonczenia &&
               CzasZakonczenia > inna.CzasRozpoczecia;
    }
}