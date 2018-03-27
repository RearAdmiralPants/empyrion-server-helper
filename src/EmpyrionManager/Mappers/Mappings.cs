namespace EmpyrionManager.Mappers
{
    using Abstractions;
    using Data.DTO;
    using AutoMapper;

    public static class Mappings
    {
        public static MapperConfiguration GetMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Backup, BackupInstance>();
            });

            return config;
        }
    }
}
