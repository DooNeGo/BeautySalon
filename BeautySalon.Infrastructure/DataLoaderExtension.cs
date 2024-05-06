using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;

namespace BeautySalon.Infrastructure;

internal static class DataLoaderExtension
{
    internal static void LoadData(this IApplicationContext context)
    {
        if (context.Salons.Any()) return;
        
        var salon = new Salon("Гомель, Советская 16");
        
        List<Position> positions = 
        [
                new Position("Визажист"),
                new Position("Парикмахер"),
                new Position("Мастер маникюра"),
                new Position("Мастре педикюра"),
                new Position("Бровист"),
                new Position("Мастер рисниц"),
        ];

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

        List<Master> masters =
        [
                new Master("Иванова", "Татьяна", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[0].Id},
                new Master("Семенова", "Мария", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[1].Id },
                new Master("Петрова", "Ольга", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[2].Id },
                new Master("Голубева", "Мария", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[3].Id },
                new Master("Ахрамович", "Мария", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[4].Id },
                new Master("Дрелько", "Дарья", null, "+375447452007") { SalonId = salon.Id, PositionId = positions[5].Id },
        ];

        salon.Masters = masters;
        salon.Positions = positions;
        salon.Services = services;

        context.Salons.Add(salon);
        context.SaveChanges();
    }
}