using ProjektPO.Interfejsy;

namespace ProjektPO.Modele;

/// <summary>
/// Klasa reprezentująca przejazd w ramach wyjazdu.
/// </summary>
public class Przejazd : Aktywnosc
{
    /// <summary>
    /// Nazwa aktywności.
    /// </summary>
    public string Nazwa { get; set; }

    /// <summary>
    /// Data i godzina rozpoczęcia przejazdu.
    /// </summary>
    public DateTime CzasRozpoczecia { get; set; }

    /// <summary>
    /// Data i godzina zakończenia przejazdu.
    /// </summary>
    public DateTime CzasZakonczenia { get; set; }

    /// <summary>
    /// Miejsce rozpoczęcia przejazdu.
    /// </summary>
    public string Skad { get; set; }

    /// <summary>
    /// Miejsce docelowe przejazdu.
    /// </summary>
    public string Dokad { get; set; }

    /// <summary>
    /// Cena biletu.
    /// </summary>
    public double CenaBiletu { get; set; }

    /// <summary>
    /// Dodatkowy koszt przejazdu, np. bagaż lub rezerwacja miejsca.
    /// </summary>
    public double KosztDodatkowy { get; set; }

    /// <summary>
    /// Tworzy nowy przejazd.
    /// </summary>
    public Przejazd(
        string nazwa,
        DateTime start,
        DateTime koniec,
        string skad,
        string dokad,
        double cenaBiletu,
        double kosztDodatkowy)
    {
        Nazwa = nazwa;
        CzasRozpoczecia = start;
        CzasZakonczenia = koniec;
        Skad = skad;
        Dokad = dokad;
        CenaBiletu = cenaBiletu;
        KosztDodatkowy = kosztDodatkowy;
    }

    /// <summary>
    /// Oblicza całkowity koszt przejazdu.
    /// </summary>
    /// <returns>Suma ceny biletu i kosztu dodatkowego.</returns>
    public double ObliczKoszt()
    {
        return CenaBiletu + KosztDodatkowy;
    }

    /// <summary>
    /// Zwraca opis przejazdu.
    /// </summary>
    /// <returns>Opis przejazdu jako tekst.</returns>
    public string PobierzOpis()
    {
        return $"Przejazd: {Nazwa}, {Skad} -> {Dokad}, {CzasRozpoczecia:g} - {CzasZakonczenia:g}, koszt: {ObliczKoszt()} zl";
    }

    /// <summary>
    /// Sprawdza, czy przejazd koliduje czasowo z inną aktywnością.
    /// </summary>
    /// <param name="inna">Inna aktywność do porównania.</param>
    /// <returns>True, jeżeli aktywności nachodzą na siebie czasowo.</returns>
    public bool CzyKolidujeZ(Aktywnosc inna)
    {
        return CzasRozpoczecia < inna.CzasZakonczenia &&
               CzasZakonczenia > inna.CzasRozpoczecia;
    }
}