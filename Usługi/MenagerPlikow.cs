using ProjektPO.Interfejsy;
using ProjektPO.Modele;

namespace ProjektPO.Uslugi;

public class MenagerPlikow
{
    public void ZapiszWyjazd(Wyjazd wyjazd, string sciezkaPliku)
    {
        using StreamWriter writer = new StreamWriter(sciezkaPliku);

        writer.WriteLine(wyjazd.CelPodrozy);
        writer.WriteLine(wyjazd.DataRozpoczecia.ToString("o")); 
        writer.WriteLine(wyjazd.DataZakonczenia.ToString("o"));

        foreach (var akt in wyjazd.PobierzHarmonogram())
        {
            if (akt is Przejazd p)
            {
                writer.WriteLine($"PRZEJAZD;{p.Nazwa};{p.CzasRozpoczecia:o};{p.CzasZakonczenia:o};{p.Skad};{p.Dokad};{p.CenaBiletu};{p.KosztDodatkowy}");
            }
            else if (akt is Nocleg n)
            {
                writer.WriteLine($"NOCLEG;{n.Nazwa};{n.CzasRozpoczecia:o};{n.CzasZakonczenia:o};{n.Adres};{n.CenaZaNoc};{n.LiczbaNocy}");
            }
            else if (akt is Atrakcja a)
            {
                writer.WriteLine($"ATRAKCJA;{a.Nazwa};{a.CzasRozpoczecia:o};{a.CzasZakonczenia:o};{a.Miejsce};{a.CenaBiletu};{a.LiczbaOsob}");
            }
        }
    }

    public Wyjazd WczytajWyjazd(string sciezkaPliku)
    {
        if (!File.Exists(sciezkaPliku))
        {
            throw new FileNotFoundException("Nie znaleziono pliku z zapisanym wyjazdem.");
        }

        using StreamReader reader = new StreamReader(sciezkaPliku);

        string cel = reader.ReadLine() ?? "Nieznany cel";
        DateTime start = DateTime.Parse(reader.ReadLine() ?? DateTime.Now.ToString());
        DateTime koniec = DateTime.Parse(reader.ReadLine() ?? DateTime.Now.ToString());

        Wyjazd w = new Wyjazd(cel, start, koniec);

        string? linia;
        while ((linia = reader.ReadLine()) != null)
        {
            string[] czesci = linia.Split(';');
            if (czesci.Length < 2) continue;
            
            string typ = czesci[0];

            if (typ == "PRZEJAZD" && czesci.Length >= 8)
            {
                w.DodajAktywnosc(new Przejazd(
                    czesci[1], DateTime.Parse(czesci[2]), DateTime.Parse(czesci[3]),
                    czesci[4], czesci[5], double.Parse(czesci[6]), double.Parse(czesci[7])
                ));
            }
            else if (typ == "NOCLEG" && czesci.Length >= 7)
            {
                w.DodajAktywnosc(new Nocleg(
                    czesci[1], DateTime.Parse(czesci[2]), DateTime.Parse(czesci[3]),
                    czesci[4], double.Parse(czesci[5]), int.Parse(czesci[6])
                ));
            }
            else if (typ == "ATRAKCJA" && czesci.Length >= 7)
            {
                w.DodajAktywnosc(new Atrakcja(
                    czesci[1], DateTime.Parse(czesci[2]), DateTime.Parse(czesci[3]),
                    czesci[4], double.Parse(czesci[5]), int.Parse(czesci[6])
                ));
            }
        }

        return w;
    }
}