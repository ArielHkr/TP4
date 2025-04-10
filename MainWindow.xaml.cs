using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Windows;

namespace TP4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// !!!! NE PAS METTRE DE CODE ICI !!!! 

    public partial class MainWindow : Window
    {

        // #############################
        // #### VARIABLES GLOBALES #####
        // Ces variables sont accessible partout dans le code arrière. 
        // Vous pouvez en ajouter au besoin.

        /// <summary>
        ///  La matrice à utiliser pour récupérer les données extraites votre fichier .CSV
        /// </summary>
        public string[,] matrice;

        public int[] ID;
        public string[] nom;
        public string[] prenom;
        public string[] DateDeNaissance;
        public string[] Pays;
        public string[] Poste;
        public string[] Adresse;
        public bool[] presence;

        /// <summary>
        /// Chemin d'accès vers le fichier CSV à lire. VOUS DEVEZ CHANGER LE NOM DE FICHIER POUR LE VOTRE
        /// </summary>
        string cheminAccesLecture = @"C:\data\420-04A-FX\TP4\TP4_Data.csv";
        public MainWindow()
        {
            // NE PAS RETIRER CE CODE
            InitializeComponent();

            // Vérifier que CmbIdentifiants contient des éléments


            if (CmbIdentifiants.SelectedIndex == -1) CmbIdentifiants.SelectedIndex = 0; // Sélection par défaut


            // Vérifier l'initialisation et la taille des tableaux


            // Note : Gardez cette section pour initialiser les variables par défaut ou les champs,
            // sans inclure les appels d'événements déclenchés par l'utilisateur.
        }


        /// <summary>
        /// Événement qui va essentiellement lancer la fonction LireCsvChargerMatrice avec le bon chemin d'accès, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChargerFichier_Click(object sender, RoutedEventArgs e)
        {

            // CODE FOURNI
            string cheminAcces = @"C:\data\420-04A-FX\TP4\TP4_Data.csv";
            // Cette fonction de départ est fourni. Voir sa documentation pour savoir comment s'en servir.
            matrice = LireCsvChargerMatrice(cheminAcces);
            //Ici j'initialise mes vecteurs globaux.
            ID = new int[matrice.GetLength(0)];
            nom = new string[matrice.GetLength(0)];
            prenom = new string[matrice.GetLength(0)];
            DateDeNaissance = new string[matrice.GetLength(0)];
            Pays = new string[matrice.GetLength(0)];
            Poste = new string[matrice.GetLength(0)];
            Adresse = new string[matrice.GetLength(0)];
            presence = new bool[matrice.GetLength(0)];

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                //Ici à l'aide de la matrice, je recupère les informations des employés. 
                ID[i] = Convert.ToInt32(matrice[i, 0]);
                nom[i] = matrice[i, 1];
                prenom[i] = matrice[i, 2];
                DateDeNaissance[i] = matrice[i, 3];
                Pays[i] = matrice[i, 4];
                Poste[i] = matrice[i, 5];
                Adresse[i] = matrice[i, 6];
                if (matrice[i, 7].ToLower() == "oui")
                {
                    presence[i] = true;
                    //Vérifier la présence d'un employé.
                }
                else
                {
                    presence[i] = false;
                }
            }

            CmbIdentifiants.Items.Clear();
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                //création des items de la liste.
                CmbIdentifiants.Items.Add(ID[i]);
            }
            CmbIdentifiants.SelectedIndex = 0;
            MessageBox.Show("Données chargées!");
        }

        // TODO
        // Suivre la séquence demandée dans l'énoncé.
        // Par défaut, le premier enregistrement doit être sélectionnée dans la liste et son contenu (toutes les propriétés) doit être affiché dans le formulaire







        //########################## VOS FONCTIONS ###############################
        //########################################################################
        //C'est à partir d'ici que vous coder votre propres fonctions, nécessaires pour votre TP.


        /// <summary>
        ///  Fonction responsable de lire le contenu d'un fichier CSV et de retirer la ligne d'entête 
        ///  afin de conserver que les lignes pertinantes (données) du fichier
        /// </summary>
        /// <param name="cheminAcces">Chemin d'accès vers le fichier CSV</param>
        /// <returns>Une matrice contenant les lignes du fichier, EXCLUANT l'entête. </returns>
        static string[,] LireCsvChargerMatrice(string cheminAcces)
        {

            // Ouvrir le fichier CSV
            StreamReader fichierEntree = new StreamReader(cheminAcces);

            // LIRE tout le fichier, INCLUANT la ligne d'entête
            string contenuFichier = fichierEntree.ReadToEnd();
            contenuFichier = contenuFichier.Replace("\r", "");
            string[] lignesFichierTemporaire = contenuFichier.Split('\n');
            string[] lignesFichier;

            // Compteur pour nombre de lignes pertinantes (au cas où il y aurait des lignes vide de trop à la suite des données)
            int nbLignesDonnees = 0;
            for (int i = 0; i < lignesFichierTemporaire.Length; i++)
            {
                if (lignesFichierTemporaire[i] != "")
                    nbLignesDonnees++;
            }

            // Taille réelle du vecteur de lignes (excluant les lignes vides)
            string[] vTemp = new string[nbLignesDonnees];

            // Refaire le vecteur de lignes (version finale) en conséquence
            for (int i = 0; i < vTemp.Length; i++)
            {
                vTemp[i] = lignesFichierTemporaire[i];
            }
            lignesFichierTemporaire = vTemp;

            // Si la dernière ligne est vide, on l'exclue dans le vecteur de lignes
            if (lignesFichierTemporaire[lignesFichierTemporaire.Length - 1] == "")
            {
                // On dimensionne le vecteur de ligne pour exclure  2 lignes (l'entête et la dernière ligne vide)
                lignesFichier = new string[lignesFichierTemporaire.Length - 2];
            }
            else
            {   // On dimensionne le vecteur de ligne pour exclure  1 ligne (l'entête)
                lignesFichier = new string[lignesFichierTemporaire.Length - 1];
            }

            // Remplir le vecteur de lignes avec les lignes du fichier, incluant l'entête.
            for (int i = 0; i < lignesFichier.Length; i++)
            {
                // [i+1] On commence à la ligne 1, car la ligne 0 est l'entête
                lignesFichier[i] = lignesFichierTemporaire[i + 1];
            }

            // Déterminer le nombre de lignes et colonnes de la matrice 
            int nbLignes = lignesFichier.Length;
            int nbColonnes = lignesFichier[0].Split(';').Length;
            string[,] uneMatrice = new string[nbLignes, nbColonnes];

            // Affectation de la matrice à partir des données de lignesFichier; 
            // Pour chaque ligne du fichier
            for (int i = 0; i < nbLignes; i++)
            {
                // Extrait les données de cette ligne dans un vecteur temporaire
                string[] vTemps = lignesFichier[i].Split(';');
                for (int j = 0; j < nbColonnes; j++)
                {
                    // Affecter les valeurs du vecteur temporaire
                    uneMatrice[i, j] = vTemps[j];
                }
            }
            fichierEntree.Close(); //Ferméture du fichier départ.
            // Retourner une matrice
            return uneMatrice;

        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            int posi = CmbIdentifiants.SelectedIndex;

            if (CmbIdentifiants.SelectedIndex == -1)
            {
                //Tests
                MessageBox.Show("Aucun indice n'a encore été choisi");
                CmbIdentifiants.SelectedIndex = 0;
            }
            if (CmbIdentifiants.SelectedIndex >= 0)
            {
                //Récupération des données changées.
                nom[CmbIdentifiants.SelectedIndex] = TxtboxNom.Text;

                prenom[CmbIdentifiants.SelectedIndex] = TxtboxPrenom.Text;

                Pays[CmbIdentifiants.SelectedIndex] = TxtBoxPays.Text;

                DateDeNaissance[CmbIdentifiants.SelectedIndex] = DatPickDateDeNaissance.Text;

                Poste[CmbIdentifiants.SelectedIndex] = TxtBoxPoste.Text;

                Adresse[CmbIdentifiants.SelectedIndex] = TxtBoxAdresse.Text;

                if (RdOui.IsChecked == true)
                {
                    presence[CmbIdentifiants.SelectedIndex] = true;
                }
                else
                {
                    presence[CmbIdentifiants.SelectedIndex] = false;
                }

                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    //Sauvegarde des informations dans la matrice.
                    matrice[i, 0] = Convert.ToString(ID[i]);
                    matrice[i, 1] = nom[i];
                    matrice[i, 2] = prenom[i];
                    matrice[i, 3] = DateDeNaissance[i];
                    matrice[i, 4] = Pays[i];
                    matrice[i, 5] = Poste[i];
                    matrice[i, 6] = Adresse[i];
                    if (presence[i])
                    {
                        matrice[i, 7] = "oui";
                    }
                    else
                    {
                        matrice[i, 7] = "non";
                    }

                }
                CmbIdentifiants.Items.Clear();
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    CmbIdentifiants.Items.Add(ID[i]);
                }

                if (CmbIdentifiants.Items.Count > 0)
                {
                    MessageBox.Show("Modification réussie!!");
                    CmbIdentifiants.SelectedIndex = posi;
                }
            }
        }

        private void BtnVider_Click(object sender, RoutedEventArgs e)
        {
            //Les champs sont vidés.
            TxtboxPrenom.Text = "";
            TxtBoxPays.Text = "";
            DatPickDateDeNaissance.Text = "";
            TxtBoxPoste.Text = "";
            TxtBoxAdresse.Text = "";

        }

        private void CmbIdentifiants_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CmbIdentifiants.SelectedItem == null)
            {
                //Pour s'assurer qu'un ID soit toujours choisi.
                CmbIdentifiants.SelectedIndex = 0;
                return;
            }
            if (CmbIdentifiants.SelectedItem != null)
            {
                TxtboxNom.Text = nom[CmbIdentifiants.SelectedIndex];
            }
            if (matrice == null)
            {
                MessageBox.Show("Chargez la matrice");
                return;
            }
            //Les vérifications d'érreur possible.
            TxtboxNom.Text = nom[CmbIdentifiants.SelectedIndex];
            TxtboxPrenom.Text = prenom[CmbIdentifiants.SelectedIndex];
            TxtBoxPays.Text = Pays[CmbIdentifiants.SelectedIndex];
            DatPickDateDeNaissance.Text = DateDeNaissance[CmbIdentifiants.SelectedIndex];
            TxtBoxPoste.Text = Poste[CmbIdentifiants.SelectedIndex];
            TxtBoxAdresse.Text = Adresse[CmbIdentifiants.SelectedIndex];
            //Affichage des informations de l'employé.
            if (presence[CmbIdentifiants.SelectedIndex])
            {
                RdOui.IsChecked = true;
                RdNon.IsChecked = false;
            }
            else
            {
                RdNon.IsChecked = true;
                RdOui.IsChecked = false;
            }
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter fichier = new StreamWriter(cheminAccesLecture, false);
            if (matrice != null)
            {
                //Mise à jour du fichier départ.
                fichier.WriteLine("ID;Nom;Prenom;Date de naissance;Pays d'origine;Poste;Adresse;Present(e)");
                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    fichier.WriteLine($"{ID[i]};{nom[i]};{prenom[i]};{DateDeNaissance[i]};{Pays[i]};{Poste[i]};{Adresse[i]};{matrice[i, 7]}");
                }
                MessageBox.Show("Enregistrement réussi");
            }
            else
            {
                MessageBox.Show("Chargez d'abord les données");
            }
            fichier.Close();
        }

        private void BtnPointage_Click(object sender, RoutedEventArgs e)
        {
            if (CmbIdentifiants.SelectedIndex != -1 && CmbIdentifiants.SelectedIndex >= 0)
            {

                if (presence[CmbIdentifiants.SelectedIndex])
                {
                    //Tester la présence de l'employé. ENsuite envoyer un signal.
                    matrice[CmbIdentifiants.SelectedIndex, 7] = "non";
                    RdNon.IsChecked = true;
                    ImgPresent.Visibility = Visibility.Hidden;
                    ImgAbsent.Visibility = Visibility.Visible;
                    presence[CmbIdentifiants.SelectedIndex] = false;
                    matrice[CmbIdentifiants.SelectedIndex, 7] = "non";
                }
                else
                {
                    RdOui.IsChecked = true;
                    ImgPresent.Visibility = Visibility.Visible;
                    ImgAbsent.Visibility = Visibility.Hidden;
                    presence[CmbIdentifiants.SelectedIndex] = true;
                    matrice[CmbIdentifiants.SelectedIndex, 7] = "oui";

                }
                if (presence[CmbIdentifiants.SelectedIndex])
                {
                    MessageBox.Show("Entré(e).");
                    //Après la vérification.
                }
                else
                {
                    MessageBox.Show("Parti(e)");
                }
            }
            else
            {
                MessageBox.Show("Chargez les données");
            }
        }

        private void BtnRechercher_Click(object sender, RoutedEventArgs e)
        {
            //Tester le contenu de la matrice.
            if (matrice != null)
            {
                bool absent = true;
                string element = Interaction.InputBox("Entrez le nom à rechercher", "Entrer un nom:");

                for (int i = 0; i < matrice.GetLength(0); i++)
                {
                    //recherche dans le vecteur des noms.
                    if (element == nom[i])
                    {
                        MessageBox.Show("L'employé existe!");
                        CmbIdentifiants.SelectedIndex = i;
                        absent = false;
                    }
                }
                //Vérifications anti-bugs
                if (element == "") return;
                if (absent)
                {
                    MessageBox.Show("N'existe pas dans la compagnie");
                }
            }
            else
            {
                MessageBox.Show("Veuillez charger les données");
            }

        }
    }
}
