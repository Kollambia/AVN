namespace AVN.Model.Initializer
{
    public interface IDbInitializer
    {
        void Initialize();

        void SeedStudent();
    }
}
