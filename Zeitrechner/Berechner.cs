using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace Zeitrechner
{
    internal class Berechner
    {
        // ToDo:
        // beschreibungen hinzufügen
        
        //public static void AusrechnenMitWerten()
        //{
        //    //Überprüft ob genug Angaben gemacht wurden
        //    if (Zeitrechner.Speicher[0].Equals(Zeitrechner.standard) | Zeitrechner.Speicher[1].Equals(Zeitrechner.standard) | Zeitrechner.Speicher[2].Equals(Zeitrechner.standard))
        //    {
        //        Thread.Sleep(300);
        //        Console.WriteLine("Die Berechnung ist nicht möglich, sie haben nicht genügend Zeitangaben gemacht. Sie können alternativ mit pauschalwerten Rechnen.");
        //        return;
        //    }
        //
        //    //läd die zeiten aus dem Speicher
        //    TimeOnly berechnungszeit = Zeitrechner.Speicher[0];
        //    TimeOnly pausenbeginn = Zeitrechner.Speicher[1];
        //    TimeOnly pausenende = Zeitrechner.Speicher[2];
        //
        //
        //}
        
        
        
        public static void BerechneMitWerten() //Berechnet mit der angegebenen Pausenzeit 
        {
            //verhindert berechnung, falls die benötigten werte leer sind
            if (Zeitrechner.Speicher[0].Equals(Zeitrechner.standard) | Zeitrechner.Speicher[1].Equals(Zeitrechner.standard) | Zeitrechner.Speicher[2].Equals(Zeitrechner.standard))
            {
                Thread.Sleep(300);
                Console.WriteLine("Die Berechnung ist nicht möglich, sie haben nicht genügend Zeitangaben gemacht. Sie können alternativ mit pauschalwerten Rechnen.");
                return;
            }
            
            //läd die zeiten aus dem Speicher
            TimeOnly berechnungszeit = Zeitrechner.Speicher[0];
            TimeOnly pausenbeginn = Zeitrechner.Speicher[1];
            TimeOnly pausenende = Zeitrechner.Speicher[2];

            //Berechnet die Pausenlänge
            TimeSpan pausenlaenge = pausenende - pausenbeginn;
            Console.WriteLine("Du hast " + pausenlaenge + " Minuten pause gemacht");

            //Arbeitsbeginn wird zur einfachen berechnung um pausenzeit nach hinten verschoben
            TimeOnly ArbeitsbeginnUndPause = berechnungszeit.Add(+pausenlaenge);
            TimeOnly spätesterZeitpunktPause = berechneZeitpunktPause();

            if (pausenbeginn > spätesterZeitpunktPause)
            {
                if(Zeitrechner.ConfigMindJaehrigAktiv == true)
                {
                    Console.WriteLine("Die Zeitbuchuchung so ist ungültig! Du musst bereits nach 4,5 Stunden Pause machen");
                }
                else
                {
                    Console.WriteLine("Die Zeitbuchuchung so ist ungültig! Du musst bereits nach 6 stunden Pause machen");
                }
                Console.WriteLine("Du hättest spätestens um" + spätesterZeitpunktPause + " eine halbe Stunde Pause machen müssen ");
            }

            //Gibt an wann die Regelarbeitszeit erreicht ist
            TimeOnly ZeitpunktRegelarbeitszeitErreicht = ArbeitsbeginnUndPause.Add(Zeitrechner.regelarbeitszeit);
            Console.WriteLine("Um " + ZeitpunktRegelarbeitszeitErreicht + " Uhr ist die Regelarbeitszeit erreicht");

            //Beendet die Berechnung da minderjährige nur 8 stunden arbeiten dürfen
            if(Zeitrechner.ConfigMindJaehrigAktiv == true)
            {
                Console.WriteLine("Du darfst nur bis " + ArbeitsbeginnUndPause.Add(Zeitrechner.achtStunden) + " Uhr arbeiten!");
                return;
            }

            //Berechnet wann 45 Minuten Pause Benötigt werden. 
            TimeOnly ZeitBisLangePauseBenoetigt = ArbeitsbeginnUndPause.Add(Zeitrechner.NeunStunden);

            //Gibt die Uhrzeit aus, ab wann 45 Minuten Pause benötigt werden //Die Config müsste eigentlich egal sein hier aber bei Azubi nochmal anders 
            if (pausenlaenge < Zeitrechner.Minuten45)
            {


                if (Zeitrechner.ConfigDualiAktiv = true && ZeitBisLangePauseBenoetigt >= Zeitrechner.achtzehnUhr)
                {
                    Console.WriteLine("Da du nicht länger als 18 Uhr arbeiten darfst kannst du die Arbeitszeit von 9 Stunden Nicht erreichen!\n");
                }
                else if (Zeitrechner.ConfigANAktiv = true && ZeitBisLangePauseBenoetigt >= Zeitrechner.zwanzigUhr)
                {
                    Console.WriteLine("Da du nicht Länger als 20 Uhr arbeiten darfst, kannst du die Arbeitszeit von 9 Stunden nicht erreichen!\n");
                }
                else
                {
                    Console.WriteLine("Wenn du Länger als " + ZeitBisLangePauseBenoetigt + " Uhr arbeitest musst du 45 Minuten Pause machen!\n");
                }
            }

            TimeOnly ZeitZehnStundenErreicht = ArbeitsbeginnUndPause.Add(Zeitrechner.zehnStunden);
            bool zeitpunktZuSpät = IstAusserhalbRandzeitÜberprüfen(ZeitZehnStundenErreicht);
            if (Zeitrechner.ConfigDualiAktiv == true && zeitpunktZuSpät == true)
            {
                Console.WriteLine("Du kannst die Maximalarbeitszeit von 10 Stunden nicht erreichen da der Zeitpunkt nach 18 Uhr wäre");
            }
            else if (Zeitrechner.ConfigANAktiv == true && zeitpunktZuSpät == true)
            {
                Console.WriteLine("Du kannst die Maximalarbeitszeit von 10 Stunden nicht erreichen, da der Zeitpunkt nach 20 wäre");
            }
            else
            {
                Console.WriteLine("Du musst um " + ZeitZehnStundenErreicht + " Uhr gehen! dann ist die Maximalarbeitszeit von 10 Stunden erreicht");
            }
            return;
                
        }


        public static bool IstAusserhalbRandzeitÜberprüfen(TimeOnly zuUerberpruefendeZeit)
        {
            if (zuUerberpruefendeZeit.Equals(Zeitrechner.standard))
            {
                Console.WriteLine("Sie haben die Zeit zurückgesetzt!");

                return false;

            }

            else if (Zeitrechner.ConfigMindJaehrigAktiv = true && (zuUerberpruefendeZeit > Zeitrechner.achtzehnUhr || zuUerberpruefendeZeit < Zeitrechner.siebenUhr))
            {
                //Console.WriteLine("Der Zeitpunkt liegt außerhalb der gesetzlichen Zeit für Azubis und Dualis von 7 bis 18 Uhr!\n");
                return true;
            }
            else if (Zeitrechner.ConfigDualiAktiv = true && (zuUerberpruefendeZeit > Zeitrechner.achtzehnUhr || zuUerberpruefendeZeit < Zeitrechner.siebenUhr))
            {
                //Console.WriteLine("Der Zeitpunkt liegt außerhalb der gesetzlichen Zeit für Azubis und Dualis von 7 bis 18 Uhr!\n");
                return true;
            }
            else if (Zeitrechner.ConfigANAktiv = true && (zuUerberpruefendeZeit > Zeitrechner.zwanzigUhr || zuUerberpruefendeZeit < Zeitrechner.sechsUhr))
            {
                //Console.WriteLine("Der Zeitpunkt liegt außerhalb der gesetzlichen Arbeitszeit von 6 bis 20 Uhr!\n");
                return true;
            }
            else
            {
                return false; 
            }

        }

        public static void AusgabeBerechneZeitpunktPause()
        {
            //Berechnet den spätesten Pausenzeitpunkt und gibt ihn auf der Konsole aus 
            TimeOnly ZeitpunktPause = berechneZeitpunktPause();
            if (ZeitpunktPause.Equals(Zeitrechner.standard))
            {
                Console.WriteLine("\nDu musst heute keine Pause machen, da der Zeitpunkt nach der Erlaubten Arbeitszeit wäre!\n");
                return;
            }
            Console.WriteLine("Du musst spätestens um " + berechneZeitpunktPause() + " Uhr eine Pause machen");
        }

        public static TimeOnly berechneZeitpunktPause()
        {   //Berechnet den Zeitpunkt wann spätestens eine Pause gemacht werden muss (6 Stunden)

            try
            {
                if (Zeitrechner.Speicher[0].Equals(Zeitrechner.standard))
                {
                    Console.WriteLine("Du hast Keine Ankunftszeit eingegeben! Damit ist die Berechnung unmöglich!");
                    throw new ArgumentException();
                    
                }

                TimeOnly arbeitsbeginn = Zeitrechner.Speicher[0];

                if (Zeitrechner.ConfigMindJaehrigAktiv == true)
                {
                    TimeOnly MJZeitpunkt30MinPause = arbeitsbeginn.Add(Zeitrechner.vierandhalbStunden);
                    
                    TimeOnly MJZeitpunkt60MinPause = arbeitsbeginn.Add(Zeitrechner.sechsStunden);
                    bool ungueltig = IstAusserhalbRandzeitÜberprüfen(MJZeitpunkt30MinPause);
                    if (ungueltig)
                    {
                        return Zeitrechner.standard;                    
                    }
                    return MJZeitpunkt30MinPause;
                }

                else
                {
                    TimeOnly zeitpunktPause = arbeitsbeginn.Add(+Zeitrechner.sechsStunden);
                    bool ungueltig = IstAusserhalbRandzeitÜberprüfen(zeitpunktPause);
                    if (ungueltig)
                    {
                        return Zeitrechner.standard;
                    }
                    return zeitpunktPause;
                
                }
            }
            catch (ArgumentException)
            {
                return Zeitrechner.standard;
            }
        }









        public static void PauschalBerechnen30Minuten() //Berechnet den Pausenzeitpunkt mit einer Pauschalpausenlänge von 30 Minuten 
        {
            //Würde ja theoretisch schon gehen da azubis auch gleitzeitkonto haben und nach 4,5 stunden nur 30 minuten und erst nach 6 stunden 60 minuten machen müssen
            if (Zeitrechner.ConfigMindJaehrigAktiv == true)
            {
                Console.WriteLine("Du bist noch Minerjährig und musst 60 Minuten Pause machen. Du kannst diese Pauschalberechnung nicht durchführen!");
                return;
            }
            Console.WriteLine("Die Pauschalberechnung mit 30 Minuten Pause wird durchgeführt\n");
            PauschalBerechnen(Zeitrechner.Minuten30);

        }
        public static void PauschalBerechnen45Minuten() //Berechnet den Pausenzeitpunkt mit einer Pauschalpausenlänge von 45 Minuten 
        {
            if (Zeitrechner.ConfigMindJaehrigAktiv == true)
            {
                Console.WriteLine("Du bist noch Minerjährig und musst 60 Minuten Pause machen. Du kannst diese Pauschalberechnung nicht durchführen!\n");
                return;
            }
            Console.WriteLine("Die Pauschalberechnung mit 45 Minuten Pause wird durchgeführt\n");
            PauschalBerechnen(Zeitrechner.Minuten45);
        }


        public static void PauschalBerechnen60Minuten()
        {
            Console.WriteLine("Die Pauschalberechnung mit 60 Minuten Pause wird durchgeführt\n");
            PauschalBerechnen(Zeitrechner.Minuten60);
        }




        public static void PauschalBerechnen(TimeSpan pausenlaenge) //Berechnet den Pausenzeitpunkt pauschal mit der Eingabelänge
        {
            if (Zeitrechner.Speicher[0].Equals(Zeitrechner.standard))
            {
                Console.WriteLine("Du hast keine Ankunftszeit eingegeben! Damit ist die berechnung unmöglich!\n");
                return;
            }

            TimeOnly arbeitsbeginn = Zeitrechner.Speicher[0];

            AusgabeBerechneZeitpunktPause();

            TimeOnly ArbeitsbeginnUndPause = arbeitsbeginn.Add(pausenlaenge);

            TimeOnly FrühstesArbeitsende = ArbeitsbeginnUndPause.Add(Zeitrechner.regelarbeitszeit);
            Console.WriteLine("Um " + FrühstesArbeitsende + " Uhr ist die Regelarbeitszeit erreicht");
            if (IstAusserhalbRandzeitÜberprüfen(FrühstesArbeitsende))
            {
                Console.WriteLine("Du kannst die Regelarbeitszeit nichtmehr erreichen, da sie außerhalb des gesetzlich erlaubten Zeitraumes ist.\n");
                return;
            }


            if (Zeitrechner.ConfigMindJaehrigAktiv == true)
            {
                TimeOnly MindJhrgMaxArbeitszeit = ArbeitsbeginnUndPause.Add(Zeitrechner.achtStunden);
                Console.WriteLine("Du darfst maximal bis " + MindJhrgMaxArbeitszeit + " Uhr arbeiten.\n");
                return;
            }
            if (pausenlaenge < Zeitrechner.Minuten45)
            {
                //Berechnet wann 45 Minuten Pause Benötigt werden. 
                TimeOnly Uhrzeit45MinutenPause = ArbeitsbeginnUndPause.Add(Zeitrechner.NeunStunden);

                bool ZeitIstAusserhalb = IstAusserhalbRandzeitÜberprüfen(Uhrzeit45MinutenPause);




                //Gibt die Uhrzeit aus, ab wann 45 Minuten Pause benötigt werden
                if (Zeitrechner.ConfigDualiAktiv = true && ZeitIstAusserhalb == true)
                {
                    Console.WriteLine("Da du nicht länger als 18 Uhr arbeiten darfst kannst du die Arbeitszeit von 9 Stunden Nicht erreichen!\n");
                    return;
                }
                else if(Zeitrechner.ConfigANAktiv = true && ZeitIstAusserhalb == true)
                {
                    Console.WriteLine("Da du nicht länger als 20 Uhr arbeiten darst kannst du die Arbeitszeit von 9 Stunden nicht erreichen!\n");
                    return;                
                }
                else
                {
                    Console.WriteLine("Wenn du Länger als " + Uhrzeit45MinutenPause + " Uhr arbeitest, hast du due 9 Stunden erreicht und musst du 45 Minuten Pause machen!\n");
                }
            }
            else
            {
             TimeOnly ArbeitszeitNeunStunden = ArbeitsbeginnUndPause.Add(Zeitrechner.NeunStunden);
             if (IstAusserhalbRandzeitÜberprüfen(ArbeitszeitNeunStunden))
                {
                    Console.WriteLine("Du kannst die Arbeitszeit von 9 Stunden nicht erreichen, da sie außerhalb der erlaubten Arbeitszeit wäre");
                }
                else
                {
                    Console.WriteLine("Um " + ArbeitszeitNeunStunden + " Uhr sind 9 Stunden Arbeitszeit erreicht");
                    TimeOnly ArbeitszeitZehnStunden = ArbeitsbeginnUndPause.Add(Zeitrechner.zehnStunden);
                    if (IstAusserhalbRandzeitÜberprüfen(ArbeitszeitZehnStunden))
                    {
                        Console.WriteLine("Du kannst die Maximalarbeitszeit von 10 Stunden nicht erreichen, da sie außerhalb der erlaubten Arbeitszeit wäre");
                    }
                    else
                    {
                        Console.WriteLine("Um " + ArbeitszeitZehnStunden + " Uhr ist die Maximalarbeitszeit von 10 Stunden erreicht.");
                    }
                }
                Console.WriteLine("");
            }




        }


        public static void ArbeitszeitBerechnen() //Berechnet die Arbeitszeit in Stunden
        {
            Console.WriteLine("Berechnen der Arbeitszeit gestartet!\n");
            if (Zeitrechner.Speicher[0].Equals(Zeitrechner.standard) || Zeitrechner.Speicher[3].Equals(Zeitrechner.standard))
            {
                Console.WriteLine("Die Berechnung ist nicht möglich! Es fehlen Angaben!");
                return;
            }
            else if (Zeitrechner.Speicher[1].Equals(Zeitrechner.standard) || Zeitrechner.Speicher[2].Equals(Zeitrechner.standard))
            {
                Console.WriteLine("Du keine Pause eingegeben. Damit ist die Berechnung nicht möglich.\n");
                return;
            }
            else
            {
                Console.WriteLine("Die Zeitberechnung wird durchgeführt!\n");
                
                //wiederholung. optimierung möglich? 
                //läd die zeiten aus dem Speicher
                TimeOnly ankunftszeit = Zeitrechner.Speicher[0];
                TimeOnly pausenbeginn = Zeitrechner.Speicher[1];
                TimeOnly pausenende = Zeitrechner.Speicher[2];
                TimeOnly heimgehzeit = Zeitrechner.Speicher[3];

                if (IstAusserhalbRandzeitÜberprüfen(ankunftszeit) || IstAusserhalbRandzeitÜberprüfen(heimgehzeit))
                {
                    Console.WriteLine("Die eingegebenen Zeitpunkte im Speicher sind außerhalb der möglichen Arbeitszeit");
                    return;
                }
                
                //Berechnet die Pausenlänge
                TimeSpan pausenlaenge = pausenende - pausenbeginn;
                Console.WriteLine("Du hast " + pausenlaenge + " Minuten pause gemacht");

                //Arbeitsbeginn wird zur einfachen berechnung um pausenzeit nach hinten verschoben
                TimeOnly ArbeitsbeginnUndPause = ankunftszeit.Add(+pausenlaenge);

                TimeSpan Arbeitslaenge = heimgehzeit - ArbeitsbeginnUndPause;
                TimeSpan ZeitImUnternehmen = heimgehzeit - ankunftszeit;


                //Console.WriteLine("Du warst " +  ZeitImUnternehmen + " im Unternehmen");
                Console.WriteLine("Arbeitszeit: " + Arbeitslaenge + " Stunden");
                
                if ((Zeitrechner.ConfigDualiAktiv || Zeitrechner.ConfigANAktiv) == true && Arbeitslaenge < Zeitrechner.sechsStunden)
                {
                    Console.WriteLine("Du musst keine Pause machen, da ");
                }
                
                if (Arbeitslaenge < Zeitrechner.regelarbeitszeit)
                {
                    Console.Write("Regelarbeitszeit: ");
                    if ((IstAusserhalbRandzeitÜberprüfen(ArbeitsbeginnUndPause.Add(Zeitrechner.regelarbeitszeit)) == false))
                    {
                        Console.WriteLine("Die Regelarbeitszeit ist nicht erreicht!\n Es fehlen " + (Zeitrechner.regelarbeitszeit - Arbeitslaenge) + "Minuten");
                    }
                    else{ 
                        Console.WriteLine("Du kannst die Regelarbeitszeit nicht mehr erreichen!");}
                    }
                else{
                    Console.WriteLine("Die Regelarbeitszeit ist um " + ArbeitsbeginnUndPause.Add(Zeitrechner.regelarbeitszeit) + " Uhr erreicht");
                }
                if (Zeitrechner.ConfigMindJaehrigAktiv && Arbeitslaenge > Zeitrechner.achtStunden)
                {
                    Console.WriteLine("Du darfst nicht so lange arbeiten! du darfst maximal bis " + (ArbeitsbeginnUndPause.Add(Zeitrechner.achtStunden)) + " Uhr arbeiten.");
                }

                if (Arbeitslaenge >= Zeitrechner.NeunStunden && pausenlaenge < Zeitrechner.Minuten45)
                {
                    Console.WriteLine("Wenn du um diese Uhrzeit gehst, brauchst du 45 minuten pause. Du hast weniger als 45 Minuten pause gemacht.\nDir fehlen " + (Zeitrechner.Minuten45 - pausenlaenge) + " Minuten!");

                }
                //hier einfügen: check anhand arbeitsstunden wie viel pause benötigt wird und ob das so passt 





            }
            //Wenn weniger als 4 stunden arbeitszeit dann ganzer gleitzeittag verbraucht (Mindestarbeitszeit)
            //Kernarbeitszeiten checken lassen auch bei den oben. Mo bis Do: 9 bis 15 uhr Freitags: 9 bis 13 Uhr


        }




    }
}
