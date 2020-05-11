﻿using System.Collections.Generic;
 using Pulumi;

 namespace Skywalker.Website.Infra
{
    public class GitHubVariablesArgs
    {
        public IDictionary<string, Input<string>> Variables { get; set; } = new Dictionary<string, Input<string>>();
    }
}