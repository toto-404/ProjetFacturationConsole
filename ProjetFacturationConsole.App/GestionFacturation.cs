using System.Text.Json;

namespace ProjetFacturationConsole.App;

public class GestionFacturation
{
    private List<Client> clients = new List<Client>();
    private List<Entreprise> entreprises = new List<Entreprise>();
    private Dictionary<int, Client> dictionnaireClients = new Dictionary<int, Client>();
    private Dictionary<int, Entreprise> dictionnaireEntreprises = new Dictionary<int, Entreprise>();

    public void AfficherMenu()
    {
        int choix = -1;
        while (choix != 0)
        {
            Console.WriteLine("\n--- MENU STARTDEV ---");
            Console.WriteLine("1- Importer les clients depuis le CSV");
            Console.WriteLine("2- Importer les entreprises depuis le CSV");
            Console.WriteLine("3- Afficher les clients");
            Console.WriteLine("4- Afficher les entreprises");
            Console.WriteLine("5- Créer une facture");
            Console.WriteLine("6- Afficher le carnet de contacts");
            Console.WriteLine("0 - Quitter");
            
            choix = int.Parse(Console.ReadLine() ?? "0");

            try {
                if (choix == 1) ImporterClientsDepuisCsv();
                else if (choix == 2) ImporterEntreprisesDepuisCsv();
                else if (choix == 3) AfficherClients();
                else if (choix == 4) AfficherEntreprises();
                else if (choix == 5) CreerFacture();
                else if (choix == 6) AfficherCarnetContacts();
            }
            catch (Exception ex) { Console.WriteLine("ERREUR : " + ex.Message); }
        }
    }

    public void ImporterClientsDepuisCsv()
    {
        string[] lignes = File.ReadAllLines("clients.csv");
        for (int i = 1; i < lignes.Length; i++)
        {
            string[] c = lignes[i].Split(';');
            Client cli = new Client { Id = int.Parse(c[0]), Nom = c[1], Email = c[2], Telephone = c[3], Adresse = c[4], Ville = c[5], CodePostal = c[6], DateInscription = DateTime.Parse(c[7]) };
            if (cli.DateInscription > DateTime.Now) throw new Exception("Date d'inscription future interdite.");
            clients.Add(cli);
            dictionnaireClients[cli.Id] = cli;
        }
        File.WriteAllText("clients.json", JsonSerializer.Serialize(clients));
    }

    public void ImporterEntreprisesDepuisCsv()
    {
        string[] lignes = File.ReadAllLines("entreprises.csv");
        for (int i = 1; i < lignes.Length; i++)
        {
            string[] c = lignes[i].Split(';');
            Entreprise ent = new Entreprise { Id = int.Parse(c[0]), Nom = c[1], Email = c[2], Telephone = c[3], Adresse = c[4], Ville = c[5], CodePostal = c[6], Siret = c[7] };
            entreprises.Add(ent);
            dictionnaireEntreprises[ent.Id] = ent;
        }
        File.WriteAllText("entreprises.json", JsonSerializer.Serialize(entreprises));
    }

    public void ChargerClientsDepuisJson()
    {
        if (File.Exists("clients.json")) {
            var deserializedClients = JsonSerializer.Deserialize<List<Client>>(File.ReadAllText("clients.json"));
            if (deserializedClients != null) {
                clients = deserializedClients;
                foreach (Client c in clients) { dictionnaireClients[c.Id] = c; }
            }
        }
    }

    public void ChargerEntreprisesDepuisJson()
    {
        if (File.Exists("entreprises.json")) {
            var deserializedEntreprises = JsonSerializer.Deserialize<List<Entreprise>>(File.ReadAllText("entreprises.json"));
            if (deserializedEntreprises != null) {
                entreprises = deserializedEntreprises;
                foreach (Entreprise e in entreprises) { dictionnaireEntreprises[e.Id] = e; }
            }
        }
    }

    public void AfficherClients()
    {
        if (clients.Count == 0) ChargerClientsDepuisJson();
        foreach (Client c in clients) { Console.WriteLine(c.Id + " - " + c.Nom); }
    }

    public void AfficherEntreprises()
    {
        if (entreprises.Count == 0) ChargerEntreprisesDepuisJson();
        foreach (Entreprise e in entreprises) { Console.WriteLine(e.Id + " - " + e.Nom); }
    }

    public void CreerFacture()
    {
        if (clients.Count == 0) ChargerClientsDepuisJson();
        if (entreprises.Count == 0) ChargerEntreprisesDepuisJson();

        AfficherEntreprises();
        Console.Write("ID Entreprise : ");
        int eid = int.Parse(Console.ReadLine() ?? "0");
        Entreprise ent = dictionnaireEntreprises[eid];

        AfficherClients();
        Console.Write("ID Client : ");
        int cid = int.Parse(Console.ReadLine() ?? "0");
        Client cli = dictionnaireClients[cid];

        Console.Write("Date émission (dd/mm/yyyy) : ");
        DateTime emission = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

        Facture f = new Facture { 
            Entreprise = ent, Client = cli, Numero = "F" + DateTime.Now.Ticks, 
            DateEmission = emission, DateEcheance = emission.AddDays(30), Statut = "Brouillon" 
        };

        string encore = "oui";
        while (encore == "oui")
        {
            Console.Write("Description : "); string? d = Console.ReadLine();
            Console.Write("Quantité : "); int q = int.Parse(Console.ReadLine() ?? "0");
            if (q <= 0) throw new Exception("Quantité invalide");
            Console.Write("Prix HT : "); decimal p = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("TVA : "); decimal tva = decimal.Parse(Console.ReadLine() ?? "0");
            
            f.AjouterLigne(new LigneFacture { Description = d, Quantite = q, PrixUnitaireHT = p, TauxTVA = tva });
            
            Console.Write("Autre ligne ? (oui/non) : ");
            encore = (Console.ReadLine() ?? "").ToLower();
        }

        if (f.Lignes.Count < 2) throw new Exception("Il faut au moins 2 lignes.");
        f.AfficherFacture();
        
        Console.Write("Confirmer la génération du fichier texte ? (oui/non) : ");
        if ((Console.ReadLine() ?? "").ToLower() == "oui") GenererFichierTexteFacture(f);
    }

    public void GenererFichierTexteFacture(Facture f)
    {
        File.WriteAllText("facture_" + f.Numero + ".txt", f.GenererTexte());
    }

    public void AfficherCarnetContacts()
    {
        if (clients.Count == 0) ChargerClientsDepuisJson();
        if (entreprises.Count == 0) ChargerEntreprisesDepuisJson();
        
        List<Personne> carnet = new List<Personne>();
        foreach (Client c in clients) { carnet.Add(c); }
        foreach (Entreprise e in entreprises) { carnet.Add(e); }
        
        foreach (Personne p in carnet) { p.AfficherInfos(); }
    }
}