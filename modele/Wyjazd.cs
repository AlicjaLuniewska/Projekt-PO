using PlanerWyjazdu.Wyjatki;

namespace PlanerWyjazdu.Modele;

public class Wyjazd
{
    public string CelPodrozy { get; set; }
    public DateTime DataRozpoczecia { get; set; }
    public DateTime DataZakonczenia { get; set; }

    private List<Aktywnosc> aktywnosci = new();

    public Wyjazd(string celPodrozy, DateTime dataRozpoczecia, DateTime dataZakonczenia)
    {
        CelPodrozy = this.celPodrozy;
        DataRozpoczecia = this.dataRozpoczecia;
        DataZakonczenia = this.dataZakonczenia;
    }

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

    public double ObliczKoszt()
    {
        double suma = 0;

        foreach (var aktywnosc in aktywnosci)
        {
            suma += aktywnosc.ObliczKoszt();
        }

        return suma;
    }

    public List<Aktywnosc> PobierzHarmonogram()
    {
        return aktywnosci
            .OrderBy(a => a.CzasRozpoczecia)
            .ToList();
    }
}