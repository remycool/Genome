using Cluster.Classes;
using Cluster.Interfaces;
using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Genome.GenomeBusiness
{
    public class GenomeBusiness : IClusterizableBusiness
    {
        public Resultat Calcul4(string chunkfile)
        {
            throw new NotImplementedException();
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
            string brin1 = string.Empty;
            string brin2 = string.Empty;
            char[] arrayChar = chunck.ToCharArray();
            int i = 0;
            bool end = false;
            Stopwatch sw = Stopwatch.StartNew();
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
            sw.Stop();

            return new Resultat { TempsExecution = sw.ElapsedMilliseconds };
        }

        /// <summary>
        /// Méthode qui compte les bases A,T,G,C du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Resultat IClusterizableBusiness.Calcul3(string chunkFile)
        {
            Base b = new Base();

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
            return new Resultat {Valeur=0};
        }
    }
}
