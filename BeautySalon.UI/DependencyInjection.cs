﻿using BeautySalon.UI.View;
using BeautySalon.UI.View.SignIn;
using BeautySalon.UI.View.SignUp;
using BeautySalon.UI.ViewModel;
using CommunityToolkit.Maui;

namespace BeautySalon.UI;

internal static class DependencyInjection
{
    public static IServiceCollection AddUI(this IServiceCollection services) =>
        services.AddTransientWithShellRoute<StartView, StartViewModel>(nameof(StartViewModel))
            .AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
            .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel))
            .AddTransientWithShellRoute<CreateUserView, CreateUserViewModel>(nameof(CreateUserViewModel))
            .AddTransientWithShellRoute<MainView, MainViewModel>(nameof(MainViewModel))
            .AddTransientWithShellRoute<MastersView, MastersViewModel>(nameof(MastersViewModel))
            .AddTransientWithShellRoute<ServicesView, ServicesViewModel>(nameof(ServicesViewModel))
            .AddTransientWithShellRoute<ChooseServicesView, ChooseServicesViewModel>(nameof(ChooseServicesViewModel))
            .AddTransientWithShellRoute<ServiceView, ServiceViewModel>(nameof(ServiceViewModel))
            .AddTransientWithShellRoute<MasterView, MasterViewModel>(nameof(MasterViewModel))
            .AddTransientWithShellRoute<ChooseMasterView, ChooseMasterViewModel>(nameof(ChooseMasterViewModel))
            .AddTransientWithShellRoute<ConfirmAppointmentView, ConfirmAppointmentViewModel>(nameof(ConfirmAppointmentViewModel))
            .AddTransientWithShellRoute<AppointmentsView, AppointmentsViewModel>(nameof(AppointmentsViewModel));
}