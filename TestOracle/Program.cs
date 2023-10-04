// See https://aka.ms/new-console-template for more information
using Oracle.ManagedDataAccess.Client;

Console.WriteLine("Hello, World!");
string connexionString = "User Id = IQ_BD_HIDA; Password = HIDA0000; Data Source = srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr";       //TROUVER LE CONNEXION STRING ICI
OracleConnection con = new OracleConnection(connexionString);

    con.Open();
Console.WriteLine(con.DatabaseName);

OracleCommand cmd = new OracleCommand("SELECT numApogee, nom, prenom, sexe, typeBac, mail, groupe, estBoursier, regimeFormation, dateNaissance, adresse, telPortable, telFixe, login FROM Etudiant", con);

OracleDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(reader.GetString(0));
    Console.WriteLine("avec crochet");
    Console.WriteLine(reader.GetString(reader.GetOrdinal("sexe"))[0]);


    Console.WriteLine("Sans crochet");
    Console.WriteLine(reader.GetString(reader.GetOrdinal("sexe")));

    //On récupere le caractère du sexe en BDD et on le converti avec l'énumération 
    char sexeBDD = reader.GetString(reader.GetOrdinal("sexe"))[0];
    /*
    int numApogee = reader.GetInt32(reader.GetOrdinal("numApogee"));
    string nom = reader.GetString(reader.GetOrdinal("nom"));
    string prenom = reader.GetString(reader.GetOrdinal("prenom"));*/
}

