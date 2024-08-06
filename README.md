# CREA IDEE PER DISEGNARE

- Creare un'app che dia consigli su cosa progettare sia in 2d che 3d ed eventualmente dica quali oggetti/soggetti prendere come reference

## Target

Chiunque voglia disegnare o creare un progetto 3D ma non ha una consegna precisa o non ha delle idee da cui partire

# Funzionalità Base

- L'app chiede all'utente se vuole creare un'ambientazione, un soggetto o entrambi.
- Per l'`ambientazione` sceglierà, in maniera `random` e da un `file` esterno, un `luogo specifico`.
- Per il `soggetto` chiederà all'utente se ha delle `preferenze` (umano, animale, creatura).
- Se l'utente non ha preferenze, il computer chiederà se l'utente desidera `un solo` soggetto o `una coppia` di soggetti (umano + animale/creatura)
- Se la scelta è `creatura` l'app chiede se si vuole una `creatura mitologica` o se si vuole creare una `propria invenzione`
- Se l'utente vuole creare una propria invenzione gli verrà fornita una `lista` di `animali` con un quantitativo di elementi tra 2 e 5
- L'app chiede se l'tente necessita di un `tema`
- L'app chiede se si necessita di una `tecnica` di disegno

# PROGETTAZIONE 

<details> 

<summary> Steps </summary>


- [X] Creare un file Json contenente le ambientazioni
- [X] Creare un file Json contenente vari animali
- [X] Creare un file Json contenente creature mitologiche
- [X] Creare un file Json contenente vari temi
- [X] Creare un file Json contenente le possibili tecniche

- [X] Scrivere il menu principale di scelta (ambientazione, soggetto, entrambi)
- [X] Creare una funzione per poter estrarre i vari elementi dai file json
- [X] - estrarre il luogo dal file Json

- [x] Aggiungere il sottomenu del soggetto (umano, animale, creatura, nessuna preferenza)
- [x] - scelta uno o due soggetti se non si ha nessuna preferenza
- [x] - - estrazione da file Json

- [x] - estrarre tema
- [x] - estrarre tecnica

- [X] Chiusura del programma
- [x] Gestione delle eccezioni

</details>

<details>
<summary> schema </summary>

```mermaid
stateDiagram-v2
    state if_state <<choice>>
    state if_state2 <<choice>>
    state if_state3 <<choice>>
    state if_state4 <<choice>>
    state if_state5 <<choice>>
    state if_state6 <<choice>>
    CREA_IDEE --> Scelta
    Scelta --> Soggetto?
    Scelta --> Ambientazione_e_Soggetto?
    Scelta --> Ambientazione?
    Ambientazione? --> Luogo_Specifico 
    Soggetto? --> Preferenza?\n(umano/animale/creatura)
    Preferenza?\n(umano/animale/creatura) --> if_state
    if_state --> Scelta_Manuale\nun_solo_soggetto : if risposta = si
    Scelta_Manuale\nun_solo_soggetto --> if_state2
    if_state2 --> Tema? : if scelta != creatura
    if_state2 --> Creatura_Mitologica_o\nPropria_Creazione? : if scelta = creatura
    Creatura_Mitologica_o\nPropria_Creazione? --> if_state3
    if_state3 --> Creatura_Mitologica\nRandom : if scelta = 1
    Creatura_Mitologica\nRandom --> Tema?
    if_state3 --> Quantità_Animali_per\nCreazione? : if scelta = 2
    Quantità_Animali_per\nCreazione? --> Lista_Animali_Random
    Lista_Animali_Random --> Tema?
    if_state --> Scelta_Soggetto_Unico_o\nDoppio_Soggetto : if risposta = no
    Scelta_Soggetto_Unico_o\nDoppio_Soggetto --> if_state4
    if_state4 --> Soggetto_Random : if risposta = 1
    if_state4 --> Soggetto_Umano_+\nSoggetto_Random : if risposta = 2
    Ambientazione_e_Soggetto? --> Luogo_Specifico
    Luogo_Specifico --> Tema? : if ambientazione
    Luogo_Specifico --> Preferenza?\n(umano/animale/creatura) : if ambientazione e soggetto
    Soggetto_Random --> Tema? : if Random != creatura
    Soggetto_Random --> Creatura_Mitologica_o\nPropria_Creazione? : if Random = creatura
    Soggetto_Umano_+\nSoggetto_Random --> Creatura_Mitologica_o\nPropria_Creazione? : if Random = creatura
    Soggetto_Umano_+\nSoggetto_Random --> Tema? : if Random != creatura
    Tema?--> if_state5
    if_state5 --> Tema_Random : if risposta = si
    Tema_Random --> Tecnica?
    if_state5 --> Tecnica? : if risposta = no
    Tecnica? --> if_state6
    if_state6 --> Tecnica_Random : if risposta = si
    if_state6 --> FINE : if risposta = no
    Tecnica_Random --> FINE
```

