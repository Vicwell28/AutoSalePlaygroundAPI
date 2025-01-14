namespace AutoSalePlaygroundAPI.Application.Interfaces
{
    // Sirve para especificar que command o query requiere un comportamiento en particular
    public interface IRequireValidation { }

    public interface IRequireAuthentication { }

    public interface IRequireAuthorization { }
}
