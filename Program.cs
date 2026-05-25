using ProjektPO.Modele;
using ProjektPO.Wyjatki;

Wyjazd wyjazd = new Wyjazd(
    "Krakow",
    new DateTime(2026, 6, 10),
    new DateTime(2026, 6, 15)
);

try
{
    wyjazd.DodajAktywnosc(new Przejazd(
        "Pociag do Krakowa",
        new DateTime(2026, 6, 10, 8, 0, 0),
        new DateTime(2026, 6, 10, 11, 0, 0),
        "Bialystok",
        "Krakow",
        120,
        20
    ));

    wyjazd.DodajAktywnosc(new Nocleg(
        "Hotel Centrum",
        new DateTime(2026, 6, 10, 14, 0, 0),
        new DateTime(2026, 6, 15, 10, 0, 0),
        "ul. Dluga 10",
        180,
        5
    ));

    wyjazd.DodajAktywnosc(new Atrakcja(
        "Zwiedzanie Wawelu",
        new DateTime(2026, 6, 11, 12, 0, 0),
        new DateTime(2026, 6, 11, 14, 0, 0),
        "Wawel",
        45,
        3
    ));

    Console.WriteLine("HARMONOGRAM WYJAZDU");
    Console.WriteLine("-------------------");

    foreach (var aktywnosc in wyjazd.PobierzHarmonogram())
    {
        Console.WriteLine(aktywnosc.PobierzOpis());
    }

    Console.WriteLine();
    Console.WriteLine($"Calkowity koszt wyjazdu: {wyjazd.ObliczKoszt()} zl");
}
catch (HarmonogramException ex)
{
    Console.WriteLine($"Blad harmonogramu: {ex.Message}");
}
