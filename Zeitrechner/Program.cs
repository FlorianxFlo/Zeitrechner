namespace Zeitrechner;

//TimeOnly Temp = eingeleseneZeit.AddMinutes(30);

//Feature List:     - Wenn heimgehzeit eingegeben dann soll stundenberechnung möglich sein
//                  - 10 Stundengrenze einfügen
//                  Speicher soll speicherbar sein als datei!
//                  Speicher vom Letzten mal einlesen möglich machen!
//                  Config datei für globale, persistente einstellungen (Unter18, in ausbildung oder duali, Regelarbeitszeit
//                  Genaue SAP berechnungen mit eingliedern
//                  - Berechnungsmöglichkeiten für Minderjährige hinzufügen 

//                  Bugs
//                  - Wenn exit geschriben wird dann wird Wert zurückgesetzt 
//                  - Bei 45 Minuten arbeitszeit soll gesagt werden wann 9 und 10 stunden erreicht werden
//                  - 
class Zeitrechner()
{
    //Grundkonfigurationen
    internal static TimeOnly[] Speicher = new TimeOnly[4];
    internal static TimeOnly standard = new TimeOnly(00, 00);
    internal static TimeOnly sechsUhr = new TimeOnly(6, 0);
    internal static TimeOnly siebenUhr = new TimeOnly(07, 00);
    internal static TimeOnly achtzehnUhr = new TimeOnly(18, 00);
    internal static TimeOnly zwanzigUhr = new TimeOnly(20, 00);
    internal static TimeSpan vierandhalbStunden = new TimeSpan(4,30,0);
    internal static TimeSpan sechsStunden = new TimeSpan(6, 0, 0);
    internal static TimeSpan achtStunden = new TimeSpan(8, 0, 0);
    internal static TimeSpan NeunStunden = new TimeSpan(9, 0, 0);
    internal static TimeSpan zehnStunden = new TimeSpan(10, 0, 0);
    internal static TimeSpan Minuten30 = new TimeSpan(0, 30, 0);
    internal static TimeSpan Minuten45 = new TimeSpan(0, 45, 0);
    internal static TimeSpan Minuten60 = new TimeSpan(0, 60, 0);
    internal static TimeSpan regelarbeitszeit = new TimeSpan(7,36,0);


    internal static bool ConfigDualiAktiv = true;
    internal static bool ConfigANAktiv = false;
    internal static bool ConfigMindJaehrigAktiv = false;

    internal static int Profil;
    
    public static void MenueBefehleAusgeben()
    {
        //Gibt die Programmübersicht aus
        System.Console.WriteLine("Vefügbahre Befehle: \n0: Beendet das Programm \n1: Ankunftszeit eingeben \n2: Pausenstartzeit eingeben \n3: Pausenendzeiteingeben \n4: Heimgehzeit eingeben \n5: Zeitberechnung durchführen \n6: Appsettings anpassen \n7: Speicherstand ausgeben\n");
    }
    
    protected static void SpeicherAusgeben()
    {
        SpeicherAusgeben(Speicher);
    }
    protected static void SpeicherAusgeben(TimeOnly[] Speicher)
    {
        //Gibt den aktuellen Speicherstand mit hinweis auf fehlende Einträge aus 
        TimeOnly standard = new TimeOnly();

        Console.WriteLine("Aktueller Speicherzustand:");
        for (int i = 0; i < Speicher.Length; i++)
        {
            if (Speicher[i].Equals(standard))
            {
                Console.WriteLine(Speicher [i] + "  -> Noch nicht eingegeben!");
            }
            else
            {
                switch (i)
                {
                    case 0:
                        Console.WriteLine(Speicher[0] + " Ankunftszeit");
                        break;
                    case 1:
                        Console.WriteLine(Speicher[1] + " Pausenbeginn");
                        break;
                    case 2:
                        Console.WriteLine(Speicher[2] + " Pausenende");
                        break;
                    case 3:
                        Console.WriteLine(Speicher[3] + " Heimgehzeit");
                        break;
                }
            }

        }

        Console.WriteLine('\n');

    }


