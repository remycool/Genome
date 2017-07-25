namespace Cluster.Classes
{
    public interface IBusinessFactory
    {
        Operation GetCalcul1(string fileChunck);
        Operation GetCalcul2(string fileChunck);
    }
}