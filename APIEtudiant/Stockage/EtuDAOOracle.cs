using APIEtudiant.Model;
using APIEtudiant.Model.Enumerations;
using Oracle.ManagedDataAccess.Client;

namespace APIEtudiant.Stockage
{
    public class EtudiantDAOOracle : IEtuDAO
    {

        /// <summary>
        /// Renvoi tout les étudiants de la BDD Oracle de al promo
        /// </summary>
        /// <param name="promo">Promo dont on veut le etudiants</param>
        /// <returns>la liste des étudiants de la BDD Oracle</returns>
        /// <author>Nordine</author>
        public IEnumerable<Etudiant> GetAllEtu(Promotion promo)
        {
            //Création d'une connexion Oracle
            Connection con = new Connection();
            //Liste d'étudiant à renvoyer
            List<Etudiant> etudiants = new List<Etudiant>();

            int idPromo = GetIdPromotion(promo);

            try
            {
                // Création d'une commande Oracle pour récuperer l'ensemble des éléments de tout les étudiants
                OracleCommand cmd = new OracleCommand($"SELECT e.numApogee, e.nom, e.prenom, e.sexe, e.typeBac, e.mail, e.groupe, e.estBoursier, e.regimeFormation, e.dateNaissance, e.adresse, e.telPortable, e.telFixe, e.login FROM Etudiant e INNER JOIN Promotion_Etudiant pe ON e.numApogee = pe.numApogee WHERE pe.idPromotion = {idPromo}", con.OracleConnexion);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //On récupere le caractère du sexe en BDD et on le converti avec l'énumération 
                    string sexeBDD = reader.GetString(reader.GetOrdinal("sexe"));
                    //Par défaut le sexe est autre
                    SEXE sexeEtu = SEXE.AUTRE;
                    switch (sexeBDD)
                    {
                        case "F":
                            sexeEtu = SEXE.FEMININ;
                            break;
                        case "M":
                            sexeEtu = SEXE.MASCULIN;
                            break;
                    }

                    //Récupération(lecture) de tous les éléments d'un étudiant en bdd
                    int numApogee = reader.GetInt32(reader.GetOrdinal("numApogee"));
                    string nom = reader.GetString(reader.GetOrdinal("nom"));
                    string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                    string typeBac = reader.GetString(reader.GetOrdinal("typeBac"));
                    string mail = reader.GetString(reader.GetOrdinal("mail"));

                    //On convertit la string du regime en valeur de l'enumération REGIME
                    string groupe = reader.GetString(reader.GetOrdinal("groupe"));
                    groupe.Substring(groupe.Length - 2,2);
                    GROUPE groupeEtu = GROUPE.A1;
                    switch (groupe)
                    {
                        case "A2":
                            groupeEtu = GROUPE.A2;
                            break;
                        case "B1":
                            groupeEtu = GROUPE.B1;
                            break;
                        case "B2":
                            groupeEtu = GROUPE.B2;
                            break;
                        case "C1":
                            groupeEtu = GROUPE.C1;
                            break;
                        case "C2":
                            groupeEtu = GROUPE.C2;
                            break;
                        case "D1":
                            groupeEtu = GROUPE.D1;
                            break;
                        case "D2":
                            groupeEtu = GROUPE.D2;
                            break;
                        case "E1":
                            groupeEtu = GROUPE.E1;
                            break;
                        case "E2":
                            groupeEtu = GROUPE.E2;
                            break;
                    }


                    bool estBoursier = reader.GetString(reader.GetOrdinal("estBoursier")) == "OUI"; // Convertir en booléen

                    //On convertit la string du regime en valeur de l'enumération REGIME
                    string regimeFormation = reader.GetString(reader.GetOrdinal("regimeFormation"));
                    REGIME regimeEtu = REGIME.FI;
                    switch (regimeFormation)
                    {
                        case "FC":
                            regimeEtu= REGIME.FC;
                            break;
                        case "FA":
                            regimeEtu = REGIME.FA;
                            break;
                    }

                    //champs complémentaire dont on vérifier s'il existe avant de les affecters
                    DateTime dateNaissance;
                    if (!reader.IsDBNull(reader.GetOrdinal("dateNaissance")))
                    {
                        dateNaissance = reader.GetDateTime(reader.GetOrdinal("dateNaissance"));
                    }
                    else
                    {
                        // Gestion du cas où dateNaissance est NULL dans la base de données
                        dateNaissance = DateTime.MinValue; // ou une autre valeur par défaut
                    }

                    string login;
                    if (!reader.IsDBNull(reader.GetOrdinal("login")))
                    {
                        login = reader.GetString(reader.GetOrdinal("login"));
                    }
                    else
                    {
                        // Gestion du cas où login est NULL dans la base de données
                        login = string.Empty; // ou une autre valeur par défaut
                    }

                    long telFixe;
                    if (!reader.IsDBNull(reader.GetOrdinal("telFixe")))
                    {
                        telFixe = reader.GetInt64(reader.GetOrdinal("telFixe"));
                    }
                    else
                    {
                        // Gestion du cas où telFixe est NULL dans la base de données
                        telFixe = 0; // ou une autre valeur par défaut
                    }

                    long telPortable;
                    if (!reader.IsDBNull(reader.GetOrdinal("telPortable")))
                    {
                        telPortable = reader.GetInt32(reader.GetOrdinal("telPortable"));
                    }
                    else
                    {
                        // Gestion du cas où telPortable est NULL dans la base de données
                        telPortable = 0; // ou une autre valeur par défaut
                    }

                    string adresse;
                    if (!reader.IsDBNull(reader.GetOrdinal("adresse")))
                    {
                        adresse = reader.GetString(reader.GetOrdinal("adresse"));
                    }
                    else
                    {
                        // Gestion du cas où adresse est NULL dans la base de données
                        adresse = string.Empty; // ou une autre valeur par défaut
                    }

                    // Création de l'objet Etudiant en utilisant les variables
                    Etudiant etudiant = new Etudiant(
                        numApogee,
                        nom,
                        prenom,
                        sexeEtu,
                        typeBac,
                        mail,
                        groupeEtu,
                        estBoursier,
                        regimeEtu,
                        dateNaissance,
                        login,
                        (int)telFixe,
                        (int)telPortable,
                        adresse
                    );


                    etudiants.Add(etudiant);
                }
            }
            // Gestion des exceptions
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //On ferme la connexion
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return etudiants;
        }

