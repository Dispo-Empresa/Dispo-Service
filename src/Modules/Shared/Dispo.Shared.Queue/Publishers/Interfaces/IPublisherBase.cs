namespace Dispo.Shared.Queue.Publishers.Interfaces
{
    public interface IPublisherBase
    {
        void Publish(string json);
    }
}