using Cluster.Interfaces;
using System;
using Cluster.Classes;

namespace Genome
{
    public class Module1Business : IClusterizableBusiness
    {
        public IResultat Calculer(string chunkFile)
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

            return new Resultat<Base> { Valeur = b, Erreur = erreur };
        }
    }
}
