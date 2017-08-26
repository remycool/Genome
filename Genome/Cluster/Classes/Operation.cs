namespace Cluster.Classes
{
    public class Operation
    {
        public string IpNoeud { get; set; }
        public string Methode { get; set; }
        public string Chunck { get; set; }
        //public int Resultat { get; set; }
       

        public Operation() { }

        //public override string ToString()
        //{
        //    return $">>> Résultat : {Resultat} effectué par {IpNoeud} en {TempsExecution} ms";
        //}

        public static Operation operator +(Operation a, Operation b)
        {
            return new Operation() { /*Resultat = a.Resultat + b.Resultat*/ };
        }
    }
}