</details>

<details>
<summary>Codice</summary>

```C#
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

        Avvertimenti();
        Proseguimento();

                    // Metodo per visualizzare il primo menu di scelta
        MenuPrincipale(); 

        Console.WriteLine("Vuoi un tema di riferimento? (s/n)");

        scelta = Console.ReadKey(true).KeyChar.ToString()!.ToLower();

        if (scelta == "s")
        {
            ScaricaElemento(@"temi.json", "Il tema sarà : ", 1);   // Metodo per ottenere un tema
            Proseguimento();
        }
        else if (scelta == "n") Proseguimento();
        else Errore();

        Console.WriteLine("Vuoi una tecnica di riferimento? (s/n)");

        scelta = Console.ReadKey(true).KeyChar.ToString()!.ToLower();

        if (scelta == "s")
        {
            ScaricaElemento(@"tecniche.json", "La tecnica sarà : ", 1);
            Proseguimento();
        } 
        else if (scelta == "n") Proseguimento();
        else Errore();

        Conclusione();  // Metodo per chiudere il programma 
    }

// METODI PER LA FLUIDITÀ DEL PROGRAMMA---------------------------------------------------------------------------------------------

    static void Avvertimenti()
    {
        Console.WriteLine("REGOLE ED AVVERTIMENTI");
        Console.WriteLine("1.I nomi di animali, creature e temi saranno scritti in inglese per convenzione");
        Console.WriteLine("2.Se si fa un inserimento sbagliato l'opzione darà errore o/e verrà saltata");
    }
//----------------------------------------------------------------------------------------------------------------------------------
    static void Proseguimento()
    {
                    // Per permettere all'utente di proseguire al premere di untasto e cancellare a schermo le linee precedenti
        Console.WriteLine("\nPremere un tasto per proseguire...");
        Console.ReadKey();
        Console.Clear();
    }
//--------------------------------------------------------------------------------------------------------------------------------------
    static void Conclusione()
    {               
                    // Frasi di chiusura
        Console.WriteLine("Ora dovresti avere tutto l'occorrente per iniziare il tuo progetto");
        Console.WriteLine("Di seguito alcuni siti/app dove poter pubblicare le tue opere");
    }
//-------------------------------------------------------------------------------------------------------------------------------
    static void Errore()
    {
        Console.WriteLine("Opzione non valida");   // in caso di inserimento non previsto
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

        try
        {               
            scelta = int.Parse(Console.ReadKey(true).KeyChar.ToString()!);

            switch (scelta)
            {
                case 1:
                    Console.Clear();
                    ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);  // Metodo per ottenere un luogo
                    Proseguimento();
                    break;

                case 2:
                    Console.Clear();
                                // Metodo per poter scegliere un soggetto specifico
                    PreferenzaSoggetto();
                    break;

                case 3:
                    Console.Clear();
                    ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);
                    Proseguimento();
                    PreferenzaSoggetto();
                    break;

                default:
                    Errore();
                    Proseguimento();
                    MenuPrincipale();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si richiede l'inserimento di un numero");
            Console.WriteLine($"ERRORE NON TRATTATO: {ex.Message}");
            Proseguimento();
            MenuPrincipale();
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

        try
        {
            scelta = int.Parse(Console.ReadKey(true).KeyChar.ToString()!);

            switch (scelta)
            {
                case 1:
                    Console.Clear();
                                // Se viene scelto umano non ci sono specifiche da consigliare
                    Console.WriteLine($"Il soggetto sarà umano");
                    Proseguimento();
                    break;

                case 2:
                    Console.Clear();
                    ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                    Proseguimento();
                    break;

                case 3:
                    Console.Clear();
                    TipoCreatura();
                    break;

                case 4:
                    Proseguimento();
                    QuantitativoSoggetti();
                    break;

                default:
                    Errore();
                    Proseguimento();
                    PreferenzaSoggetto();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si richiede l'inserimento di un numero");
            Console.WriteLine($"ERRORE NON TRATTATO: {ex.Message}");
            Proseguimento();
            PreferenzaSoggetto();
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

        try
        {
            sceltaCreatura = Console.ReadKey(true).KeyChar.ToString()!.ToLower();

            if (sceltaCreatura == "m")  // restituiamo una creatura random
            {
                Console.Clear();
                            // Metodo per ottenere una creatura mitologica e Metodo per pulire la console
                ScaricaElemento(@"creature.json", "La creatura sarà: ", 1);
                Proseguimento();
            }
            else if (sceltaCreatura == "i") // restituiamo una lista di animali
            {
                Console.Clear();
                Console.WriteLine("quanti animali vuoi usare per comporre la tua creatura?(2-5)");

                            // Scelta numero animali
                quantitativoAnimali = Convert.ToInt32(Console.ReadLine()!.Trim());
                ScaricaElemento(@"animali.json", "Gli animali saranno: ", quantitativoAnimali);
                Proseguimento();
            }
            else 
            {
                Errore();
                Proseguimento();
                TipoCreatura();
            }
        }
        catch (Exception ex)
        {
                        // Non preciso un messaggio perchè non sono a conoscenza di tipi di eccezioni per questa casistica
            Console.WriteLine($"ERRORE NON TRATTATO: {ex.Message}");
            Proseguimento();
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
        quantitativoSoggetti = Console.ReadKey(true).KeyChar.ToString()!.ToLower();

        if (quantitativoSoggetti == "u") SoggettoCasuale();
        
        else if (quantitativoSoggetti == "c")
        {
            Console.WriteLine("Il primo soggetto sarà umano il secondo sarà sorteggiato casualmente");

            Proseguimento();
            SoggettoCasuale();
        }
        else 
        {
            Errore();
            Proseguimento();
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
                Proseguimento();
                break;
            case 2:
                ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                Proseguimento();
                break;
            case 3:
                TipoCreatura();
                Proseguimento();
                break;

            default:
                Errore();
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
}
```

