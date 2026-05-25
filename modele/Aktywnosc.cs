using PlanerWyjazdu.Interfejsy;

namespace PlanerWyjazdu.Modele;

public abstract class Aktywnosc : IKosztowy
{
    public string Nazwa { get; set; }
    public DateTime CzasRozpoczecia { get; set; }
    public DateTime CzasZakonczenia { get; set; }

    protected Aktywnosc(string nazwa, DateTime start, DateTime koniec)
    {
        Nazwa = this.nazwa;
        CzasRozpoczecia = this.start;
        CzasZakonczenia = this.koniec;
    }

    public abstract double ObliczKoszt();

    public virtual string PobierzOpis()
    {
        return $"{Nazwa}: {CzasRozpoczecia:g} - {CzasZakonczenia:g}";
    }

    public bool CzyKolidujeZ(Aktywnosc inna)
    {
        return CzasRozpoczecia < inna.CzasZakonczenia &&
               CzasZakonczenia > inna.CzasRozpoczecia;
    }
}