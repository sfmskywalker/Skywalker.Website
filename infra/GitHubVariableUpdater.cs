using System;
using System.Threading.Tasks;
using Pulumi;

namespace Skywalker.Website.Infra
{
    public class GitHubVariableUpdater
    {
        private readonly GitHubClient _client;
        private readonly bool _isDryRun;

        public GitHubVariableUpdater()
        {
            var config = new Config("gitHub");
            var owner = config.Require("owner");
            var repo = config.Require("repo");
            var token = config.Require("token");
            
            _isDryRun = Deployment.Instance.IsDryRun;
            _client = new GitHubClient(owner, repo, token);
        }

        public async Task SetSecretAsync(string name, string value)
        {
            Console.WriteLine("Updating variable {0} with {1}", name, value);
            
            if (_isDryRun)
                return;
            
            await _client.SetSecretAsync(name, value);
        }
    }
}