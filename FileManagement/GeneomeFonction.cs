using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    public class geneomeFonction
    {
        
            public string Text { get; set; }
            public static string[] Lines { get; set; }
            public static List<string> fileToArray = new ArrayList<string>();
            public List<string> second = new ArrayList<string>();
            public List<string> secondColumn = new ArrayList<string>();
            public List<string> firstColumn = new ArrayList<string>();
            public List<string> columnGenom = new ArrayList<string>();
            List<string> secondPart = new ArrayList<string>();
            public int nombreCodonsStart = 0;
            public int nombreCodonStop = 0;
            private int baseInconnue { get; set; }
            private int nbPaireBase { get; set; }
            public static string verifFile;
            public delegate List<string> TestColumn(List<string> meth);

            public geneomeFonction()
            {
                nbPaireBase = 0;
                baseInconnue = 0;
            }

            //Cette méthode permet de récupérer les génotype du fichier texte envoyer par l'utilisateur
            public static List<string> arrayFromFile(string file)
            {
                verifFile = Path.GetExtension(file);
                //Lines = File.ReadAllLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt"); 
                try
                {
                    if (verifFile == ".txt")
                    {
                        Lines = File.ReadAllLines(file);
                        if (Lines.First().Contains("\t"))
                        {
                            if (Lines.First().Contains("genotype"))
                            {
                                Lines = Lines.Skip(1).ToArray();
                                int i = 0;
                                foreach (string line in Lines)
                                {
                                    //récupère les derniers caractère d'une ligne
                                    string t = line.Substring(line.Length - 2, 2).Trim();
                                    fileToArray.Add(t);
                                    //Console.WriteLine($"{t}\t : " + i);
                                    i++;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Avertissement : Votre fichier n'est pas valide manque d'en-tête");
                            
                            }
                        }
                        else
                        {
                            Console.WriteLine("Avertissement : Votre fichier n'est pas valide - manque te tabulation");
                        }
                    }
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Le chemin vers le fichier est null");
                }
                return fileToArray;
            }


            //Méthode qui permet de diviser le fichier en deux et récupère la la première partie du fichier
            public List<string> fisrtPartFile(string file)
            {
                List<string> arrListChar = arrayFromFile(file);
                //permet de diviser le tableau en deux
                List<string> firstPart = arrListChar.Take(arrListChar.Count / 2).ToList();
                return firstPart;
            }

            //Méthode qui permet de récupérer la deuxieme partie du fichser
            public List<string> secondPartFile(string file)
            {
                List<string> arrListChar = arrayFromFile(file);
                secondPart = arrListChar.Skip(arrListChar.Count / 2).ToList();
                return secondPart;
            }


            //Méthode qui permet de récupérer la première colonne d'un tableau (genotype du fichier)
            public List<string> getFirstColumn(List<string> firstPartFile)
            {
                try
                {
                    for (int i = 0; i < firstPartFile.Count(); i++)
                    {
                        try
                        {
                            if (!(Convert.ToString(firstPartFile[i][0])).Equals(""))
                            {
                                firstColumn.Add(Convert.ToString(firstPartFile[i][0]));
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("L'index est hors du périmètre");
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("erreur sur le paramètre de la méthode, attend une list avec deux colonnes ");
                }

                return firstColumn;
            }

            //Méthode qui permet de récupérer la deuxième colonne du genotype - FileOne
            public List<string> getSecondColumn(List<string> firstPartFile)
            {
                try
                {
                    for (int i = 0; i < firstPartFile.Count(); i++)
                    {
                        try
                        {
                            if (!(Convert.ToString(firstPartFile[i]).Substring(1)).Equals(""))
                            {
                                secondColumn.Add(Convert.ToString(firstPartFile[i]).Substring(1));
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("L'index est hors du périmètre");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("erreur sur le paramètre de la méthode, attend une list avec deux colonnes");
                }
                return secondColumn;
            }


            //Méthode nombre total de paire de base
            public int totalPaireDeBase(TestColumn getFirstColumn, TestColumn getSecondColumn, List<string> firstPartFile)
            {
                try
                {
                    for (int i = 0; i < firstPartFile.Count; i++)
                    {
                        if ((getFirstColumn(firstPartFile)[i]).Equals(getSecondColumn(firstPartFile)[i]))
                        {
                            nbPaireBase++;
                        }
                    }
                    Console.WriteLine("trouvé : " + nbPaireBase);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("erreur sur les paramètres de la méthode");
                }

                return nbPaireBase;
            }


            public void tester(Func<string, List<string>> secondPartFile/*TestColumn secondPartFile*/, string file)
            {
                //List<string> arrContent = secondPartFile(file);
                /* for (int i = 0; i < arrContent.Count(); i++)
                  {
                      if (!(Convert.ToString(arrContent[i]).Substring(1)).Equals(""))
                      {
                          //récupère la deuxième colonne du tableau
                        //  secondColumn.Add(Convert.ToString(arrContent[i]).Substring(1));
                          Console.WriteLine("Elements : " + Convert.ToString(arrContent[i]).Substring(1));
                      }
                  }*/
                Console.WriteLine("{0} {1}", secondPartFile, file, secondPartFile(file));
                Console.ReadKey();
            }


            //Methode permettant de retourner le nombre de base inconnue

            public int calculBaseInconnue(List<string> columnGenom)
            {
                try
                {
                    for (int i = 0; i < columnGenom.Count(); i++)
                    {
                        if (columnGenom[i].Contains("-"))
                        {
                            baseInconnue++;
                        }
                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("erreur sur les paramètres de la méthode");
                }
                return baseInconnue;
            }



            public void rechercheCodonsStop(string codonStart, string codonStop, List<string> columnGenom)
            {
                try
                {
                    List<string> tripletArr = new ArrayList<string>();
                    string tripletFind;
                    string tripletstop;
                    int w = 0;
                    int p = 3;
                    for (int i = 0; i < columnGenom.Count(); i++)
                    {
                        if (p <= columnGenom.Count())
                        {
                            Console.WriteLine("Premier tour w val : " + w);
                            while (w < p)
                            {
                                Console.WriteLine("valeur :" + Convert.ToString(columnGenom[w]) + " w =" + w);
                                tripletArr.Add(Convert.ToString(columnGenom[w]));
                                w++;


                            }
                            tripletFind = string.Join("", tripletArr);
                            tripletstop = string.Join("", tripletArr);
                            if (tripletFind.Equals(codonStart))
                            {
                                Console.WriteLine("Codons Start trouvé :" + tripletFind);
                                nombreCodonsStart++;
                            }
                            else { Console.WriteLine("Codons Start Not found"); }
                            if (tripletstop.Equals(codonStop))
                            {
                                Console.WriteLine("Codons Stop trouvé :" + tripletstop);
                                nombreCodonStop++;
                            }
                            else { Console.WriteLine("Codons ATGC Not found"); }
                            w = w - 2;
                            //Console.WriteLine("valeur de w à la sorti de de la boucle while : " + w);                                   
                            p = p + 1;
                        }
                        //vide le tableau afin de récupérer les nouveaus triplets 
                        tripletArr.Clear();
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    throw new ArgumentOutOfRangeException("index parameter is out of range.", e);

                }
                Console.ReadKey();
            }


        }

        public class ArrayList<T> : List<string> { }
    }


