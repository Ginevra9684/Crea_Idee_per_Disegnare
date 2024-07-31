using Newtonsoft.Json;  

using Spectre.Console;
class Program
{
                // Contenitore per gli elementi selezionati
    static Dictionary <string, List<string>> tabellaElementi = new Dictionary <string, List<string>>();

                //  Buleano per far ripartire un nuovo progetto finchè non si setta a falso
    static bool progettando = true;
    public static void Main(string[] args)
    {
        do 
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup("[50]IDEE PER ILLUSTRAZIONI[/]:artist_palette:\n\n");

            Avvertimenti();
            MenuPrincipale(); 
            ScelteSecondarie();
            MenuFinale(); 
            Console.CursorVisible = true;
        }
        while (progettando);
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
                ScaricaElemento(@"caricamenti/luoghi.json", "Il luogo sarà: ", 1, "luogo"); 
                CaratteristicheLuogo();
                break;
            case "[85]2.[/]Soggetto[85].[/]":   PreferenzaSoggetto();    
                break;
            case "[49]3.[/]Ambiente e Soggetto[49].[/]":
                ScaricaElemento(@"caricamenti/luoghi.json", "Il luogo sarà: ", 1, "luogo");
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

        if (caratteristiche.Contains("[86]1.[/] Meteo[86].[/]"))    ScaricaElemento(@"caricamenti/meteo.json", "Il meteo sarà: ", 1, "meteo");
        if (caratteristiche.Contains("[85]2.[/] Momento[85].[/]"))    ScaricaElemento(@"caricamenti/momenti.json", "Il momento sarà: ", 1, "momento giornata");
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
            case "[85]2.[/]Animale[85].[/]":    ScaricaElemento(@"caricamenti/animali.json", "L'animale sarà: ", 1, "animale");
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
            case "[86]1.[/]Creatura Mitologica[86].[/]":    ScaricaElemento(@"caricamenti/creature.json", "La creatura sarà: ", 1, "creatura mitologica");
                break;
            case "[85]2.[/]Propria Invenzione[85].[/]":
                AnsiConsole.Clear();
                
                quantitativoAnimali = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("<-<-<-[50]QUANTI ANIMALI PER LA CREAZIONE?[/]->->->")
                    .PageSize(3)
                    .MoreChoicesText("Spostati con le frecce direzionali.")
                    .AddChoices(new[] {"2","3","4","5"  
                    }));
                
                ScaricaElemento(@"caricamenti/animali.json", "Gli animali saranno: ", int.Parse(quantitativoAnimali), "creatura composta da");
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
            case 2:    ScaricaElemento(@"caricamenti/animali.json", "L'animale sarà: ", 1, "animale");
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
        
        if (altreOpzioni.Contains("[86]1.[/] Tema[86].[/]"))    ScaricaElemento(@"caricamenti/temi.json", "Il tema sarà : ", 1, "tema"); 
        if (altreOpzioni.Contains("[85]2.[/] Tecnica[85].[/]"))    ScaricaElemento(@"caricamenti/tecniche.json", "La tecnica sarà : ", 1, "tecnica");
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void MenuFinale()
    {
        string input;  //
    //-----------------//

        input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("<-<-<-[50]GESTIONALE[/]->->->")
            .PageSize(4)
            .MoreChoicesText("Spostati con le frecce direzionali.")
            .AddChoices(new[] {"[86]1.[/] tabellaElementi Dati [86].[/]","[85]2.[/] Progetti [85].[/]","[49]3.[/] Nuovo Progetto [49].[/]","[79]4.[/] Esci [79].[/]"   // Quattro opzioni
            }));

        switch (input)
        {               // Stampa tutte le chiavi e i valori del dizionario
            case "[86]1.[/] tabellaElementi Dati [86].[/]":

                foreach (var elemento in tabellaElementi)
                {
                    Console.WriteLine($"{elemento.Key} : {string.Join(", ", elemento.Value)}");
                }
                Proseguimento();
                MenuFinale();
                break;

            case "[85]2.[/] Progetti [85].[/]":

                            //NAVIGAZIONE ALL'INTERNO DELLA CARTELLA "PROGETTI"
                            //POSSIBILITà DI SELEZIONARE UN PROGETTO SPECIFICO
                            //IL PROGETTO DEVE POTER MOSTRARE IL CONTENUTO IN TABELLA E POTER ELIMINARE IL PROGETTO STESSO



                break;
            case "[49]3.[/] Nuovo Progetto [49].[/]":
                input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("<-<-<-[50]GESTIONALE[/]->->->")
                        .PageSize(4)
                        .MoreChoicesText("Spostati con le frecce direzionali.")
                        .AddChoices(new[] {"[86]1.[/] Salva progetto corrente [86].[/]","[85]2.[/] Scarta progetto corrente [85].[/]"
                        }));
                switch (input)
                {
                    case "[86]1.[/] Salva progetto corrente [86].[/]" :
                                    // Inserisce gli elementi del dizionario in un file json
                        CreaProgetto();
                        tabellaElementi.Clear();
                        break;
                    case "[85]2.[/] Scarta progetto corrente [85].[/]" :    tabellaElementi.Clear();
                        break;
                }
                break;
            case "[79]4.[/] Esci [79].[/]":
                input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("<-<-<-[50]GESTIONALE[/]->->->")
                    .PageSize(4)
                    .MoreChoicesText("Spostati con le frecce direzionali.")
                    .AddChoices(new[] {"[86]1.[/] Salva progetto corrente [86].[/]","[85]2.[/] Scarta progetto corrente [85].[/]"
                    }));
                switch (input)
                {
                    case "[86]1.[/] Salva progetto corrente [86].[/]" :
                        CreaProgetto();
                        Conclusione();
                        progettando = false;
                        break;
                    case "[85]2.[/] Scarta progetto corrente [85].[/]" :    
                        Conclusione();
                        progettando = false;
                        break;
                }
                break;
        }
    }

