namespace PlanerWyjazdu.Modele;

public class Nocleg : Aktywnosc
{
    public string Adres { get; set; }
    public double CenaZaNoc { get; set; }
    public int LiczbaNocy { get; set; }

    public Nocleg(string nazwa, DateTime start, DateTime koniec,
        string adres, double cenaZaNoc, int liczbaNocy)
        : base(nazwa, start, koniec)
    {
        Adres = this.adres;
        CenaZaNoc = this.cenaZaNoc;
        LiczbaNocy = this.liczbaNocy;
    }

    public override double ObliczKoszt()
    {
        return CenaZaNoc * LiczbaNocy;
    }

    public override string PobierzOpis()
    {
        return $"Nocleg: {Nazwa}, adres: {Adres}, koszt: {ObliczKoszt()} zl";
    }
}