</details>

<details>
<summary>Commits</summary>

```bash
git add --all
git commit -m "Progettazione ReadMe"
git push -u origin main

git add --all
git commit -m "Aggiunti 5 file json per estrapolare elementi"
git push -u origin main

git add --all
git commit -m "Menu principale + funzione per estrarre elementi da file json"
git push -u origin main

git add --all
git commit -m "Menu selezione soggetto"
git push -u origin main

git add --all
git commit -m "Scelta soggetto singolo o doppio"
git push -u origin main

git add --all
git commit -m "Aggiunta tecnica e tema"
git push -u origin main

git add --all
git commit -m "Funzioni per la fluidità del programma"
git push -u origin main

git add --all
git commit -m "Aggiunta gestione eccezioni e conclusione progetto base"
git push -u origin main
```

</details>

## IMPLEMENTAZIONI

- [x] Spectre.Console
- [x] Aggiunta caratteristiche dei luoghi (es meteo, orario);
- [x] Menu gestionale
- [X] Stampa tabella finale con tutte le scelte
- [ ] Inserire più elementi nei vari file json
- [ ] Inserire una lista esaustiva nel file animali
- [ ] Opzione lingua inglese
- [ ] Opzione lingua francese
- [ ] Opzione altre lingue
- [ ] Aggiunta opzioni come caratteristiche e personalità dei personaggi, materiali degli oggetti
- [ ] Link a siti esterni
- [ ] visualizzazione foto di esempio
- [ ] HTML e CSS

