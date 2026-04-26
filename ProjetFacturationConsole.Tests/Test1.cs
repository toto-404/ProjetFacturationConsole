using ProjetFacturationConsole.App;

namespace ProjetFacturationConsole.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestCalculTotalHTLigne()
    {
        LigneFacture l = new LigneFacture { Quantite = 2, PrixUnitaireHT = 150 };
        Assert.AreEqual(300, l.CalculerTotalHT());
    }

    [TestMethod]
    public void TestCalculTotalTTCLigne()
    {
        LigneFacture l = new LigneFacture { Quantite = 2, PrixUnitaireHT = 150M, TauxTVA = 0.2M };
        Assert.AreEqual(360, l.CalculerTotalTTC());
    }

    [TestMethod]
    public void TestTotalTTCFacturePlusieursLignes()
    {
        Facture f = new Facture { Client = new Client(), Entreprise = new Entreprise() };
        f.AjouterLigne(new LigneFacture { Quantite = 1, PrixUnitaireHT = 100M, TauxTVA = 0.2M });
        f.AjouterLigne(new LigneFacture { Quantite = 1, PrixUnitaireHT = 50M, TauxTVA = 0.1M });
        Assert.AreEqual(175, f.CalculerTotalTTC());
    }

    [TestMethod]
    public void TestTotalHT()
    {
        Facture f = new Facture { Client = new Client(), Entreprise = new Entreprise() };
        f.AjouterLigne(new LigneFacture { Quantite = 1, PrixUnitaireHT = 100M, TauxTVA = 0.2M });
        f.AjouterLigne(new LigneFacture { Quantite = 1, PrixUnitaireHT = 50M, TauxTVA = 0.1M });
        Assert.AreEqual(150, f.CalculerTotalHT());
    }

    [TestMethod]
    public void TestTotalTTCFacture()
    {
        Facture f = new Facture { Client = new Client(), Entreprise = new Entreprise() };
        f.AjouterLigne(new LigneFacture { Quantite = 2, PrixUnitaireHT = 100M, TauxTVA = 0.2M });
        Assert.AreEqual(240, f.CalculerTotalTTC());
    }
}