public class Client : Personne {
    private DateTime dateInscription;
    public DateTime DateInscription { get { return dateInscription; } set { dateInscription = value; } }

    public override void AfficherInfos() {
        Console.WriteLine(id + " - " + nom + " - " + email + " - " + telephone + " - " + adresse + " - " + ville + " - " + codePostal + " - " + dateInscription.ToString("dd/MM/yyyy"));
    }
}