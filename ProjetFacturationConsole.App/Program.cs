namespace ProjetFacturationConsole.App;

class Program {
    static void Main(string[] args) {
        GestionFacturation gestion = new GestionFacturation();
        gestion.AfficherMenu();
    }
}
