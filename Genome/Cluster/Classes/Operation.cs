namespace Cluster.Classes
{
    public class Operation
    {
        #region PROPRIETES
        public int Id { get; set; }
        public string IpNoeud { get; set; }
        public string Methode { get; set; }
        public string Chunck { get; set; }
        #endregion

        public Operation() { }

        public override string ToString()
        {
            return $"\n>>> Operation {Id} reçue ";
        }
    }
}
