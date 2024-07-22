using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnagrammeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string[] mots;
        private string motCible;
        private int essaisMax = 3;
        private int essaisRestants;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            mots = new string[]
          {
             "TABLE","CHIEN","MANGER","SOLEIL","VOITURE","POMME","MAISON","ARBRES","CAFE","ORDINATEUR"
          };
            NouvellePartie(); // Démarre une nouvelle partie au chargement de la fenêtre
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string motPropose = TextBoxProposition.Text; // Récupérer le mot proposé dans le TextBox

            if (motPropose.ToUpper() == motCible) // Vérifier si le mot proposé est correct
            {
                MotCorrect();
            }
            else
            {
                MotIncorrect();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NouvellePartie();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private string Melanger(string chaine)
        {
            // Méthode pour mélanger une chaîne de caractères
            string chaineMelangee = new string(chaine.OrderBy(c => random.Next()).ToArray());
            return chaineMelangee;
        }

        private void NouvellePartie()
        {
            // Réinitialiser le nombre d'essais restants
            essaisRestants = essaisMax;

            // Générer un nouveau mot aléatoire
            motCible = mots[random.Next(0, mots.Length)];

            // Mélanger les lettres du mot cible
            string motMelange = Melanger(motCible);

            // Afficher le mot mélangé dans l'interface utilisateur
            LabelMot.Content = motMelange;

            // Mettre à jour l'affichage du nombre d'essais restants dans l'interface utilisateur
            LabelEssaisRestants.Content = $"Essais restants : {essaisRestants}";
        }

        private void MotCorrect()
        {
            MessageBoxResult resultat = MessageBox.Show("Félicitations ! Vous avez trouvé le mot ! Voulez-vous rejouer ?", "Partie gagnée", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultat == MessageBoxResult.Yes)
            {
                // Convertir le mot proposé en majuscules
                string motPropose = TextBoxProposition.Text.ToUpper();

                // Ajouter le mot proposé à la ListBox des mots proposés
                ListBoxMotsProposes.Items.Add(motPropose);

                // Ajouter l'information à la ListBox de l'historique des parties
                int coupsUtilises = essaisMax - essaisRestants + 1;
                string resultatPartie = $"Partie {ListBoxHistoriqueParties.Items.Count + 1} - Mot à trouver : {motCible} - Gagné en {coupsUtilises} coup(s)";
                ListBoxHistoriqueParties.Items.Add(resultatPartie);

                NouvellePartie();
            }
            else
            {
                int coupsUtilises = essaisMax - essaisRestants + 1;
                string resultatPartie = $"Partie {ListBoxHistoriqueParties.Items.Count + 1} - Mot à trouver : {motCible} - Gagné en {coupsUtilises} coup(s)";
                ListBoxHistoriqueParties.Items.Add(resultatPartie);
                // Convertir le mot proposé en majuscules
                string motPropose = TextBoxProposition.Text.ToUpper();

                // Ajouter le mot proposé à la ListBox des mots proposés
                ListBoxMotsProposes.Items.Add(motPropose);
            }
        }

        private void MotIncorrect()
        {
            essaisRestants--;

            if (essaisRestants == 0)
            {
                MessageBox.Show($"Désolé, vous avez épuisé tous vos essais. Le mot à trouver était : {motCible}. Voulez-vous rejouer ?", "Partie perdue", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Convertir le mot proposé en majuscules
                string motPropose = TextBoxProposition.Text.ToUpper();

                // Ajouter le mot proposé à la ListBox des mots proposés
                ListBoxMotsProposes.Items.Add(motPropose);

                // Ajouter l'information à la ListBox de l'historique des parties
                string resultatPartie = $"Partie {ListBoxHistoriqueParties.Items.Count + 1} - Mot à trouver : {motCible} - Perdu";
                ListBoxHistoriqueParties.Items.Add(resultatPartie);

                NouvellePartie();
            }
            else
            {
                // Mettre à jour l'affichage du nombre d'essais restants dans l'interface utilisateur
                LabelEssaisRestants.Content = $"Essais restants : {essaisRestants}";
            }
        }





    }

}

