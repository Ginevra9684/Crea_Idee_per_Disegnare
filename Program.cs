using Newtonsoft.Json;  

using Spectre.Console;
class Program
{
    public static void Main(string[] args)
    {
        string scelta;  //
        //--------------//
        AnsiConsole.Clear();

        AnsiConsole.Markup("[50]IDEE PER ILLUSTRAZIONI[/]:artist_palette:\n\n");

        Avvertimenti();
        Proseguimento();

        MenuPrincipale(); 

        Console.WriteLine("Vuoi un tema di riferimento? (s/n)");

        scelta = Console.ReadKey(true).KeyChar.ToString()!.ToLower();

        if (scelta == "s")
        {
            ScaricaElemento(@"temi.json", "Il tema sarà : ", 1); 
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

        Conclusione();  
    }

// METODI PER LA FLUIDITÀ DEL PROGRAMMA---------------------------------------------------------------------------------------------

    static void Avvertimenti()
    {
        AnsiConsole.Markup("[204]REGOLE ED AVVERTIMENTI[/]\n");
        AnsiConsole.Markup("[208]1.[/]I nomi di animali, creature e temi saranno scritti in inglese per convenzione[208].[/]:anger_symbol:\n");
        AnsiConsole.Markup("[208]2.[/]Se si fa un inserimento sbagliato l'opzione darà errore o/e verrà saltata[208].[/]:cross_mark:\n");
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
                AnsiConsole.Clear();
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1); 
                Proseguimento();
                break;

            case "[85]2.[/]Soggetto[85].[/]":
                AnsiConsole.Clear();
                PreferenzaSoggetto();
                break;

            case "[49]3.[/]Ambiente e Soggetto[49].[/]":
                AnsiConsole.Clear();
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ", 1);
                Proseguimento();
                PreferenzaSoggetto();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
static void PreferenzaSoggetto()
    {
        string input;  //
    //-----------------//

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

            case "[85]2.[/]Animale[85].[/]":
                AnsiConsole.Clear();
                ScaricaElemento(@"animali.json", "L'animale sarà: ", 1);
                Proseguimento();
                break;

            case "[49]3.[/]Creatura[49].[/]":
                AnsiConsole.Clear();
                TipoCreatura();
                break;

            case "[79]4.[/]Nessuna preferenza[79].[/]":
                Proseguimento();
                QuantitativoSoggetti();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void TipoCreatura()
    {
        string input;                //
        string quantitativoAnimali;  //
    //-------------------------------//

        input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("<-<-<-[50]PREFERENZA CREATURA[/]->->->")
            .PageSize(3)
            .MoreChoicesText("Spostati con le frecce direzionali.")
            .AddChoices(new[] {"[86]1.[/]Creatura Mitologica[86].[/]","[85]2.[/]Propria Invenzione[85].[/]"  
            }));

        switch (input)
        {
            case "[86]1.[/]Creatura Mitologica[86].[/]":
                AnsiConsole.Clear();
                ScaricaElemento(@"creature.json", "La creatura sarà: ", 1);
                Proseguimento();
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
                    case "[86].2[/]":
                        AnsiConsole.Clear();
                        ScaricaElemento(@"animali.json", "Gli animali saranno: ", 2);
                        break;
                    case "[85].3[/]":
                        AnsiConsole.Clear();
                        ScaricaElemento(@"animali.json", "Gli animali saranno: ", 3);
                        break;
                    case "[49].4[/]":
                        AnsiConsole.Clear();
                        ScaricaElemento(@"animali.json", "Gli animali saranno: ", 4);
                        break;
                    case "[79].5[/]":
                        AnsiConsole.Clear();
                        ScaricaElemento(@"animali.json", "Gli animali saranno: ", 5);
                        break;
                }
                Proseguimento();
                break;
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------
    static void QuantitativoSoggetti()
    {
        string quantitativoSoggetti; //
    //-------------------------------//

        quantitativoSoggetti = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("<-<-<-[50]QUANTITÀ SOGGETTI[/]->->->")
                .PageSize(3)
                .MoreChoicesText("Spostati con le frecce direzionali.")
                .AddChoices(new[] {"[86]1.[/]Soggetto Unico[86].[/]","[85]2.[/]Doppio Soggetto" 
                }));

        switch(quantitativoSoggetti)
        {
            case "[86]1.[/]Soggetto Unico[86].[/]" :
                AnsiConsole.Clear();
                SoggettoCasuale();
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

        string json = File.ReadAllText(path);
        dynamic obj = JsonConvert.DeserializeObject(json)!;

        indice = random.Next(0,obj.Count);

        AnsiConsole.Markup($":backhand_index_pointing_right: {messaggio} [154]:[/]\n\n");;

        for (int i = 1; i <= quantitativoElementi ;i++ ) 
            {
                indice = random.Next(0, obj.Count);
                AnsiConsole.Markup($"[154]-[/] {obj[indice].elemento}[154].[/]\n");
            }
    }
}