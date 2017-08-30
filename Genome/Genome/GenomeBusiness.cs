using Cluster.Classes;
using Cluster.Interfaces;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
            string erreur = string.Empty;
            string res = string.Empty;
            //int count = 0;
            Stopwatch sw = Stopwatch.StartNew();
            int paireAT = 0;
            int paireCG = 0;
            try
            {
                Char[] concatColonne = chunkfile.ToCharArray();

                paireAT = Regex.Matches(chunkfile, "AT").Cast<Match>().Count();
                paireCG = Regex.Matches(chunkfile, "CG").Cast<Match>().Count();
                //for (int i = 0; i < concatColonne.Count(); i++)
                //{
                //    switch (concatColonne[i])
                //    {
                //        case 'AT':
                //            count++;
                //            break;

                //        case 'CG':
                //            count++;
                //            break;
                //    }
                //}

                res = "\n Paire AT : " + Convert.ToString(paireAT) + "Paire CG : " + Convert.ToString(paireCG);
            }
            catch (Exception ex)
            {
                erreur = $"{ ex.Message}";
            }
            sw.Stop();

            return new Resultat {ValeurString = res, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
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
            return new Resultat { Valeur = count, TempsExecution = 0 };
        }

        /// <summary>
        /// Méthode qui sépare les 2 brins du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul2(string chunck)
        {
            string erreur = string.Empty;
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
                erreur = $"{ex.Message}";
            }
            sw.Stop();

            return new Resultat { TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Méthode qui compte les bases A,T,G,C du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul3(string chunkFile)
        {
            string erreur = string.Empty;
            Base b = new Base();
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
                erreur = $"{ex.Message}";
            }

            return new Resultat { Valeur = 0, Erreur = erreur };
        }

        /// <summary>
        /// Cette Methode permettant de retourner le nombre de base inconnue
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul5(string chunkfile)
        {
            string erreur = string.Empty;
            int baseInconnue = 0;
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                char charToCount = '-';
                //opération
                baseInconnue = chunkfile.Count(c => c == charToCount);

            }
            catch (Exception ex)
            {
                erreur = $"{ ex.Message}";
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
            string erreur = string.Empty;
            int nbA = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                char charToCount = 'A';
                //opération
                nbA = chunkfile.Count(c => c == charToCount);
            }
            catch (Exception ex)
            {
                erreur = $"{ex.Message}";
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
            string erreur = string.Empty;
            int nbT = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                char charToCount = 'T';
                //opération
                nbT = chunkfile.Count(c => c == charToCount);
            }
            catch (Exception ex)
            {
                erreur = $"{ex.Message}";
            }
            sw.Stop();
            return new Resultat { Valeur = nbT, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de G
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul8(string chunkfile)
        {
            string erreur = string.Empty;
            int nbG = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                char charToCount = 'G';
                //opération
                nbG = chunkfile.Count(c => c == charToCount);
            }
            catch (Exception ex)
            {
                erreur = $"{ex.Message}";
            }
            sw.Stop();
            return new Resultat { Valeur = nbG, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }

        /// <summary>
        /// Cette méthode permet de renvoyé le nombre de C
        /// </summary>
        /// <param name="file"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul9(string chunkfile)
        {
            string erreur = string.Empty;
            int nbC = 0;
            List<string> concatColonne = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                char charToCount = 'C';
                //opération
                nbC = chunkfile.Count(c => c == charToCount);
            }
            catch (Exception ex)
            {
                erreur = $"{ex.Message}";
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
            string erreur = string.Empty;
            string result = string.Empty;
            int totalA = 0;
            int totalC = 0;
            int totalG = 0;
            int totalT = 0;
            int totalChar = 0;
            double pourcentageA = 0;
            double pourcentageT = 0;
            double pourcentageG = 0;
            double pourcentageC = 0;
            Char[] concatColonne = chunkfile.ToCharArray();

            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                for (int i = 0; i < concatColonne.Count(); i++)
                {
                    switch (concatColonne[i])
                    {
                        case 'A':
                            totalA++;
                            break;

                        case 'T':
                            totalT++;
                            break;

                        case 'G':
                            totalG++;
                            break;

                        case 'C':
                            totalC++;
                            break;
                    }
                }


                for (int i = 0; i < concatColonne.Count(); i++)
                {
                    totalChar++;
                }

                pourcentageA = (totalA * 100 / Convert.ToDouble(totalChar));
                pourcentageT = (totalT * 100 / Convert.ToDouble(totalChar));
                pourcentageG = (totalG * 100 / Convert.ToDouble(totalChar));
                pourcentageC = (totalC * 100 / Convert.ToDouble(totalChar));

                result = "\n % A : " + Convert.ToString(pourcentageA) + " % T : " + Convert.ToString(pourcentageT) + " % G : " + Convert.ToString(pourcentageG) + "% C : " + Convert.ToString(pourcentageC);

            }
            catch (Exception ex)
            {
                erreur = $"{ex.Message}";
            }
            sw.Stop();
            return new Resultat { ValeurString = result, TempsExecution = sw.ElapsedMilliseconds, Erreur = erreur };
        }
    }
}
