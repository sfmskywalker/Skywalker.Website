using Pulumi;

namespace Skywalker.Website.Infra
{
    public class GitHubVariables : ComponentResource
    {
        public GitHubVariables(string name, GitHubVariablesArgs args, ComponentResourceOptions? options = null)
            : base("github-variables", name, options)
        {
            var updater = new GitHubVariableUpdater();

            foreach (var variable in args.Variables)
            {
                variable.Value.Apply(value =>
                {
                    updater
                        .SetSecretAsync(variable.Key, value)
                        .GetAwaiter()
                        .GetResult();

                    return value;
                });
            }
        }
    }
}