using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Globalization;


namespace Zeitrechner
{
    internal static class Settings
    {
        private static TimeOnly[] PersistenterSpeicher = new TimeOnly[4];
        private enum Profilauswahl
        {
            Minderjaehrig,
            AzubiUndDuali,
            Arbeitnehmer
        }

        private static Profilauswahl GewähltesProfil = Profilauswahl.AzubiUndDuali;

        internal static int GetProfileAsInt() {
            int Profil = (int)GewähltesProfil;
            return Profil; 
        }

        internal static TimeOnly[] GetPersistenterSpeicher() //übergibt den Persistenten speicher aus, aber nur über einen zwischenspeicher, um veränderungen am Persistenten speicher zu verhindern
        {
            TimeOnly[] übergabeSpeicher = new TimeOnly[4];

            for(int i = 0; i < PersistenterSpeicher.Length; i++)
            {
                übergabeSpeicher[i] = PersistenterSpeicher[i];
            }
            return übergabeSpeicher;
        }



        //internal static void ProfilAusJsonLaden()
        //{
        //    IstJsonVorhanden();
        //}

        //private static bool IstJsonVorhanden()
        //{
        //    string path = "C:\\source\\Zeitrechner\\Zeitrechner\\ZeitrechnerEinstellungen.json";
        //    if (File.Exists(path) == true)
        //    {
        //        //Console.WriteLine("Datei gefunden!");
        //        Thread.Sleep(1000);
        //        return true;
        //    }
        //    else
        //    {
        //        string profile = "test";
        //        List<string> test = new List<string> { "{", """Profile" : "Azubi""", """hallo welt""", "}"};
        //        System.Text.Json.JsonSerializer(profile);
        //        File.WriteAllLines(path, test);
        //        return false;
        //    }


 //           {
 //               "Profil" : "Azubi",
 // "Regelarbeitszeit" : {
 //                   "Stunden" : 7,
 //   "Minuten" : 26
 //  } ,
 // "Arbeitsbeginn" : {
 //
 //               }
 //           }
        //}
























        public static void ProfilLaden()
        {
            Zeitrechner.ConfigMindJaehrigAktiv = false;
            Zeitrechner.ConfigDualiAktiv = false;
            Zeitrechner.ConfigANAktiv = false;
            if (GetProfileAsInt().Equals(0))
            {
                Zeitrechner.ConfigMindJaehrigAktiv = true;
                return;
            }
            else if (GetProfileAsInt().Equals(1))
            { 
                Zeitrechner.ConfigDualiAktiv = true;
                return;
            }
            else if (GetProfileAsInt().Equals(1))
            { 
                Zeitrechner.ConfigANAktiv = true;
                return;
            }
            else
            {
                Console.WriteLine("Ein Fehler ist aufgetreten! Es konnte Kein Profil geladen Werden! Es wird das Profil: Regulärer Arbeitnehmer geladen. Um dies zu ändern, passen sie die Appsettings an.");
                Zeitrechner.ConfigANAktiv = true;
                return;
            }


        }





    }


    
}