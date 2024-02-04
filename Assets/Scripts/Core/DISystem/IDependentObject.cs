namespace Core.DISystem
{
    public interface IDependentObject
    {
        void InjectDependencies(IDependencyContainer container);
    }
}