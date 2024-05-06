using Autofac;
using TransactionSystem.BAL.Services.Contracts;
using TransactionSystem.BAL.Services.Implementations;
using TransactionSystem.DAL.Services.Contracts;
using TransactionSystem.DAL.Services.Implementations;
using TransactionSystem.PL;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var container = ConfigureContainer();
            var application = container.Resolve<TransactionSystemApplication>();

            await application.RunAsync();

        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static IContainer ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<TransactionSystemApplication>().AsSelf();
        builder.RegisterType<TransactionService>().As<ITransactionService>();
        builder.RegisterType<UISystemService>().As<IUISystemService>();
        builder.RegisterType<AccountRepository>().As<IAccountRepository>();


        return builder.Build();
    }
}
