using Cluster.Classes;
using Cluster.Interfaces;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Genome.GenomeBusiness
{
    public class GenomeBusiness : IClusterizableBusiness
    {
        /// <summary>
        /// Méthode nombre total de paire de base
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation qui contient le résultat</returns>
        Resultat IClusterizableBusiness.Calcul4(string chunkfile)
        {
            Exception erreur = null;
            int count = 0;
            Stopwatch sw = Stopwatch.StartNew();
            try
                {
                string[]  Lines = File.ReadAllLines(chunkfile);
                
                foreach (string line in Lines)
                    {
                        switch (line)
                        {
                            case "AT":
                                count++;
                                break;

                            case "CG":
                                count++;
                                break;
                        }
                    }
                
                }
            catch (Exception ex)
                {
                erreur = ex;
                }
                sw.Stop();
            return new Resultat { Valeur = count, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };          
        }

        /// <summary>
        /// méthode qui compte le nombre de charactère contenu dans la chaine de caractère passé en paramètre 
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns> un objet Operation qui contient le résultat</returns>
        Resultat IClusterizableBusiness.Calcul1(string chunk)
        {
            char charToCount = '-';
            //opération
            int count = chunk.Count(c => c == charToCount);
            return new Resultat {Valeur = count, TempsExecution = 0 };
        }

        /// <summary>
        /// Méthode qui sépare les 2 brins du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul2(string chunck)
        {
            Exception erreur = null;
            string brin1 = string.Empty;
            string brin2 = string.Empty;
            char[] arrayChar = chunck.ToCharArray();
            int i = 0;
            bool end = false;
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                while (!end)
                {
                    bool carriageReturn = false;
                    bool switchBrin = false;

                    //lire ligne aprés ligne
                    while (!carriageReturn)
                    {
                        //Teste si on arrive à la fin du fichier
                        if (i == chunck.Length)
                        {
                            end = true;
                            break;
                        }

                        if (arrayChar[i] == 'A' || arrayChar[i] == 'T' || arrayChar[i] == 'G' || arrayChar[i] == 'C')
                        {

                            if (!switchBrin)
                            {
                                brin1 += arrayChar[i];
                                switchBrin = !switchBrin;
                            }
                            else
                                brin2 += arrayChar[i];
                        }
                        else
                            if (arrayChar[i] == '\n')
                            carriageReturn = true;
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;
            }
            sw.Stop();

            return new Resultat { TempsExecution = sw.ElapsedMilliseconds, Erreur =  erreur};
        }

        /// <summary>
        /// Méthode qui compte les bases A,T,G,C du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul3(string chunkFile)
        {
            Base b = new Base();
            Exception erreur = null;
            try
            {
                for (int i = 0; i < chunkFile.Length; i++)
                {
                    switch (chunkFile[i])
                    {
                        case 'A':
                            b.NbBaseA++;
                            break;
                        case 'T':
                            b.NbBaseT++;
                            break;
                        case 'G':
                            b.NbBaseG++;
                            break;
                        case 'C':
                            b.NbBaseC++;
                            break;
                        case '-':
                            b.NbBaseInconnue++;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;
            }
            
            return new Resultat {Valeur=0, Erreur= erreur};
        }

        /// <summary>
        /// Cette Methode permettant de retourner le nombre de base inconnue
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul5(string chunkfile)
        {
            Exception erreur= null;
            int baseInconnue = 0;
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string[]  Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    //première colone du fichier
                    if (Convert.ToString(Lines[i][0]).Contains("-"))
                    {
                        baseInconnue++;
                    }
                }

                //deuxième colone du fichier
                for (int i = 0; i < Lines.Count(); i++)
                {
                    if (Convert.ToString((Lines[i]).Substring(1)).Contains("-"))
                    {
                        baseInconnue++;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;               
            }
            sw.Stop();
            return new Resultat { Valeur = baseInconnue, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de A
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul6(string chunkfile)
        {
            Exception erreur = null;
            int nbA = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string [] Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    //Rassemble les deux colonne du fichier en une seul
                    concatColonne.Add(Convert.ToString(Lines[i][0]));
                    concatColonne.Add(Convert.ToString(Lines[i]).Substring(1));
                }
                foreach (string val in concatColonne)
                {
                    switch (val)
                    {
                        case "A":
                            nbA++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;
            }
            sw.Stop();
            return new Resultat { Valeur = nbA, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de T
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul7(string chunkfile)
        {
            Exception erreur = null;
            int nbT = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string [] Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    concatColonne.Add(Convert.ToString(Lines[i][0]));
                    concatColonne.Add(Convert.ToString(Lines[i]).Substring(1));
                }
                foreach (string val in concatColonne)
                {
                    switch (val)
                    {
                        case "T":
                            nbT++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;               
            }
            sw.Stop();
            return new Resultat { Valeur = nbT, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur  };
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de G
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul8(string chunkfile)
        {
            Exception erreur = null;
            int nbG = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string []Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    concatColonne.Add(Convert.ToString(Lines[i][0]));
                    concatColonne.Add(Convert.ToString(Lines[i]).Substring(1));
                }
                foreach (string val in concatColonne)
                {
                    switch (val)
                    {
                        case "G":
                            nbG++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;           
            }
            sw.Stop();
            return new Resultat { Valeur = nbG, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur};
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de C
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul9(string chunkfile)
        {
            Exception erreur = null;
            int nbC = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string [] Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    concatColonne.Add(Convert.ToString(Lines[i][0]));
                    concatColonne.Add(Convert.ToString(Lines[i]).Substring(1));
                }
                foreach (string val in concatColonne)
                {
                    switch (val)
                    {
                        case "C":
                            nbC++;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                erreur = ex;             
            }
            sw.Stop();
            return new Resultat { Valeur = nbC, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Cette méthode permet de récupérer le pourcentage d'occurrence A, T, G, C
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul10(string chunkfile)
        {
            Exception erreur = null;
            int total = 0;
            int totalChar = 0;
            double pourcentage = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                string [] Lines = File.ReadAllLines(chunkfile);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    concatColonne.Add(Convert.ToString(Lines[i][0]));
                    concatColonne.Add(Convert.ToString(Lines[i]).Substring(1));
                }
                foreach (string val in concatColonne)
                {
                    switch (val)
                    {
                        case "A":
                            total++;
                            break;

                        case "T":
                            total++;
                            break;

                        case "G":
                            total++;
                            break;

                        case "C":
                            total++;
                            break;
                    }
                }

                foreach (string val in concatColonne)
                {
                    totalChar++;
                }
                pourcentage = (total / Convert.ToDouble(totalChar));

                Console.WriteLine("Pourcentage : " + pourcentage);
            }
            catch (Exception ex)
            {
                erreur = ex;
            }
            sw.Stop();
            return new Resultat { ValeurPrcent = pourcentage, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }
    }
}
