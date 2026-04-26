public abstract class Personne {
    protected int id;
    protected string? nom;
    protected string? email;
    protected string? telephone;
    protected string? adresse;
    protected string? ville;
    protected string? codePostal;

    public int Id { get { return id; } set { id = value; } }
    public string? Nom { get { return nom; } set { nom = value; } }
    public string? Email { get { return email; } set { email = value; } }
    public string? Telephone { get { return telephone; } set { telephone = value; } }
    public string? Adresse { get { return adresse; } set { adresse = value; } }
    public string? Ville { get { return ville; } set { ville = value; } }
    public string? CodePostal { get { return codePostal; } set { codePostal = value; } }

    public abstract void AfficherInfos();
}