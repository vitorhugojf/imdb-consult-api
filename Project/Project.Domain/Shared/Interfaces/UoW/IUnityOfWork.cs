namespace Project.Domain.Shared.Interfaces.UoW
{
    public interface IUnityOfWork
    {
        bool Commit();
    }
}
