using ProjektPO.Modele;
using ProjektPO.Wyjatki;
using ProjektPO.Uslugi;
using ProjektPO.Interfejsy;

MenagerPlikow menadzer = new MenagerPlikow();
GeneratorTXT generator = new GeneratorTXT();
Wyjazd mojWyjazd = null!; // Inicjalizacja pustą wartością, zaraz ją utworzymy lub wczytamy

Console.Clear();
Console.WriteLine("====================================================");
Console.WriteLine("        WITAJ W UNIWERSALNYM PLANERZE PODRÓŻY       ");
Console.WriteLine("====================================================");
Console.WriteLine("1. Stwórz nowy plan wyjazdu od zera");
Console.WriteLine("2. Wczytaj istniejący plan z pliku wyjazd.txt");
Console.Write("\nWybierz co chcesz zrobić: ");

string startowyWybor = Console.ReadLine() ?? "";

if (startowyWybor == "2")
{
    try
    {
        mojWyjazd = menadzer.WczytajWyjazd("wyjazd.txt");
        Console.WriteLine("\nPlan wyjazdu został pomyślnie załadowany z pliku!");
        Console.WriteLine("Naciśnij dowolny klawisz, aby przejść do menu...");
        Console.ReadKey();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nNie udało się wczytać pliku ({ex.Message}).");
        Console.WriteLine("Musisz utworzyć nowy wyjazd ręcznie.");
        startowyWybor = "1"; // Wymuszamy utworzenie nowego w przypadku błędu pliku
    }
}