<details> 
<summary>Schema finale senza menu gestionale</summary>

```C#
using Newtonsoft.Json;  

using Spectre.Console;
class Program
{
    public static void Main(string[] args)
    {
        AnsiConsole.Clear();
        AnsiConsole.Markup("[50]IDEE PER ILLUSTRAZIONI[/]:artist_palette:\n\n");

        Avvertimenti();
        MenuPrincipale(); 
        ScelteSecondarie();
        Conclusione();  
    }

// METODI PER LA FLUIDITÀ DEL PROGRAMMA---------------------------------------------------------------------------------------------

    static void Avvertimenti()
    {
        AnsiConsole.Markup("[204]REGOLE ED AVVERTIMENTI[/]\n");
        AnsiConsole.Markup("[208]1.[/]I nomi di animali, creature e temi saranno scritti in inglese per convenzione[208].[/]:anger_symbol:\n");
        AnsiConsole.Markup("[208]2.[/]Se si fa un inserimento sbagliato l'opzione darà errore o/e verrà saltata[208].[/]:cross_mark:\n");
        Proseguimento();
    }
//----------------------------------------------------------------------------------------------------------------------------------
    static void Proseguimento()
    {
        AnsiConsole.Markup("\n:red_exclamation_mark:Premere un tasto per proseguire[221].[/][222].[/][223].[/]");
        Console.ReadKey();
        AnsiConsole.Clear();
    }
//--------------------------------------------------------------------------------------------------------------------------------------
    static void Conclusione()
    {               
        AnsiConsole.Markup(":blowfish: Ora dovresti avere tutto l'occorrente per iniziare il tuo progetto\n");
        AnsiConsole.Markup(":backhand_index_pointing_down: Di seguito alcuni siti/app dove poter pubblicare le tue opere : \n");
    }
//-------------------------------------------------------------------------------------------------------------------------------
    static void Errore()
    {
        AnsiConsole.Markup(":red_circle:Opzione non valida\n");
    }

// FUNZIONI PER MENU E SOTTOMENU------------------------------------------------------------------------------------------------------

    static void MenuPrincipale()
    {
        string input;  //
    //-----------------//

        input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("<-<-<-[50]SCELTA PRINCIPALE[/]->->->")
            .PageSize(3)
            .MoreChoicesText("Spostati con le frecce direzionali.")
            .AddChoices(new[] {"[86]1.[/]Ambiente[86].[/]","[85]2.[/]Soggetto[85].[/]","[49]3.[/]Ambiente e Soggetto[49].[/]"   // Tre opzioni
            }));
                    
        switch (input)
        {
            case "[86]1.[/]Ambiente[86].[/]":
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1); 
                CaratteristicheLuogo();
                break;
            case "[85]2.[/]Soggetto[85].[/]":   PreferenzaSoggetto();    
                break;
            case "[49]3.[/]Ambiente e Soggetto[49].[/]":
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);
                CaratteristicheLuogo();
                PreferenzaSoggetto();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void CaratteristicheLuogo()
    {
        var caratteristiche = AnsiConsole.Prompt(
        new MultiSelectionPrompt<string>()
            .Title("<-<-<-[50]CARATTERISTICHE LUOGO[/]->->->")
            .NotRequired() 
            .PageSize(3)
            .MoreChoicesText("[grey](Spostati su e giù per più opzioni)[/]")
            .InstructionsText(
                "[grey](Premi [117]<spacebar>[/] per aggiungere una richiesta, " + 
                "[123]<enter>[/] per confermare le tue scelte)[/]")
            .AddChoices(new[] {
                "[86]1.[/] Meteo[86].[/]", "[85]2.[/] Momento[85].[/]"
            }));

        if (caratteristiche.Contains("[86]1.[/] Meteo[86].[/]"))    ScaricaElemento(@"meteo.json", "Il meteo sarà: ", 1);
        if (caratteristiche.Contains("[85]2.[/] Momento[85].[/]"))    ScaricaElemento(@"momenti.json", "Il momento sarà: ", 1);
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void PreferenzaSoggetto()
    {
        string input;  //
    //-----------------//

        AnsiConsole.Clear();
        input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("<-<-<-[50]SCELTA SOGGETTO[/]->->->")
            .PageSize(4)
            .MoreChoicesText("Spostati con le frecce direzionali.")
            .AddChoices(new[] {"[86]1.[/]Umano[86].[/]","[85]2.[/]Animale[85].[/]","[49]3.[/]Creatura[49].[/]","[79]4.[/]Nessuna preferenza[79].[/]"   // Quattro opzioni
            }));

        switch (input)
        {
            case "[86]1.[/]Umano[86].[/]":
                AnsiConsole.Clear();
                Console.WriteLine($"Il soggetto sarà umano");
                Proseguimento();
                break;
            case "[85]2.[/]Animale[85].[/]":    ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                break;
            case "[49]3.[/]Creatura[49].[/]":    TipoCreatura();
                break;
            case "[79]4.[/]Nessuna preferenza[79].[/]":    QuantitativoSoggetti();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void TipoCreatura()
    {
        string input;                //
        string quantitativoAnimali;  //
    //-------------------------------//

        AnsiConsole.Clear();
        input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("<-<-<-[50]PREFERENZA CREATURA[/]->->->")
            .PageSize(3)
            .MoreChoicesText("Spostati con le frecce direzionali.")
            .AddChoices(new[] {"[86]1.[/]Creatura Mitologica[86].[/]","[85]2.[/]Propria Invenzione[85].[/]"  
            }));

        switch (input)
        {
            case "[86]1.[/]Creatura Mitologica[86].[/]":    ScaricaElemento(@"creature.json", "La creatura sarà: ", 1);
                break;
            case "[85]2.[/]Propria Invenzione[85].[/]":
                AnsiConsole.Clear();
                
                quantitativoAnimali = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("<-<-<-[50]QUANTI ANIMALI PER LA CREAZIONE?[/]->->->")
                    .PageSize(3)
                    .MoreChoicesText("Spostati con le frecce direzionali.")
                    .AddChoices(new[] {"[86].2[/]","[85].3[/]","[49].4[/]","[79].5[/]"  
                    }));
                switch (quantitativoAnimali)
                {
                    case "[86].2[/]":   ScaricaElemento(@"animali.json", "Gli animali saranno: ", 2);
                        break;
                    case "[85].3[/]":   ScaricaElemento(@"animali.json", "Gli animali saranno: ", 3);
                        break;
                    case "[49].4[/]":    ScaricaElemento(@"animali.json", "Gli animali saranno: ", 4);
                        break;
                    case "[79].5[/]":    ScaricaElemento(@"animali.json", "Gli animali saranno: ", 5);
                        break;
                }
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void QuantitativoSoggetti()
    {
        string quantitativoSoggetti; //
    //-------------------------------//

        AnsiConsole.Clear();
        quantitativoSoggetti = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("<-<-<-[50]QUANTITÀ SOGGETTI[/]->->->")
                .PageSize(3)
                .MoreChoicesText("Spostati con le frecce direzionali.")
                .AddChoices(new[] {"[86]1.[/]Soggetto Unico[86].[/]","[85]2.[/]Doppio Soggetto" 
                }));

        switch(quantitativoSoggetti)
        {
            case "[86]1.[/]Soggetto Unico[86].[/]" :    SoggettoCasuale();
                break;
            case "[85]2.[/]Doppio Soggetto" :
                AnsiConsole.Markup("[97]-[/]Il primo soggetto sarà umano il secondo sarà sorteggiato casualmente\n");
                Proseguimento();
                SoggettoCasuale();
                break;
        }
    }

// METODI PER SCELTE RANDOM ------------------------------------------------------------------------------------------------------------------------------------
    static void SoggettoCasuale()
    {
        Random random = new Random(); //
        int soggettoRandom;           //
    //--------------------------------//

        AnsiConsole.Clear();

        soggettoRandom = random.Next(1, 4);

        switch(soggettoRandom)
        {
            case 1:     Console.WriteLine("Il soggetto sarà umano");
                break;
            case 2:    ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                break;
            case 3:    TipoCreatura();
                break;
            default:    Errore();
                break;
        }
        if (soggettoRandom != 2 & soggettoRandom != 3 )    Proseguimento();
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void ScelteSecondarie()
    {
                    // Prompt per scelte multiple
        var altreOpzioni = AnsiConsole.Prompt(
        new MultiSelectionPrompt<string>()
            .Title("<-<-<-[50]AGGIUNTE[/]->->->")
            .NotRequired() 
            .PageSize(10)
            .MoreChoicesText("[grey](Spostati su e giù per più opzioni)[/]")
            .InstructionsText(
                "[grey](Premi [117]<spacebar>[/] per aggiungere una richiesta, " + 
                "[123]<enter>[/] per confermare le tue scelte)[/]")
            .AddChoices(new[] {
                "[86]1.[/] Tema[86].[/]", "[85]2.[/] Tecnica[85].[/]"
            }));
        
        if (altreOpzioni.Contains("[86]1.[/] Tema[86].[/]"))    ScaricaElemento(@"temi.json", "Il tema sarà : ", 1); 
        if (altreOpzioni.Contains("[85]2.[/] Tecnica[85].[/]"))    ScaricaElemento(@"tecniche.json", "La tecnica sarà : ", 1);
    }

// FUNZIONE PER ESTRAPOLARE GLI ELEMENTI DAI FILE JSON--------------------------------------------------------------------------------

    static void ScaricaElemento(string path, string messaggio, int quantitativoElementi)
    {
        Random random = new Random(); //
        int indice;                   //
    //--------------------------------//

        AnsiConsole.Clear();
        string json = File.ReadAllText(path);
        dynamic obj = JsonConvert.DeserializeObject(json)!;

        indice = random.Next(0,obj.Count);

        AnsiConsole.Markup($":backhand_index_pointing_right: {messaggio} [154]:[/]\n\n");;

        for (int i = 1; i <= quantitativoElementi ;i++ ) 
        {
            indice = random.Next(0, obj.Count);
            AnsiConsole.Markup($"[154]-[/] {obj[indice].elemento}[154].[/]\n");
        }
        Proseguimento();
    }
}
```
</details>

