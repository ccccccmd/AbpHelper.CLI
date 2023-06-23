using System;
using System.IO;
using System.Threading.Tasks;
using EasyAbp.AbpHelper.Core.Commands.Generate.Crud;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace EasyAbp.AbpHelper
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            using var application = await AbpApplicationFactory.CreateAsync<AbpHelperModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });
            await application.InitializeAsync();

            Console.WriteLine("请输入项目名称:");
            var projectName = Console.ReadLine();

            Console.WriteLine($"请输入项目根目录（{projectName}.sln所在目录）:");
            var rootPath = Console.ReadLine();
            var slnFile = Path.Combine(rootPath, $"{projectName}.sln");
            if (!File.Exists(slnFile))
            {
                Console.WriteLine(
                    $"The solution file '{projectName}.sln' is not found in '{rootPath}'. Make sure you specific the right folder.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("请输入Entity实体名字:");


            var entityName = Console.ReadLine();
            var entityFilePath = Path.Combine(rootPath, "services", "DEC.Domain");
            if (!SearchFileRecursive(entityFilePath, $"{entityName}.cs"))
            {
                Console.WriteLine(
                    $"The Entity file '{entityName}.cs' is not found in '{entityFilePath}'. Make sure you specific the right name.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("============开始生成中============");
            var crud = application.ServiceProvider.GetService<CrudCommand>();

            await crud.RunCommand(new CrudCommandOption
            {
                Directory = rootPath,
                ProjectName = projectName,
                Entity = entityName,
                SkipUi = false,
                SkipLocalization = true, SeparateDto = true
            });
            Console.WriteLine("============FINISHED!============");
        }


        private static bool SearchFileRecursive(string directory, string targetFile)
        {
            var filePath = Path.Combine(directory, targetFile);
            if (File.Exists(filePath))
            {
                return true;
            }


            var subDirectories = Directory.GetDirectories(directory);
            foreach (var subDirectory in subDirectories)
            {
                if (SearchFileRecursive(subDirectory, targetFile))
                {
                    return true;
                }
            }

            return false;
        }
    }
}