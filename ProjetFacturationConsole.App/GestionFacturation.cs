using System.Text.Json;
public class GestionFacturation {
    private List<Client> clients = new List<Client>();
    private List<Entreprise> entreprises = new List<Entreprise>();
    private Dictionary<int, Client> dictionnaireClients = new Dictionary<int, Client>();
    private Dictionary<int, Entreprise> dictionnaireEntreprises = new Dictionary<int, Entreprise>();

    public void AfficherMenu() {
        int choix = -1;
        while (choix != 0) {
            Console.WriteLine("\n1- Import Clients CSV\n2- Import Entreprises CSV\n3- Afficher Clients\n4- Afficher Entreprises\n5- Créer Facture\n6- Carnet Contacts\n0- Quitter");
            choix = int.Parse(Console.ReadLine());
            try {
                if (choix == 1) ImporterClientsDepuisCsv();
                else if (choix == 3) AfficherClients();
                else if (choix == 5) CreerFacture();
                else if (choix == 6) AfficherCarnetContacts();
            } catch (Exception ex) { Console.WriteLine("Erreur : " + ex.Message); }
        }
    }

    //imports
    public void ImporterClientsDepuisCsv() {
        string[] lignes = File.ReadAllLines("clients.csv");
        for (int i = 1; i < lignes.Length; i++) {
            string[] colonnes = lignes[i].Split(';');
            Client c = new Client { Id = int.Parse(colonnes[0]), Nom = colonnes[1], Email = colonnes[2], Telephone = colonnes[3], Adresse = colonnes[4], Ville = colonnes[5], CodePostal = colonnes[6], DateInscription = DateTime.Parse(colonnes[7]) };
            if (c.DateInscription > DateTime.Now) throw new Exception("Date invalide");
            clients.Add(c);
            dictionnaireClients[c.Id] = c;
        }
        File.WriteAllText("clients.json", JsonSerializer.Serialize(clients));
    }

    public void ChargerClientsDepuisJson() {
        if (File.Exists("clients.json")) {
            string json = File.ReadAllText("clients.json");
            clients = JsonSerializer.Deserialize<List<Client>>(json);
            foreach (var c in clients) { dictionnaireClients[c.Id] = c; }
        }
    }

    public void AfficherClients() {
        if (clients.Count == 0) ChargerClientsDepuisJson();
        foreach (var c in clients) { Console.WriteLine(c.Id + " - " + c.Nom); }
    }

    public void CreerFacture() {
        AfficherClients();
        Console.Write("ID Client : ");
        int cid = int.Parse(Console.ReadLine());
        // (Logique identique pour Entreprise simplifiée ici)
        Facture f = new Facture { 
            Client = dictionnaireClients[cid], 
            Entreprise = new Entreprise { Nom = "StartDev" }, // Exemple simplifié
            Numero = "F2026-001", DateEmission = DateTime.Now, DateEcheance = DateTime.Now.AddDays(30), Statut = "Brouillon" 
        };
        // Saisie des lignes...
        f.AjouterLigne(new LigneFacture { Description = "Test", Quantite = 2, PrixUnitaireHT = 50, TauxTVA = 20 });
        f.AjouterLigne(new LigneFacture { Description = "Service", Quantite = 1, PrixUnitaireHT = 100, TauxTVA = 20 });
        f.AfficherFacture();
    }

    public void AfficherCarnetContacts() {
        List<Personne> contacts = new List<Personne>();
        foreach (var c in clients) contacts.Add(c);
        foreach (var e in entreprises) contacts.Add(e);
        foreach (var p in contacts) p.AfficherInfos();
    }
}