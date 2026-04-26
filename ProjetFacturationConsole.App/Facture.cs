using System.Text;

public class Facture : DocumentCommercial {
    public DateTime DateEcheance { get; set; }
    public string Statut { get; set; }

    public override void AfficherFacture() {
        Console.WriteLine(ConstruireTexte());
    }

    public string ConstruireTexte() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("FACTURE");
        sb.AppendLine("Numéro: " + numero);
        sb.AppendLine("Date d'émission : " + dateEmission.ToString("dd/MM/yyyy"));
        sb.AppendLine("Date d'échéance: " + DateEcheance.ToString("dd/MM/yyyy"));
        sb.AppendLine("Statut: " + Statut);
        sb.AppendLine("Entreprise :");
        sb.AppendLine(Entreprise.Id + " - " + Entreprise.Nom + " - " + Entreprise.Email);
        sb.AppendLine("Client :");
        sb.AppendLine(Client.Id + " - " + Client.Nom + " - " + Client.Email);
        sb.AppendLine("Lignes :");
        int i = 1;
        foreach (var l in lignes) {
            sb.AppendLine(i + ". " + l.Description + " - Qté: " + l.Quantite + " PUHT: " + l.PrixUnitaireHT + " Total TTC: " + l.CalculerTotalTTC());
            i++;
        }
        sb.AppendLine("Total TTC: " + CalculerTotalTTC());
        return sb.ToString();
    }
}