// Jeśli użytkownik wybrał opcję 1 lub wczytanie pliku się nie powiodło
if (startowyWybor != "2" || mojWyjazd == null)
{
    Console.WriteLine("\n--- TWORZENIE NOWEGO WYJAZDU ---");
    Console.Write("Podaj cel podróży (np. Paryż, Bieszczady, Tokio): ");
    string cel = Console.ReadLine() ?? "Nieznany cel";

    DateTime start;
    while (true)
    {
        Console.Write("Podaj datę rozpoczęcia wyjazdu (RRRR-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out start)) break;
        Console.WriteLine("Niepoprawny format daty! Spróbuj ponownie.");
    }

    DateTime koniec;
    while (true)
    {
        Console.Write("Podaj datę zakończenia wyjazdu (RRRR-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out koniec) && koniec >= start) break;
        Console.WriteLine("Niepoprawny format daty lub data zakończenia jest przed startem! Spróbuj ponownie.");
    }

    mojWyjazd = new Wyjazd(cel, start, koniec);
    Console.WriteLine($"\nPomyślnie utworzono plan dla wyjazdu: {cel}!");
    Console.WriteLine("Naciśnij dowolny klawisz, aby przejść do menu głównego...");
    Console.ReadKey();
}

// --- GŁÓWNE MENU APLIKACJI ---
bool aplikacjaDziala = true;

while (aplikacjaDziala)
{
    Console.Clear();
    Console.WriteLine($"====================================================");
    Console.WriteLine($" PLANER WYJAZDÓW - Cel: {mojWyjazd.CelPodrozy.ToUpper()} ({mojWyjazd.DataRozpoczecia:d} - {mojWyjazd.DataZakonczenia:d})");
    Console.WriteLine($"====================================================");
    Console.WriteLine("1. Wyświetl harmonogram i koszty");
    Console.WriteLine("2. Dodaj przejazd");
    Console.WriteLine("3. Dodaj nocleg");
    Console.WriteLine("4. Dodaj atrakcję");
    Console.WriteLine("5. Wyszukaj aktywność");
    Console.WriteLine("6. Usuń aktywność");
    Console.WriteLine("7. Zapisz plan do pliku wyjazd.txt");
    Console.WriteLine("8. Wczytaj inny plan z pliku wyjazd.txt (nadpisz obecny)");
    Console.WriteLine("9. Generuj dokumenty podsumowania (TXT)");
    Console.WriteLine("0. Wyjście");
    Console.WriteLine($"----------------------------------------------------");
    Console.Write("Wybierz opcję: ");

    string wybor = Console.ReadLine() ?? "";

    try
    {
        switch (wybor)
        {
            case "1":
                Console.WriteLine("\n--- AKTUALNY HARMONOGRAM ---");
                var harmonogram = mojWyjazd.PobierzHarmonogram();
                if (harmonogram.Count == 0) Console.WriteLine("Brak zaplanowanych aktywności.");
                foreach (var akt in harmonogram)
                {
                    Console.WriteLine(akt.PobierzOpis());
                }
                Console.WriteLine($"\nCałkowity koszt wyjazdu: {mojWyjazd.ObliczKoszt()} zł");
                break;

            case "2":
                Console.WriteLine("\n--- DODAWANIE PRZEJAZDU ---");
                Console.Write("Nazwa przejazdu: "); string np = Console.ReadLine() ?? "";
                Console.Write("Data startu (RRRR-MM-DD GG:MM): "); DateTime sp = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Data końca (RRRR-MM-DD GG:MM): "); DateTime kp = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Skąd: "); string skad = Console.ReadLine() ?? "";
                Console.Write("Dokąd: "); string dokad = Console.ReadLine() ?? "";
                Console.Write("Cena biletu: "); double cp = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Koszt dodatkowy (np. bagaż): "); double kdp = double.Parse(Console.ReadLine() ?? "0");

                mojWyjazd.DodajAktywnosc(new Przejazd(np, sp, kp, skad, dokad, cp, kdp));
                Console.WriteLine("Przejazd dodany pomyślnie!");
                break;

            case "3":
                Console.WriteLine("\n--- DODAWANIE NOCLEGU ---");
                Console.Write("Nazwa hotelu/noclegu: "); string nn = Console.ReadLine() ?? "";
                Console.Write("Data zameldowania (RRRR-MM-DD GG:MM): "); DateTime sn = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Data wymeldowania (RRRR-MM-DD GG:MM): "); DateTime kn = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Adres: "); string adres = Console.ReadLine() ?? "";
                Console.Write("Cena za jedną noc: "); double czn = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Liczba nocy: "); int ln = int.Parse(Console.ReadLine() ?? "0");

                mojWyjazd.DodajAktywnosc(new Nocleg(nn, sn, kn, adres, czn, ln));
                Console.WriteLine("Nocleg dodany pomyślnie!");
                break;

            case "4":
                Console.WriteLine("\n--- DODAWANIE ATRAKCJI ---");
                Console.Write("Nazwa atrakcji: "); string na = Console.ReadLine() ?? "";
                Console.Write("Data startu (RRRR-MM-DD GG:MM): "); DateTime sa = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Data końca (RRRR-MM-DD GG:MM): "); DateTime ka = DateTime.Parse(Console.ReadLine() ?? "");
                Console.Write("Miejsce: "); string miejsce = Console.ReadLine() ?? "";
                Console.Write("Cena biletu wstępu: "); double cb = double.Parse(Console.ReadLine() ?? "0");
                Console.Write("Liczba osób: "); int lo = int.Parse(Console.ReadLine() ?? "0");

                mojWyjazd.DodajAktywnosc(new Atrakcja(na, sa, ka, miejsce, cb, lo));
                Console.WriteLine("Atrakcja dodana pomyślnie!");
                break;

            case "5":
                Console.Write("\nPodaj szukaną frazę w nazwie: ");
                string fraza = Console.ReadLine() ?? "";
                var znalezione = mojWyjazd.WyszukajAktywnosci(fraza);
                Console.WriteLine("\n--- WYNIKI WYSZUKIWANIA ---");
                if (znalezione.Count == 0) Console.WriteLine("Nie znaleziono pasujących pozycji.");
                foreach (var z in znalezione) Console.WriteLine(z.PobierzOpis());
                break;

            case "6":
                Console.Write("\nPodaj DOKŁADNĄ nazwę aktywności do usunięcia: ");
                string doUsuniecia = Console.ReadLine() ?? "";
                if (mojWyjazd.UsunAktywnosc(doUsuniecia)) Console.WriteLine("Pomyślnie usunięto aktywność.");
                else Console.WriteLine("Nie znaleziono aktywności o takiej nazwie.");
                break;

            case "7":
                menadzer.ZapiszWyjazd(mojWyjazd, "wyjazd.txt");
                Console.WriteLine("\nDane zostały pomyślnie utrwalone w pliku wyjazd.txt!");
                break;

            case "8":
                mojWyjazd = menadzer.WczytajWyjazd("wyjazd.txt");
                Console.WriteLine("\nPlan wyjazdu został pomyślnie załadowany z pliku!");
                break;

            case "9":
                generator.GenerujHarmonogramTXT(mojWyjazd, "harmonogram.txt");
                generator.GenerujKosztyTXT(mojWyjazd, "koszty.txt");
                Console.WriteLine("\nWygenerowano oficjalne raporty: harmonogram.txt oraz koszty.txt!");
                break;

            case "0":
                aplikacjaDziala = false;
                Console.WriteLine("\nZamykanie programu. Udanej podróży!");
                break;

            default:
                Console.WriteLine("\nNiepoprawna opcja! Wybierz cyfrę od 0 do 9.");
                break;
        }
    }
    catch (HarmonogramException ex)
    {
        Console.WriteLine($"\n[BŁĄD HARMONOGRAMU] {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[BŁĄD SYSTEMOWY/FORMATU] Upewnij się, że wpisujesz poprawne dane. Szczegóły: {ex.Message}");
    }

    if (wybor != "0")
    {
        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
}