# Schema 

I menu di scelta singola funzionano tramite uno switch

I menu di scelta multipla funzionano tramite degli if

```mermaid

stateDiagram-v2
    state if_state <<choice>>
    state if_state2 <<choice>>
    state if_state3 <<choice>>
    state if_state4 <<choice>>
    state if_state5 <<choice>>

    CREA_IDEE --> Prompt_Scelta\nSingola\nMenu_Principale
    Prompt_Scelta\nSingola\nMenu_Principale --> if_state
    if_state--> Soggetto
    if_state --> Ambientazione_e_Soggetto
    if_state --> Ambientazione
    Ambientazione --> Luogo_Random
    Luogo_Random --> Prompt_Scelta\nMultipla\nCaratteristiche_Luogo
    Prompt_Scelta\nMultipla\nCaratteristiche_Luogo --> Condizioni_Metereologiche
    Prompt_Scelta\nMultipla\nCaratteristiche_Luogo --> Momento
    Condizioni_Metereologiche --> Meteo_Random : if selezionato
    Momento --> Momento_Random : if selezionato
    Soggetto --> Prompt_Scelta\nSingola\nPreferenza_Soggetto
    Prompt_Scelta\nSingola\nPreferenza_Soggetto --> if_state2
    if_state2 --> Soggetto_Umano
    if_state2 --> Animale
    if_state2 --> Creatura
    if_state2 --> Nessuna_Preferenza
    Ambientazione_e_Soggetto --> Luogo_Random
    Meteo_Random --> Prompt_Scelta\nMultipla\nAggiunte : if ambientazione
    Momento_Random --> Prompt_Scelta\nSingola\nPreferenza_Soggetto : if ambientazione e soggetto
    Momento_Random --> Prompt_Scelta\nMultipla\nAggiunte : if ambientazione
    Meteo_Random --> Prompt_Scelta\nSingola\nPreferenza_Soggetto : if ambientazione e soggetto
    Soggetto_Umano --> Prompt_Scelta\nMultipla\nAggiunte
    Animale --> Animale_Random
    Animale_Random --> Prompt_Scelta\nMultipla\nAggiunte
    Creatura --> Prompt_Scelta\nSingola\nPreferenza_Creatura
    Prompt_Scelta\nSingola\nPreferenza_Creatura --> if_state3
    if_state3 --> Creatura_Mitologica
    if_state3 --> Propria_Creazione
    Creatura_Mitologica --> Creatura_Mitologica\nRandom
    Propria_Creazione --> Quantità_Animali\nComposizione
    Quantità_Animali\nComposizione --> Prompt_Scelta\nSingola\nNumero_Animali\nComposizione
    Prompt_Scelta\nSingola\nNumero_Animali\nComposizione --> Lista_Animali
    Creatura_Mitologica\nRandom --> Prompt_Scelta\nMultipla\nAggiunte
    Lista_Animali --> Prompt_Scelta\nMultipla\nAggiunte
    Nessuna_Preferenza --> Prompt_Scelta\nSingola\nQuantità_Soggetti
    Prompt_Scelta\nSingola\nQuantità_Soggetti --> if_state4
    if_state4 --> Soggetto_Unico
    if_state4 --> Doppio_Soggetto
    Soggetto_Unico --> Soggetto_Random
    Doppio_Soggetto --> Soggetto_Umano_+\nSoggetto_Random
    Soggetto_Random --> Prompt_Scelta\nMultipla\nAggiunte : if Soggetto != Creatura
    Soggetto_Random --> Prompt_Scelta\nSingola\nPreferenza_Creatura : if Soggetto = Creatura
    Soggetto_Umano_+\nSoggetto_Random --> Prompt_Scelta\nSingola\nPreferenza_Creatura : if Soggetto = Creatura
    Soggetto_Umano_+\nSoggetto_Random --> Prompt_Scelta\nMultipla\nAggiunte : if Soggetto != Creatura
    Prompt_Scelta\nMultipla\nAggiunte --> Tema
    Prompt_Scelta\nMultipla\nAggiunte --> Tecnica
    Tema --> Tema_Random : if selezionato
    Tecnica --> Tecnica_Random : if selezionato
    Tema_Random --> Prompt_Scelta\nSingola\nGestionale
    Tecnica_Random --> Prompt_Scelta\nSingola\nGestionale
    Prompt_Scelta\nSingola\nGestionale --> if_state5
    if_state5 --> Tabella_Dati 
    Tabella_Dati --> Tabella_con\nSelezioni_Correnti
    if_state5 --> Progetti
    Progetti --> Prompt_Selezione\nSingola\nOpzioni
    Prompt_Selezione\nSingola\nOpzioni --> Visualizza
    Prompt_Selezione\nSingola\nOpzioni --> Elimina
    Visualizza --> Prompt_Selezione\nMultipla\nSeleziona_Progetto
    Elimina --> Prompt_Selezione\nMultipla\nSeleziona_Progetto
    Prompt_Selezione\nMultipla\nSeleziona_Progetto --> Tutti_File.json\nCartella_Progetti
    if_state5 --> Inizia_Nuovo_Progetto 
    Inizia_Nuovo_Progetto -->  RESTART 
    if_state5 --> Esci 
    Esci --> TERMINA_PROGRAMMA
```


<details>
<summary>Commits implementazioni</summary>

```bash
git add --all
git commit -m "Progettazione implementazioni"
git push -u origin main

git add --all
git commit -m "Spectre.Console"
git push -u origin main

git add --all
git commit -m "Menu tema/tecnica + nuovo menu meteo/momento giornata"
git push -u origin main

git add --all
git commit -m "Migliorati Console.Clear"
git push -u origin main

git add --all
git commit -m "Struttura menu finale + Tabella scelte"
git push -u origin main

git add --all
git commit -m "Migliorati Console.Clear"
git push -u origin main

git add --all
git commit -m "Restart, Esci, Salva/Scarta progetto"
git push -u origin main

git add --all
git commit -m "Gestionale finale da perfezionare"
git push -u origin main

git add --all
git commit -m "Perfezionamento salvataggi e soggetto umano in dizionario"
git push -u origin main
```


</deyails>

