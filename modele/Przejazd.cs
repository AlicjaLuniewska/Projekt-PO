namespace PlanerWyjazdu.Modele;

public class Przejazd : Aktywnosc
{
    public string Skad { get; set; }
    public string Dokad { get; set; }
    public double CenaBiletu { get; set; }
    public double KosztDodatkowy { get; set; }

    public Przejazd(string nazwa, DateTime start, DateTime koniec,
        string skad, string dokad, double cenaBiletu, double kosztDodatkowy)
        : base(nazwa, start, koniec)
    {
        Skad = this.skad;
        Dokad = this.dokad;
        CenaBiletu = this.cenaBiletu;
        KosztDodatkowy = this.kosztDodatkowy;
    }

    public override double ObliczKoszt()
    {
        return CenaBiletu + KosztDodatkowy;
    }

    public override string PobierzOpis()
    {
        return $"Przejazd: {Skad} -> {Dokad}, koszt: {ObliczKoszt()} zl";
    }
}