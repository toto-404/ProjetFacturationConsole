using System.Text;

namespace ProjetFacturationConsole.App;

public class Facture : DocumentCommercial {
    public DateTime DateEcheance { get; set; }
    public string? Statut { get; set; }

    public override void AfficherFacture() {
        Console.WriteLine(GenererTexte());
    }

    // Nommé GenererTexte pour correspondre à l'appel dans GestionFacturation
    public string GenererTexte() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("FACTURE");
        sb.AppendLine("Numéro: " + numero);
        sb.AppendLine("Date d'émission : " + dateEmission.ToString("dd/MM/yyyy"));
        sb.AppendLine("Date d'échéance: " + DateEcheance.ToString("dd/MM/yyyy"));
        sb.AppendLine("Statut: " + Statut);
        
        sb.AppendLine("Entreprise :");
        // Format détaillé selon le PDF
        sb.AppendLine(Entreprise!.Id + "- " + Entreprise.Nom + " - " + Entreprise.Email + " - " + Entreprise.Telephone + " - " + Entreprise.Adresse + " - " + Entreprise.Ville + " - " + Entreprise.CodePostal + " - " + Entreprise.Siret);
        
        sb.AppendLine("Client:");
        sb.AppendLine(Client!.Id + "- " + Client.Nom + " - " + Client.Email + " - " + Client.Telephone + " - " + Client.Adresse + " - " + Client.Ville + " - " + Client.CodePostal + " - " + Client.DateInscription.ToString("dd/MM/yyyy"));
        
        sb.AppendLine("Lignes:");
        int i = 1;
        foreach (var l in lignes) {
            sb.AppendLine(i + ". " + l.Description + " - Qté: " + l.Quantite + " PUHT: " + l.PrixUnitaireHT + " - TVA: " + l.TauxTVA + " Total HT: " + l.CalculerTotalHT() + " - Total TTC: " + l.CalculerTotalTTC());
            i++;
        }
        
        // Ajout des totaux manquants
        sb.AppendLine("Total HT: " + CalculerTotalHT());
        sb.AppendLine("Total TVA: " + CalculerTotalTVA());
        sb.AppendLine("Total TTC: " + CalculerTotalTTC());
        
        return sb.ToString();
    }
}