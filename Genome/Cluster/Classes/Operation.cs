namespace Cluster.Classes
{
    public class Operation
    {
        public string IpNoeud { get; set; }
        public string Type { get; set; }
        public string Param { get; set; }
        public int Resultat { get; set; }
        public long TempsExecution { get; set; }

        public Operation() { }

        public override string ToString()
        {
            return $">>> Résultat : {Resultat} effectué en {TempsExecution} ms";
        }

        public static Operation operator+ (Operation a, Operation b)
        {
            return new Operation() { Resultat = a.Resultat+b.Resultat };
        }
    }
}
