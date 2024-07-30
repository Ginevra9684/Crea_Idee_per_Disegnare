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

- [ ] Spectre.Console
- [ ] Aggiunta caratteristiche dei luoghi (es meteo, orario);
- [ ] Menu gestionale
- [ ] Stampa tabella finale con tutte le scelte
- [ ] Inserire più elementi nei vari file json
- [ ] Inserire una lista esaustiva nel file animali
- [ ] Opzione lingua inglese
- [ ] Opzione lingua francese
- [ ] Opzione altre lingue
- [ ] Aggiunta opzioni come caratteristiche e personalità dei personaggi, materiali degli oggetti
- [ ] Link a siti esterni
- [ ] visualizzazione foto di esempio
- [ ] HTML e CSS


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

    <details>
    <summary>Commits implementazioni</summary>

    ```bash

    ```


    </deyails>