        /// <summary>
        /// Ajoute un nouvelle étudiant (ou le modifie s'il existe déjà)
        /// </summary>
        /// <param name="etu">etudiant qu'on veut ajouter</param>
        /// <param name="promotion">promo dans laquel on doit mettre l'etudiant</param>
        /// <returns>si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddEtu(Etudiant etu, Promotion promotion)
        {
            bool ajoutReussi = false;

            if (etu != null && promotion != null)
            {
                // Si le couple etudiant/promo existe
                if (IsEtudiantExistInPromo(etu,promotion))
                {
                    // On modifie l'etudiant a cette promo
                    ajoutReussi = ModifierEtudiant(etu);
                }
                // Sinon,
                else
                {
                    //si l'étudiant existe mais n'est pas dans la promo
                    if (IsEtudiantExist(etu))
                    {
                        //on l'ajoute dans la promo
                        ajoutReussi = AjouterEtudiantAPromotion(etu.NumApogee, promotion);
                    }

                    //sinon il n'existe pas du tout alors on le creer et on l'ajoute a la promo
                    else
                    {
                        ajoutReussi = CreerEtudiant(etu, promotion);

                        if (ajoutReussi)
                        {
                            // On ajoute l'étudiant à la promotion spécifiée
                            ajoutReussi = AjouterEtudiantAPromotion(etu.NumApogee, promotion);
                        }
                    }
                }
            }
            return ajoutReussi;
        }

