using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;

namespace BeautySalon.Infrastructure;

internal static class DataLoaderExtension
{
    internal static void LoadData(this IApplicationContext context)
    {
        if (context.Salons.Any()) return;

        var salon = new Salon("Гомель, Советская 16", new TimeOnly(9, 0), new TimeOnly(21, 0));

        List<Service> services =
        [
            new Service("Маникюр с покрытием", 40, TimeSpan.FromMinutes(90)),
            new Service("Свадебный макияж", 45, TimeSpan.FromMinutes(60)),
            new Service("Маникюр", 30, TimeSpan.FromMinutes(60)),
            new Service("Педикюр", 35, TimeSpan.FromMinutes(60)),
            new Service("Педикюр с покрытием", 45, TimeSpan.FromMinutes(90)),
            new Service("Празднечный макияж", 45, TimeSpan.FromMinutes(70)),
            new Service("Наращивание ресниц 2D", 45, TimeSpan.FromMinutes(180)),
            new Service("Наращивание ресниц 3D", 50, TimeSpan.FromMinutes(240)),
            new Service("Естественное наращивание ресниц", 40, TimeSpan.FromMinutes(180)),
            new Service("Комплекс бровей", 40, TimeSpan.FromMinutes(60)),
            new Service("Окрашевание бровей", 25, TimeSpan.FromMinutes(20))
        ];

        List<Position> positions =
        [
            new Position("Визажист") { Services = [services[1], services[5]]},
            new Position("Мастер маникюра") { Services = [services[2], services[0]]},
            new Position("Мастер педикюра") { Services = [services[3], services[4]]},
            new Position("Бровист") { Services = [services[10], services[9]]},
            new Position("Мастер рeсниц") { Services = [services[8], services[7], services[6]]},
        ];

        List<Master> masters =
        [
            new Master("Иванова", "Татьяна", null, "+375447452007", positions[0]),
            new Master("Семенова", "Мария", null, "+375447452007", positions[1]),
            new Master("Петрова", "Ольга", null, "+375447452007", positions[1]),
            new Master("Голубева", "Мария", null, "+375447452007", positions[2]),
            new Master("Ахрамович", "Мария", null, "+375447452007", positions[3]),
            new Master("Дрелько", "Дарья", null, "+375447452007", positions[4]),
            new Master("Кострома", "Матвей", null, "+375447452007", positions[4])
        ];

        salon.Masters = masters;
        salon.Positions = positions;
        salon.Services = services;

        var user = new User("qwerty", "123123123", "dasdas@gmail.com");
        var customer = new Customer("dasdas", "dsads", null, "+375447452007", user.Id);
        user.Customer = customer;

        List<Appointment> appointments =
        [
            new Appointment(DateTime.Now.AddDays(1), masters[0], customer, [services[1], services[5]]),
            new Appointment(DateTime.Now.AddDays(2), masters[1], customer, [services[2], services[0]]),
            new Appointment(DateTime.Now.AddDays(3), masters[2], customer, [services[3]]),
        ];

        context.Salons.Add(salon);
        context.Users.Add(user);
        context.Appointments.AddRange(appointments);
        context.SaveChanges();
    }
}