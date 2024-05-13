using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using BeautySalon.UI.View.SignIn;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ServiceViewModel(IIdentityService identityService, IMediator mediator, GlobalContext globalContext)
    : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private List<Master> _masters = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        Task.Run(async () =>
            Masters = await mediator.Send(new GetMastersByServiceIdQuery(Service.Id, globalContext.Salon.Id)));
    }

    [RelayCommand]
    private async Task SignUp()
    {
        if (identityService.CurrentUser is not null)
        {
            await Shell.Current.GoToAsync(nameof(ChooseMasterViewModel),
                new Dictionary<string, object> { { "Service", Service } });
        }
        else if (await Shell.Current.CurrentPage.DisplayAlert("Авторизация",
                     "Вы должны войти в аккаунт для записи на услугу", "Войти", "Отмена"))
        {
            await Shell.Current.GoToAsync(nameof(StartView));
        }
    }
}