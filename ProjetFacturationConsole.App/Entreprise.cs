public class Entreprise : Personne {
    private string? siret;
    public string? Siret { get { return siret; } set { siret = value; } }

    public override void AfficherInfos() {
        Console.WriteLine(id + " - " + nom + " - " + email + " - " + telephone + " - " + adresse + " - " + ville + " - " + codePostal + " - " + siret);
    }
}