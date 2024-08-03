using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoRe.Core.Infrastructures;
using WoRe.Core.Interfaces;
using WoRe.Core.Kernel.Services;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IFlashcardService, FlashcardService>();

        return services;
    }

    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source=app.db"))
            .AddUnitOfWork<AppDbContext>();
    }
}
