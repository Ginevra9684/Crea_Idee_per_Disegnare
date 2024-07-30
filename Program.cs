using Newtonsoft.Json;  
class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();

                    // Titolo
        Console.WriteLine("IDEE PER ILLUSTRAZIONI\n");

                    // Metodo per visualizzare il primo menu di scelta
        MenuPrincipale();   
    }

// FUNZIONI PER MENU E SOTTOMENU

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
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ");  // Metodo per ottenere un luogo
                break;

            case 2:
                Console.Clear();

                // Servirà un sottomenu per scegliere il soggetto

                break;

            case 3:
                Console.Clear();
                ScaricaElemento(@"luoghi.json", "Il luogo sarà: ");

                // Servirà un sottomenu per scegliere un soggetto

                break;

            default:
                MenuPrincipale();
                break;
        }
    }
// FUNZIONE PER ESTRAPOLARE GLI ELEMENTI DAI FILE JSON

    static void ScaricaElemento(string path, string messaggio)
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

                    // Viene stampato il valore dell'oggetto random
        Console.WriteLine(messaggio);
        Console.WriteLine(obj[indice].elemento);
    }
    
}