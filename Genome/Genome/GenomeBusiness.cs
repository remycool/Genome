using Cluster.Classes;
using Cluster.Interfaces;
using System.Diagnostics;
using System.Linq;

namespace Genome.GenomeBusiness
{
    public class GenomeBusiness : IClusterizableBusiness
    {
        /// <summary>
        /// méthode qui compte le nombre de charactère contenu dans la chaine de caractère passé en paramètre 
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns> un objet Operation qui contient le résultat</returns>
        Operation IClusterizableBusiness.Calcul1(string chunk)
        {
            char charToCount = 'C';
            //opération
            int count = chunk.Count(c => c == charToCount);
            return new Operation { Resultat = count, TempsExecution = 0 };
        }

        /// <summary>
        /// Méthode qui sépare les 2 brins du morceau de génome qui est une chaine de caractère passée en paramètre  
        /// </summary>
        /// <param name="chunck"></param>
        /// <returns>un objet Operation contenant le résultat et le temps d'éxéution du cacul</returns>
        Operation IClusterizableBusiness.Calcul2(string chunck)
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

            return new Operation { Param = $"{brin1}\n{brin2}", TempsExecution = sw.ElapsedMilliseconds };
        }
    }
}
