namespace Zeitrechner
{
    internal class Reader
    {
        public static int readMenueChoiceFromConsole()
        {
            //Liest die Menüwahl ein
            try
            {

                System.Console.Write("Welche Option möchten sie wählen: ");
                string eingabe = System.Console.ReadLine();
                if (eingabe == "exit" || eingabe == "Exit")
                {
                    return 0;
                }
                int eingabeAlsInt = int.Parse(eingabe);
                return eingabeAlsInt;
            }

            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Diese Option gibt es nicht!");
                return readMenueChoiceFromConsole();
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Keine Valide eingabe!");
                return readMenueChoiceFromConsole();

            }
            catch (Exception)
            {
                Console.WriteLine("Ein Problem ist aufgetreten! Sie werden zurück ins Haupt-Menü geschickt!");
                return 9;
            }


        }


        public static TimeOnly readTimeFromConsole()
        {
            //liest eine Zeit ein und gibt sie zurück

            //Feature möglichkeit: Man muss nur das wieder eingeben was man auch falsch gemacht hat.
            //                      Man Kann während der Zeiteingabe zurück zum Menü

            try
            {
                Console.WriteLine("Um wie viel Uhr haben sie gestempelt?");

                System.Console.Write("Uhrzeit in Stunden: ");
                string stundeString = System.Console.ReadLine();
                if (stundeString == "exit" || stundeString == "Exit")
                {
                    System.Console.WriteLine("Gehe zurück zum Hauptmenü!");
                    Thread.Sleep(500);
                    return Zeitrechner.standard;
                }
                int stunde = int.Parse(stundeString);

                if (stunde > 24)
                {
                    throw new ArgumentOutOfRangeException();
                }

                System.Console.Write("Uhrzeit in Minuten: ");
                string minuteString = System.Console.ReadLine();
                if (minuteString == "exit" || minuteString == "Exit")
                {
                    System.Console.WriteLine("gehe Zurück zum Hauptmenü");
                    return Zeitrechner.standard;
                }

                int minute = int.Parse(minuteString);

                if (minute > 60)
                {
                    throw new ArgumentOutOfRangeException();
                }

                TimeOnly eingeleseneZeit = new TimeOnly(stunde, minute);
                Console.WriteLine("Die Zeit: " + eingeleseneZeit + " wurde erfolgreich eingelesen!");

                

                if (Berechner.IstAusserhalbRandzeitÜberprüfen(eingeleseneZeit) == true)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Die Eingesesene Zeit ist Außerhalb deiner Möglichen Arbeitszeit! Bitte geben sie eine Gültige Zeit ein");
                    return readTimeFromConsole();
                }
                else
                {
                    Console.WriteLine("Einlesen Beendet!");
                    Thread.Sleep(1000);
                    return eingeleseneZeit;
                }

            }

            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Sie haben keine Valide Zeit eingegeben! Versuchen sie es erneut\n");
                return readTimeFromConsole();

            }
            catch (System.FormatException)
            {
                Console.WriteLine("Sie haben keine Valide Eigabe getätigt! Versuchen sie es erneut\n");
                return readTimeFromConsole();
            }

            catch (Exception)
            {
                Console.WriteLine("\nEin Problem ist aufgetreten, sie werden zurück ins Menü geschickt!");
                return default;
            }

        }






    }
}