        /// <summary>
        /// Ajoute l'étudiant dans la promotion
        /// </summary>
        /// <param name="numApogee">num apogee de l'étudiant</param>
        /// <param name="promotion">promo dans laquel on doit l'ajouter</param>
        /// <returns>si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        private bool AjouterEtudiantAPromotion(int numApogee, Promotion promotion)
        {
            bool ajoutReussi = false;

            //on recupere l'id de la promotion
            int idpromo = GetIdPromotion(promotion);

            Connection con = new Connection();

            try
            {
                // Vérifier si l'étudiant n'est pas déjà dans la promotion
                string checkIfExists = $"SELECT * FROM Promotion_Etudiant WHERE IDPROMOTION = {idpromo} AND numApogee = {numApogee}";

                OracleCommand checkCmd = new OracleCommand(checkIfExists, con.OracleConnexion);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    // Ajouter l'étudiant à la promotion
                    string insertIntoPromotion = $"INSERT INTO Promotion_Etudiant (idPromotion, numApogee) VALUES ({idpromo}, {numApogee})";
                    OracleCommand insertCmd = new OracleCommand(insertIntoPromotion, con.OracleConnexion);
                    insertCmd.ExecuteNonQuery();

                    ajoutReussi = true;
                }
                else
                {
                    Console.WriteLine("L'étudiant est déjà dans la promotion.");
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ajoutReussi;
        }

        /// <summary>
        /// Ajout un étudiant a la BDD s'il n'existe PAS et renvoi true, sinon renvoi false
        /// </summary>
        /// <param name="etu">etudiant à ajouté</param>
        /// <param name="promo">Promo actuel où on veut ajouter l'étu</param>
        /// <returns>si l'ajout est un succès</returns>
        public bool CreateEtu(Etudiant etu,Promotion promo)
        {
            bool ajoutReussi = false;

            if (etu != null)
            {
                //Si l'étudiant existe pas dans la promo 
                if (!IsEtudiantExistInPromo(etu,promo))                      
                {
                    //s'il n'existe pas du tout
                    if (!IsEtudiantExist(etu))
                    {
                        //on le creer 
                        ajoutReussi = CreerEtudiant(etu, promo);
                    }
                    // On ajoute l'étudiant à la promotion spécifiée
                    ajoutReussi = AjouterEtudiantAPromotion(etu.NumApogee, promo);
                }
            }
            return ajoutReussi;
        }

        /// <summary>
        /// Renvoi le string equivalent au sexe de l'étudiant
        /// </summary>
        /// <param name="etu">etudiant dont on veut le sexe</param>
        /// <returns>string equivalent au sexe de l'étudiant</returns>
        /// <author>Nordine</author>
        private string getSexeString(Etudiant etu)
        {
  
            string etuSexe;
            switch (etu.Sexe)
            {
                case SEXE.FEMININ:
                    etuSexe = "F";
                    break;
                case SEXE.MASCULIN:
                    etuSexe = "M";
                    break;
                default:
                    etuSexe = "A";
                    break;
            }
            return etuSexe;
        }

        /// <summary>
        /// Renvoi le string equivalent au regime de formation de l'étudiant
        /// </summary>
        /// <param name="etu">etudiant dont on veut le regime de formation</param>
        /// <returns>string equivalent au regime de formation de l'étudiant</returns>
        /// <author>Laszlo</author>
        private string getRegimeString(Etudiant etu)
        {

            string etuRegime;
            switch (etu.TypeFormation)
            {
                case REGIME.FC:
                    etuRegime = "FC";
                    break;
                case REGIME.FA:
                    etuRegime = "FA";
                    break;
                default:
                    etuRegime = "FI";
                    break;
            }
            return etuRegime;
        }

        /// <summary>
        /// Renvoi le string equivalent au groupe de l'étudiant
        /// </summary>
        /// <param name="etu">etudiant dont on veut le regime de formation</param>
        /// <returns>string equivalent au regime de formation de l'étudiant</returns>
        /// <author>Laszlo</author>
        private string getGroupeString(Etudiant etu)
        {

            string etuGroupe;
            switch (etu.Groupe)
            {
                default:
                    etuGroupe = "A1";
                    break;
                case GROUPE.A2:
                    etuGroupe = "A2";
                    break;
                case GROUPE.B1:
                    etuGroupe = "B1";
                    break;
                case GROUPE.B2:
                    etuGroupe = "B2";
                    break;
                case GROUPE.C1:
                    etuGroupe = "C1";
                    break;
                case GROUPE.C2:
                    etuGroupe = "C2";
                    break;
                case GROUPE.D1:
                    etuGroupe = "D1";
                    break;
                case GROUPE.D2:
                    etuGroupe = "D2";
                    break;
                case GROUPE.E1:
                    etuGroupe = "E1";
                    break;
                case GROUPE.E2:
                    etuGroupe = "E2";
                    break;
            }
            return etuGroupe;
        }


        /// <summary>
        /// Modifie l'étudiant existant
        /// </summary>
        /// <param name="etu">etudiant à modifier</param>
        /// <returns>si la modification est un succès</returns>
        /// <author>Nordine</author>
        private bool ModifierEtudiant(Etudiant etu)
        {
            bool ajoutReussi = false;

            // Création d'une connexion Oracle
            Connection con = new Connection();


            string etuSexe = getSexeString(etu);

            string estBoursier = etu.EstBoursier ? "OUI" : "NON";

            try
            {
                // L'étudiant existe déjà, nous devons effectuer une mise à jour
                string updateQuery = string.Format(@"UPDATE Etudiant
                                                   SET nom = '{0}', prenom = '{1}', sexe = '{2}', typeBac = '{3}', mail = '{4}', groupe = '{5}', estBoursier = '{6}', regimeFormation = '{7}', dateNaissance = TO_DATE('{8}', 'YYYY-MM-DD'), adresse = '{9}', telPortable = {10}, telFixe = {11}, login = '{12}'
                                                   WHERE numApogee = {13}",
                                                  etu.Nom, etu.Prenom, etuSexe, etu.TypeBac, etu.Mail, getGroupeString(etu),
                                                  estBoursier, getRegimeString(etu), etu.DateNaissance.Date.ToString("yyyy-MM-dd"),
                                                  etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login, etu.NumApogee);

                OracleCommand updateCmd = new OracleCommand(updateQuery, con.OracleConnexion);

                if (updateCmd.ExecuteNonQuery() == 1)
                {
                    ajoutReussi = true;
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ajoutReussi;
        }

        /// <summary>
        /// Creer un étudiant sur la bdd 
        /// </summary>
        /// <param name="etu">etudiant à creer</param>
        /// <returns>si la creation est un succès</returns>
        /// <author>Nordine</author>
        private bool CreerEtudiant(Etudiant etu, Promotion promotion)
        {
            bool ajoutReussi = false;

            // Création d'une connexion Oracle
            Connection con = new Connection();

            string etuSexe = getSexeString(etu);
            string estBoursier = etu.EstBoursier ? "OUI" : "NON";
            int idPromo = GetIdPromotion(promotion);

            try
            {
                // Insertion de l'étudiant dans la table Etudiant
                string insertQuery = string.Format(@"INSERT INTO Etudiant(numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login)
                                           VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', TO_DATE('{9}', 'YYYY-MM-DD'), '{10}', {11}, {12}, '{13}')",
                                                  etu.NumApogee, etu.Nom, etu.Prenom, etuSexe, etu.TypeBac, etu.Mail, getGroupeString(etu),
                                                  estBoursier, getRegimeString(etu), etu.DateNaissance.Date.ToString("yyyy-MM-dd"),
                                                  etu.Adresse, etu.TelPortable, etu.TelFixe, etu.Login);

                OracleCommand insertCmd = new OracleCommand(insertQuery, con.OracleConnexion);

                if (insertCmd.ExecuteNonQuery() == 1)
                {
                    ajoutReussi = true;
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ajoutReussi;
        }

        /// <summary>
        /// Renvoie l'idPromotion dans la bdd
        /// </summary>
        /// <param name="promo">Promo dont on veut l'id</param>
        /// <returns>Id de la promo</returns>
        /// <author>Nordine</author>
        private int GetIdPromotion(Promotion promotion)
        {
            int idPromo = -1;

            int idNomPromo = 0;
            switch (promotion.NomPromotion)
            {
                case NOMPROMOTION.BUT1:
                    idNomPromo = 0;
                    break;
                case NOMPROMOTION.BUT2:
                    idNomPromo = 1;
                    break;
                case NOMPROMOTION.BUT3:
                    idNomPromo = 2;
                    break;
            }

            Connection con = new Connection();

            try
            {
                // On récupère l'id
                string requete = $"SELECT IDPROMOTION FROM Promotion WHERE IDNOMPROMOTION = {idNomPromo} AND Anneedebut = {promotion.AnneeDebut}";

                OracleCommand checkCmd = new OracleCommand(requete, con.OracleConnexion);

                // Utilisation d'ExecuteScalar pour obtenir une seule valeur
                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    // Conversion de la valeur en int
                    idPromo = Convert.ToInt32(result);
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return idPromo;
        }

        /// <summary>
        /// Renvoi si l'étudiant existe dans la bdd
        /// </summary>
        /// <param name="etu">etudiant à cherché</param>
        /// <returns>si létudiant existe déjà dans la bdd</returns>
        /// <author>Nordine</author>
        private bool IsEtudiantExist(Etudiant etu)
        {
            bool etudiantExist = false;
            // Création d'une connexion Oracle
            Connection con = new Connection();
            try
            {
                // On vérifie si un étudiant avec le même numéro d'apogée existe déjà
                string checkIfExistsQuery = $"SELECT COUNT(*) FROM Etudiant WHERE numApogee = {etu.NumApogee}";
                OracleCommand checkIfExistsCmd = new OracleCommand(checkIfExistsQuery, con.OracleConnexion);
                int existingCount = Convert.ToInt32(checkIfExistsCmd.ExecuteScalar());
                if (existingCount > 0)
                {
                    etudiantExist = true;
                }

            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            return etudiantExist;
        }


        /// <summary>
        /// Renvoie si le couple numApogee/idPromo existe dans la bdd
        /// </summary>
        /// <param name="etu">Etudiant à chercher</param>
        /// <param name="promo">Promotion dans laquelle vérifier l'existence</param>
        /// <returns>si le couple numApogee/idPromo existe dans la bdd</returns>
        /// <author>Nordine</author>
        private bool IsEtudiantExistInPromo(Etudiant etu, Promotion promo)
        {
            bool etudiantExist = false;
            // Création d'une connexion Oracle
            Connection con = new Connection();

            try
            {
                // On vérifie si un étudiant avec le même numéro d'apogée existe déjà dans la promotion
                int idPromo = GetIdPromotion(promo);

                if (idPromo != -1)
                {
                    string checkIfExistsQuery = $"SELECT COUNT(*) FROM Promotion_Etudiant WHERE IDPROMOTION = {idPromo} AND numApogee = {etu.NumApogee}";
                    OracleCommand checkIfExistsCmd = new OracleCommand(checkIfExistsQuery, con.OracleConnexion);
                    int existingCount = Convert.ToInt32(checkIfExistsCmd.ExecuteScalar());
                    if (existingCount > 0)
                    {
                        etudiantExist = true;
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return etudiantExist;
        }


        /// <summary>
        /// Ajoute les touts les étudiants de la liste d'étudiants
        /// </summary>
        /// <param name="listeEtu">Liste d'étudiant à ajouter</param>
        /// <param name="promotion">promo dans laquel on ajoute les étudiants</param>
        /// <returns>true si l'ajout est un succes</returns>
        /// <author>Nordine</author>
        public bool AddSeveralEtu(IEnumerable<Etudiant> listeEtu, Promotion promotion)
        {
            bool ajoutReussi = true;

            // Pour tous les étudiants de la liste
            foreach (Etudiant etu in listeEtu)
            {
                // On essaie de les ajouter
                try
                {
                    // On ajoute et on récupère le bool de succès
                    bool succes = AddEtu(etu, promotion);

                    // Si le succès est faux lors d'un ajout, l'ajout total est considéré comme un échec
                    if (!succes)
                    {
                        ajoutReussi = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return ajoutReussi;
        }


        /// <summary>
        /// Renvoi tous les étudiants ayant au moins une note de la catégorie spécifiée
        /// </summary>
        /// <param name="categorie">Catégorie spécifiée</param>
        /// <returns>Un dictionnaire avec les étudiants et le nombre de notes de la catégorie</returns>
        /// <author>Nordine</author>
        public List<Tuple<Etudiant, int>> GetAllEtuByCategorie(CATEGORIE categorie)
        {
            // Création d'une connexion Oracle
            Connection con = new Connection();
            // Dictionnaire pour stocker les résultats
            List<Tuple<Etudiant, int>> etudiantsByCategorie = new List<Tuple<Etudiant, int>>();

            try
            {
                string requete = "SELECT e.numApogee, e.nom, e.prenom, e.sexe, e.typeBac, e.mail, e.groupe, e.estBoursier, e.regimeFormation, e.dateNaissance, e.adresse, e.telPortable, e.telFixe, e.login, COUNT(n.idNote) AS NombreNotes " +
                     "FROM Etudiant e " +
                     "JOIN Note n ON e.numApogee = n.apogeeEtudiant " +
                     "WHERE n.idCategorie = :categorie " +
                     "GROUP BY e.numApogee, e.nom, e.prenom, e.sexe, e.typeBac, e.mail, e.groupe, e.estBoursier, e.regimeFormation, e.dateNaissance, e.adresse, e.telPortable, e.telFixe, e.login";


                using (OracleCommand cmd = new OracleCommand(requete, con.OracleConnexion))
                {
                    cmd.Parameters.Add("categorie", OracleDbType.Int32).Value = (int)categorie;

                    OracleDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Récupération(lecture) de tous les éléments d'un étudiant en bdd
                        int numApogee = reader.GetInt32(reader.GetOrdinal("numApogee"));
                        string nom = reader.GetString(reader.GetOrdinal("nom"));
                        string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                        string sexeBDD = reader.GetString(reader.GetOrdinal("sexe"));
                        SEXE sexeEtu = sexeBDD == "F" ? SEXE.FEMININ : (sexeBDD == "M" ? SEXE.MASCULIN : SEXE.AUTRE);
                        string typeBac = reader.GetString(reader.GetOrdinal("typeBac"));
                        string mail = reader.GetString(reader.GetOrdinal("mail"));

                        //On convertit la string du regime en valeur de l'enumération REGIME
                        string groupe = reader.GetString(reader.GetOrdinal("groupe"));
                        groupe.Substring(groupe.Length - 2, 2);
                        GROUPE groupeEtu = GROUPE.A1;
                        switch (groupe)
                        {
                            case "A2":
                                groupeEtu = GROUPE.A2;
                                break;
                            case "B1":
                                groupeEtu = GROUPE.B1;
                                break;
                            case "B2":
                                groupeEtu = GROUPE.B2;
                                break;
                            case "C1":
                                groupeEtu = GROUPE.C1;
                                break;
                            case "C2":
                                groupeEtu = GROUPE.C2;
                                break;
                            case "D1":
                                groupeEtu = GROUPE.D1;
                                break;
                            case "D2":
                                groupeEtu = GROUPE.D2;
                                break;
                            case "E1":
                                groupeEtu = GROUPE.E1;
                                break;
                            case "E2":
                                groupeEtu = GROUPE.E2;
                                break;
                        }
                        bool estBoursier = reader.GetString(reader.GetOrdinal("estBoursier")) == "OUI";
                        string regimeFormation = reader.GetString(reader.GetOrdinal("regimeFormation"));
                        REGIME regimeEtu = (REGIME)Enum.Parse(typeof(REGIME), regimeFormation);
                        // Récupération du nombre de notes de la catégorie
                        int nombreNotes = reader.GetInt32(reader.GetOrdinal("NombreNotes"));

                        DateTime dateNaissance;
                        if (!reader.IsDBNull(reader.GetOrdinal("dateNaissance")))
                        {
                            dateNaissance = reader.GetDateTime(reader.GetOrdinal("dateNaissance"));
                        }
                        else
                        {
                            dateNaissance = DateTime.MinValue;
                        }

                        string login;
                        if (!reader.IsDBNull(reader.GetOrdinal("login")))
                        {
                            login = reader.GetString(reader.GetOrdinal("login"));
                        }
                        else
                        {
                            login = string.Empty;
                        }

                        long telFixe;
                        if (!reader.IsDBNull(reader.GetOrdinal("telFixe")))
                        {
                            telFixe = reader.GetInt64(reader.GetOrdinal("telFixe"));
                        }
                        else
                        {
                            telFixe = 0;
                        }

                        long telPortable;
                        if (!reader.IsDBNull(reader.GetOrdinal("telPortable")))
                        {
                            telPortable = reader.GetInt64(reader.GetOrdinal("telPortable"));
                        }
                        else
                        {
                            telPortable = 0;
                        }

                        string adresse;
                        if (!reader.IsDBNull(reader.GetOrdinal("adresse")))
                        {
                            adresse = reader.GetString(reader.GetOrdinal("adresse"));
                        }
                        else
                        {
                            adresse = string.Empty;
                        }

                        // Création de l'objet Etudiant en utilisant les variables
                        Etudiant etudiant = new Etudiant(numApogee, nom,prenom,sexeEtu,typeBac,mail,groupeEtu,estBoursier,regimeEtu,dateNaissance,login,(int)telFixe,(int)telPortable,adresse);

                        //Creation couple 
                        Tuple<Etudiant, int> couple = new (etudiant, nombreNotes);
                        // Ajout du couple (Etudiant, Nombre de notes de la catégorie) dans le dictionnaire
                        etudiantsByCategorie.Add(couple);
                    }

                }
            }
            // Gestion des exceptions
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Fermeture de la connexion
                try
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return etudiantsByCategorie;
        }

    }
}
