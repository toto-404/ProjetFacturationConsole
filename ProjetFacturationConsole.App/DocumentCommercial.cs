public abstract class DocumentCommercial {
    protected string? numero;
    protected DateTime dateEmission;
    protected Client? client;
    protected Entreprise? entreprise;
    protected List<LigneFacture> lignes = new List<LigneFacture>();

    public string? Numero { get { return numero; } set { numero = value; } }
    public DateTime DateEmission { get { return dateEmission; } set { dateEmission = value; } }
    public Client? Client { get { return client; } set { client = value; } }
    public Entreprise? Entreprise { get { return entreprise; } set { entreprise = value; } }
    public List<LigneFacture> Lignes { get { return lignes; } }

    public void AjouterLigne(LigneFacture ligne) {
        lignes.Add(ligne);
    }

    public decimal CalculerTotalHT() {
        decimal total = 0;
        foreach (var l in lignes) { total += l.CalculerTotalHT(); } //using vars to let the smart compiler choose :)
        return total;
    }

    public decimal CalculerTotalTVA() {
        decimal total = 0;
        foreach (var l in lignes) { total += l.CalculerMontantTVA(); }
        return total;
    }

    public decimal CalculerTotalTTC() {
        decimal total = 0;
        foreach (var l in lignes) { total += l.CalculerTotalTTC(); }
        return total;
    }

    public abstract void AfficherFacture();
}