    public static void menueAufrufen()
    {
        menueAufrufen(Speicher);
    }
    public static void menueAufrufen(TimeOnly[] Speicher)
    {
        //Zentrales Menü zur Programmverwaltung! Steuert das Programm
        
        int eingabe = (int)Reader.readMenueChoiceFromConsole();
        switch (eingabe)
        {
            case 0:
                System.Console.WriteLine("Das Programm wird beendet!");
                Thread.Sleep(1000);
                Console.WriteLine("Das Programm wurde erfolgreich Heruntergefahren!");
                Thread.Sleep(400);
                Environment.Exit(0);
                return;
            case 1:

                Console.WriteLine("Einlesen der Ankunfstzeit gestartet!\n");
                TimeOnly ZeitEingabe = Reader.readTimeFromConsole();
                if (ZeitEingabe.Equals(Zeitrechner.standard))
                {
                    Speicher[0] = Speicher[0];
                }
                else {
                    Console.WriteLine("Hat funktioniert");
                    Thread.Sleep(1000);
                    Speicher[0] = ZeitEingabe; //macht das überhaupt sinn? 
                    
                }
                Console.Clear();
                SpeicherAusgeben(Speicher);
                MenueBefehleAusgeben();
                
                break;

            case 2:
                Console.WriteLine("Eingeben der Startzeit der Pause gestartet!\n");
                Speicher[1] = Reader.readTimeFromConsole();
                Console.Clear();
                SpeicherAusgeben(Speicher);
                MenueBefehleAusgeben();

                break;

            case 3:
                Console.WriteLine("Eingeben der Endzeit der Pause gestartet!\n");
                Speicher[2] = Reader.readTimeFromConsole();
                Console.Clear();
                SpeicherAusgeben(Speicher);
                MenueBefehleAusgeben();
                break;

            case 4:
                Console.WriteLine("Eingeben der Heimgehzeit gestartet!\n");
                Speicher[3] = Reader.readTimeFromConsole();
                Console.Clear();
                SpeicherAusgeben();
                MenueBefehleAusgeben();
                break;

            case 5:
                
                Console.WriteLine("\nWelche Zeitberechnung möchten sie Durchführen?");
                berechnungsmöglichkeitenAusgeben();
                break;

            case 6:

                break;

            case 7://Hier vielleicht Details zum Speicher als Feature
                Console.Clear();
                SpeicherAusgeben(Speicher);
                MenueBefehleAusgeben();
                break;

            case 9:
                Console.Clear();
                SpeicherAusgeben(Speicher);
                MenueBefehleAusgeben();
                Console.WriteLine("Ein Fehler ist aufgetreten! sie Wurden zurück ins Menü geschickt!");
                break;

            default:
                System.Console.WriteLine("Sie haben keine Valide Option eingegeben.");
                break;
        }
    }

    private static void berechnungsmöglichkeitenAusgeben()
    {
        Console.Clear();
        SpeicherAusgeben();
        
        Console.WriteLine("Die Berechnungsmöglichkeiten sind: ");
        Console.WriteLine("0: Zurück zum Menü \n1: Mit angegebenen Pausenzeiten berechnen \n2: Pausenzeitpunkt berechnen \n3: Mit 30 Minuten Pauschal ausrechnen \n4: Mit 45 Minuten Pauschal Ausrechnen \n5: Arbeitslänge ausrechnen und überprüfen\n6: Mit 60 Minuten Pauschal Ausrechnen\n");
        Console.WriteLine("Das Berechnungsmodul wurde gesartet!\n");
        int menueEingabeBerechnung;
        
        do
        {
            menueEingabeBerechnung = Reader.readMenueChoiceFromConsole();
            switch (menueEingabeBerechnung)
            {
                case 0:
                    Console.WriteLine("zurück zum Menü");
                    Thread.Sleep(500);
                    Console.Clear();
                    SpeicherAusgeben();
                    MenueBefehleAusgeben();
                    menueAufrufen(Speicher);
                    return;

                case 1:
                    Berechner.BerechneMitWerten();
                    break;
                
                 case 2:
                    Berechner.AusgabeBerechneZeitpunktPause();
                    break;

                case 3: 
                    Berechner.PauschalBerechnen30Minuten();
                    break;

                case 4:
                    Berechner.PauschalBerechnen45Minuten();
                    break;

                case 5:
                    Berechner.ArbeitszeitBerechnen();
                    break;

                 case 6:
                    Berechner.PauschalBerechnen60Minuten();
                    break;

                default:
                    Console.WriteLine("keine Valide Möglichkeit eingegeben!");
                    break;



            }
        } while (menueEingabeBerechnung != 0);
    }

    public static void Main(String[] args)
    {
        Console.Title = "Florians Zeitrechner";
        System.Console.WriteLine("Zeitrechner gestartet!");
        Console.WriteLine("-----------------------------------");
        SpeicherAusgeben();
        MenueBefehleAusgeben();
       // Settings.ProfilAusJsonLaden();
        while (true)
        {
            menueAufrufen(Speicher);
        }
    
    
    }


}
