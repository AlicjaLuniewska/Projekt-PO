namespace ProjektPO.Interfejsy;

/// <summary>
/// Interfejs opisujący wspólne zachowania i dane każdej aktywności w harmonogramie wyjazdu.
/// </summary>
public interface Aktywnosc
{
    /// <summary>
    /// Nazwa aktywności.
    /// </summary>
    string Nazwa { get; set; }

    /// <summary>
    /// Data i godzina rozpoczęcia aktywności.
    /// </summary>
    DateTime CzasRozpoczecia { get; set; }

    /// <summary>
    /// Data i godzina zakończenia aktywności.
    /// </summary>
    DateTime CzasZakonczenia { get; set; }

    /// <summary>
    /// Oblicza koszt aktywności.
    /// </summary>
    /// <returns>Koszt aktywności.</returns>
    double ObliczKoszt();

    /// <summary>
    /// Zwraca opis aktywności.
    /// </summary>
    /// <returns>Opis aktywności jako tekst.</returns>
    string PobierzOpis();

    /// <summary>
    /// Sprawdza, czy aktywność koliduje czasowo z inną aktywnością.
    /// </summary>
    /// <param name="inna">Inna aktywność do porównania.</param>
    /// <returns>True, jeżeli aktywności nachodzą na siebie czasowo.</returns>
    bool CzyKolidujeZ(Aktywnosc inna);
}