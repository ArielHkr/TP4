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

        /// <summary>
        ///  La matrice à utiliser pour récupérer les données extraites votre fichier .CSV
        /// </summary>
        public string[,] matrice;


        /// <summary>
        /// Chemin d'accès vers le fichier CSV à lire.
        /// </summary>
        string cheminAccesLecture = @"C:\data\420-04A-FX\TP4\TP4_SOLUTION.csv";

        public MainWindow()
        {
            // NE PAS RETIRER CE CODE
            InitializeComponent();

            // Ici, c'est comme votre programme "Main" : vous pouvez coder essentiellement 
            // l'initialisation des vos variables, des champs par défaut de formulaire (si le cas),
            // Attention, vous n'avez pas à coder les appels des fonctions événements de l'interface graphique 
            // car celles-ci sont lancées par l'interration de l'utilisateur avec le programme (ex. click, sélection, etc)

        }

        /// <summary>
        /// Événement qui va essentiellement lancer la fonction LireCsvChargerMatrice avec le bon chemin d'accès, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChargerFichier_Click(object sender, RoutedEventArgs e)
        {
            // CODE FOURNI
            string cheminAcces = @"C:\data\420-04A-FX\TP4\TP4_exemple.csv";
            // La fonction LireCsvChargerMatrice doit être complétée par vous.
            matrice = LireCsvChargerMatrice(cheminAcces);

            // Charger la liste déroulante de votre formulaire. Cette liste doit contenir les noms de vos entités
            // TODO

            // Par défaut, la première entitée doit être sélectionnée dans la liste et son contenu doit être affiché dans le formulaire
            // TODO

        }

        #region VOS FONCTIONS 

        //########################## VOS FONCTIONS ###############################
        //########################################################################
        //C'est à partir d'ici que vous coder votre propres fonctions, nécessaires pour votre TP.


        /// <summary>
        ///  Fonction responsable de lire le contenu d'un fichier CSV, de retirer la dernière ligne vide et de retourner
        ///  une matrice de strings où chaque case représente une ligne du fichier, EXCLUANT l'entête.
        /// </summary>
        /// <param name="cheminAcces">Chemin d'accès vers le fichier CSV</param>
        /// <returns>Une matrice contenant les lignes du fichier, EXCLUANT l'entête. </returns>
        static string[,] LireCsvChargerMatrice(string cheminAcces)
        {

            #region CODE FOURI
            // Ouvrir le fichier CSV
            StreamReader fichierEntree = new StreamReader(cheminAcces);

            // LIRE tout le fichier, INCLUANT la ligne d'entête
            string contenuFichier = fichierEntree.ReadToEnd();
            contenuFichier = contenuFichier.Replace("\r", "");
            string[] lignesFichierTemporaire = contenuFichier.Split('\n');
            string[] lignesFichier;

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
                lignesFichier[i] = lignesFichierTemporaire[i+1];
            }
            #endregion



            #region À COMPLÉTER

            /* ##### COMPLÉTER LE RESTE DE LA FONCTION ###### */
            /* ############################################## */

            // Trouver le nombre de lignes et de colonnes du fichier, afin de préparer
            // la construction de la matrice de strings
            // TODO

            // Création de la matrice : à modifier avec la bonne dimension
            // TODO
            string[,] uneMatrice = new string[0, 0];

            // Affectation de la matrice à partir des données de lignesFichier; 
            // TODO

            #endregion

            // Retourner une matrice
            return uneMatrice;



        }

        #endregion
    }
}
