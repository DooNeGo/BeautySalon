using BeautySalon.UI.View;
using BeautySalon.UI.View.SignIn;
using BeautySalon.UI.View.SignUp;
using BeautySalon.UI.ViewModel;
using CommunityToolkit.Maui;

namespace BeautySalon.UI;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection services)
    {
        return services.AddTransient<StartView>()
                .AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
                .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
                .AddTransientWithShellRoute<CreateUserView, CreateUserViewModel>(nameof(CreateUserViewModel))
                .AddTransientWithShellRoute<MainView, MainViewModel>(nameof(MainViewModel))
                .AddTransientWithShellRoute<MastersView, MastersViewModel>(nameof(MastersViewModel))
                .AddTransientWithShellRoute<ServicesView, ServicesViewModel>(nameof(ServicesViewModel))
                .AddTransientWithShellRoute<SignUpForServicePage, SignUpForServicePageModel>(nameof(SignUpForServicePageModel))
                .AddTransientWithShellRoute<ServiceView, ServiceViewModel>(nameof(ServiceViewModel));
    }
}