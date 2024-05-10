﻿using BeautySalon.UI.View;
using BeautySalon.UI.View.SignIn;
using BeautySalon.UI.View.SignUp;
using BeautySalon.UI.ViewModel;
using CommunityToolkit.Maui;

namespace BeautySalon.UI;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection services)
    {
        return services.AddTransientWithShellRoute<StartView, StartView>(nameof(StartView))
                .AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
                .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
                .AddTransientWithShellRoute<CreateUserView, CreateUserViewModel>(nameof(CreateUserViewModel))
                .AddTransientWithShellRoute<MainView, MainViewModel>(nameof(MainViewModel))
                .AddTransientWithShellRoute<MastersView, MastersViewModel>(nameof(MastersViewModel))
                .AddTransientWithShellRoute<ServicesView, ServicesViewModel>(nameof(ServicesViewModel))
                .AddTransientWithShellRoute<SignUpForServiceView, SignUpForServiceViewModel>(nameof(SignUpForServiceViewModel))
                .AddTransientWithShellRoute<ServiceView, ServiceViewModel>(nameof(ServiceViewModel))
                .AddTransientWithShellRoute<MasterView, MasterViewModel>(nameof(MasterViewModel));
    }
}