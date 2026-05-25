namespace PlanerWyjazdu.Modele;

public class Atrakcja : Aktywnosc
{
    public string Miejsce { get; set; }
    public double CenaBiletu { get; set; }
    public int LiczbaOsob { get; set; }

    public Atrakcja(string nazwa, DateTime start, DateTime koniec,
        string miejsce, double cenaBiletu, int liczbaOsob)
        : base(nazwa, start, koniec)
    {
        Miejsce = this.miejsce;
        CenaBiletu = this.cenaBiletu;
        LiczbaOsob = this.liczbaOsob;
    }

    public override double ObliczKoszt()
    {
        return CenaBiletu * LiczbaOsob;
    }

    public override string PobierzOpis()
    {
        return $"Atrakcja: {Nazwa}, miejsce: {Miejsce}, koszt: {ObliczKoszt()} zl";
    }
}