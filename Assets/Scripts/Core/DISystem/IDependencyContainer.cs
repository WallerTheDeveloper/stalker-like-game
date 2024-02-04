namespace Core.DISystem
{
    public interface IDependencyContainer
    {
        T GetDependency<T>();
        void Tick(IDependency dependency);
        // void TickAll();
    }
}