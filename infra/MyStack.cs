using System.Collections.Generic;
using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.ContainerService;
using Pulumi.Azure.Core;
using Pulumi.Azure.Sql;
using Pulumi.Azure.Storage;
using Pulumi.Random;

namespace Skywalker.Website.Infra
{
    internal class MyStack : Stack
    {
        public MyStack()
        {
            var env = Deployment.Instance.StackName;
            var config = new Config();

            // Create an Azure Resource Group
            var resourceGroup = new ResourceGroup($"{env}-skywalker-website");

            // Create an Azure Storage Account
            var storageAccount = new Account("storage", new AccountArgs
            {
                ResourceGroupName = resourceGroup.Name,
                AccountReplicationType = "LRS",
                AccountTier = "Standard",
            });

            // Create a container registry.
            var registry = new Registry("registry", new RegistryArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Sku = "Basic",
                AdminEnabled = true
            });

            // Create a username for SQL Server.
            var sqlUserName = "skywalker-admin";

            // Create a random password for SQL Server.
            var sqlPassword = new RandomPassword("password", new RandomPasswordArgs
            {
                Length = 20,
                Special = true,
            }).Result;

            // Create a Sql Server.
            var sqlServer = new SqlServer("sql", new SqlServerArgs
            {
                ResourceGroupName = resourceGroup.Name,
                AdministratorLogin = sqlUserName,
                AdministratorLoginPassword = sqlPassword,
                Version = "12.0",
            });

            // Create a database.
            var database = new Database("skywalker", new DatabaseArgs
            {
                ResourceGroupName = resourceGroup.Name,
                ServerName = sqlServer.Name,
                RequestedServiceObjectiveName = "S0",
            });

            var dbConnectionString = Output.Tuple(sqlServer.Name, database.Name, sqlPassword).Apply(x =>
            {
                (var server, var dbName, string pwd) = x;
                return
                    $"Server= tcp:{server}.database.windows.net;initial catalog={dbName};user ID={sqlUserName};password={pwd};Min Pool Size=0;Max Pool Size=30;Persist Security Info=true;";
            });

            // Create an app service plan.
            var plan = new Plan($"skywalker-apps", new PlanArgs
            {
                ResourceGroupName = resourceGroup.Name,
                Kind = "Linux",
                Reserved = true,
                Sku = new PlanSkuArgs
                {
                    Tier = "Basic",
                    Size = "B1"
                }
            });

            // Create an app service.
            var appService = new AppService($"skywalker-website", new AppServiceArgs
            {
                ResourceGroupName = resourceGroup.Name,
                AppServicePlanId = plan.Id,
                AppSettings = new InputMap<string>
                {
                    ["WEBSITES_ENABLE_APP_SERVICE_STORAGE"] = "false",
                    ["WEBSITE_HTTPLOGGING_RETENTION_DAYS"] = "1",
                    ["DOCKER_REGISTRY_SERVER_URL"] = registry.LoginServer.Apply(x => $"https://{x}"),
                    ["DOCKER_REGISTRY_SERVER_USERNAME"] = registry.AdminUsername,
                    ["DOCKER_REGISTRY_SERVER_PASSWORD"] = registry.AdminPassword,
                    ["DOCKER_ENABLE_CI"] = "true",
                    ["WEBSITES_PORT"] = "80"
                },
                ConnectionStrings = new AppServiceConnectionStringArgs
                {
                    Name = "db",
                    Type = "SQLAzure",
                    Value = dbConnectionString,
                },
                SiteConfig = new AppServiceSiteConfigArgs
                {
                    AlwaysOn = true,
                    LinuxFxVersion = registry.LoginServer.Apply(x => $"DOCKER|{x}/skywalker-website:latest"),
                },
                HttpsOnly = true
            });

            StorageConnectionString = storageAccount.PrimaryConnectionString;
            DatabaseConnectionString = dbConnectionString;
            RegistryServer = registry.LoginServer;
            RegistryUser = registry.AdminUsername;
            RegistryPassword = registry.AdminPassword;
            WebsiteUrl = appService.DefaultSiteHostname.Apply(x => $"https://{x}");
            
            // Push secrets to GitHub.
            var variablePrefix = env.ToUpperInvariant();
            var gitHubVariables = new GitHubVariables("github-variables", new GitHubVariablesArgs
            {
                Variables = new Dictionary<string, Input<string>>
                {
                    [$"{variablePrefix}_DOCKER_REGISTRY"] = RegistryServer,
                    [$"{variablePrefix}_DOCKER_USER"] = RegistryUser,
                    [$"{variablePrefix}_DOCKER_PASSWORD"] = RegistryPassword
                }
            });
        }

        [Output] public Output<string> StorageConnectionString { get; set; }
        [Output] public Output<string> DatabaseConnectionString { get; set; }
        [Output] public Output<string> RegistryServer { get; set; }
        [Output] public Output<string> RegistryUser { get; set; }
        [Output] public Output<string> RegistryPassword { get; set; }
        [Output] public Output<string> WebsiteUrl { get; set; }
    }
}