// FUNZIONE PER ESTRAPOLARE GLI ELEMENTI DAI FILE JSON--------------------------------------------------------------------------------

    static void ScaricaElemento(string path, string messaggio, int quantitativoElementi, string chiavePerDizionario)
    {
        Random random = new Random(); //
        int indice;                   //
        string valorePerDizionario;   //  
    //--------------------------------//

        AnsiConsole.Clear();
        string json = File.ReadAllText(path);
        dynamic obj = JsonConvert.DeserializeObject(json)!;

        indice = random.Next(0,obj.Count);

        AnsiConsole.Markup($":backhand_index_pointing_right: {messaggio} [154]:[/]\n\n");;

        for (int i = 1; i <= quantitativoElementi ;i++ ) 
        {
            indice = random.Next(0, obj.Count);
            valorePerDizionario = obj[indice].elemento;
            AnsiConsole.Markup($"[154]-[/] {obj[indice].elemento}[154].[/]\n");
            CaricaDizionario( chiavePerDizionario, valorePerDizionario);
        }
        Proseguimento();
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void CaricaDizionario(string chiave, string valore)
    {
        if(tabellaElementi.ContainsKey(chiave)) tabellaElementi[chiave].Add(valore);
        else tabellaElementi [chiave] = new List<string> {valore};
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void CreaProgetto()
    {
        string path;          //
        DateTime giorno;      //
        string oggi;          //
    //------------------------//
                    // Denomina un file json con data e ora corrente
        giorno = DateTime.Now;
        oggi = giorno.ToString("dd-MM-yyyy-HHHH-mm-ss");
        path = (@"progetti/"+ oggi + ".json");
                    // Crea il file json 
        File.Create(path).Close();
        string jsonString = JsonConvert.SerializeObject(tabellaElementi, Formatting.Indented);
        File.AppendAllText(path, jsonString + ",\n"); // Aggiunge , anche a fine file

        string file = File.ReadAllText(path);
                    // Toglie , a fine file per evitare l'errore
        file = file.Remove(file.Length - 2, 1); 
        File.WriteAllText(path,file);

        AnsiConsole.Markup("[147]Il progetto è stato creato correttamente :red_exclamation_mark::red_exclamation_mark::file_cabinet:[/]");

        Proseguimento();
    }
}