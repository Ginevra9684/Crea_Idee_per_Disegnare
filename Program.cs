using Newtonsoft.Json;  
class Program
{
    public static void Main(string[] args)
    {
        string scelta;  //
        //--------------//
        Console.Clear();

                    // Titolo
        Console.WriteLine("IDEE PER ILLUSTRAZIONI\n");

                    // Metodo per visualizzare il primo menu di scelta
        MenuPrincipale(); 

        Console.WriteLine("Vuoi un tema di riferimento? (s/n)");

        scelta = Console.ReadLine()!.ToLower().Trim();

        if (scelta == "s")
        {
            ScaricaElemento(@"temi.json", "Il tema sarà : ", 1);   // Metodo per ottenere un tema
        }

        Console.WriteLine("Vuoi una tecnica di riferimento? (s/n)");

        scelta = Console.ReadLine()!.ToLower().Trim();

        if (scelta == "s")
        {
            ScaricaElemento(@"tecniche.json", "La tecnica sarà : ", 1);
        } 
    }

// FUNZIONI PER MENU E SOTTOMENU------------------------------------------------------------------------------------------------------

    static void MenuPrincipale()
    {
        int scelta; //
    //--------------//

                    // Tre opzioni
        Console.WriteLine("Scegliere l'area principale di proprio interesse! (1/2/3)");
        Console.WriteLine("1.Ambiente");
        Console.WriteLine("2.Soggetto");
        Console.WriteLine("3.Ambiente e Soggetto");

        scelta = Convert.ToInt32(Console.ReadLine());

        switch (scelta)
        {
            case 1:
                Console.Clear();
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);  // Metodo per ottenere un luogo
                break;

            case 2:
                Console.Clear();
                            // Metodo per poter scegliere un soggetto specifico
                PreferenzaSoggetto();
                break;

            case 3:
                Console.Clear();
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);
                PreferenzaSoggetto();
                break;

            default:
                MenuPrincipale();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void PreferenzaSoggetto()
    {
        int scelta; //
    //--------------//

                    // Menu per la preferenza di soggetto
        Console.WriteLine("Scegliere tra le seguenti opzioni (1/2/3/4)");
        Console.WriteLine("1.Umano");
        Console.WriteLine("2.Animale");
        Console.WriteLine("3.Creatura");
        Console.WriteLine("4.Nessuna preferenza");
        
        scelta = Convert.ToInt32(Console.ReadLine()!.Trim());

        switch (scelta)
        {
            case 1:
                            // Se viene scelto umano non ci sono specifiche da consigliare
                break;

            case 2:
                Console.Clear();
                ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                break;

            case 3:
                Console.Clear();

                TipoCreatura();

                break;

            case 4:

                QuantitativoSoggetti();

                break;

            default:
                PreferenzaSoggetto();
                break;
        }
        
        
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void TipoCreatura()
    {
        string sceltaCreatura;   //
        int quantitativoAnimali; //
    //---------------------------//

                    // Scelta tra una creatura mitologica e una propria creazione
        Console.WriteLine("Preferisci una creatura mitologica o inventarne una te? (m/i)");

        sceltaCreatura = Console.ReadLine()!.ToLower().Trim();

        if (sceltaCreatura == "m")  // restituiamo una creatura random
        {
            Console.Clear();
                        // Metodo per ottenere una creatura mitologica e Metodo per pulire la console
            ScaricaElemento(@"creature.json", "La creatura sarà: ", 1);
        }
        else if (sceltaCreatura == "i") // restituiamo una lista di animali
        {
            Console.Clear();
            Console.WriteLine("quanti animali vuoi usare per comporre la tua creatura?(2-5)");

                        // Scelta numero animali
            quantitativoAnimali = Convert.ToInt32(Console.ReadLine()!.Trim());
            ScaricaElemento(@"animali.json", "Gli animali saranno: ", quantitativoAnimali);
        }
        else 
        {
            TipoCreatura();
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void QuantitativoSoggetti()
    {
        string quantitativoSoggetti; //
    //-------------------------------//

        Console.WriteLine("Preferisci un soggetto unico o una coppia di soggetti? (u/c)");

                    // scelta quantità soggetti e relative casistiche 
        quantitativoSoggetti = Console.ReadLine()!.ToLower().Trim();

        if (quantitativoSoggetti == "u") SoggettoCasuale();
        
        else if (quantitativoSoggetti == "c")
        {
            Console.WriteLine("Il primo soggetto sarà umano il secondo sarà sorteggiato casualmente");


            SoggettoCasuale();
        }
        else 
        {

            QuantitativoSoggetti();
        }
    }

// METODI PER SCELTE RANDOM ------------------------------------------------------------------------------------------------------------------------------------
    static void SoggettoCasuale()
    {
        Random random = new Random(); //
        int soggettoRandom;           //
    //--------------------------------//

        soggettoRandom = random.Next(1, 4);

        switch(soggettoRandom)
        {
            case 1:
                Console.WriteLine("Il soggetto sarà umano");
                break;
            case 2:
                ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                break;
            case 3:
                TipoCreatura();
                break;
        }
    }


// FUNZIONE PER ESTRAPOLARE GLI ELEMENTI DAI FILE JSON--------------------------------------------------------------------------------

    static void ScaricaElemento(string path, string messaggio, int quantitativoElementi)
    {
        Random random = new Random(); //
        int indice;                   //
    //--------------------------------//
                    // Il path corrisponde a una rotta a un file json che viene cambiato quando viene richiamata la funzione
                    // Il messaggio stampato sarà diverso a seconda del file json
        string json = File.ReadAllText(path);
        dynamic obj = JsonConvert.DeserializeObject(json)!;

                    // Viene selezionato in maniera random uno degli oggetti interni al file "dato in pasto" alla funzione
        indice = random.Next(0,obj.Count);

        Console.WriteLine(messaggio);
                    // Viene stampato il valore dell'oggetto random
        for (int i = 1; i <= quantitativoElementi ;i++ ) 
            {
                            // Il programma stampa un oggetto del file tramite indice scelto in maniera random
                indice = random.Next(0, obj.Count);
                Console.WriteLine(obj[indice].elemento);
            }
    }
//------------------------------------------------------------------------------------------------------------------------------------
}