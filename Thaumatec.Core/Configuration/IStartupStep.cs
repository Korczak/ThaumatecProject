namespace Thaumatec.Core.Configuration
{
    public interface IStartupStep
    {
        IStartupValidation Configure();
    }
}
