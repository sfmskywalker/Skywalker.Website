using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Content Extensions",
    Author = "Sipke Schoorstra",
    Website = "https://www.skywalker-isp.nl",
    Version = "1.0.0",
    Description = "Provides various content utilities, such as liquid filters and author details.",
    Category = "Content Extensions"
)]

[assembly: Feature(
    Id = "Skywalker.OrchardCore.ContentExtensions",
    Name = "Content Extensions",
    Description = "Provides various content utilities.",
    Category = "Content Extensions"
)]

[assembly: Feature(
    Id = "Skywalker.OrchardCore.ContentExtensions.Author",
    Name = "Author",
    Description = "Extends the User object with an Author section and provides a display controller.",
    Category = "Content Extensions",
    Dependencies = new[] {"Skywalker.OrchardCore.ContentExtensions", "OrchardCore.Users"}
)]