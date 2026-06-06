using ProjektPO.Modele;

namespace ProjektPO.Interfejsy;

/// <summary>
/// Interfejs odpowiedzialny za generowanie dokumentów dla wyjazdu.
/// </summary>
public interface IGeneratorDokumentow
{
    /// <summary>
    /// Generuje plik z harmonogramem wyjazdu.
    /// </summary>
    /// <param name="wyjazd">Wyjazd, dla którego generowany jest dokument.</param>
    /// <param name="sciezkaPliku">Ścieżka do pliku wynikowego.</param>
    void GenerujHarmonogramTXT(Wyjazd wyjazd, string sciezkaPliku);

    /// <summary>
    /// Generuje plik z podsumowaniem kosztów wyjazdu.
    /// </summary>
    /// <param name="wyjazd">Wyjazd, dla którego generowany jest dokument.</param>
    /// <param name="sciezkaPliku">Ścieżka do pliku wynikowego.</param>
    void GenerujKosztyTXT(Wyjazd wyjazd, string sciezkaPliku);
}