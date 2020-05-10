using OrchardCore.DisplayManagement.Manifest;
using OrchardCore.Modules.Manifest;

[assembly: Theme(
    Name = "The Medium Theme",
    Author = "Sipke Schoorstra",
    Website = "https://www.skywalker-isp.nl",
    Version = ManifestConstants.OrchardCoreVersion,
    Description = "A theme based on the Mediumish theme from Wow Themes.",
    Dependencies = new[]
    {
        "OrchardCore.Taxonomies",
        "Skywalker.OrchardCore.ContentExtensions.Author",
        "Skywalker.OrchardCore.Gravatar",
    }
)]