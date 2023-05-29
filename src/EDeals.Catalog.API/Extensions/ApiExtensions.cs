﻿namespace EDeals.Catalog.API.Extensions
{
    internal static class ApiExtensions
    {
        public static void AddApplicationSettings(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("appsettings.json", false, true);
            builder.Configuration.AddJsonFile("appsettings.Local.json", false, true);
            builder.Configuration.AddEnvironmentVariables();
        }
    }
}
